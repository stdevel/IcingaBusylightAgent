using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//Threading
using System.Threading;
//Localization
using System.Resources;
//Version
using System.Reflection;

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
            Application.Run(new AgentContext());
        }
    }

    //TrayIcon context
    public class AgentContext : ApplicationContext
    {
        //Some variables
        private NotifyIcon trayIcon;
        private Thread dataThread;
        private Icinga2Client workerObject;

        private void showTip(string title, string message, ToolTipIcon icon)
        {
            //Show baloon tooltip
            trayIcon.BalloonTipTitle = title;
            trayIcon.BalloonTipText = message;
            trayIcon.BalloonTipIcon = icon;
            trayIcon.ShowBalloonTip(10000);
        }

        public AgentContext()
        {
            //Translate _all_ the strings!
            ResourceManager rm = Strings.ResourceManager;

            //Initialize tray icon
            trayIcon = new NotifyIcon()
            {
                Icon = IcingaBusylightAgent.Properties.Resources.icinga,
                ContextMenu = new ContextMenu(
                        new MenuItem[] {
                            new MenuItem(rm.GetString("mnu_configure"), configure),
                            new MenuItem(rm.GetString("mnu_update"), update),
                            new MenuItem("-"),
                            new MenuItem(rm.GetString("mnu_about"), about),
                            new MenuItem(rm.GetString("mnu_exit"), exit)
                        }
                    ),
                Visible = true,
                Text = "Icinga Busylight Agent"
            };

            //Start data thread
            initializeThread();

            //Show tool-tip
            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;
            showTip(rm.GetString("welcome_title"),
                "Icinga Busylight Agent " + ver.ToString() + " " +
            rm.GetString("welcome_message"), ToolTipIcon.Info);

            //Initialize log
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Application startup completed at {0}", DateTime.Now.ToString()), Properties.Settings.Default.log_level, 2);
        }

        void initializeThread()
        {
            //Create and (re-)start data thread
            workerObject = new Icinga2Client(
                IcingaBusylightAgent.Properties.Settings.Default.icinga_url,
                IcingaBusylightAgent.Properties.Settings.Default.icinga_user,
                IcingaBusylightAgent.Properties.Settings.Default.icinga_pass,
                (IcingaBusylightAgent.Properties.Settings.Default.icinga_update_interval * 1000 * 60),
                IcingaBusylightAgent.Properties.Settings.Default.color_up_ok,
                IcingaBusylightAgent.Properties.Settings.Default.color_down_crit,
                IcingaBusylightAgent.Properties.Settings.Default.color_unreach_warn,
                IcingaBusylightAgent.Properties.Settings.Default.color_unknown,
                IcingaBusylightAgent.Properties.Settings.Default.sound,
                IcingaBusylightAgent.Properties.Settings.Default.sound_volume
                );

            //Set sound file
            workerObject.setSoundfile(Properties.Settings.Default.sound_file);

            //Update _all_ the data
            dataThread = new Thread(workerObject.updateData);
        }

        void update(object sender, EventArgs e)
        {
            //Update data thread
            dataThread.Start();
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
            var dialogResult = form_conf.ShowDialog();

            //Reload thread
            initializeThread();
        }
        void exit(object sender, EventArgs e)
        {
            //Hide tray icon and die in a fire
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
