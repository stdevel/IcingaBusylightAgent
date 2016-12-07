namespace IcingaBusylightAgent
{
    partial class FormAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.llabel_icinga = new System.Windows.Forms.LinkLabel();
            this.llabel_plenom = new System.Windows.Forms.LinkLabel();
            this.llabel_stackoverflow = new System.Windows.Forms.LinkLabel();
            this.llabel_github = new System.Windows.Forms.LinkLabel();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.TabStop = false;
            // 
            // llabel_icinga
            // 
            resources.ApplyResources(this.llabel_icinga, "llabel_icinga");
            this.llabel_icinga.Name = "llabel_icinga";
            this.llabel_icinga.TabStop = true;
            this.llabel_icinga.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llabel_icinga_LinkClicked);
            // 
            // llabel_plenom
            // 
            resources.ApplyResources(this.llabel_plenom, "llabel_plenom");
            this.llabel_plenom.Name = "llabel_plenom";
            this.llabel_plenom.TabStop = true;
            this.llabel_plenom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llabel_plenom_LinkClicked);
            // 
            // llabel_stackoverflow
            // 
            resources.ApplyResources(this.llabel_stackoverflow, "llabel_stackoverflow");
            this.llabel_stackoverflow.Name = "llabel_stackoverflow";
            this.llabel_stackoverflow.TabStop = true;
            this.llabel_stackoverflow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llabel_stackoverflow_LinkClicked);
            // 
            // llabel_github
            // 
            resources.ApplyResources(this.llabel_github, "llabel_github");
            this.llabel_github.Name = "llabel_github";
            this.llabel_github.TabStop = true;
            this.llabel_github.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llabel_github_LinkClicked);
            // 
            // lbl_Version
            // 
            resources.ApplyResources(this.lbl_Version, "lbl_Version");
            this.lbl_Version.Name = "lbl_Version";
            // 
            // FormAbout
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_Version);
            this.Controls.Add(this.llabel_github);
            this.Controls.Add(this.llabel_stackoverflow);
            this.Controls.Add(this.llabel_plenom);
            this.Controls.Add(this.llabel_icinga);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.LinkLabel llabel_icinga;
        private System.Windows.Forms.LinkLabel llabel_plenom;
        private System.Windows.Forms.LinkLabel llabel_stackoverflow;
        private System.Windows.Forms.LinkLabel llabel_github;
        private System.Windows.Forms.Label lbl_Version;
    }
}