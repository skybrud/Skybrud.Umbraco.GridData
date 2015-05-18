using System;
using Newtonsoft.Json;

namespace Skybrud.Umbraco.GridData.Json.Converters {

    /// <summary>
    /// Converter for dictionary based values in the grid.
    /// </summary>
    public class GridJsonConverter : JsonConverter {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            GridJsonObject obj = value as GridJsonObject;
            if (obj != null) {
                serializer.Serialize(writer, obj.JObject);
                return;
            }

            GridDictionary dictionary = value as GridDictionary;
            if (dictionary != null) {
                serializer.Serialize(writer, dictionary.JObject);
                return;
            }
            
            serializer.Serialize(writer, value);
        
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead {
            get { return false; }
        }

        public override bool CanConvert(Type type) {
            return false;
        }
    
    }

}