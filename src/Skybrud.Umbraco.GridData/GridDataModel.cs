using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {

    public class GridDataModel {
        
        [JsonIgnore]
        public string Raw { get; private set; }

        ///// <summary>
        ///// Not exactly sure what this property contains.
        ///// </summary>
        //[JsonProperty("gridWidth")]
        //public object GridWidth { get; private set; }

        /// <summary>
        /// Gets an array of the columns in the grid.
        /// </summary>
        [JsonProperty("columns")]
        public GridColumn[] Columns { get; private set; }

        /// <summary>
        /// Gets an array of all nested controls. 
        /// </summary>
        public GridControl[] GetAllControls() {
            return (
                from column in Columns
                from row in column.Rows
                from cell in row.Cells
                from control in cell.Controls
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
                Columns = obj.GetArray("sections", GridColumn.Parse) ?? obj.GetArray("columns", GridColumn.Parse) ?? new GridColumn[0]
            };
        
        }

    }

}
