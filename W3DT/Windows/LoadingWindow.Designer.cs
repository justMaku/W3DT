namespace W3DT
{
    partial class LoadingWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.UI_TextLine1 = new System.Windows.Forms.Label();
            this.UI_TextLine2 = new System.Windows.Forms.Label();
            this.UI_CancelButton = new System.Windows.Forms.Button();
            this.LoadingEye = new W3DT.Controls.LoadingEye();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(89, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please Wait...";
            // 
            // UI_TextLine1
            // 
            this.UI_TextLine1.AutoSize = true;
            this.UI_TextLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.UI_TextLine1.Location = new System.Drawing.Point(91, 37);
            this.UI_TextLine1.Name = "UI_TextLine1";
            this.UI_TextLine1.Size = new System.Drawing.Size(54, 13);
            this.UI_TextLine1.TabIndex = 1;
            this.UI_TextLine1.Text = "TextLine1";
            // 
            // UI_TextLine2
            // 
            this.UI_TextLine2.AutoSize = true;
            this.UI_TextLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_TextLine2.Location = new System.Drawing.Point(89, 53);
            this.UI_TextLine2.Name = "UI_TextLine2";
            this.UI_TextLine2.Size = new System.Drawing.Size(54, 13);
            this.UI_TextLine2.TabIndex = 1;
            this.UI_TextLine2.Text = "TextLine2";
            // 
            // UI_CancelButton
            // 
            this.UI_CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_CancelButton.Location = new System.Drawing.Point(381, 57);
            this.UI_CancelButton.Name = "UI_CancelButton";
            this.UI_CancelButton.Size = new System.Drawing.Size(60, 23);
            this.UI_CancelButton.TabIndex = 2;
            this.UI_CancelButton.Text = "Cancel";
            this.UI_CancelButton.UseVisualStyleBackColor = true;
            this.UI_CancelButton.Click += new System.EventHandler(this.UI_CancelButton_Click);
            // 
            // LoadingEye
            // 
            this.LoadingEye.Location = new System.Drawing.Point(12, 12);
            this.LoadingEye.Name = "LoadingEye";
            this.LoadingEye.Size = new System.Drawing.Size(64, 64);
            this.LoadingEye.TabIndex = 0;
            // 
            // LoadingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 89);
            this.ControlBox = false;
            this.Controls.Add(this.UI_CancelButton);
            this.Controls.Add(this.UI_TextLine2);
            this.Controls.Add(this.UI_TextLine1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoadingEye);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(468, 127);
            this.MinimumSize = new System.Drawing.Size(468, 127);
            this.Name = "LoadingWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Important stuff is being done, hold on...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private W3DT.Controls.LoadingEye LoadingEye;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label UI_TextLine1;
        private System.Windows.Forms.Label UI_TextLine2;
        private System.Windows.Forms.Button UI_CancelButton;

    }
}