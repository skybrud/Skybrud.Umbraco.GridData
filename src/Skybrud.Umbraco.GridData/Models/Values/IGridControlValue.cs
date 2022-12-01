using System.IO;
using Newtonsoft.Json;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Interface describing a grid control value.
    /// </summary>
    public interface IGridControlValue {

        /// <summary>
        /// Gets a reference to the parent <see cref="GridControl"/>.
        /// </summary>
        [JsonIgnore]
        GridControl? Control { get; }

        /// <summary>
        /// Gets whether the value of the control is valid.
        /// </summary>
        [JsonIgnore]
        bool? IsValid { get; }

        /// <summary>
        /// Writes a textual representation of this control value to the specified <paramref name="writer"/>.
        /// </summary>
        /// <param name="context">The current <see cref="GridContext"/>.</param>
        /// <param name="writer">The writer to write to.</param>
        void WriteSearchableText(GridContext? context, TextWriter writer);

        /// <summary>
        /// Gets the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>An instance of <see cref="string"/> with the value as a searchable text.</returns>
        string? GetSearchableText(GridContext? context);

    }

}