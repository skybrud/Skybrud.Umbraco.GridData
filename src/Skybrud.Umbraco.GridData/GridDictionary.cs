using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Dictionary representing a configuration for an element in the Umbraco Grid.
    /// </summary>
    public class GridDictionary : GridJsonObject, IEnumerable<GridDictionaryItem> {

        #region Private fields

        private readonly Dictionary<string, string> _dictionary;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the keys of the underlying dictionary.
        /// </summary>
        [JsonIgnore]
        public string[] Keys {
            get { return _dictionary.Keys.ToArray(); }
        }

        /// <summary>
        /// Gets the keys of the underlying dictionary.
        /// </summary>
        [JsonIgnore]
        public string[] Values {
            get { return _dictionary.Keys.ToArray(); }
        }

        /// <summary>
        /// Gets the amount of items in the dictionary.
        /// </summary>
        public int Count {
            get { return _dictionary.Count; }
        }

        /// <summary>
        /// Gets the value of an item with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of the dictionary item.</param>
        public string this[string key] {
            get { return _dictionary[key]; }
        }

        #endregion

        #region Constructors

        private GridDictionary(Dictionary<string, string> config, JObject obj) : base(obj) {
            _dictionary = config;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets whether the specified <paramref name="key"/> is contained in the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool ContainsKey(string key) {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets the value associated with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the
        /// key is found; otherwise, the default value for the type of the value parameter. This parameter is passed
        /// uninitialized.</param>
        /// <returns><code>true</code> if the dictionary contains an element with the specified key; otherwise,
        /// <code>false</code>.</returns>
        public bool TryGetValue(string key, out string value) {
            return _dictionary.TryGetValue(key, out value);
        }

        public IEnumerator<GridDictionaryItem> GetEnumerator() {
            return _dictionary.Select(x => new GridDictionaryItem(x.Key, x.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GridDictionary"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>Returns an instance of <see cref="GridDictionary"/>.</returns>
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
            return new GridDictionary(config, obj);

        }

        #endregion

    }

}