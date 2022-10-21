using System.Text;
using System.Web;

namespace RazorLibrary.Helpers
{
    static class QueryHelper
    {
        public static string ObjectPropertiesToQueryString<T>(this T obj)
        {
            var result = new StringBuilder();
            result.Append("?");

            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propValue = property.GetValue(obj);
                if (propValue == null) continue;

                if (property.PropertyType.IsArray)
                {
                    foreach (var item in (Array)propValue)
                    {
                        AppendKeyValueToStringBuilder(result, property.Name, item.ToString()!);
                    }
                }
                else
                {
                    AppendKeyValueToStringBuilder(result, property.Name, propValue.ToString()!);
                }
            }
            
            return result.ToString().TrimEnd('&');
        }

        private static void AppendKeyValueToStringBuilder(StringBuilder builder, string key, string value)
        {
            builder.Append($"{key}={HttpUtility.UrlEncode(value)}&");
        }
    }
}
