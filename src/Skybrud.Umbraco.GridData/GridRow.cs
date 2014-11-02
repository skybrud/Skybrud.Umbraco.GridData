using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {

    public class GridRow {

        [JsonProperty("id", Order = 3)]
        public string Id { get; set; }

        [JsonProperty("name", Order = 1)]
        public string Name { get; set; }

        [JsonProperty("areas", Order = 2)]
        public GridArea[] Areas { get; set; }

        public static GridRow Parse(JObject obj) {
            return new GridRow {
                Id = obj.GetString("id"),
                Name = obj.GetString("name"),
                Areas = obj.GetArray("areas", GridArea.Parse) ?? new GridArea[0]
            };
        }

    }

}