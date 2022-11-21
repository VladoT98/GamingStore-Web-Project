using Newtonsoft.Json;

namespace GamingStore.Infrastructure.Extensions
{
    public static class SessionHelperExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
            => session.SetString(key, JsonConvert.SerializeObject(value));

        public static T GetObjectFromJson<T>(this ISession session, string key)
            => session.GetString(key) == null
                ? default
                : JsonConvert.DeserializeObject<T>(session.GetString(key));
    }
}