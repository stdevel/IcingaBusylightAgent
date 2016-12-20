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
            this.lbl_checks = new System.Windows.Forms.Label();
            this.chkHosts = new System.Windows.Forms.CheckBox();
            this.chkServices = new System.Windows.Forms.CheckBox();
            this.lbl_soundfile = new System.Windows.Forms.Label();
            this.btn_soundfile_set = new System.Windows.Forms.Button();
            this.txt_soundfile = new System.Windows.Forms.TextBox();
            this.btn_validate = new System.Windows.Forms.Button();
            this.lbl_filter = new System.Windows.Forms.Label();
            this.lbox_hostgroups = new System.Windows.Forms.ListBox();
            this.ofd_sound = new System.Windows.Forms.OpenFileDialog();
            this.lbl_logmode = new System.Windows.Forms.Label();
            this.box_logmode = new System.Windows.Forms.ComboBox();
            this.btn_default = new System.Windows.Forms.Button();
            this.lbl_loglevel = new System.Windows.Forms.Label();
            this.box_loglevel = new System.Windows.Forms.ComboBox();
            this.tabc_settings = new System.Windows.Forms.TabControl();
            this.tabp_icinga2 = new System.Windows.Forms.TabPage();
            this.tabp_notification = new System.Windows.Forms.TabPage();
            this.txt_SpamThreshold = new System.Windows.Forms.TextBox();
            this.lbl_TipThreshold = new System.Windows.Forms.Label();
            this.lbl_balloontips = new System.Windows.Forms.Label();
            this.chkEnableTips = new System.Windows.Forms.CheckBox();
            this.tabp_other = new System.Windows.Forms.TabPage();
            this.chkEnableTipStart = new System.Windows.Forms.CheckBox();
            this.chkEnableLyncWorkaround = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.track_volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_timer)).BeginInit();
            this.tabc_settings.SuspendLayout();
            this.tabp_icinga2.SuspendLayout();
            this.tabp_notification.SuspendLayout();
            this.tabp_other.SuspendLayout();
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
            this.box_sound.FormattingEnabled = true;
            this.box_sound.Items.AddRange(new object[] {
            resources.GetString("box_sound.Items"),
            resources.GetString("box_sound.Items1")});
            resources.ApplyResources(this.box_sound, "box_sound");
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
            this.track_volume.BackColor = System.Drawing.SystemColors.Window;
            this.track_volume.LargeChange = 1;
            resources.ApplyResources(this.track_volume, "track_volume");
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
            this.track_timer.BackColor = System.Drawing.SystemColors.Window;
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
            // lbl_checks
            // 
            resources.ApplyResources(this.lbl_checks, "lbl_checks");
            this.lbl_checks.Name = "lbl_checks";
            // 
            // chkHosts
            // 
            resources.ApplyResources(this.chkHosts, "chkHosts");
            this.chkHosts.Name = "chkHosts";
            this.chkHosts.UseVisualStyleBackColor = true;
            // 
            // chkServices
            // 
            resources.ApplyResources(this.chkServices, "chkServices");
            this.chkServices.Name = "chkServices";
            this.chkServices.UseVisualStyleBackColor = true;
            // 
            // lbl_soundfile
            // 
            resources.ApplyResources(this.lbl_soundfile, "lbl_soundfile");
            this.lbl_soundfile.Name = "lbl_soundfile";
            // 
            // btn_soundfile_set
            // 
            resources.ApplyResources(this.btn_soundfile_set, "btn_soundfile_set");
            this.btn_soundfile_set.Name = "btn_soundfile_set";
            this.btn_soundfile_set.UseVisualStyleBackColor = true;
            this.btn_soundfile_set.Click += new System.EventHandler(this.btn_soundfile_set_Click);
            // 
            // txt_soundfile
            // 
            resources.ApplyResources(this.txt_soundfile, "txt_soundfile");
            this.txt_soundfile.Name = "txt_soundfile";
            // 
            // btn_validate
            // 
            resources.ApplyResources(this.btn_validate, "btn_validate");
            this.btn_validate.Name = "btn_validate";
            this.btn_validate.UseVisualStyleBackColor = true;
            this.btn_validate.Click += new System.EventHandler(this.btn_validate_Click);
            // 
            // lbl_filter
            // 
            resources.ApplyResources(this.lbl_filter, "lbl_filter");
            this.lbl_filter.Name = "lbl_filter";
            // 
            // lbox_hostgroups
            // 
            this.lbox_hostgroups.FormattingEnabled = true;
            resources.ApplyResources(this.lbox_hostgroups, "lbox_hostgroups");
            this.lbox_hostgroups.Name = "lbox_hostgroups";
            this.lbox_hostgroups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // ofd_sound
            // 
            this.ofd_sound.FileName = "openFileDialog1";
            resources.ApplyResources(this.ofd_sound, "ofd_sound");
            // 
            // lbl_logmode
            // 
            resources.ApplyResources(this.lbl_logmode, "lbl_logmode");
            this.lbl_logmode.Name = "lbl_logmode";
            // 
            // box_logmode
            // 
            this.box_logmode.FormattingEnabled = true;
            resources.ApplyResources(this.box_logmode, "box_logmode");
            this.box_logmode.Name = "box_logmode";
            this.box_logmode.SelectedIndexChanged += new System.EventHandler(this.box_logmode_SelectedIndexChanged);
            // 
            // btn_default
            // 
            resources.ApplyResources(this.btn_default, "btn_default");
            this.btn_default.Name = "btn_default";
            this.btn_default.UseVisualStyleBackColor = true;
            this.btn_default.Click += new System.EventHandler(this.btn_default_Click);
            // 
            // lbl_loglevel
            // 
            resources.ApplyResources(this.lbl_loglevel, "lbl_loglevel");
            this.lbl_loglevel.Name = "lbl_loglevel";
            // 
            // box_loglevel
            // 
            this.box_loglevel.FormattingEnabled = true;
            resources.ApplyResources(this.box_loglevel, "box_loglevel");
            this.box_loglevel.Name = "box_loglevel";
            this.box_loglevel.SelectedIndexChanged += new System.EventHandler(this.box_loglevel_SelectedIndexChanged);
            // 
            // tabc_settings
            // 
            this.tabc_settings.Controls.Add(this.tabp_icinga2);
            this.tabc_settings.Controls.Add(this.tabp_notification);
            this.tabc_settings.Controls.Add(this.tabp_other);
            resources.ApplyResources(this.tabc_settings, "tabc_settings");
            this.tabc_settings.Name = "tabc_settings";
            this.tabc_settings.SelectedIndex = 0;
            // 
            // tabp_icinga2
            // 
            this.tabp_icinga2.Controls.Add(this.lbl_url);
            this.tabp_icinga2.Controls.Add(this.lbl_username);
            this.tabp_icinga2.Controls.Add(this.lbl_password);
            this.tabp_icinga2.Controls.Add(this.txt_url);
            this.tabp_icinga2.Controls.Add(this.txt_username);
            this.tabp_icinga2.Controls.Add(this.txt_password);
            this.tabp_icinga2.Controls.Add(this.lbox_hostgroups);
            this.tabp_icinga2.Controls.Add(this.lbl_timer);
            this.tabp_icinga2.Controls.Add(this.lbl_filter);
            this.tabp_icinga2.Controls.Add(this.track_timer);
            this.tabp_icinga2.Controls.Add(this.btn_validate);
            this.tabp_icinga2.Controls.Add(this.lbl_track_timer);
            this.tabp_icinga2.Controls.Add(this.lbl_checks);
            this.tabp_icinga2.Controls.Add(this.chkHosts);
            this.tabp_icinga2.Controls.Add(this.chkServices);
            resources.ApplyResources(this.tabp_icinga2, "tabp_icinga2");
            this.tabp_icinga2.Name = "tabp_icinga2";
            this.tabp_icinga2.UseVisualStyleBackColor = true;
            // 
            // tabp_notification
            // 
            this.tabp_notification.Controls.Add(this.txt_SpamThreshold);
            this.tabp_notification.Controls.Add(this.lbl_TipThreshold);
            this.tabp_notification.Controls.Add(this.lbl_balloontips);
            this.tabp_notification.Controls.Add(this.chkEnableTips);
            this.tabp_notification.Controls.Add(this.lbl_sounds);
            this.tabp_notification.Controls.Add(this.box_sound);
            this.tabp_notification.Controls.Add(this.lbl_volume);
            this.tabp_notification.Controls.Add(this.track_volume);
            this.tabp_notification.Controls.Add(this.lbl_track_volume);
            this.tabp_notification.Controls.Add(this.lbl_soundfile);
            this.tabp_notification.Controls.Add(this.btn_up_ok);
            this.tabp_notification.Controls.Add(this.txt_soundfile);
            this.tabp_notification.Controls.Add(this.btn_unknown);
            this.tabp_notification.Controls.Add(this.btn_soundfile_set);
            this.tabp_notification.Controls.Add(this.btn_unreach_warn);
            this.tabp_notification.Controls.Add(this.lbl_colors);
            this.tabp_notification.Controls.Add(this.btn_down_crit);
            resources.ApplyResources(this.tabp_notification, "tabp_notification");
            this.tabp_notification.Name = "tabp_notification";
            this.tabp_notification.UseVisualStyleBackColor = true;
            // 
            // txt_SpamThreshold
            // 
            resources.ApplyResources(this.txt_SpamThreshold, "txt_SpamThreshold");
            this.txt_SpamThreshold.Name = "txt_SpamThreshold";
            // 
            // lbl_TipThreshold
            // 
            resources.ApplyResources(this.lbl_TipThreshold, "lbl_TipThreshold");
            this.lbl_TipThreshold.Name = "lbl_TipThreshold";
            // 
            // lbl_balloontips
            // 
            resources.ApplyResources(this.lbl_balloontips, "lbl_balloontips");
            this.lbl_balloontips.Name = "lbl_balloontips";
            // 
            // chkEnableTips
            // 
            resources.ApplyResources(this.chkEnableTips, "chkEnableTips");
            this.chkEnableTips.Name = "chkEnableTips";
            this.chkEnableTips.UseVisualStyleBackColor = true;
            // 
            // tabp_other
            // 
            this.tabp_other.Controls.Add(this.chkEnableTipStart);
            this.tabp_other.Controls.Add(this.chkEnableLyncWorkaround);
            this.tabp_other.Controls.Add(this.lbl_logmode);
            this.tabp_other.Controls.Add(this.lbl_loglevel);
            this.tabp_other.Controls.Add(this.box_logmode);
            this.tabp_other.Controls.Add(this.box_loglevel);
            resources.ApplyResources(this.tabp_other, "tabp_other");
            this.tabp_other.Name = "tabp_other";
            this.tabp_other.UseVisualStyleBackColor = true;
            // 
            // chkEnableTipStart
            // 
            resources.ApplyResources(this.chkEnableTipStart, "chkEnableTipStart");
            this.chkEnableTipStart.Name = "chkEnableTipStart";
            this.chkEnableTipStart.UseVisualStyleBackColor = true;
            // 
            // chkEnableLyncWorkaround
            // 
            resources.ApplyResources(this.chkEnableLyncWorkaround, "chkEnableLyncWorkaround");
            this.chkEnableLyncWorkaround.Name = "chkEnableLyncWorkaround";
            this.chkEnableLyncWorkaround.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabc_settings);
            this.Controls.Add(this.btn_default);
            this.Controls.Add(this.btn_save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.track_volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_timer)).EndInit();
            this.tabc_settings.ResumeLayout(false);
            this.tabp_icinga2.ResumeLayout(false);
            this.tabp_icinga2.PerformLayout();
            this.tabp_notification.ResumeLayout(false);
            this.tabp_notification.PerformLayout();
            this.tabp_other.ResumeLayout(false);
            this.tabp_other.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label lbl_checks;
        private System.Windows.Forms.CheckBox chkHosts;
        private System.Windows.Forms.CheckBox chkServices;
        private System.Windows.Forms.Label lbl_soundfile;
        private System.Windows.Forms.Button btn_soundfile_set;
        private System.Windows.Forms.TextBox txt_soundfile;
        private System.Windows.Forms.Button btn_validate;
        private System.Windows.Forms.Label lbl_filter;
        private System.Windows.Forms.ListBox lbox_hostgroups;
        private System.Windows.Forms.OpenFileDialog ofd_sound;
        private System.Windows.Forms.Label lbl_logmode;
        private System.Windows.Forms.ComboBox box_logmode;
        private System.Windows.Forms.Button btn_default;
        private System.Windows.Forms.Label lbl_loglevel;
        private System.Windows.Forms.ComboBox box_loglevel;
        private System.Windows.Forms.TabControl tabc_settings;
        private System.Windows.Forms.TabPage tabp_icinga2;
        private System.Windows.Forms.TabPage tabp_notification;
        private System.Windows.Forms.TabPage tabp_other;
        private System.Windows.Forms.Label lbl_balloontips;
        private System.Windows.Forms.CheckBox chkEnableTips;
        private System.Windows.Forms.Label lbl_TipThreshold;
        private System.Windows.Forms.TextBox txt_SpamThreshold;
        private System.Windows.Forms.CheckBox chkEnableLyncWorkaround;
        private System.Windows.Forms.CheckBox chkEnableTipStart;
    }
}

