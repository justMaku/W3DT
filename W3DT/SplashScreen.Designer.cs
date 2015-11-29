namespace W3DT
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.UI_SplashImage = new System.Windows.Forms.PictureBox();
            this.Timer_SplashClose = new System.Windows.Forms.Timer(this.components);
            this.loadBar = new W3DT.Controls.LoadBar();
            ((System.ComponentModel.ISupportInitialize)(this.UI_SplashImage)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_SplashImage
            // 
            this.UI_SplashImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UI_SplashImage.Image = global::W3DT.Properties.Resources._3d_toolkit_splash;
            this.UI_SplashImage.Location = new System.Drawing.Point(0, 0);
            this.UI_SplashImage.Name = "UI_SplashImage";
            this.UI_SplashImage.Size = new System.Drawing.Size(1035, 691);
            this.UI_SplashImage.TabIndex = 0;
            this.UI_SplashImage.TabStop = false;
            // 
            // Timer_SplashClose
            // 
            this.Timer_SplashClose.Enabled = true;
            this.Timer_SplashClose.Interval = 4000;
            this.Timer_SplashClose.Tick += new System.EventHandler(this.Timer_SplashClose_Tick);
            // 
            // loadBar
            // 
            this.loadBar.BackColor = System.Drawing.Color.LightGray;
            this.loadBar.Location = new System.Drawing.Point(662, 648);
            this.loadBar.Maximum = 60;
            this.loadBar.Name = "loadBar";
            this.loadBar.Size = new System.Drawing.Size(361, 31);
            this.loadBar.TabIndex = 1;
            this.loadBar.Tag = "This is a test";
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 691);
            this.ControlBox = false;
            this.Controls.Add(this.loadBar);
            this.Controls.Add(this.UI_SplashImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Warcraft 3D Toolkit - Loading";
            ((System.ComponentModel.ISupportInitialize)(this.UI_SplashImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox UI_SplashImage;
        private System.Windows.Forms.Timer Timer_SplashClose;
        private W3DT.Controls.LoadBar loadBar;
    }
}