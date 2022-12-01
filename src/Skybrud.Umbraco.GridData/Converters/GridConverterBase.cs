using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Config;
using Skybrud.Umbraco.GridData.Models.Values;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Converters {

    /// <summary>
    /// Abstract base implementation of <see cref="IGridConverter"/>.
    /// </summary>
    public abstract class GridConverterBase : IGridConverter {

        public virtual bool GetConfigType(GridEditor? editor, out Type? type) {
            type = null;
            return false;
        }

        public virtual bool GetValueType(GridControl? control, out Type? type) {
            type = null;
            return false;
        }

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">A reference to the parent <see cref="GridControl"/>.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the control value.</param>
        /// <param name="value">The converted control value.</param>
        public virtual bool ConvertControlValue(GridControl control, JToken? token, out IGridControlValue? value) {
            value = null;
            return false;
        }

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridEditorConfig"/>.
        /// </summary>
        /// <param name="editor">A reference to the parent <see cref="GridEditor"/>.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the editor config.</param>
        /// <param name="config">The converted editor config.</param>
        public virtual bool ConvertEditorConfig(GridEditor? editor, JToken? token, out IGridEditorConfig? config) {
            config = null;
            return false;
        }

        public virtual bool WriteSearchableText(GridContext context, IPublishedElement element, TextWriter writer) {
            return false;
        }

        public virtual bool IsValid(IGridControlValue? value, out bool result) {
            result = false;
            return false;
        }

        public virtual bool IsValid(IPublishedElement? element, out bool result) {
            result = false;
            return false;
        }

        /// <summary>
        /// Returns whether <paramref name="value"/> is contained in <paramref name="source"/> (case insensitive).
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns><c>true</c> if <paramref name="source"/> contains <paramref name="value"/>; otherwise <c>false</c>.</returns>
        protected bool ContainsIgnoreCase(string source, string value) {
            if (string.IsNullOrWhiteSpace(source)) return false;
            if (string.IsNullOrWhiteSpace(value)) return false;
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(source, value, CompareOptions.IgnoreCase) >= 0;
        }

        /// <summary>
        /// Returns whether <paramref name="value"/> is equal <paramref name="source"/> (case insensitive).
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns><c>true</c> if <paramref name="value"/> equal to <paramref name="source"/>; otherwise <c>false</c>.</returns>
        protected bool EqualsIgnoreCase(string source, string value) {
            if (string.IsNullOrWhiteSpace(source)) return false;
            if (string.IsNullOrWhiteSpace(value)) return false;
            return source.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

    }

}