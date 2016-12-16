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
        //Variables
        Dictionary<String, Busylight.BusylightJingleClip> dictSound = new Dictionary<string, Busylight.BusylightJingleClip>()
        {
            {"IM1", Busylight.BusylightJingleClip.IM1 },
            {"IM2", Busylight.BusylightJingleClip.IM2 },
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

        //Icinga2 client
        Icinga2Client demoClient;

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            //Pre-select Icinga settings
            try
            {
                txt_url.Text = Properties.Settings.Default.icinga_url;
                txt_username.Text = Properties.Settings.Default.icinga_user;
                txt_password.Text = Properties.Settings.Default.icinga_pass;
                track_timer.Value = Properties.Settings.Default.icinga_update_interval;
                lbl_track_timer.Text = track_timer.Value + " " + rm.GetString("lbl_minutes");
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

                //Validate
                validateSettings();

                //Pre-select listbox items
                System.Console.WriteLine("Hostgroup filter is '{0}'", Properties.Settings.Default.icinga_hostgroups);
                for(int i=0; i < lbox_hostgroups.Items.Count; i++)
                {
                    if (Properties.Settings.Default.icinga_hostgroups.Contains( lbox_hostgroups.Items[i].ToString().Substring(0, lbox_hostgroups.Items[i].ToString().IndexOf(" ")) ))
                    {
                        System.Console.WriteLine("Pre-selecting item '{0}'", lbox_hostgroups.Items[i].ToString());
                        lbox_hostgroups.SetSelected(i, true);
                    }
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                System.Console.WriteLine("Unable to pre-select settings, might by fscked up or unset");
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
            String new_filter = "";
            String temp = null;
            for(int i=0; i < lbox_hostgroups.SelectedItems.Count; i++)
            {
                temp = lbox_hostgroups.SelectedItems[i].ToString();
                temp = temp.Substring(0, temp.IndexOf(" "));     
                System.Console.WriteLine("Adding entry to hostgroup filter: '{0}'", temp);
                if (new_filter == "") { new_filter = temp; }
                else { new_filter = new_filter + ";" + temp; }
            }
            Properties.Settings.Default.icinga_hostgroups = new_filter;

            //Set colors
            Properties.Settings.Default.color_up_ok = cdg_up_ok.Color;
            Properties.Settings.Default.color_unreach_warn = cdg_unreach_warn.Color;
            Properties.Settings.Default.color_down_crit = cdg_down_crit.Color;
            Properties.Settings.Default.color_unknown = cdg_unknown.Color;
            Properties.Settings.Default.icinga_update_interval = track_timer.Value;

            //Set sound and volume
            Properties.Settings.Default.sound = this.dictSound[box_sound.SelectedItem.ToString()];
            Properties.Settings.Default.sound_volume = this.dictVol[track_volume.Value];
            Properties.Settings.Default.sound_file = txt_soundfile.Text;

            //Save changes
            Properties.Settings.Default.Save();

            System.Console.WriteLine("Saved settings!");
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
               this.dictSound[box_sound.SelectedItem.ToString()],
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
                MessageBox.Show("Unable to connect to Icinga2 instance, check settings!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Console.WriteLine("Unable to connect to Icinga2 instance");
            }
            catch (FormatException)
            {
                MessageBox.Show("Unable to connect to Icinga2 instance, check settings!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Console.WriteLine("Unable to connect to Icinga2 instance");
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
                MessageBox.Show("Unable to connect to Icinga2 instance, check settings!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Console.WriteLine("Unable to connect to Icinga2 instance");
            }
            catch (FormatException)
            {
                MessageBox.Show("Unable to connect to Icinga2 instance, check settings!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Console.WriteLine("Unable to connect to Icinga2 instance");
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
            //Play sound
            var controller = new Busylight.SDK();
            //TODO: Volume depending on setting?
            if (box_sound.SelectedItem.ToString() == "IM1")
            {
                //IM1
                controller.Alert(new Busylight.BusylightColor { RedRgbValue = 0, GreenRgbValue = 0, BlueRgbValue = 0 }, Busylight.BusylightSoundClip.IM1, Properties.Settings.Default.sound_volume);
            }
            else
            {
                //IM2
                controller.Alert(new Busylight.BusylightColor { RedRgbValue = 0, GreenRgbValue = 0, BlueRgbValue = 0 }, Busylight.BusylightSoundClip.IM2, Properties.Settings.Default.sound_volume);
            }
            //TODO: Use selected sound?
            //Impossibru because of mismatching Type (BusylightSoundClip vs. BusylightJingleClip)
            //controller.Alert(new Busylight.BusylightColor { RedRgbValue = 0, GreenRgbValue = 0, BlueRgbValue = 0 }, this.dictSound[box_sound.SelectedText], Properties.Settings.Default.sound_volume);
            Thread.Sleep(3000);
            controller.Terminate();
        }

        private void track_volume_Scroll(object sender, EventArgs e)
        {
            System.Console.WriteLine("Value is '{0}', setting will be '{1}'", track_volume.Value, this.dictVol[track_volume.Value].ToString());

            //Demonstrate
            var controller = new Busylight.SDK();
            //TODO: Use configured sound?
            //Impossibru because of mismatching Type (BusylightSoundClip vs. BusylightJingleClip)
            controller.Alert(new Busylight.BusylightColor { RedRgbValue = 0, GreenRgbValue = 0, BlueRgbValue = 0 }, Busylight.BusylightSoundClip.IM2,
                this.dictVol[track_volume.Value]
                );

            //Set label
            lbl_track_volume.Text = this.dictVol[track_volume.Value].ToString();
        }

        private void track_timer_Scroll(object sender, EventArgs e)
        {
            System.Console.WriteLine("Timer is '{0}'", track_timer.Value);

            //Set label
            lbl_track_timer.Text = track_timer.Value + " " + rm.GetString("lbl_minutes");
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
    }
}
