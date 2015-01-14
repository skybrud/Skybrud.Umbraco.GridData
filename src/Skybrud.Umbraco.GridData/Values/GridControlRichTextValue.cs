using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlRichTextValue : GridControlHtmlValue { }

}