using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Umbraco.GridData.Models.Config {
    
    /// <summary>
    /// Class describing the desired size of a media (image).
    /// </summary>
    public class GridEditorMediaConfigSize : GridJsonObject {

        #region Properties
        
        /// <summary>
        /// Gets the desired width of the media.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; }

        /// <summary>
        /// Gets the desired height of the media.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; }

        #endregion

        #region Constructors

        private GridEditorMediaConfigSize(JObject json) : base(json) {
            Width = json.GetInt32("width");
            Height = json.GetInt32("height");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridEditorMediaConfigSize"/> from the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridEditorMediaConfigSize? Parse(JObject json)  {
            return json == null ? null : new GridEditorMediaConfigSize(json);
        }

        #endregion
    
    }

}