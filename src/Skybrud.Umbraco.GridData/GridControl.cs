using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing a control in an Umbraco Grid.
    /// </summary>
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

        /// <summary>
        /// Gets a reference to the editor of the control.
        /// </summary>
        [JsonProperty("editor")]
        public GridEditor Editor { get; private set; }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the value of the control casted to the type of <code>T</code>.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        public T GetValue<T>() where T : IGridControlValue {
            return (T) Value;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a control from the specified <code>obj</code>.
        /// </summary>
        /// <param name="area">The parent area of the control.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridControl Parse(GridArea area, JObject obj) {
            
            // Set basic properties
            GridControl control = new GridControl {
                Area = area,
                JObject = obj
            };

            // Parse the editor
            control.Editor = obj.GetObject("editor", x => GridEditor.Parse(control, x));

            // Parse the value
            string alias = control.Editor.Alias;
            string view = control.Editor.View;
            Func<JToken, IGridControlValue> func;
            if (GridContext.Current.TryGetValueConverter(alias + ":" + view, out func)) {
                control.Value = func(obj.GetValue("value"));
            } else if (GridContext.Current.TryGetValueConverter(alias, out func)) {
                control.Value = func(obj.GetValue("value"));
            }

            return control;

        }

        #endregion

    }

}