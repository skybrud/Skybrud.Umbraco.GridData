using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {
    
    public class GridCell {

        [JsonProperty("controls")]
        public GridControl[] Controls { get; set; }

        public static GridCell Parse(JObject obj) {
            return new GridCell {
                Controls = obj.GetArray("controls", GridControl.Parse)
            };
        }

    }

}