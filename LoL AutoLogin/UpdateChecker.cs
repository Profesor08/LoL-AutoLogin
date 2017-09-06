using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace LoL_AutoLogin
{
    class UpdateChecker
    {
        public static readonly string DownloadUrl = "https://github.com/Profesor08/LoL-AutoLogin/releases";
        public static readonly string UpdateUrl = "https://api.github.com/repos/Profesor08/LoL-AutoLogin/releases";

        public static readonly string UserAgent = "LoL AutoLogin";

        public bool IsNewVersion(Version currentVersion)
        {
            using (var wc = new WebClient())
            {
                try 
                {
                    wc.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);

                    var res = wc.DownloadString(UpdateUrl);
                    var json = JArray.Parse(res);

                    if (json != null && json.Count > 0)
                    {
                        var lastVersion = new Version(json[0]["tag_name"].ToString());

                        return lastVersion.CompareTo(currentVersion) > 0;
                    }
                }
                catch(Exception ex)
                {
                    Log.Write(ex);
                }
            }

            return false;
        }

        public static bool Check(string version)
        {
            return new UpdateChecker().IsNewVersion(new Version(version));
        }
    }
}
