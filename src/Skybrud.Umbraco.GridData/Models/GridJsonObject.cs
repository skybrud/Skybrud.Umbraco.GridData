using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Models {
    
    /// <summary>
    /// Class representing an object derived from an instance of <see cref="JObject"/>.
    /// </summary>
    [JsonConverter(typeof(GridJsonConverter))]
    public class GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a reference to the underlying instance of <see cref="JObject"/>.
        /// </summary>
        [JsonIgnore]
        public JObject? JObject { get; }

        #endregion

        #region Constructors

        /// <param name="obj">The underlying instance of <see cref="JObject"/>.</param>
        public GridJsonObject(JObject? obj) {
            JObject = obj;
        }

        #endregion

    }

}