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
        public String name { get; set; }
        public double state { get; set; }
    }

    class Icinga2Client
    {
        //Icinga2 variables
        private String url;
        private String username;
        private String password;
        private int updateInterval;
        private Timer updateTimer;

        //Notification variables
        Color color_up_ok, color_down_crit, color_unreach_warn, color_unknown;
        BusylightJingleClip sound;
        BusylightVolume volume;

        //Some Icinga2 getters/setters
        public void setUrl(String new_url) { this.url = new_url; }
        public void setUsername(String new_user) { this.username = new_user; }
        public String getUsername() { return this.username; }
        public void setPassword(String new_pass) { this.password = new_pass; }
        public String getPassword() { return this.password; }
        public void setInterval(int new_interval) { this.updateInterval = new_interval; }
        public int getInterval() { return this.updateInterval; }

        //Some notification getters/setters
        public void setColorUpOk(Color new_color) { this.color_up_ok = new_color; }
        public Color getColorUpOk() { return this.color_up_ok; }
        public void setColorDownCrit(Color new_color) { this.color_down_crit = new_color; }
        public Color getColorDownCrit() { return this.color_down_crit; }
        public void setColorUnreachWarn(Color new_color) { this.color_unreach_warn = new_color; }
        public Color getColorUnreachWarn() { return this.color_unreach_warn; }
        public void setColorUnknown(Color new_color) { this.color_unknown = new_color; }
        public Color getColorUnknown() { return this.color_unknown; }
        public void setSound(BusylightJingleClip new_sound) { this.sound = new_sound; }
        public BusylightJingleClip getSound() { return this.sound; }
        public void setVolume(BusylightVolume new_vol) { this.volume = new_vol; }
        public BusylightVolume getVolume() { return this.volume; }

        public Icinga2Client(String url, String username, String password, int interval,
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

            System.Console.WriteLine("Color warn RGB: {0}/{1}/{2}", this.color_unreach_warn.R, this.color_unreach_warn.G, this.color_unreach_warn.B);

            //Test: fix broken colors
            /*this.color_up_ok = Color.Green;
            this.color_unreach_warn = Color.OrangeRed;
            this.color_down_crit = Color.Red;
            this.color_unknown = Color.Fuchsia;*/

            //Set timer
            updateTimer = new System.Threading.Timer(updateData, null, interval, interval);
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

        private String getHTMLResult(String url)
        {
            //This function is proudly inspired by: http://stackoverflow.com/questions/16642196/get-html-code-from-website-in-c-sharp

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            //Create request
            HttpWebRequest request = HttpWebRequest.CreateHttp(url);

            //Ignore errors forced by self-signed certificates
            //Proudly inspired by: http://stackoverflow.com/questions/12506575/how-to-ignore-the-certificate-check-when-ssl
            //request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

            //Set user-agent and credentials
            //Proudly inspired by: http://stackoverflow.com/questions/4334521/c-sharp-httpwebrequest-using-basic-authentication
            request.UserAgent = "IcingaBusylightAgent";
            request.Credentials = createCredentials();

            //Get response
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Return String if valid result
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
                    return "FAIL";
                }
            }
            catch(System.Net.WebException e)
            {
                //Could not access information
                System.Console.WriteLine("Could not access information, error: {0}", e.Message);
                return "FAIL";
            }
            catch(System.ArgumentException e)
            {
                System.Console.WriteLine("Error: {0}", e.Message);
                return "FAIL";
            }
            catch
            {
                //Impossibru!
                return "FAIL";
            }
        }

        public void updateData(object state)
        {
            //Update data
            BusylightColor targetColor;

            lock (this)
            {
                System.Console.WriteLine("Updating data thread...");
                //Initializing Busylight
                var controller = new Busylight.SDK();   

                try
                {
                    string result = getHTMLResult(this.url + "v1/objects/services?filter=service.state!=0&attrs=name&attrs=state&attrs=acknowledgement");
                    //BOO: Removing root level as I'm too lame to do this nicer...
                    result = result.Substring(11, (result.Length-12));
                    System.Console.WriteLine("GET: {0}", this.url + "v1/objects/services?filter=service.state!=0&attrs=name&attrs=state&attrs=acknowledgement");
                    System.Console.WriteLine(result);
                    //Try to deserialize objects
                    var test = JsonConvert.DeserializeObject<List<apiDataset>>(result);
                    foreach(apiDataset entry in test)
                    {
                        //dump result
                        System.Console.WriteLine("Name: '{0}', Type: '{1}', State: '{2}', Acknowledgement: '{3}', Raw Service: '{4}'", entry.name, entry.type, entry.attrs.state, entry.attrs.acknowledgement, entry.attrs.name);
                    }
                    //filter for non-acknowledged information
                    //Proudly inspired by: http://stackoverflow.com/questions/26196/filtering-collections-in-c-sharp
                    foreach (apiDataset entry in test.Where(entry => (entry.attrs.acknowledgement == 0.0)))
                    {
                        //dump result
                        System.Console.WriteLine("UNACKNOWLEDGED!!! Name: '{0}', Type: '{1}', State: '{2}', Acknowledgement: '{3}', Raw Service: '{4}'", entry.name, entry.type, entry.attrs.state, entry.attrs.acknowledgement, entry.attrs.name);
                        if(entry.attrs.state == 3)
                        {
                            //unknown
                            targetColor = new BusylightColor { RedRgbValue = this.color_unknown.R, GreenRgbValue = this.color_unknown.G, BlueRgbValue = this.color_unknown.B };
                        }
                        else if(entry.attrs.state == 2)
                        {
                            //critical
                            targetColor = new BusylightColor { RedRgbValue = this.color_down_crit.R, GreenRgbValue = this.color_down_crit.G, BlueRgbValue = this.color_down_crit.B };
                        }
                        else
                        {
                            //warning
                            targetColor = new BusylightColor { RedRgbValue = this.color_unreach_warn.R, GreenRgbValue = this.color_unreach_warn.G, BlueRgbValue = this.color_unreach_warn.B };
                        }
                        System.Console.WriteLine("Target color is R/G/B: {0}/{1}/{2}", targetColor.RedRgbValue, targetColor.GreenRgbValue, targetColor.BlueRgbValue);
                        controller.Jingle(targetColor, this.sound, this.volume);
                        Thread.Sleep(5000);
                        controller.Terminate();
                    }
                }
                catch (UriFormatException e)
                {
                    //Connection could not be openend - URL invalid/host down?
                    System.Console.WriteLine("Invalid URL ({0}) (Host unreachable?) - error: {1}", this.url+"v1/objects/hosts", e.Message);
                }
            }
        }

    }
}
