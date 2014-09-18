using System.Web;
using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlHtmlValue : GridControlTextValue {

        private HtmlString _html;
        
        public override string Value {
            get { return base.Value; }
            set { base.Value = value; _html = new HtmlString(value); }
        }

        [JsonIgnore]
        public virtual HtmlString HtmlValue {
            get { return _html; }
        }
        
    }

}