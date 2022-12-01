using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Config;
using Skybrud.Umbraco.GridData.Models.Values;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Converters {

    /// <summary>
    /// Interface describing a Grid converter.
    /// </summary>
    public interface IGridConverter {

        bool GetConfigType(GridEditor? editor, out Type? type);

        bool GetValueType(GridControl? control, out Type? type);

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the control value.</param>
        /// <param name="value">The converted value.</param>
        bool ConvertControlValue(GridControl control, JToken? token, out IGridControlValue? value);

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridEditorConfig"/>.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the editor config.</param>
        /// <param name="config">The converted config.</param>
        bool ConvertEditorConfig(GridEditor? editor, JToken? token, out IGridEditorConfig? config);
        
        bool WriteSearchableText(GridContext context, IPublishedElement element, TextWriter writer);

        bool IsValid(IGridControlValue? value, out bool result);

        bool IsValid(IPublishedElement? element, out bool result);

    }

}