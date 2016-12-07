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

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            //Preselect Icinga settings
            txt_url.Text = Properties.Settings.Default.icinga_url;
            txt_username.Text = Properties.Settings.Default.icinga_user;
            txt_password.Text = Properties.Settings.Default.icinga_pass;
            track_timer.Value = (Properties.Settings.Default.icinga_update_interval/60);
            lbl_track_timer.Text = track_timer.Value + " " + rm.GetString("lbl_minutes");

            //Preselect color items
            btn_up_ok.BackColor = Properties.Settings.Default.color_up_ok;
            btn_unreach_warn.BackColor = Properties.Settings.Default.color_unreach_warn;
            btn_down_crit.BackColor = Properties.Settings.Default.color_down_crit;
            btn_unknown.BackColor = Properties.Settings.Default.color_unknown;
            cdg_up_ok.Color = Properties.Settings.Default.color_up_ok;
            cdg_unreach_warn.Color = Properties.Settings.Default.color_unreach_warn;
            cdg_down_crit.Color = Properties.Settings.Default.color_down_crit;
            cdg_unknown.Color = Properties.Settings.Default.color_unknown;
            //Preselect sound items
            box_sound.SelectedItem = Properties.Settings.Default.sound.ToString();
            track_volume.Value = this.dictVol.FirstOrDefault(x => x.Value == Properties.Settings.Default.sound_volume).Key;
            lbl_track_volume.Text = this.dictVol[track_volume.Value].ToString();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //TODO: Validate settings - attach / for URL?

            //Set colors
            Properties.Settings.Default.color_up_ok = cdg_up_ok.Color;
            Properties.Settings.Default.color_unreach_warn = cdg_unreach_warn.Color;
            Properties.Settings.Default.color_down_crit = cdg_down_crit.Color;
            Properties.Settings.Default.color_unknown = cdg_unknown.Color;
            Properties.Settings.Default.icinga_update_interval = (track_timer.Value * 60);
            //Set sound and volume
            Properties.Settings.Default.sound = this.dictSound[box_sound.SelectedItem.ToString()];
            Properties.Settings.Default.sound_volume = this.dictVol[track_volume.Value];
            //Save changes
            Properties.Settings.Default.Save();

            System.Console.WriteLine("Saved settings, going home!");
            this.Close();
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
            System.Console.WriteLine("Timer is '{0}', value will be '{1}'", track_timer.Value, (track_timer.Value * 60));

            //Set label
            lbl_track_timer.Text = track_timer.Value + " " + rm.GetString("lbl_minutes");
        }
    }
}
