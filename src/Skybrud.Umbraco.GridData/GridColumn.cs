using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {
    
    public class GridColumn {

        [JsonProperty("grid")]
        public int Grid { get; set; }

        [JsonProperty("percentage")]
        public double Percentage { get; set; }

        [JsonProperty("rows")]
        public GridRow[] Rows { get; set; }

        public static GridColumn Parse(JObject obj) {
            return new GridColumn {
                Grid = obj.GetInt32("grid"),
                Percentage = obj.GetDouble("percentage"),
                Rows = obj.GetArray("rows", GridRow.Parse) ?? new GridRow[0]
            };
        }

    }

}
