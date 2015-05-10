using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the media value of a control.
    /// </summary>
    public class GridControlMediaValue : GridJsonObject, IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        public GridControl Control { get; private set; }

        /// <summary>
        /// Gets the focal point with information on how the iamge should be cropped.
        /// </summary>
        [JsonProperty("focalPoint")]
        public GridControlMediaFocalPoint FocalPoint { get; protected set; }

        /// <summary>
        /// Gets the ID of the image.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; protected set; }

        /// <summary>
        /// Gets the URL of the media.
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; protected set; }

        /// <summary>
        /// Gets the caption of the media.
        /// </summary>
        [JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore)]
        public string Caption { get; protected set; }

        #endregion

        #region Constructors

        protected GridControlMediaValue(JObject obj) : base(obj) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a media value from the specified <code>JsonObject</code>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridControlMediaValue Parse(GridControl control, JObject obj) {
            if (obj == null) return null;
            return new GridControlMediaValue(obj) {
                Control = control,
                FocalPoint = obj.GetObject("focalPoint", GridControlMediaFocalPoint.Parse),
                Id = obj.GetInt32("id"),
                Image = obj.GetString("image"),
                Caption = obj.GetString("caption")
            };
        }

        #endregion

    }

}