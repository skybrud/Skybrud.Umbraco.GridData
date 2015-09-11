namespace UnitTestProject1 {

    public static class PathResolver {

        public static string MapPath(string path) {
            
            // TODO: Find the root directory automatically
            string root = "D:/Repositories/Skybrud.Umbraco.GridData/src/UnitTestProject1";
            
            return path.StartsWith("~/") ? root + path.Substring(1) : path;
        
        }

    }

}