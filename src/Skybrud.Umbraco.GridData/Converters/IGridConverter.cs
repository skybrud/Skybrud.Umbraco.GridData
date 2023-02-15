using System;
using System.Diagnostics.CodeAnalysis;
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

        /// <summary>
        /// Attemtps to get the type of the configuration object of the specified <paramref name="editor"/>.
        /// </summary>
        /// <param name="editor">The editor.</param>
        /// <param name="type">When this method returns, holds an instance of <see cref="Type"/> representing the type if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        bool TryGetConfigType(GridEditor editor, [NotNullWhen(true)] out Type? type);

        /// <summary>
        /// Attempts to get the type of the value of the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="type">When this method returns, holds an instance of <see cref="Type"/> representing the type if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        bool TryGetValueType(GridControl control, [NotNullWhen(true)] out Type? type);

        /// <summary>
        /// Attempts to convert the specified <paramref name="token"/> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the control value.</param>
        /// <param name="value">The converted value.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        bool TryConvertControlValue(GridControl control, JToken token, [NotNullWhen(true)] out IGridControlValue? value);

        /// <summary>
        /// Attempts to convert the specified <paramref name="token"/> into an instance of <see cref="IGridEditorConfig"/>.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the editor config.</param>
        /// <param name="config">The converted config.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        bool TryConvertEditorConfig(GridEditor editor, JToken token, [NotNullWhen(true)] out IGridEditorConfig? config);

        /// <summary>
        /// Attempts to write a string representation of <paramref name="element"/> to <paramref name="writer"/>.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <param name="element">The element.</param>
        /// <param name="writer">The writer.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        bool TryWriteSearchableText(GridContext context, IPublishedElement element, TextWriter writer);

        /// <summary>
        /// Attempts to check whether the specified <paramref name="value"/> represents a valid grid control value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="result">When this method returns, holds a boolean value indicating whether <paramref name="value"/> is valid if successful; otherwise, <see langword="false"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        bool TryGetValid(IGridControlValue value, out bool result);

        /// <summary>
        /// Attempts to check whether the specified <paramref name="element"/> represents a valid element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="result">When this method returns, holds a boolean value indicating whether <paramref name="element"/> is valid if successful; otherwise, <see langword="false"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        bool TryGetValid(IPublishedElement element, out bool result);

    }

}