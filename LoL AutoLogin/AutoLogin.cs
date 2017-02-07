using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LoL_AutoLogin
{

    public class LeagueClient
    {
        private string leagueClientPath;
        private string leageClientFile;
        private string leagueClientUx;

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public LeagueClient(string leagueClientPath, string leageClientFile, string leagueClientUx)
        {
            this.leagueClientPath = leagueClientPath;
            this.leageClientFile = leageClientFile;
            this.leagueClientUx = leagueClientUx;
        }

        public bool IsRunning()
        {
            foreach (var process in Process.GetProcessesByName(leageClientFile.Substring(0, leageClientFile.Length - 4)))
            {
                return true;
            }

            return false;
        }

        public Process Start()
        {
            Process process = new Process();

            process.StartInfo.FileName = leagueClientPath + leageClientFile;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();

            return process;
        }

        public void Login(string password)
        {
            Process[] localByName = Process.GetProcessesByName(leagueClientUx);

            foreach (var p in localByName)
            {
                SetForegroundWindow(p.MainWindowHandle);
                ShowWindowAsync(p.MainWindowHandle, 1);
                System.Threading.Thread.Sleep(500);
                // clearing input field
                SendKeys.SendWait("^{a}");
                SendKeys.SendWait(password);
                SendKeys.SendWait("{ENTER}");
            }
        }

        public string GetClientFile()
        {
            return leageClientFile;
        }
    }
}
