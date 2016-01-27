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
            this.UI_ExportButton = new System.Windows.Forms.Button();
            this.UI_SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.UI_DBCTable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.UI_DBCTable)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_FileList
            // 
            this.UI_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FileList.Location = new System.Drawing.Point(12, 12);
            this.UI_FileList.Name = "UI_FileList";
            this.UI_FileList.Size = new System.Drawing.Size(227, 547);
            this.UI_FileList.TabIndex = 0;
            this.UI_FileList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.UI_FileList_AfterSelect);
            // 
            // UI_FilesFound
            // 
            this.UI_FilesFound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FilesFound.AutoSize = true;
            this.UI_FilesFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.UI_FilesFound.Location = new System.Drawing.Point(12, 569);
            this.UI_FilesFound.Name = "UI_FilesFound";
            this.UI_FilesFound.Size = new System.Drawing.Size(303, 25);
            this.UI_FilesFound.TabIndex = 1;
            this.UI_FilesFound.Text = "0 Files Found (Searching: 0%)";
            // 
            // UI_ExportButton
            // 
            this.UI_ExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_ExportButton.Location = new System.Drawing.Point(706, 570);
            this.UI_ExportButton.Name = "UI_ExportButton";
            this.UI_ExportButton.Size = new System.Drawing.Size(114, 23);
            this.UI_ExportButton.TabIndex = 2;
            this.UI_ExportButton.Text = "Export selected file";
            this.UI_ExportButton.UseVisualStyleBackColor = true;
            this.UI_ExportButton.Click += new System.EventHandler(this.UI_ExportButton_Click);
            // 
            // UI_SaveDialog
            // 
            this.UI_SaveDialog.Filter = "Client Database File (*.dbc)|*.dbc";
            // 
            // UI_DBCTable
            // 
            this.UI_DBCTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_DBCTable.BackgroundColor = System.Drawing.Color.White;
            this.UI_DBCTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.UI_DBCTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UI_DBCTable.Location = new System.Drawing.Point(245, 12);
            this.UI_DBCTable.Name = "UI_DBCTable";
            this.UI_DBCTable.ReadOnly = true;
            this.UI_DBCTable.Size = new System.Drawing.Size(575, 547);
            this.UI_DBCTable.TabIndex = 3;
            // 
            // DBCViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 605);
            this.Controls.Add(this.UI_DBCTable);
            this.Controls.Add(this.UI_ExportButton);
            this.Controls.Add(this.UI_FilesFound);
            this.Controls.Add(this.UI_FileList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DBCViewer";
            this.Text = "DBC Viewer - W3DT";
            ((System.ComponentModel.ISupportInitialize)(this.UI_DBCTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView UI_FileList;
        private System.Windows.Forms.Label UI_FilesFound;
        private System.Windows.Forms.Button UI_ExportButton;
        private System.Windows.Forms.SaveFileDialog UI_SaveDialog;
        private System.Windows.Forms.DataGridView UI_DBCTable;
    }
}