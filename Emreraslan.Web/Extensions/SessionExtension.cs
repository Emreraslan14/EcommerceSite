using System.Text.Json;

namespace Emreraslan.Web.Extensions
{
    public static class SessionExtensions
    {
        public static void SetList<T>(this ISession session, string key, List<T> value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static List<T> GetList<T>(this ISession session, string key)
        {
            var serializedValue = session.GetString(key);
       
            return serializedValue == null ? null : JsonSerializer.Deserialize<List<T>>(serializedValue);
        }
    }
}
