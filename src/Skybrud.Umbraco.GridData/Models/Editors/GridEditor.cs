using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;
using Umbraco.Core.Logging;

namespace Skybrud.Umbraco.GridData.Models.Editors {

    /// <summary>
    /// Class representing an editor of a control in an Umbraco Grid.
    /// </summary>
    public class GridEditor : GridJsonObject, IGridEditor {

        #region Properties

        /// <inheritdoc />
        public IGridControl Control { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Alias { get; set; }

        /// <inheritdoc />
        public string View { get; set; }

        /// <inheritdoc />
        public string Render { get; set; }

        /// <inheritdoc />
        public string Icon { get; set; }

        /// <inheritdoc />
        public IGridEditorConfig Config { get; set; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance with default options.
        /// </summary>
        public GridEditor() : base(null) { }

        /// <summary>
        /// Initializes a new instance with the specified <paramref name="alias"/>.
        /// </summary>
        /// <param name="alias">The alias of the editor.</param>
        public GridEditor(string alias) : base(null) {
            Alias = alias;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="obj"/> and <paramref name="control"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> representing the control.</param>
        /// <param name="control">An instance of <see cref="IGridControl"/> representing the parent area.</param>
        public GridEditor(JObject obj, IGridControl control) : base(obj) {

            // Parse basic properties
            Control = control;
            Name = obj.GetString("name");
            Alias = obj.GetString("alias");
            View = obj.GetString("view");
            Render = obj.GetString("render");
            Icon = obj.GetString("icon");

            // Parse the editor configuration
            JToken config = obj.GetValue("config");
            foreach (IGridConverter converter in GridContext.Current.Converters) {
                try {
                    if (!converter.ConvertEditorConfig(this, config, out IGridEditorConfig converted)) continue;
                    Config = converted;
                    break;
                } catch (Exception ex) {
                    global::Umbraco.Core.Composing.Current.Logger.Error<GridEditor>(ex, "Converter of type " + converter + " failed for ConvertEditorConfig()");
                }
            }

        }

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

    }

}