using Newtonsoft.Json;

namespace Skybrud.Umbraco.GridData.Interfaces {

    /// <summary>
    /// Interface describing a grid editor config.
    /// </summary>
    public interface IGridEditorConfig {

        /// <summary>
        /// Gets a reference to the parent editor of the configuration.
        /// </summary>
        [JsonIgnore]
        GridEditor Editor { get; }

    }

}