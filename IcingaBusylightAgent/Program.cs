using System;
using System.Collections.Generic;
using System.Windows.Forms;
//Threading
using System.Threading;
//Localization
using System.Resources;
//Version
using System.Reflection;
//Busylight
using Busylight;
using System.Drawing;
//Sound
using System.Media;

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
        private Dictionary<int, string> dictStates = new Dictionary<int, string>();
        private Dictionary<int, string> dictStatesHost = new Dictionary<int, string>();
        private Dictionary<int, ToolTipIcon> dictIcons = new Dictionary<int, ToolTipIcon>() {
            { 0, ToolTipIcon.Info },
            { 1, ToolTipIcon.Warning },
            { 2, ToolTipIcon.Error },
            { 3, ToolTipIcon.None }
        };

        //Data result
        private Dictionary<string, int> failHosts = new Dictionary<string, int>();
        private Dictionary<string, List<string>> failServices = new Dictionary<string, List<string>>();

        //Color variables
        Color color_up_ok;
        Color color_unreach_warn;
        Color color_down_crit;
        Color color_unknown;
        private Dictionary<int, Busylight.BusylightColor> dictColor = new Dictionary<int, Busylight.BusylightColor>();
        private Dictionary<int, Busylight.BusylightColor> dictColorHost = new Dictionary<int, Busylight.BusylightColor>();

        //Notification variables
        BusylightJingleClip sound;
        BusylightVolume volume;
        private string sound_file;
        private SoundPlayer player;

        //Translate _all_ the strings!
        ResourceManager rm = Strings.ResourceManager;

        private void showTip(string title, string message, ToolTipIcon icon)
        {
            //Show baloon tooltip
            trayIcon.BalloonTipTitle = title;
            trayIcon.BalloonTipText = message;
            trayIcon.BalloonTipIcon = icon;
            trayIcon.ShowBalloonTip(5000);
        }

        public AgentContext()
        {
            //Import state strings
            dictStates.Add(0, rm.GetString("state_0"));
            dictStates.Add(1, rm.GetString("state_1"));
            dictStates.Add(2, rm.GetString("state_2"));
            dictStates.Add(3, rm.GetString("state_3"));
            dictStatesHost.Add(0, rm.GetString("state_0"));
            dictStatesHost.Add(1, rm.GetString("state_2"));
            dictStatesHost.Add(2, rm.GetString("state_2"));
            dictStatesHost.Add(3, rm.GetString("state_2"));

            //Import color settings
            color_up_ok = Properties.Settings.Default.color_up_ok;
            color_unreach_warn = Properties.Settings.Default.color_unreach_warn;
            color_down_crit = Properties.Settings.Default.color_down_crit;
            color_unknown = Properties.Settings.Default.color_unknown;
            dictColor.Add(0, new BusylightColor
            {
                RedRgbValue = color_up_ok.R,
                GreenRgbValue = color_up_ok.G,
                BlueRgbValue = color_up_ok.B
            });
            dictColor.Add(1, new BusylightColor
            {
                RedRgbValue = color_unreach_warn.R,
                GreenRgbValue = color_unreach_warn.G,
                BlueRgbValue = color_unreach_warn.B
            });
            dictColor.Add(2, new BusylightColor
            {
                RedRgbValue = color_down_crit.R,
                GreenRgbValue = color_down_crit.G,
                BlueRgbValue = color_down_crit.B
            });
            dictColor.Add(3, new BusylightColor
            {
                RedRgbValue = color_unknown.R,
                GreenRgbValue = color_unknown.G,
                BlueRgbValue = color_unknown.B
            });
            dictColorHost.Add(0, new BusylightColor
            {
                RedRgbValue = color_up_ok.R,
                GreenRgbValue = color_up_ok.G,
                BlueRgbValue = color_up_ok.B
            });
            dictColorHost.Add(1, new BusylightColor
            {
                RedRgbValue = color_down_crit.R,
                GreenRgbValue = color_down_crit.G,
                BlueRgbValue = color_down_crit.B
            });
            dictColorHost.Add(2, new BusylightColor
            {
                RedRgbValue = color_down_crit.R,
                GreenRgbValue = color_down_crit.G,
                BlueRgbValue = color_down_crit.B
            });
            dictColorHost.Add(3, new BusylightColor
            {
                RedRgbValue = color_down_crit.R,
                GreenRgbValue = color_down_crit.G,
                BlueRgbValue = color_down_crit.B
            });

            //Import sound settings
            sound = Properties.Settings.Default.sound;
            volume = Properties.Settings.Default.sound_volume;
            sound_file = Properties.Settings.Default.sound_file;

            //Initialize tray icon
            trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.icinga,
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
            if (Properties.Settings.Default.balloon_enable_start == true)
            {
                showTip(rm.GetString("welcome_title"),
                    "Icinga Busylight Agent " + ver.ToString() + " " +
                rm.GetString("welcome_message"), ToolTipIcon.Info);
            }

            //Initialize log
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Application startup completed at {0}", DateTime.Now.ToString()), Properties.Settings.Default.log_level, 2);
        }

        void initializeThread()
        {
            //Create and (re-)start data thread
            workerObject = new Icinga2Client(
                Properties.Settings.Default.icinga_url,
                Properties.Settings.Default.icinga_user,
                Properties.Settings.Default.icinga_pass,
                (Properties.Settings.Default.icinga_update_interval * 1000 * 60)
                );

            //Update _all_ the data
            dataThread = new Thread(workerObject.updateData);
            //Set update event
            workerObject.complete += WorkerObject_complete;
            workerObject.inProgress += WorkerObject_inProgress;
        }

        private void WorkerObject_inProgress()
        {
            //Update in progress
            trayIcon.Icon = Properties.Resources.icinga_update;
        }

        private void WorkerObject_complete(Dictionary<string, int> hosts, Dictionary<string, List<string>> services)
        {
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Data update retrieved by UI class", Properties.Settings.Default.log_level, 2);
            trayIcon.Icon = Properties.Resources.icinga;
            //Set data and alert failures
            failHosts = hosts;
            failServices = services;
            alertInventory();
        }

        private void annoyUser(BusylightColor myColor)
        {
            //Play sound
            if (this.sound_file != "")
            {
                player = new SoundPlayer(sound_file);
                player.Play();
            }

            //Flash Busylight and play sound
            var controller = new Busylight.SDK();
            controller.Jingle(myColor, sound, volume);
            Thread.Sleep(5000);
            controller.Terminate();
        }

        private void alertInventory()
        {
            //Scans the inventory and alerts
            if (failHosts.Count + failServices.Count > Properties.Settings.Default.spam_thres)
            {
                //The shit just hit the fan, gross!
                showTip(rm.GetString("balloon_chaos_title"),
                    rm.GetString("balloon_chaos_msg").Replace("X", failHosts.Count.ToString())
                    .Replace("Y", failServices.Count.ToString()),
                    ToolTipIcon.Warning);
            }
            else
            {

                //Hosts
                try
                {
                    foreach (string host in failHosts.Keys)
                    {
                        if (Properties.Settings.Default.balloon_enable == true)
                        {
                            //Show tooltip if enabled
                            showTip(string.Format("Host {0}", dictStatesHost[failHosts[host]].ToUpper()), string.Format("Host '{0}' {1}", host, dictStatesHost[failHosts[host]]), ToolTipIcon.Error);
                        }

                        SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode,
                            string.Format("Alarm: Host '{0}' is '{1}'", host, dictStatesHost[failHosts[host]]),
                            Properties.Settings.Default.log_level, 2);

                        annoyUser(dictColorHost[failHosts[host]]);
                    }

                    //Services
                    foreach (string host in failServices.Keys)
                    {
                        //Only report if host not generally down
                        if(failHosts.ContainsKey(host) == false)
                        {
                            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode,
                                string.Format("Host '{0}' not blacklisted!", host),
                                Properties.Settings.Default.log_level, 2);

                            //Report service
                            foreach (string service in failServices[host])
                            {
                                try
                                {
                                    //Extract state and service
                                    int thisState = Convert.ToInt32(service.Substring(service.IndexOf(';') + 1));
                                    string thisService = service.Substring(service.IndexOf('!') + 1);
                                    thisService = thisService.Substring(0, thisService.IndexOf(';'));

                                    SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode,
                                        string.Format("Alarm: '{0}' on '{1}' is '{2}'", thisService, host, dictStates[thisState]),
                                        Properties.Settings.Default.log_level, 2);

                                    //Show tooltip if enabled
                                    if (Properties.Settings.Default.balloon_enable == true)
                                    {
                                        showTip(string.Format("Service {0}", dictStates[thisState].ToUpper()),
                                        string.Format("Service '{0}' {1} '{2}' {3}", thisService, rm.GetString("state_on"), host, dictStates[thisState]), dictIcons[thisState]);
                                    }

                                    annoyUser(dictColor[thisState]);
                                }
                                catch (FormatException e)
                                {
                                    //Format error
                                    SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Format error: '{0}'", e.Message), Properties.Settings.Default.log_level, 2);
                                }
                            }
                        }
                        else
                        {
                            //Ignoring host
                            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Ignoring host '{0}' as it's down", host), Properties.Settings.Default.log_level, 2);
                        }
                    }
                }
                catch (InvalidOperationException e)
                {
                    SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Invalid operation: '{0}'", e.Message), Properties.Settings.Default.log_level, 2);
                }
            }
            //TODO: Reset to Skype state
        }

        private void update(object sender, EventArgs e)
        {
            //Update data thread
            try
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Thread state is: '{0}'", dataThread.ThreadState), Properties.Settings.Default.log_level, 2);
                if (dataThread.ThreadState == ThreadState.WaitSleepJoin || dataThread.ThreadState == ThreadState.Running)
                {
                    //already running
                    MessageBox.Show(rm.GetString("msgbox_update_running"), rm.GetString("msgbox_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dataThread.ThreadState == ThreadState.Stopped)
                {
                    //Start over
                    dataThread = new Thread(workerObject.updateData);
                    dataThread.Start();
                }
                else { dataThread.Start(); }
                }
            catch(ThreadStateException exc)
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Error with thread handling: '{0}'", exc.Message), Properties.Settings.Default.log_level, 2);
            }
        }

        private void about(object sender, EventArgs e)
        {
            //Show about dialog
            FormAbout form_about = new FormAbout();
            form_about.Show();
        }
        
        private void configure(object sender, EventArgs e)
        {
            //Show configuration dialog
            FormSettings form_conf = new FormSettings();
            var dialogResult = form_conf.ShowDialog();

            //Reload thread
            initializeThread();
        }

        private void exit(object sender, EventArgs e)
        {
            //Hide tray icon and die in a fire
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
