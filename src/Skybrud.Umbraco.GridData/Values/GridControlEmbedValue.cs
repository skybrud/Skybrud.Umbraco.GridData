using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Values
{

    /// <summary>
    /// Class representing the embed value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlEmbedValue : GridControlValueBase
    {

        #region Properties
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
        public HtmlString Preview { get; set; }

        /// <summary>
        /// Gets an instance of <see cref="HtmlString"/> representing the text value.
        /// </summary>
        [JsonIgnore]
        public HtmlString HtmlValue { get; }

        /// <summary>
        /// Gets whether the value of the control is valid.
        /// </summary>
        public override bool IsValid => !String.IsNullOrWhiteSpace(HtmlValue.ToHtmlString());


        #endregion

        #region Member methods

        /// <summary>
        /// Gets a string representing the raw value of the control.
        /// </summary>
        /// <returns>An instance of <see cref="System.String"/>.</returns>
        public override string ToString()
        {
            return HtmlValue.ToHtmlString();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="token">An instance of <see cref="JToken"/> representing the value of the control</param>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the value of the control.</param>
        protected GridControlEmbedValue(GridControl control, JToken token, JObject obj = null) : base(control, obj)
        {
            if (obj == null)
            {
                HtmlValue = new HtmlString(token.ToString());
            }
            else
            {
                Width = obj.GetInt16("width");
                Height = obj.GetInt16("height");
                Url = obj.GetString("url");
                Info = obj.GetString("info");
                var previewValue = obj.GetString("preview");
                if (!string.IsNullOrEmpty(previewValue))
                {
                    Preview = new HtmlString(previewValue);
                    HtmlValue = Preview;
                }
            }
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an embed value from the specified <paramref name="control"/> and <paramref name="token"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <code>JToken</code> to be parsed.</param>
        public static GridControlEmbedValue Parse(GridControl control, JToken token)
        {
            if (token != null)
            {
                // legacy: JValue
                if (token.GetType() == typeof(JValue))
                {
                    return new GridControlEmbedValue(control, token);
                }
                // Umbraco 8.2+ JObject
                return new GridControlEmbedValue(control, null, token as JObject);
            }

            return null;
        }

        #endregion

    }

}