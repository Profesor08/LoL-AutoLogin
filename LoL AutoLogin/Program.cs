using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing;

namespace LoL_AutoLogin
{
    class Program
    {

        public static System.Threading.Timer exitTimer;

        public static GUI gui;

        public static NotifyIcon notifyIcon;

        public static readonly string Version = "1.8.1";

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                if (File.Exists(Log.logFile))
                {
                    File.Delete(Log.logFile);
                }

                Data.Load();

                gui = new GUI();

                InitTrayIcon();
                CheckUpdate();

                if (!Data.ShowUI || gui.ShowDialog() == DialogResult.OK)
                {
                    StartClient();
                    InitExitTimer();
                    Application.Run();
                }
                else
                {
                    notifyIcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message + " For more info refer to .log file.");
                Log.Write(ex);
                if (notifyIcon != null)
                {
                    notifyIcon.Dispose();
                }
            }
        }

        public static void InitExitTimer()
        {
            exitTimer = new System.Threading.Timer(Exit, null, 30000, 0);
        }

        public static void StopExitTimer()
        {
            if (exitTimer != null)
            {
                exitTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        public static void Exit(object obj)
        {
            Exit();
        }

        public static void Exit()
        {
            notifyIcon.Dispose();
            Application.Exit();
            Environment.Exit(0);
        }

        public static void InitTrayIcon()
        {
            var exit = new MenuItem();
            exit.Text = "Exit";
            exit.Click += new System.EventHandler(exitItem_Click);

            var options = new MenuItem();
            options.Text = "Options";
            options.Click += new System.EventHandler(optionsItem_Click);

            var start = new MenuItem();
            start.Text = "Start League";
            start.Click += new System.EventHandler(startItem_Click);

            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.AddRange(new MenuItem[] { start, options, exit });

            var components = new System.ComponentModel.Container();

            notifyIcon = new NotifyIcon(components);
            notifyIcon.Icon = Icon.FromHandle(Properties.Resources.icon.GetHicon());
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.Text = "LoL AutoLogin";
            notifyIcon.Visible = true;

            notifyIcon.BalloonTipTitle = notifyIcon.Text;
            notifyIcon.BalloonTipText = ""
                + "Newer version is available"
                + Environment.NewLine
                + "Click there to go to download page on github.com";
            notifyIcon.BalloonTipClicked += (sender, e) =>
            {
                System.Diagnostics.Process.Start(UpdateChecker.DownloadUrl);
            };
        }

        private static void CheckUpdate()
        {
            var checkUpdate = new Thread(() =>
            {
                if (UpdateChecker.Check(Version))
                {
                    notifyIcon.ShowBalloonTip(5000);
                }
            });

            checkUpdate.Start();
        }

        private static void exitItem_Click(object Sender, EventArgs e)
        {
            Exit();
        }

        private static void optionsItem_Click(object Sender, EventArgs e)
        {
            StopExitTimer();

            if (gui.Modal)
            {
                gui.WindowState = FormWindowState.Normal;
            }
            else
            {
                if (gui.ShowDialog() == DialogResult.OK)
                {
                    StartClient();
                }

                InitExitTimer();
            }
        }

        private static void startItem_Click(object Sender, EventArgs e)
        {
            gui.DialogResult = DialogResult.OK;
        }

        private static void StartClient()
        {
            Log.Write("Initialising game client.");
            LeagueClient client = new LeagueClient();

            Log.Write("Checking if game client is running.");
            if (client.IsRunning)
            {
                Log.Write("Game client is running. Stopping it.");
                client.Stop();
            }

            Log.Write("Game client not running. Starting it.");
            client.Start();

            Log.Write("Checking if game client is ready for login.");
            if (client.Ready)
            {
                Log.Write("Game client is ready for login.");
                client.Login();
                Log.Write("Login is seems to be successful");
            }

            GC.Collect();
        }

        private static void Error(string message)
        {
            MessageBox.Show(message, "AutoLogin", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
