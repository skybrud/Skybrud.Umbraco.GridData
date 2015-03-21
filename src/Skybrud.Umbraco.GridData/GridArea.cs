using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing an area in an Umbraco Grid.
    /// </summary>
    public class GridArea {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <code>GridRow</code>.
        /// </summary>
        [JsonIgnore]
        public GridRow Row { get; private set; }

        /// <summary>
        /// Gets a reference to the instance of <code>JObject</code> this area was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets the column width of the area.
        /// </summary>
        [JsonProperty("grid")]
        public int Grid { get; private set; }

        /// <summary>
        /// Gets wether all editors are allowed for this area.
        /// </summary>
        [JsonProperty("allowAll")]
        public bool AllowAll { get; private set; }

        /// <summary>
        /// Gets an array of all editors allowed for this area. If <code>AllowAll</code> is <code>TRUE</code>, this
        /// array may be empty.
        /// </summary>
        [JsonProperty("allowed")]
        public string[] Allowed { get; private set; }

        /// <summary>
        /// Gets an array of all controls added to this area.
        /// </summary>
        [JsonProperty("controls")]
        public GridControl[] Controls { get; private set; }

        public Dictionary<string, string> Settings { get; private set; }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses an area from the specified <code>obj</code>.
        /// </summary>
        /// <param name="row">The parent row of the area.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridArea Parse(GridRow row, JObject obj) {

            // Some input validation
            if (obj == null) throw new ArgumentNullException("obj");
            
            // Parse the array of allow blocks
            JArray allowed = obj.GetArray("allowed");
            
            // Parse basic properties
            GridArea area = new GridArea {
                Row = row,
                JObject = obj,
                Grid = obj.GetInt32("grid"),
                AllowAll = obj.GetBoolean("allowAll"),
                Allowed = allowed == null ? new string[0] : allowed.Select(x => (string) x).ToArray(),
                Settings = GridCustomSetting.Parse(obj)
            };

            // Parse the controls
            area.Controls = obj.GetArray("controls", x => GridControl.Parse(area, x)) ?? new GridControl[0];
            
            // Return the row
            return area;
        
        }

        #endregion

    }

}