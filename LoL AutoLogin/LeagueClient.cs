using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using AForge.Imaging;
using WindowsInput;
using WindowsInput.Native;
using System.Collections.Generic;

namespace LoL_AutoLogin
{


    class LeagueClient
    {

        /// <summary>
        /// Default login form template for comparation in different sizes 
        /// </summary>
        private Dictionary<int, Bitmap> templates;

        public LeagueClient()
        {
            templates = new Dictionary<int, Bitmap>()
            {
                { 179, Properties.Resources.small },
                { 224, Properties.Resources.medium },
                { 280, Properties.Resources.big }
            };
        }

        /// <summary>
        /// Check if LeagueClient is running
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return Process.GetProcessesByName(Data.Client).FirstOrDefault() != null;
            }
        }

        /// <summary>
        /// Start LeagueClient
        /// </summary>
        public void Start()
        {
            var process = new Process();
            process.StartInfo.FileName = Data.GamePath + Data.ClientFile;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
        }

        /// <summary>
        /// Closing LeagueClient and it LeagueClientUx
        /// </summary>
        public void Stop()
        {
            var clientUx = Process.GetProcessesByName(Data.ClientUx).FirstOrDefault();

            if (clientUx != null)
            {
                clientUx.Kill();
            }

            var client = Process.GetProcessesByName(Data.Client).FirstOrDefault();

            if (client != null)
            {
                client.Kill();
            }
        }

        /// <summary>
        /// Waiting for LeagueClientUx is loaded and it is ready for entering login and password
        /// </summary>
        public bool Ready
        {
            get
            {
                return LoginFormReady(ClientUxProcess);
            }
        }

        /// <summary>
        /// Getting LeagueClientUx Process
        /// </summary>
        private Process ClientUxProcess
        {
            get
            {
                Process clientUx;

                while (true)
                {
                    clientUx = Process.GetProcessesByName(Data.ClientUx).FirstOrDefault();

                    if (clientUx != null && (int)clientUx.MainWindowHandle > 0)
                    {
                        return clientUx;
                    }
                }
            }
        }

        /// <summary>
        /// Waiting until login form is ready
        /// </summary>
        /// <param name="clientUx"></param>
        /// <returns></returns>
        private bool LoginFormReady(Process clientUx)
        {
            Log.Write("Checking if LeagueClientUx load finished");

            while (true)
            {
                Thread.Sleep(200);
                Log.Write("Focus on LeagueClientUx");
                FocusProcess(clientUx);

                Log.Write("Capturing Screenshot from LeagueClientUx");
                var clientBitmap = CaptureClientUxScreenshot(clientUx);

                var x1 = (int)(clientBitmap.Width * 0.825);
                var y1 = 0;
                var x2 = (int)(clientBitmap.Width * 0.175);
                var y2 = clientBitmap.Height;

                if (!templates.ContainsKey(x2))
                {
                    // If LeagueClientUx window is not displayed
                    continue;
                }

                Log.Write("Croping screenshot");
                var cropped = CropImage(clientBitmap, new Rectangle(x1, y1, x2, y2));

                Log.Write("Comparing images");
                var equal = CompareImages(cropped, templates[x2], 0.95, 0.99f);

                clientBitmap.Dispose();
                cropped.Dispose();

                if (equal)
                {
                    Log.Write("Images are equal enouhg");
                    Log.Write("Game client is ready for password entering");
                    return true;
                }
            }
        }

        /// <summary>
        /// Set focus to Process and moving it window to Foreground
        /// </summary>
        /// <param name="process"></param>
        private void FocusProcess(Process process)
        {
            while (!ApplicationIsActivated(process))
            {
                Thread.Sleep(10);
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                NativeMethods.ShowWindow(process.MainWindowHandle, NativeMethods.ShowWindowEnum.Restore);
            }
        }

        /// <summary>
        /// Check if Process is focused and it window in Foreground
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static bool ApplicationIsActivated(Process process)
        {
            var activatedHandle = NativeMethods.GetForegroundWindow();

            if (activatedHandle == IntPtr.Zero)
            {
                // No window is currently activated
                return false;
            }

            int activeProcId;
            NativeMethods.GetWindowThreadProcessId(activatedHandle, out activeProcId);

            // Compare process id with current active process, if the are the same 
            // it's mean process if currently is focused
            return activeProcId == process.Id;
        }

        /// <summary>
        /// Capturing LeagueClientUx window ScreenShot
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public Bitmap CaptureClientUxScreenshot(Process process)
        {
            // Get Windows dimensions
            var rect = NativeMethods.GetWindowRect(process.MainWindowHandle);

            // Calculation window size by extracting from right corner position on positions of left corner
            // becouse position of corners is calculateted from 0x0 of screen
            var width = rect.right - rect.left;
            var height = rect.bottom - rect.top;
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                // Copy all pixels from screen to image 
                graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

                return bmp;
            }
        }

        /// <summary>
        /// Crop LeagueClientUx window screenshot to get only login form image
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private Bitmap CropImage(Bitmap bitmap, Rectangle rect)
        {
            Bitmap cropped = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(cropped))
            {
                g.DrawImage(bitmap, -rect.X, -rect.Y);
            }

            return cropped;
        }

        /// <summary>
        /// Compare image with template image according to compare level and similarity threshold
        /// </summary>
        /// <param name="image"></param>
        /// <param name="targetImage"></param>
        /// <param name="compareLevel"></param>
        /// <param name="similarityThreshold"></param>
        /// <returns></returns>
        public bool CompareImages(Bitmap image, Bitmap targetImage, double compareLevel, float similarityThreshold)
        {

            var newBitmap1 = ChangePixelFormat(new Bitmap(image), PixelFormat.Format24bppRgb);
            var newBitmap2 = ChangePixelFormat(new Bitmap(targetImage), PixelFormat.Format24bppRgb);

            // Setup the AForge library
            var tm = new ExhaustiveTemplateMatching(similarityThreshold);

            // Process the images
            var results = tm.ProcessImage(newBitmap1, newBitmap2);

            image.Dispose();
            newBitmap1.Dispose();
            newBitmap2.Dispose();

            // Compare the results, 0 indicates no match so return false
            if (results.Length <= 0)
            {
                return false;
            }

            // Return true if similarity score is equal or greater than the comparison level
            var match = results[0].Similarity >= compareLevel;

            return match;
        }

        /// <summary>
        /// Changind pixel format of image into indicated format
        /// </summary>
        /// <param name="inputImage"></param>
        /// <param name="newFormat"></param>
        /// <returns></returns>
        private static Bitmap ChangePixelFormat(Bitmap inputImage, PixelFormat newFormat)
        {
            return inputImage.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), newFormat);
        }

        /// <summary>
        /// Entering login and password to login form and submiting it
        /// </summary>
        public void Login()
        {
            Log.Write("Get LeagueClientUx handle");
            Process clientUx = ClientUxProcess;

            if (clientUx != null)
            {
                Log.Write("Focus on LeagueClientUx");
                FocusProcess(clientUx);

                Log.Write("Focus on Login field");
                FocusLogin(clientUx);

                Log.Write("Simulation keyboard input");
                var sim = new InputSimulator();

                //FocusProcess(clientUx);
                Thread.Sleep(100);
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
                Thread.Sleep(100);
                sim.Keyboard.TextEntry(Data.Login);
                Thread.Sleep(200);
                sim.Keyboard.KeyPress(VirtualKeyCode.TAB);
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
                sim.Keyboard.TextEntry(Data.Password);
                Thread.Sleep(500);
                FocusProcess(clientUx);
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

                sim = null;
            }
        }

        /// <summary>
        /// Focusing on login field by clicking on it by mouse and restoring cursor posiion back
        /// </summary>
        /// <param name="clientUx"></param>
        private void FocusLogin(Process clientUx)
        {
            var defaultCursor = new Point()
            {
                X = Cursor.Position.X,
                Y = Cursor.Position.Y
            };

            var sim = new InputSimulator();

            Cursor.Position = GetRequiredCursorPosition(clientUx);
            sim.Mouse.LeftButtonClick();
            Cursor.Position = defaultCursor;
        }

        /// <summary>
        /// Get position of cursor for clicking on login field
        /// </summary>
        /// <param name="clientUx"></param>
        /// <returns></returns>
        public Point GetRequiredCursorPosition(Process clientUx)
        {
            var rect = new NativeMethods.Rect();

            int x, y;

            NativeMethods.GetWindowRect(clientUx.MainWindowHandle, ref rect);

            switch (rect.right - rect.left)
            {
                case 1600:
                    x = rect.right - 200;
                    y = rect.top + 240;
                    break;
                case 1280:
                    x = rect.right - 100;
                    y = rect.top + 200;
                    break;
                case 1024:
                    x = rect.right - 100;
                    y = rect.top + 150;
                    break;
                default:
                    x = rect.right - 200;
                    y = rect.top + 240;
                    break;
            }

            return new Point(x, y);
        }

    }
}
