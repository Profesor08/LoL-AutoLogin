using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace LoL_AutoLogin
{
    public partial class GUI : Form
    {

        private FontFamily BeaufortforLOL_Regular;
        private FontFamily BeaufortforLOL_Bold;
        private FontFamily BeaufortforLOL_Italic;
        private FontFamily Spiegel_Regular;
        private FontFamily Spiegel_Italic;
        private FontFamily Friz_Quadrata;

        private ImageTextBox login;
        private ImageTextBox password;

        private AntiAliasedLabel loginLabel;
        private AntiAliasedLabel passwordLabel;
        private AntiAliasedLabel showWindowLabel;
        private AntiAliasedLabel gameFolder;
        private AntiAliasedLabel titleLabel;

        private About about;

        public GUI()
        {
            InitializeComponent();
            InitializeFonts();
            InitializeCustomComponent();

            Icon = Icon.FromHandle(Properties.Resources.icon.GetHicon());

            about = new About(BeaufortforLOL_Regular);

            closeButton.ForeColor = Color.FromArgb(160, 155, 140);
            hideButton.ForeColor = Color.FromArgb(160, 155, 140);

            InitPlayButtonAnimations();

            if (Data.ShowUI)
            {
                showWindowChechBox.Image = Properties.Resources.check_checked;
            }

            gameFolder.Text = Data.GamePath;
            login.Text = Data.Login;
            password.Text = Data.Password;
        }

        private void InitializeFonts()
        {
            BeaufortforLOL_Regular = LoadFont(Properties.Resources.BeaufortforLOL_Regular);
            BeaufortforLOL_Bold = LoadFont(Properties.Resources.BeaufortforLOL_Bold);
            BeaufortforLOL_Italic = LoadFont(Properties.Resources.BeaufortforLOL_Italic);
            Spiegel_Regular = LoadFont(Properties.Resources.Spiegel_Regular);
            Spiegel_Italic = LoadFont(Properties.Resources.Spiegel_RegularItalic);
            Friz_Quadrata = LoadFont(Properties.Resources.Friz_Quadrata);
        }

        private void InitializeCustomComponent()
        {
            login = new ImageTextBox();
            password = new ImageTextBox();
            loginLabel = new AntiAliasedLabel();
            passwordLabel = new AntiAliasedLabel();
            showWindowLabel = new AntiAliasedLabel();
            titleLabel = new AntiAliasedLabel();
            gameFolder = new AntiAliasedLabel();


            login.Location = new Point(74, 228);
            login.BorderStyle = BorderStyle.None;
            login.Size = new Size(238, 37);
            login.ForeColor = Color.Red;
            login.Multiline = true;
            login.Font = new Font(BeaufortforLOL_Bold, 17, GraphicsUnit.Pixel);
            login.BackgroundImage = Properties.Resources.textBox;
            login.BackgroundImageActive = Properties.Resources.textBox_hover;
            login.Enter += new EventHandler(login_Enter);
            login.Leave += new EventHandler(login_Leave);

            password.Location = new Point(318, 228);
            password.BorderStyle = BorderStyle.None;
            password.Size = new Size(238, 37);
            password.ForeColor = Color.Red;
            password.Multiline = true;
            password.Font = new Font(BeaufortforLOL_Bold, 17, GraphicsUnit.Pixel);
            password.BackgroundImage = Properties.Resources.textBox;
            password.BackgroundImageActive = Properties.Resources.textBox_hover;
            password.PasswordChar = '*';
            password.Enter += new EventHandler(password_Enter);
            password.Leave += new EventHandler(password_Leave);



            loginLabel.Location = new Point(71, 209);
            loginLabel.ForeColor = Color.FromArgb(160, 155, 140);
            loginLabel.BackColor = Color.Transparent;
            loginLabel.Font = new Font(BeaufortforLOL_Bold, 15, GraphicsUnit.Pixel);
            loginLabel.Text = "Login";

            passwordLabel.Location = new Point(318, 209);
            passwordLabel.ForeColor = Color.FromArgb(160, 155, 140);
            passwordLabel.BackColor = Color.Transparent;
            passwordLabel.Font = new Font(BeaufortforLOL_Bold, 15, GraphicsUnit.Pixel);
            passwordLabel.Text = "Password";

            showWindowLabel.Location = new Point(90, 270);
            showWindowLabel.ForeColor = Color.FromArgb(160, 155, 140);
            showWindowLabel.BackColor = Color.Transparent;
            showWindowLabel.Font = new Font(BeaufortforLOL_Bold, 15, GraphicsUnit.Pixel);
            showWindowLabel.Text = "Show window on start";

            titleLabel.Location = new Point(234, 20);
            titleLabel.ForeColor = Color.DarkGoldenrod;
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Font = new Font(BeaufortforLOL_Bold, 30, FontStyle.Bold, GraphicsUnit.Pixel);
            titleLabel.Text = "AutoLogin";
            titleLabel.MouseDown += new MouseEventHandler(GUI_MouseDown);
            titleLabel.MouseUp += new MouseEventHandler(GUI_MouseUp);
            titleLabel.MouseMove += new MouseEventHandler(GUI_MouseMove);

            gameFolder.Location = new Point(74, 174);
            gameFolder.ForeColor = Color.FromArgb(160, 155, 140);
            gameFolder.BackColor = Color.Transparent;
            gameFolder.BorderStyle = BorderStyle.FixedSingle;
            gameFolder.AutoSize = false;
            gameFolder.Width = 455;
            gameFolder.Height = 22;
            gameFolder.TextAlign = ContentAlignment.MiddleLeft;
            gameFolder.Click += new EventHandler(selectFolderButton_Click);

            Controls.Add(login);
            Controls.Add(password);
            Controls.Add(loginLabel);
            Controls.Add(passwordLabel);
            Controls.Add(showWindowLabel);
            Controls.Add(gameFolder);
            Controls.Add(titleLabel);
        }

        private void InitPlayButtonAnimations()
        {
            var fadeIn = new Timer();
            var fadeOut = new Timer();
            int interval = 10;
            int duration = 150;
            var animationStarted = DateTime.Now;
            var animationDuration = TimeSpan.FromMilliseconds(duration);

            fadeIn.Interval = interval;
            fadeOut.Interval = interval;

            playButton.MouseEnter += (_, args) =>
            {
                fadeOut.Stop();
                fadeIn.Start();
                animationStarted = DateTime.Now;
            };

            playButton.MouseLeave += (_, args) =>
            {
                fadeIn.Stop();
                fadeOut.Start();
                animationStarted = DateTime.Now;
            };

            fadeIn.Tick += (_, args) =>
            {
                float percentComplete = (DateTime.Now - animationStarted).Ticks
                    / (float)animationDuration.Ticks;

                if (percentComplete >= 1)
                {
                    fadeIn.Stop();
                }
                else
                {
                    playButton.Image = SetImageOpacity(Properties.Resources.play_hover, percentComplete);
                }
            };

            fadeOut.Tick += (_, args) =>
            {
                float percentComplete = (DateTime.Now - animationStarted).Ticks
                    / (float)animationDuration.Ticks;

                if (percentComplete >= 1)
                {
                    fadeIn.Stop();
                }
                else
                {
                    playButton.Image = SetImageOpacity(Properties.Resources.play_hover, 1 - percentComplete);
                }
            };

        }

        private void login_Enter(object sender, EventArgs e)
        {
            login.TempText = login.Text;
            login.Text = "";
            Data.Changed = true;
        }

        private void login_Leave(object sender, EventArgs e)
        {
            if (login.Text.Length == 0)
            {
                login.Text = login.TempText;
                Data.Changed = false;
            }
        }

        private void password_Enter(object sender, EventArgs e)
        {
            password.TempText = password.Text;
            password.Text = "";
            Data.Changed = true;
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (password.Text.Length == 0)
            {
                password.Text = password.TempText;
                Data.Changed = false;
            }
        }

        private FontFamily LoadFont(byte[] font)
        {
            var pfc = new PrivateFontCollection();

            IntPtr pointer = Marshal.AllocCoTaskMem(font.Length);
            Marshal.Copy(font, 0, pointer, font.Length);
            pfc.AddMemoryFont(pointer, font.Length);
            uint cFonts = 0;
            NativeMethods.AddFontMemResourceEx(pointer, (uint)font.Length, IntPtr.Zero, ref cFonts);
            Marshal.FreeCoTaskMem(pointer);

            return pfc.Families[0];
        }

        public static bool SetStyle(Control c, ControlStyles Style, bool value)
        {
            bool retval = false;
            Type typeTB = typeof(Control);
            System.Reflection.MethodInfo misSetStyle = typeTB.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (misSetStyle != null && c != null) { misSetStyle.Invoke(c, new object[] { Style, value }); retval = true; }
            return retval;
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    Data.GamePath = fbd.SelectedPath.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
                    gameFolder.Text = Data.GamePath;
                }
                else
                {
                    MessageBox.Show("Game folder not selected!", "AutoLogin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            Data.GamePath = gameFolder.Text;
            Data.Login = login.Text;
            Data.Password = password.Text;
            Data.Save();
        }

        public Bitmap SetImageOpacity(Bitmap image, float opacity)
        {
            // Initialize the color matrix.
            // Note the value 0.8 in row 4, column 4.
            float[][] matrixItems ={
               new float[] {1, 0, 0, 0, 0},
               new float[] {0, 1, 0, 0, 0},
               new float[] {0, 0, 1, 0, 0},
               new float[] {0, 0, 0, opacity, 0},
               new float[] {0, 0, 0, 0, 1}};
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            // Create an ImageAttributes object and set its color matrix.
            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(
               colorMatrix,
               ColorMatrixFlag.Default,
               ColorAdjustType.Bitmap);

            var transparentImage = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(transparentImage))
            {
                g.DrawImage(
                   image,
                   new Rectangle(0, 0, image.Width, image.Height),  // destination rectangle
                   0.0f,                          // source rectangle x 
                   0.0f,                          // source rectangle y
                   image.Width,                        // source rectangle width
                   image.Height,                       // source rectangle height
                   GraphicsUnit.Pixel,
                   imageAtt);
            }

            return transparentImage;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveData();
        }

        private void playButton_Paint(object sender, PaintEventArgs e)
        {
            var brush = new SolidBrush(Color.FromArgb(240, 230, 210));
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("Start", new Font(BeaufortforLOL_Bold, 25, GraphicsUnit.Pixel), brush, new Point(80, 8));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (Data.Changed)
            {
                DialogResult dialogResult = MessageBox.Show("Save changes?", "LoL AutoLogin", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    SaveData();
                }
            }

            Close();
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.FromArgb(240, 230, 210);
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.FromArgb(160, 155, 140);
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void hideButton_MouseEnter(object sender, EventArgs e)
        {
            hideButton.ForeColor = Color.FromArgb(240, 230, 210);
        }

        private void hideButton_MouseLeave(object sender, EventArgs e)
        {
            hideButton.ForeColor = Color.FromArgb(160, 155, 140);
        }

        bool mouseDown = false;
        Point mousePosition;

        private void GUI_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;

            if (sender is AntiAliasedLabel)
            {
                var label = (AntiAliasedLabel)sender;
                mousePosition = new Point(label.Location.X + e.X, label.Location.Y + e.Y);
            }
            else if(sender is GUI)
            {
                mousePosition = new Point(e.X, e.Y);
            }
        }

        private void GUI_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void GUI_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point(Cursor.Position.X - mousePosition.X, Cursor.Position.Y - mousePosition.Y);
            }
        }

        private void showWindowChechBox_MouseDown(object sender, MouseEventArgs e)
        {
            Data.ShowUI = !Data.ShowUI;

            if (Data.ShowUI)
            {
                showWindowChechBox.Image = Properties.Resources.check_checked_hover;
            }
            else
            {
                showWindowChechBox.Image = Properties.Resources.check_hover;
            }
        }

        private void showWindow_MouseEnter(object sender, EventArgs e)
        {
            if (Data.ShowUI)
            {
                showWindowChechBox.Image = Properties.Resources.check_checked_hover;
            }
            else
            {
                showWindowChechBox.Image = Properties.Resources.check_hover;
            }
        }

        private void showWindow_MouseLeave(object sender, EventArgs e)
        {
            if (Data.ShowUI)
            {
                showWindowChechBox.Image = Properties.Resources.check_checked;
            }
            else
            {
                showWindowChechBox.Image = Properties.Resources.check;
            }
        }

        private void infoButton_MouseEnter(object sender, EventArgs e)
        {
            infoButton.Image = Properties.Resources.info_hover;
        }

        private void infoButton_MouseLeave(object sender, EventArgs e)
        {
            infoButton.Image = Properties.Resources.info;
        }

        private void GUI_Click(object sender, EventArgs e)
        {
            loginLabel.Focus();
        }

        private void selectFolder_MouseEnter(object sender, EventArgs e)
        {
            selectFolder.BackgroundImage = Properties.Resources.options_hover;
        }

        private void selectFolder_MouseLeave(object sender, EventArgs e)
        {
            selectFolder.BackgroundImage = Properties.Resources.options;
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            about.ShowDialog();
        }
    }

}
