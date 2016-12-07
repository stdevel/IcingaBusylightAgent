using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Version
using System.Reflection;
//Link
using System.Diagnostics;

namespace IcingaBusylightAgent
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            //Set links
            //LinkLabel.Link = new Link
            llabel_icinga.Links.Add(0, 100, "http://www.icinga.org");
            llabel_plenom.Links.Add(0, 100, "http://www.plenom.com");
            llabel_stackoverflow.Links.Add(0, 100, "http://www.stackoverflow.com");
            llabel_github.Links.Add(0, 100, "https://github.com/stdevel/IcingaBusylightAgent");

            //Set version
            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;
            lbl_Version.Text = "Version: " + ver.ToString();
        }

        private void llabel_icinga_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Start browser
            Process.Start(e.Link.LinkData as string);
        }

        private void llabel_plenom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Start browser
            Process.Start(e.Link.LinkData as string);
        }

        private void llabel_stackoverflow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Start browser
            Process.Start(e.Link.LinkData as string);
        }

        private void llabel_github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Start browser
            Process.Start(e.Link.LinkData as string);
        }
    }
}
