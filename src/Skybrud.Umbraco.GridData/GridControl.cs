using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData {

    public class GridControl {

        [JsonIgnore]
        public JObject JObject { get; private set; }

        [JsonProperty("value")]
        public IGridControlValue Value { get; private set; }

        [JsonProperty("editor")]
        public GridEditor Editor { get; private set; }

        public T GetValue<T>() where T : IGridControlValue {
            return (T) Value;
        }

        public static GridControl Parse(JObject obj) {
            
            GridControl control = new GridControl {
                JObject = obj,
                Editor = obj.GetObject("editor").ToObject<GridEditor>()
            };

            Func<JToken, IGridControlValue> func;
            if (PiggyBank.OneLittlePiggy.TryGetValue(control.Editor.Alias, out func)) {
                control.Value = func(obj.GetValue("value"));
            }

            return control;

        }

    }

}