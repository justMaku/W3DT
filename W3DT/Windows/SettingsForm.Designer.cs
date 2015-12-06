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
            this.UI_RemoteVersion_Field = new System.Windows.Forms.ComboBox();
            this.UI_RemoteVersion_Label = new System.Windows.Forms.Label();
            this.UI_FileLocale_Field = new System.Windows.Forms.ComboBox();
            this.UI_FileLocale_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UI_SaveButton
            // 
            this.UI_SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_SaveButton.Location = new System.Drawing.Point(360, 155);
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
            // UI_RemoteVersion_Field
            // 
            this.UI_RemoteVersion_Field.FormattingEnabled = true;
            this.UI_RemoteVersion_Field.Location = new System.Drawing.Point(98, 114);
            this.UI_RemoteVersion_Field.Name = "UI_RemoteVersion_Field";
            this.UI_RemoteVersion_Field.Size = new System.Drawing.Size(154, 21);
            this.UI_RemoteVersion_Field.TabIndex = 4;
            this.UI_RemoteVersion_Field.SelectedIndexChanged += new System.EventHandler(this.UI_RemoteVersion_Field_SelectedIndexChanged);
            // 
            // UI_RemoteVersion_Label
            // 
            this.UI_RemoteVersion_Label.AutoSize = true;
            this.UI_RemoteVersion_Label.Location = new System.Drawing.Point(10, 117);
            this.UI_RemoteVersion_Label.Name = "UI_RemoteVersion_Label";
            this.UI_RemoteVersion_Label.Size = new System.Drawing.Size(85, 13);
            this.UI_RemoteVersion_Label.TabIndex = 5;
            this.UI_RemoteVersion_Label.Text = "Remote Version:";
            // 
            // UI_FileLocale_Field
            // 
            this.UI_FileLocale_Field.FormattingEnabled = true;
            this.UI_FileLocale_Field.Location = new System.Drawing.Point(98, 141);
            this.UI_FileLocale_Field.Name = "UI_FileLocale_Field";
            this.UI_FileLocale_Field.Size = new System.Drawing.Size(154, 21);
            this.UI_FileLocale_Field.TabIndex = 4;
            this.UI_FileLocale_Field.SelectedIndexChanged += new System.EventHandler(this.UI_FileLocale_Field_SelectedIndexChanged);
            // 
            // UI_FileLocale_Label
            // 
            this.UI_FileLocale_Label.AutoSize = true;
            this.UI_FileLocale_Label.Location = new System.Drawing.Point(34, 144);
            this.UI_FileLocale_Label.Name = "UI_FileLocale_Label";
            this.UI_FileLocale_Label.Size = new System.Drawing.Size(61, 13);
            this.UI_FileLocale_Label.TabIndex = 5;
            this.UI_FileLocale_Label.Text = "File Locale:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 190);
            this.Controls.Add(this.UI_FileLocale_Label);
            this.Controls.Add(this.UI_RemoteVersion_Label);
            this.Controls.Add(this.UI_FileLocale_Field);
            this.Controls.Add(this.UI_RemoteVersion_Field);
            this.Controls.Add(this.UI_Info_DataSource);
            this.Controls.Add(this.UI_Info_CurrVersion);
            this.Controls.Add(this.UI_DataSourceButton);
            this.Controls.Add(this.UI_AutomaticUpdates);
            this.Controls.Add(this.UI_SaveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(472, 198);
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
        private System.Windows.Forms.ComboBox UI_RemoteVersion_Field;
        private System.Windows.Forms.Label UI_RemoteVersion_Label;
        private System.Windows.Forms.ComboBox UI_FileLocale_Field;
        private System.Windows.Forms.Label UI_FileLocale_Label;
    }
}