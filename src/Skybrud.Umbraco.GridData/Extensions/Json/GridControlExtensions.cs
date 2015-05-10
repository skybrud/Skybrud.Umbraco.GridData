using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;

namespace Skybrud.Umbraco.GridData.Extensions.Json {

    /// <summary>
    /// A set of extension methods for the <code>GridControl</code> class.
    /// </summary>
    public static class GridControlExtensions {

        /// <summary>
        /// Initializes a new control wrapper around <code>control</code>.
        /// </summary>
        /// <typeparam name="TValue">The type of the control value.</typeparam>
        /// <param name="control">The control to wrap.</param>
        public static GridControlWrapper<TValue> GetControlWrapper<TValue>(this GridControl control) where TValue : IGridControlValue {

            // Get the value
            TValue value = control.GetValue<TValue>();

            // Wrap the control
            return new GridControlWrapper<TValue>(control, value);

        }

        /// <summary>
        /// Initializes a new control wrapper around <code>control</code>.
        /// </summary>
        /// <typeparam name="TValue">The type of the control value.</typeparam>
        /// <typeparam name="TConfig">The type of the editor config.</typeparam>
        /// <param name="control">The control to wrap.</param>
        public static GridControlWrapper<TValue, TConfig> GetControlWrapper<TValue, TConfig>(this GridControl control) where TValue : IGridControlValue where TConfig : IGridEditorConfig {

            // Get the value
            TValue value = control.GetValue<TValue>();

            // Get the configuration
            TConfig config = control.Editor.GetConfig<TConfig>();

            // Wrap the control
            return new GridControlWrapper<TValue, TConfig>(control, value, config);

        }

    }

}