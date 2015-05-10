namespace Skybrud.Umbraco.GridData.Rendering {

    /// <summary>
    /// Helper class for wrapping a grid control.
    /// </summary>
    public class GridControlWrapper {

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

        #endregion

        #region Constructors

        public GridControlWrapper(GridControl control) {
            Control = control;
        }

        #endregion

    }

    /// <summary>
    /// Helper class for wrapping a grid control and its strongly typed value. The wrapper class
    /// can be used as the model for a partial view.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class GridControlWrapper<TValue> : GridControlWrapper {

        #region Properties

        /// <summary>
        /// Gets a referece to the control value.
        /// </summary>
        public TValue Value { get; private set; }

        #endregion

        #region Constructors

        public GridControlWrapper(GridControl control, TValue value) : base(control) {
            Value = value;
        }

        #endregion

    }
    
    /// <summary>
    /// Helper class for wrapping a grid control, its strongly typed value and the config of the
    /// editor. The wrapper class can be used as the model for a partial view.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TConfig">The type of the config.</typeparam>
    public class GridControlWrapper<TValue, TConfig> : GridControlWrapper<TValue> {

        #region Properties

        /// <summary>
        /// Gets a referece to the editor config.
        /// </summary>
        public TConfig Config { get; private set; }

        #endregion

        #region Constructors

        public GridControlWrapper(GridControl control, TValue value, TConfig config) : base(control, value) {
            Config = config;
        }

        #endregion

    }

}