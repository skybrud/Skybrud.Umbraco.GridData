using System.IO;
using System.Text;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Umbraco.GridData.Json.Converters;
using Umbraco.Extensions;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Class representing the embed value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlEmbedValue : IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        public GridControl Control { get; }

        /// <summary>
        /// Gets a reference to the underlying instance of <see cref="JToken"/>.
        /// </summary>
        public JToken JToken { get; }

        /// <summary>
        /// Gets a string representing the value.
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// Gets an instance of <see cref="HtmlString"/> representing the text value.
        /// </summary>
        [JsonIgnore]
        public HtmlString HtmlValue { get; protected set; }

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlEmbedValue"/>, this means
        /// checking whether the <see cref="Value"/> property has a value.
        /// </summary>
        [JsonIgnore]
        public bool IsValid => string.IsNullOrWhiteSpace(Value) == false;

        /// <summary>
        /// Gets the width of the embed.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets the height of the embed.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// Gets the url of the embed.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets the info of the embed.
        /// </summary>
        [JsonProperty("info")]
        public string Info { get; set; }

        /// <summary>
        /// Gets the preview html of the embed.
        /// </summary>
        [JsonProperty("preview")]
        public HtmlString Preview => HtmlValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the value of the control</param>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        public GridControlEmbedValue(JObject json, GridControl control) {

            Control = control;
            JToken = json;

            Value = json.GetString("preview")!;
            HtmlValue = new HtmlString(Value);

            Width = json.GetInt32("width");
            Height = json.GetInt32("height");
            Url = json.GetString("url")!;
            Info = json.GetString("info") ?? string.Empty;

        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets a string representing the raw value of the control.
        /// </summary>
        /// <returns>An instance of <see cref="string"/>.</returns>
        public override string ToString() {
            return HtmlValue.ToHtmlString();
        }

        /// <summary>
        /// Gets a HTML representing the value of the control.
        /// </summary>
        /// <returns>An instance of <see cref="string"/>.</returns>
        public string ToHtmlString() {
            return Value;
        }

        /// <summary>
        /// Writes a string representation of this value to <paramref name="writer"/>.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <param name="writer">The writer.</param>
        public virtual void WriteSearchableText(GridContext context, TextWriter writer) { }

        /// <summary>
        /// Returns a string representation of this value.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>A string representation of this value.</returns>
        public virtual string GetSearchableText(GridContext context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        #endregion

    }

}