using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoL_AutoLogin
{
    public partial class About : Form
    {

        bool mouseDown = false;
        Point mousePosition;

        public About()
        {
            InitializeComponent();
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
        }

        public About(FontFamily fontCollection)
        {
            InitializeComponent();
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;

            button.Font = new Font(fontCollection, 17, FontStyle.Bold, GraphicsUnit.Pixel);
            button.ForeColor = Color.FromArgb(160, 155, 140);

            var label = new AntiAliasedLabel();
            label.Text = "LoL AutoLogin was developed by Profesor08 in 2017 for comfortable and free use";
            label.Location = new Point(30, 35);
            label.Size = new Size(316, 69);
            label.AutoSize = false;
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.FromArgb(160, 155, 140);
            label.Font = new Font(fontCollection, 17, FontStyle.Bold, GraphicsUnit.Pixel);
            label.TextAlign = ContentAlignment.MiddleCenter;

            Controls.Add(label);
        }

        private void About_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mousePosition = new Point(e.X, e.Y);
        }

        private void About_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void About_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point(Cursor.Position.X - mousePosition.X, Cursor.Position.Y - mousePosition.Y);
            }
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            button.BackgroundImage = Properties.Resources.smallFormButton_hover;
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            button.BackgroundImage = Properties.Resources.smallFormButton;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
