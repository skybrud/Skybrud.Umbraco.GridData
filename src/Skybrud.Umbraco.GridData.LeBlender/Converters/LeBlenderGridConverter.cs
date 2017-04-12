using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.LeBlender.Config;
using Skybrud.Umbraco.GridData.LeBlender.Values;
using Skybrud.Umbraco.GridData.Rendering;

namespace Skybrud.Umbraco.GridData.LeBlender.Converters {
    
    public class LeBlenderGridConverter : IGridConverter {

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">A reference to the parent <see cref="GridControl"/>.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the control value.</param>
        /// <param name="value">The converted control value.</param>
        public bool ConvertControlValue(GridControl control, JToken token, out IGridControlValue value) {
            value = null;
            if (IsLeBlenderEditor(control.Editor)) {
                value = GridControlLeBlenderValue.Parse(control);
            }
            return value != null;
        }

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridEditorConfig"/>.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the editor config.</param>
        /// <param name="config">The converted editor config.</param>
        public bool ConvertEditorConfig(GridEditor editor, JToken token, out IGridEditorConfig config) {
            config = null;
            if (IsLeBlenderEditor(editor)) {
                config = GridEditorLeBlenderConfig.Parse(editor, token as JObject);
            }
            return config != null;
        }

        /// <summary>
        /// Gets an instance <see cref="GridControlWrapper"/> for the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The control to be wrapped.</param>
        /// <param name="wrapper">The wrapper.</param>
        public bool GetControlWrapper(GridControl control, out GridControlWrapper wrapper) {
            wrapper = null;
            if (IsLeBlenderEditor(control.Editor)) {
                wrapper = control.GetControlWrapper<GridControlLeBlenderValue, GridEditorLeBlenderConfig>();
            }
            return wrapper != null;
        }

        private bool IsLeBlenderEditor(GridEditor editor) {
            return editor.Alias.ToLower().StartsWith("leblender.");
        }
    
    }

}