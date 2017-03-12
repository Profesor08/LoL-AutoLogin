using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoL_AutoLogin
{
    public partial class ImageTextBox : TextBox
    {

        private PictureBox backgroundPictureBox;
        private Image backgroundImage;
        private Image backgroundImageActive;

        public string TempText = "";

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

        public DockStyle BackgroundDockStyle
        {
            get
            {
                return backgroundPictureBox.Dock;
            }

            set
            {
                backgroundPictureBox.Dock = value;
            }
        }

        public ImageTextBox()
        {
            InitializeComponent();

            backgroundPictureBox = new PictureBox();
            BackgroundDockStyle = DockStyle.Fill;

            backgroundPictureBox.MouseClick += (_, args) =>
            {
                Focus();
            };
 
            Controls.Add(backgroundPictureBox);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            DrawTextOnBackgroundImage();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            DrawTextOnBackgroundImage();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            DrawTextOnBackgroundImage();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
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
