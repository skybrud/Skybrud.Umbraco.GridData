using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the rich text value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlRichTextValue : GridControlHtmlValue {

        #region Constructors

        protected GridControlRichTextValue(GridControl control, JToken token) : base(control, token) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a rich text value from the specified <code>JToken</code>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <code>JToken</code> to be parsed.</param>
        public new static GridControlRichTextValue Parse(GridControl control, JToken token) {
            return token == null ? null : new GridControlRichTextValue(control, token);
        }

        #endregion

    }

}