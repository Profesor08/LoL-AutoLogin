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

namespace LoL_AutoLogin
{

    public class LeagueClient
    {

        public LeagueClient()
        {

        }

        public bool IsRunning()
        {
            foreach (var process in Process.GetProcessesByName(Data.ClientFile.Substring(0, Data.ClientFile.Length - 4)))
            {
                return true;
            }

            return false;
        }

        public Process Start()
        {
            var process = new Process();
            process.StartInfo.FileName = Data.GamePath + Data.ClientFile;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();

            return process;
        }

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

        private void FocusProcess(Process process)
        {
            while (!ApplicationIsActivated(process))
            {
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                NativeMethods.ShowWindow(process.MainWindowHandle, NativeMethods.ShowWindowEnum.Restore);
            }
        }

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

            return activeProcId == process.Id;
        }

        public void Login()
        {
            Log.Write("Get clientUx handle");
            Process clientUx = Process.GetProcessesByName(Data.ClientUx).FirstOrDefault();

            if (clientUx != null)
            {
                Log.Write("Focus on ClientUx");
                FocusProcess(clientUx);

                Log.Write("Focus on Login field");
                FocusLogin(clientUx);

                Log.Write("Simulation keyboard input");
                var sim = new InputSimulator();

                FocusProcess(clientUx);
                sim.Keyboard.TextEntry(Data.Login);
                FocusProcess(clientUx);
                sim.Keyboard.KeyPress(VirtualKeyCode.TAB);
                FocusProcess(clientUx);
                sim.Keyboard.TextEntry(Data.Password);
                Thread.Sleep(500);
                FocusProcess(clientUx);
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            }
        }

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
                var clientUx = Process.GetProcessesByName(Data.ClientUx).FirstOrDefault();

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
            var rect = new NativeMethods.Rect();

            NativeMethods.GetWindowRect(process.MainWindowHandle, ref rect);

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
