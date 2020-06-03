using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the macro value of a control.
    /// </summary>
    public class GridControlMacroValue : GridControlValueBase {

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

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlMacroValue"/>, this means
        /// checking whether a macro alias has been specified.
        /// </summary>
        [JsonIgnore]
        public override bool IsValid => !string.IsNullOrWhiteSpace(MacroAlias);

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the value of the control.</param>
        protected GridControlMacroValue(GridControl control, JObject obj) : base(control, obj) {
            Syntax = obj.GetString("syntax");
            MacroAlias = obj.GetString("macroAlias");
            Parameters = obj.GetObject("macroParamsDictionary").ToObject<Dictionary<string, object>>();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a macro value from the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridControlMacroValue Parse(GridControl control, JObject obj) {
            return obj == null ? null : new GridControlMacroValue(control, obj);
        }

        #endregion
        
    }

}