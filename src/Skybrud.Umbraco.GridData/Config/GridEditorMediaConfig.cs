using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Models.Editors;

namespace Skybrud.Umbraco.GridData.Config {
    
    /// <summary>
    /// Class representing the configuration of a media editor.
    /// </summary>
    public class GridEditorMediaConfig : GridEditorConfigBase {

        #region Properties

        /// <summary>
        /// Gets an object describing the desired size of the media.
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public GridEditorMediaConfigSize Size { get; private set; }

        #endregion

        #region Constructors

        private GridEditorMediaConfig(GridEditor editor, JObject obj) : base(editor, obj) {
            Size = obj.GetObject("size", GridEditorMediaConfigSize.Parse);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridEditorMediaConfig"/> from the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="editor">The parent editor.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridEditorMediaConfig Parse(GridEditor editor, JObject obj) {
            return obj == null ? null : new GridEditorMediaConfig(editor, obj);
        }

        #endregion

    }

}