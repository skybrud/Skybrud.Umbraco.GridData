using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Factories;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Class representing an area in an Umbraco Grid.
    /// </summary>
    public class GridArea : GridElement, IGridArea {

        #region Properties
        
        /// <summary>
        /// Gets a reference to the entire <see cref="IGridDataModel"/>.
        /// </summary>
        [JsonIgnore]
        public IGridDataModel Model => Section?.Model;

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridSection"/>.
        /// </summary>
        [JsonIgnore]
        public IGridSection Section => Row?.Section;

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridRow"/>.
        /// </summary>
        [JsonIgnore]
        public IGridRow Row { get; }

        /// <summary>
        /// Gets the column width of the area.
        /// </summary>
        public int Grid { get; }

        /// <summary>
        /// Gets wether all editors are allowed for this area.
        /// </summary>
        public bool AllowAll { get; }

        /// <summary>
        /// Gets an array of all editors allowed for this area. If <see cref="AllowAll"/> is <c>true</c>, this
        /// array may be empty.
        /// </summary>
        public string[] Allowed { get; }

        /// <summary>
        /// Gets an array of all controls added to this area.
        /// </summary>
        public IGridControl[] Controls { get; }

        /// <summary>
        /// Gets a reference to the previous area.
        /// </summary>
        public IGridArea PreviousArea { get; internal set; }

        /// <summary>
        /// Gets a reference to the next area.
        /// </summary>
        public IGridArea NextArea { get; internal set; }

        /// <summary>
        /// Gets whether the area has any controls.
        /// </summary>
        public bool HasControls => Controls.Length > 0;

        /// <summary>
        /// Gets the first control of the area. If the area doesn't contain any controls, this property will return <c>null</c>.
        /// </summary>
        public IGridControl FirstControl => Controls.FirstOrDefault();

        /// <summary>
        /// Gets the last control of the area. If the area doesn't contain any controls, this property will return <c>null</c>.
        /// </summary>
        public IGridControl LastControl => Controls.LastOrDefault();

        /// <summary>
        /// Gets whether at least one control within the area is valid.
        /// </summary>
        public override bool IsValid {
            get { return Controls.Any(x => x.IsValid); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object, <paramref name="row"/> and <paramref name="factory"/>.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the section.</param>
        /// <param name="row">The parent row.</param>
        /// <param name="factory">The factory used for parsing subsequent parts of the grid.</param>
        public GridArea(JObject json, IGridRow row, IGridFactory factory) : base(json) {

            Row = row;
            Grid = json.GetInt32("grid");
            AllowAll = json.GetBoolean("allowAll");
            Allowed = json.GetStringArray("allowed");
            Controls = json.GetArray("controls", x => factory.CreateGridControl(x, this)) ?? new IGridControl[0];

            // Update "PreviousControl" and "NextControl" properties
            for (int i = 1; i <  Controls.Length; i++) {
                // TODO: Due to the factory, we can no longer assume rows are GridControl
                ((GridControl) Controls[i - 1]).NextControl = Controls[i];
                ((GridControl) Controls[i]).PreviousControl = Controls[i - 1];
            }

        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns a textual representation of the area - eg. to be used in Examine.
        /// </summary>
        /// <returns>An instance of <see cref="string"/> representing the value of the area.</returns>
        public override string GetSearchableText() {
            return Controls.Aggregate("", (current, control) => current + control.GetSearchableText());
        }

        #endregion

    }

}