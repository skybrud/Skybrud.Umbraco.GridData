using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing a row in an Umbraco Grid.
    /// </summary>
    public class GridRow {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <code>GridSection</code>.
        /// </summary>
        [JsonIgnore]
        public GridSection Section { get; private set; }

        /// <summary>
        /// Gets a reference to the instance of <code>JObject</code> this row was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets the unique ID of the row.
        /// </summary>
        [JsonProperty("id", Order = 3)]
        public string Id { get; private set; }

        /// <summary>
        /// Gets the name of the row.
        /// </summary>
        [JsonProperty("name", Order = 1)]
        public string Name { get; private set; }

        /// <summary>
        /// Gets an array of all areas in the row.
        /// </summary>
        [JsonProperty("areas", Order = 2)]
        public GridArea[] Areas { get; private set; }

        /// <summary>
        /// Gets a dictionary representing the styles of the row.
        /// </summary>
        public GridDictionary Styles { get; private set; }

        /// <summary>
        /// Gets a dictionary representing the configuration (called Settings in the backoffice) of the row.
        /// </summary>
        public GridDictionary Config { get; private set; }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a row from the specified <code>obj</code>.
        /// </summary>
        /// <param name="section">The parent section of the row.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridRow Parse(GridSection section, JObject obj) {

            // Some input validation
            if (obj == null) throw new ArgumentNullException("obj");
            
            // Parse basic properties
            GridRow row = new GridRow {
                Section = section,
                JObject = obj,
                Id = obj.GetString("id"),
                Name = obj.GetString("name"),
                Styles = obj.GetObject("styles", GridDictionary.Parse),
                Config = obj.GetObject("config", GridDictionary.Parse)
            };

            row.Areas = obj.GetArray("areas", x => GridArea.Parse(row, x)) ?? new GridArea[0];

            // Return the row
            return row;

        }

        #endregion

    }

}