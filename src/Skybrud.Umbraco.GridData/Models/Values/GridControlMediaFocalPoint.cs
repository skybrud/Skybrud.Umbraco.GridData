using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Class representing the focal point of a media.
    /// </summary>
    public class GridControlMediaFocalPoint : GridJsonObject {

        #region Properties

        /// <summary>
        /// The horizontal (X-axis) coordinate of the focal point.
        /// </summary>
        [JsonProperty("left")]
        public float Left { get; }

        /// <summary>
        /// The vertical (Y-axis) coordinate of the focal point.
        /// </summary>
        [JsonProperty("top")]
        public float Top { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/>.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the the focal point.</param>
        protected GridControlMediaFocalPoint(JObject json) : base(json) {
            Left = json.GetFloat("left");
            Top = json.GetFloat("top");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a focal point from the specified <paramref name="json"/>.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> to be parsed.</param>
        [return: NotNullIfNotNull("json")]
        public static GridControlMediaFocalPoint? Parse(JObject? json) {
            return json == null ? null : new GridControlMediaFocalPoint(json);
        }

        #endregion

    }

}