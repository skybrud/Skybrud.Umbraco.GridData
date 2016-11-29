using System;
using System.Diagnostics;
using System.Reflection;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Various utility methods for the grid.
    /// </summary>
    public class GridUtils {

        #region Version specific methods

        /// <summary>
        /// Gets the assembly version as a string.
        /// </summary>
        public static string GetVersion() {
            return typeof(GridUtils).Assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// Gets the file version as a string.
        /// </summary>
        /// <returns></returns>
        public static string GetFileVersion() {
            Assembly assembly = typeof(GridUtils).Assembly;
            return assembly.Location == null ? null : FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        }

        /// <summary>
        /// Gets the amount of days between the date of this build and the date the project was started - that is the 30th of July, 2012.
        /// </summary>
        public static int GetDaysSinceStart() {

            // Get the third bit as a string
            string str = GetFileVersion().Split('.')[2];

            // Parse the string into an integer
            return Int32.Parse(str);

        }

        /// <summary>
        /// Gets the date of this build of Skybrud.Social.
        /// </summary>
        public static DateTime GetBuildDate() {
            return new DateTime(2014, 9, 18).AddDays(GetDaysSinceStart());
        }

        /// <summary>
        /// Gets the build number of the day. This is mostly used for internal
        /// purposes to distinguish builds with the same assembly version.
        /// </summary>
        public static int GetBuildNumber() {

            // Get the fourth bit as a string
            string str = GetFileVersion().Split('.')[3];

            // Parse the string into an integer
            return Int32.Parse(str);

        }
        
        #endregion

    }

}