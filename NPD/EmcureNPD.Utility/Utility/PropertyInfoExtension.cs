using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace EmcureNPD.Utility.Utility
{
    public static class PropertyInfoExtension
    {
        private static PropertyInfo GetDeepPropertyValue(this object instance, string path)
        {
            var pp = path.Split('.');
            Type t = instance.GetType();
            PropertyInfo propInfo = null;
            foreach (var prop in pp)
            {
               
                propInfo = t.GetProperty(prop);
               
                if (propInfo != null)
                {
                    instance = propInfo.GetValue(instance, null);
                    t = propInfo.PropertyType;
                }
                
            }
            return propInfo;
        }

        public static string TryGetDisplayName<T>(this T instance, string path) where T : new()
        {
            var propertyInfo = instance.GetDeepPropertyValue(path);
            if (propertyInfo == null)
                return path;
            string result = null;
            try
            {
                var attrs = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true);
                if (attrs.Any())
                    result = ((DisplayAttribute)attrs[0]).Name;
            }
            catch (Exception)
            {
                //eat the exception
            }
            return result;
        }
    }
}
