using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace UCMS.Core
{
    public static class JsonHelper
    {
        public static string SerializeToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static T ReadAs<T>(this HttpContent content)
        {
            var stringContent = content.ReadAsStringAsync().Result;
            return stringContent.FromJson<T>();
        }

        public static StringContent ToJsonStringContent(this object obj)
        {
            return new StringContent(obj.SerializeToJson(), Encoding.UTF8, "application/json");
        }
    }
}
