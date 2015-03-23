using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing the value/model saved by an Umbraco Grid property.
    /// </summary>
    public class GridDataModel {

        #region Properties

        /// <summary>
        /// Gets the raw JSON value this model was parsed from.
        /// </summary>
        [JsonIgnore]
        public string Raw { get; private set; }

        /// <summary>
        /// Gets a reference to the instance of <code>JObject</code> this section was parsed from.
        /// </summary>
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

        #region Exposing properties from the JSON due to http://issues.umbraco.org/issue/U4-5750

        /// <summary>
        /// Same as <code>Name</code>.
        /// </summary>
        [JsonIgnore]
        [Obsolete]
        public string name {
            get { return Name; }
        }

        /// <summary>
        /// Gets the underlying JSON array for the <code>sections</code> property. 
        /// </summary>
        [JsonIgnore]
        [Obsolete]
        public dynamic sections {
            get { return ((dynamic)JObject).sections; }
        }

        #endregion
        
        #endregion

        #region Member methods

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
        /// Gets an array of all nested controls with the specified editor <code>alias</code>. 
        /// </summary>
        /// <param name="alias">The editor alias of controls to be returned.</param>
        public GridControl[] GetAllControls(string alias) {
            return GetAllControls(x => x.Editor.Alias == alias);
        }

        /// <summary>
        /// Gets an array of all nested controls matching the specified <code>predicate</code>. 
        /// </summary>
        /// <param name="predicate">The predicate (callback function) used for comparison.</param>
        public GridControl[] GetAllControls(Func<GridControl, bool> predicate) {
            return (
                from section in Sections
                from row in section.Rows
                from area in row.Areas
                from control in area.Controls
                where predicate(control)
                select control
            ).ToArray();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Deserializes the specified JSON string into an instance of <code>GridDataModel</code>.
        /// </summary>
        /// <param name="json">The JSON string to be deserialized.</param>
        public static GridDataModel Deserialize(string json) {

            // Validate the JSON
            if (json == null || !json.StartsWith("{") || !json.EndsWith("}")) return null;

            // Deserialize the JSON
            JObject obj = JObject.Parse(json);

            // Parse basic properties
            GridDataModel model = new GridDataModel {
                Raw = json,
                JObject = obj,
                Name = obj.GetString("name")
            };

            // Parse the sections
            model.Sections = obj.GetArray("sections", x => GridSection.Parse(model, x)) ?? new GridSection[0];

            // Return the model
            return model;

        }

        [Obsolete("Use Deserialize method instead")]
        public static GridDataModel Parse(JObject obj) {
            if (obj == null) return null;
            return new GridDataModel {
                Raw = obj.ToString(),
                Name = obj.GetString("name"),
                Sections = obj.GetArray("sections", x => GridSection.Parse(null, obj)) ?? new GridSection[0]
            };
        }

        #endregion

    }

}