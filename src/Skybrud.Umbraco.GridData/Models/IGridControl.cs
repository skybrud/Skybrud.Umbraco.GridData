using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Interface describing a control in an Umbraco Grid.
    /// </summary>
    public interface IGridControl {

        #region Properties

        /// <summary>
        /// Gets a reference to the entire <see cref="IGridDataModel"/>.
        /// </summary>
        [JsonIgnore]
        IGridDataModel Model { get; }

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridSection"/>.
        /// </summary>
        [JsonIgnore]
        IGridSection Section { get; }

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridRow"/>.
        /// </summary>
        [JsonIgnore]
        IGridRow Row { get; }

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridArea"/>.
        /// </summary>
        [JsonIgnore]
        IGridArea Area { get; }

        /// <summary>
        /// Gets the value of the control.
        /// </summary>
        [JsonProperty("value")]
        IGridControlValue Value { get; }

        /// <summary>
        /// Gets a reference to the editor of the control.
        /// </summary>
        [JsonProperty("editor")]
        GridEditor Editor { get; }

        /// <summary>
        /// Gets a reference to the previous control.
        /// </summary>
        IGridControl PreviousControl { get; }

        /// <summary>
        /// Gets a reference to the next control.
        /// </summary>
        IGridControl NextControl { get; }

        /// <summary>
        /// Gets whether the control and it's value is valid.
        /// </summary>
        [JsonIgnore]
        bool IsValid { get; }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <returns>An instance of <see cref="string"/> with the value as a searchable text.</returns>
        string GetSearchableText();

        #endregion

    }

}