using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Umbraco.GridData.Models.Config {
    
    /// <summary>
    /// Class representing the configuration of a text editor.
    /// </summary>
    public class GridEditorTextConfig : GridEditorConfigBase {

        #region Properties
        
        /// <summary>
        /// Gets the style properties for the text.
        /// </summary>
        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public string? Style { get; private set; }

        /// <summary>
        /// Gets whether the <see cref="Style"/> property has a value.
        /// </summary>
        public bool? HasStyle => !string.IsNullOrWhiteSpace(Style);

        /// <summary>
        /// Gets the markup for the text.
        /// </summary>
        [JsonProperty("markup", NullValueHandling = NullValueHandling.Ignore)]
        public string? Markup { get; private set; }

        /// <summary>
        /// Gets whether the <see cref="Markup"/> property has a value.
        /// </summary>
        public bool? HasMarkup => !string.IsNullOrWhiteSpace(Markup);

        #endregion

        #region Constructors

        public GridEditorTextConfig(GridEditor? editor, JObject? obj) : base(editor, obj) {
            Style = obj.GetString("style");
            Markup = obj.GetString("markup");
        }

        #endregion

    }

}