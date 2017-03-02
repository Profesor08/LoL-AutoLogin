using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace LoL_AutoLogin
{
    class Program
    {

        static void Main(string[] args)
        {

            var gamePath = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Riot Games\\League of Legends", "Path", null);
            var password = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Riot Games\\League of Legends", "Password", null);
          
            if (gamePath == null)
            {
                MessageBox.Show("Game folder not found!", "AutoLogin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password == null)
            {
                Console.WriteLine("You not have saved password. Please enter it below.");
                Console.Write("Password: ");
                password = Console.ReadLine();
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Riot Games\\League of Legends", "Password", password);
            }


            LeagueClient client = new LeagueClient(gamePath.ToString(), "LeagueClient.exe", "LeagueClientUx");

            if (client.IsRunning())
            {
                client.Stop();
            }

            client.Start();

            if (client.Ready())
            {
                client.Login(password.ToString());
            }

        }
    }
}
