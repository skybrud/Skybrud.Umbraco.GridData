using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;
using Umbraco.Core.Logging;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing an editor of a control in an Umbraco Grid.
    /// </summary>
    public class GridEditor : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <see cref="GridControl"/>.
        /// </summary>
        [JsonIgnore]
        public GridControl Control { get; private set; }

        /// <summary>
        /// Gets the name of the editor.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        [JsonProperty("alias")]
        public string Alias { get; private set; }

        /// <summary>
        /// Gets the view of the editor.
        /// </summary>
        [JsonProperty("view")]
        public string View { get; private set; }

        /// <summary>
        /// Gets renderer for the control/editor. If specified, the renderer refers to a partial
        /// view that should be used for rendering the control.
        /// </summary>
        [JsonProperty("render")]
        public string Render { get; private set; }

        /// <summary>
        /// Gets the icon of the editor.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; private set; }

        /// <summary>
        /// Gets the configuration object for the editor. This property will return <c>null</c> if the
        /// corresponding property in the underlying JSON is also <c>null</c>.
        /// </summary>
        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        public IGridEditorConfig Config { get; set; }
        
        #endregion

        #region Constructors

        private GridEditor(JObject obj) : base(obj) { }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the config of the editor casted to the type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the config to be returned.</typeparam>
        public T GetConfig<T>() where T : IGridEditorConfig {
            return (T) Config;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses an editor from the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">The parent control of the editor.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridEditor Parse(GridControl control, JObject obj) {

            // Parse basic properties
            GridEditor editor = new GridEditor(obj) {
                Control = control,
                Name = obj.GetString("name"),
                Alias = obj.GetString("alias"),
                View = obj.GetString("view"),
                Render = obj.GetString("render"),
                Icon = obj.GetString("icon")
            };

            // Parse the editor configuration
            JToken config = obj.GetValue("config");
            foreach (IGridConverter converter in GridContext.Current.Converters) {
                try {
                    if (!converter.ConvertEditorConfig(editor, config, out IGridEditorConfig converted)) continue;
                    editor.Config = converted;
                    break;
                } catch (Exception ex) {
                    global::Umbraco.Core.Composing.Current.Logger.Error<GridEditor>(ex, "Converter of type " + converter + " failed for ConvertEditorConfig()");
                }
            }

            return editor;

        }

        #endregion

    }

}