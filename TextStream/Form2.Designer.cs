namespace TextStream
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 67);
            label1.Name = "label1";
            label1.Size = new Size(154, 14);
            label1.TabIndex = 0;
            label1.Text = "Developer: @Kayleskii";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 94);
            label2.Name = "label2";
            label2.Size = new Size(63, 14);
            label2.TabIndex = 1;
            label2.Text = "Socials:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("UniDreamLED", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(12, 19);
            label3.Name = "label3";
            label3.Size = new Size(140, 36);
            label3.TabIndex = 2;
            label3.Text = "KTPS2025";
            // 
            // button1
            // 
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 192);
            button1.FlatAppearance.BorderSize = 2;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("UniDreamLED", 9F, FontStyle.Bold);
            button1.Location = new Point(28, 124);
            button1.Name = "button1";
            button1.Size = new Size(72, 23);
            button1.TabIndex = 3;
            button1.Text = "Facebook";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.FlatAppearance.BorderColor = Color.FromArgb(192, 0, 192);
            button2.FlatAppearance.BorderSize = 2;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("UniDreamLED", 9F, FontStyle.Bold);
            button2.Location = new Point(121, 124);
            button2.Name = "button2";
            button2.Size = new Size(70, 23);
            button2.TabIndex = 4;
            button2.Text = "GitHub";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            button3.FlatAppearance.BorderSize = 2;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("UniDreamLED", 9F, FontStyle.Bold);
            button3.Location = new Point(217, 124);
            button3.Name = "button3";
            button3.Size = new Size(74, 23);
            button3.TabIndex = 5;
            button3.Text = "Linkedln";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(89, 174);
            label4.Name = "label4";
            label4.Size = new Size(175, 14);
            label4.TabIndex = 6;
            label4.Text = "All Rights Reserved 2025";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(257, 9);
            label5.Name = "label5";
            label5.Size = new Size(98, 14);
            label5.TabIndex = 7;
            label5.Text = "TextStream v1";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(367, 197);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ABOUT ME";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label4;
        private Label label5;
    }
}