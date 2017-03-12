using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoL_AutoLogin
{
    public partial class YesNoDialog : Form
    {
        bool mouseDown = false;
        Point mousePosition;

        public YesNoDialog()
        {
            InitializeComponent();
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
        }

        public YesNoDialog(string message, FontFamily fontCollection)
        {
            InitializeComponent();
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;

            yesButton.Font = new Font(fontCollection, 17, FontStyle.Bold, GraphicsUnit.Pixel);
            yesButton.ForeColor = Color.FromArgb(160, 155, 140);
            noButton.Font = new Font(fontCollection, 17, FontStyle.Bold, GraphicsUnit.Pixel);
            noButton.ForeColor = Color.FromArgb(160, 155, 140);

            var label = new AntiAliasedLabel();
            label.Text = message;
            label.Location = new Point(30, 35);
            label.Size = new Size(316, 69);
            label.AutoSize = false;
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.FromArgb(160, 155, 140);
            label.Font = new Font(fontCollection, 17, FontStyle.Bold, GraphicsUnit.Pixel);
            label.TextAlign = ContentAlignment.MiddleCenter;

            Controls.Add(label);
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void yesButton_MouseEnter(object sender, EventArgs e)
        {
            yesButton.BackgroundImage = Properties.Resources.smallFormButton_hover;
        }

        private void yesButton_MouseLeave(object sender, EventArgs e)
        {
            yesButton.BackgroundImage = Properties.Resources.smallFormButton;
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void noButton_MouseEnter(object sender, EventArgs e)
        {
            noButton.BackgroundImage = Properties.Resources.smallFormButton_hover;
        }

        private void noButton_MouseLeave(object sender, EventArgs e)
        {
            noButton.BackgroundImage = Properties.Resources.smallFormButton;
        }

        public static DialogResult Show(string message, FontFamily fontCollection)
        {
            return new YesNoDialog(message, fontCollection).ShowDialog();
        }

        private void YesNoDialog_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mousePosition = new Point(e.X, e.Y);
        }

        private void YesNoDialog_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void YesNoDialog_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point(Cursor.Position.X - mousePosition.X, Cursor.Position.Y - mousePosition.Y);
            }
        }
    }
}
