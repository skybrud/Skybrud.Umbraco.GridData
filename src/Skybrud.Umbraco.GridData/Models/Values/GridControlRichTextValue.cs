using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Class representing the rich text value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlRichTextValue : GridControlHtmlValue {

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="token"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="token">An instance of <see cref="JToken"/> representing the value of the control.</param>
        public GridControlRichTextValue(GridControl control, JToken? token) : base(control, token) { }

    }

}