namespace LoL_AutoLogin
{
    partial class YesNoDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // yesButton
            // 
            this.yesButton.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormButton;
            this.yesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yesButton.FlatAppearance.BorderSize = 0;
            this.yesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yesButton.ForeColor = System.Drawing.SystemColors.Control;
            this.yesButton.Location = new System.Drawing.Point(104, 128);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(79, 41);
            this.yesButton.TabIndex = 1;
            this.yesButton.Text = "YES";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            this.yesButton.MouseEnter += new System.EventHandler(this.yesButton_MouseEnter);
            this.yesButton.MouseLeave += new System.EventHandler(this.yesButton_MouseLeave);
            // 
            // noButton
            // 
            this.noButton.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormButton;
            this.noButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.noButton.FlatAppearance.BorderSize = 0;
            this.noButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noButton.ForeColor = System.Drawing.SystemColors.Control;
            this.noButton.Location = new System.Drawing.Point(190, 128);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(79, 41);
            this.noButton.TabIndex = 2;
            this.noButton.Text = "NO";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            this.noButton.MouseEnter += new System.EventHandler(this.noButton_MouseEnter);
            this.noButton.MouseLeave += new System.EventHandler(this.noButton_MouseLeave);
            // 
            // YesNoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormYesNo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(374, 172);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "YesNoDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "YesNoDialog";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.YesNoDialog_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.YesNoDialog_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.YesNoDialog_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button noButton;
    }
}