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

        public static void Write(Exception ex)
        {
            using (var writer = new StreamWriter(logFile, true))
            {
                writer.WriteLine(
                    "=>{0} An Error occurred: {1}  Message: {2}{3}",
                    DateTime.Now,
                    ex.StackTrace,
                    ex.Message,
                    Environment.NewLine
               );
            }
        }
    }
}
