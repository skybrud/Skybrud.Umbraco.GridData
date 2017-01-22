using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Rendering;

namespace Skybrud.Umbraco.GridData.Interfaces {

    /// <summary>
    /// Interface describing a Grid converter.
    /// </summary>
    public interface IGridConverter {

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <code>JToken</code> representing the control value.</param>
        /// <param name="value">The converted value.</param>
        bool ConvertControlValue(GridControl control, JToken token, out IGridControlValue value);

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridEditorConfig"/>.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="token">The instance of <code>JToken</code> representing the editor config.</param>
        /// <param name="config">The converted config.</param>
        bool ConvertEditorConfig(GridEditor editor, JToken token, out IGridEditorConfig config);

        /// <summary>
        /// Gets an instance <see cref="GridControlWrapper"/> for the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The control to be wrapped.</param>
        /// <param name="wrapper">The wrapper.</param>
        bool GetControlWrapper(GridControl control, out GridControlWrapper wrapper);

    }

}