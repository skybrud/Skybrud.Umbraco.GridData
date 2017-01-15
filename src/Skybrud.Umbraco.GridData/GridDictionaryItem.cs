namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Class representing an item of <see cref="GridDictionary"/>.
    /// </summary>
    public class GridDictionaryItem {

        #region Properties

        /// <summary>
        /// Gets the key of the item.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the value of the item.
        /// </summary>
        public string Value { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new item based on the specified <paramref name="key"/> and <paramref name="value"/>.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="value">The value of the item.</param>
        public GridDictionaryItem(string key, string value) {
            Key = key;
            Value = value;
        }

        #endregion

    }

}