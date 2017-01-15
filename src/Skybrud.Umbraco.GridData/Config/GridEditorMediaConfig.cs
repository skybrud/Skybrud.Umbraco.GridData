using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData.Config {
    
    /// <summary>
    /// Class representing the configuration of a media editor.
    /// </summary>
    public class GridEditorMediaConfig : GridJsonObject, IGridEditorConfig {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent editor.
        /// </summary>
        public GridEditor Editor { get; private set; }

        /// <summary>
        /// Gets an object describing the desired size of the media.
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public GridEditorMediaConfigSize Size { get; private set; }

        #endregion

        #region Constructors

        private GridEditorMediaConfig(GridEditor editor, JObject obj) : base(obj) {
            Editor = editor;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <code>GridEditorMediaConfig</code> from the specified <code>JsonObject</code>.
        /// </summary>
        /// <param name="editor">The parent editor.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridEditorMediaConfig Parse(GridEditor editor, JObject obj) {
            if (obj == null) return null;
            return new GridEditorMediaConfig(editor, obj) {
                Size = obj.GetObject("size", GridEditorMediaConfigSize.Parse)
            };
        }

        #endregion

    }

}