using Newtonsoft.Json;

namespace Skybrud.Umbraco.GridData {

    public class GridEditorConfig {

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("markup")]
        public string Markup { get; set; }

    }

}