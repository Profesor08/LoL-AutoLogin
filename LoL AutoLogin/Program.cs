using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LoL_AutoLogin
{
    class Program
    {

        [DllImportAttribute("User32.dll")]
        private static extern int FindWindow(String ClassName, String WindowName);

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        private const int WS_SHOWNORMAL = 1;

        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                int timeout = int.Parse(args[0]);
                string passowrd = args[1];

                Process process = new Process();
                // Configure the process using the StartInfo properties.
                process.StartInfo.FileName = "LeagueClient.exe";
                process.StartInfo.Arguments = "-n";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();

                // wait for process loads
                System.Threading.Thread.Sleep(timeout);

                Process[] localByName = Process.GetProcessesByName("LeagueClientUx");

                foreach (var p in localByName)
                {
                    SetForegroundWindow(p.MainWindowHandle);
                    ShowWindowAsync(p.MainWindowHandle, WS_SHOWNORMAL);
                    System.Threading.Thread.Sleep(500);
                    // clearing input field
                    SendKeys.SendWait("^{a}");
                    SendKeys.SendWait(passowrd);
                    SendKeys.SendWait("{ENTER}");
                    Application.Exit();
                }
            }
            else
            {
                Console.WriteLine("Set parameters: [timeout] [password]");
                Console.ReadLine();
            }
        }
    }
}
