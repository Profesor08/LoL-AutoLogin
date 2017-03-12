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

        FontFamily BeaufortforLOL_Regular;
        FontFamily BeaufortforLOL_Bold;
        FontFamily BeaufortforLOL_Italic;
        FontFamily Spiegel_Regular;
        FontFamily Spiegel_Italic;
        FontFamily Friz_Quadrata;

        Font leagueFont_25;
        Font inputFont;
        Font labelFont;

        ImageTextBox login;
        ImageTextBox password;

        AntiAliasedLabel loginLabel;
        AntiAliasedLabel passwordLabel;
        AntiAliasedLabel showWindowLabel;
        AntiAliasedLabel gameFolder;
        AntiAliasedLabel titleLabel;

        About about;

        public GUI()
        {
            InitializeComponent();

            Icon = Icon.FromHandle(Properties.Resources.icon.GetHicon());

            BeaufortforLOL_Regular = LoadFont(Properties.Resources.BeaufortforLOL_Regular);
            BeaufortforLOL_Bold = LoadFont(Properties.Resources.BeaufortforLOL_Bold);
            BeaufortforLOL_Italic = LoadFont(Properties.Resources.BeaufortforLOL_Italic);
            Spiegel_Regular = LoadFont(Properties.Resources.Spiegel_Regular);
            Spiegel_Italic = LoadFont(Properties.Resources.Spiegel_RegularItalic);
            Friz_Quadrata = LoadFont(Properties.Resources.Friz_Quadrata);

            about = new About(BeaufortforLOL_Regular);

            leagueFont_25 = new Font(BeaufortforLOL_Bold, 25, GraphicsUnit.Pixel);
            inputFont = new Font(BeaufortforLOL_Bold, 17, GraphicsUnit.Pixel);

   
            labelFont = new Font(BeaufortforLOL_Bold, 15, GraphicsUnit.Pixel);

            login = new ImageTextBox();
            login.Location = new Point(74, 228);
            login.BorderStyle = BorderStyle.None;
            login.Size = new Size(238, 37);
            login.ForeColor = Color.Red;
            login.Multiline = true;
            login.Font = inputFont;
            login.BackgroundImage = Properties.Resources.textBox;
            login.BackgroundImageActive = Properties.Resources.textBox_hover;

            password = new ImageTextBox();
            password.Location = new Point(318, 228);
            password.BorderStyle = BorderStyle.None;
            password.Size = new Size(238, 37);
            password.ForeColor = Color.Red;
            password.Multiline = true;
            password.Font = inputFont;
            password.BackgroundImage = Properties.Resources.textBox;
            password.BackgroundImageActive = Properties.Resources.textBox_hover;
            password.PasswordChar = '*';

            

            loginLabel = InitAntiAliasedLabel("Login", new Point(71, 209), Color.FromArgb(160, 155, 140));
            passwordLabel = InitAntiAliasedLabel("Password", new Point(318, 209), Color.FromArgb(160, 155, 140));
            showWindowLabel = InitAntiAliasedLabel("Show window on start", new Point(90, 270), Color.FromArgb(160, 155, 140));

            titleLabel = InitAntiAliasedLabel("AutoLogin", new Point(234, 20), Color.FromArgb(160, 155, 140));
            titleLabel.Font = new Font(BeaufortforLOL_Bold, 30, FontStyle.Bold, GraphicsUnit.Pixel);
            titleLabel.ForeColor = Color.DarkGoldenrod;

            gameFolder = InitAntiAliasedLabel("", new Point(74, 174), Color.FromArgb(160, 155, 140));
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

        private FontFamily LoadFont(byte[] font)
        {
            var pfc = new PrivateFontCollection();

            IntPtr pointer = Marshal.AllocCoTaskMem(font.Length);
            Marshal.Copy(font, 0, pointer, font.Length);
            pfc.AddMemoryFont(pointer, font.Length);
            Marshal.FreeCoTaskMem(pointer);

            return pfc.Families[0];
        }

        private AntiAliasedLabel InitAntiAliasedLabel(string text, Point position, Color color)
        {
            var aaLabel = new AntiAliasedLabel();
            aaLabel.Text = text;
            aaLabel.Location = position;
            aaLabel.BackColor = Color.Transparent;
            aaLabel.ForeColor = color;
            aaLabel.Font = labelFont;

            return aaLabel;
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
            e.Graphics.DrawString("Start", leagueFont_25, brush, new Point(80, 8));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Save changes?", "LoL AutoLogin", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                SaveData();
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
            mousePosition = new Point(e.X, e.Y);
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
