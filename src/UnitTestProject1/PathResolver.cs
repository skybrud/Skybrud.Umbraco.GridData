using System.IO;

namespace UnitTestProject1 {

    public static class PathResolver {

        public static string MapPath(string path) {
            string root = Path.GetDirectoryName(typeof(PathResolver).Assembly.Location);
            return path.StartsWith("~/") ? root + path.Substring(1) : path;
        }

    }

}