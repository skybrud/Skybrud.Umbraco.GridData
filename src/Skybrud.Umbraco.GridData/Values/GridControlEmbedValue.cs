using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the embed value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlEmbedValue : GridControlValueBase {

        /// <summary>
        /// Gets the syntax of the macro.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }
        /// <summary>
        /// Gets the syntax of the macro.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }
        /// <summary>
        /// Gets the syntax of the macro.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
        /// <summary>
        /// Gets the syntax of the macro.
        /// </summary>
        [JsonProperty("info")]
        public string Info { get; set; }
        /// <summary>
        /// Gets the syntax of the macro.
        /// </summary>
        [JsonProperty("preview")]
        public HtmlString Preview { get; set; }
        /// <summary>
        /// Gets an instance of <see cref="HtmlString"/> representing the text value.
        /// </summary>
        [JsonIgnore]
        [Obsolete("Use Preview")]
        public HtmlString HtmlValue { get; }



        #region Properties

        /// <summary>
        /// Gets whether the value of the control is valid.
        /// </summary>
        public override bool IsValid => !String.IsNullOrWhiteSpace(Url);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the value of the control.</param>
        
        protected GridControlEmbedValue(GridControl control, JObject obj) : base(control, obj)
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


        /// <summary>
        /// Gets a string representing the raw value of the control.
        /// </summary>
        /// <returns>An instance of <see cref="System.String"/>.</returns>
        public override string ToString()
        {
            return Preview.ToString();
        }
        
        #endregion

        #region Static methods

        /// <summary>
        /// Gets an embed value from the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public new static GridControlEmbedValue Parse(GridControl control, JObject obj)
        {
            return obj == null ? null : new GridControlEmbedValue(control, obj);
        }

        #endregion

    }

}