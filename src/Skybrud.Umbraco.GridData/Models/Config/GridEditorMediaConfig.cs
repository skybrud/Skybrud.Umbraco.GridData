using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

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
        public GridEditorMediaConfigSize Size { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the configuration of the editor.</param>
        /// <param name="editor">An instance of <see cref="GridEditor"/> representing the parent editor.</param>
        public GridEditorMediaConfig(JObject json, GridEditor editor) : base(json, editor) {
            Size = json.GetObject("size", GridEditorMediaConfigSize.Parse)!;
        }

        #endregion

    }

}