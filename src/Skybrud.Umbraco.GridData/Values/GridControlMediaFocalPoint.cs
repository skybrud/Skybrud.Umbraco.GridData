using Newtonsoft.Json;

namespace Skybrud.Umbraco.GridData.Values {
    
    public class GridControlMediaFocalPoint {

        [JsonProperty("left")]
        public float Left { get; set; }
        
        [JsonProperty("top")]
        public float Top { get; set; }

    }

}