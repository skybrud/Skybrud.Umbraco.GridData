using System;

namespace Skybrud.Umbraco.GridData.Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class GridViewAttribute : Attribute {

        public string ViewPath { get; }

        public GridViewAttribute(string viewPath) {
            if (string.IsNullOrWhiteSpace(viewPath)) throw new ArgumentNullException(nameof(viewPath));
            ViewPath = viewPath;
        }

    }

}