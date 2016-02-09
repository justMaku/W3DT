namespace W3DT
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.UI_SectionLabel = new System.Windows.Forms.Label();
            this.UI_Button_Help = new W3DT.Controls.MainFormButton();
            this.UI_Button_Settings = new W3DT.Controls.MainFormButton();
            this.UI_Button_DBC = new W3DT.Controls.MainFormButton();
            this.UI_Button_Spellbook = new W3DT.Controls.MainFormButton();
            this.UI_Button_Artwork = new W3DT.Controls.MainFormButton();
            this.UI_Button_SoundPlayer = new W3DT.Controls.MainFormButton();
            this.UI_Button_MapViewer = new W3DT.Controls.MainFormButton();
            this.UI_Button_Doodad = new W3DT.Controls.MainFormButton();
            this.UI_Button_WMO = new W3DT.Controls.MainFormButton();
            this.UI_Button_ModelViewer = new W3DT.Controls.MainFormButton();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Help)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Settings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_DBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Spellbook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Artwork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_SoundPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_MapViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Doodad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_WMO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_ModelViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_SectionLabel
            // 
            this.UI_SectionLabel.AutoSize = true;
            this.UI_SectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_SectionLabel.Location = new System.Drawing.Point(14, 88);
            this.UI_SectionLabel.Name = "UI_SectionLabel";
            this.UI_SectionLabel.Size = new System.Drawing.Size(266, 31);
            this.UI_SectionLabel.TabIndex = 1;
            this.UI_SectionLabel.Tag = "Warcraft 3D Toolkit";
            this.UI_SectionLabel.Text = "Warcraft 3D Toolkit";
            // 
            // UI_Button_Help
            // 
            this.UI_Button_Help.BackgroundImage = global::W3DT.Properties.Resources.inv_misc_questionmark;
            this.UI_Button_Help.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_Help.Cursor = System.Windows.Forms.Cursors.No;
            this.UI_Button_Help.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_Help.Image")));
            this.UI_Button_Help.Location = new System.Drawing.Point(678, 12);
            this.UI_Button_Help.Name = "UI_Button_Help";
            this.UI_Button_Help.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_Help.TabIndex = 0;
            this.UI_Button_Help.TabStop = false;
            this.UI_Button_Help.Tag = "Support/Information|Details on how to get support for W3DT along with some inform" +
    "ation about the application and its development.";
            this.UI_Button_Help.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_Help.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // UI_Button_Settings
            // 
            this.UI_Button_Settings.BackgroundImage = global::W3DT.Properties.Resources.inv_misc_enggizmos_30;
            this.UI_Button_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_Settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UI_Button_Settings.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_Settings.Image")));
            this.UI_Button_Settings.Location = new System.Drawing.Point(604, 12);
            this.UI_Button_Settings.Name = "UI_Button_Settings";
            this.UI_Button_Settings.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_Settings.TabIndex = 0;
            this.UI_Button_Settings.TabStop = false;
            this.UI_Button_Settings.Tag = "Settings|Adjust how W3DT works to your liking!";
            this.UI_Button_Settings.Click += new System.EventHandler(this.UI_Button_Settings_Click);
            this.UI_Button_Settings.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_Settings.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // UI_Button_DBC
            // 
            this.UI_Button_DBC.BackgroundImage = global::W3DT.Properties.Resources.inv_misc_punchcards_blue;
            this.UI_Button_DBC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_DBC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UI_Button_DBC.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_DBC.Image")));
            this.UI_Button_DBC.Location = new System.Drawing.Point(530, 12);
            this.UI_Button_DBC.Name = "UI_Button_DBC";
            this.UI_Button_DBC.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_DBC.TabIndex = 0;
            this.UI_Button_DBC.TabStop = false;
            this.UI_Button_DBC.Tag = "DBC/DB2 Viewer|DBC/DB2 files are client databases that contain various data for t" +
    "he game, mostly strings of localized information! This tool allows you to browse" +
    " through them.";
            this.UI_Button_DBC.Click += new System.EventHandler(this.UI_Button_DBC_Click);
            this.UI_Button_DBC.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_DBC.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // UI_Button_Spellbook
            // 
            this.UI_Button_Spellbook.BackgroundImage = global::W3DT.Properties.Resources.inv_misc_book_09;
            this.UI_Button_Spellbook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_Spellbook.Cursor = System.Windows.Forms.Cursors.No;
            this.UI_Button_Spellbook.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_Spellbook.Image")));
            this.UI_Button_Spellbook.Location = new System.Drawing.Point(456, 12);
            this.UI_Button_Spellbook.Name = "UI_Button_Spellbook";
            this.UI_Button_Spellbook.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_Spellbook.TabIndex = 0;
            this.UI_Button_Spellbook.TabStop = false;
            this.UI_Button_Spellbook.Tag = "Spellbook|Browse and export spells from within the game; magical!";
            this.UI_Button_Spellbook.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_Spellbook.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // UI_Button_Artwork
            // 
            this.UI_Button_Artwork.BackgroundImage = global::W3DT.Properties.Resources.inv_inscription_scroll;
            this.UI_Button_Artwork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_Artwork.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UI_Button_Artwork.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_Artwork.Image")));
            this.UI_Button_Artwork.Location = new System.Drawing.Point(382, 12);
            this.UI_Button_Artwork.Name = "UI_Button_Artwork";
            this.UI_Button_Artwork.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_Artwork.TabIndex = 0;
            this.UI_Button_Artwork.TabStop = false;
            this.UI_Button_Artwork.Tag = "Music/Sound Player|Listen and export any of the sound/music files located within " +
    "the game files. The language of dialog will depend on your game version (or remo" +
    "te locale in the settings).";
            this.UI_Button_Artwork.Click += new System.EventHandler(this.UI_Button_Artwork_Click);
            this.UI_Button_Artwork.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_Artwork.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // UI_Button_SoundPlayer
            // 
            this.UI_Button_SoundPlayer.BackgroundImage = global::W3DT.Properties.Resources.trade_archaeology_delicatemusicbox;
            this.UI_Button_SoundPlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_SoundPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UI_Button_SoundPlayer.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_SoundPlayer.Image")));
            this.UI_Button_SoundPlayer.Location = new System.Drawing.Point(308, 12);
            this.UI_Button_SoundPlayer.Name = "UI_Button_SoundPlayer";
            this.UI_Button_SoundPlayer.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_SoundPlayer.TabIndex = 0;
            this.UI_Button_SoundPlayer.TabStop = false;
            this.UI_Button_SoundPlayer.Tag = "Music/Sound Player|Listen and export any of the sound/music files located within " +
    "the game files. The language of dialog will depend on your game version (or remo" +
    "te locale in the settings).";
            this.UI_Button_SoundPlayer.Click += new System.EventHandler(this.UI_Button_SoundPlayer_Click);
            this.UI_Button_SoundPlayer.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_SoundPlayer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // UI_Button_MapViewer
            // 
            this.UI_Button_MapViewer.BackgroundImage = global::W3DT.Properties.Resources.inv_misc_map03;
            this.UI_Button_MapViewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_MapViewer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UI_Button_MapViewer.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_MapViewer.Image")));
            this.UI_Button_MapViewer.Location = new System.Drawing.Point(234, 12);
            this.UI_Button_MapViewer.Name = "UI_Button_MapViewer";
            this.UI_Button_MapViewer.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_MapViewer.TabIndex = 0;
            this.UI_Button_MapViewer.TabStop = false;
            this.UI_Button_MapViewer.Tag = "Map Viewer|Browse all maps available in the game using a minimap-based tool. Sele" +
    "ct regions of the maps (or even the entire maps) can be exported as full-3D terr" +
    "ain!";
            this.UI_Button_MapViewer.Click += new System.EventHandler(this.UI_Button_MapViewer_Click);
            this.UI_Button_MapViewer.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_MapViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // UI_Button_Doodad
            // 
            this.UI_Button_Doodad.BackgroundImage = global::W3DT.Properties.Resources.inv_drink_03;
            this.UI_Button_Doodad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_Doodad.Cursor = System.Windows.Forms.Cursors.No;
            this.UI_Button_Doodad.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_Doodad.Image")));
            this.UI_Button_Doodad.Location = new System.Drawing.Point(160, 12);
            this.UI_Button_Doodad.Name = "UI_Button_Doodad";
            this.UI_Button_Doodad.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_Doodad.TabIndex = 0;
            this.UI_Button_Doodad.TabStop = false;
            this.UI_Button_Doodad.Tag = "Object (Doodad) Viewer|Doodads are small objects generally used for clutter and f" +
    "urnishing both interior and exterior environments.";
            this.UI_Button_Doodad.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_Doodad.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // UI_Button_WMO
            // 
            this.UI_Button_WMO.BackgroundImage = global::W3DT.Properties.Resources.inv_hammer_20__1_;
            this.UI_Button_WMO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_WMO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UI_Button_WMO.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_WMO.Image")));
            this.UI_Button_WMO.Location = new System.Drawing.Point(86, 12);
            this.UI_Button_WMO.Name = "UI_Button_WMO";
            this.UI_Button_WMO.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_WMO.TabIndex = 0;
            this.UI_Button_WMO.TabStop = false;
            this.UI_Button_WMO.Tag = "WMO Viewer|WMO objects are large 3D objects that often contain numerous other sma" +
    "ller objects (doodads). As an example, buildings and caves are generally WMO obj" +
    "ects.";
            this.UI_Button_WMO.Click += new System.EventHandler(this.UI_Button_WMO_Click);
            this.UI_Button_WMO.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_WMO.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // UI_Button_ModelViewer
            // 
            this.UI_Button_ModelViewer.BackgroundImage = global::W3DT.Properties.Resources.achievement_character_human_female;
            this.UI_Button_ModelViewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UI_Button_ModelViewer.Cursor = System.Windows.Forms.Cursors.No;
            this.UI_Button_ModelViewer.Image = ((System.Drawing.Image)(resources.GetObject("UI_Button_ModelViewer.Image")));
            this.UI_Button_ModelViewer.Location = new System.Drawing.Point(12, 12);
            this.UI_Button_ModelViewer.Name = "UI_Button_ModelViewer";
            this.UI_Button_ModelViewer.Size = new System.Drawing.Size(68, 68);
            this.UI_Button_ModelViewer.TabIndex = 0;
            this.UI_Button_ModelViewer.TabStop = false;
            this.UI_Button_ModelViewer.Tag = "Model Viewer|View/export characters and creatures from the game. Characters can b" +
    "e equipped with items and even imported from the armory.";
            this.UI_Button_ModelViewer.MouseEnter += new System.EventHandler(this.ShowTooltip);
            this.UI_Button_ModelViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 472);
            this.Controls.Add(this.UI_SectionLabel);
            this.Controls.Add(this.UI_Button_Help);
            this.Controls.Add(this.UI_Button_Settings);
            this.Controls.Add(this.UI_Button_DBC);
            this.Controls.Add(this.UI_Button_Spellbook);
            this.Controls.Add(this.UI_Button_Artwork);
            this.Controls.Add(this.UI_Button_SoundPlayer);
            this.Controls.Add(this.UI_Button_MapViewer);
            this.Controls.Add(this.UI_Button_Doodad);
            this.Controls.Add(this.UI_Button_WMO);
            this.Controls.Add(this.UI_Button_ModelViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Warcraft 3D Toolkit";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateTooltip);
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Help)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Settings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_DBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Spellbook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Artwork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_SoundPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_MapViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_Doodad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_WMO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Button_ModelViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MainFormButton UI_Button_ModelViewer;
        private Controls.MainFormButton UI_Button_WMO;
        private Controls.MainFormButton UI_Button_Doodad;
        private Controls.MainFormButton UI_Button_MapViewer;
        private Controls.MainFormButton UI_Button_SoundPlayer;
        private Controls.MainFormButton UI_Button_Artwork;
        private Controls.MainFormButton UI_Button_Spellbook;
        private Controls.MainFormButton UI_Button_DBC;
        private Controls.MainFormButton UI_Button_Settings;
        private Controls.MainFormButton UI_Button_Help;
        private System.Windows.Forms.Label UI_SectionLabel;

    }
}