namespace IcingaBusylightAgent
{
    partial class FormSettings
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.btn_save = new System.Windows.Forms.Button();
            this.lbl_url = new System.Windows.Forms.Label();
            this.lbl_username = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.box_sound = new System.Windows.Forms.ComboBox();
            this.lbl_sounds = new System.Windows.Forms.Label();
            this.cdg_up_ok = new System.Windows.Forms.ColorDialog();
            this.lbl_colors = new System.Windows.Forms.Label();
            this.btn_down_crit = new System.Windows.Forms.Button();
            this.btn_unreach_warn = new System.Windows.Forms.Button();
            this.btn_unknown = new System.Windows.Forms.Button();
            this.btn_up_ok = new System.Windows.Forms.Button();
            this.lbl_volume = new System.Windows.Forms.Label();
            this.cdg_down_crit = new System.Windows.Forms.ColorDialog();
            this.cdg_unreach_warn = new System.Windows.Forms.ColorDialog();
            this.cdg_unknown = new System.Windows.Forms.ColorDialog();
            this.track_volume = new System.Windows.Forms.TrackBar();
            this.lbl_timer = new System.Windows.Forms.Label();
            this.track_timer = new System.Windows.Forms.TrackBar();
            this.lbl_track_timer = new System.Windows.Forms.Label();
            this.lbl_track_volume = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.track_volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_timer)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.Name = "btn_save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // lbl_url
            // 
            resources.ApplyResources(this.lbl_url, "lbl_url");
            this.lbl_url.Name = "lbl_url";
            // 
            // lbl_username
            // 
            resources.ApplyResources(this.lbl_username, "lbl_username");
            this.lbl_username.Name = "lbl_username";
            // 
            // lbl_password
            // 
            resources.ApplyResources(this.lbl_password, "lbl_password");
            this.lbl_password.Name = "lbl_password";
            // 
            // txt_url
            // 
            resources.ApplyResources(this.txt_url, "txt_url");
            this.txt_url.Name = "txt_url";
            // 
            // txt_username
            // 
            resources.ApplyResources(this.txt_username, "txt_username");
            this.txt_username.Name = "txt_username";
            // 
            // txt_password
            // 
            resources.ApplyResources(this.txt_password, "txt_password");
            this.txt_password.Name = "txt_password";
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // box_sound
            // 
            resources.ApplyResources(this.box_sound, "box_sound");
            this.box_sound.FormattingEnabled = true;
            this.box_sound.Items.AddRange(new object[] {
            resources.GetString("box_sound.Items"),
            resources.GetString("box_sound.Items1")});
            this.box_sound.Name = "box_sound";
            this.box_sound.SelectedIndexChanged += new System.EventHandler(this.box_sound_SelectedIndexChanged);
            // 
            // lbl_sounds
            // 
            resources.ApplyResources(this.lbl_sounds, "lbl_sounds");
            this.lbl_sounds.Name = "lbl_sounds";
            // 
            // lbl_colors
            // 
            resources.ApplyResources(this.lbl_colors, "lbl_colors");
            this.lbl_colors.Name = "lbl_colors";
            // 
            // btn_down_crit
            // 
            resources.ApplyResources(this.btn_down_crit, "btn_down_crit");
            this.btn_down_crit.Name = "btn_down_crit";
            this.btn_down_crit.UseVisualStyleBackColor = true;
            this.btn_down_crit.Click += new System.EventHandler(this.btn_down_crit_Click);
            // 
            // btn_unreach_warn
            // 
            resources.ApplyResources(this.btn_unreach_warn, "btn_unreach_warn");
            this.btn_unreach_warn.Name = "btn_unreach_warn";
            this.btn_unreach_warn.UseVisualStyleBackColor = true;
            this.btn_unreach_warn.Click += new System.EventHandler(this.btn_unreach_warn_Click);
            // 
            // btn_unknown
            // 
            resources.ApplyResources(this.btn_unknown, "btn_unknown");
            this.btn_unknown.Name = "btn_unknown";
            this.btn_unknown.UseVisualStyleBackColor = true;
            this.btn_unknown.Click += new System.EventHandler(this.btn_unknown_Click);
            // 
            // btn_up_ok
            // 
            resources.ApplyResources(this.btn_up_ok, "btn_up_ok");
            this.btn_up_ok.Name = "btn_up_ok";
            this.btn_up_ok.UseVisualStyleBackColor = true;
            this.btn_up_ok.Click += new System.EventHandler(this.btn_up_ok_Click);
            // 
            // lbl_volume
            // 
            resources.ApplyResources(this.lbl_volume, "lbl_volume");
            this.lbl_volume.Name = "lbl_volume";
            // 
            // track_volume
            // 
            resources.ApplyResources(this.track_volume, "track_volume");
            this.track_volume.LargeChange = 1;
            this.track_volume.Maximum = 4;
            this.track_volume.Name = "track_volume";
            this.track_volume.Scroll += new System.EventHandler(this.track_volume_Scroll);
            // 
            // lbl_timer
            // 
            resources.ApplyResources(this.lbl_timer, "lbl_timer");
            this.lbl_timer.Name = "lbl_timer";
            // 
            // track_timer
            // 
            resources.ApplyResources(this.track_timer, "track_timer");
            this.track_timer.Maximum = 60;
            this.track_timer.Minimum = 1;
            this.track_timer.Name = "track_timer";
            this.track_timer.Value = 30;
            this.track_timer.Scroll += new System.EventHandler(this.track_timer_Scroll);
            // 
            // lbl_track_timer
            // 
            resources.ApplyResources(this.lbl_track_timer, "lbl_track_timer");
            this.lbl_track_timer.Name = "lbl_track_timer";
            // 
            // lbl_track_volume
            // 
            resources.ApplyResources(this.lbl_track_volume, "lbl_track_volume");
            this.lbl_track_volume.Name = "lbl_track_volume";
            // 
            // FormSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_track_volume);
            this.Controls.Add(this.lbl_track_timer);
            this.Controls.Add(this.track_timer);
            this.Controls.Add(this.lbl_timer);
            this.Controls.Add(this.track_volume);
            this.Controls.Add(this.lbl_volume);
            this.Controls.Add(this.btn_up_ok);
            this.Controls.Add(this.btn_unknown);
            this.Controls.Add(this.btn_unreach_warn);
            this.Controls.Add(this.btn_down_crit);
            this.Controls.Add(this.lbl_colors);
            this.Controls.Add(this.lbl_sounds);
            this.Controls.Add(this.box_sound);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.lbl_username);
            this.Controls.Add(this.lbl_url);
            this.Controls.Add(this.btn_save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.track_volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_timer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label lbl_url;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.ComboBox box_sound;
        private System.Windows.Forms.Label lbl_sounds;
        private System.Windows.Forms.ColorDialog cdg_up_ok;
        private System.Windows.Forms.Label lbl_colors;
        private System.Windows.Forms.Button btn_down_crit;
        private System.Windows.Forms.Button btn_unreach_warn;
        private System.Windows.Forms.Button btn_unknown;
        private System.Windows.Forms.Button btn_up_ok;
        private System.Windows.Forms.Label lbl_volume;
        private System.Windows.Forms.ColorDialog cdg_down_crit;
        private System.Windows.Forms.ColorDialog cdg_unreach_warn;
        private System.Windows.Forms.ColorDialog cdg_unknown;
        private System.Windows.Forms.TrackBar track_volume;
        private System.Windows.Forms.Label lbl_timer;
        private System.Windows.Forms.TrackBar track_timer;
        private System.Windows.Forms.Label lbl_track_timer;
        private System.Windows.Forms.Label lbl_track_volume;
    }
}

