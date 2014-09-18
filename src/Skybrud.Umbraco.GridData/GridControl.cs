using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData {

    public class GridControl {

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("value")]
        public IGridControlValue Value { get; set; }

        [JsonProperty("editor")]
        public GridEditor Editor { get; set; }

        [JsonProperty("editorPath")]
        public string EditorPath { get; set; }

        public T GetValue<T>() where T : IGridControlValue {
            return (T) Value;
        }

        public static GridControl Parse(JObject obj) {
            
            GridControl control = new GridControl {
                UniqueId = obj.GetString("uniqueId"),
                Editor = obj.GetObject("editor").ToObject<GridEditor>(),
                EditorPath = obj.GetString("editorPath")
            };

            Func<JToken, IGridControlValue> func;
            if (PiggyBank.OneLittlePiggy.TryGetValue(control.Editor.Alias, out func)) {
                control.Value = func(obj.GetValue("value"));
            }

            return control;

        }

    }

}