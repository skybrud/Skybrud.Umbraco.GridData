using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Config;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;
using Skybrud.Umbraco.GridData.Values;

namespace Skybrud.Umbraco.GridData.Fanoe {
    
    public class FanoeGridConverter : IGridConverter {
        
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