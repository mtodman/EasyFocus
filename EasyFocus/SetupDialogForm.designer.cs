namespace ASCOM.EasyFocus
{
    partial class SetupDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialogForm));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textStepDelay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textMaxPos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textMinPos = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textCurrentPos = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdOK.BackColor = System.Drawing.Color.Maroon;
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmdOK.ForeColor = System.Drawing.Color.Red;
            this.cmdOK.Location = new System.Drawing.Point(12, 170);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCancel.BackColor = System.Drawing.Color.Maroon;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.ForeColor = System.Drawing.Color.Red;
            this.cmdCancel.Location = new System.Drawing.Point(12, 200);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 25);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.EasyFocus.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(135, 179);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(10, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port";
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.BackColor = System.Drawing.Color.Maroon;
            this.chkTrace.ForeColor = System.Drawing.Color.Red;
            this.chkTrace.Location = new System.Drawing.Point(103, 136);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = false;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.BackColor = System.Drawing.SystemColors.InfoText;
            this.comboBoxComPort.ForeColor = System.Drawing.Color.Red;
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(96, 6);
            this.comboBoxComPort.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(76, 21);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(10, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Step Delay (ms)";
            // 
            // textStepDelay
            // 
            this.textStepDelay.BackColor = System.Drawing.SystemColors.InfoText;
            this.textStepDelay.ForeColor = System.Drawing.Color.Red;
            this.textStepDelay.Location = new System.Drawing.Point(96, 76);
            this.textStepDelay.Margin = new System.Windows.Forms.Padding(2);
            this.textStepDelay.Name = "textStepDelay";
            this.textStepDelay.Size = new System.Drawing.Size(76, 20);
            this.textStepDelay.TabIndex = 26;
            this.textStepDelay.Text = "20";
            this.textStepDelay.Leave += new System.EventHandler(this.textStepDelay_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(10, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Max Position";
            // 
            // textMaxPos
            // 
            this.textMaxPos.BackColor = System.Drawing.SystemColors.InfoText;
            this.textMaxPos.ForeColor = System.Drawing.Color.Red;
            this.textMaxPos.Location = new System.Drawing.Point(96, 54);
            this.textMaxPos.Margin = new System.Windows.Forms.Padding(2);
            this.textMaxPos.Name = "textMaxPos";
            this.textMaxPos.Size = new System.Drawing.Size(76, 20);
            this.textMaxPos.TabIndex = 24;
            this.textMaxPos.Text = "10000";
            this.textMaxPos.Leave += new System.EventHandler(this.textMaxPos_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(10, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Min Position";
            // 
            // textMinPos
            // 
            this.textMinPos.BackColor = System.Drawing.SystemColors.ControlText;
            this.textMinPos.ForeColor = System.Drawing.Color.Red;
            this.textMinPos.Location = new System.Drawing.Point(96, 31);
            this.textMinPos.Margin = new System.Windows.Forms.Padding(2);
            this.textMinPos.Name = "textMinPos";
            this.textMinPos.Size = new System.Drawing.Size(76, 20);
            this.textMinPos.TabIndex = 22;
            this.textMinPos.Text = "-10000";
            this.textMinPos.Leave += new System.EventHandler(this.textMinPos_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(10, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Current Position";
            // 
            // textCurrentPos
            // 
            this.textCurrentPos.BackColor = System.Drawing.SystemColors.InfoText;
            this.textCurrentPos.ForeColor = System.Drawing.Color.Red;
            this.textCurrentPos.Location = new System.Drawing.Point(96, 99);
            this.textCurrentPos.Margin = new System.Windows.Forms.Padding(2);
            this.textCurrentPos.Name = "textCurrentPos";
            this.textCurrentPos.Size = new System.Drawing.Size(76, 20);
            this.textCurrentPos.TabIndex = 16;
            this.textCurrentPos.Leave += new System.EventHandler(this.textCurrentPos_Leave);
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(185, 235);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textStepDelay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textMaxPos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textMinPos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textCurrentPos);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EasyFocus2 Setup";
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textStepDelay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textMaxPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textMinPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textCurrentPos;
    }
}