using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Class representing a generic element in an Umbraco Grid.
    /// </summary>
    public abstract class GridElement : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a dictionary representing the configuration (called <strong>Settings</strong> in the backoffice) of the element.
        /// </summary>
        public GridDictionary? Config { get; }

        /// <summary>
        /// Gets whether the element has one or more config values.
        /// </summary>
        public bool? HasConfig => Config != null && Config.Count > 0;

        /// <summary>
        /// Gets a dictionary representing the styles of the element.
        /// </summary>
        public GridDictionary? Styles { get; }

        /// <summary>
        /// Gets whetehr the element has one or more style values.
        /// </summary>
        public bool? HasStyles => Styles != null && Styles.Count > 0;

        /// <summary>
        /// Gets whether at least one control within the element is valid.
        /// </summary>
        public abstract bool? IsValid { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the area.</param>
        protected GridElement(JObject obj) : base(obj) {
            Styles = obj.GetObject("styles", GridDictionary.Parse);
            Config = obj.GetObject("config", GridDictionary.Parse);
        }

        #endregion

        #region Member methods

        public abstract void WriteSearchableText(GridContext context, TextWriter writer);

        /// <summary>
        /// Gets a textual representation of the element - eg. to be used in Examine.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>An instance of <see cref="string"/> representing the value of the element.</returns>
        public string GetSearchableText(GridContext context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        #endregion

    }

}