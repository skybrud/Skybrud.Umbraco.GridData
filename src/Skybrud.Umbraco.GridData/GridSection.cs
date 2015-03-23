using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing a section in an Umbraco Grid.
    /// </summary>
    public class GridSection {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <code>GridDataModel</code>.
        /// </summary>
        [JsonIgnore]
        public GridDataModel Model { get; private set; }

        /// <summary>
        /// Gets a reference to the instance of <code>JObject</code> this section was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets the overall column width of the section.
        /// </summary>
        [JsonProperty("grid")]
        public int Grid { get; private set; }

        /// <summary>
        /// Gets an array of all rows in the sections.
        /// </summary>
        [JsonProperty("rows")]
        public GridRow[] Rows { get; private set; }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a section from the specified <code>obj</code>.
        /// </summary>
        /// <param name="model">The parent model of the section.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridSection Parse(GridDataModel model, JObject obj) {

            // Some input validation
            if (obj == null) throw new ArgumentNullException("obj");

            // Parse basic properties
            GridSection section = new GridSection {
                Model = model,
                JObject = obj,
                Grid = obj.GetInt32("grid")
            };

            // Parse the rows
            section.Rows = obj.GetArray("rows", x => GridRow.Parse(section, x)) ?? new GridRow[0];

            // Return the section
            return section;

        }

        #endregion

    }

}