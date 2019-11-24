using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Config;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Editors;
using Skybrud.Umbraco.GridData.Rendering;
using Skybrud.Umbraco.GridData.Values;

namespace Skybrud.Umbraco.GridData.Converters.Umbraco {

    /// <summary>
    /// Converter for handling the default editors (and their values and configs) of Umbraco.
    /// </summary>
    public class UmbracoGridConverter : IGridConverter {

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the control value.</param>
        /// <param name="value">The converted value.</param>
        public virtual bool ConvertControlValue(GridControl control, JToken token, out IGridControlValue value) {
            
            value = null;

            if (IsEmbedEditor(control.Editor)) {
                value = GridControlEmbedValue.Parse(control, token);
            } else if (IsMacroEditor(control.Editor)) {
                value = GridControlMacroValue.Parse(control, token as JObject);
            } else if (IsMediaEditor(control.Editor)) {
                value = GridControlMediaValue.Parse(control, token as JObject);
            } else if (IsRichTextEditor(control.Editor)) {
                value = GridControlRichTextValue.Parse(control, token);
            } else if (IsTextStringEditor(control.Editor)) {
                value = GridControlTextValue.Parse(control, token);
            }
            
            return value != null;
        
        }

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridEditorConfig"/>.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the editor config.</param>
        /// <param name="config">The converted config.</param>
        public virtual bool ConvertEditorConfig(GridEditor editor, JToken token, out IGridEditorConfig config) {
       
            config = null;

            if (IsMediaEditor(editor)) {
                config = GridEditorMediaConfig.Parse(editor, token as JObject);
            } else if (IsTextStringEditor(editor)) {
                config = GridEditorTextConfig.Parse(editor, token as JObject);
            }

            return config != null;
        
        }

        /// <summary>
        /// Gets an instance <see cref="GridControlWrapper"/> for the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The control to be wrapped.</param>
        /// <param name="wrapper">The wrapper.</param>
        public virtual bool GetControlWrapper(GridControl control, out GridControlWrapper wrapper) {

            wrapper = null;
            
            if (IsEmbedEditor(control.Editor)) {
                wrapper = control.GetControlWrapper<GridControlEmbedValue>();
            } else if (IsMacroEditor(control.Editor)) {
                wrapper = control.GetControlWrapper<GridControlMacroValue>();
            } else if (IsMediaEditor(control.Editor)) {
                wrapper = control.GetControlWrapper<GridControlMediaValue, GridEditorMediaConfig>();
            } else if (IsRichTextEditor(control.Editor)) {
                wrapper = control.GetControlWrapper<GridControlRichTextValue>();
            } else if (IsTextStringEditor(control.Editor)) {
                wrapper = control.GetControlWrapper<GridControlTextValue, GridEditorTextConfig>();
            }

            return wrapper != null;

        }

        private bool IsEmbedEditor(IGridEditor editor) {
            return editor != null && editor.View == "embed";
        }

        private bool IsTextStringEditor(IGridEditor editor) {
            return editor != null && editor.View == "textstring";
        }

        private bool IsMediaEditor(IGridEditor editor) {
            return editor != null && editor.View == "media";
        }

        private bool IsMacroEditor(IGridEditor editor) {
            return editor != null && editor.View == "macro";
        }

        private bool IsRichTextEditor(IGridEditor editor) {
            return editor != null && editor.View == "rte";
        }

    }

}