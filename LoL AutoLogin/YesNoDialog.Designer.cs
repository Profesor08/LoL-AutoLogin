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
            this.Yes = new System.Windows.Forms.Button();
            this.No = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ignore = new System.Windows.Forms.Button();
            this.Retry = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.Abort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Yes
            // 
            this.Yes.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormButton;
            this.Yes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Yes.FlatAppearance.BorderSize = 0;
            this.Yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Yes.ForeColor = System.Drawing.SystemColors.Control;
            this.Yes.Location = new System.Drawing.Point(107, 21);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(79, 41);
            this.Yes.TabIndex = 1;
            this.Yes.Text = "YES";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Visible = false;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            this.Yes.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.Yes.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // No
            // 
            this.No.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormButton;
            this.No.Cursor = System.Windows.Forms.Cursors.Hand;
            this.No.FlatAppearance.BorderSize = 0;
            this.No.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.No.ForeColor = System.Drawing.SystemColors.Control;
            this.No.Location = new System.Drawing.Point(192, 21);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(79, 41);
            this.No.TabIndex = 2;
            this.No.Text = "NO";
            this.No.UseVisualStyleBackColor = true;
            this.No.Visible = false;
            this.No.Click += new System.EventHandler(this.No_Click);
            this.No.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.No.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // Cancel
            // 
            this.Cancel.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormButton;
            this.Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancel.FlatAppearance.BorderSize = 0;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.ForeColor = System.Drawing.SystemColors.Control;
            this.Cancel.Location = new System.Drawing.Point(277, 21);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(79, 41);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "CANCEL";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Visible = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            this.Cancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.Cancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // Ignore
            // 
            this.Ignore.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormButton;
            this.Ignore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Ignore.FlatAppearance.BorderSize = 0;
            this.Ignore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ignore.ForeColor = System.Drawing.SystemColors.Control;
            this.Ignore.Location = new System.Drawing.Point(232, 68);
            this.Ignore.Name = "Ignore";
            this.Ignore.Size = new System.Drawing.Size(79, 41);
            this.Ignore.TabIndex = 5;
            this.Ignore.Text = "IGNORE";
            this.Ignore.UseVisualStyleBackColor = true;
            this.Ignore.Visible = false;
            this.Ignore.Click += new System.EventHandler(this.Ignore_Click);
            this.Ignore.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.Ignore.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // Retry
            // 
            this.Retry.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormButton;
            this.Retry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Retry.FlatAppearance.BorderSize = 0;
            this.Retry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Retry.ForeColor = System.Drawing.SystemColors.Control;
            this.Retry.Location = new System.Drawing.Point(145, 68);
            this.Retry.Name = "Retry";
            this.Retry.Size = new System.Drawing.Size(79, 41);
            this.Retry.TabIndex = 4;
            this.Retry.Text = "RETRY";
            this.Retry.UseVisualStyleBackColor = true;
            this.Retry.Visible = false;
            this.Retry.Click += new System.EventHandler(this.Retry_Click);
            this.Retry.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.Retry.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // OK
            // 
            this.OK.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormButton;
            this.OK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OK.FlatAppearance.BorderSize = 0;
            this.OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OK.ForeColor = System.Drawing.SystemColors.Control;
            this.OK.Location = new System.Drawing.Point(22, 21);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(79, 41);
            this.OK.TabIndex = 6;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Visible = false;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            this.OK.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.OK.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // Abort
            // 
            this.Abort.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormButton;
            this.Abort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Abort.FlatAppearance.BorderSize = 0;
            this.Abort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Abort.ForeColor = System.Drawing.SystemColors.Control;
            this.Abort.Location = new System.Drawing.Point(58, 68);
            this.Abort.Name = "Abort";
            this.Abort.Size = new System.Drawing.Size(79, 41);
            this.Abort.TabIndex = 7;
            this.Abort.Text = "ABORT";
            this.Abort.UseVisualStyleBackColor = true;
            this.Abort.Visible = false;
            this.Abort.Click += new System.EventHandler(this.Abort_Click);
            this.Abort.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.Abort.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // YesNoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.smallFormYesNoCancel;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(374, 172);
            this.Controls.Add(this.Abort);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Ignore);
            this.Controls.Add(this.Retry);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.No);
            this.Controls.Add(this.Yes);
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

        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.Button No;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ignore;
        private System.Windows.Forms.Button Retry;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Abort;
    }
}