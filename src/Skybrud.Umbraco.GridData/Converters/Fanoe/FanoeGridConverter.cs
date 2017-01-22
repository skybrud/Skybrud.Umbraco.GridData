using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Config;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;
using Skybrud.Umbraco.GridData.Values;

namespace Skybrud.Umbraco.GridData.Converters.Fanoe {
    
    /// <summary>
    /// Converter for Grid editors from the Fanoe starter kit.
    /// </summary>
    public class FanoeGridConverter : IGridConverter {

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the control value.</param>
        /// <param name="value">The converted value.</param>
        public bool ConvertControlValue(GridControl control, JToken token, out IGridControlValue value) {
            
            value = null;
            
            switch (control.Editor.Alias) {

                case "media_wide":
                case "media_wide_cropped":
                    value = GridControlMediaValue.Parse(control, token as JObject);
                    break;

                case "banner_headline":
                case "banner_tagline":
                case "headline_centered":
                case "abstract":
                case "paragraph":
                case "quote_D":
                case "code":
                    value = GridControlTextValue.Parse(control, token);
                    break;
            
            }
            
            return value != null;
        
        }

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridEditorConfig"/>.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the editor config.</param>
        /// <param name="config">The converted config.</param>
        public bool ConvertEditorConfig(GridEditor editor, JToken token, out IGridEditorConfig config) {
            
            config = null;

            switch (editor.Alias) {

                case "media_wide":
                case "media_wide_cropped":
                    config = GridEditorMediaConfig.Parse(editor, token as JObject);
                    break;

                case "banner_headline":
                case "banner_tagline":
                case "headline_centered":
                case "abstract":
                case "paragraph":
                case "quote_D":
                case "code":
                    config = GridEditorTextConfig.Parse(editor, token as JObject);
                    break;
                    
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

            switch (control.Editor.Alias) {

                case "media_wide":
                case "media_wide_cropped":
                    wrapper = control.GetControlWrapper<GridControlMediaValue, GridEditorMediaConfig>();
                    break;

                case "banner_headline":
                case "banner_tagline":
                case "headline_centered":
                case "abstract":
                case "paragraph":
                case "quote_D":
                case "code":
                    wrapper = control.GetControlWrapper<GridControlTextValue, GridEditorTextConfig>();
                    break;
            
            }

            return wrapper != null;

        }
    
    }

}