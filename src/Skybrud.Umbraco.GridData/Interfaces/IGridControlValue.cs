using Newtonsoft.Json;

namespace Skybrud.Umbraco.GridData.Interfaces {

    /// <summary>
    /// Interface describing a grid control value.
    /// </summary>
    public interface IGridControlValue {

        /// <summary>
        /// Gets a reference to the parent <see cref="GridControl"/>.
        /// </summary>
        [JsonIgnore]
        GridControl Control { get; }

        /// <summary>
        /// Gets whether the value of the control is valid.
        /// </summary>
        [JsonIgnore]
        bool IsValid { get; }

        /// <summary>
        /// Gets the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>An instance of <see cref="string"/> with the value as a searchable text.</returns>
        string GetSearchableText(GridContext context);

    }

}