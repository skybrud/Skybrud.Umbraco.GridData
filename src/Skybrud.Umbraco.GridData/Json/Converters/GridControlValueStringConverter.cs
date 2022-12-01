using System;
using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Models.Values;

namespace Skybrud.Umbraco.GridData.Json.Converters {

    /// <summary>
    /// Converter for text based grid control values.
    /// </summary>
    public class GridControlValueStringConverter : JsonConverter {

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
            
            if (value is GridControlTextValue text) {
                writer.WriteValue(text.Value);
                return;
            }
            
            serializer.Serialize(writer, value);

        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        /// <summary>
        /// Gets a value indicating whether this Newtonsoft.Json.JsonConverter can read JSON.
        /// </summary>
        public override bool CanRead => false;

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="type">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise <c>false</c>.</returns>
        public override bool CanConvert(Type type) {
            return false;
        }
    
    }

}