using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace LoL_AutoLogin
{
    public partial class GUI : Form
    {

        public GUI()
        {
            InitializeComponent();

            Icon = Icon.FromHandle(Properties.Resources.icon.GetHicon());

            gameFolder.Text = Data.GamePath;
            login.Text = Data.Login;
            password.Text = Data.Password;
            checkBox1.Checked = Data.ShowUI;

            startButton.DialogResult = DialogResult.OK;
        }

        private void UI_Load(object sender, EventArgs e)
        {
            /*if (!Data.ShowUI)
            {
                BeginInvoke(new MethodInvoker(Close));
                StartClient();
            }*/
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
            Data.ShowUI = checkBox1.Checked;
            Data.Save();
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.None)
            {
                DialogResult dialogResult = MessageBox.Show("Save changes?", "LoL AutoLogin", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    SaveData();
                }
            }
        }
    }
}
