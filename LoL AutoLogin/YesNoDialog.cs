using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoL_AutoLogin
{
    public partial class YesNoDialog : Form
    {
        bool mouseDown = false;
        Point mousePosition;
        AntiAliasedLabel text;
        AntiAliasedLabel Caption;

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
            InitializeDialogView(buttons);
            SetMessage(message);
        }

        private YesNoDialog(string message, string caption, MessageBoxButtons buttons)
        {
            InitializeComponent();
            InitializeCustomComponent();
            InitializeDialogView(buttons);
            DialogViewCaption(caption);
            SetMessage(message);
        }

        private void InitializeDialogView(MessageBoxButtons buttons)
        {
            DialogViewBackground(buttons);
            DialogViewButtons(buttons);
        }

        private void DialogViewBackground(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore: BackgroundImage = Properties.Resources.smallFormYesNoCancel; break;
                case MessageBoxButtons.OK: BackgroundImage = Properties.Resources.smallFormYes; break;
                case MessageBoxButtons.OKCancel: BackgroundImage = Properties.Resources.smallFormYesNo; break;
                case MessageBoxButtons.RetryCancel: BackgroundImage = Properties.Resources.smallFormYesNo; break;
                case MessageBoxButtons.YesNo: BackgroundImage = Properties.Resources.smallFormYesNo; break;
                case MessageBoxButtons.YesNoCancel: BackgroundImage = Properties.Resources.smallFormYesNoCancel; break;
            }
        }

        private void DialogViewCaption(string text)
        {
            Caption.Visible = true;
            Caption.Text = text;
            this.text.Top = 45;

            /*
            switch (icon)
            {
                case MessageBoxIcon.Error: break;
                case MessageBoxIcon.Warning: break;
                case MessageBoxIcon.Information: break;
                case MessageBoxIcon.Question: break;
                case MessageBoxIcon.None: break;
            }
            */
        }

        private void DialogViewButtons(MessageBoxButtons buttons)
        {

            switch (buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        Abort.Visible = true;
                        Retry.Visible = true;
                        Ignore.Visible = true;
                        Abort.Location = new Point(61, 128);
                        Retry.Location = new Point(148, 128);
                        Ignore.Location = new Point(235, 128);
                        break;
                    }
                case MessageBoxButtons.OK:
                    {
                        OK.Visible = true;
                        OK.Location = new Point(142, 127);
                        break;
                    }
                case MessageBoxButtons.OKCancel:
                    {
                        OK.Visible = true;
                        OK.Location = new Point(104, 128);
                        Cancel.Visible = true;
                        Cancel.Location = new Point(191, 128);
                        break;
                    }
                case MessageBoxButtons.RetryCancel:
                    {
                        Retry.Visible = true;
                        Retry.Location = new Point(104, 128);
                        Cancel.Visible = true;
                        Cancel.Location = new Point(191, 128);
                        break;
                    }
                case MessageBoxButtons.YesNo:
                    {
                        Yes.Visible = true;
                        Yes.Location = new Point(104, 128);
                        No.Visible = true;
                        No.Location = new Point(191, 128);
                        break;
                    }
                case MessageBoxButtons.YesNoCancel:
                    {
                        Yes.Visible = true;
                        Yes.Location = new Point(61, 128);
                        No.Visible = true;
                        No.Location = new Point(148, 128);
                        Cancel.Visible = true;
                        Cancel.Location = new Point(235, 128);
                        break;
                    }
            }
        }

        private void InitializeCustomComponent()
        {
            TransparencyKey = Color.Turquoise;
            BackColor = Color.Turquoise;

            var regular = CustomFont.Load(Properties.Resources.BeaufortforLOL_Regular);
            var bold = CustomFont.Load(Properties.Resources.BeaufortforLOL_Bold);

            OK.Font = new Font(regular, 13, FontStyle.Bold, GraphicsUnit.Pixel);
            OK.ForeColor = Color.FromArgb(160, 155, 140);

            Yes.Font = new Font(regular, 13, FontStyle.Bold, GraphicsUnit.Pixel);
            Yes.ForeColor = Color.FromArgb(160, 155, 140);

            No.Font = new Font(regular, 13, FontStyle.Bold, GraphicsUnit.Pixel);
            No.ForeColor = Color.FromArgb(160, 155, 140);

            Cancel.Font = new Font(regular, 13, FontStyle.Bold, GraphicsUnit.Pixel);
            Cancel.ForeColor = Color.FromArgb(160, 155, 140);

            Abort.Font = new Font(regular, 13, FontStyle.Bold, GraphicsUnit.Pixel);
            Abort.ForeColor = Color.FromArgb(160, 155, 140);

            Retry.Font = new Font(regular, 13, FontStyle.Bold, GraphicsUnit.Pixel);
            Retry.ForeColor = Color.FromArgb(160, 155, 140);

            Ignore.Font = new Font(regular, 13, FontStyle.Bold, GraphicsUnit.Pixel);
            Ignore.ForeColor = Color.FromArgb(160, 155, 140);

            OK.Click += new EventHandler(buttonAudioClick);
            Yes.Click += new EventHandler(buttonAudioClick);
            No.Click += new EventHandler(buttonAudioClick);
            Cancel.Click += new EventHandler(buttonAudioClick);
            Abort.Click += new EventHandler(buttonAudioClick);
            Retry.Click += new EventHandler(buttonAudioClick);
            Ignore.Click += new EventHandler(buttonAudioClick);

            OK.MouseEnter += new EventHandler(buttonAudioHover);
            Yes.MouseEnter += new EventHandler(buttonAudioHover);
            No.MouseEnter += new EventHandler(buttonAudioHover);
            Cancel.MouseEnter += new EventHandler(buttonAudioHover);
            Abort.MouseEnter += new EventHandler(buttonAudioHover);
            Retry.MouseEnter += new EventHandler(buttonAudioHover);
            Ignore.MouseEnter += new EventHandler(buttonAudioHover);

            text = new AntiAliasedLabel();
            text.Location = new Point(20, 35);
            text.Size = new Size(Width - 40, 70);
            text.AutoSize = false;
            text.BackColor = Color.Transparent;
            text.ForeColor = Color.FromArgb(160, 155, 140);
            text.TextAlign = ContentAlignment.TopCenter;
            text.Font = new Font(regular, 17, FontStyle.Bold, GraphicsUnit.Pixel);

            Caption = new AntiAliasedLabel();
            Caption.AutoSize = false;
            Caption.BackColor = Color.Transparent;
            Caption.Width = Width - 40;
            Caption.Height = 30;
            Caption.Location = new Point(20, 20);
            Caption.TextAlign = ContentAlignment.TopCenter;
            Caption.Font = new Font(bold, 20, FontStyle.Bold, GraphicsUnit.Pixel);
            Caption.ForeColor = Color.FromArgb(240, 230, 210);
            Caption.Visible = false;

            Controls.Add(Caption);
            Controls.Add(text);
        }

        private void SetMessage(string message)
        {
            text.Text = message;
        }

        private void buttonAudioHover(object sender, EventArgs e)
        {
            Audio.ButtonHover();
        }

        private void buttonAudioClick(object sender, EventArgs e)
        {
            Audio.ButtonClick();
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources.smallFormButton_hover;
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources.smallFormButton;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void No_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Abort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void Retry_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
        }

        private void Ignore_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
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

        public static DialogResult Show(string message, string cation, MessageBoxButtons buttons)
        {
            return new YesNoDialog(message, cation, buttons).ShowDialog();
        }
    }
}
