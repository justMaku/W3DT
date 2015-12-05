namespace W3DT.Windows
{
    partial class SoundPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundPlayer));
            this.UI_TrackTitle = new System.Windows.Forms.Label();
            this.UI_SeekBar = new System.Windows.Forms.TrackBar();
            this.UI_StateLabel = new System.Windows.Forms.Label();
            this.UI_RewindButton = new System.Windows.Forms.Button();
            this.UI_ForwardButton = new System.Windows.Forms.Button();
            this.UI_PauseButton = new System.Windows.Forms.Button();
            this.UI_PlayButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UI_SeekBar)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_TrackTitle
            // 
            this.UI_TrackTitle.AutoSize = true;
            this.UI_TrackTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_TrackTitle.Location = new System.Drawing.Point(12, 9);
            this.UI_TrackTitle.Name = "UI_TrackTitle";
            this.UI_TrackTitle.Size = new System.Drawing.Size(399, 20);
            this.UI_TrackTitle.TabIndex = 0;
            this.UI_TrackTitle.Text = "This is where we would put the name of the track";
            // 
            // UI_SeekBar
            // 
            this.UI_SeekBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_SeekBar.Location = new System.Drawing.Point(12, 61);
            this.UI_SeekBar.Maximum = 1000;
            this.UI_SeekBar.Name = "UI_SeekBar";
            this.UI_SeekBar.Size = new System.Drawing.Size(494, 45);
            this.UI_SeekBar.TabIndex = 1;
            this.UI_SeekBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // UI_StateLabel
            // 
            this.UI_StateLabel.AutoSize = true;
            this.UI_StateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_StateLabel.Location = new System.Drawing.Point(23, 31);
            this.UI_StateLabel.Name = "UI_StateLabel";
            this.UI_StateLabel.Size = new System.Drawing.Size(125, 18);
            this.UI_StateLabel.TabIndex = 0;
            this.UI_StateLabel.Text = "Currently: Paused";
            // 
            // UI_RewindButton
            // 
            this.UI_RewindButton.Image = global::W3DT.Properties.Resources.rewind45;
            this.UI_RewindButton.Location = new System.Drawing.Point(44, 124);
            this.UI_RewindButton.Name = "UI_RewindButton";
            this.UI_RewindButton.Size = new System.Drawing.Size(44, 44);
            this.UI_RewindButton.TabIndex = 2;
            this.UI_RewindButton.UseVisualStyleBackColor = true;
            // 
            // UI_ForwardButton
            // 
            this.UI_ForwardButton.Image = global::W3DT.Properties.Resources.fast46;
            this.UI_ForwardButton.Location = new System.Drawing.Point(194, 124);
            this.UI_ForwardButton.Name = "UI_ForwardButton";
            this.UI_ForwardButton.Size = new System.Drawing.Size(44, 44);
            this.UI_ForwardButton.TabIndex = 2;
            this.UI_ForwardButton.UseVisualStyleBackColor = true;
            // 
            // UI_PauseButton
            // 
            this.UI_PauseButton.Image = global::W3DT.Properties.Resources.pause441;
            this.UI_PauseButton.Location = new System.Drawing.Point(144, 124);
            this.UI_PauseButton.Name = "UI_PauseButton";
            this.UI_PauseButton.Size = new System.Drawing.Size(44, 44);
            this.UI_PauseButton.TabIndex = 2;
            this.UI_PauseButton.UseVisualStyleBackColor = true;
            // 
            // UI_PlayButton
            // 
            this.UI_PlayButton.Image = global::W3DT.Properties.Resources.play106;
            this.UI_PlayButton.Location = new System.Drawing.Point(94, 124);
            this.UI_PlayButton.Name = "UI_PlayButton";
            this.UI_PlayButton.Size = new System.Drawing.Size(44, 44);
            this.UI_PlayButton.TabIndex = 2;
            this.UI_PlayButton.UseVisualStyleBackColor = true;
            // 
            // SoundPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 266);
            this.Controls.Add(this.UI_RewindButton);
            this.Controls.Add(this.UI_ForwardButton);
            this.Controls.Add(this.UI_PauseButton);
            this.Controls.Add(this.UI_PlayButton);
            this.Controls.Add(this.UI_SeekBar);
            this.Controls.Add(this.UI_StateLabel);
            this.Controls.Add(this.UI_TrackTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SoundPlayer";
            this.Text = "No Track - W3DT";
            ((System.ComponentModel.ISupportInitialize)(this.UI_SeekBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_TrackTitle;
        private System.Windows.Forms.TrackBar UI_SeekBar;
        private System.Windows.Forms.Label UI_StateLabel;
        private System.Windows.Forms.Button UI_PlayButton;
        private System.Windows.Forms.Button UI_PauseButton;
        private System.Windows.Forms.Button UI_ForwardButton;
        private System.Windows.Forms.Button UI_RewindButton;
    }
}