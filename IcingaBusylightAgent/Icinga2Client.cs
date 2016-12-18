using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//HTML
using System.Net;
using System.IO;
//Timer
using System.Threading;
//JSON
using Newtonsoft.Json;
using System.Data;
//Busylight
using Busylight;
using System.Drawing;
//MessageBox
using System.Windows.Forms;
//Sound
using System.Media;
//Localization
using System.Resources;

namespace IcingaBusylightAgent
{
    //Pseudo-class for deserializing api datasets
    public class apiDataset
    {
        public apiDatasetAttrs attrs { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
    //Pseudo-class for deserializing result attributes
    public class apiDatasetAttrs
    {
        public double acknowledgement { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public double state { get; set; }
    }

    class Icinga2Client
    {
        //Translate _all_ the strings!
        ResourceManager rm = Strings.ResourceManager;

        //Icinga2 variables
        private string url;
        private string username;
        private string password;
        private int updateInterval;
        private System.Threading.Timer updateTimer;
        private string sound_file = "";
        private SoundPlayer player;

        //Notification colors
        Dictionary<int, Busylight.BusylightColor> dictColor = new Dictionary<int, Busylight.BusylightColor>();

        //Notification variables
        Color color_up_ok, color_down_crit, color_unreach_warn, color_unknown;
        BusylightJingleClip sound;
        BusylightVolume volume;

        //Some Icinga2 getters/setters
        public void setUrl(string new_url) { this.url = new_url; }
        public string getUrl() { return this.url; }
        public void setUsername(string new_user) { this.username = new_user; }
        public string getUsername() { return this.username; }
        public void setPassword(string new_pass) { this.password = new_pass; }
        public string getPassword() { return this.password; }
        public void setInterval(int new_interval) { this.updateInterval = new_interval; }
        public int getInterval() { return this.updateInterval; }

        /*
 * 
 * 
 *                                 if (entry.attrs.state == 3)
                        {
                            //unknown
                            targetColor = new BusylightColor { RedRgbValue = this.color_unknown.R, GreenRgbValue = this.color_unknown.G, BlueRgbValue = this.color_unknown.B };
                        }
                        else if (entry.attrs.state == 2)
                        {
                            //critical
                            targetColor = new BusylightColor { RedRgbValue = this.color_down_crit.R, GreenRgbValue = this.color_down_crit.G, BlueRgbValue = this.color_down_crit.B };
                        }
                        else
                        {
                            //warning
                            targetColor = new BusylightColor { RedRgbValue = this.color_unreach_warn.R, GreenRgbValue = this.color_unreach_warn.G, BlueRgbValue = this.color_unreach_warn.B };
                        }
 * */

        //Some notification getters/setters
        public void setColorUpOk(Color new_color)
        {
            this.color_up_ok = new_color;
            //Add dictionary entry
            dictColor.Add(0, new BusylightColor { RedRgbValue = new_color.R, GreenRgbValue = new_color.G, BlueRgbValue = new_color.B });
        }
        public Color getColorUpOk() { return this.color_up_ok; }
        public void setColorDownCrit(Color new_color)
        {
            this.color_down_crit = new_color;
            //Add dictionary entry
            dictColor.Add(2, new BusylightColor { RedRgbValue = new_color.R, GreenRgbValue = new_color.G, BlueRgbValue = new_color.B });
        }
        public Color getColorDownCrit() { return this.color_down_crit; }
        public void setColorUnreachWarn(Color new_color)
        {
            this.color_unreach_warn = new_color;
            //Add dictionary entry
            dictColor.Add(1, new BusylightColor { RedRgbValue = new_color.R, GreenRgbValue = new_color.G, BlueRgbValue = new_color.B });
        }
        public Color getColorUnreachWarn() { return this.color_unreach_warn; }
        public void setColorUnknown(Color new_color)
        {
            this.color_unknown = new_color;
            //Add dictionary entry
            dictColor.Add(3, new BusylightColor { RedRgbValue = new_color.R, GreenRgbValue = new_color.G, BlueRgbValue = new_color.B });
        }
        public Color getColorUnknown() { return this.color_unknown; }
        public void setSound(BusylightJingleClip new_sound) { this.sound = new_sound; }
        public BusylightJingleClip getSound() { return this.sound; }
        public void setVolume(BusylightVolume new_vol) { this.volume = new_vol; }
        public BusylightVolume getVolume() { return this.volume; }
        public void setSoundfile(string new_file)
        {
            if (new_file != "")
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Notification sound set to: '{0}'", new_file), Properties.Settings.Default.log_level, 2);
                this.sound_file = new_file;
                player = new SoundPlayer(new_file);
            }
        }
        public string getSoundfile() { return this.sound_file; }

        public Icinga2Client(string url, string username, string password, int interval,
            Color upOk, Color downCrit, Color unreach, Color unknown,
            BusylightJingleClip sound, BusylightVolume volume
            )
        {
            //Set Icinga2 API client information
            setUrl(url);
            setUsername(username);
            setPassword(password);
            setInterval(interval);

            //Set notification information
            setColorUpOk(upOk);
            setColorDownCrit(downCrit);
            setColorUnreachWarn(unreach);
            setColorUnknown(unknown);
            setSound(sound);
            setVolume(volume);

            //Set timer
            updateTimer = new System.Threading.Timer(updateData, null, interval, interval);
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Yes, this is Icinga2Client: URL='{0}', username='{1}', interval='{2}'", getUrl(), getUsername(), getInterval()), Properties.Settings.Default.log_level, 2);

            //Enable SoundPlayer
            if (this.sound_file != "") { player = new SoundPlayer(this.sound_file); }

            //INVENTORY TEST
            //getInventory("HostGroup");
            //getInventory("Host", "", new string[] { "name", "display_name", "state", "acknowledgement" });
        }

        private CredentialCache createCredentials()
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(IcingaBusylightAgent.Properties.Settings.Default.icinga_url), "Basic", new NetworkCredential(
                IcingaBusylightAgent.Properties.Settings.Default.icinga_user,
                IcingaBusylightAgent.Properties.Settings.Default.icinga_pass
                ));
            return credentialCache;
        }

        public List<apiDataset> getInventory(string type = "Hosts", string filter = "", string[] attributes = null)
        {
            //Set default attributes if none given
            attributes = attributes ?? new string[] { "name", "display_name" };

            //Return hostgroups
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Retrieving objects '{0}' with filter '{1}' and attributes '{2}'", type, filter, string.Join(", ", attributes)), Properties.Settings.Default.log_level, 2);
            string result = "";
            string post = "";

            try
            {
                //Setup URL prefix based on type
                string url_prefix="";
                switch(type)
                {
                    case "Host":
                        url_prefix = "hosts";
                        break;
                    case "HostGroup":
                        url_prefix = "hostgroups";
                        break;
                    case "Service":
                        url_prefix = "services";
                        break;
                }
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("URL prefix is '{0}'", url_prefix), Properties.Settings.Default.log_level, 2);

                //Get result
                string attrs = "";
                foreach(string attribute in attributes)
                {
                    if (attrs == "") { attrs = string.Format("\"{0}\"", attribute); }
                    else { attrs = string.Format("{0}, \"{1}\"", attrs, attribute); }
                }

                //Set POST data
                post = "{ \"type\": \"" + type + "\"";
                if(filter != "") { post = post + ", \"filter\": \"" + filter + "\""; }
                post = post + ", \"attrs\": [ " + attrs + " ] }";
                /*post = string.Format("{ \"type\": \"{0}\"", type);
                if(filter != "") { post = string.Format("{0}, \"filter\": \"{1}\"", post, filter); }
                post = string.Format("{0}, \"attrs\": [ {1} ] }", post, attrs);*/
                //Get result
                result = getHTMLPostResult(this.url + "v1/objects/" + url_prefix, post);
                //BOO: Removing root level as I'm too lame to do this nicer...
                result = result.Substring(11, (result.Length - 12));
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("RESULT: '{0}'", result), Properties.Settings.Default.log_level, 2);
                //Try to deserialize objects
                var datasetList = JsonConvert.DeserializeObject<List<apiDataset>>(result);
                foreach (apiDataset entry in datasetList)
                {
                    //dump result
                    SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Name: '{0}', Type: '{1}', Display Name: '{2}', State: '{3}', Acknowledgement: '{4}'",
                        entry.name, entry.type, entry.attrs.display_name, entry.attrs.state, entry.attrs.acknowledgement), Properties.Settings.Default.log_level, 2);
                }
                return datasetList;
            }
            catch (UriFormatException e)
            {
                //Connection could not be openend - URL invalid/host down?
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Invalid URL ({0}) (Host unreachable?) - error: {1}", this.url + "v1/objects/hosts", e.Message), Properties.Settings.Default.log_level);
                return null;
            }
            catch (ArgumentOutOfRangeException e)
            {
                //No hosts
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("No hosts found matching conditions! ('{0}', '{1}')", e.Message, result), Properties.Settings.Default.log_level);
                return null;
            }
            catch (Exception e)
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Generic error: '{0}'", e.Message), Properties.Settings.Default.log_level, 0);
                return null;
            }
        }

        private string getHTMLPostResult(string url, string content)
        {
            //This function is proudly inspired by: http://stackoverflow.com/questions/16642196/get-html-code-from-website-in-c-sharp
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("POST: '{0}' (Content: '{1}')", url, content), Properties.Settings.Default.log_level, 2);

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            try
            {
                //Create request
                HttpWebRequest request = HttpWebRequest.CreateHttp(url);
                
                //Set headers
                request.Headers["X-HTTP-Method-Override"] = "GET";
                request.Method = WebRequestMethods.Http.Post;

                //Set user-agent and credentials
                //Proudly inspired by: http://stackoverflow.com/questions/4334521/c-sharp-httpwebrequest-using-basic-authentication
                request.UserAgent = "IcingaBusylightAgent";
                request.Credentials = createCredentials();

                //Set payload and length
                byte[] contentArray = Encoding.UTF8.GetBytes(content);
                request.ContentLength = contentArray.Length;
                request.ContentType = "application/json";

                //Write to stream
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(contentArray, 0, contentArray.Length);
                dataStream.Close();

                //Get response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Return string if valid result
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //Create stream reader
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    //Try to guess character set
                    if (response.CharacterSet == "") { readStream = new StreamReader(receiveStream); }
                    else { readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)); }

                    //Read data and close streams
                    string data = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                    return data;
                }
                else
                {
                    //Impossibru!
                    return null;
                }
            }
            catch (System.Net.WebException e)
            {
                //Could not access information
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Could not access information, error: {0}", e.Message), Properties.Settings.Default.log_level);
                return null;
            }
            catch (System.ArgumentException e)
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Error: {0}", e.Message), Properties.Settings.Default.log_level);
                return null;
            }
            catch (Exception e)
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Generic error: {0}", e.Message), Properties.Settings.Default.log_level);
                return null;
            }
        }

        private List<apiDataset> updateHosts()
        {
            //Update host information
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Updating host information"), Properties.Settings.Default.log_level, 2);
            return getInventory("Host", "host.state!=00 && host.acknowledgement==0", new String[] { "name", "state", "acknowledgement" });
        }

        private List<apiDataset> updateServices()
        {
            //Update service information
            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Updating service information", Properties.Settings.Default.log_level, 2);
            return getInventory("Service", "service.state!=00 && service.acknowledgement==0", new String[] { "name", "state", "acknowledgement" });
        }

        public void updateData(object state)
        {
            //Update data

            try
            {
                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, "Updating data thread...", Properties.Settings.Default.log_level, 2);

                lock (this)
                {
                    //Initializing Busylight
                    var controller = new Busylight.SDK();

                    //Check host information if requested
                    if (Properties.Settings.Default.icinga_check_hosts == true)
                    {
                        try
                        {
                            //Get host information
                            List<apiDataset> hostData = updateHosts();
                            foreach (apiDataset entry in hostData)
                            {
                                //Unacknowledged alert
                                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("UNACKNOWLEDGED HOST FAILURE!!! - Name: '{0}', State: '{1}', Acknowledgement: '{2}'",
                                    entry.name, entry.attrs.state, entry.attrs.acknowledgement), Properties.Settings.Default.log_level, 1);

                                //Play sound
                                if (this.sound_file != "") { player.Play(); }
                                //Flash light
                                controller.Jingle(dictColor[Convert.ToInt32(entry.attrs.state)], this.sound, this.volume);
                                Thread.Sleep(5000);
                                controller.Terminate();
                            }
                        }
                        catch (ArgumentNullException e)
                        {
                            //Empty dataset
                            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Empty dataset: '{0}' - so, no faults? :-)", e.Message), Properties.Settings.Default.log_level, 2);
                        }
                        catch (NullReferenceException e)
                        {
                            //Empty dataset
                            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Empty dataset: '{0}' - so, no faults? :-)", e.Message), Properties.Settings.Default.log_level, 2);
                        }
                    }

                    //Check service information if requested
                    if (Properties.Settings.Default.icinga_check_services == true)
                    {
                        try
                        {

                            //Get service information
                            List<apiDataset> serviceData = updateServices();
                            foreach (apiDataset entry in serviceData)
                            {
                                //Unacknowledged alert
                                SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("UNACKNOWLEDGED SERVICE FAILURE!!! Name: '{0}', Type: '{1}', State: '{2}', Acknowledgement: '{3}', Raw Service: '{4}'",
                                    entry.name, entry.type, entry.attrs.state, entry.attrs.acknowledgement, entry.attrs.name), Properties.Settings.Default.log_level, 1);

                                //Play sound
                                if (this.sound_file != "") { player.Play(); }
                                //Flash light
                                controller.Jingle(dictColor[Convert.ToInt32(entry.attrs.state)], this.sound, this.volume);
                                Thread.Sleep(5000);
                                controller.Terminate();
                            }
                        }
                        catch (ArgumentNullException e)
                        {
                            //Empty dataset
                            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Empty dataset: '{0}' - so, no faults? :-)", e.Message), Properties.Settings.Default.log_level, 2);
                        }
                        catch (NullReferenceException e)
                        {
                            //Empty dataset
                            SimpleLoggerHelper.Log(Properties.Settings.Default.log_mode, string.Format("Empty dataset: '{0}' - so, no faults? :-)", e.Message), Properties.Settings.Default.log_level, 2);
                        }
                    }

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

    }
}