namespace W3DT
{
    partial class MapViewerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewerWindow));
            this.UI_FileCount_Label = new System.Windows.Forms.Label();
            this.UI_FileList = new System.Windows.Forms.TreeView();
            this.UI_FilterTimer = new System.Windows.Forms.Timer(this.components);
            this.UI_PreviewStatus = new W3DT.Controls.StaticTextBox();
            this.UI_Map = new W3DT.Controls.DoubleBufferedPanel();
            this.UI_ExportButton = new System.Windows.Forms.Button();
            this.UI_Map.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_FileCount_Label
            // 
            this.UI_FileCount_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FileCount_Label.AutoSize = true;
            this.UI_FileCount_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_FileCount_Label.Location = new System.Drawing.Point(13, 652);
            this.UI_FileCount_Label.Name = "UI_FileCount_Label";
            this.UI_FileCount_Label.Size = new System.Drawing.Size(166, 25);
            this.UI_FileCount_Label.TabIndex = 1;
            this.UI_FileCount_Label.Text = "Loading Maps...";
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
            // UI_FilterTimer
            // 
            this.UI_FilterTimer.Interval = 1000;
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
            this.UI_PreviewStatus.Text = "<- Select map";
            this.UI_PreviewStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UI_Map
            // 
            this.UI_Map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_Map.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UI_Map.Controls.Add(this.UI_ExportButton);
            this.UI_Map.Location = new System.Drawing.Point(477, 12);
            this.UI_Map.Name = "UI_Map";
            this.UI_Map.Size = new System.Drawing.Size(586, 629);
            this.UI_Map.TabIndex = 6;
            this.UI_Map.Paint += new System.Windows.Forms.PaintEventHandler(this.UI_Map_Paint);
            this.UI_Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UI_Map_MouseDown);
            this.UI_Map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UI_Map_MouseMove);
            this.UI_Map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UI_Map_MouseUp);
            // 
            // UI_ExportButton
            // 
            this.UI_ExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_ExportButton.Location = new System.Drawing.Point(462, 597);
            this.UI_ExportButton.Name = "UI_ExportButton";
            this.UI_ExportButton.Size = new System.Drawing.Size(115, 23);
            this.UI_ExportButton.TabIndex = 0;
            this.UI_ExportButton.Text = "Export Map Terrain";
            this.UI_ExportButton.UseVisualStyleBackColor = true;
            this.UI_ExportButton.Visible = false;
            this.UI_ExportButton.Click += new System.EventHandler(this.UI_ExportButton_Click);
            // 
            // MapViewerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 688);
            this.Controls.Add(this.UI_PreviewStatus);
            this.Controls.Add(this.UI_Map);
            this.Controls.Add(this.UI_FileList);
            this.Controls.Add(this.UI_FileCount_Label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(900, 421);
            this.Name = "MapViewerWindow";
            this.Text = "Map Viewer - W3DT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapViewerWindow_FormClosing);
            this.UI_Map.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_FileCount_Label;
        private System.Windows.Forms.TreeView UI_FileList;
        private W3DT.Controls.DoubleBufferedPanel UI_Map;
        private Controls.StaticTextBox UI_PreviewStatus;
        private System.Windows.Forms.Button UI_ExportButton;
        private System.Windows.Forms.Timer UI_FilterTimer;
    }
}