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
            this.UI_SplashImage = new System.Windows.Forms.PictureBox();
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
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 691);
            this.ControlBox = false;
            this.Controls.Add(this.UI_SplashImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Warcraft 3D Toolkit - Loading";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.UI_SplashImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox UI_SplashImage;
    }
}