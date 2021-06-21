using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Models.Values;

namespace Skybrud.Umbraco.GridData.Models {

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
        public IGridControlValue Value { get; internal set; }

        /// <summary>
        /// Gets a reference to the editor of the control.
        /// </summary>
        [JsonProperty("editor")]
        public GridEditor Editor { get; internal set; }

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

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="json"/> object and <paramref name="area"/>.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> representing the control.</param>
        /// <param name="area">An instance of <see cref="GridArea"/> representing the parent area.</param>
        public GridControl(JObject json, GridArea area) : base(json) {
            Area = area;
        }

        internal GridControl(GridControl control) : base(control.JObject) {
            Area = control.Area;
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

        ///// <summary>
        ///// Generates the HTML for the Grid control.
        ///// </summary>
        ///// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        ///// <returns>The Grid control as an instance of <see cref="HtmlString"/>.</returns>
        //public HtmlString GetHtml(HtmlHelper helper) {

        //    // Some input validation
        //    if (helper == null) throw new ArgumentNullException(nameof(helper));

        //    // If the control isn't valid, we shouldn't render it
        //    if (Value == null || !IsValid) return new HtmlString("");

        //    // Does the control specify it's own path?
        //    GridViewAttribute attr = Value.GetType().GetCustomAttributes(true).OfType<GridViewAttribute>().FirstOrDefault();
        //    if (attr != null) return GetHtml(helper, attr.ViewPath);
            
        //    // Get the type name of the value instance
        //    string typeName = Value.GetType().Name;

        //    // Match the class name
        //    Match match1 = Regex.Match(typeName, "^GridControl(.+?)Value$");
        //    Match match2 = Regex.Match(typeName, "^(.+?)GridControl(.+?)Value$");

        //    // Render the HTML
        //    HtmlString html;
        //    if (match1.Success) {
        //        html = GetHtml(helper, "TypedGrid/Editors/" + match1.Groups[1].Value);
        //    } else if (match2.Success) {
        //        html = GetHtml(helper, "TypedGrid/Editors/" + match2.Groups[1].Value + "/" + match2.Groups[2].Value);
        //    } else {
        //        html = GetHtml(helper, Editor.Alias);
        //    }

        //    // Return the HTML
        //    return html;

        //}
        
        ///// <summary>
        ///// Generates the HTML for the Grid control.
        ///// </summary>
        ///// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        ///// <param name="partial">The alias or virtual path to the partial view for rendering the Grid control.</param>
        ///// <returns>The Grid control as an instance of <see cref="HtmlString"/>.</returns>
        //public HtmlString GetHtml(HtmlHelper helper, string partial) {

        //    // Some input validation
        //    if (helper == null) throw new ArgumentNullException(nameof(helper));
        //    if (string.IsNullOrWhiteSpace(partial)) throw new ArgumentNullException(nameof(partial));

        //    // If the control isn't valid, we shouldn't render it
        //    if (!IsValid) return new HtmlString("");

        //    // Get a wrapper for the control
        //    GridControlWrapper wrapper = GridContext.Current.GetControlWrapper(this);

        //    // If the wrapper is NULL, we shouldn't render the control
        //    if (wrapper == null) return new HtmlString("");

        //    // Prepend the path to the "Editors" folder if not already specified
        //    if (GridUtils.IsValidPartialName(partial)) {
        //        partial = "TypedGrid/Editors/" + partial;
        //    }

        //    // Render the partial view
        //    return helper.Partial(partial, wrapper);

        //}

        ///// <summary>
        ///// Generates the HTML for the Grid control based on either a partial view found using conventions, or
        ///// <paramref name="fallbackPartial"/> if a partial could not be found.
        ///// </summary>
        ///// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        ///// <param name="fallbackPartial">The fallback partial view to be used if a partial view could not be found.</param>
        ///// <returns>An instance of <see cref="T:System.Web.HtmlString" />.</returns>
        //public HtmlString GetHtmlOrFallback(HtmlHelper helper, string fallbackPartial) {

        //    // Some input validation
        //    if (helper == null) throw new ArgumentNullException(nameof(helper));

        //    // If the control isn't valid, we shouldn't render it
        //    if (Value == null || !IsValid) return new HtmlString("");

        //    // Get the type name of the value instance
        //    string typeName = Value.GetType().Name;

        //    // Match the class name
        //    Match match1 = Regex.Match(typeName, "^GridControl(.+?)Value$");
        //    Match match2 = Regex.Match(typeName, "^(.+?)GridControl(.+?)Value$");

        //    // Determine the virtual path to the partial view
        //    string partial;
        //    if (match1.Success) {
        //        partial = "TypedGrid/Editors/" + match1.Groups[1].Value;
        //    } else if (match2.Success) {
        //        partial = "TypedGrid/Editors/" + match2.Groups[1].Value + "/" + match2.Groups[2].Value;
        //    } else {
        //        partial = Editor.Alias;
        //    }

        //    // Return the HTML
        //    return GetHtmlOrFallback(helper, partial, fallbackPartial);

        //}

        ///// <summary>
        ///// Generates the HTML for the Grid control based on the specified <paramref name="partial"/> view, or
        ///// <paramref name="fallbackPartial"/> if <paramref name="partial"/> could not be found.
        ///// </summary>
        ///// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid control.</param>
        ///// <param name="partial">The alias or virtual path to the partial view for rendering the Grid control.</param>
        ///// <param name="fallbackPartial">The fallback partial view to be used if <paramref name="partial"/> isn't found.</param>
        ///// <returns>An instance of <see cref="T:System.Web.HtmlString" />.</returns>
        //public HtmlString GetHtmlOrFallback(HtmlHelper helper, string partial, string fallbackPartial) {

        //    // Some input validation
        //    if (helper == null) throw new ArgumentNullException(nameof(helper));
        //    if (string.IsNullOrWhiteSpace(partial)) throw new ArgumentNullException(nameof(partial));

        //    // If the control isn't valid, we shouldn't render it
        //    if (!IsValid) return new HtmlString("");

        //    // Get a wrapper for the control
        //    GridControlWrapper wrapper = GridContext.Current.GetControlWrapper(this);

        //    // If the wrapper is NULL, we shouldn't render the control
        //    if (wrapper == null) return new HtmlString("");

        //    // Prepend the path to the "Editors" folder if not already specified
        //    if (GridUtils.IsValidPartialName(partial)) {
        //        partial = "TypedGrid/Editors/" + partial;
        //    }

        //    // Prepend the path to the "Editors" folder if not already specified
        //    if (GridUtils.IsValidPartialName(fallbackPartial)) {
        //        fallbackPartial = "TypedGrid/Editors/" + fallbackPartial;
        //    }

        //    // Render the partial view
        //    return helper.ViewExists(partial) ? helper.Partial(partial, wrapper) : helper.Partial(fallbackPartial, wrapper);

        //}
        
        public void WriteSearchableText(GridContext context, TextWriter writer) {
            Value?.WriteSearchableText(context, writer);
        }
        
        /// <summary>
        /// Gets the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>An instance of <see cref="string"/> with the value as a searchable text.</returns>
        public string GetSearchableText(GridContext context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        #endregion

    }
    
    public class GridControl<TValue> : GridControl where TValue : IGridControlValue {

        public new TValue Value => (TValue) base.Value;

        public GridControl(GridControl control) : base(control) { }

    }

}