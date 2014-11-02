using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {
    
    public class GridArea {

        [JsonProperty("grid")]
        public int Grid { get; set; }

        [JsonProperty("editors", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Editors { get; set; }

        [JsonProperty("controls")]
        public GridControl[] Controls { get; set; }

        public static GridArea Parse(JObject obj) {

            JArray editors = obj.GetArray("editors");

            return new GridArea {
                Grid = obj.GetInt32("grid"),
                Editors = editors == null ? new string[0] : editors.Select(x => (string) x).ToArray(),
                Controls = obj.GetArray("controls", GridControl.Parse)
            };

        }

    }

}