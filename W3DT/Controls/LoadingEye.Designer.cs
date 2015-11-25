namespace W3DT.Controls
{
    partial class LoadingEye
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
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            image = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(image)).BeginInit();
            this.SuspendLayout();

            // 
            // image
            // 
            image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            image.Image = global::W3DT.Properties.Resources.green_eye;
            image.Location = new System.Drawing.Point(0, 0);
            image.Name = "LoadingEyeImage";
            image.Size = new System.Drawing.Size(512, 256);
            image.TabIndex = 0;
            image.TabStop = false;

            this.Controls.Add(image);
            ((System.ComponentModel.ISupportInitialize)(image)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Timer timer;
    }
}
