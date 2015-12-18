namespace FileWatcherActionKicker
{
    partial class FileWatcher_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileWatcher_Form));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxWatchFolder = new System.Windows.Forms.TextBox();
            this.btnWatchFolder = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.btnScript = new System.Windows.Forms.Button();
            this.tbxScript = new System.Windows.Forms.TextBox();
            this.lstScript = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.dgvStatus = new System.Windows.Forms.DataGridView();
            this.clmThreadID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTimeStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbRunExisting = new System.Windows.Forms.CheckBox();
            this.lblNetworkOffline = new System.Windows.Forms.Label();
            this.numRetryTime = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numProcesses = new System.Windows.Forms.NumericUpDown();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.tbxOutputFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numWaitTime = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.lblThreadCount = new System.Windows.Forms.Label();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProcesses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitTime)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(569, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logLocationToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // logLocationToolStripMenuItem
            // 
            this.logLocationToolStripMenuItem.Name = "logLocationToolStripMenuItem";
            this.logLocationToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.logLocationToolStripMenuItem.Text = "&Log Location";
            this.logLocationToolStripMenuItem.Click += new System.EventHandler(this.logLocationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "A&bout";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folder";
            // 
            // tbxWatchFolder
            // 
            this.tbxWatchFolder.AllowDrop = true;
            this.tbxWatchFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxWatchFolder.Location = new System.Drawing.Point(60, 38);
            this.tbxWatchFolder.Name = "tbxWatchFolder";
            this.tbxWatchFolder.Size = new System.Drawing.Size(461, 21);
            this.tbxWatchFolder.TabIndex = 2;
            this.tbxWatchFolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbxWatchFolder_DragDrop);
            this.tbxWatchFolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbxWatchFolder_DragEnter);
            // 
            // btnWatchFolder
            // 
            this.btnWatchFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWatchFolder.ImageIndex = 0;
            this.btnWatchFolder.ImageList = this.imageList1;
            this.btnWatchFolder.Location = new System.Drawing.Point(527, 36);
            this.btnWatchFolder.Name = "btnWatchFolder";
            this.btnWatchFolder.Size = new System.Drawing.Size(23, 23);
            this.btnWatchFolder.TabIndex = 3;
            this.btnWatchFolder.UseVisualStyleBackColor = true;
            this.btnWatchFolder.Click += new System.EventHandler(this.btnWatchFolder_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "browse.bmp");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Script";
            // 
            // btnScript
            // 
            this.btnScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScript.ImageIndex = 0;
            this.btnScript.ImageList = this.imageList1;
            this.btnScript.Location = new System.Drawing.Point(527, 73);
            this.btnScript.Name = "btnScript";
            this.btnScript.Size = new System.Drawing.Size(23, 23);
            this.btnScript.TabIndex = 6;
            this.btnScript.UseVisualStyleBackColor = true;
            this.btnScript.Click += new System.EventHandler(this.btnScript_Click);
            // 
            // tbxScript
            // 
            this.tbxScript.AllowDrop = true;
            this.tbxScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxScript.Location = new System.Drawing.Point(60, 75);
            this.tbxScript.Name = "tbxScript";
            this.tbxScript.Size = new System.Drawing.Size(461, 21);
            this.tbxScript.TabIndex = 5;
            this.tbxScript.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbxScript_DragDrop);
            this.tbxScript.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbxScript_DragEnter);
            // 
            // lstScript
            // 
            this.lstScript.AllowDrop = true;
            this.lstScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstScript.FormattingEnabled = true;
            this.lstScript.HorizontalScrollbar = true;
            this.lstScript.ItemHeight = 15;
            this.lstScript.Location = new System.Drawing.Point(63, 112);
            this.lstScript.Name = "lstScript";
            this.lstScript.Size = new System.Drawing.Size(458, 94);
            this.lstScript.TabIndex = 7;
            this.lstScript.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstScript_DragDrop);
            this.lstScript.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstScript_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Log";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // cmdStart
            // 
            this.cmdStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStart.ForeColor = System.Drawing.Color.Green;
            this.cmdStart.Location = new System.Drawing.Point(201, 571);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(75, 32);
            this.cmdStart.TabIndex = 10;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdStop.Enabled = false;
            this.cmdStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStop.ForeColor = System.Drawing.Color.Red;
            this.cmdStop.Location = new System.Drawing.Point(295, 571);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(75, 32);
            this.cmdStop.TabIndex = 11;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // dgvStatus
            // 
            this.dgvStatus.AllowUserToAddRows = false;
            this.dgvStatus.AllowUserToDeleteRows = false;
            this.dgvStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmThreadID,
            this.clmName,
            this.clmStatus,
            this.clmTimeStamp});
            this.dgvStatus.Location = new System.Drawing.Point(63, 329);
            this.dgvStatus.Name = "dgvStatus";
            this.dgvStatus.ReadOnly = true;
            this.dgvStatus.Size = new System.Drawing.Size(458, 222);
            this.dgvStatus.TabIndex = 12;
            // 
            // clmThreadID
            // 
            this.clmThreadID.HeaderText = "ThreadID";
            this.clmThreadID.Name = "clmThreadID";
            this.clmThreadID.ReadOnly = true;
            this.clmThreadID.Visible = false;
            // 
            // clmName
            // 
            this.clmName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmName.HeaderText = "File Name";
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
            // 
            // clmStatus
            // 
            this.clmStatus.HeaderText = "Status";
            this.clmStatus.Name = "clmStatus";
            this.clmStatus.ReadOnly = true;
            // 
            // clmTimeStamp
            // 
            this.clmTimeStamp.HeaderText = "Time Stamp";
            this.clmTimeStamp.Name = "clmTimeStamp";
            this.clmTimeStamp.ReadOnly = true;
            // 
            // cbRunExisting
            // 
            this.cbRunExisting.AutoSize = true;
            this.cbRunExisting.Location = new System.Drawing.Point(63, 256);
            this.cbRunExisting.Name = "cbRunExisting";
            this.cbRunExisting.Size = new System.Drawing.Size(145, 19);
            this.cbRunExisting.TabIndex = 13;
            this.cbRunExisting.Text = "Process Existing Files";
            this.cbRunExisting.UseVisualStyleBackColor = true;
            // 
            // lblNetworkOffline
            // 
            this.lblNetworkOffline.AutoSize = true;
            this.lblNetworkOffline.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetworkOffline.ForeColor = System.Drawing.Color.Red;
            this.lblNetworkOffline.Location = new System.Drawing.Point(174, 367);
            this.lblNetworkOffline.Name = "lblNetworkOffline";
            this.lblNetworkOffline.Size = new System.Drawing.Size(244, 24);
            this.lblNetworkOffline.TabIndex = 14;
            this.lblNetworkOffline.Text = "Searching For Directory...";
            this.lblNetworkOffline.Visible = false;
            // 
            // numRetryTime
            // 
            this.numRetryTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRetryTime.Location = new System.Drawing.Point(434, 256);
            this.numRetryTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numRetryTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRetryTime.Name = "numRetryTime";
            this.numRetryTime.Size = new System.Drawing.Size(87, 21);
            this.numRetryTime.TabIndex = 15;
            this.numRetryTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRetryTime.ValueChanged += new System.EventHandler(this.numRetryTime_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Retry (milliseconds)";
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(462, 591);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 15);
            this.lblVersion.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "Maximum Processing Threads";
            // 
            // numProcesses
            // 
            this.numProcesses.Location = new System.Drawing.Point(458, 288);
            this.numProcesses.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numProcesses.Name = "numProcesses";
            this.numProcesses.Size = new System.Drawing.Size(63, 21);
            this.numProcesses.TabIndex = 19;
            this.numProcesses.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numProcesses.ValueChanged += new System.EventHandler(this.numProcesses_ValueChanged);
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputFolder.ImageIndex = 0;
            this.btnOutputFolder.ImageList = this.imageList1;
            this.btnOutputFolder.Location = new System.Drawing.Point(527, 218);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(23, 23);
            this.btnOutputFolder.TabIndex = 22;
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // tbxOutputFolder
            // 
            this.tbxOutputFolder.AllowDrop = true;
            this.tbxOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxOutputFolder.Location = new System.Drawing.Point(152, 220);
            this.tbxOutputFolder.Name = "tbxOutputFolder";
            this.tbxOutputFolder.Size = new System.Drawing.Size(369, 21);
            this.tbxOutputFolder.TabIndex = 21;
            this.tbxOutputFolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbxOutputFolder_DragDrop);
            this.tbxOutputFolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbxOutputFolder_DragEnter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "Output folder (Optional)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::FileWatcherActionKicker.Properties.Resources.Death_Star_icon;
            this.pictureBox1.Location = new System.Drawing.Point(12, 557);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 50);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 294);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 24;
            this.label7.Text = "Start in";
            // 
            // numWaitTime
            // 
            this.numWaitTime.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numWaitTime.Location = new System.Drawing.Point(107, 292);
            this.numWaitTime.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numWaitTime.Name = "numWaitTime";
            this.numWaitTime.Size = new System.Drawing.Size(67, 21);
            this.numWaitTime.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(179, 294);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 15);
            this.label8.TabIndex = 26;
            this.label8.Text = "minutes";
            // 
            // lblThreadCount
            // 
            this.lblThreadCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblThreadCount.AutoSize = true;
            this.lblThreadCount.Location = new System.Drawing.Point(359, 553);
            this.lblThreadCount.Name = "lblThreadCount";
            this.lblThreadCount.Size = new System.Drawing.Size(14, 15);
            this.lblThreadCount.TabIndex = 27;
            this.lblThreadCount.Text = "0";
            this.lblThreadCount.Visible = false;
            // 
            // lblProcessing
            // 
            this.lblProcessing.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.Location = new System.Drawing.Point(195, 553);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(158, 15);
            this.lblProcessing.TabIndex = 28;
            this.lblProcessing.Text = "Threads Actively Processing";
            this.lblProcessing.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblProcessing.Visible = false;
            // 
            // FileWatcher_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 615);
            this.Controls.Add(this.lblProcessing);
            this.Controls.Add(this.lblThreadCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numWaitTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOutputFolder);
            this.Controls.Add(this.tbxOutputFolder);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numProcesses);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numRetryTime);
            this.Controls.Add(this.lblNetworkOffline);
            this.Controls.Add(this.cbRunExisting);
            this.Controls.Add(this.dgvStatus);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstScript);
            this.Controls.Add(this.btnScript);
            this.Controls.Add(this.tbxScript);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnWatchFolder);
            this.Controls.Add(this.tbxWatchFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(585, 538);
            this.Name = "FileWatcher_Form";
            this.Text = "File Watcher - Action Kicker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileWatcher_Form_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileWatcher_Form_FormClosed);
            this.Load += new System.EventHandler(this.FileWatcher_Form_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProcesses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWatchFolder;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnScript;
        private System.Windows.Forms.TextBox tbxScript;
        private System.Windows.Forms.ListBox lstScript;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.CheckBox cbRunExisting;
        public System.Windows.Forms.TextBox tbxWatchFolder;
        public System.Windows.Forms.DataGridView dgvStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmThreadID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTimeStamp;
        public System.Windows.Forms.Label lblNetworkOffline;
        private System.Windows.Forms.NumericUpDown numRetryTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numProcesses;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.TextBox tbxOutputFolder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numWaitTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblThreadCount;
        private System.Windows.Forms.Label lblProcessing;
    }
}

