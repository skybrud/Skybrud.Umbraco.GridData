using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;
using Skybrud.Umbraco.GridData.Rendering;
using Umbraco.Core.Configuration;
using Umbraco.Core.Logging;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing a control in an Umbraco Grid.
    /// </summary>
    public class GridControl : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <see cref="GridArea"/>.
        /// </summary>
        [JsonIgnore]
        public GridArea Area { get; private set; }

        /// <summary>
        /// Gets the value of the control. Alternately use the <code>GetValue&lt;T&gt;</code> method to get the type safe value.
        /// </summary>
        [JsonProperty("value")]
        public IGridControlValue Value { get; private set; }

        /// <summary>
        /// Gets a reference to the editor of the control.
        /// </summary>
        [JsonProperty("editor")]
        public GridEditor Editor { get; private set; }

        /// <summary>
        /// Gets a reference to the previous control.
        /// </summary>
        public GridControl PreviousControl { get; internal set; }

        /// <summary>
        /// Gets a reference to the next control.
        /// </summary>
        public GridControl NextControl { get; internal set; }

        /// <summary>
        /// Gets whether the control and it's value is valid.
        /// </summary>
        [JsonIgnore]
        public bool IsValid {
            get { return Value != null && Value.IsValid; }
        }

        #endregion

        #region Constructors

        private GridControl(JObject obj) : base(obj) { }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the value of the control casted to the type of <code>T</code>.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        public T GetValue<T>() where T : IGridControlValue {
            return (T) Value;
        }

        #region Member methods

        /// <summary>
        /// Generates the HTML for the Grid control.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        /// <returns>Returns the Grid control as an instance of <see cref="HtmlString"/>.</returns>
        public HtmlString GetHtml(HtmlHelper helper) {
            return GetHtml(helper, Editor.Alias);
        }

        /// <summary>
        /// Generates the HTML for the Grid control.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        /// <param name="partial">The alias or virtual path to the partial view for rendering the Grid control.</param>
        /// <returns>Returns the Grid control as an instance of <see cref="HtmlString"/>.</returns>
        public HtmlString GetHtml(HtmlHelper helper, string partial) {

            // Some input validation
            if (helper == null) throw new ArgumentNullException("helper");
            if (String.IsNullOrWhiteSpace(partial)) throw new ArgumentNullException("partial");

            // If the control isn't valid, we shouldn't render it
            if (!IsValid) return new HtmlString("");

            // Get a wrapper for the control
            GridControlWrapper wrapper = GridContext.Current.GetControlWrapper(this);

            // If the wrapper is NULL, we shouldn't render the control
            if (wrapper == null) return new HtmlString("");

            // Prepend the path to the "Controls" folder if not already specified
            if (!partial.StartsWith("~/") && !partial.StartsWith("~/")) {
                partial = "~/Views/Partials/TypedGrid/Controls/" + partial;
            }

            // Append the ".cshtml" extension if not already specified
            if (!partial.EndsWith(".cshtml")) partial += ".cshtml";

            // Render the partial view
            return helper.Partial(partial, wrapper);

        }

        /// <summary>
        /// Gets the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <returns>Returns an instance of <see cref="System.String"/> with the value as a searchable text.</returns>
        public virtual string GetSearchableText() {
            return IsValid ? Value.GetSearchableText() : "";
        }

        #endregion

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a control from the specified <code>obj</code>.
        /// </summary>
        /// <param name="area">The parent area of the control.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridControl Parse(GridArea area, JObject obj) {
            
            // Set basic properties
            GridControl control = new GridControl(obj) {
                Area = area
            };

            // As of Umbraco 7.3, information about the editor is no longer saved in the JSON, since these should be read from the configuration
            if (UmbracoVersion.Current.Major == 7 && UmbracoVersion.Current.Minor >= 3) Howdy.ReplaceEditorObjectFromConfig(control);

            // Parse the editor
            control.Editor = obj.GetObject("editor", x => GridEditor.Parse(control, x));

            // Parse the control value
            JToken value = obj.GetValue("value");
            foreach (IGridConverter converter in GridContext.Current.Converters) {
                try {
                    IGridControlValue converted;
                    if (!converter.ConvertControlValue(control, value, out converted)) continue;
                    control.Value = converted;
                    break;
                } catch (Exception ex) {
                    LogHelper.Error<GridControl>("Converter of type " + converter + " failed for ConvertControlValue()", ex);
                }
            }
            
            return control;

        }

        #endregion

    }

}