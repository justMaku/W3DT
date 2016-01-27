namespace W3DT
{
    partial class MusicExplorerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicExplorerWindow));
            this.UI_FileCount_Label = new System.Windows.Forms.Label();
            this.UI_FilterField = new System.Windows.Forms.TextBox();
            this.UI_FilterCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.UI_FilterOverlay = new System.Windows.Forms.Label();
            this.UI_MultiWindows_Field = new System.Windows.Forms.CheckBox();
            this.UI_FileList = new System.Windows.Forms.TreeView();
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
            // UI_FilterCheckTimer
            // 
            this.UI_FilterCheckTimer.Enabled = true;
            this.UI_FilterCheckTimer.Interval = 1000;
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
            // UI_MultiWindows_Field
            // 
            this.UI_MultiWindows_Field.AutoSize = true;
            this.UI_MultiWindows_Field.Location = new System.Drawing.Point(723, 661);
            this.UI_MultiWindows_Field.Name = "UI_MultiWindows_Field";
            this.UI_MultiWindows_Field.Size = new System.Drawing.Size(223, 17);
            this.UI_MultiWindows_Field.TabIndex = 4;
            this.UI_MultiWindows_Field.Text = "Allow multiple sound players open at once";
            this.UI_MultiWindows_Field.UseVisualStyleBackColor = true;
            this.UI_MultiWindows_Field.CheckedChanged += new System.EventHandler(this.UI_MultiWindows_Field_CheckedChanged);
            // 
            // UI_FileList
            // 
            this.UI_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_FileList.Location = new System.Drawing.Point(12, 12);
            this.UI_FileList.Name = "UI_FileList";
            this.UI_FileList.Size = new System.Drawing.Size(934, 629);
            this.UI_FileList.TabIndex = 5;
            this.UI_FileList.DoubleClick += new System.EventHandler(this.UI_FileList_DoubleClick);
            // 
            // MusicExplorerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 688);
            this.Controls.Add(this.UI_FileList);
            this.Controls.Add(this.UI_MultiWindows_Field);
            this.Controls.Add(this.UI_FilterOverlay);
            this.Controls.Add(this.UI_FilterField);
            this.Controls.Add(this.UI_FileCount_Label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MusicExplorerWindow";
            this.Text = "Music/Sound Explorer - W3DT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MusicExplorerWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_FileCount_Label;
        private System.Windows.Forms.TextBox UI_FilterField;
        private System.Windows.Forms.Timer UI_FilterCheckTimer;
        private System.Windows.Forms.Label UI_FilterOverlay;
        private System.Windows.Forms.CheckBox UI_MultiWindows_Field;
        private System.Windows.Forms.TreeView UI_FileList;
    }
}