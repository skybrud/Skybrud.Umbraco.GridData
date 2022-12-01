using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Models.Config;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Class representing an editor of a control in an Umbraco Grid.
    /// </summary>
    public class GridEditor : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets the name of the editor.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; }

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        [JsonProperty("alias")]
        public string? Alias { get; }

        /// <summary>
        /// Gets the view of the editor.
        /// </summary>
        [JsonProperty("view")]
        public string? View { get; }

        /// <summary>
        /// Gets renderer for the control/editor. If specified, the renderer refers to a partial
        /// view that should be used for rendering the control.
        /// </summary>
        [JsonProperty("render")]
        public string? Render { get; }

        /// <summary>
        /// Gets the icon of the editor.
        /// </summary>
        [JsonProperty("icon")]
        public string? Icon { get; }

        /// <summary>
        /// Gets the configuration object for the editor. This property will return <c>null</c> if the
        /// corresponding property in the underlying JSON is also <c>null</c>.
        /// </summary>
        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        public IGridEditorConfig? Config { get; internal set; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance with the specified <paramref name="alias"/>.
        /// </summary>
        /// <param name="alias">The alias of the editor.</param>
        public GridEditor(string alias) : base(null) {
            Alias = alias;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> representing the control.</param>
        public GridEditor(JObject? json) : base(json) {

            // Parse basic properties
            Name = json?.GetString("name");
            Alias = json?.GetString("alias");
            View = json?.GetString("view");
            Render = json?.GetString("render");
            Icon = json?.GetString("icon");

        }
        
        public GridEditor(GridEditor editor) : base(editor.JObject) {
            Name = editor.Name;
            Alias = editor.Alias;
            View = editor.View;
            Render = editor.Render;
            Icon = editor.Icon;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the config of the editor casted to the type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the config to be returned.</typeparam>
        public T? GetConfig<T>() where T : IGridEditorConfig {
            return (T?) Config;
        }

        #endregion

    }

    public class GridEditor<TConfig> : GridEditor where TConfig : IGridEditorConfig {

        public new TConfig? Config => (TConfig?) base.Config;

        public GridEditor(GridEditor editor) : base(editor) { }

    }

}