using System;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the HTML value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlHtmlValue : GridControlTextValue, IHtmlString {

        #region Properties

        /// <summary>
        /// Gets an instance of <see cref="HtmlString"/> representing the text value.
        /// </summary>
        [JsonIgnore]
        public HtmlString HtmlValue { get; protected set; }

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlHtmlValue"/>, this means
        /// checking whether the specified text is not an empty string (using <see cref="string.IsNullOrWhiteSpace"/>
        /// against the value returned by the <see cref="GetSearchableText"/> method).
        /// </summary>
        [JsonIgnore]
        public override bool IsValid => !string.IsNullOrWhiteSpace(Regex.Replace(Value, "<(p|/p)>", string.Empty));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="token"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="token">An instance of <see cref="JToken"/> representing the value of the control.</param>
        protected GridControlHtmlValue(GridControl control, JToken token) : base(control, token) {
            HtmlValue = new HtmlString(Value);
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>An instance of <see cref="string"/> with the value as a searchable text.</returns>
        public override string GetSearchableText(GridContext context) {
            return Regex.Replace(Value, "<.*?>", " ") + Environment.NewLine;
        }
        
        /// <summary>
        /// Gets a string representing the raw value of the control.
        /// </summary>
        /// <returns>An instance of <see cref="string"/>.</returns>
        public string ToHtmlString() {
            return Value;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a text value from the specified <paramref name="control"/> and <paramref name="token"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <see cref="JToken"/> to be parsed.</param>
        public new static GridControlTextValue Parse(GridControl control, JToken token) {
            return token == null ? null : new GridControlHtmlValue(control, token);
        }

        #endregion

    }

}