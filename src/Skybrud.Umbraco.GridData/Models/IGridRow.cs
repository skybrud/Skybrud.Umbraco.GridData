using System;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Interface describing a row in an Umbraco Grid.
    /// </summary>
    public interface IGridRow {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridSection"/>.
        /// </summary>
        IGridSection Section { get; }

        /// <summary>
        /// Gets the unique ID of the row.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the label of the row. Use <see cref="HasLabel"/> to check whether a label has been specified.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Gets whether a label has been specified for the definition of this row.
        /// </summary>
        bool HasLabel { get; }

        /// <summary>
        /// Gets the name of the row.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets an array of all areas in the row.
        /// </summary>
        IGridArea[] Areas { get; }

        /// <summary>
        /// Gets a reference to the previous row.
        /// </summary>
        IGridRow PreviousRow { get; }

        /// <summary>
        /// Gets a reference to the next row.
        /// </summary>
        IGridRow NextRow { get; }

        /// <summary>
        /// Gets whether the row has any areas.
        /// </summary>
        bool HasAreas { get; }

        /// <summary>
        /// Gets the first area of the row. If the row doesn't contain any areas, this property will return <c>null</c>.
        /// </summary>
        IGridArea FirstRow { get; }

        /// <summary>
        /// Gets the last area of the row. If the row doesn't contain any areas, this property will return <c>null</c>.
        /// </summary>
        IGridArea LastRow { get; }

        /// <summary>
        /// Gets whether at least one area or control within the row is valid.
        /// </summary>
        bool IsValid { get; }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets an array of all nested controls. 
        /// </summary>
        IGridControl[] GetAllControls();

        /// <summary>
        /// Gets an array of all nested controls with the specified editor <paramref name="alias"/>. 
        /// </summary>
        /// <param name="alias">The editor alias of controls to be returned.</param>
        IGridControl[] GetAllControls(string alias);

        /// <summary>
        /// Gets an array of all nested controls matching the specified <paramref name="predicate"/>. 
        /// </summary>
        /// <param name="predicate">The predicate (callback function) used for comparison.</param>
        IGridControl[] GetAllControls(Func<IGridControl, bool> predicate);

        /// <summary>
        /// Gets a textual representation of the row - eg. to be used in Examine.
        /// </summary>
        /// <returns>An instance of <see cref="string"/> representing the value of the row.</returns>
        string GetSearchableText();

        #endregion

    }

}