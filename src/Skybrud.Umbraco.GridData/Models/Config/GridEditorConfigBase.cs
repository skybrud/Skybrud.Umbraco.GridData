using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Models.Values;

namespace Skybrud.Umbraco.GridData.Models.Config {

    /// <summary>
    /// Abstract class with a basic implementation of the <see cref="IGridControlValue"/> interface.
    /// </summary>
    public abstract class GridEditorConfigBase : GridJsonObject, IGridEditorConfig {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent editor of the configuration.
        /// </summary>
        [JsonIgnore]
        public GridEditor Editor { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the configuration of the editor.</param>
        /// <param name="editor">An instance of <see cref="GridEditor"/> representing the parent editor.</param>
        protected GridEditorConfigBase(JObject json, GridEditor editor) : base(json) {
            Editor = editor;
        }

        #endregion

    }

}