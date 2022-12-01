using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Umbraco.GridData.Models.Config {
    
    /// <summary>
    /// Class representing the configuration of a media editor.
    /// </summary>
    public class GridEditorMediaConfig : GridEditorConfigBase {

        #region Properties

        /// <summary>
        /// Gets an object describing the desired size of the media.
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public GridEditorMediaConfigSize? Size { get; }

        #endregion

        #region Constructors

        public GridEditorMediaConfig(GridEditor? editor, JObject? obj) : base(editor, obj) {
            Size = obj.GetObject("size", GridEditorMediaConfigSize.Parse);
        }

        #endregion

    }

}