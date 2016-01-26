namespace W3DT
{
    partial class DBCViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBCViewer));
            this.UI_FileList = new System.Windows.Forms.TreeView();
            this.UI_FilesFound = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UI_FileList
            // 
            this.UI_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FileList.Location = new System.Drawing.Point(12, 12);
            this.UI_FileList.Name = "UI_FileList";
            this.UI_FileList.Size = new System.Drawing.Size(195, 547);
            this.UI_FileList.TabIndex = 0;
            // 
            // UI_FilesFound
            // 
            this.UI_FilesFound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FilesFound.AutoSize = true;
            this.UI_FilesFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.UI_FilesFound.Location = new System.Drawing.Point(12, 569);
            this.UI_FilesFound.Name = "UI_FilesFound";
            this.UI_FilesFound.Size = new System.Drawing.Size(303, 25);
            this.UI_FilesFound.TabIndex = 1;
            this.UI_FilesFound.Text = "0 Files Found (Searching: 0%)";
            // 
            // DBCViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 605);
            this.Controls.Add(this.UI_FilesFound);
            this.Controls.Add(this.UI_FileList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DBCViewer";
            this.Text = "DBC Viewer - W3DT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView UI_FileList;
        private System.Windows.Forms.Label UI_FilesFound;
    }
}