namespace TextStream
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            cmbMode = new ComboBox();
            label1 = new Label();
            txtServerIP = new TextBox();
            label3 = new Label();
            btnStart = new Button();
            txtStream = new RichTextBox();
            panel1 = new Panel();
            label6 = new Label();
            label2 = new Label();
            button1 = new Button();
            lblCurrentFile = new Label();
            label5 = new Label();
            txtPort = new TextBox();
            label4 = new Label();
            btnSave = new Button();
            btnClear = new Button();
            chkAllowClientSend = new CheckBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cmbMode
            // 
            cmbMode.BackColor = Color.White;
            cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMode.Font = new Font("Consolas", 9F);
            cmbMode.FormattingEnabled = true;
            cmbMode.Location = new Point(13, 107);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(130, 22);
            cmbMode.TabIndex = 0;
            cmbMode.SelectedIndexChanged += cmbMode_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 85);
            label1.Name = "label1";
            label1.Size = new Size(105, 14);
            label1.TabIndex = 1;
            label1.Text = "Server/Client:";
            // 
            // txtServerIP
            // 
            txtServerIP.BorderStyle = BorderStyle.FixedSingle;
            txtServerIP.Font = new Font("Consolas", 9F);
            txtServerIP.Location = new Point(171, 108);
            txtServerIP.Name = "txtServerIP";
            txtServerIP.Size = new Size(140, 22);
            txtServerIP.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(171, 85);
            label3.Name = "label3";
            label3.Size = new Size(84, 14);
            label3.TabIndex = 4;
            label3.Text = "IP Address:";
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.White;
            btnStart.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStart.ForeColor = Color.Green;
            btnStart.Location = new Point(452, 108);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(129, 46);
            btnStart.TabIndex = 5;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // txtStream
            // 
            txtStream.Font = new Font("Consolas", 9F);
            txtStream.Location = new Point(11, 163);
            txtStream.Name = "txtStream";
            txtStream.Size = new Size(570, 286);
            txtStream.TabIndex = 6;
            txtStream.Text = "";
            txtStream.TextChanged += txtStream_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(-2, -2);
            panel1.Name = "panel1";
            panel1.Size = new Size(597, 75);
            panel1.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(20, 44);
            label6.Name = "label6";
            label6.Size = new Size(126, 19);
            label6.TabIndex = 13;
            label6.Text = "By: Kayleskii";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 192, 0);
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(197, 37);
            label2.TabIndex = 12;
            label2.Text = "TextStream";
            // 
            // button1
            // 
            button1.BackColor = Color.White;
            button1.Font = new Font("Consolas", 9F);
            button1.Location = new Point(452, 504);
            button1.Name = "button1";
            button1.Size = new Size(129, 35);
            button1.TabIndex = 12;
            button1.Text = "Choose File";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // lblCurrentFile
            // 
            lblCurrentFile.AutoSize = true;
            lblCurrentFile.Font = new Font("Consolas", 9F);
            lblCurrentFile.Location = new Point(13, 524);
            lblCurrentFile.Name = "lblCurrentFile";
            lblCurrentFile.Size = new Size(175, 14);
            lblCurrentFile.TabIndex = 14;
            lblCurrentFile.Text = "No file streaming yet...";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(330, 85);
            label5.Name = "label5";
            label5.Size = new Size(42, 14);
            label5.TabIndex = 16;
            label5.Text = "Port:";
            // 
            // txtPort
            // 
            txtPort.BorderStyle = BorderStyle.FixedSingle;
            txtPort.Font = new Font("Consolas", 9F);
            txtPort.Location = new Point(330, 107);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(88, 22);
            txtPort.TabIndex = 17;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(13, 504);
            label4.Name = "label4";
            label4.Size = new Size(182, 14);
            label4.TabIndex = 18;
            label4.Text = "You can also Stream Files";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.White;
            btnSave.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(452, 455);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(129, 37);
            btnSave.TabIndex = 19;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClear.Location = new Point(11, 455);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(129, 37);
            btnClear.TabIndex = 20;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // chkAllowClientSend
            // 
            chkAllowClientSend.AutoSize = true;
            chkAllowClientSend.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkAllowClientSend.Location = new Point(13, 135);
            chkAllowClientSend.Name = "chkAllowClientSend";
            chkAllowClientSend.Size = new Size(166, 18);
            chkAllowClientSend.TabIndex = 21;
            chkAllowClientSend.Text = "Allow Clients Stream";
            chkAllowClientSend.UseVisualStyleBackColor = true;
            chkAllowClientSend.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(594, 550);
            Controls.Add(chkAllowClientSend);
            Controls.Add(btnClear);
            Controls.Add(btnSave);
            Controls.Add(label4);
            Controls.Add(txtPort);
            Controls.Add(label5);
            Controls.Add(lblCurrentFile);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(txtStream);
            Controls.Add(btnStart);
            Controls.Add(label3);
            Controls.Add(txtServerIP);
            Controls.Add(label1);
            Controls.Add(cmbMode);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TextStreamer";
            FormClosing += Form1_FormClosing;
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbMode;
        private Label label1;
        private TextBox txtServerIP;
        private Label label3;
        private Button btnStart;
        private RichTextBox txtStream;
        private Panel panel1;
        private Label label2;
        private Label label6;
        private Button button1;
        private Label lblCurrentFile;
        private Label label5;
        private TextBox txtPort;
        private Label label4;
        private Button btnSave;
        private Button btnClear;
        private CheckBox chkAllowClientSend;
    }
}
