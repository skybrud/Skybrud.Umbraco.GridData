using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.ExtensionMethods;

namespace Skybrud.Umbraco.GridData {

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

        [JsonProperty("grid")]
        public int Grid { get; set; }

        [JsonProperty("allowAll")]
        public bool AllowAll { get; set; }

        [JsonProperty("allowed")]
        public string[] Allowed { get; set; }

        [JsonProperty("controls")]
        public GridControl[] Controls { get; set; }

        public Dictionary<string, string> Settings { get; set; }

        #endregion

        #region Static methods

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
            area.Controls = obj.GetArray("controls", x => GridControl.Parse(area, obj)) ?? new GridControl[0];
            
            // Return the row
            return area;
        
        }

        #endregion

    }

}