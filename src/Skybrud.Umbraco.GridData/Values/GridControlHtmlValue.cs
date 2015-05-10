using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the HTML value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlHtmlValue : GridControlTextValue {

        #region Private fields

        private HtmlString _html;

        #endregion

        #region Properties

        public override string Value {
            get { return base.Value; }
            protected set { base.Value = value; _html = new HtmlString(value); }
        }

        /// <summary>
        /// Gets an instance of <code>HtmlString</code> representing the text value.
        /// </summary>
        [JsonIgnore]
        public virtual HtmlString HtmlValue {
            get { return _html; }
        }

        #endregion

        #region Constructors

        protected GridControlHtmlValue(GridControl control, JToken token) : base(control, token) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a text value from the specified <code>JToken</code>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <code>JToken</code> to be parsed.</param>
        public new static GridControlTextValue Parse(GridControl control, JToken token) {
            return token == null ? null : new GridControlHtmlValue(control, token);
        }

        #endregion
        
    }

}