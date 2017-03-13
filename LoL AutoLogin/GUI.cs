using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
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

        ImageTextBox login;
        ImageTextBox password;

        AntiAliasedLabel loginLabel;
        AntiAliasedLabel passwordLabel;
        AntiAliasedLabel showWindowLabel;
        AntiAliasedLabel gameFolder;
        AntiAliasedLabel titleLabel;

        public GUI()
        {
            InitializeComponent();

            InitializeFonts();

            InitializeCustomConponet();

            InitPlayButtonAnimations();

            Icon = Icon.FromHandle(Properties.Resources.icon.GetHicon());

            closeButton.ForeColor = Color.FromArgb(160, 155, 140);

            hideButton.ForeColor = Color.FromArgb(160, 155, 140);

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
            BeaufortforLOL_Regular = CustomFont.Load(Properties.Resources.BeaufortforLOL_Regular);
            BeaufortforLOL_Bold = CustomFont.Load(Properties.Resources.BeaufortforLOL_Bold);
            BeaufortforLOL_Italic = CustomFont.Load(Properties.Resources.BeaufortforLOL_Italic);
            Spiegel_Regular = CustomFont.Load(Properties.Resources.Spiegel_Regular);
            Spiegel_Italic = CustomFont.Load(Properties.Resources.Spiegel_RegularItalic);
            Friz_Quadrata = CustomFont.Load(Properties.Resources.Friz_Quadrata);
        }

        private void InitializeCustomConponet()
        {
            login = new ImageTextBox();
            password = new ImageTextBox();
            loginLabel = new AntiAliasedLabel();
            titleLabel = new AntiAliasedLabel();
            gameFolder = new AntiAliasedLabel();
            passwordLabel = new AntiAliasedLabel();
            showWindowLabel = new AntiAliasedLabel();

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

            loginLabel.Text = "Login";
            loginLabel.Location = new Point(71, 209);
            loginLabel.BackColor = Color.Transparent;
            loginLabel.ForeColor = Color.FromArgb(160, 155, 140);
            loginLabel.Font = new Font(BeaufortforLOL_Bold, 15, GraphicsUnit.Pixel);

            passwordLabel.Text = "Password";
            passwordLabel.Location = new Point(318, 209);
            passwordLabel.BackColor = Color.Transparent;
            passwordLabel.ForeColor = Color.FromArgb(160, 155, 140);
            passwordLabel.Font = new Font(BeaufortforLOL_Bold, 15, GraphicsUnit.Pixel);

            showWindowLabel.Text = "Show window on start";
            showWindowLabel.Location = new Point(90, 270);
            showWindowLabel.BackColor = Color.Transparent;
            showWindowLabel.ForeColor = Color.FromArgb(160, 155, 140);
            showWindowLabel.Font = new Font(BeaufortforLOL_Bold, 15, GraphicsUnit.Pixel);

            gameFolder.Text = "";
            gameFolder.Location = new Point(74, 174);
            gameFolder.BackColor = Color.Transparent;
            gameFolder.ForeColor = Color.FromArgb(160, 155, 140);
            gameFolder.Font = new Font(BeaufortforLOL_Bold, 15, GraphicsUnit.Pixel);
            gameFolder.BorderStyle = BorderStyle.FixedSingle;
            gameFolder.AutoSize = false;
            gameFolder.Width = 455;
            gameFolder.Height = 22;
            gameFolder.TextAlign = ContentAlignment.MiddleLeft;
            gameFolder.Click += new EventHandler(selectFolderButton_Click);

            titleLabel.Text = "AutoLogin";
            titleLabel.Location = new Point(234, 20);
            titleLabel.BackColor = Color.Transparent;
            titleLabel.ForeColor = Color.DarkGoldenrod;
            titleLabel.Font = new Font(BeaufortforLOL_Bold, 30, FontStyle.Bold, GraphicsUnit.Pixel);

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

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    var path = fbd.SelectedPath.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
                    gameFolder.Text = path;
                    Data.GamePath = path;
                }
                else
                {
                    MessageBox.Show("Game folder not selected!", "AutoLogin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void SaveData()
        {
            Data.GamePath = gameFolder.Text;
            Data.Login = login.Text;
            Data.Password = password.Text;
            Data.Save();
            Data.Changed = false;
        }

        string tempLoginText = "";

        private void login_Enter(object sender, EventArgs e)
        {
            tempLoginText = login.Text;
            login.Text = "";
            Data.Changed = true;
        }

        private void login_Leave(object sender, EventArgs e)
        {
            if (login.Text.Length == 0)
            {
                login.Text = tempLoginText;
                Data.Changed = false;
            }
            else
            {
                Data.Changed = true;
            }
        }

        string tempPasswordText = "";

        private void password_Enter(object sender, EventArgs e)
        {
            tempPasswordText = password.Text;
            password.Text = "";
            Data.Changed = true;
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (password.Text.Length == 0)
            {
                password.Text = tempPasswordText;
                Data.Changed = false;
            }
            else
            {
                Data.Changed = true;
            }
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
                if (YesNoDialog.Show("Save changes?") == DialogResult.Yes)
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
            YesNoDialog.Show("LoL AutoLogin was developed by Profesor08 in 2017 for comfortable and free use", MessageBoxButtons.OK);
        }
    }

}
