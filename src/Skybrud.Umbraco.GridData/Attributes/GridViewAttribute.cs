using System;

namespace Skybrud.Umbraco.GridData.Attributes {

    /// <summary>
    /// Attribute used for specifying the suggested view path for a grid control value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class GridViewAttribute : Attribute {

        /// <summary>
        /// Gets the suggested view path.
        /// </summary>
        public string ViewPath { get; }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="viewPath"/>.
        /// </summary>
        /// <param name="viewPath">The suggested view path.</param>
        public GridViewAttribute(string viewPath) {
            if (string.IsNullOrWhiteSpace(viewPath)) throw new ArgumentNullException(nameof(viewPath));
            ViewPath = viewPath;
        }

    }

}