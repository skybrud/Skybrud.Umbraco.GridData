using Newtonsoft.Json;
using Umbraco.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Interface describing the value/model saved by an Umbraco Grid property.
    /// </summary>
    public interface IGridDataModel {

        /// <summary>
        /// Gets whether the model is valid.
        /// </summary>
        [JsonIgnore]
        bool IsValid { get; }

        /// <summary>
        /// Gets the name of the selected layout.
        /// </summary>
        [JsonProperty("name")]
        string Name { get; }

        /// <summary>
        /// Gets an array of the columns in the grid.
        /// </summary>
        [JsonProperty("sections")]
        IGridSection[] Sections { get; }

        /// <summary>
        /// Gets a reference to the parent <see cref="IPublishedElement"/>, if the Grid model was loaded directly from a property value.
        /// </summary>
        [JsonIgnore]
        IPublishedElement Owner { get; }

        /// <summary>
        /// Gets whether the grid model has a reference to it's <see cref="IPublishedElement"/> owner.
        /// </summary>
        [JsonIgnore]
        bool HasOwner { get; }

        /// <summary>
        /// Gets a reference to the parent property type, if the Grid model was loaded directly from a property value.
        /// </summary>
        [JsonIgnore]
        IPublishedPropertyType PropertyType { get; }

        /// <summary>
        /// Gets whether a property type has been specified for the model.
        /// </summary>
        [JsonIgnore]
        bool HasPropertyType { get; }

    }

}