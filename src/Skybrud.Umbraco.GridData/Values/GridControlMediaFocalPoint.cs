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

        protected GridControlMediaFocalPoint(JObject obj) : base(obj) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a focal point from the specified <code>JsonObject</code>.
        /// </summary>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridControlMediaFocalPoint Parse(JObject obj) {
            if (obj == null) return null;
            return new GridControlMediaFocalPoint(obj) {
                Left = obj.GetFloat("left"),
                Top = obj.GetFloat("top")
            };
        }

        #endregion

    }

}