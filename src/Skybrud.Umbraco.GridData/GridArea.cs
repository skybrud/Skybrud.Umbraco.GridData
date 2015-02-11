using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {
    using System.Collections.Generic;

    public class GridArea {

        [JsonProperty("grid")]
        public int Grid { get; set; }

        [JsonProperty("allowAll")]
        public bool AllowAll { get; set; }

        [JsonProperty("allowed")]
        public string[] Allowed { get; set; }

        [JsonProperty("controls")]
        public GridControl[] Controls { get; set; }

        public Dictionary<string, string> Settings { get; set; }

        public static GridArea Parse(JObject obj) {
            JArray allowed = obj.GetArray("allowed");
            return new GridArea {
                Grid = obj.GetInt32("grid"),
                AllowAll = obj.GetBoolean("allowAll"),
                Allowed = allowed == null ? new string[0] : allowed.Select(x => (string) x).ToArray(),
                Controls = obj.GetArray("controls", GridControl.Parse),
                Settings = GridCustomSetting.Parse(obj)
            };
        }

    }

}