using Skybrud.Umbraco.GridData.Interfaces;

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

        /// <summary>
        /// Gets whether the value of the grid control is valid.
        /// </summary>
        public bool IsValid {
            get { return Control.Value != null && Control.Value.IsValid; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Wraps an instance of <code>GridControl</code>.
        /// </summary>
        /// <param name="control">The control to be wrap.</param>
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
    public class GridControlWrapper<TValue> : GridControlWrapper where TValue : IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a referece to the control value.
        /// </summary>
        public TValue Value { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Wraps an instance of <code>GridControl</code>.
        /// </summary>
        /// <param name="control">The control to be wrap.</param>
        /// <param name="value">The type of the value.</param>
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
    public class GridControlWrapper<TValue, TConfig> : GridControlWrapper<TValue> where TValue : IGridControlValue  where TConfig : IGridEditorConfig {

        #region Properties

        /// <summary>
        /// Gets a referece to the editor config.
        /// </summary>
        public TConfig Config { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Wraps an instance of <code>GridControl</code>.
        /// </summary>
        /// <param name="control">The control to be wrap.</param>
        /// <param name="value">The type of the value.</param>
        /// <param name="config">The type of the editor config.</param>
        public GridControlWrapper(GridControl control, TValue value, TConfig config) : base(control, value) {
            Config = config;
        }

        #endregion

    }

}