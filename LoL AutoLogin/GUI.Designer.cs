namespace LoL_AutoLogin
{
    partial class GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.selectFolder = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.PictureBox();
            this.closeButton = new System.Windows.Forms.Label();
            this.hideButton = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.showWindowChechBox = new System.Windows.Forms.PictureBox();
            this.infoButton = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWindowChechBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoButton)).BeginInit();
            this.SuspendLayout();
            // 
            // selectFolder
            // 
            this.selectFolder.BackColor = System.Drawing.Color.Transparent;
            this.selectFolder.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.options;
            this.selectFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.selectFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selectFolder.FlatAppearance.BorderSize = 0;
            this.selectFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.selectFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.selectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectFolder.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.selectFolder.Location = new System.Drawing.Point(532, 173);
            this.selectFolder.Margin = new System.Windows.Forms.Padding(0);
            this.selectFolder.Name = "selectFolder";
            this.selectFolder.Size = new System.Drawing.Size(24, 24);
            this.selectFolder.TabIndex = 2;
            this.selectFolder.Text = "Select";
            this.selectFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.selectFolder.UseVisualStyleBackColor = false;
            this.selectFolder.Click += new System.EventHandler(this.selectFolderButton_Click);
            this.selectFolder.MouseEnter += new System.EventHandler(this.selectFolder_MouseEnter);
            this.selectFolder.MouseLeave += new System.EventHandler(this.selectFolder_MouseLeave);
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.Color.Transparent;
            this.playButton.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.play;
            this.playButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.playButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playButton.Location = new System.Drawing.Point(411, 281);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(202, 50);
            this.playButton.TabIndex = 9;
            this.playButton.TabStop = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            this.playButton.Paint += new System.Windows.Forms.PaintEventHandler(this.playButton_Paint);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(581, 3);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(23, 23);
            this.closeButton.TabIndex = 10;
            this.closeButton.Text = "✖";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.closeButton.MouseEnter += new System.EventHandler(this.closeButton_MouseEnter);
            this.closeButton.MouseLeave += new System.EventHandler(this.closeButton_MouseLeave);
            // 
            // hideButton
            // 
            this.hideButton.BackColor = System.Drawing.Color.Transparent;
            this.hideButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hideButton.ForeColor = System.Drawing.Color.White;
            this.hideButton.Location = new System.Drawing.Point(549, -2);
            this.hideButton.Margin = new System.Windows.Forms.Padding(0);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(20, 23);
            this.hideButton.TabIndex = 11;
            this.hideButton.Text = "_";
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
            this.hideButton.MouseEnter += new System.EventHandler(this.hideButton_MouseEnter);
            this.hideButton.MouseLeave += new System.EventHandler(this.hideButton_MouseLeave);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox2.Location = new System.Drawing.Point(318, 228);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(238, 37);
            this.richTextBox2.TabIndex = 15;
            this.richTextBox2.Text = " gdfg dfg dfg dfg";
            this.richTextBox2.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(74, 228);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(238, 37);
            this.richTextBox1.TabIndex = 14;
            this.richTextBox1.Text = " gdfg dfg dfg dfg";
            this.richTextBox1.Visible = false;
            // 
            // showWindowChechBox
            // 
            this.showWindowChechBox.BackColor = System.Drawing.Color.Transparent;
            this.showWindowChechBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("showWindowChechBox.BackgroundImage")));
            this.showWindowChechBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.showWindowChechBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showWindowChechBox.Image = ((System.Drawing.Image)(resources.GetObject("showWindowChechBox.Image")));
            this.showWindowChechBox.Location = new System.Drawing.Point(74, 272);
            this.showWindowChechBox.Name = "showWindowChechBox";
            this.showWindowChechBox.Size = new System.Drawing.Size(16, 16);
            this.showWindowChechBox.TabIndex = 16;
            this.showWindowChechBox.TabStop = false;
            this.showWindowChechBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showWindowChechBox_MouseDown);
            this.showWindowChechBox.MouseEnter += new System.EventHandler(this.showWindow_MouseEnter);
            this.showWindowChechBox.MouseLeave += new System.EventHandler(this.showWindow_MouseLeave);
            // 
            // infoButton
            // 
            this.infoButton.BackColor = System.Drawing.Color.Transparent;
            this.infoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.infoButton.Image = ((System.Drawing.Image)(resources.GetObject("infoButton.Image")));
            this.infoButton.Location = new System.Drawing.Point(5, 3);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(23, 42);
            this.infoButton.TabIndex = 17;
            this.infoButton.TabStop = false;
            this.infoButton.Click += new System.EventHandler(this.infoButton_Click);
            this.infoButton.MouseEnter += new System.EventHandler(this.infoButton_MouseEnter);
            this.infoButton.MouseLeave += new System.EventHandler(this.infoButton_MouseLeave);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(74, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(455, 22);
            this.label1.TabIndex = 18;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LoL_AutoLogin.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(625, 343);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.showWindowChechBox);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.hideButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.selectFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoL AutoLogin";
            this.Click += new System.EventHandler(this.GUI_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GUI_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GUI_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GUI_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWindowChechBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button selectFolder;
        private System.Windows.Forms.PictureBox playButton;
        private System.Windows.Forms.Label closeButton;
        private System.Windows.Forms.Label hideButton;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox showWindowChechBox;
        private System.Windows.Forms.PictureBox infoButton;
        private System.Windows.Forms.Label label1;
    }
}