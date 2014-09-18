using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Converters;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData.Values {

    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlTextValue : IGridControlValue {

        [JsonProperty("value")]
        public virtual string Value { get; set; }
        
    }

}