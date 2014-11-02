using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.Umbraco.GridData {
    
    public class GridEditor {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("view")]
        public string View { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        public JObject Config { get; set; }

    }

}