using Microsoft.Win32;
using System;
using System.IO;

namespace LoL_AutoLogin
{
    class Data
    {

        public static readonly string ClientFile = "LeagueClient.exe";

        public static readonly string ClientUx = "LeagueClientUx";

        public static readonly string Client = "LeagueClient";

        public static bool Changed = false;

        private static string gamePath;

        private static string login;

        private static string password;

        private static bool showUI = true;

        public static string GamePath
        {
            get
            {
                return gamePath;
            }

            set
            {
                Changed = true;
                gamePath = value;
            }
        }

        public static string Login
        {
            get
            {
                return login;
            }

            set
            {
                Changed = true;
                login = value;
            }
        }

        public static string Password
        {
            get
            {
                return password;
            }

            set
            {
                Changed = true;
                password = value;
            }
        }

        public static bool ShowUI
        {
            get
            {
                return showUI;
            }

            set
            {
                Changed = true;
                showUI = value;
            }
        }

        public static void Save()
        {
            Reg.Set("ShowUI", ShowUI);
            Reg.Set("Path", GamePath);
            Reg.Set("Login", Encryption.Encrypt(Login));
            Reg.Set("Password", Encryption.Encrypt(Password));
            Changed = false;
        }

        public static void Load()
        {
            try
            {
                showUI = GetShowUI();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                showUI = true;
            }

            try
            {
                gamePath = GetGamePath();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                showUI = true;
                gamePath = "";
            }

            if (!File.Exists(GamePath + ClientFile))
            {
                Log.Write("Game path is wrong. File " + ClientFile + " not found.");
                showUI = true;
                gamePath = "";
            }

            try
            {
                login = GetLogin();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                showUI = true;
                login = "";
            }

            try
            {
                password = GetPassword();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                showUI = true;
                password = "";
            }
        }

        static string GetGamePath()
        {
            Log.Write("Reading game path from registry");
            var gamePath = Reg.Get("Path");

            if (gamePath == null)
            {
                Log.Write("Game directory not found. Trying to find game installation info.");
                gamePath = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Riot Games\\League of Legends\\", "Path", null);
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

            try
            {
                return Convert.ToBoolean(show.ToString());
            }
            catch (FormatException)
            {
                Log.Write("Wrong ShowUI format. Returning false.");
            }

            return true;
        }
    }
}
