namespace W3DT
{
    partial class ModelViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelViewer));
            this.UI_3DView = new SharpGL.OpenGLControl();
            this.UI_FilterField = new System.Windows.Forms.TextBox();
            this.UI_FileCount_Label = new System.Windows.Forms.Label();
            this.UI_FileList = new System.Windows.Forms.TreeView();
            this.UI_FilterOverlay = new System.Windows.Forms.Label();
            this.UI_FilterTime = new System.Windows.Forms.Timer(this.components);
            this.UI_ColourDialog = new System.Windows.Forms.ColorDialog();
            this.UI_ColourChangeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UI_3DView)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_3DView
            // 
            this.UI_3DView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_3DView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UI_3DView.DrawFPS = false;
            this.UI_3DView.Location = new System.Drawing.Point(261, 12);
            this.UI_3DView.Name = "UI_3DView";
            this.UI_3DView.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.UI_3DView.RenderContextType = SharpGL.RenderContextType.FBO;
            this.UI_3DView.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.UI_3DView.Size = new System.Drawing.Size(686, 475);
            this.UI_3DView.TabIndex = 1;
            this.UI_3DView.OpenGLInitialized += new System.EventHandler(this.UI_3DView_OpenGLInitialized);
            this.UI_3DView.OpenGLDraw += new SharpGL.RenderEventHandler(this.UI_3DView_OpenGLDraw);
            this.UI_3DView.Resized += new System.EventHandler(this.UI_3DView_Resized);
            this.UI_3DView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UI_3DView_MouseDown);
            this.UI_3DView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UI_3DView_MouseMove);
            this.UI_3DView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UI_3DView_MouseUp);
            this.UI_3DView.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.UI_3DView_MouseWheel);
            // 
            // UI_FilterField
            // 
            this.UI_FilterField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FilterField.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_FilterField.Location = new System.Drawing.Point(12, 597);
            this.UI_FilterField.Name = "UI_FilterField";
            this.UI_FilterField.Size = new System.Drawing.Size(238, 31);
            this.UI_FilterField.TabIndex = 8;
            // 
            // UI_FileCount_Label
            // 
            this.UI_FileCount_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FileCount_Label.AutoSize = true;
            this.UI_FileCount_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_FileCount_Label.Location = new System.Drawing.Point(256, 600);
            this.UI_FileCount_Label.Name = "UI_FileCount_Label";
            this.UI_FileCount_Label.Size = new System.Drawing.Size(327, 25);
            this.UI_FileCount_Label.TabIndex = 7;
            this.UI_FileCount_Label.Text = "0 Files Found (Searching: 100%)";
            // 
            // UI_FileList
            // 
            this.UI_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FileList.Location = new System.Drawing.Point(12, 12);
            this.UI_FileList.Name = "UI_FileList";
            this.UI_FileList.Size = new System.Drawing.Size(238, 579);
            this.UI_FileList.TabIndex = 6;
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
            this.UI_FilterOverlay.Location = new System.Drawing.Point(12, 600);
            this.UI_FilterOverlay.Name = "UI_FilterOverlay";
            this.UI_FilterOverlay.Size = new System.Drawing.Size(135, 25);
            this.UI_FilterOverlay.TabIndex = 9;
            this.UI_FilterOverlay.Text = "Enter Filter...";
            // 
            // UI_FilterTime
            // 
            this.UI_FilterTime.Interval = 1000;
            // 
            // UI_ColourDialog
            // 
            this.UI_ColourDialog.FullOpen = true;
            // 
            // UI_ColourChangeButton
            // 
            this.UI_ColourChangeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_ColourChangeButton.Image = global::W3DT.Properties.Resources.paint;
            this.UI_ColourChangeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UI_ColourChangeButton.Location = new System.Drawing.Point(889, 460);
            this.UI_ColourChangeButton.Name = "UI_ColourChangeButton";
            this.UI_ColourChangeButton.Size = new System.Drawing.Size(81, 23);
            this.UI_ColourChangeButton.TabIndex = 10;
            this.UI_ColourChangeButton.UseVisualStyleBackColor = true;
            this.UI_ColourChangeButton.Click += new System.EventHandler(this.UI_ColourChangeButton_Click);
            // 
            // ModelViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 643);
            this.Controls.Add(this.UI_FilterOverlay);
            this.Controls.Add(this.UI_FilterField);
            this.Controls.Add(this.UI_FileCount_Label);
            this.Controls.Add(this.UI_FileList);
            this.Controls.Add(this.UI_3DView);
            this.Controls.Add(this.UI_ColourChangeButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModelViewer";
            this.Text = "Model Viewer - Warcraft 3D Toolkit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelViewer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.UI_3DView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl UI_3DView;
        private System.Windows.Forms.TextBox UI_FilterField;
        private System.Windows.Forms.Label UI_FileCount_Label;
        private System.Windows.Forms.TreeView UI_FileList;
        private System.Windows.Forms.Label UI_FilterOverlay;
        private System.Windows.Forms.Timer UI_FilterTime;
        private System.Windows.Forms.ColorDialog UI_ColourDialog;
        private System.Windows.Forms.Button UI_ColourChangeButton;
    }
}