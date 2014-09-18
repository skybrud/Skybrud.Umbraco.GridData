using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData.Values {
    
    public class GridControlMediaValue : IGridControlValue {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }
        
    }

}