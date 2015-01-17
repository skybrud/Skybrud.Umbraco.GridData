using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData.Values {

    public class GridControlMediaValue : IGridControlValue {

        [JsonProperty("focalPoint")]
        public GridControlMediaFocalPoint FocalPoint { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore)]
        public string Caption { get; set; }
        
    }

}