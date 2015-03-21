using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData {

    public class GridControl {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <code>GridArea</code>.
        /// </summary>
        [JsonIgnore]
        public GridArea Area { get; private set; }

        /// <summary>
        /// Gets a reference to the instance of <code>JObject</code> this control was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets the value of the control. Alternately use the <code>GetValue&lt;T&gt;</code> method to get the type safe value.
        /// </summary>
        [JsonProperty("value")]
        public IGridControlValue Value { get; private set; }

        [JsonProperty("editor")]
        public GridEditor Editor { get; private set; }

        public Dictionary<string, string> Settings { get; set; }

        #endregion

        #region Member methods

        public T GetValue<T>() where T : IGridControlValue {
            return (T)Value;
        }

        #endregion

        #region Static methods

        public static GridControl Parse(GridArea area, JObject obj) {
            
            GridControl control = new GridControl {
                Area = area,
                JObject = obj,
                Editor = obj.GetObject("editor").ToObject<GridEditor>(),
                Settings = obj.GetObject("config", GridHelpers.ParseDictionary)
            };

            string alias = control.Editor.Alias;
            string view = control.Editor.View;

            Func<JToken, IGridControlValue> func;
            if (GridContext.Current.TryGetValue(alias + ":" + view, out func)) {
                control.Value = func(obj.GetValue("value"));
            } else if (GridContext.Current.TryGetValue(view, out func)) {
                control.Value = func(obj.GetValue("value"));
            }

            return control;

        }

        #endregion

    }

}