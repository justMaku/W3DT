namespace W3DT
{
    partial class ArtExplorerWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArtExplorerWindow));
            this.UI_FileCount_Label = new System.Windows.Forms.Label();
            this.UI_FilterField = new System.Windows.Forms.TextBox();
            this.UI_FilterOverlay = new System.Windows.Forms.Label();
            this.UI_FileList = new System.Windows.Forms.TreeView();
            this.UI_ImagePreview = new System.Windows.Forms.Panel();
            this.UI_ExportButton = new System.Windows.Forms.Button();
            this.UI_AutoLoadPreview_Field = new System.Windows.Forms.CheckBox();
            this.UI_DialogSave = new System.Windows.Forms.SaveFileDialog();
            this.UI_PreviewStatus = new W3DT.Controls.StaticTextBox();
            this.UI_FilterTimer = new System.Windows.Forms.Timer(this.components);
            this.UI_ImagePreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_FileCount_Label
            // 
            this.UI_FileCount_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FileCount_Label.AutoSize = true;
            this.UI_FileCount_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_FileCount_Label.Location = new System.Drawing.Point(256, 650);
            this.UI_FileCount_Label.Name = "UI_FileCount_Label";
            this.UI_FileCount_Label.Size = new System.Drawing.Size(327, 25);
            this.UI_FileCount_Label.TabIndex = 1;
            this.UI_FileCount_Label.Text = "0 Files Found (Searching: 100%)";
            // 
            // UI_FilterField
            // 
            this.UI_FilterField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FilterField.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_FilterField.Location = new System.Drawing.Point(12, 647);
            this.UI_FilterField.Name = "UI_FilterField";
            this.UI_FilterField.Size = new System.Drawing.Size(238, 31);
            this.UI_FilterField.TabIndex = 2;
            // 
            // UI_FilterOverlay
            // 
            this.UI_FilterOverlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FilterOverlay.AutoSize = true;
            this.UI_FilterOverlay.BackColor = System.Drawing.SystemColors.Window;
            this.UI_FilterOverlay.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.UI_FilterOverlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_FilterOverlay.ForeColor = System.Drawing.Color.Gray;
            this.UI_FilterOverlay.Location = new System.Drawing.Point(15, 650);
            this.UI_FilterOverlay.Name = "UI_FilterOverlay";
            this.UI_FilterOverlay.Size = new System.Drawing.Size(135, 25);
            this.UI_FilterOverlay.TabIndex = 3;
            this.UI_FilterOverlay.Text = "Enter Filter...";
            // 
            // UI_FileList
            // 
            this.UI_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FileList.Location = new System.Drawing.Point(12, 12);
            this.UI_FileList.Name = "UI_FileList";
            this.UI_FileList.Size = new System.Drawing.Size(459, 629);
            this.UI_FileList.TabIndex = 5;
            this.UI_FileList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.UI_FileList_AfterSelect);
            // 
            // UI_ImagePreview
            // 
            this.UI_ImagePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_ImagePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UI_ImagePreview.Controls.Add(this.UI_ExportButton);
            this.UI_ImagePreview.Location = new System.Drawing.Point(477, 12);
            this.UI_ImagePreview.Name = "UI_ImagePreview";
            this.UI_ImagePreview.Size = new System.Drawing.Size(586, 629);
            this.UI_ImagePreview.TabIndex = 6;
            // 
            // UI_ExportButton
            // 
            this.UI_ExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_ExportButton.Location = new System.Drawing.Point(478, 597);
            this.UI_ExportButton.Name = "UI_ExportButton";
            this.UI_ExportButton.Size = new System.Drawing.Size(99, 23);
            this.UI_ExportButton.TabIndex = 0;
            this.UI_ExportButton.Text = "Export Image";
            this.UI_ExportButton.UseVisualStyleBackColor = true;
            this.UI_ExportButton.Visible = false;
            this.UI_ExportButton.Click += new System.EventHandler(this.UI_ExportButton_Click);
            // 
            // UI_AutoLoadPreview_Field
            // 
            this.UI_AutoLoadPreview_Field.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_AutoLoadPreview_Field.AutoSize = true;
            this.UI_AutoLoadPreview_Field.Location = new System.Drawing.Point(876, 661);
            this.UI_AutoLoadPreview_Field.Name = "UI_AutoLoadPreview_Field";
            this.UI_AutoLoadPreview_Field.Size = new System.Drawing.Size(187, 17);
            this.UI_AutoLoadPreview_Field.TabIndex = 8;
            this.UI_AutoLoadPreview_Field.Text = "Automatically load image previews";
            this.UI_AutoLoadPreview_Field.UseVisualStyleBackColor = true;
            this.UI_AutoLoadPreview_Field.CheckedChanged += new System.EventHandler(this.UI_AutoLoadPreview_Field_CheckedChanged);
            // 
            // UI_DialogSave
            // 
            this.UI_DialogSave.Filter = "Bitmap Image (*.bmp)|*.bmp|Portable Networks Graphics (*.png)|*.png|JPEG Image (." +
    "jpg)|*.jpg";
            // 
            // UI_PreviewStatus
            // 
            this.UI_PreviewStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_PreviewStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UI_PreviewStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.UI_PreviewStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_PreviewStatus.ForeColor = System.Drawing.Color.Black;
            this.UI_PreviewStatus.Location = new System.Drawing.Point(491, 288);
            this.UI_PreviewStatus.MinimumSize = new System.Drawing.Size(0, 36);
            this.UI_PreviewStatus.Multiline = true;
            this.UI_PreviewStatus.Name = "UI_PreviewStatus";
            this.UI_PreviewStatus.ReadOnly = true;
            this.UI_PreviewStatus.Size = new System.Drawing.Size(561, 36);
            this.UI_PreviewStatus.TabIndex = 7;
            this.UI_PreviewStatus.TabStop = false;
            this.UI_PreviewStatus.Text = "Click to load preview...";
            this.UI_PreviewStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UI_PreviewStatus.Click += new System.EventHandler(this.UI_PreviewStatus_Click);
            // 
            // UI_FilterTimer
            // 
            this.UI_FilterTimer.Interval = 1000;
            // 
            // ArtExplorerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 688);
            this.Controls.Add(this.UI_AutoLoadPreview_Field);
            this.Controls.Add(this.UI_PreviewStatus);
            this.Controls.Add(this.UI_ImagePreview);
            this.Controls.Add(this.UI_FileList);
            this.Controls.Add(this.UI_FilterOverlay);
            this.Controls.Add(this.UI_FilterField);
            this.Controls.Add(this.UI_FileCount_Label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(900, 421);
            this.Name = "ArtExplorerWindow";
            this.Text = "Artwork Explorer - W3DT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArtExplorerWindow_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.ArtExplorerWindow_ResizeEnd);
            this.UI_ImagePreview.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_FileCount_Label;
        private System.Windows.Forms.TextBox UI_FilterField;
        private System.Windows.Forms.Label UI_FilterOverlay;
        private System.Windows.Forms.TreeView UI_FileList;
        private System.Windows.Forms.Panel UI_ImagePreview;
        private Controls.StaticTextBox UI_PreviewStatus;
        private System.Windows.Forms.CheckBox UI_AutoLoadPreview_Field;
        private System.Windows.Forms.Button UI_ExportButton;
        private System.Windows.Forms.SaveFileDialog UI_DialogSave;
        private System.Windows.Forms.Timer UI_FilterTimer;
    }
}