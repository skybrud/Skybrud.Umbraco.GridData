using Newtonsoft.Json;

namespace Skybrud.Umbraco.GridData {
    
    public class GridEditor {

        /// <summary>
        /// Gets the name of the editors.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        [JsonProperty("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Gets the view of the editor.
        /// </summary>
        [JsonProperty("view")]
        public string View { get; set; }

        /// <summary>
        /// Gets the icon of the editor.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets the configuration object for the editor. This property will return <code>NULL</code> if the
        /// corresponding property in the underlying JSON is also <code>NULL</code>.
        /// </summary>
        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        public GridEditorConfig Config { get; set; }

    }

}