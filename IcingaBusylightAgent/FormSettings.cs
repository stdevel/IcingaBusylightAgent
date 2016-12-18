using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Threading
using System.Threading;
//Localization
using System.Resources;

namespace IcingaBusylightAgent
{
    public partial class FormSettings : Form
    {
        //Some variables
        Dictionary<string, Busylight.BusylightJingleClip> dictJingle = new Dictionary<string, Busylight.BusylightJingleClip>()
        {
            {"IM1", Busylight.BusylightJingleClip.IM1 },
            {"IM2", Busylight.BusylightJingleClip.IM2 },
        };
        Dictionary<string, Busylight.BusylightSoundClip> dictSound = new Dictionary<string, Busylight.BusylightSoundClip>()
        {
            {"IM1", Busylight.BusylightSoundClip.IM1 },
            {"IM2", Busylight.BusylightSoundClip.IM2 },
        };
        Dictionary<int, Busylight.BusylightVolume> dictVol = new Dictionary<int, Busylight.BusylightVolume>()
        {
            {0, Busylight.BusylightVolume.Mute },
            {1, Busylight.BusylightVolume.Low },
            {2, Busylight.BusylightVolume.Middle },
            {3, Busylight.BusylightVolume.High },
            {4, Busylight.BusylightVolume.Max }
        };

        //Translate _all_ the strings!
        ResourceManager rm = Strings.ResourceManager;

        //Some logger variables
        string loggerMode;
        int loggerLevel;

        //Icinga2 client
        Icinga2Client demoClient;

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            try
            {
                //Pre-select Icinga settings
                txt_url.Text = Properties.Settings.Default.icinga_url;
                txt_username.Text = Properties.Settings.Default.icinga_user;
                txt_password.Text = Properties.Settings.Default.icinga_pass;
                track_timer.Value = Properties.Settings.Default.icinga_update_interval;
                lbl_track_timer.Text = string.Format("{0} {1}",track_timer.Value, rm.GetString("lbl_minutes"));
                chkHosts.Checked = Properties.Settings.Default.icinga_check_hosts;
                chkServices.Checked = Properties.Settings.Default.icinga_check_services;
                txt_soundfile.Text = Properties.Settings.Default.sound_file;

                //Pre-select color items
                btn_up_ok.BackColor = Properties.Settings.Default.color_up_ok;
                btn_unreach_warn.BackColor = Properties.Settings.Default.color_unreach_warn;
                btn_down_crit.BackColor = Properties.Settings.Default.color_down_crit;
                btn_unknown.BackColor = Properties.Settings.Default.color_unknown;
                cdg_up_ok.Color = Properties.Settings.Default.color_up_ok;
                cdg_unreach_warn.Color = Properties.Settings.Default.color_unreach_warn;
                cdg_down_crit.Color = Properties.Settings.Default.color_down_crit;
                cdg_unknown.Color = Properties.Settings.Default.color_unknown;

                //Pre-select sound items
                box_sound.SelectedItem = Properties.Settings.Default.sound.ToString();
                track_volume.Value = this.dictVol.FirstOrDefault(x => x.Value == Properties.Settings.Default.sound_volume).Key;
                lbl_track_volume.Text = this.dictVol[track_volume.Value].ToString();

                //Add logger entries
                box_logmode.Items.Add(rm.GetString("logger_console"));
                box_logmode.Items.Add(rm.GetString("logger_file"));
                box_logmode.Items.Add(rm.GetString("logger_eventlog"));
                box_loglevel.Items.Add(rm.GetString("logger_error"));
                box_loglevel.Items.Add(rm.GetString("logger_info"));
                box_loglevel.Items.Add(rm.GetString("logger_debug"));

                //Log mode
                switch (Properties.Settings.Default.log_mode)
                {   
                    case "eventlog":
                        //Eventlog
                        box_logmode.SelectedItem = rm.GetString("logger_eventlog");
                        loggerMode = "eventlog";
                        break;
                    case "file":
                        //file
                        box_logmode.SelectedItem = rm.GetString("logger_file");
                        loggerMode = "file";
                        break;
                    case "console":
                        //console
                        box_logmode.SelectedItem = rm.GetString("logger_console");
                        loggerMode = "console";
                        break;
                }
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Log mode is '{0}'", loggerMode), Properties.Settings.Default.log_level, 2);

                //Log level
                switch (Properties.Settings.Default.log_level)
                {
                    case 0:
                        //Error
                        box_loglevel.SelectedItem = rm.GetString("logger_error");
                        loggerLevel = 0;
                        break;
                    case 1:
                        //Info
                        box_loglevel.SelectedItem = rm.GetString("logger_info");
                        loggerLevel = 1;
                        break;
                    case 2:
                        //Debug
                        box_loglevel.SelectedItem = rm.GetString("logger_debug");
                        loggerLevel = 2;
                        break;
                }
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Log level is '{0}'", loggerLevel), Properties.Settings.Default.log_level, 2);

                //Validate
                validateSettings();

                //Pre-select listbox items
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Hostgroup filter is '{0}'", Properties.Settings.Default.icinga_hostgroups), Properties.Settings.Default.log_level, 2);
                for (int i=0; i < lbox_hostgroups.Items.Count; i++)
                {
                    if (Properties.Settings.Default.icinga_hostgroups.Contains( lbox_hostgroups.Items[i].ToString().Substring(0, lbox_hostgroups.Items[i].ToString().IndexOf(" ")) ))
                    {
                        SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Pre-selecting item '{0}'", lbox_hostgroups.Items[i].ToString()), Properties.Settings.Default.log_level, 2);
                        lbox_hostgroups.SetSelected(i, true);
                    }
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Unable to pre-select settings, might by fscked up or unset", Properties.Settings.Default.log_level, 1);
            }
        }

        private void saveSettings()
        {
            //Save _all_ the settings

            //Set Icinga settings
            Properties.Settings.Default.icinga_url = txt_url.Text;
            Properties.Settings.Default.icinga_user = txt_username.Text;
            Properties.Settings.Default.icinga_pass = txt_password.Text;
            Properties.Settings.Default.icinga_update_interval = track_timer.Value;
            Properties.Settings.Default.icinga_check_hosts = chkHosts.Checked;
            Properties.Settings.Default.icinga_check_services = chkServices.Checked;

            //Set hostgroup filter
            string new_filter = "";
            string temp = null;
            for(int i=0; i < lbox_hostgroups.SelectedItems.Count; i++)
            {
                temp = lbox_hostgroups.SelectedItems[i].ToString();
                temp = temp.Substring(0, temp.IndexOf(" "));
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Adding entry to hostgroup filter: '{0}'", temp), Properties.Settings.Default.log_level, 2);
                if (new_filter == "") { new_filter = temp; }
                else { new_filter = string.Format("{0};{1}", new_filter, temp); }
            }
            Properties.Settings.Default.icinga_hostgroups = new_filter;

            //Set colors
            Properties.Settings.Default.color_up_ok = cdg_up_ok.Color;
            Properties.Settings.Default.color_unreach_warn = cdg_unreach_warn.Color;
            Properties.Settings.Default.color_down_crit = cdg_down_crit.Color;
            Properties.Settings.Default.color_unknown = cdg_unknown.Color;
            Properties.Settings.Default.icinga_update_interval = track_timer.Value;

            //Set sound and volume
            Properties.Settings.Default.sound = this.dictJingle[box_sound.SelectedItem.ToString()];
            Properties.Settings.Default.sound_volume = this.dictVol[track_volume.Value];
            Properties.Settings.Default.sound_file = txt_soundfile.Text;

            //Logging
            Properties.Settings.Default.log_mode = loggerMode;
            Properties.Settings.Default.log_level = loggerLevel;
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Log mode is '{0}', log level is '{1}'", loggerMode, loggerLevel), Properties.Settings.Default.log_level, 2);

            //Save changes
            Properties.Settings.Default.Save();
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Saved settings!", Properties.Settings.Default.log_level, 2);
        }

        private void validateSettings()
        {
            //Validate settings and pre-select listbox items

            //Test login
            demoClient = new Icinga2Client(
               txt_url.Text,
               txt_username.Text,
               txt_password.Text,
               (track_timer.Value * 1000 * 60),
               cdg_up_ok.Color,
               cdg_down_crit.Color,
               cdg_unreach_warn.Color,
               cdg_unknown.Color,
               this.dictJingle[box_sound.SelectedItem.ToString()],
               this.dictVol[track_volume.Value]
               );

            //Retrieve hostgroups
            try
            {
                List<apiDataset> hostgroups = demoClient.getInventory("HostGroup");
                foreach (apiDataset entry in hostgroups)
                {
                    //add entry
                    lbox_hostgroups.Items.Add(entry.name + " (" + entry.attrs.display_name + ")");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(rm.GetString("msgbox_icinga_unavailable"), rm.GetString("msgbox_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Unable to connect to Icinga2 instance", Properties.Settings.Default.log_level);
            }
            catch (FormatException)
            {
                MessageBox.Show(rm.GetString("msgbox_icinga_unavailable"), rm.GetString("msgbox_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Unable to connect to Icinga2 instance", Properties.Settings.Default.log_level);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate settings
                validateSettings();

                //Save settings
                saveSettings();
                this.Close();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(rm.GetString("msgbox_icinga_unavailable"), rm.GetString("msgbox_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Unable to connect to Icinga2 instance", Properties.Settings.Default.log_level);
            }
            catch (FormatException)
            {
                MessageBox.Show(rm.GetString("msgbox_icinga_unavailable"), rm.GetString("msgbox_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Unable to connect to Icinga2 instance", Properties.Settings.Default.log_level);
            }
        }

        private void btn_up_ok_Click(object sender, EventArgs e)
        {
            //Open dialog
            DialogResult result = cdg_up_ok.ShowDialog();
            if(result == DialogResult.OK)
            {
                //Set button color
                btn_up_ok.BackColor = cdg_up_ok.Color;
                //Demonstrate color
                var controller = new Busylight.SDK();
                controller.Light(new Busylight.BusylightColor() { RedRgbValue = cdg_up_ok.Color.R, GreenRgbValue = cdg_up_ok.Color.G, BlueRgbValue = cdg_up_ok.Color.B });
            }
        }

        private void btn_down_crit_Click(object sender, EventArgs e)
        {
            //Open dialog
            DialogResult result = cdg_down_crit.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Set button color
                btn_down_crit.BackColor = cdg_down_crit.Color;
                //Demonstrate color
                var controller = new Busylight.SDK();
                controller.Light(new Busylight.BusylightColor() { RedRgbValue = cdg_down_crit.Color.R, GreenRgbValue = cdg_down_crit.Color.G, BlueRgbValue = cdg_down_crit.Color.B });
            }
        }

        private void btn_unreach_warn_Click(object sender, EventArgs e)
        {
            //Open dialog
            DialogResult result = cdg_unreach_warn.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Set button color
                btn_unreach_warn.BackColor = cdg_unreach_warn.Color;
                //Demonstrate color
                var controller = new Busylight.SDK();
                controller.Light(new Busylight.BusylightColor() { RedRgbValue = cdg_unreach_warn.Color.R, GreenRgbValue = cdg_unreach_warn.Color.G, BlueRgbValue = cdg_unreach_warn.Color.B });
            }
        }

        private void btn_unknown_Click(object sender, EventArgs e)
        {
            //Open dialog
            DialogResult result = cdg_unknown.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Set button color
                btn_unknown.BackColor = cdg_unknown.Color;
                //Demonstrate color
                var controller = new Busylight.SDK();
                controller.Light(new Busylight.BusylightColor() { RedRgbValue = cdg_unknown.Color.R, GreenRgbValue = cdg_unknown.Color.G, BlueRgbValue = cdg_unknown.Color.B });
            }
        }

        private void box_sound_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Play sound
                var controller = new Busylight.SDK();
                controller.Alert(new Busylight.BusylightColor { RedRgbValue = 0, GreenRgbValue = 0, BlueRgbValue = 0 },
                    dictSound[box_sound.SelectedItem.ToString()],
                    dictVol[track_volume.Value]);
                Thread.Sleep(3000);
                controller.Terminate();
            }
            catch(KeyNotFoundException)
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Something went wrong when defining the sound based on the form's current selection", Properties.Settings.Default.log_level, 2);
            }
        }

        private void track_volume_Scroll(object sender, EventArgs e)
        {
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Value is '{0}', setting will be '{1}'", track_volume.Value, this.dictVol[track_volume.Value].ToString()), Properties.Settings.Default.log_level, 2);

            try
            {
                //Demonstrate
                var controller = new Busylight.SDK();
                controller.Alert(new Busylight.BusylightColor { RedRgbValue = 0, GreenRgbValue = 0, BlueRgbValue = 0 }, dictSound[box_sound.SelectedItem.ToString()],
                    this.dictVol[track_volume.Value]
                    );

                //Set label
                lbl_track_volume.Text = this.dictVol[track_volume.Value].ToString();
            }
            catch (KeyNotFoundException)
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Something went wrong when defining the volume based on the form's current selection", Properties.Settings.Default.log_level, 2);
            }
        }

        private void track_timer_Scroll(object sender, EventArgs e)
        {
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Timer is '{0}'", track_timer.Value), Properties.Settings.Default.log_level, 2);

            //Set label
            lbl_track_timer.Text = string.Format("{0} {1}", track_timer.Value, rm.GetString("lbl_minutes"));
        }

        private void btn_validate_Click(object sender, EventArgs e)
        {
            //Validate settings
            validateSettings();
        }

        private void btn_soundfile_set_Click(object sender, EventArgs e)
        {
            //Open file
            DialogResult result = ofd_sound.ShowDialog();
            if(result == DialogResult.OK)
            {
                //Valid file selected
                txt_soundfile.Text = ofd_sound.FileName;
            }
        }

        private void btn_default_Click(object sender, EventArgs e)
        {
            //Restore senseful default settings
            txt_url.Text = "https://myhost.localdomain.loc:5665/";
            track_timer.Value = 5;
            lbl_track_timer.Text = string.Format("5 {0}", rm.GetString("lbl_minutes"));
            chkHosts.Checked = true;
            chkServices.Checked = true;
            box_sound.SelectedItem = "IM1";
            track_volume.Value = 3;
            cdg_up_ok.Color = Color.Green;
            btn_up_ok.BackColor = cdg_up_ok.Color;
            cdg_unreach_warn.Color = Color.Orange;
            btn_unreach_warn.BackColor = cdg_unreach_warn.Color;
            cdg_down_crit.Color = Color.Red;
            btn_down_crit.BackColor = cdg_down_crit.Color;
            cdg_unknown.Color = Color.Fuchsia;
            btn_unknown.BackColor = cdg_unknown.Color;
            box_logmode.SelectedIndex = 0;
            loggerMode = "console";
            loggerLevel = 1;
        }

        private void box_logmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Log mode
            if (box_logmode.SelectedItem.ToString() == rm.GetString("logger_eventlog")) { loggerMode = "eventlog"; }
            else if (box_logmode.SelectedItem.ToString() == rm.GetString("logger_file")) { loggerMode = "file"; }
            else { loggerMode = "console"; }
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, String.Format("Log mode changed to '{0}'", loggerMode), Properties.Settings.Default.log_level, 2);
        }

        private void box_loglevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Log level
            if (box_loglevel.SelectedItem.ToString() == rm.GetString("logger_debug")) { loggerLevel = 2; }
            else if (box_loglevel.SelectedItem.ToString() == rm.GetString("logger_info")) { loggerLevel = 1; }
            else { loggerLevel = 0; }
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Log level changed to '{0}'", loggerLevel), Properties.Settings.Default.log_level, 2);
        }
    }
}
