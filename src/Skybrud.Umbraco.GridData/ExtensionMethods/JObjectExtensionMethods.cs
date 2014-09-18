using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Skybrud.Umbraco.GridData.ExtensionMethods {

    public static class JObjectExtensionMethods {

        public static JObject GetObject(this JObject obj, string propertyName) {
            if (obj == null) return null;
            return obj.GetValue(propertyName) as JObject;
        }

        public static T GetObject<T>(this JObject obj, string propertyName) {
            if (obj == null) return default(T);
            JObject child = obj.GetValue(propertyName) as JObject;
            return child == null ? default(T) : child.ToObject<T>();
        }

        public static T GetObject<T>(this JObject obj, string propertyName, Func<JObject, T> func) {
            return obj == null ? default(T) : func(obj.GetValue(propertyName) as JObject);
        }

        public static string GetString(this JObject obj, string propertyName) {
            if (obj == null) return null;
            JToken property = obj.GetValue(propertyName);
            return property == null ? null : property.Value<string>();
        }

        public static int GetInt32(this JObject obj, string propertyName) {
            if (obj == null) return 0;
            JToken property = obj.GetValue(propertyName);
            return property == null ? 0 : property.Value<int>();
        }

        public static double GetDouble(this JObject obj, string propertyName) {
            if (obj == null) return 0;
            JToken property = obj.GetValue(propertyName);
            return property == null ? 0 : property.Value<double>();
        }

        public static T[] GetArray<T>(this JObject obj, string propertyName, Func<JObject, T> func) {

            if (obj == null) return null;

            JArray property = obj.GetValue(propertyName) as JArray;
            if (property == null) return null;

            return (
                from JObject child in property
                select func(child)
            ).ToArray();

        }

    }

}