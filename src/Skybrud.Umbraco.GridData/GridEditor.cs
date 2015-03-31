﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing an editor of a control in an Umbraco Grid.
    /// </summary>
    public class GridEditor {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <code>GridControl</code>.
        /// </summary>
        [JsonIgnore]
        public GridControl Control { get; private set; }

        /// <summary>
        /// Gets a reference to the instance of <code>JObject</code> this editor was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

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
        [JsonProperty("render", NullValueHandling = NullValueHandling.Ignore)]
        public string Render { get; private set; }

        /// <summary>
        /// Gets the icon of the editor.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; private set; }

        /// <summary>
        /// Gets the configuration object for the editor. This property will return <code>NULL</code> if the
        /// corresponding property in the underlying JSON is also <code>NULL</code>.
        /// </summary>
        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        public IGridEditorConfig Config { get; set; }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the config of the editor casted to the type of <code>T</code>.
        /// </summary>
        /// <typeparam name="T">The type of the config to be returned.</typeparam>
        public T GetConfig<T>() where T : IGridEditorConfig {
            return (T) Config;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses an editor from the specified <code>obj</code>.
        /// </summary>
        /// <param name="control">The parent control of the editor.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridEditor Parse(GridControl control, JObject obj) {

            GridEditor editor = new GridEditor {
                Control = control,
                JObject = obj,
                Name = obj.GetString("name"),
                Alias = obj.GetString("alias"),
                View = obj.GetString("view"),
                Render = obj.GetString("render"),
                Icon = obj.GetString("icon")
            };

            string alias = editor.Alias;
            string view = editor.View;

            Func<JToken, IGridEditorConfig> func;
            if (GridContext.Current.TryGetConfigConverter(alias + ":" + view, out func)) {
                editor.Config = func(obj.GetValue("config"));
            } else if (GridContext.Current.TryGetConfigConverter(alias, out func)) {
                editor.Config = func(obj.GetValue("config"));
            }

            return editor;

        }

        #endregion

    }

}