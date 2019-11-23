namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Interface describing the value/model saved by an Umbraco Grid property.
    /// </summary>
    public interface IGridDataModel {

        /// <summary>
        /// Gets whether the model is valid.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Gets the raw JSON value this model was parsed from.
        /// </summary>
        string Raw { get; }

        /// <summary>
        /// Gets the name of the selected layout.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets an array of the columns in the grid.
        /// </summary>
        IGridSection[] Sections { get; }

        /// <summary>
        /// Gets the alias of the property property used for the grid.
        /// </summary>
        string PropertyAlias { get; }

        /// <summary>
        /// Gets whether a property alias has been specified for the model.
        /// </summary>
        bool HasPropertyAlias { get; }

    }

}