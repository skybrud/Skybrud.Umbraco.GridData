using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Class representing the media value of a control.
    /// </summary>
    public class GridControlMediaValue : GridControlValueBase {

        #region Properties

        /// <summary>
        /// Gets the focal point with information on how the iamge should be cropped.
        /// </summary>
        [JsonProperty("focalPoint")]
        public GridControlMediaFocalPoint? FocalPoint { get; protected set; }

        /// <summary>
        /// Gets the ID of the image.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; protected set; }

        /// <summary>
        /// Gets the URL of the media.
        /// </summary>
        [JsonProperty("image")]
        public string? Image { get; protected set; }

        /// <summary>
        /// Gets the alt text of the media.
        /// </summary>
        [JsonProperty("altText", NullValueHandling = NullValueHandling.Ignore)]
        public string? AlternativeText { get; protected set; }

        /// <summary>
        /// Gets whether the <see cref="AlternativeText"/> property has a value.
        /// </summary>
        [JsonIgnore]
        public bool? HasAlternativeText => !string.IsNullOrWhiteSpace(AlternativeText);

        /// <summary>
        /// Gets the caption of the media.
        /// </summary>
        [JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore)]
        public string? Caption { get; protected set; }

        /// <summary>
        /// Gets whether the <see cref="Caption"/> property has a value.
        /// </summary>
        public bool? HasCaption => !string.IsNullOrWhiteSpace(Caption);

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlMediaValue"/>, this means
        /// checking whether an image has been selected. The property will however not validate the image against the
        /// media cache.
        /// </summary>
        [JsonIgnore]
        public override bool? IsValid => Id > 0;

        [JsonIgnore]
        public IPublishedContent? PublishedImage { get; internal set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the value of the control.</param>
        public GridControlMediaValue(GridControl control, JObject? obj) : base(control, obj) {
            FocalPoint = obj.GetObject("focalPoint", GridControlMediaFocalPoint.Parse);
            Id = obj.GetInt32("id");
            Image = obj.GetString("image");
            AlternativeText = obj.GetString("altText");
            Caption = obj.GetString("caption");
        }

        #endregion

    }

}