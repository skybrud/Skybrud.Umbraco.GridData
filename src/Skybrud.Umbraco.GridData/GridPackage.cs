using System;
using System.Diagnostics;
using Umbraco.Cms.Core.Semver;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Static class with various information and constants about the package.
    /// </summary>
    public class GridPackage {

        /// <summary>
        /// Gets the alias of the package.
        /// </summary>
        public const string Alias = "Skybrud.Umbraco.GridData";

        /// <summary>
        /// Gets the friendly name of the package.
        /// </summary>
        public const string Name = "Limbo Grid Data";

        /// <summary>
        /// Gets the version of the package.
        /// </summary>
        public static readonly Version Version = typeof(GridPackage).Assembly.GetName().Version!;

        /// <summary>
        /// Gets the informational version of the package.
        /// </summary>
        public static readonly string InformationalVersion = FileVersionInfo.GetVersionInfo(typeof(GridPackage).Assembly.Location).ProductVersion!;

        /// <summary>
        /// Gets the semantic version of the package.
        /// </summary>
        public static readonly SemVersion SemVersion = InformationalVersion;

        /// <summary>
        /// Gets the URL of the GitHub repository for this package.
        /// </summary>
        public const string GitHubUrl = "https://github.com/skybrud/Skybrud.Umbraco.GridData/";

        /// <summary>
        /// Gets the URL of the issue tracker for this package.
        /// </summary>
        public const string IssuesUrl = "https://github.com/skybrud/Skybrud.Umbraco.GridData/issues";

        /// <summary>
        /// Gets the website URL of the package.
        /// </summary>
        public const string WebsiteUrl = "https://packages.skybrud.dk/skybrud.umbraco.griddata/v5/";

        /// <summary>
        /// Gets the URL of the documentation for this package.
        /// </summary>
        public const string DocumentationUrl = "https://packages.skybrud.dk/skybrud.umbraco.griddata/v5/docs/";

    }

}