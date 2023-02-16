using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Config;
using Skybrud.Umbraco.GridData.Models.Values;
using Umbraco.Cms.Core.Web;

#pragma warning disable CS1591

namespace Skybrud.Umbraco.GridData.Converters.Umbraco {

    /// <summary>
    /// Converter for handling the default editors (and their values and configs) of Umbraco.
    /// </summary>
    public class UmbracoGridConverter : GridConverterBase {

        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public UmbracoGridConverter(IUmbracoContextAccessor umbracoContextAccessor) {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public override bool TryGetConfigType(GridEditor editor, [NotNullWhen(true)] out Type? type) {

            type = null;

            if (IsMediaEditor(editor)) {
                type = typeof(GridEditorMediaConfig);
            } else if (IsTextStringEditor(editor)) {
                type = typeof(GridEditorTextConfig);
            }

            return type != null;

        }

        public override bool TryGetValueType(GridControl control, [NotNullWhen(true)] out Type? type) {

            type = null;

            if (IsEmbedEditor(control.Editor)) {
                type = typeof(GridControlEmbedValue);
            } else if (IsMacroEditor(control.Editor)) {
                type = typeof(GridControlMacroValue);
            } else if (IsMediaEditor(control.Editor)) {
                type = typeof(GridControlMediaValue);
            } else if (IsRichTextEditor(control.Editor)) {
                type = typeof(GridControlRichTextValue);
            } else if (IsTextStringEditor(control.Editor)) {
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
        public override bool TryConvertControlValue(GridControl control, JToken token, [NotNullWhen(true)] out IGridControlValue? value) {

            value = null;

            if (IsEmbedEditor(control.Editor)) {
                value = new GridControlEmbedValue(control);
            } else if (IsMacroEditor(control.Editor)) {
                value = new GridControlMacroValue(control);
            } else if (IsMediaEditor(control.Editor)) {
                value = ParseGridControlMediaValue(control);
            } else if (IsRichTextEditor(control.Editor)) {
                value = new GridControlRichTextValue(control);
            } else if (IsTextStringEditor(control.Editor)) {
                value = new GridControlTextValue(control);
            }

            return value != null;

        }

        protected virtual IGridControlValue ParseGridControlMediaValue(GridControl control) {

            GridControlMediaValue value = new(control);

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
        public override bool TryConvertEditorConfig(GridEditor editor, JToken token, [NotNullWhen(true)] out IGridEditorConfig? config) {

            config = null;

            if (IsMediaEditor(editor)) {
                config = new GridEditorMediaConfig((JObject) token, editor);
            } else if (IsTextStringEditor(editor)) {
                config = new GridEditorTextConfig((JObject) token, editor);
            }

            return config != null;

        }

        protected static bool IsEmbedEditor(GridEditor? editor) {
            return editor?.View == "embed";
        }

        protected static bool IsTextStringEditor(GridEditor? editor) {
            return editor?.View == "textstring";
        }

        protected static bool IsMediaEditor(GridEditor? editor) {
            return editor?.View == "media";
        }

        protected static bool IsMacroEditor(GridEditor? editor) {
            return editor?.View == "macro";
        }

        protected static bool IsRichTextEditor(GridEditor? editor) {
            return editor?.View == "rte";
        }

    }

}