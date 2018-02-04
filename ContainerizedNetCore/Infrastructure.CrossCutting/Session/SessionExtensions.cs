using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

namespace Infrastructure.CrossCutting.Session
{
    /// <summary>
    /// Session extensions for net core
    /// </summary>
    /// <remarks>
    /// Credits: http://benjii.me/2015/07/using-sessions-and-httpcontext-in-aspnet5-and-mvc6/
    /// </remarks>
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
