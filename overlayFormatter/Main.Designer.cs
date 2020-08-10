namespace overlayFormatter
{
    partial class Main
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
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.selectedPathLbl = new System.Windows.Forms.Label();
            this.selectedPathTxt = new System.Windows.Forms.TextBox();
            this.formatBtn = new System.Windows.Forms.Button();
            this.actionsLog = new System.Windows.Forms.TextBox();
            this.exportBtn = new System.Windows.Forms.Button();
            this.creditLabel = new System.Windows.Forms.LinkLabel();
            this.tattooRadioButton = new System.Windows.Forms.RadioButton();
            this.hairRadioButton = new System.Windows.Forms.RadioButton();
            this.overlayRadioButton = new System.Windows.Forms.RadioButton();
            this.exportGroupBox = new System.Windows.Forms.GroupBox();
            this.decalRadioButton = new System.Windows.Forms.RadioButton();
            this.errorCheckBox = new System.Windows.Forms.CheckBox();
            this.exportGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Location = new System.Drawing.Point(238, 23);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(75, 23);
            this.selectFileBtn.TabIndex = 0;
            this.selectFileBtn.Text = "Open Folder";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // selectedPathLbl
            // 
            this.selectedPathLbl.AutoSize = true;
            this.selectedPathLbl.Location = new System.Drawing.Point(12, 9);
            this.selectedPathLbl.Name = "selectedPathLbl";
            this.selectedPathLbl.Size = new System.Drawing.Size(76, 13);
            this.selectedPathLbl.TabIndex = 1;
            this.selectedPathLbl.Text = "Selected path:";
            // 
            // selectedPathTxt
            // 
            this.selectedPathTxt.Location = new System.Drawing.Point(15, 25);
            this.selectedPathTxt.Name = "selectedPathTxt";
            this.selectedPathTxt.ReadOnly = true;
            this.selectedPathTxt.Size = new System.Drawing.Size(217, 20);
            this.selectedPathTxt.TabIndex = 2;
            // 
            // formatBtn
            // 
            this.formatBtn.Enabled = false;
            this.formatBtn.Location = new System.Drawing.Point(232, 203);
            this.formatBtn.Name = "formatBtn";
            this.formatBtn.Size = new System.Drawing.Size(75, 23);
            this.formatBtn.TabIndex = 3;
            this.formatBtn.Text = "Format";
            this.formatBtn.UseVisualStyleBackColor = true;
            this.formatBtn.Click += new System.EventHandler(this.formatBtn_Click);
            // 
            // actionsLog
            // 
            this.actionsLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.4F);
            this.actionsLog.Location = new System.Drawing.Point(15, 52);
            this.actionsLog.Multiline = true;
            this.actionsLog.Name = "actionsLog";
            this.actionsLog.ReadOnly = true;
            this.actionsLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.actionsLog.Size = new System.Drawing.Size(298, 145);
            this.actionsLog.TabIndex = 4;
            // 
            // exportBtn
            // 
            this.exportBtn.Enabled = false;
            this.exportBtn.Location = new System.Drawing.Point(217, 35);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(75, 23);
            this.exportBtn.TabIndex = 5;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // creditLabel
            // 
            this.creditLabel.AutoSize = true;
            this.creditLabel.Location = new System.Drawing.Point(210, 305);
            this.creditLabel.Name = "creditLabel";
            this.creditLabel.Size = new System.Drawing.Size(97, 13);
            this.creditLabel.TabIndex = 7;
            this.creditLabel.TabStop = true;
            this.creditLabel.Text = "Created by Frazzle.";
            this.creditLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.creditLabel_LinkClicked);
            // 
            // tattooRadioButton
            // 
            this.tattooRadioButton.AutoSize = true;
            this.tattooRadioButton.Enabled = false;
            this.tattooRadioButton.Location = new System.Drawing.Point(113, 18);
            this.tattooRadioButton.Name = "tattooRadioButton";
            this.tattooRadioButton.Size = new System.Drawing.Size(98, 17);
            this.tattooRadioButton.TabIndex = 8;
            this.tattooRadioButton.Text = "Tattoo overlays";
            this.tattooRadioButton.UseVisualStyleBackColor = true;
            // 
            // hairRadioButton
            // 
            this.hairRadioButton.AutoSize = true;
            this.hairRadioButton.Enabled = false;
            this.hairRadioButton.Location = new System.Drawing.Point(6, 41);
            this.hairRadioButton.Name = "hairRadioButton";
            this.hairRadioButton.Size = new System.Drawing.Size(86, 17);
            this.hairRadioButton.TabIndex = 9;
            this.hairRadioButton.Text = "Hair overlays";
            this.hairRadioButton.UseVisualStyleBackColor = true;
            // 
            // overlayRadioButton
            // 
            this.overlayRadioButton.AutoSize = true;
            this.overlayRadioButton.Checked = true;
            this.overlayRadioButton.Enabled = false;
            this.overlayRadioButton.Location = new System.Drawing.Point(6, 18);
            this.overlayRadioButton.Name = "overlayRadioButton";
            this.overlayRadioButton.Size = new System.Drawing.Size(78, 17);
            this.overlayRadioButton.TabIndex = 10;
            this.overlayRadioButton.TabStop = true;
            this.overlayRadioButton.Text = "All overlays";
            this.overlayRadioButton.UseVisualStyleBackColor = true;
            // 
            // exportGroupBox
            // 
            this.exportGroupBox.Controls.Add(this.decalRadioButton);
            this.exportGroupBox.Controls.Add(this.overlayRadioButton);
            this.exportGroupBox.Controls.Add(this.exportBtn);
            this.exportGroupBox.Controls.Add(this.hairRadioButton);
            this.exportGroupBox.Controls.Add(this.tattooRadioButton);
            this.exportGroupBox.Location = new System.Drawing.Point(15, 232);
            this.exportGroupBox.Name = "exportGroupBox";
            this.exportGroupBox.Size = new System.Drawing.Size(298, 67);
            this.exportGroupBox.TabIndex = 11;
            this.exportGroupBox.TabStop = false;
            this.exportGroupBox.Text = "Export options";
            // 
            // decalRadioButton
            // 
            this.decalRadioButton.AutoSize = true;
            this.decalRadioButton.Enabled = false;
            this.decalRadioButton.Location = new System.Drawing.Point(113, 41);
            this.decalRadioButton.Name = "decalRadioButton";
            this.decalRadioButton.Size = new System.Drawing.Size(95, 17);
            this.decalRadioButton.TabIndex = 11;
            this.decalRadioButton.Text = "Decal overlays";
            this.decalRadioButton.UseVisualStyleBackColor = true;
            // 
            // errorCheckBox
            // 
            this.errorCheckBox.AutoSize = true;
            this.errorCheckBox.Checked = true;
            this.errorCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorCheckBox.Location = new System.Drawing.Point(15, 207);
            this.errorCheckBox.Name = "errorCheckBox";
            this.errorCheckBox.Size = new System.Drawing.Size(85, 17);
            this.errorCheckBox.TabIndex = 12;
            this.errorCheckBox.Text = "Ignore errors";
            this.errorCheckBox.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 327);
            this.Controls.Add(this.errorCheckBox);
            this.Controls.Add(this.exportGroupBox);
            this.Controls.Add(this.creditLabel);
            this.Controls.Add(this.actionsLog);
            this.Controls.Add(this.formatBtn);
            this.Controls.Add(this.selectedPathTxt);
            this.Controls.Add(this.selectedPathLbl);
            this.Controls.Add(this.selectFileBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Main";
            this.Text = "overlayFormatter";
            this.Load += new System.EventHandler(this.Main_Load);
            this.exportGroupBox.ResumeLayout(false);
            this.exportGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.Label selectedPathLbl;
        private System.Windows.Forms.TextBox selectedPathTxt;
        private System.Windows.Forms.Button formatBtn;
        private System.Windows.Forms.TextBox actionsLog;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.LinkLabel creditLabel;
        private System.Windows.Forms.RadioButton tattooRadioButton;
        private System.Windows.Forms.RadioButton hairRadioButton;
        private System.Windows.Forms.RadioButton overlayRadioButton;
        private System.Windows.Forms.GroupBox exportGroupBox;
        private System.Windows.Forms.RadioButton decalRadioButton;
        private System.Windows.Forms.CheckBox errorCheckBox;
    }
}

