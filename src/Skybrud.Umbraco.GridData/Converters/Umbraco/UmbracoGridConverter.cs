using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Config;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;
using Skybrud.Umbraco.GridData.Values;

namespace Skybrud.Umbraco.GridData.Converters.Umbraco {

    /// <summary>
    /// Converter for handling the default editors (and their values and configs) of Umbraco.
    /// </summary>
    public class UmbracoGridConverter : IGridConverter {

        /// <summary>
        /// Converts the specified <code>token</code> into an instance of <code>IGridControlValue</code>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <code>JToken</code> representing the control value.</param>
        /// <param name="value">The converted value.</param>
        public virtual bool ConvertControlValue(GridControl control, JToken token, out IGridControlValue value) {
            
            value = null;

            switch (control.Editor.Alias) {

                case "media":
                    value = GridControlMediaValue.Parse(control, token as JObject);
                    break;

                case "embed":
                    value = GridControlEmbedValue.Parse(control, token);
                    break;

                case "rte":
                    value = GridControlRichTextValue.Parse(control, token);
                    break;

                case "macro":
                    value = GridControlMacroValue.Parse(control, token as JObject);
                    break;

                case "headline":
                case "quote":
                    value = GridControlTextValue.Parse(control, token);
                    break;

            }
            
            return value != null;
        
        }

        /// <summary>
        /// Converts the specified <code>token</code> into an instance of <code>IGridEditorConfig</code>.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="token">The instance of <code>JToken</code> representing the editor config.</param>
        /// <param name="config">The converted config.</param>
        public virtual bool ConvertEditorConfig(GridEditor editor, JToken token, out IGridEditorConfig config) {
       
            config = null;

            switch (editor.Alias) {

                case "media":
                    config = GridEditorMediaConfig.Parse(editor, token as JObject);
                    break;

                case "headline":
                case "quote":
                    config = GridEditorTextConfig.Parse(editor, token as JObject);
                    break;

            }

            return config != null;
        
        }

        /// <summary>
        /// Gets an instance <code>GridControlWrapper</code> for the specified <code>control</code>.
        /// </summary>
        /// <param name="control">The control to be wrapped.</param>
        /// <param name="wrapper">The wrapper.</param>
        public virtual bool GetControlWrapper(GridControl control, out GridControlWrapper wrapper) {

            wrapper = null;
            
            switch (control.Editor.Alias) {

                case "media":
                    wrapper = control.GetControlWrapper<GridControlMediaValue, GridEditorMediaConfig>();
                    break;

                case "embed":
                    wrapper = control.GetControlWrapper<GridControlEmbedValue>();
                    break;

                case "rte":
                    wrapper = control.GetControlWrapper<GridControlRichTextValue>();
                    break;

                case "macro":
                    wrapper = control.GetControlWrapper<GridControlMacroValue>();
                    break;

                case "quote":
                case "headline":
                    wrapper = control.GetControlWrapper<GridControlTextValue, GridEditorTextConfig>();
                    break;

            }

            return wrapper != null;

        }
    
    }

}
