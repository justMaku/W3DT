namespace W3DT
{
    partial class WMOViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WMOViewer));
            this.openGLControl = new SharpGL.OpenGLControl();
            this.UI_FileList = new System.Windows.Forms.TreeView();
            this.UI_FilterOverlay = new System.Windows.Forms.Label();
            this.UI_FilterField = new System.Windows.Forms.TextBox();
            this.UI_FileCount_Label = new System.Windows.Forms.Label();
            this.UI_FilterTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(333, 12);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(624, 391);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            // 
            // UI_FileList
            // 
            this.UI_FileList.Location = new System.Drawing.Point(12, 12);
            this.UI_FileList.Name = "UI_FileList";
            this.UI_FileList.Size = new System.Drawing.Size(315, 617);
            this.UI_FileList.TabIndex = 1;
            this.UI_FileList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.UI_FileList_AfterSelect);
            // 
            // UI_FilterOverlay
            // 
            this.UI_FilterOverlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FilterOverlay.AutoSize = true;
            this.UI_FilterOverlay.BackColor = System.Drawing.SystemColors.Window;
            this.UI_FilterOverlay.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.UI_FilterOverlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_FilterOverlay.ForeColor = System.Drawing.Color.Gray;
            this.UI_FilterOverlay.Location = new System.Drawing.Point(15, 638);
            this.UI_FilterOverlay.Name = "UI_FilterOverlay";
            this.UI_FilterOverlay.Size = new System.Drawing.Size(135, 25);
            this.UI_FilterOverlay.TabIndex = 6;
            this.UI_FilterOverlay.Text = "Enter Filter...";
            // 
            // UI_FilterField
            // 
            this.UI_FilterField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FilterField.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_FilterField.Location = new System.Drawing.Point(12, 635);
            this.UI_FilterField.Name = "UI_FilterField";
            this.UI_FilterField.Size = new System.Drawing.Size(238, 31);
            this.UI_FilterField.TabIndex = 5;
            // 
            // UI_FileCount_Label
            // 
            this.UI_FileCount_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FileCount_Label.AutoSize = true;
            this.UI_FileCount_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_FileCount_Label.Location = new System.Drawing.Point(256, 638);
            this.UI_FileCount_Label.Name = "UI_FileCount_Label";
            this.UI_FileCount_Label.Size = new System.Drawing.Size(327, 25);
            this.UI_FileCount_Label.TabIndex = 4;
            this.UI_FileCount_Label.Text = "0 Files Found (Searching: 100%)";
            // 
            // UI_FilterTime
            // 
            this.UI_FilterTime.Interval = 1000;
            // 
            // WMOViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 672);
            this.Controls.Add(this.UI_FilterOverlay);
            this.Controls.Add(this.UI_FilterField);
            this.Controls.Add(this.UI_FileCount_Label);
            this.Controls.Add(this.UI_FileList);
            this.Controls.Add(this.openGLControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WMOViewer";
            this.Text = "Warcraft 3D Toolkit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WMOViewer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.TreeView UI_FileList;
        private System.Windows.Forms.Label UI_FilterOverlay;
        private System.Windows.Forms.TextBox UI_FilterField;
        private System.Windows.Forms.Label UI_FileCount_Label;
        private System.Windows.Forms.Timer UI_FilterTime;
    }
}

