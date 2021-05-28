using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PublicCms.Web.Extensions
{
    public static class SessionExtensions
    {
        private static string sessionKey = "EditingPage";
        public static void SavePageToSession(this ISession session, object page)
        {
            session.SetString(sessionKey, JsonSerializer.Serialize(page));
        }
        public static T GetPageFromSession<T>(this ISession session)
        {
            var val = session.GetString(sessionKey);
            return val == null ? default(T) : JsonSerializer.Deserialize<T>(val);

        }
    }
}
