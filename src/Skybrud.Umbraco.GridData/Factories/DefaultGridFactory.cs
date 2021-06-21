using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Converters;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Values;
using Umbraco.Cms.Core.Configuration.Grid;
using Umbraco.Cms.Core.Models.PublishedContent;
using IGridEditorConfig = Skybrud.Umbraco.GridData.Models.Config.IGridEditorConfig;

// ReSharper disable InconsistentNaming

namespace Skybrud.Umbraco.GridData.Factories {
    
    public class DefaultGridFactory : IGridFactory {
        
        private readonly ILogger _logger;
        private readonly IGridConfig _gridConfig;
        private readonly GridConverterCollection _converters;

        #region Constructors

        public DefaultGridFactory(ILogger<DefaultGridFactory> logger, IGridConfig gridConfig, GridConverterCollection converters) {
            _logger = logger;
            _gridConfig = gridConfig;
            _converters = converters;
        }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public virtual GridDataModel CreateGridModel(IPublishedElement owner, IPublishedPropertyType propertyType, JObject json, bool preview) {
            return new GridDataModel(owner, propertyType, json, this);
        }

        /// <inheritdoc />
        public virtual GridSection CreateGridSection(JObject json, GridDataModel grid) {
            return new GridSection(json, grid, this);
        }

        /// <inheritdoc />
        public virtual GridRow CreateGridRow(JObject json, GridSection section) {
            return new GridRow(json, section, this);
        }

        /// <inheritdoc />
        public virtual GridArea CreateGridArea(JObject json, GridRow row) {
            return new GridArea(json, row, this);
        }

        /// <inheritdoc />
        public virtual GridControl CreateGridControl(JObject json, GridArea area) {

            // The saved JSON for the editor only contains the alias of the editor as other information may change over
            // time. As a result of this, we need to inject a new editor object into the JSON.
            ReplaceEditorObjectFromConfig(json);


            // Parse the Grid editor
            var editor = json.GetObject("editor", CreateGridEditor);


            //Type valueType = null;

            //foreach (var converter in conver)




            // Initialize a new Grid control
            GridControl control = new GridControl(json, area);

            control.Editor = editor;

            // Parse the control value
            control.Value = ParseGridControlValue(control);





            // Return the control
            return control;

        }

        /// <inheritdoc />
        public virtual GridEditor CreateGridEditor(JObject json) {

            Type configType = null;

            // Initialize a new Grid editor
            GridEditor editor = new GridEditor(json);

            foreach (var converter in _converters) {

                if (converter.GetConfigType(editor, out configType)) break;

            }

            if (configType != null) {

                Type genericType = typeof(GridEditor<>).MakeGenericType(configType);

                editor = (GridEditor) Activator.CreateInstance(genericType, editor);

            }

            // Parse the grid editor configuration
            editor.Config = ParseGridEditorConfig(editor);

            // Return the editor
            return editor;

        }

        protected virtual IGridControlValue ParseGridControlValue(GridControl control) {
            
            // Parse the control value
            JToken value = control.JObject.GetValue("value");
            foreach (IGridConverter converter in _converters) {
                try {
                    if (!converter.ConvertControlValue(control, value, out IGridControlValue converted)) continue;
                    return converted;
                } catch (Exception ex) {
                    _logger.LogError(ex, $"Converter of type {converter} failed for ConvertControlValue()");
                }
            }

            return null;

        }

        protected virtual IGridEditorConfig ParseGridEditorConfig(GridEditor editor) {

            // Parse the editor configuration
            JToken config = editor.JObject.GetValue("config");
            foreach (IGridConverter converter in _converters) {
                try {
                    if (!converter.ConvertEditorConfig(editor, config, out IGridEditorConfig converted)) continue;
                    return converted;
                } catch (Exception ex)  {
                    _logger.LogError(ex, $"Converter of type {converter} failed for ConvertEditorConfig()");
                }
            }

            return null;

        }

        protected virtual void ReplaceEditorObjectFromConfig(JObject json) {

            // Get the "editor" object from the JSON
            JObject editor = json.GetObject("editor");

            // Get the alias of the editor
            string alias = editor?.GetString("alias");

            // Skip if we dont have an alias
            if (string.IsNullOrWhiteSpace(alias)) return;

            // Find the editor in the configuration
            var found = _gridConfig.EditorsConfig.Editors.FirstOrDefault(x => x.Alias == alias);

            // Skip if not found
            if (found == null) return;

            // Set a new editor object with the updated config
            json["editor"] = new JObject {
                {"name", found.Name},
                {"alias", found.Alias},
                {"view", found.View},
                {"render", found.Render},
                {"icon", found.Icon},
                {"config", JObject.FromObject(found.Config)}
            };

        }

        #endregion

    }

}