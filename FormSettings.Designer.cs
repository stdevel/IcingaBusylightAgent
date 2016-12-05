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
            this.btn_save.Location = new System.Drawing.Point(281, 418);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // lbl_url
            // 
            this.lbl_url.AutoSize = true;
            this.lbl_url.Location = new System.Drawing.Point(12, 15);
            this.lbl_url.Name = "lbl_url";
            this.lbl_url.Size = new System.Drawing.Size(105, 13);
            this.lbl_url.TabIndex = 1;
            this.lbl_url.Text = "Nagios /Icinga URL:";
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Location = new System.Drawing.Point(12, 41);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(58, 13);
            this.lbl_username.TabIndex = 2;
            this.lbl_username.Text = "Username:";
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(12, 66);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(56, 13);
            this.lbl_password.TabIndex = 3;
            this.lbl_password.Text = "Password:";
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(207, 12);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(149, 20);
            this.txt_url.TabIndex = 4;
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(207, 38);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(149, 20);
            this.txt_username.TabIndex = 5;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(207, 63);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(149, 20);
            this.txt_password.TabIndex = 6;
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // box_sound
            // 
            this.box_sound.FormattingEnabled = true;
            this.box_sound.Items.AddRange(new object[] {
            "IM1",
            "IM2"});
            this.box_sound.Location = new System.Drawing.Point(207, 162);
            this.box_sound.Name = "box_sound";
            this.box_sound.Size = new System.Drawing.Size(149, 21);
            this.box_sound.TabIndex = 7;
            this.box_sound.SelectedIndexChanged += new System.EventHandler(this.box_sound_SelectedIndexChanged);
            // 
            // lbl_sounds
            // 
            this.lbl_sounds.AutoSize = true;
            this.lbl_sounds.Location = new System.Drawing.Point(12, 165);
            this.lbl_sounds.Name = "lbl_sounds";
            this.lbl_sounds.Size = new System.Drawing.Size(41, 13);
            this.lbl_sounds.TabIndex = 8;
            this.lbl_sounds.Text = "Sound:";
            // 
            // lbl_colors
            // 
            this.lbl_colors.AutoSize = true;
            this.lbl_colors.Location = new System.Drawing.Point(12, 264);
            this.lbl_colors.Name = "lbl_colors";
            this.lbl_colors.Size = new System.Drawing.Size(39, 13);
            this.lbl_colors.TabIndex = 9;
            this.lbl_colors.Text = "Colors:";
            // 
            // btn_down_crit
            // 
            this.btn_down_crit.Location = new System.Drawing.Point(207, 292);
            this.btn_down_crit.Name = "btn_down_crit";
            this.btn_down_crit.Size = new System.Drawing.Size(149, 23);
            this.btn_down_crit.TabIndex = 10;
            this.btn_down_crit.Text = "Down / Critical";
            this.btn_down_crit.UseVisualStyleBackColor = true;
            this.btn_down_crit.Click += new System.EventHandler(this.btn_down_crit_Click);
            // 
            // btn_unreach_warn
            // 
            this.btn_unreach_warn.Location = new System.Drawing.Point(207, 321);
            this.btn_unreach_warn.Name = "btn_unreach_warn";
            this.btn_unreach_warn.Size = new System.Drawing.Size(149, 23);
            this.btn_unreach_warn.TabIndex = 11;
            this.btn_unreach_warn.Text = "Unreachable / Warning";
            this.btn_unreach_warn.UseVisualStyleBackColor = true;
            this.btn_unreach_warn.Click += new System.EventHandler(this.btn_unreach_warn_Click);
            // 
            // btn_unknown
            // 
            this.btn_unknown.Location = new System.Drawing.Point(207, 350);
            this.btn_unknown.Name = "btn_unknown";
            this.btn_unknown.Size = new System.Drawing.Size(149, 23);
            this.btn_unknown.TabIndex = 12;
            this.btn_unknown.Text = "Unknown";
            this.btn_unknown.UseVisualStyleBackColor = true;
            this.btn_unknown.Click += new System.EventHandler(this.btn_unknown_Click);
            // 
            // btn_up_ok
            // 
            this.btn_up_ok.Location = new System.Drawing.Point(207, 263);
            this.btn_up_ok.Name = "btn_up_ok";
            this.btn_up_ok.Size = new System.Drawing.Size(149, 23);
            this.btn_up_ok.TabIndex = 13;
            this.btn_up_ok.Text = "Up / Okay";
            this.btn_up_ok.UseVisualStyleBackColor = true;
            this.btn_up_ok.Click += new System.EventHandler(this.btn_up_ok_Click);
            // 
            // lbl_volume
            // 
            this.lbl_volume.AutoSize = true;
            this.lbl_volume.Location = new System.Drawing.Point(12, 189);
            this.lbl_volume.Name = "lbl_volume";
            this.lbl_volume.Size = new System.Drawing.Size(45, 13);
            this.lbl_volume.TabIndex = 15;
            this.lbl_volume.Text = "Volume:";
            // 
            // track_volume
            // 
            this.track_volume.LargeChange = 1;
            this.track_volume.Location = new System.Drawing.Point(207, 189);
            this.track_volume.Maximum = 4;
            this.track_volume.Name = "track_volume";
            this.track_volume.Size = new System.Drawing.Size(149, 45);
            this.track_volume.TabIndex = 16;
            this.track_volume.Scroll += new System.EventHandler(this.track_volume_Scroll);
            // 
            // lbl_timer
            // 
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.Location = new System.Drawing.Point(12, 96);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(36, 13);
            this.lbl_timer.TabIndex = 17;
            this.lbl_timer.Text = "Timer:";
            // 
            // track_timer
            // 
            this.track_timer.Location = new System.Drawing.Point(207, 89);
            this.track_timer.Maximum = 60;
            this.track_timer.Minimum = 1;
            this.track_timer.Name = "track_timer";
            this.track_timer.Size = new System.Drawing.Size(149, 45);
            this.track_timer.TabIndex = 18;
            this.track_timer.Value = 30;
            this.track_timer.Scroll += new System.EventHandler(this.track_timer_Scroll);
            // 
            // lbl_track_timer
            // 
            this.lbl_track_timer.AutoSize = true;
            this.lbl_track_timer.Location = new System.Drawing.Point(299, 137);
            this.lbl_track_timer.Name = "lbl_track_timer";
            this.lbl_track_timer.Size = new System.Drawing.Size(57, 13);
            this.lbl_track_timer.TabIndex = 19;
            this.lbl_track_timer.Text = "x minute(s)";
            // 
            // lbl_track_volume
            // 
            this.lbl_track_volume.AutoSize = true;
            this.lbl_track_volume.Location = new System.Drawing.Point(309, 238);
            this.lbl_track_volume.Name = "lbl_track_volume";
            this.lbl_track_volume.Size = new System.Drawing.Size(49, 13);
            this.lbl_track_volume.TabIndex = 20;
            this.lbl_track_volume.Text = "loudness";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 452);
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
            this.Text = "Settings";
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

