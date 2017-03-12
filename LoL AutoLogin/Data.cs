using Microsoft.Win32;
using System;
using System.IO;

namespace LoL_AutoLogin
{
    class Data
    {

        public static string ClientFile = "LeagueClient.exe";

        public static string ClientUx = "LeagueClientUx";

        public static string Client = "LeagueClient";

        private static bool showUI = true;

        private static string gamePath;

        private static string login;

        private static string password;

        private static bool TrackChanges = false;

        public static bool Changed = false;

        public static string GamePath
        {
            get
            {
                return gamePath;
            }

            set
            {
                gamePath = value;

                if (TrackChanges)
                {
                    Changed = true;
                }
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
                login = value;

                if (TrackChanges)
                {
                    Changed = true;
                }
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
                password = value;

                if (TrackChanges)
                {
                    Changed = true;
                }
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
                showUI = value;

                if (TrackChanges)
                {
                    Changed = true;
                }
            }
        }

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
            catch (Exception ex)
            {
                Log.Write(ex);
                ShowUI = true;
            }

            try
            {
                GamePath = GetGamePath();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                ShowUI = true;
                GamePath = "";
            }

            if (!File.Exists(GamePath + ClientFile))
            {
                Log.Write("Game path is wrong. File " + ClientFile + " not found.");
                ShowUI = true;
                GamePath = "";
            }

            try
            {
                Login = GetLogin();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                ShowUI = true;
                Login = "";
            }

            try
            {
                Password = GetPassword();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                ShowUI = true;
                Password = "";
            }

            TrackChanges = true;
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
