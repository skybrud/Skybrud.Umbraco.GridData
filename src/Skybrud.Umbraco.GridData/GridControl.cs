using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Attributes;
using Skybrud.Umbraco.GridData.Extensions;
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
        /// Gets a reference to the entire <see cref="GridDataModel"/>.
        /// </summary>
        [JsonIgnore]
        public GridDataModel Model => Section?.Model;

        /// <summary>
        /// Gets a reference to the parent <see cref="GridSection"/>.
        /// </summary>
        [JsonIgnore]
        public GridSection Section => Row?.Section;

        /// <summary>
        /// Gets a reference to the parent <see cref="GridRow"/>.
        /// </summary>
        [JsonIgnore]
        public GridRow Row => Area?.Row;

        /// <summary>
        /// Gets a reference to the parent <see cref="GridArea"/>.
        /// </summary>
        [JsonIgnore]
        public GridArea Area { get; private set; }

        /// <summary>
        /// Gets the value of the control. Alternately use the <see cref="GetValue{T}"/> method to get the type safe value.
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
        public bool IsValid => Value != null && Value.IsValid;

        #endregion

        #region Constructors

        private GridControl(JObject obj) : base(obj) { }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the value of the control casted to the type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        public T GetValue<T>() where T : IGridControlValue {
            return (T) Value;
        }

        /// <summary>
        /// Generates the HTML for the Grid control.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        /// <returns>Returns the Grid control as an instance of <see cref="HtmlString"/>.</returns>
        public HtmlString GetHtml(HtmlHelper helper) {

            // Some input validation
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            // If the control isn't valid, we shouldn't render it
            if (Value == null || !IsValid) return new HtmlString("");

            // Does the control specify it's own path?
            GridViewAttribute attr = Value.GetType().GetCustomAttributes(true).OfType<GridViewAttribute>().FirstOrDefault();
            if (attr != null) return GetHtml(helper, attr.ViewPath);
            
            // Get the type name of the value instance
            string typeName = Value.GetType().Name;

            // Match the class name
            Match match1 = Regex.Match(typeName, "^GridControl(.+?)Value$");
            Match match2 = Regex.Match(typeName, "^(.+?)GridControl(.+?)Value$");

            // Render the HTML
            HtmlString html;
            if (match1.Success) {
                html = GetHtml(helper, "TypedGrid/Editors/" + match1.Groups[1].Value);
            } else if (match2.Success) {
                html = GetHtml(helper, "TypedGrid/Editors/" + match2.Groups[1].Value + "/" + match2.Groups[2].Value);
            } else {
                html = GetHtml(helper, Editor.Alias);
            }

            // Return the HTML
            return html;

        }
        
        /// <summary>
        /// Generates the HTML for the Grid control.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        /// <param name="partial">The alias or virtual path to the partial view for rendering the Grid control.</param>
        /// <returns>Returns the Grid control as an instance of <see cref="HtmlString"/>.</returns>
        public HtmlString GetHtml(HtmlHelper helper, string partial) {

            // Some input validation
            if (helper == null) throw new ArgumentNullException(nameof(helper));
            if (String.IsNullOrWhiteSpace(partial)) throw new ArgumentNullException(nameof(partial));

            // If the control isn't valid, we shouldn't render it
            if (!IsValid) return new HtmlString("");

            // Get a wrapper for the control
            GridControlWrapper wrapper = GridContext.Current.GetControlWrapper(this);

            // If the wrapper is NULL, we shouldn't render the control
            if (wrapper == null) return new HtmlString("");

            // Prepend the path to the "Editors" folder if not already specified
            if (GridUtils.IsValidPartialName(partial)) {
                partial = "TypedGrid/Editors/" + partial;
            }

            // Render the partial view
            return helper.Partial(partial, wrapper);

        }

        /// <summary>
        /// Generates the HTML for the Grid control based on either a partial view found using conventions, or
        /// <paramref name="fallbackPartial"/> if a partial could not be found.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        /// <param name="fallbackPartial">The fallback partial view to be used if a partial view could not be found.</param>
        /// <returns>An instance of <see cref="T:System.Web.HtmlString" />.</returns>
        public HtmlString GetHtmlOrFallback(HtmlHelper helper, string fallbackPartial) {

            // Some input validation
            if (helper == null) throw new ArgumentNullException(nameof(helper));

            // If the control isn't valid, we shouldn't render it
            if (Value == null || !IsValid) return new HtmlString("");

            // Get the type name of the value instance
            string typeName = Value.GetType().Name;

            // Match the class name
            Match match1 = Regex.Match(typeName, "^GridControl(.+?)Value$");
            Match match2 = Regex.Match(typeName, "^(.+?)GridControl(.+?)Value$");

            // Determine the virtual path to the partial view
            string partial;
            if (match1.Success) {
                partial = "TypedGrid/Editors/" + match1.Groups[1].Value;
            } else if (match2.Success) {
                partial = "TypedGrid/Editors/" + match2.Groups[1].Value + "/" + match2.Groups[2].Value;
            } else {
                partial = Editor.Alias;
            }

            // Return the HTML
            return GetHtmlOrFallback(helper, partial, fallbackPartial);

        }

        /// <summary>
        /// Generates the HTML for the Grid control based on the specified <paramref name="partial"/> view, or
        /// <paramref name="fallbackPartial"/> if <paramref name="partial"/> could not be found.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        /// <param name="partial">The alias or virtual path to the partial view for rendering the Grid control.</param>
        /// <param name="fallbackPartial">The fallback partial view to be used if <paramref name="partial"/> isn't found.</param>
        /// <returns>An instance of <see cref="T:System.Web.HtmlString" />.</returns>
        public HtmlString GetHtmlOrFallback(HtmlHelper helper, string partial, string fallbackPartial) {

            // Some input validation
            if (helper == null) throw new ArgumentNullException(nameof(helper));
            if (String.IsNullOrWhiteSpace(partial)) throw new ArgumentNullException(nameof(partial));

            // If the control isn't valid, we shouldn't render it
            if (!IsValid) return new HtmlString("");

            // Get a wrapper for the control
            GridControlWrapper wrapper = GridContext.Current.GetControlWrapper(this);

            // If the wrapper is NULL, we shouldn't render the control
            if (wrapper == null) return new HtmlString("");

            // Prepend the path to the "Editors" folder if not already specified
            if (GridUtils.IsValidPartialName(partial)) {
                partial = "TypedGrid/Editors/" + partial;
            }

            // Prepend the path to the "Editors" folder if not already specified
            if (GridUtils.IsValidPartialName(fallbackPartial)) {
                fallbackPartial = "TypedGrid/Editors/" + fallbackPartial;
            }

            // Render the partial view
            return helper.ViewExists(partial) ? helper.Partial(partial, wrapper) : helper.Partial(fallbackPartial, wrapper);

        }

        /// <summary>
        /// Gets the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <returns>An instance of <see cref="System.String"/> with the value as a searchable text.</returns>
        public virtual string GetSearchableText() {
            return IsValid ? Value?.GetSearchableText() ?? String.Empty : String.Empty;
        }

        /// <summary>
        /// Initializes a new control wrapper around the control.
        /// </summary>
        /// <typeparam name="TValue">The type of the control value.</typeparam>
        public GridControlWrapper<TValue> GetControlWrapper<TValue>() where TValue : IGridControlValue {

            // Get the value
            TValue value = GetValue<TValue>();

            // Wrap the control
            return new GridControlWrapper<TValue>(this, value);

        }

        /// <summary>
        /// Initializes a new control wrapper around the control.
        /// </summary>
        /// <typeparam name="TValue">The type of the control value.</typeparam>
        /// <typeparam name="TConfig">The type of the editor config.</typeparam>
        public GridControlWrapper<TValue, TConfig> GetControlWrapper<TValue, TConfig>() where TValue : IGridControlValue where TConfig : IGridEditorConfig {

            // Get the value
            TValue value = GetValue<TValue>();

            // Get the configuration
            TConfig config = Editor.GetConfig<TConfig>();

            // Wrap the control
            return new GridControlWrapper<TValue, TConfig>(this, value, config);

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a control from the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="area">The parent area of the control.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridControl Parse(GridArea area, JObject obj) {
            
            // Set basic properties
            GridControl control = new GridControl(obj) {
                Area = area
            };

            Howdy.ReplaceEditorObjectFromConfig(control);

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
                    global::Umbraco.Core.Composing.Current.Logger.Error<GridControl>(ex, "Converter of type " + converter + " failed for ConvertControlValue()");
                }
            }
            
            return control;

        }

        #endregion

    }

}
