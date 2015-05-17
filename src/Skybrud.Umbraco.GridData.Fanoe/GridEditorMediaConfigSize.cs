using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData.Fanoe {
    
    public class GridEditorMediaConfigSize : GridJsonObject {

        #region Properties
        
        [JsonProperty("width")]
        public int Width { get; private set; }

        [JsonProperty("height")]
        public int Height { get; private set; }

        #endregion

        #region Constructors

        private GridEditorMediaConfigSize(JObject obj) : base(obj) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <code>GridEditorMediaConfigSize</code> from the specified <code>JsonObject</code>.
        /// </summary>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridEditorMediaConfigSize Parse(JObject obj) {
            if (obj == null) return null;
            return new GridEditorMediaConfigSize(obj) {
                Width = obj.GetInt32("width"),
                Height = obj.GetInt32("height")
            };
        }

        #endregion
    
    }

}