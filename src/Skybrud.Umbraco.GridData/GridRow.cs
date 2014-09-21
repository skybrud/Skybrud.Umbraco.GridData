using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {

    public class GridRow {

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("cells")]
        public GridCell[] Cells { get; set; }

        public static GridRow Parse(JObject obj) {
            return new GridRow {
                UniqueId = obj.GetString("uniqueId"),
                Cells = obj.GetArray("areas", GridCell.Parse) ?? obj.GetArray("cells", GridCell.Parse) ?? new GridCell[0]
            };
        }

    }

}