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

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists(Log.logFile))
            {
                File.Delete(Log.logFile);
            }

            Data.Load();

            gui = new GUI();

            if (Data.ShowUI)
            {
                if (gui.ShowDialog() == DialogResult.OK)
                {
                    StartClient();
                }
            }
            else
            {
                StartClient();
            }

            InitTrayIcon();

            InitExitTimer();

            Application.Run();
        }

        public static void InitExitTimer()
        {
            exitTimer = new System.Threading.Timer(Exit, null, 30000, 0);
        }

        public static void StopExitTimer()
        {
            exitTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public static void Exit(object obj)
        {
            Exit();
        }

        public static void Exit()
        {
            notifyIcon.Dispose();
            Application.Exit();
        }

        public static void InitTrayIcon()
        {
            var components = new System.ComponentModel.Container();

            var contextMenu = new ContextMenu();


            var exit = new MenuItem();
            exit.Text = "Exit";
            exit.Click += new System.EventHandler(exitItem_Click);

            var options = new MenuItem();
            options.Text = "Options";
            options.Click += new System.EventHandler(optionsItem_Click);

            var start = new MenuItem();
            start.Text = "Start League";
            start.Click += new System.EventHandler(startItem_Click);

            // Initialize contextMenu1
            contextMenu.MenuItems.AddRange(new MenuItem[] { start, options, exit });

            // Create the NotifyIcon.
            notifyIcon = new NotifyIcon(components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon.Icon = Icon.FromHandle(Properties.Resources.icon.GetHicon());

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon.ContextMenu = contextMenu;

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon.Text = "LoL AutoLogin";
            notifyIcon.Visible = true;

        }

        private static void exitItem_Click(object Sender, EventArgs e)
        {
            Exit();
        }

        private static void optionsItem_Click(object Sender, EventArgs e)
        {
            StopExitTimer();

            if (gui.ShowDialog() == DialogResult.OK)
            {
                StartClient();
            }

            InitExitTimer();
        }

        private static void startItem_Click(object Sender, EventArgs e)
        {
            StartClient();
        }

        private static void StartClient()
        {
            Log.Write("Initialising game client.");
            LeagueClient client = new LeagueClient();

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
                client.Login();
                Log.Write("Password entering is seems to be successful");
            }

            Log.Write("Application exit.");
        }

    }
}
