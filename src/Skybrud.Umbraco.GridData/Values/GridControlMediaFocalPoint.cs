using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData.Values {
    
    /// <summary>
    /// Class representing the focal point of a media.
    /// </summary>
    public class GridControlMediaFocalPoint : GridJsonObject {

        #region Properties

        /// <summary>
        /// The horizontal (X-axis) coordinate of the focal point.
        /// </summary>
        [JsonProperty("left")]
        public float Left { get; private set; }
        
        /// <summary>
        /// The vertical (Y-axis) coordinate of the focal point.
        /// </summary>
        [JsonProperty("top")]
        public float Top { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the the focal point.</param>
        protected GridControlMediaFocalPoint(JObject obj) : base(obj) {
            Left = obj.GetFloat("left");
            Top = obj.GetFloat("top");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a focal point from the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridControlMediaFocalPoint Parse(JObject obj) {
            return obj == null ? null : new GridControlMediaFocalPoint(obj);
        }

        #endregion

    }

}