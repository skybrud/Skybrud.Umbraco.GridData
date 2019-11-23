namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Interface describing a section in an Umbraco Grid.
    /// </summary>
    public interface IGridSection {

        #region Properties

        /// <summary>
        /// Gets the section name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridDataModel"/>.
        /// </summary>
        IGridDataModel Model { get; }

        /// <summary>
        /// Gets the overall column width of the section.
        /// </summary>
        int Grid { get; }

        /// <summary>
        /// Gets an array of all rows in the sections.
        /// </summary>
        IGridRow[] Rows { get; }

        /// <summary>
        /// Gets whether the section has any rows.
        /// </summary>
        bool HasRows { get; }

        /// <summary>
        /// Gets the first row of the section. If the section doesn't contain any rows, this property will return <c>null</c>.
        /// </summary>
        IGridRow FirstRow { get; }

        /// <summary>
        /// Gets the last row of the section. If the section doesn't contain any rows, this property will return <c>null</c>.
        /// </summary>
        IGridRow LastRow { get; }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets a textual representation of the section - eg. to be used in Examine.
        /// </summary>
        /// <returns>An instance of <see cref="string"/> representing the value of the section.</returns>
        string GetSearchableText();
        
        #endregion

    }

}