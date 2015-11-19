namespace W3DT
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.UI_DiscardButton = new System.Windows.Forms.Button();
            this.UI_SaveButton = new System.Windows.Forms.Button();
            this.UI_AutomaticUpdates = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // UI_DiscardButton
            // 
            this.UI_DiscardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_DiscardButton.Location = new System.Drawing.Point(396, 335);
            this.UI_DiscardButton.Name = "UI_DiscardButton";
            this.UI_DiscardButton.Size = new System.Drawing.Size(156, 23);
            this.UI_DiscardButton.TabIndex = 0;
            this.UI_DiscardButton.Text = "Cancel (Discard Changes)";
            this.UI_DiscardButton.UseVisualStyleBackColor = true;
            this.UI_DiscardButton.Click += new System.EventHandler(this.UI_DiscardButton_Click);
            // 
            // UI_SaveButton
            // 
            this.UI_SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_SaveButton.Location = new System.Drawing.Point(258, 335);
            this.UI_SaveButton.Name = "UI_SaveButton";
            this.UI_SaveButton.Size = new System.Drawing.Size(132, 23);
            this.UI_SaveButton.TabIndex = 0;
            this.UI_SaveButton.Text = "Done (Save Settings)";
            this.UI_SaveButton.UseVisualStyleBackColor = true;
            this.UI_SaveButton.Click += new System.EventHandler(this.UI_SaveButton_Click);
            // 
            // UI_AutomaticUpdates
            // 
            this.UI_AutomaticUpdates.AutoSize = true;
            this.UI_AutomaticUpdates.Location = new System.Drawing.Point(12, 12);
            this.UI_AutomaticUpdates.Name = "UI_AutomaticUpdates";
            this.UI_AutomaticUpdates.Size = new System.Drawing.Size(411, 17);
            this.UI_AutomaticUpdates.TabIndex = 1;
            this.UI_AutomaticUpdates.Text = "Automatically check for and install updates for W3DT on launch. (Recommended)";
            this.UI_AutomaticUpdates.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 370);
            this.Controls.Add(this.UI_AutomaticUpdates);
            this.Controls.Add(this.UI_SaveButton);
            this.Controls.Add(this.UI_DiscardButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UI_DiscardButton;
        private System.Windows.Forms.Button UI_SaveButton;
        private System.Windows.Forms.CheckBox UI_AutomaticUpdates;
    }
}