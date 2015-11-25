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
            this.UI_SaveButton = new System.Windows.Forms.Button();
            this.UI_AutomaticUpdates = new System.Windows.Forms.CheckBox();
            this.UI_DataSourceButton = new System.Windows.Forms.Button();
            this.UI_Info_CurrVersion = new System.Windows.Forms.Label();
            this.UI_Info_DataSource = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UI_SaveButton
            // 
            this.UI_SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_SaveButton.Location = new System.Drawing.Point(468, 335);
            this.UI_SaveButton.Name = "UI_SaveButton";
            this.UI_SaveButton.Size = new System.Drawing.Size(84, 23);
            this.UI_SaveButton.TabIndex = 0;
            this.UI_SaveButton.Text = "Done";
            this.UI_SaveButton.UseVisualStyleBackColor = true;
            this.UI_SaveButton.Click += new System.EventHandler(this.UI_SaveButton_Click);
            // 
            // UI_AutomaticUpdates
            // 
            this.UI_AutomaticUpdates.AutoSize = true;
            this.UI_AutomaticUpdates.Location = new System.Drawing.Point(12, 91);
            this.UI_AutomaticUpdates.Name = "UI_AutomaticUpdates";
            this.UI_AutomaticUpdates.Size = new System.Drawing.Size(411, 17);
            this.UI_AutomaticUpdates.TabIndex = 1;
            this.UI_AutomaticUpdates.Text = "Automatically check for and install updates for W3DT on launch. (Recommended)";
            this.UI_AutomaticUpdates.UseVisualStyleBackColor = true;
            this.UI_AutomaticUpdates.CheckedChanged += new System.EventHandler(this.UI_AutomaticUpdates_CheckedChanged);
            // 
            // UI_DataSourceButton
            // 
            this.UI_DataSourceButton.Location = new System.Drawing.Point(12, 13);
            this.UI_DataSourceButton.Name = "UI_DataSourceButton";
            this.UI_DataSourceButton.Size = new System.Drawing.Size(161, 23);
            this.UI_DataSourceButton.TabIndex = 2;
            this.UI_DataSourceButton.Text = "Change Data Source";
            this.UI_DataSourceButton.UseVisualStyleBackColor = true;
            this.UI_DataSourceButton.Click += new System.EventHandler(this.UI_DataSourceButton_Click);
            // 
            // UI_Info_CurrVersion
            // 
            this.UI_Info_CurrVersion.AutoSize = true;
            this.UI_Info_CurrVersion.Location = new System.Drawing.Point(12, 49);
            this.UI_Info_CurrVersion.Name = "UI_Info_CurrVersion";
            this.UI_Info_CurrVersion.Size = new System.Drawing.Size(164, 13);
            this.UI_Info_CurrVersion.TabIndex = 3;
            this.UI_Info_CurrVersion.Text = "Current Version: PLACEHOLDER";
            // 
            // UI_Info_DataSource
            // 
            this.UI_Info_DataSource.AutoSize = true;
            this.UI_Info_DataSource.Location = new System.Drawing.Point(12, 65);
            this.UI_Info_DataSource.Name = "UI_Info_DataSource";
            this.UI_Info_DataSource.Size = new System.Drawing.Size(152, 13);
            this.UI_Info_DataSource.TabIndex = 3;
            this.UI_Info_DataSource.Text = "Data Source: PLACEHOLDER";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 370);
            this.Controls.Add(this.UI_Info_DataSource);
            this.Controls.Add(this.UI_Info_CurrVersion);
            this.Controls.Add(this.UI_DataSourceButton);
            this.Controls.Add(this.UI_AutomaticUpdates);
            this.Controls.Add(this.UI_SaveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UI_SaveButton;
        private System.Windows.Forms.CheckBox UI_AutomaticUpdates;
        private System.Windows.Forms.Button UI_DataSourceButton;
        private System.Windows.Forms.Label UI_Info_CurrVersion;
        private System.Windows.Forms.Label UI_Info_DataSource;
    }
}