using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Skybrud.Essentials.Reflection;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Various utility methods for the grid.
    /// </summary>
    public static class GridUtils {

        #region Version specific methods

        /// <summary>
        /// Gets the assembly version as a string.
        /// </summary>
        public static string GetVersion() {
            return ReflectionUtils.GetVersion(typeof(GridUtils).Assembly);
        }

        /// <summary>
        /// Gets the file version as a string.
        /// </summary>
        /// <returns></returns>
        public static string? GetFileVersion() {
            Assembly assembly = typeof(GridUtils).Assembly;
            return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        }
        
        #endregion

        #region Rendering

        /// <summary>
        /// Gets whether the specified <paramref name="name"/> is a valid partial name.
        /// </summary>
        /// <param name="name">The name of the partial.</param>
        /// <returns><c>true</c> if <paramref name="name"/> is valid; otherwise <c>false</c>.</returns>
        public static bool IsValidPartialName(string name) {
            return Regex.IsMatch(name ?? string.Empty, "^[a-zA-Z0-9-\\._ ]+$");
        }

        #endregion

    }

}