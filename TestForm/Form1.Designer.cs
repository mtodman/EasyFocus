namespace ASCOM.EasyFocus
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonChoose = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelDriverId = new System.Windows.Forms.Label();
            this.textCurrentPos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMoveIn = new System.Windows.Forms.Button();
            this.btnMoveOut = new System.Windows.Forms.Button();
            this.textMoveIn = new System.Windows.Forms.TextBox();
            this.textMoveOut = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textMinPos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textMaxPos = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textStepDelay = new System.Windows.Forms.TextBox();
            this.btnSetPos = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnGetTemp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTemp = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(309, 10);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(72, 23);
            this.buttonChoose.TabIndex = 0;
            this.buttonChoose.Text = "Choose";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(309, 39);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(72, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelDriverId
            // 
            this.labelDriverId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.EasyFocus.Properties.Settings.Default, "DriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.labelDriverId.Location = new System.Drawing.Point(12, 40);
            this.labelDriverId.Name = "labelDriverId";
            this.labelDriverId.Size = new System.Drawing.Size(291, 21);
            this.labelDriverId.TabIndex = 2;
            this.labelDriverId.Text = global::ASCOM.EasyFocus.Properties.Settings.Default.DriverId;
            this.labelDriverId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textCurrentPos
            // 
            this.textCurrentPos.Enabled = false;
            this.textCurrentPos.Location = new System.Drawing.Point(96, 87);
            this.textCurrentPos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textCurrentPos.Name = "textCurrentPos";
            this.textCurrentPos.Size = new System.Drawing.Size(76, 20);
            this.textCurrentPos.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 89);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Current Position";
            // 
            // btnMoveIn
            // 
            this.btnMoveIn.Enabled = false;
            this.btnMoveIn.Location = new System.Drawing.Point(12, 123);
            this.btnMoveIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMoveIn.Name = "btnMoveIn";
            this.btnMoveIn.Size = new System.Drawing.Size(56, 19);
            this.btnMoveIn.TabIndex = 5;
            this.btnMoveIn.Text = "Move In";
            this.btnMoveIn.UseVisualStyleBackColor = true;
            this.btnMoveIn.Click += new System.EventHandler(this.btnMoveIn_Click);
            // 
            // btnMoveOut
            // 
            this.btnMoveOut.Enabled = false;
            this.btnMoveOut.Location = new System.Drawing.Point(12, 169);
            this.btnMoveOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMoveOut.Name = "btnMoveOut";
            this.btnMoveOut.Size = new System.Drawing.Size(63, 18);
            this.btnMoveOut.TabIndex = 6;
            this.btnMoveOut.Text = "Move Out";
            this.btnMoveOut.UseVisualStyleBackColor = true;
            this.btnMoveOut.Click += new System.EventHandler(this.btnMoveOut_Click);
            // 
            // textMoveIn
            // 
            this.textMoveIn.Enabled = false;
            this.textMoveIn.Location = new System.Drawing.Point(96, 123);
            this.textMoveIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textMoveIn.Name = "textMoveIn";
            this.textMoveIn.Size = new System.Drawing.Size(76, 20);
            this.textMoveIn.TabIndex = 7;
            this.textMoveIn.Text = "--";
            // 
            // textMoveOut
            // 
            this.textMoveOut.Enabled = false;
            this.textMoveOut.Location = new System.Drawing.Point(96, 169);
            this.textMoveOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textMoveOut.Name = "textMoveOut";
            this.textMoveOut.Size = new System.Drawing.Size(76, 20);
            this.textMoveOut.TabIndex = 8;
            this.textMoveOut.Text = "--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 215);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Min Position";
            // 
            // textMinPos
            // 
            this.textMinPos.Enabled = false;
            this.textMinPos.Location = new System.Drawing.Point(96, 213);
            this.textMinPos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textMinPos.Name = "textMinPos";
            this.textMinPos.Size = new System.Drawing.Size(76, 20);
            this.textMinPos.TabIndex = 9;
            this.textMinPos.Text = "-----";
            this.textMinPos.Leave += new System.EventHandler(this.textMinPos_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 244);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Max Position";
            // 
            // textMaxPos
            // 
            this.textMaxPos.Enabled = false;
            this.textMaxPos.Location = new System.Drawing.Point(96, 241);
            this.textMaxPos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textMaxPos.Name = "textMaxPos";
            this.textMaxPos.Size = new System.Drawing.Size(76, 20);
            this.textMaxPos.TabIndex = 11;
            this.textMaxPos.Text = "-----";
            this.textMaxPos.Leave += new System.EventHandler(this.textMaxPos_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 266);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Step Delay (ms)";
            // 
            // textStepDelay
            // 
            this.textStepDelay.Enabled = false;
            this.textStepDelay.Location = new System.Drawing.Point(96, 264);
            this.textStepDelay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textStepDelay.Name = "textStepDelay";
            this.textStepDelay.Size = new System.Drawing.Size(76, 20);
            this.textStepDelay.TabIndex = 13;
            this.textStepDelay.Text = "--";
            this.textStepDelay.Leave += new System.EventHandler(this.textStepDelay_Leave);
            // 
            // btnSetPos
            // 
            this.btnSetPos.Enabled = false;
            this.btnSetPos.Location = new System.Drawing.Point(183, 87);
            this.btnSetPos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSetPos.Name = "btnSetPos";
            this.btnSetPos.Size = new System.Drawing.Size(56, 19);
            this.btnSetPos.TabIndex = 15;
            this.btnSetPos.Text = "Set";
            this.btnSetPos.UseVisualStyleBackColor = true;
            this.btnSetPos.Click += new System.EventHandler(this.btnSetPos_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(359, 271);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 50);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // btnGetTemp
            // 
            this.btnGetTemp.Location = new System.Drawing.Point(183, 288);
            this.btnGetTemp.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetTemp.Name = "btnGetTemp";
            this.btnGetTemp.Size = new System.Drawing.Size(56, 19);
            this.btnGetTemp.TabIndex = 19;
            this.btnGetTemp.Text = "Get";
            this.btnGetTemp.UseVisualStyleBackColor = true;
            this.btnGetTemp.Click += new System.EventHandler(this.btnGetTemp_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 290);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Current Temp";
            // 
            // txtTemp
            // 
            this.txtTemp.Enabled = false;
            this.txtTemp.Location = new System.Drawing.Point(96, 288);
            this.txtTemp.Margin = new System.Windows.Forms.Padding(2);
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.Size = new System.Drawing.Size(76, 20);
            this.txtTemp.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 330);
            this.Controls.Add(this.btnGetTemp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTemp);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSetPos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textStepDelay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textMaxPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textMinPos);
            this.Controls.Add(this.textMoveOut);
            this.Controls.Add(this.textMoveIn);
            this.Controls.Add(this.btnMoveOut);
            this.Controls.Add(this.btnMoveIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textCurrentPos);
            this.Controls.Add(this.labelDriverId);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonChoose);
            this.Name = "Form1";
            this.Text = "TEMPLATEDEVICETYPE Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelDriverId;
        private System.Windows.Forms.TextBox textCurrentPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMoveIn;
        private System.Windows.Forms.Button btnMoveOut;
        private System.Windows.Forms.TextBox textMoveIn;
        private System.Windows.Forms.TextBox textMoveOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textMinPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textMaxPos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textStepDelay;
        private System.Windows.Forms.Button btnSetPos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnGetTemp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTemp;
    }
}

