using System.Collections.Generic;
using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData.Values {

    public class GridControlMacroValue : IGridControlValue {

        [JsonProperty("syntax")]
        public string Syntax { get; set; }

        [JsonProperty("macroAlias")]
        public string MacroAlias { get; set; }

        [JsonProperty("macroParamsDictionary")]
        public Dictionary<string, object> Parameters { get; set; }
        
    }

}