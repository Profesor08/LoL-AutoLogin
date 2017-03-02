using System;
using System.IO;

namespace LoL_AutoLogin
{
    class Log
    {
        public const string logFile = "Lol AutoLogin.log";

        public static void Write(string msg)
        {
            File.AppendAllText(logFile, msg + Environment.NewLine);
        }
    }
}
