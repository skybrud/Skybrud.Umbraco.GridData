using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Class representing the media value of a control.
    /// </summary>
    public class GridControlMediaValue : GridControlValueBase<JObject> {

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
        [MemberNotNullWhen(true, nameof(AlternativeText))]
        public bool HasAlternativeText => !string.IsNullOrWhiteSpace(AlternativeText);

        /// <summary>
        /// Gets the caption of the media.
        /// </summary>
        [JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore)]
        public string? Caption { get; protected set; }

        /// <summary>
        /// Gets whether the <see cref="Caption"/> property has a value.
        /// </summary>
        [JsonIgnore]
        [MemberNotNullWhen(true, nameof(Caption))]
        public bool HasCaption => !string.IsNullOrWhiteSpace(Caption);

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlMediaValue"/>, this means
        /// checking whether an image has been selected. The property will however not validate the image against the
        /// media cache.
        /// </summary>
        [JsonIgnore]
        public override bool IsValid => Id > 0;

        /// <summary>
        /// Gets a reference to the underlying <see cref="IPublishedContent"/> representing the selected image.
        /// </summary>
        [JsonIgnore]
        public IPublishedContent? PublishedImage { get; internal set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the value of the specified grid <paramref name="control"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the parent grid control.</param>
        public GridControlMediaValue(GridControl control) : base(control) {
            FocalPoint = Json.GetObject("focalPoint", GridControlMediaFocalPoint.Parse);
            Id = Json.GetInt32("id");
            Image = Json.GetString("image");
            AlternativeText = Json.GetString("altText");
            Caption = Json.GetString("caption");
        }

        #endregion

    }

}