using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace LoL_AutoLogin
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {

            if (File.Exists(Log.logFile))
            {
                File.Delete(Log.logFile);
            }

            Log.Write("Reading game path from registry");
            var gamePath = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\LolAutoLogin\\", "Path", null);

            Log.Write("Reading password from registry");
            var password = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\LolAutoLogin\\", "Password", null);

            if (gamePath == null)
            {
                Log.Write("Game directory not found. Trying to find game installation info.");
                gamePath = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Riot Games\\League of Legends\\", "Path", null);

                Log.Write("Game directory not found. Prompting to select it.");
                if (gamePath == null)
                {
                    Log.Write("Opening file browser dialog.");
                    using (var fbd = new FolderBrowserDialog())
                    {
                        DialogResult result = fbd.ShowDialog();

                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            gamePath = fbd.SelectedPath;

                            Log.Write("Writing game path to registry.");
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\LolAutoLogin\\", "Path", gamePath);
                        }
                        else
                        {
                            MessageBox.Show("Game folder not selected!", "AutoLogin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else
                {
                    Log.Write("Game directory found in installation info. Writing it to registry.");
                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\LolAutoLogin\\", "Path", gamePath);
                }
            }


            if (password == null)
            {
                Log.Write("Password not found. Requesting password entering.");
                Console.WriteLine("You not have saved password. Please enter it below.");
                Console.Write("Password: ");
                password = Console.ReadLine();

                Log.Write("Writing password to registry.");
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\LolAutoLogin\\", "Password", password);
            }

            Log.Write("Initialising game client.");
            LeagueClient client = new LeagueClient(gamePath.ToString(), "LeagueClient.exe", "LeagueClientUx");

            Log.Write("Checking if game client is running.");
            if (client.IsRunning())
            {
                Log.Write("Game client is running. Stopping it.");
                client.Stop();
            }

            Log.Write("Game client not running. Starting it.");
            client.Start();

            Log.Write("Checking if game client is ready for entering password.");
            if (client.Ready())
            {
                Log.Write("Game client is ready for entering password.");
                client.Login(password.ToString());
                Log.Write("Password entering is seems to be successful");
            }

            Log.Write("Application exit.");

        }
    }
}
