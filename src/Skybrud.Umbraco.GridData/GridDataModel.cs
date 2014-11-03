using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {

    public class GridDataModel {

        [JsonIgnore]
        public string Raw { get; private set; }

        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets the name of the selected layout.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets an array of the columns in the grid.
        /// </summary>
        [JsonProperty("sections")]
        public GridSection[] Sections { get; private set; }

        /// <summary>
        /// Gets an array of all nested controls. 
        /// </summary>
        public GridControl[] GetAllControls() {
            return (
                from section in Sections
                from row in section.Rows
                from area in row.Areas
                from control in area.Controls
                select control
            ).ToArray();
        }

        /// <summary>
        /// Deserializes the specified JSON string into an instance of <code>GridDataModel</code>.
        /// </summary>
        /// <param name="json">The JSON string to be deserialized.</param>
        public static GridDataModel Deserialize(string json) {

            // Validate the JSON
            if (json == null || !json.StartsWith("{") || !json.EndsWith("}")) return null;

            // Deserialize the JSON
            JObject obj = JObject.Parse(json);

            // Parse the JObject
            return new GridDataModel {
                Raw = json,
                JObject = obj,
                Name = obj.GetString("name"),
                Sections = obj.GetArray("sections", GridSection.Parse) ?? new GridSection[0]
            };

        }

        public static GridDataModel Parse(JObject obj) {
            if (obj == null) return null;
            return new GridDataModel {
                Raw = obj.ToString(),
                Name = obj.GetString("name"),
                Sections = obj.GetArray("sections", GridSection.Parse) ?? new GridSection[0]
            };
        }

    }

}
