using Newtonsoft.Json;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Interface describing an area in an Umbraco Grid.
    /// </summary>
    public interface IGridArea {

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
        /// Gets the column width of the area.
        /// </summary>
        int Grid { get; }

        /// <summary>
        /// Gets wether all editors are allowed for this area.
        /// </summary>
        bool AllowAll { get; }

        /// <summary>
        /// Gets an array of all editors allowed for this area. If <see cref="AllowAll"/> is <c>true</c>, this
        /// array may be empty.
        /// </summary>
        string[] Allowed { get; }

        /// <summary>
        /// Gets an array of all controls added to this area.
        /// </summary>
        IGridControl[] Controls { get; }

        /// <summary>
        /// Gets a reference to the previous area.
        /// </summary>
        IGridArea PreviousArea { get; }

        /// <summary>
        /// Gets a reference to the next area.
        /// </summary>
        IGridArea NextArea { get; }

        /// <summary>
        /// Gets whether the area has any controls.
        /// </summary>
        bool HasControls { get; }

        /// <summary>
        /// Gets the first control of the area. If the area doesn't contain any controls, this property will return <c>null</c>.
        /// </summary>
        IGridControl FirstControl { get; }

        /// <summary>
        /// Gets the last control of the area. If the area doesn't contain any controls, this property will return <c>null</c>.
        /// </summary>
        IGridControl LastControl { get; }

        /// <summary>
        /// Gets whether at least one control within the area is valid.
        /// </summary>
        bool IsValid { get; }

        #endregion
        
        #region Member methods

        /// <summary>
        /// Gets a textual representation of the area - eg. to be used in Examine.
        /// </summary>
        /// <returns>Returns an instance of <see cref="string"/> representing the value of the area.</returns>
        string GetSearchableText();

        #endregion

    }

}