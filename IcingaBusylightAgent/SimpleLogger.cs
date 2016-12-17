using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//StreamWriter
using System.IO;
//EventLog
using System.Diagnostics;
using System.Security;
//MessageBox
using System.Windows.Forms;
//Localization
using System.Resources;

namespace IcingaBusylightAgent
{
    public enum LogTarget
    {
        File, EventLog, Console
    }

    //SimpleLogger class
    public abstract class SimpleLogger
    {
        //Proudly inspired by: http://www.infoworld.com/article/2980677/application-architecture/implement-a-simple-logger-in-c.html

        //Translate _all_ the strings!
        ResourceManager rm = Strings.ResourceManager;

        protected readonly object lockObj = new object();
        public abstract void Log(String message, int filterType, int messageType);
        //log levels: 0: error, 1: info, 2: debug

        public class FileLogger: SimpleLogger
        {
            //Create logfile in current working directory
            public String filePath = AppDomain.CurrentDomain.BaseDirectory + "IcingaBusylightAgent.log";

            public override void Log(string message, int filterType, int messageType)
            {
                //Log to file
                Dictionary<int, string> logType = new Dictionary<int, string>()
                {
                    { 0, "ERROR" },
                    { 1, "INFO" },
                    { 2, "DEBUG" }
                };

                if (messageType <= filterType)
                {
                    lock (lockObj)
                    {
                        using (StreamWriter streamWriter = new StreamWriter(filePath, true))
                        {
                            //Write message and die in a fire
                            streamWriter.WriteLine(String.Format("{0}: {1}", logType[messageType], message));
                            streamWriter.Close();
                        }
                    }
                }
            }
        }

        public class EventLogger: SimpleLogger
        {
            public override void Log(string message, int filterType, int messageType)
            {
                //Log to EventLog
                Dictionary<int, EventLogEntryType> logType = new Dictionary<int, EventLogEntryType>()
                {
                    { 0, EventLogEntryType.Error },
                    { 1, EventLogEntryType.Information },
                    { 2, EventLogEntryType.Information }
                };
                lock (lockObj)
                {
                    try
                    {
                        EventLog eventlog = new EventLog("Application");
                        eventlog.Source = "IcingaBusylightAgent";
                        eventlog.WriteEntry(message, logType[messageType]);
                    }
                    catch(SecurityException)
                    {
                        //Unable to write to EventLog
                        MessageBox.Show(rm.GetString("msgbox_eventlog_unavailable"), rm.GetString("msgbox_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public class ConsoleLogger: SimpleLogger
        {
            public override void Log(string message, int filterType, int messageType)
            {
                //Log to console
                Dictionary<int, string> logType = new Dictionary<int, string>()
                {
                    { 0, "ERROR" },
                    { 1, "INFO" },
                    { 2, "DEBUG" }
                };
                lock (lockObj)
                {
                    if (messageType <= filterType) { System.Console.WriteLine(String.Format("{0}: {1}", logType[messageType], message)); }
                }
            }
        }
    }

    //SimpleLoggerHelper class
    public static class SimpleLoggerHelper
    {
        private static SimpleLogger logger = null;

        public static void Log(string target, string message, int filterType, int messageType=0)
        {
            switch (target)
            {
                case "file":
                    //File logger
                    logger = new SimpleLogger.FileLogger();
                    logger.Log(message, filterType, messageType);
                    break;
                case "eventlog":
                    //EventLog logger
                    logger = new SimpleLogger.EventLogger();
                    logger.Log(message, filterType, messageType);
                    break;
                case "console":
                    //Console logger
                    logger = new SimpleLogger.ConsoleLogger();
                    logger.Log(message, filterType, messageType);
                    break;
                default:
                    //Die in a fire
                    return;
            }
        }
    }
}
