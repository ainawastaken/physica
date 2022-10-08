namespace physica.projWind
{
    partial class projWind
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
            this.components = new System.ComponentModel.Container();
            this.tabControl12 = new System.Windows.Forms.TabControl();
            this.openProjectTab = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OPopenRecentProject = new System.Windows.Forms.Button();
            this.OPreloadRecentProjecs = new System.Windows.Forms.Button();
            this.OPrecentProjectsList = new System.Windows.Forms.ListBox();
            this.OPpathTextBox = new System.Windows.Forms.TextBox();
            this.OPpathButton = new System.Windows.Forms.Button();
            this.newProjectTab = new System.Windows.Forms.TabPage();
            this.NPcreateProject = new System.Windows.Forms.Button();
            this.NPpathTextBox = new System.Windows.Forms.TextBox();
            this.NPopenPath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.NPnameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl12.SuspendLayout();
            this.openProjectTab.SuspendLayout();
            this.newProjectTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl12
            // 
            this.tabControl12.Controls.Add(this.openProjectTab);
            this.tabControl12.Controls.Add(this.newProjectTab);
            this.tabControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl12.Location = new System.Drawing.Point(0, 0);
            this.tabControl12.Name = "tabControl12";
            this.tabControl12.SelectedIndex = 0;
            this.tabControl12.Size = new System.Drawing.Size(305, 183);
            this.tabControl12.TabIndex = 0;
            // 
            // openProjectTab
            // 
            this.openProjectTab.Controls.Add(this.label2);
            this.openProjectTab.Controls.Add(this.label1);
            this.openProjectTab.Controls.Add(this.OPopenRecentProject);
            this.openProjectTab.Controls.Add(this.OPreloadRecentProjecs);
            this.openProjectTab.Controls.Add(this.OPrecentProjectsList);
            this.openProjectTab.Controls.Add(this.OPpathTextBox);
            this.openProjectTab.Controls.Add(this.OPpathButton);
            this.openProjectTab.Location = new System.Drawing.Point(4, 22);
            this.openProjectTab.Name = "openProjectTab";
            this.openProjectTab.Padding = new System.Windows.Forms.Padding(3);
            this.openProjectTab.Size = new System.Drawing.Size(297, 157);
            this.openProjectTab.TabIndex = 0;
            this.openProjectTab.Text = "Open project";
            this.openProjectTab.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Recently opened:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Path:";
            // 
            // OPopenRecentProject
            // 
            this.OPopenRecentProject.BackgroundImage = global::physica.Properties.Resources.load;
            this.OPopenRecentProject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OPopenRecentProject.Enabled = false;
            this.OPopenRecentProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OPopenRecentProject.Location = new System.Drawing.Point(259, 115);
            this.OPopenRecentProject.Name = "OPopenRecentProject";
            this.OPopenRecentProject.Size = new System.Drawing.Size(32, 32);
            this.OPopenRecentProject.TabIndex = 5;
            this.OPopenRecentProject.UseVisualStyleBackColor = true;
            // 
            // OPreloadRecentProjecs
            // 
            this.OPreloadRecentProjecs.BackgroundImage = global::physica.Properties.Resources.relo;
            this.OPreloadRecentProjecs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OPreloadRecentProjecs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OPreloadRecentProjecs.Location = new System.Drawing.Point(259, 65);
            this.OPreloadRecentProjecs.Name = "OPreloadRecentProjecs";
            this.OPreloadRecentProjecs.Size = new System.Drawing.Size(32, 32);
            this.OPreloadRecentProjecs.TabIndex = 4;
            this.OPreloadRecentProjecs.UseVisualStyleBackColor = true;
            // 
            // OPrecentProjectsList
            // 
            this.OPrecentProjectsList.FormattingEnabled = true;
            this.OPrecentProjectsList.Location = new System.Drawing.Point(6, 65);
            this.OPrecentProjectsList.Name = "OPrecentProjectsList";
            this.OPrecentProjectsList.Size = new System.Drawing.Size(247, 82);
            this.OPrecentProjectsList.TabIndex = 3;
            this.OPrecentProjectsList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // OPpathTextBox
            // 
            this.OPpathTextBox.Location = new System.Drawing.Point(44, 21);
            this.OPpathTextBox.Name = "OPpathTextBox";
            this.OPpathTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.OPpathTextBox.Size = new System.Drawing.Size(247, 20);
            this.OPpathTextBox.TabIndex = 2;
            this.OPpathTextBox.WordWrap = false;
            // 
            // OPpathButton
            // 
            this.OPpathButton.BackgroundImage = global::physica.Properties.Resources.folder;
            this.OPpathButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OPpathButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OPpathButton.Location = new System.Drawing.Point(6, 9);
            this.OPpathButton.Name = "OPpathButton";
            this.OPpathButton.Size = new System.Drawing.Size(32, 32);
            this.OPpathButton.TabIndex = 1;
            this.OPpathButton.UseVisualStyleBackColor = true;
            this.OPpathButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // newProjectTab
            // 
            this.newProjectTab.Controls.Add(this.NPcreateProject);
            this.newProjectTab.Controls.Add(this.NPpathTextBox);
            this.newProjectTab.Controls.Add(this.NPopenPath);
            this.newProjectTab.Controls.Add(this.label4);
            this.newProjectTab.Controls.Add(this.NPnameTextBox);
            this.newProjectTab.Controls.Add(this.label3);
            this.newProjectTab.Location = new System.Drawing.Point(4, 22);
            this.newProjectTab.Name = "newProjectTab";
            this.newProjectTab.Padding = new System.Windows.Forms.Padding(3);
            this.newProjectTab.Size = new System.Drawing.Size(297, 157);
            this.newProjectTab.TabIndex = 1;
            this.newProjectTab.Text = "New project";
            this.newProjectTab.UseVisualStyleBackColor = true;
            // 
            // NPcreateProject
            // 
            this.NPcreateProject.BackgroundImage = global::physica.Properties.Resources._new;
            this.NPcreateProject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.NPcreateProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NPcreateProject.Location = new System.Drawing.Point(6, 87);
            this.NPcreateProject.Name = "NPcreateProject";
            this.NPcreateProject.Size = new System.Drawing.Size(64, 64);
            this.NPcreateProject.TabIndex = 5;
            this.NPcreateProject.UseVisualStyleBackColor = true;
            // 
            // NPpathTextBox
            // 
            this.NPpathTextBox.Location = new System.Drawing.Point(44, 61);
            this.NPpathTextBox.Name = "NPpathTextBox";
            this.NPpathTextBox.Size = new System.Drawing.Size(247, 20);
            this.NPpathTextBox.TabIndex = 4;
            // 
            // NPopenPath
            // 
            this.NPopenPath.BackgroundImage = global::physica.Properties.Resources.folder;
            this.NPopenPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NPopenPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NPopenPath.Location = new System.Drawing.Point(6, 49);
            this.NPopenPath.Name = "NPopenPath";
            this.NPopenPath.Size = new System.Drawing.Size(32, 32);
            this.NPopenPath.TabIndex = 3;
            this.NPopenPath.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Path:";
            // 
            // NPnameTextBox
            // 
            this.NPnameTextBox.Location = new System.Drawing.Point(6, 23);
            this.NPnameTextBox.Name = "NPnameTextBox";
            this.NPnameTextBox.Size = new System.Drawing.Size(285, 20);
            this.NPnameTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            // 
            // projWind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 183);
            this.Controls.Add(this.tabControl12);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "projWind";
            this.Text = "Project";
            this.tabControl12.ResumeLayout(false);
            this.openProjectTab.ResumeLayout(false);
            this.openProjectTab.PerformLayout();
            this.newProjectTab.ResumeLayout(false);
            this.newProjectTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tabControl12;
        private System.Windows.Forms.TabPage openProjectTab;
        private System.Windows.Forms.TabPage newProjectTab;
        private System.Windows.Forms.Button OPpathButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox OPpathTextBox;
        private System.Windows.Forms.ListBox OPrecentProjectsList;
        private System.Windows.Forms.Button OPopenRecentProject;
        private System.Windows.Forms.Button OPreloadRecentProjecs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NPnameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NPpathTextBox;
        private System.Windows.Forms.Button NPopenPath;
        private System.Windows.Forms.Button NPcreateProject;
    }
}