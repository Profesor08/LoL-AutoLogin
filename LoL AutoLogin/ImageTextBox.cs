using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace LoL_AutoLogin
{
    public partial class ImageTextBox : TextBox
    {

        private PictureBox backgroundPictureBox;
        private Image backgroundImage;
        private Image backgroundImageActive;

        public new Image BackgroundImage
        {
            get
            {
                return backgroundImage;
            }

            set
            {
                backgroundImage = value;
                DrawTextOnBackgroundImage();
            }
        }

        public Image BackgroundImageActive
        {
            get
            {
                return backgroundImageActive;
            }

            set
            {
                backgroundImageActive = value;
                DrawTextOnBackgroundImage();
            }
        }

        public ImageTextBox()
        {
            InitializeComponent();

            backgroundPictureBox = new PictureBox();
            backgroundPictureBox.Dock = DockStyle.Fill;

            backgroundPictureBox.MouseClick += (_, args) =>
            {
                Focus();
            };

            Controls.Add(backgroundPictureBox);

            Enter += new EventHandler(FocusEnter);
            Leave += new EventHandler(FocusLeave);
        }

        private void FocusEnter(object sender, EventArgs e)
        {
            DrawTextOnBackgroundImage();
        }

        private void FocusLeave(object sender, EventArgs e)
        {
            DrawTextOnBackgroundImage();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            DrawTextOnBackgroundImage();
        }

        private void DrawTextOnBackgroundImage()
        {
            var image = new Bitmap(Focused ? BackgroundImageActive : BackgroundImage);

            var brush = new SolidBrush(Color.FromArgb(240, 230, 210));
            var g = Graphics.FromImage(image);

            var text = PasswordChar == 0 ? Text : GetPasswordText();

            var point = PasswordChar == 0 ? new Point(5, 6) : new Point(5, 10);

            //g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.DrawString(text, Font, brush, point);
            backgroundPictureBox.Image = image;
        }

        private string GetPasswordText()
        {
            return new string('*', Text.Length);
        }
    }
}
