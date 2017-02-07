using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LoL_AutoLogin
{
    class Program
    {

        static void Main(string[] args)
        {
            //args = new string[] {"7000", "123", "C:\\Games\\League of Legends\\" };
            if (args.Length == 3)
            {
                int timeout = int.Parse(args[0]);
                string password = args[1];
                string gamePath = args[2];

                LeagueClient client = new LeagueClient(gamePath, "LeagueClient.exe", "LeagueClientUx");

                try
                {
                    if (!client.IsRunning())
                    {
                        client.Start();
                        System.Threading.Thread.Sleep(timeout);
                    }
                    
                    client.Login(password);
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    MessageBox.Show(ex.Message + ": " + client.GetClientFile(), "AutoLogin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Set parameters: [timeout] [password] [gamepath]", "AutoLogin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
