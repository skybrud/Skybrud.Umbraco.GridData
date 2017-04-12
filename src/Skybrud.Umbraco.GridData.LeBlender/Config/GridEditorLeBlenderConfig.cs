using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Config;

namespace Skybrud.Umbraco.GridData.LeBlender.Config {

    /// <summary>
    /// Class representing the configuration of a link picker.
    /// </summary>
    public class GridEditorLeBlenderConfig : GridEditorConfigBase {

        #region Properties

        public int Max { get; private set; }

        public bool RenderInGrid { get; private set; }

        #endregion

        #region Constructors

        private GridEditorLeBlenderConfig(GridEditor editor, JObject obj) : base(editor, obj) {
            Max = obj.GetInt32("max");
            RenderInGrid = obj.GetBoolean("renderInGrid");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GridEditorLeBlenderConfig"/>.
        /// </summary>
        /// <param name="editor">The parent editor.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GridEditorLeBlenderConfig"/>.</returns>
        public static GridEditorLeBlenderConfig Parse(GridEditor editor, JObject obj) {
            return obj == null ? null : new GridEditorLeBlenderConfig(editor, obj);
        }

        #endregion

    }

}