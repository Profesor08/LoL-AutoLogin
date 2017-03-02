using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using AForge.Imaging;


namespace LoL_AutoLogin
{

    public class LeagueClient
    {
        private string leagueClientPath;
        private string leageClientFile;
        private string leagueClientUx;

        private Process process;

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

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
                this.process = process;
                return true;
            }

            return false;
        }

        public Process Start()
        {
            process = new Process();
            process.StartInfo.FileName = leagueClientPath + leageClientFile;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();

            return process;
        }

        public void Stop()
        {
            foreach(var p in Process.GetProcessesByName(leagueClientUx))
            {
                p.Kill();
            }

            process.Kill();
        }

        private void FocusProcess(Process process)
        {
            SetForegroundWindow(process.MainWindowHandle);
            ShowWindow(process.MainWindowHandle, ShowWindowEnum.Restore);
        }

        public static bool ApplicationIsActivated(Process process)
        {
            var activatedHandle = GetForegroundWindow();

            if (activatedHandle == IntPtr.Zero)
            {
                // No window is currently activated
                return false;       
            }

            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == process.Id;
        }

        public void Login(string password)
        {
            Log.Write("Get clientUx handle");
            Process clientUx = Process.GetProcessesByName(leagueClientUx).FirstOrDefault();

            if (clientUx != null)
            {
                Log.Write("Focus on console");
                while(!ApplicationIsActivated(Process.GetCurrentProcess()))
                {
                    FocusProcess(Process.GetCurrentProcess());
                }
                
                Thread.Sleep(500);

                Log.Write("Focus back to game client");

                while (!ApplicationIsActivated(clientUx))
                {
                    FocusProcess(clientUx);
                }

                Thread.Sleep(500);

                Log.Write("Entering password");
                // clearing input field
                SendKeys.SendWait("^{a}");
                FocusProcess(clientUx);
                SendKeys.SendWait(password);
                FocusProcess(clientUx);
                SendKeys.SendWait("{ENTER}");
            }
        }

        public bool Ready()
        {
            return LoginFormReady(GetClientUxProcess());
        }

        public bool LoginFormReady(Process clientUx)
        {
            Log.Write("Checking if ClientUx load finished");
            while (true)
            {
                Log.Write("Focus on ClientUx");
                FocusProcess(clientUx);

                Thread.Sleep(200);
                Log.Write("Capturing Screenshot from ClientUx");
                var clientBitmap = CaptureClientUxScreenshot(clientUx);

                var x1 = (int)(clientBitmap.Width * 0.825);
                var y1 = 0;
                var x2 = (int)(clientBitmap.Width * 0.175);
                var y2 = clientBitmap.Height;

                Log.Write("Croping screenshot");
                var cropped = CropImage(clientBitmap, new Rectangle(x1, y1, x2, y2));
                Bitmap template;
                
                switch (cropped.Width)
                {
                    case 179: template = Properties.Resources.small; break;
                    case 224: template = Properties.Resources.medium; break;
                    case 280: template = Properties.Resources.big; break;
                    default: template = Properties.Resources.big; break;
                }

                //cropped.Save("C:\\Portable Files\\Tests\\a.png", ImageFormat.Png);
                //template.Save("C:\\Portable Files\\Tests\\b.png", ImageFormat.Png);

                Log.Write("Comparing images");
                if (CompareImages(cropped, template, 0.95, 0.99f))
                {
                    Log.Write("Images are equal enouhg");
                    Log.Write("Game client is ready for password entering");
                    return true;
                }
            }
        }

        public static Bitmap ResizeImage(Bitmap image, Size size)
        {
            return new Bitmap(image, size);
        }

        public Process GetClientUxProcess()
        {
            Log.Write("Waiting for ClientUx");
            while (true)
            {
                var clientUx = Process.GetProcessesByName(leagueClientUx).FirstOrDefault();

                if (clientUx != null && (int)clientUx.MainWindowHandle > 0)
                {
                    Log.Write("ClientUx found");
                    return clientUx;
                }
            }
        }

        private static Bitmap ChangePixelFormat(Bitmap inputImage, PixelFormat newFormat)
        {
            return inputImage.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), newFormat);
        }

        public bool CompareImages(Bitmap image, Bitmap targetImage, double compareLevel, float similarityThreshold)
        {

            var newBitmap1 = ChangePixelFormat(new Bitmap(image), PixelFormat.Format24bppRgb);
            var newBitmap2 = ChangePixelFormat(new Bitmap(targetImage), PixelFormat.Format24bppRgb);

            // Setup the AForge library
            var tm = new ExhaustiveTemplateMatching(similarityThreshold);

            // Process the images
            var results = tm.ProcessImage(newBitmap1, newBitmap2);
            
            // Compare the results, 0 indicates no match so return false
            if (results.Length <= 0)
            {
                return false;
            }

            // Return true if similarity score is equal or greater than the comparison level
            var match = results[0].Similarity >= compareLevel;

            return match;
        }

        public Bitmap CaptureClientUxScreenshot(Process process)
        {
            var rect = new User32.Rect();

            User32.GetWindowRect(process.MainWindowHandle, ref rect);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(bmp);

            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

            return bmp;
        }

        private Bitmap CropImage(Bitmap bitmap, Rectangle rect)
        {
            Bitmap cropped = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(cropped))
            {
                g.DrawImage(bitmap, -rect.X, -rect.Y);
            }

            return cropped;
        }

    }
}
