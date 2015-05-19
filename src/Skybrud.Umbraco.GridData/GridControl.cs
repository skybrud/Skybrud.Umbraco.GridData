using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;
using Umbraco.Core.Logging;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing a control in an Umbraco Grid.
    /// </summary>
    public class GridControl : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <code>GridArea</code>.
        /// </summary>
        [JsonIgnore]
        public GridArea Area { get; private set; }

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

        #region Constructors

        private GridControl(JObject obj) : base(obj) { }

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
            GridControl control = new GridControl(obj) {
                Area = area
            };

            // Parse the editor
            control.Editor = obj.GetObject("editor", x => GridEditor.Parse(control, x));

            // Parse the control value
            JToken value = obj.GetValue("value");
            foreach (IGridConverter converter in GridContext.Current.Converters) {
                try {
                    IGridControlValue converted;
                    if (!converter.ConvertControlValue(control, value, out converted)) continue;
                    control.Value = converted;
                    break;
                } catch (Exception ex) {
                    LogHelper.Error<GridControl>("Converter of type " + converter + " failed for ConvertControlValue()", ex);
                }
            }
            
            return control;

        }

        #endregion

    }

}