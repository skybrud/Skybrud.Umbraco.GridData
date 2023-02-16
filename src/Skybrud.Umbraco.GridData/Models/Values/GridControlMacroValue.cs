using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Class representing the macro value of a control.
    /// </summary>
    public class GridControlMacroValue : GridControlValueBase<JObject> {

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
        /// Initializes a new instance based on the value of the specified grid <paramref name="control"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the parent grid control.</param>
        public GridControlMacroValue(GridControl control) : base(control) {
            Syntax = Json.GetString("syntax")!;
            MacroAlias = Json.GetString("macroAlias")!;
            Parameters = Json.GetObject("macroParamsDictionary")?.ToObject<Dictionary<string, object>>() ?? new Dictionary<string, object>();
        }

        #endregion

    }

}