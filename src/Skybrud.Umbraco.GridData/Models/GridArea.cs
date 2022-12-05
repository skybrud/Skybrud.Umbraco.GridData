using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Factories;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Class representing an area in an Umbraco Grid.
    /// </summary>
    public class GridArea : GridElement {

        #region Properties

        /// <summary>
        /// Gets a reference to the entire <see cref="GridDataModel"/>.
        /// </summary>
        [JsonIgnore]
        public GridDataModel Model => Section?.Model;

        /// <summary>
        /// Gets a reference to the parent <see cref="GridSection"/>.
        /// </summary>
        [JsonIgnore]
        public GridSection Section => Row?.Section;

        /// <summary>
        /// Gets a reference to the parent <see cref="GridRow"/>.
        /// </summary>
        [JsonIgnore]
        public GridRow Row { get; private set; }

        /// <summary>
        /// Gets the column width of the area.
        /// </summary>
        public int Grid { get; private set; }

        /// <summary>
        /// Gets wether all editors are allowed for this area.
        /// </summary>
        public bool AllowAll { get; private set; }

        /// <summary>
        /// Gets an array of all editors allowed for this area. If <see cref="AllowAll"/> is <c>true</c>, this
        /// array may be empty.
        /// </summary>
        public string[] Allowed { get; private set; }

        /// <summary>
        /// Gets an array of all controls added to this area.
        /// </summary>
        public IReadOnlyList<GridControl> Controls { get; private set; }

        /// <summary>
        /// Gets a reference to the previous area.
        /// </summary>
        public GridArea PreviousArea { get; internal set; }

        /// <summary>
        /// Gets a reference to the next area.
        /// </summary>
        public GridArea NextArea { get; internal set; }

        /// <summary>
        /// Gets whether the area has any controls.
        /// </summary>
        public bool HasControls => Controls.Count > 0;

        /// <summary>
        /// Gets the first control of the area. If the area doesn't contain
        /// any controls, this property will return <c>null</c>.
        /// </summary>
        public GridControl FirstControl => Controls.FirstOrDefault();

        /// <summary>
        /// Gets the last control of the area. If the area doesn't contain
        /// any controls, this property will return <c>null</c>.
        /// </summary>
        public GridControl LastControl => Controls.LastOrDefault();

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
        public GridArea(JObject json, GridRow row, IGridFactory factory) : base(json) {

            Row = row;
            Grid = json.GetInt32("grid");
            AllowAll = json.GetBoolean("allowAll");
            Allowed = json.GetStringArray("allowed");
            Controls = json.GetArray("controls", x => factory.CreateGridControl(x, this)) ?? Array.Empty<GridControl>();

            // Update "PreviousControl" and "NextControl" properties
            for (int i = 1; i < Controls.Count; i++) {
                Controls[i - 1].NextControl = Controls[i];
                Controls[i].PreviousControl = Controls[i - 1];
            }

        }

        #endregion

        #region Member methods

        public override void WriteSearchableText(GridContext context, TextWriter writer) {
            foreach (GridControl control in Controls) control.WriteSearchableText(context, writer);
        }

        #endregion

    }

}