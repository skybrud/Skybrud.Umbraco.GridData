using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Class containg a number of helper methods used internally in this package.
    /// </summary>
    public static class GridHelpers {

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into a dictionary.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static Dictionary<string, string> ParseDictionary(JObject obj) {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            if (obj != null) {
                foreach (JProperty property in obj.Properties()) {
                    settings.Add(property.Name, property.Value.ToString());
                }
            }
            return settings;
        }

    }

}