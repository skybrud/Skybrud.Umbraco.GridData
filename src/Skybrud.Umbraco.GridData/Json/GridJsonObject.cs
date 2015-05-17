using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Json {
    
    /// <summary>
    /// Class representing an object derived from an instance of <code>JObject</code>.
    /// </summary>
    [JsonConverter(typeof(GridJsonConverter))]
    public class GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a reference to the underlying instance of <code>JObject</code>.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        #endregion

        #region Constructors

        /// <param name="obj">The underlying instance of <code>JObject</code>.</param>
        public GridJsonObject(JObject obj) {
            JObject = obj;
        }

        #endregion

    }

}