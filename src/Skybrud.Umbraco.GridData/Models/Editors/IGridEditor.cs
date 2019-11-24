using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData.Models.Editors {

    /// <summary>
    /// Interface describing an editor of a control in an Umbraco Grid.
    /// </summary>
    public interface IGridEditor {

        /// <summary>
        /// Gets a reference to the parent <see cref="GridControl"/>.
        /// </summary>
        [JsonIgnore]
        IGridControl Control { get; }
        
        /// <summary>
        /// Gets the name of the editor.
        /// </summary>
        [JsonProperty("name")]
        string Name { get; }

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        [JsonProperty("alias")]
        string Alias { get; }

        /// <summary>
        /// Gets the view of the editor.
        /// </summary>
        [JsonProperty("view")]
        string View { get; }

        /// <summary>
        /// Gets renderer for the control/editor. If specified, the renderer refers to a partial
        /// view that should be used for rendering the control.
        /// </summary>
        [JsonProperty("render")]
        string Render { get; }

        /// <summary>
        /// Gets the icon of the editor.
        /// </summary>
        [JsonProperty("icon")]
        string Icon { get; }

        /// <summary>
        /// Gets the configuration object for the editor.
        /// </summary>
        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        IGridEditorConfig Config { get; }

        /// <summary>
        /// Returns the config of the editor casted to the type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the config to be returned.</typeparam>
        /// <returns>The editor config as an instance of <typeparamref name="T"/>.</returns>
        T GetConfig<T>() where T : IGridEditorConfig;

    }

}