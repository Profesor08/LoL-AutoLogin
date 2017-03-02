using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace LoL_AutoLogin
{

    public class LeagueClient
    {
        private string leagueClientPath;
        private string leageClientFile;
        private string leagueClientUx;

        private Process process;

        private int MinPixelsMatched = int.MaxValue;
        private int MaxPixelsMatched = int.MinValue;

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

        public void Login(string password)
        {
            Process[] localByName = Process.GetProcessesByName(leagueClientUx);
            Thread.Sleep(1000);
            foreach (var p in localByName)
            {
                SetForegroundWindow(p.MainWindowHandle);
                ShowWindowAsync(p.MainWindowHandle, 0);
                Thread.Sleep(1000);
                // clearing input field
                /*SendKeys.SendWait("\t");
                SendKeys.SendWait("\t");
                SendKeys.SendWait("\t");
                SendKeys.SendWait("\t");
                SendKeys.SendWait("^{a}");
                SendKeys.SendWait(login);
                SendKeys.SendWait("\t");*/
                SendKeys.SendWait("^{a}");
                SendKeys.SendWait(password);
                SendKeys.SendWait("{ENTER}");
            }
        }

        public string GetClientFile()
        {
            return leageClientFile;
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
                    Thread.Sleep(500);

                    var clientBitmap = CaptureWindow(process);

                    Bitmap cropped = CropImage(clientBitmap, new Rectangle((int)(clientBitmap.Width * 0.825), 0, (int)(clientBitmap.Width * 0.175), clientBitmap.Height));

                    found = CompareImage(clientBitmap, cropped, 10, 0.85);
                }

                return true;
            }

            return false;
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

        public bool CompareImage(Bitmap a, Bitmap b, double brightnessTolerance = 10, double matchTolerance = 0.85)
        {
            List<short> hashA = GetHash(a);
            List<short> hashB = GetHash(b);

            // get amount of similar pixels
            int similar = hashA.Zip(hashB, (i, j) => Math.Abs(i - j) < brightnessTolerance).Count(sim => sim);
            
            if (similar > MaxPixelsMatched)
                MaxPixelsMatched = similar;

            if (similar < MinPixelsMatched)
                MinPixelsMatched = similar;

            // return true if the amount of similar pixels is over tolerance, false if not
            return similar > hashA.Count * matchTolerance;
        }

        public List<short> GetHash(Bitmap source)
        {
            // create list
            List<short> list = new List<short>();

            // resize bitmap
            Bitmap resized = new Bitmap(source, new Size(900, 900));

            // iterate through every pixel
            for (int i = 0; i < resized.Width; i++)
            {
                for (int j = 0; j < resized.Height; j++)
                {
                    // get brightness
                    float b = resized.GetPixel(i, j).GetBrightness();

                    // convert brightness 0-1 to 0-255 value
                    list.Add((short)(b * 255));
                }
            }

            // return brightness list
            return list;
        }
    }
}
