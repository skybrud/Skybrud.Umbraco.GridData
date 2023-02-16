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
    public class GridControlEmbedValue : GridControlValueBase<JObject> {

        #region Properties

        /// <summary>
        /// Gets a string representing the value.
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// Gets an instance of <see cref="IHtmlContent"/> representing the text value.
        /// </summary>
        [JsonIgnore]
        public IHtmlContent HtmlValue { get; protected set; }

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlEmbedValue"/>, this means
        /// checking whether the <see cref="Value"/> property has a value.
        /// </summary>
        [JsonIgnore]
        public override bool IsValid => string.IsNullOrWhiteSpace(Value) == false;

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
        public IHtmlContent Preview => HtmlValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the value of the specified grid <paramref name="control"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the parent grid control.</param>
        public GridControlEmbedValue( GridControl control) : base(control) {

            Value = Json.GetString("preview")!;
            HtmlValue = new HtmlString(Value);

            Width = Json.GetInt32("width");
            Height = Json.GetInt32("height");
            Url = Json.GetString("url")!;
            Info = Json.GetString("info") ?? string.Empty;

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
        public override void WriteSearchableText(GridContext context, TextWriter writer) { }

        /// <summary>
        /// Returns a string representation of this value.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>A string representation of this value.</returns>
        public override string GetSearchableText(GridContext context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        #endregion

    }

}