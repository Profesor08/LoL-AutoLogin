using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoL_AutoLogin
{
    public partial class YesNoDialog : Form
    {
        bool mouseDown = false;
        Point mousePosition;
        AntiAliasedLabel label;

        private YesNoDialog()
        {
            InitializeComponent();
            InitializeCustomComponent();
        }

        private YesNoDialog(string message)
        {
            InitializeComponent();
            InitializeCustomComponent();
            SetMessage(message);
        }

        private YesNoDialog(string message, MessageBoxButtons buttons)
        {
            InitializeComponent();
            InitializeCustomComponent();
            SetMessage(message);

            if (buttons == MessageBoxButtons.OK)
            {
                BackgroundImage = Properties.Resources.smallForm;
                noButton.Visible = false;
                yesButton.Location = new Point(142, 127);
                yesButton.Text = "OK";
            }
        }

        private void InitializeCustomComponent()
        {
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;

            var font = CustomFont.Load(Properties.Resources.BeaufortforLOL_Regular);

            yesButton.Font = new Font(font, 17, FontStyle.Bold, GraphicsUnit.Pixel);
            yesButton.ForeColor = Color.FromArgb(160, 155, 140);
            noButton.Font = new Font(font, 17, FontStyle.Bold, GraphicsUnit.Pixel);
            noButton.ForeColor = Color.FromArgb(160, 155, 140);
            
            label = new AntiAliasedLabel();
            label.Location = new Point(30, 35);
            label.Size = new Size(316, 69);
            label.AutoSize = false;
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.FromArgb(160, 155, 140);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font(font, 17, FontStyle.Bold, GraphicsUnit.Pixel);
            Controls.Add(label);
        }

        private void SetMessage(string message)
        {
            label.Text = message;
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

        public static DialogResult Show(string message)
        {
            return new YesNoDialog(message).ShowDialog();
        }

        public static DialogResult Show(string message, MessageBoxButtons buttons)
        {
            return new YesNoDialog(message, buttons).ShowDialog();
        }
    }
}
