namespace Skybrud.Umbraco.GridData.Rendering {

    /// <summary>
    /// Helper class for wrapping a grid control and its strongly typed value. The wrapper class
    /// can be used as the model for a partial view.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class GridControlWrapper<T> {

        #region Properties

        /// <summary>
        /// Gets a reference to the wrapped control.
        /// </summary>
        public GridControl Control { get; private set; }

        /// <summary>
        /// Gets a reference to the control editor.
        /// </summary>
        public GridEditor Editor {
            get { return Control == null ? null : Control.Editor; }
        }

        /// <summary>
        /// Gets a referece to the control value.
        /// </summary>
        public T Value { get; private set; }

        #endregion

        #region Constructors

        internal GridControlWrapper(GridControl control, T value) {
            Control = control;
            Value = value;
        }

        #endregion

    }

}