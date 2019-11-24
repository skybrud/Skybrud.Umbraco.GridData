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
using Skybrud.Umbraco.GridData.Factories;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;
using Skybrud.Umbraco.GridData.Models.Editors;
using Skybrud.Umbraco.GridData.Rendering;
using Umbraco.Core.Logging;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Class representing a control in an Umbraco Grid.
    /// </summary>
    public class GridControl : GridJsonObject, IGridControl {

        #region Properties

        /// <summary>
        /// Gets a reference to the entire <see cref="IGridDataModel"/>.
        /// </summary>
        [JsonIgnore]
        public IGridDataModel Model => Section?.Model;

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridSection"/>.
        /// </summary>
        [JsonIgnore]
        public IGridSection Section => Row?.Section;

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridRow"/>.
        /// </summary>
        [JsonIgnore]
        public IGridRow Row => Area?.Row;

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridArea"/>.
        /// </summary>
        [JsonIgnore]
        public IGridArea Area { get; private set; }

        /// <summary>
        /// Gets the value of the control. Alternately use the <see cref="GetValue{T}"/> method to get the type safe value.
        /// </summary>
        [JsonProperty("value")]
        public IGridControlValue Value { get; }

        /// <summary>
        /// Gets a reference to the editor of the control.
        /// </summary>
        [JsonProperty("editor")]
        public IGridEditor Editor { get; }

        /// <summary>
        /// Gets a reference to the previous control.
        /// </summary>
        public IGridControl PreviousControl { get; internal set; }

        /// <summary>
        /// Gets a reference to the next control.
        /// </summary>
        public IGridControl NextControl { get; internal set; }

        /// <summary>
        /// Gets whether the control and it's value is valid.
        /// </summary>
        [JsonIgnore]
        public bool IsValid => Value != null && Value.IsValid;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="json"/> object, <paramref name="area"/> and <paramref name="factory"/>.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> representing the control.</param>
        /// <param name="area">An instance of <see cref="IGridArea"/> representing the parent area.</param>
        /// <param name="factory">The factory used for parsing subsequent parts of the grid.</param>
        public GridControl(JObject json, IGridArea area, IGridFactory factory) : base(json) {

            // Set basic properties
            Area = area;

            Howdy.ReplaceEditorObjectFromConfig(this);

            // Parse the editor
            Editor = json.GetObject("editor", x => factory.CreateGridEditor(x, this));

            // Parse the control value
            JToken value = json.GetValue("value");
            foreach (IGridConverter converter in GridContext.Current.Converters) {
                try {
                    if (!converter.ConvertControlValue(this, value, out IGridControlValue converted)) continue;
                    Value = converted;
                    break;
                } catch (Exception ex) {
                    global::Umbraco.Core.Composing.Current.Logger.Error<GridControl>(ex, "Converter of type " + converter + " failed for ConvertControlValue()");
                }
            }

        }

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
            if (string.IsNullOrWhiteSpace(partial)) throw new ArgumentNullException(nameof(partial));

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
            if (string.IsNullOrWhiteSpace(partial)) throw new ArgumentNullException(nameof(partial));

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
        /// Returns the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <returns>An instance of <see cref="string"/> with the value as a searchable text.</returns>
        public virtual string GetSearchableText() {
            return IsValid ? Value?.GetSearchableText() ?? string.Empty : string.Empty;
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

    }

}
