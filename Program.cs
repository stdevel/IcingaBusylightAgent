using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//Threading
using System.Threading;

namespace IcingaBusylightAgent
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new AgentContext());
        }
    }

    //Test
    public class AgentContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        public AgentContext()
        {
            //Initialize tray icon
            trayIcon = new NotifyIcon()
            {
                Icon = IcingaBusylightAgent.Properties.Resources.icinga,
                ContextMenu = new ContextMenu(
                        new MenuItem[] {
                            new MenuItem("Configure", configure),
                            new MenuItem("Update", update),
                            new MenuItem("-"),
                            new MenuItem("About Icinga Busylight...", about),
                            new MenuItem("Exit", exit)
                        }
                    ),
                Visible = true,
                Text = "Icinga Busylight Agent"
            };

            //Start data thread
            Icinga2Client workerObject = new Icinga2Client(
                IcingaBusylightAgent.Properties.Settings.Default.icinga_url,
                IcingaBusylightAgent.Properties.Settings.Default.icinga_user,
                IcingaBusylightAgent.Properties.Settings.Default.icinga_pass,
                (IcingaBusylightAgent.Properties.Settings.Default.icinga_update_interval*1000),
                IcingaBusylightAgent.Properties.Settings.Default.color_up_ok,
                IcingaBusylightAgent.Properties.Settings.Default.color_down_crit,
                IcingaBusylightAgent.Properties.Settings.Default.color_unreach_warn,
                IcingaBusylightAgent.Properties.Settings.Default.color_unknown,
                IcingaBusylightAgent.Properties.Settings.Default.sound,
                IcingaBusylightAgent.Properties.Settings.Default.sound_volume
                );
            Thread dataThread = new Thread(workerObject.updateData);

        }
        //TODO: auto update?

        void update(object sender, EventArgs e)
        {
            //TODO: Update data thread
        }

        void about(object sender, EventArgs e)
        {
            //Show about dialog
            FormAbout form_about = new FormAbout();
            form_about.Show();
        }
        
        void configure(object sender, EventArgs e)
        {
            //Show configuration dialog
            FormSettings form_conf = new FormSettings();
            form_conf.Show();
        }
        void exit(object sender, EventArgs e)
        {
            //Hide tray icon and die in a fire
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
