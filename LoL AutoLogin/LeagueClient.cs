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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);
        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        private void FocusProcess(Process process)
        {
            ShowWindow(process.Handle, ShowWindowEnum.Restore);
            SetForegroundWindow(process.MainWindowHandle);
        }

        public void Login(string password)
        {
            Process clientUx = Process.GetProcessesByName(leagueClientUx).FirstOrDefault();

            if (clientUx != null)
            {
                FocusProcess(Process.GetCurrentProcess());
                Thread.Sleep(500);
                FocusProcess(clientUx);
                Thread.Sleep(100);
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
            Process[] localByName;

            while(true)
            {
                localByName = Process.GetProcessesByName(leagueClientUx);

                if (localByName.Length > 0)
                {
                    if ((int)localByName[0].MainWindowHandle > 0)
                    {
                        break;
                    }
                }
            }

            foreach (var process in localByName)
            {
                bool found = false;

                while (!found)
                {
                    SetForegroundWindow(process.MainWindowHandle);
                    ShowWindowAsync(process.MainWindowHandle, 1);

                    Thread.Sleep(200);

                    var clientBitmap = CaptureWindow(process);

                    var x1 = (int)(clientBitmap.Width * 0.825);
                    var y1 = 0;
                    var x2 = (int)(clientBitmap.Width * 0.175);
                    var y2 = clientBitmap.Height;

                    Bitmap cropped = CropImage(clientBitmap, new Rectangle(x1, y1, x2, y2));

                    found = CompareImages(cropped, Properties.Resources.login_form, 0.95, 0.99f);

                    //found = CompareImage(Properties.Resources.login_form, cropped, 10, 0.85);

                    //clientBitmap.Save("C:\\Portable Files\\Tests\\a.png", ImageFormat.Png);
                    //cropped.Save("C:\\Portable Files\\Tests\\b.png", ImageFormat.Png);
                    //Properties.Resources.login_form.Save("C:\\Portable Files\\Tests\\c.png", ImageFormat.Png);
                }

                return true;
            }

            return false;
        }

        private static Bitmap ChangePixelFormat(Bitmap inputImage, System.Drawing.Imaging.PixelFormat newFormat)
        {
            return (inputImage.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), newFormat));
        }

        public bool CompareImages(Bitmap image, Bitmap targetImage, double compareLevel, float similarityThreshold)
        {

            var newBitmap1 = ChangePixelFormat(new Bitmap(image), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var newBitmap2 = ChangePixelFormat(new Bitmap(targetImage), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

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

        public Bitmap CaptureWindow(Process process)
        {
            var rect = new User32.Rect();

            User32.GetWindowRect(process.MainWindowHandle, ref rect);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(bmp);

            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

            //bmp.Save("c:\\test.png", ImageFormat.Png);

            return bmp;
        }

        private Bitmap CropImage(Bitmap bitmap, Rectangle rect)
        {
            // create bitmap
            Bitmap cropped = new Bitmap(rect.Width, rect.Height);

            // draw image on cropped bitmap
            using (Graphics g = Graphics.FromImage(cropped))
                g.DrawImage(bitmap, -rect.X, -rect.Y);

            // return cropped bitmap
            return cropped;
        }

    }
}
