using System;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Config;
using Skybrud.Umbraco.GridData.Models.Values;
using Umbraco.Cms.Core.Web;

// ReSharper disable InconsistentNaming

namespace Skybrud.Umbraco.GridData.Converters.Umbraco {

    /// <summary>
    /// Converter for handling the default editors (and their values and configs) of Umbraco.
    /// </summary>
    public class UmbracoGridConverter : GridConverterBase {
        
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public UmbracoGridConverter(IUmbracoContextAccessor umbracoContextAccessor) {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public override bool GetConfigType(GridEditor? editor, out Type? type) {

            type = null;

            if (IsMediaEditor(editor)) {
                type = typeof(GridEditorMediaConfig);
            } else if (IsTextStringEditor(editor)) {
                type = typeof(GridEditorTextConfig);
            }

            return type != null;

        }

        public override bool GetValueType(GridControl? control, out Type? type) {

            type = null;

            if (IsEmbedEditor(control?.Editor)) {
                type = typeof(GridControlEmbedValue);
            } else if (IsMacroEditor(control?.Editor)) {
                type = typeof(GridControlMacroValue);
            } else if (IsMediaEditor(control?.Editor)) {
                type = typeof(GridControlMediaValue);
            } else if (IsRichTextEditor(control?.Editor)) {
                type = typeof(GridControlRichTextValue);
            } else if (IsTextStringEditor(control?.Editor)) {
                type = typeof(GridControlTextValue);
            }

            return type != null;

        }

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the control value.</param>
        /// <param name="value">The converted value.</param>
        public override bool ConvertControlValue(GridControl control, JToken? token, out IGridControlValue? value) {
            
            value = null;

            if (IsEmbedEditor(control.Editor)) {
                value = new GridControlEmbedValue(control, token);
            } else if (IsMacroEditor(control.Editor)) {
                value = new GridControlMacroValue(control, token as JObject);
            } else if (IsMediaEditor(control.Editor)) {
                value = ParseGridControlMediaValue(control, token as JObject);
            } else if (IsRichTextEditor(control.Editor)) {
                value = new GridControlRichTextValue(control, token);
            } else if (IsTextStringEditor(control.Editor)) {
                value = new GridControlTextValue(control, token);
            }
            
            return value != null;
        
        }

        protected virtual IGridControlValue? ParseGridControlMediaValue(GridControl control, JObject? json) {
            
            GridControlMediaValue value = new GridControlMediaValue(control, json);
            
            if (value.Id > 0 && _umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? context)) {
                value.PublishedImage = context.Media?.GetById(value.Id);
            }

            return value;

        }

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridEditorConfig"/>.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the editor config.</param>
        /// <param name="config">The converted config.</param>
        public override bool ConvertEditorConfig(GridEditor? editor, JToken? token, out IGridEditorConfig? config) {
       
            config = null;

            if (IsMediaEditor(editor)) {
                config = new GridEditorMediaConfig(editor, token as JObject);
            } else if (IsTextStringEditor(editor)) {
                config = new GridEditorTextConfig(editor, token as JObject);
            }

            return config != null;
        
        }

        protected bool IsEmbedEditor(GridEditor? editor) {
            return editor?.View == "embed";
        }

        protected bool IsTextStringEditor(GridEditor? editor) {
            return editor?.View == "textstring";
        }

        protected bool IsMediaEditor(GridEditor? editor) {
            return editor?.View == "media";
        }

        protected bool IsMacroEditor(GridEditor? editor) {
            return editor?.View == "macro";
        }

        protected bool IsRichTextEditor(GridEditor? editor) {
            return editor?.View == "rte";
        }

    }

}