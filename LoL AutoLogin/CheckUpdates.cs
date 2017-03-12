using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace LoL_AutoLogin
{
    class CheckUpdates
    {
        public static string DownloadUrl = "https://github.com/Profesor08/LoL-AutoLogin/releases";

        private static string updateUrl = "https://api.github.com/repos/Profesor08/LoL-AutoLogin/releases";
        private static string userAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";

        public bool UpdateAvailable(Version version)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.UserAgent, userAgent);

                string response = wc.DownloadString(updateUrl);
                var json = JArray.Parse(response);
                var new_version = new Version(json[0]["tag_name"].ToString());

                return new_version.CompareTo(version) > 0;
            }
        }

        public static bool Check(string version)
        {
            return new CheckUpdates().UpdateAvailable(new Version(version));
        }
    }
}
