namespace W3DT
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
            this.UI_RewindButton = new System.Windows.Forms.Button();
            this.UI_PlayButton = new System.Windows.Forms.Button();
            this.UI_PauseButton = new System.Windows.Forms.Button();
            this.UI_StopButton = new System.Windows.Forms.Button();
            this.UI_FastButton = new System.Windows.Forms.Button();
            this.UI_SaveButton = new System.Windows.Forms.Button();
            this.UI_TrackTitle = new System.Windows.Forms.Label();
            this.UI_StateLabel = new System.Windows.Forms.Label();
            this.UI_SeekBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.UI_SeekBar)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_RewindButton
            // 
            this.UI_RewindButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_RewindButton.Image = global::W3DT.Properties.Resources.rewind45;
            this.UI_RewindButton.Location = new System.Drawing.Point(12, 103);
            this.UI_RewindButton.Name = "UI_RewindButton";
            this.UI_RewindButton.Size = new System.Drawing.Size(44, 44);
            this.UI_RewindButton.TabIndex = 0;
            this.UI_RewindButton.UseVisualStyleBackColor = true;
            // 
            // UI_PlayButton
            // 
            this.UI_PlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_PlayButton.Image = global::W3DT.Properties.Resources.play106;
            this.UI_PlayButton.Location = new System.Drawing.Point(62, 103);
            this.UI_PlayButton.Name = "UI_PlayButton";
            this.UI_PlayButton.Size = new System.Drawing.Size(44, 44);
            this.UI_PlayButton.TabIndex = 1;
            this.UI_PlayButton.UseVisualStyleBackColor = true;
            this.UI_PlayButton.Click += new System.EventHandler(this.UI_PlayButton_Click);
            // 
            // UI_PauseButton
            // 
            this.UI_PauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_PauseButton.Image = global::W3DT.Properties.Resources.pause441;
            this.UI_PauseButton.Location = new System.Drawing.Point(112, 103);
            this.UI_PauseButton.Name = "UI_PauseButton";
            this.UI_PauseButton.Size = new System.Drawing.Size(44, 44);
            this.UI_PauseButton.TabIndex = 2;
            this.UI_PauseButton.UseVisualStyleBackColor = true;
            this.UI_PauseButton.Click += new System.EventHandler(this.UI_PauseButton_Click);
            // 
            // UI_StopButton
            // 
            this.UI_StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_StopButton.Image = global::W3DT.Properties.Resources.stop;
            this.UI_StopButton.Location = new System.Drawing.Point(162, 103);
            this.UI_StopButton.Name = "UI_StopButton";
            this.UI_StopButton.Size = new System.Drawing.Size(44, 44);
            this.UI_StopButton.TabIndex = 3;
            this.UI_StopButton.UseVisualStyleBackColor = true;
            this.UI_StopButton.Click += new System.EventHandler(this.UI_StopButton_Click);
            // 
            // UI_FastButton
            // 
            this.UI_FastButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_FastButton.Image = global::W3DT.Properties.Resources.fast46;
            this.UI_FastButton.Location = new System.Drawing.Point(212, 103);
            this.UI_FastButton.Name = "UI_FastButton";
            this.UI_FastButton.Size = new System.Drawing.Size(44, 44);
            this.UI_FastButton.TabIndex = 4;
            this.UI_FastButton.UseVisualStyleBackColor = true;
            // 
            // UI_SaveButton
            // 
            this.UI_SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_SaveButton.Location = new System.Drawing.Point(262, 103);
            this.UI_SaveButton.Name = "UI_SaveButton";
            this.UI_SaveButton.Size = new System.Drawing.Size(149, 44);
            this.UI_SaveButton.TabIndex = 5;
            this.UI_SaveButton.Text = "Save File As...";
            this.UI_SaveButton.UseVisualStyleBackColor = true;
            // 
            // UI_TrackTitle
            // 
            this.UI_TrackTitle.AutoSize = true;
            this.UI_TrackTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_TrackTitle.Location = new System.Drawing.Point(12, 9);
            this.UI_TrackTitle.Name = "UI_TrackTitle";
            this.UI_TrackTitle.Size = new System.Drawing.Size(289, 20);
            this.UI_TrackTitle.TabIndex = 6;
            this.UI_TrackTitle.Text = "There should be a track title here...";
            // 
            // UI_StateLabel
            // 
            this.UI_StateLabel.AutoSize = true;
            this.UI_StateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_StateLabel.Location = new System.Drawing.Point(23, 32);
            this.UI_StateLabel.Name = "UI_StateLabel";
            this.UI_StateLabel.Size = new System.Drawing.Size(132, 16);
            this.UI_StateLabel.TabIndex = 7;
            this.UI_StateLabel.Text = "Currently: Initializing...";
            // 
            // UI_SeekBar
            // 
            this.UI_SeekBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_SeekBar.Location = new System.Drawing.Point(12, 59);
            this.UI_SeekBar.Maximum = 1000;
            this.UI_SeekBar.Name = "UI_SeekBar";
            this.UI_SeekBar.Size = new System.Drawing.Size(399, 45);
            this.UI_SeekBar.TabIndex = 8;
            this.UI_SeekBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // SoundPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 159);
            this.Controls.Add(this.UI_SeekBar);
            this.Controls.Add(this.UI_StateLabel);
            this.Controls.Add(this.UI_TrackTitle);
            this.Controls.Add(this.UI_SaveButton);
            this.Controls.Add(this.UI_FastButton);
            this.Controls.Add(this.UI_StopButton);
            this.Controls.Add(this.UI_PauseButton);
            this.Controls.Add(this.UI_PlayButton);
            this.Controls.Add(this.UI_RewindButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(439, 197);
            this.MinimumSize = new System.Drawing.Size(439, 197);
            this.Name = "SoundPlayer";
            this.Text = "No File Loaded - W3DT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SoundPlayer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.UI_SeekBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UI_RewindButton;
        private System.Windows.Forms.Button UI_PlayButton;
        private System.Windows.Forms.Button UI_PauseButton;
        private System.Windows.Forms.Button UI_StopButton;
        private System.Windows.Forms.Button UI_FastButton;
        private System.Windows.Forms.Button UI_SaveButton;
        private System.Windows.Forms.Label UI_TrackTitle;
        private System.Windows.Forms.Label UI_StateLabel;
        private System.Windows.Forms.TrackBar UI_SeekBar;
    }
}