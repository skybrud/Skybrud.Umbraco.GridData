using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the macro value of a control.
    /// </summary>
    public class GridControlMacroValue : GridJsonObject, IGridControlValue {

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        public GridControl Control { get; private set; }

        /// <summary>
        /// Gets the syntax of the macro.
        /// </summary>
        [JsonProperty("syntax")]
        public string Syntax { get; set; }

        /// <summary>
        /// Gets the alias of the macro.
        /// </summary>
        [JsonProperty("macroAlias")]
        public string MacroAlias { get; set; }

        /// <summary>
        /// Gets a dictionary containing the macro parameters.
        /// </summary>
        [JsonProperty("macroParamsDictionary")]
        public Dictionary<string, object> Parameters { get; set; }

        #region Constructors

        protected GridControlMacroValue(GridControl control, JObject obj) : base(obj) {
            Control = control;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a macro value from the specified <code>JsonObject</code>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridControlMacroValue Parse(GridControl control, JObject obj) {
            if (obj == null) return null;
            return new GridControlMacroValue(control, obj) {
                Syntax = obj.GetString("syntax"),
                MacroAlias = obj.GetString("macroAlias"),
                Parameters = obj.GetObject("macroParamsDictionary").ToObject<Dictionary<string, object>>()
            };
        }

        #endregion
        
    }

}