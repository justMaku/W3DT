namespace W3DT_Updater
{
    partial class UpdateWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateWindow));
            this.UpdateSplash = new System.Windows.Forms.PictureBox();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.UpdateSplash)).BeginInit();
            this.SuspendLayout();
            // 
            // UpdateSplash
            // 
            this.UpdateSplash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpdateSplash.Image = global::W3DT_Updater.Properties.Resources._3d_toolkit_splash_updating;
            this.UpdateSplash.Location = new System.Drawing.Point(0, 0);
            this.UpdateSplash.Name = "UpdateSplash";
            this.UpdateSplash.Size = new System.Drawing.Size(1035, 691);
            this.UpdateSplash.TabIndex = 0;
            this.UpdateSplash.TabStop = false;
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // UpdateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 691);
            this.Controls.Add(this.UpdateSplash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updating...";
            ((System.ComponentModel.ISupportInitialize)(this.UpdateSplash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox UpdateSplash;
        private System.Windows.Forms.Timer MainTimer;
    }
}

