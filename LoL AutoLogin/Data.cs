using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace LoL_AutoLogin
{
    class Data
    {

        public static string ClientFile = "LeagueClient.exe";

        public static string ClientUx = "LeagueClientUx";

        public static string Client = "LeagueClient";

        public static bool ShowUI = true;

        public static string GamePath;

        public static string Login;

        public static string Password;

        public static void Save()
        {
            Reg.Set("ShowUI", ShowUI);
            Reg.Set("Path", GamePath);
            Reg.Set("Login", Encryption.Encrypt(Login));
            Reg.Set("Password", Encryption.Encrypt(Password));
        }

        public static void Load()
        {
            try
            {
                ShowUI = GetShowUI();
            }
            catch (Exception)
            {
                ShowUI = true;
            }

            try
            {
                GamePath = GetGamePath();
            }
            catch (Exception)
            {
                ShowUI = true;
                GamePath = "";
            }

            if (!File.Exists(GamePath + ClientFile))
            {
                Error("Wrong game directory! File " + ClientFile + " not found.");
            }

            try
            {
                Login = GetLogin();
            }
            catch (Exception)
            {
                ShowUI = true;
                Login = "";
                Error("Login not set!");
            }

            try
            {
                Password = GetPassword();
            }
            catch (Exception)
            { 
                ShowUI = true;
                Password = "";
                Error("Password not set!");
            }
        }

        static void Error(string message)
        {
            MessageBox.Show(message, "AutoLogin", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static string GetGamePath()
        {
            Log.Write("Reading game path from registry");
            var gamePath = Reg.Get("Path");

            if (gamePath != null)
            {
                Log.Write("Game directory not found. Trying to find game installation info.");
                gamePath = Reg.Get("Path");
            }

            return gamePath.ToString().TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
        }

        static string GetLogin()
        {
            return Encryption.Decrypt(Reg.Get("Login", "").ToString());
        }

        static string GetPassword()
        {
            return Encryption.Decrypt(Reg.Get("Password", "").ToString());
        }

        static bool GetShowUI()
        {
            var show = Reg.Get("ShowUI");

            if (show == null)
            {
                return true;
            }

            return Convert.ToBoolean(show.ToString());
        }
    }
}
