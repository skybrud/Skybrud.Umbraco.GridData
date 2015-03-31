using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Dictionary representing a configuration for an element in the Umbraco Grid.
    /// </summary>
    public class GridDictionary {

        #region Private fields

        private readonly Dictionary<string, string> _config;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a reference to the instance of <code>JObject</code> this object was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets the keys of the underlying dictionary.
        /// </summary>
        [JsonIgnore]
        public string[] Keys {
            get { return _config.Keys.ToArray(); }
        }

        /// <summary>
        /// Gets the value of an item with the specified <code>key</code>.
        /// </summary>
        /// <param name="key">The key of the dictionary item.</param>
        public string this[string key] {
            get { return _config[key]; }
        }

        #endregion

        #region Constructors

        private GridDictionary(Dictionary<string, string> config) {
            _config = config;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a dictionary from the specified <code>obj</code>.
        /// </summary>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridDictionary Parse(JObject obj) {

            // Initialize an empty dictionary
            Dictionary<string, string> config = new Dictionary<string, string>();

            // Add all properties to the dictionary
            if (obj != null) {
                foreach (JProperty property in obj.Properties()) {
                    config.Add(property.Name, String.Format(CultureInfo.InvariantCulture, "{0}", property.Value));
                }
            }

            // Return the instance
            return new GridDictionary(config) {
                JObject = obj
            };

        }

        #endregion

    }

}