using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {
    
    public class GridSection {

        [JsonProperty("grid")]
        public int Grid { get; set; }

        [JsonProperty("rows")]
        public GridRow[] Rows { get; set; }

        public static GridSection Parse(JObject obj) {
            return new GridSection {
                Grid = obj.GetInt32("grid"),
                Rows = obj.GetArray("rows", GridRow.Parse) ?? new GridRow[0]
            };
        }

    }

}
