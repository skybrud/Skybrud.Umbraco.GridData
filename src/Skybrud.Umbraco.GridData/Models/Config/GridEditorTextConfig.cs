using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

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
        public string Style { get; private set; }

        /// <summary>
        /// Gets whether the <see cref="Style"/> property has a value.
        /// </summary>
        public bool HasStyle => !string.IsNullOrWhiteSpace(Style);

        /// <summary>
        /// Gets the markup for the text.
        /// </summary>
        [JsonProperty("markup", NullValueHandling = NullValueHandling.Ignore)]
        public string Markup { get; private set; }

        /// <summary>
        /// Gets whether the <see cref="Markup"/> property has a value.
        /// </summary>
        public bool HasMarkup => !string.IsNullOrWhiteSpace(Markup);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the configuration of the editor.</param>
        /// <param name="editor">An instance of <see cref="GridEditor"/> representing the parent editor.</param>
        public GridEditorTextConfig(JObject json, GridEditor editor) : base(json, editor) {
            Style = json.GetString("style")!;
            Markup = json.GetString("markup")!;
        }

        #endregion

    }

}