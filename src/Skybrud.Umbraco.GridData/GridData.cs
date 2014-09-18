using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {
    
    public class GridData {

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
        /// Parses the specified <code>JObject</code>.
        /// </summary>
        /// <param name="obj">The <code>JObject</code> to be parsed.</param>
        public static GridData Parse(JObject obj) {
            return new GridData {
                Columns = obj.GetArray("columns", GridColumn.Parse) ?? new GridColumn[0]
            };
        }

        /// <summary>
        /// Deserializes the specified JSON string into an instance of <code>GridData</code>.
        /// </summary>
        /// <param name="json">The JSON string to be deserialized.</param>
        public static GridData Deserialize(string json) {

            if (json == null || !json.StartsWith("{") || !json.EndsWith("}")) {
                return new GridData {
                    Columns = new GridColumn[0]
                };
            }

            JObject obj = JObject.Parse(json);
            return Parse(obj);
        
        }

    }

}
