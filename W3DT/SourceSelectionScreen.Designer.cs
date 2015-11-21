namespace W3DT
{
    partial class SourceSelectionScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceSelectionScreen));
            this.UI_DataSourceTitle = new System.Windows.Forms.Label();
            this.UI_DataSourceInfo = new W3DT.Controls.StaticTextBox();
            this.UI_SourceOption_Local = new System.Windows.Forms.RadioButton();
            this.UI_SourceOptions = new System.Windows.Forms.Panel();
            this.UI_SourceOption_Remote = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.UI_SourceOptions_Done = new System.Windows.Forms.Button();
            this.UI_SourceOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_DataSourceTitle
            // 
            this.UI_DataSourceTitle.AutoSize = true;
            this.UI_DataSourceTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_DataSourceTitle.Location = new System.Drawing.Point(12, 9);
            this.UI_DataSourceTitle.Name = "UI_DataSourceTitle";
            this.UI_DataSourceTitle.Size = new System.Drawing.Size(218, 25);
            this.UI_DataSourceTitle.TabIndex = 0;
            this.UI_DataSourceTitle.Text = "Set-up Data Source";
            // 
            // UI_DataSourceInfo
            // 
            this.UI_DataSourceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_DataSourceInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UI_DataSourceInfo.Location = new System.Drawing.Point(17, 42);
            this.UI_DataSourceInfo.Multiline = true;
            this.UI_DataSourceInfo.Name = "UI_DataSourceInfo";
            this.UI_DataSourceInfo.ReadOnly = true;
            this.UI_DataSourceInfo.Size = new System.Drawing.Size(596, 30);
            this.UI_DataSourceInfo.TabIndex = 1;
            this.UI_DataSourceInfo.Text = "Before the magic can begin, you need to choose where you would like W3DT to sourc" +
    "e data from. The two available options are outlined below.";
            // 
            // UI_SourceOption_Local
            // 
            this.UI_SourceOption_Local.AutoSize = true;
            this.UI_SourceOption_Local.Checked = true;
            this.UI_SourceOption_Local.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_SourceOption_Local.Location = new System.Drawing.Point(3, 3);
            this.UI_SourceOption_Local.Name = "UI_SourceOption_Local";
            this.UI_SourceOption_Local.Size = new System.Drawing.Size(447, 24);
            this.UI_SourceOption_Local.TabIndex = 2;
            this.UI_SourceOption_Local.TabStop = true;
            this.UI_SourceOption_Local.Text = "Local World of Warcraft Installation (Recommended)";
            this.UI_SourceOption_Local.UseVisualStyleBackColor = true;
            // 
            // UI_SourceOptions
            // 
            this.UI_SourceOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_SourceOptions.Controls.Add(this.label15);
            this.UI_SourceOptions.Controls.Add(this.label12);
            this.UI_SourceOptions.Controls.Add(this.label5);
            this.UI_SourceOptions.Controls.Add(this.label11);
            this.UI_SourceOptions.Controls.Add(this.label4);
            this.UI_SourceOptions.Controls.Add(this.label14);
            this.UI_SourceOptions.Controls.Add(this.label13);
            this.UI_SourceOptions.Controls.Add(this.label10);
            this.UI_SourceOptions.Controls.Add(this.label6);
            this.UI_SourceOptions.Controls.Add(this.label9);
            this.UI_SourceOptions.Controls.Add(this.label2);
            this.UI_SourceOptions.Controls.Add(this.label8);
            this.UI_SourceOptions.Controls.Add(this.label3);
            this.UI_SourceOptions.Controls.Add(this.label7);
            this.UI_SourceOptions.Controls.Add(this.label1);
            this.UI_SourceOptions.Controls.Add(this.UI_SourceOption_Remote);
            this.UI_SourceOptions.Controls.Add(this.UI_SourceOption_Local);
            this.UI_SourceOptions.Location = new System.Drawing.Point(27, 80);
            this.UI_SourceOptions.Name = "UI_SourceOptions";
            this.UI_SourceOptions.Size = new System.Drawing.Size(574, 227);
            this.UI_SourceOptions.TabIndex = 3;
            // 
            // UI_SourceOption_Remote
            // 
            this.UI_SourceOption_Remote.AutoSize = true;
            this.UI_SourceOption_Remote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_SourceOption_Remote.Location = new System.Drawing.Point(3, 91);
            this.UI_SourceOption_Remote.Name = "UI_SourceOption_Remote";
            this.UI_SourceOption_Remote.Size = new System.Drawing.Size(267, 24);
            this.UI_SourceOption_Remote.TabIndex = 2;
            this.UI_SourceOption_Remote.Text = "Remote Blizzard CDN Servers";
            this.UI_SourceOption_Remote.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pros";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(23, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Generally faster";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(219, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cons";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(219, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Requires World of Warcraft installation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(219, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Cannot be used while WoW is running";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Green;
            this.label6.Location = new System.Drawing.Point(23, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Does not require internet connection";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Pros";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(219, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Cons";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Green;
            this.label9.Location = new System.Drawing.Point(23, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Less likely to be corrupt";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Green;
            this.label10.Location = new System.Drawing.Point(23, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(173, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Can be used while WoW is running";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(219, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(241, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Requires internet (impacted by connection speed)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(219, 148);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Likely to be slower";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Green;
            this.label13.Location = new System.Drawing.Point(23, 163);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(168, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Does not require WoW installation";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Green;
            this.label14.Location = new System.Drawing.Point(23, 178);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Always up-to-date";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 207);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(314, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "NOTE: You can change this at any time within the settings panel.";
            // 
            // UI_SourceOptions_Done
            // 
            this.UI_SourceOptions_Done.Location = new System.Drawing.Point(494, 317);
            this.UI_SourceOptions_Done.Name = "UI_SourceOptions_Done";
            this.UI_SourceOptions_Done.Size = new System.Drawing.Size(130, 23);
            this.UI_SourceOptions_Done.TabIndex = 4;
            this.UI_SourceOptions_Done.Text = "Continue";
            this.UI_SourceOptions_Done.UseVisualStyleBackColor = true;
            this.UI_SourceOptions_Done.Click += new System.EventHandler(this.UI_SourceOptions_Done_Click);
            // 
            // SourceSelectionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 352);
            this.Controls.Add(this.UI_SourceOptions_Done);
            this.Controls.Add(this.UI_SourceOptions);
            this.Controls.Add(this.UI_DataSourceInfo);
            this.Controls.Add(this.UI_DataSourceTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SourceSelectionScreen";
            this.Text = "Select Data Source...";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SourceSelectionScreen_FormClosed);
            this.UI_SourceOptions.ResumeLayout(false);
            this.UI_SourceOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_DataSourceTitle;
        private W3DT.Controls.StaticTextBox UI_DataSourceInfo;
        private System.Windows.Forms.RadioButton UI_SourceOption_Local;
        private System.Windows.Forms.Panel UI_SourceOptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton UI_SourceOption_Remote;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button UI_SourceOptions_Done;
    }
}