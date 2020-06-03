using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Umbraco.Core.Composing;
using Umbraco.Core.Configuration.Grid;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Internal class referencing stuff from Umbraco 7.3, which isn't available in Umbraco 7.2. Since only this class
    /// is referencing code in Umbraco 7.3, we can make a release that works for both Umbraco 7.2.x and 7.3.x.
    /// </summary>
    internal static class Howdy {

        public static string MapPath(string virtualPath) {
            return System.Web.Hosting.HostingEnvironment.MapPath(virtualPath);
        }

        public static void ReplaceEditorObjectFromConfig(GridControl control) {

            // Get the "editor" object from the JSON
            JObject editor = control.JObject.GetObject("editor");

            // Get the alias of the editor
            string alias = editor?.GetString("alias");

            // Skip if we dont have an alias
            if (string.IsNullOrWhiteSpace(alias)) return;

            // Get a reference to the grid configuration
            IGridConfig config = Current.Configs.GetConfig<IGridConfig>();

            //// Get a reference to the grid configuration
            //IGridConfig config = UmbracoConfig.For.GridConfig(
            //    ApplicationContext.Current.ProfilingLogger.Logger,
            //    ApplicationContext.Current.ApplicationCache.RuntimeCache,
            //    new DirectoryInfo(MapPath(SystemDirectories.AppPlugins)),
            //    new DirectoryInfo(MapPath(SystemDirectories.Config)),
            //    HttpContext.Current == null || HttpContext.Current.IsDebuggingEnabled
            //);

            // Find the editor in the configuration
            IGridEditorConfig found = config.EditorsConfig.Editors.FirstOrDefault(x => x.Alias == alias);

            // Skip if not found
            if (found == null) return;
            
            JObject serialized = new JObject();
            serialized["name"] = found.Name;
            serialized["alias"] = found.Alias;
            serialized["view"] = found.View;
            serialized["render"] = found.Render;
            serialized["icon"] = found.Icon;
            serialized["config"] = JObject.FromObject(found.Config);

            control.JObject["editor"] = serialized;
        
        }

    }

}