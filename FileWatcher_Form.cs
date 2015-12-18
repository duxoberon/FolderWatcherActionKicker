//****************************************************************************
//Class:     FileWatcher_Form.cs
//Company:   Woolpert, Inc.
//Copyright: Copyright © Woolpert 2011
//Author:    Jonathan Meyer
//Purpose:   GUI for configuring the monitoring of a directory and specifying
//           the script file to run for files added to that directory.
//Comments:  

//History:   
// 
//*****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace FileWatcherActionKicker
{
    public partial class FileWatcher_Form : Form
    {

        #region Fields

        private List<string> mScriptEntries = new List<string>();
        private string mFileWatchExtension = String.Empty;
        private Guid mAppInstanceID;
        private string mLogFileName = String.Empty;
        private string mMachineName = String.Empty;
        private Util _util;

        private ThreadManager mThreadManager;
        private Thread mProcessThread;
        private string _log = string.Empty;

        public List<string> ScriptEntries
        {
            get
            {
                return mScriptEntries;
            }
        }

        public string LogFileName
        {
            get
            {
                return mLogFileName;
            }
        }


        #endregion

        #region Constructor

        public FileWatcher_Form()
        {
            InitializeComponent();

            mMachineName = System.Environment.MachineName;
            mAppInstanceID = Guid.NewGuid();

            DateTime startTime = DateTime.Now;

            mLogFileName = "_" + mMachineName + "-" + startTime.Month.ToString() + "-" + startTime.Date.Day.ToString() + "-" + startTime.Year.ToString() + "-" + startTime.Hour.ToString() + "." + startTime.Minute.ToString() + "." + startTime.Second.ToString() + ".log";

            _util = new Util();

        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the form closed event. Will attempt to delete the lock folder from the watch directory if it exists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileWatcher_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            // On close delete lock file for this instance if there is one

            if (File.Exists(Path.Combine(tbxWatchFolder.Text.Trim(), mAppInstanceID + "." + mMachineName + ".lock")))
            {
                try
                {
                    File.Delete(Path.Combine(tbxWatchFolder.Text.Trim(), mAppInstanceID + "." + mMachineName + ".lock"));
                }
                catch
                {
                    Logger.WriteLogLine(Path.Combine(tbxWatchFolder.Text.Trim(), LogFileName), "Unable to delete " + Path.Combine(tbxWatchFolder.Text.Trim(), mAppInstanceID + "." + mMachineName + ".lock") + "  lock file. Please delete this file by hand.");
                }
            }
        }

        /// <summary>
        /// Handles the form closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileWatcher_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mThreadManager != null) mThreadManager.Stop();
            _util.SaveSettings();
        }

        /// <summary>
        /// Handles the button click event of the directory browse button for the watch folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWatchFolder_Click(object sender, EventArgs e)
        {
            if (this.tbxWatchFolder.Text.Length > 0)
            {
                if (Directory.Exists(this.tbxWatchFolder.Text))
                    this.folderBrowserDialog1.SelectedPath = this.tbxWatchFolder.Text;                
            }
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.tbxWatchFolder.Text = this.folderBrowserDialog1.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not find folder from disk. Original error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Handles the button click event of the directory browse button for the output folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            if (this.tbxOutputFolder.Text.Length > 0)
            {
                if (Directory.Exists(this.tbxOutputFolder.Text))
                    this.folderBrowserDialog1.SelectedPath = this.tbxOutputFolder.Text;
            }
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.tbxOutputFolder.Text = this.folderBrowserDialog1.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not find folder from disk. Original error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Handles the click even of the directory browse button for the script file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScript_Click(object sender, EventArgs e)
        {
            if (_util.SCRIPTFOLDER.Length > 0)
                this.openFileDialog1.InitialDirectory = _util.SCRIPTFOLDER;
            else
            {
                if (this.tbxWatchFolder.Text.Length > 0)
                {
                    if (Directory.Exists(this.tbxWatchFolder.Text))
                        this.openFileDialog1.InitialDirectory = this.tbxWatchFolder.Text;
                    else this.openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                }
                else this.openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }

            this.openFileDialog1.Filter = "Batch files (*.bat)|*.range|All files (*.*)|*.*";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.RestoreDirectory = true;

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.tbxScript.Text = this.openFileDialog1.FileName;

                    validateAndParseFile(tbxScript.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Handles the drag enter event for the watch folder text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxWatchFolder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        /// <summary>
        /// Handles the drag drop event for the watch folder text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxWatchFolder_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string s in files)
                {
                    if (System.IO.Directory.Exists(s))
                    {
                        this.tbxWatchFolder.Text = s;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the drag enter event for the script location text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxScript_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        /// <summary>
        /// Handles the drag drop event for the script location text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxScript_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string s in files)
                {
                    if (string.Compare(System.IO.Path.GetExtension(s), ".bat") == 0)
                    {
                        this.tbxScript.Text = s;
                        break;
                    }
                }

                validateAndParseFile(tbxScript.Text);
            }
        }

        /// <summary>
        /// Handles the drag enter event for the script location list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstScript_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        /// <summary>
        /// Handles the drag drop event for the script location list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstScript_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string s in files)
                {
                    if (string.Compare(System.IO.Path.GetExtension(s), ".bat") == 0)
                    {
                        this.tbxScript.Text = s;
                        break;
                    }
                }

                validateAndParseFile(tbxScript.Text);
            }
        }

        private void tbxOutputFolder_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string s in files)
                {
                    if (System.IO.Directory.Exists(s))
                    {
                        this.tbxOutputFolder.Text = s;
                        break;
                    }
                }
            }
        }

        private void tbxOutputFolder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void logLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this._util.LOGFOLDER.Length > 0)
            {
                if (Directory.Exists(this._util.LOGFOLDER))
                    this.folderBrowserDialog1.SelectedPath = _util.LOGFOLDER;
            }
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _util.LOGFOLDER = this.folderBrowserDialog1.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not find folder from disk. Original error: " + ex.Message);
                }
            }
        }

        private void numRetryTime_ValueChanged(object sender, EventArgs e)
        {
            _util.NETWORKTRY = this.numRetryTime.Value;
            if (mThreadManager != null)
            {
                int retryTime = (int)numRetryTime.Value * 1000;  // convert to milliseconds
                mThreadManager.NetworkRetry = retryTime;
            }
        }

        private void numProcesses_ValueChanged(object sender, EventArgs e)
        {
            _util.PROCESSINGTHREADS = this.numProcesses.Value;
            if (mThreadManager != null)
            {
                int numberOfProcesses = (int)numProcesses.Value;
                //mThreadManager.NumberOfProcess = numberOfProcesses;
                mThreadManager.SetNumberOfProcess(numberOfProcesses);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the click event for the start button. This will start the monitoring of the watch directory and processing of
        /// any input files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStart_Click(object sender, EventArgs e)
        {
            bool runExisting = false;

            try
            {
                if (cbRunExisting.Checked == true)
                {
                    DialogResult dr = MessageBox.Show("You have selected to process existing files in the watch folder. This may overwrite any existing output " +
                        "files of the type your selected script is creating.\nAre you sure you want to do this?", "Confirm Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        runExisting = true;
                    }
                }

                DisableControls();
                this.cmdStop.Enabled = true;

                if (validateScriptFile(tbxScript.Text) == false || !Directory.Exists(tbxWatchFolder.Text.Trim()))
                {
                    MessageBox.Show("Please select a valid script file and directory to monitor", "Validation Error", MessageBoxButtons.OK);
                    EnableControls();
                    this.cmdStop.Enabled = false;
                    return;
                }

                string[] lockFiles = Directory.GetFiles(tbxWatchFolder.Text.Trim(), "*.lock");
                bool isSameMachine = true;

                foreach (string fileName in lockFiles)
                {
                    if (fileName.Contains(mMachineName))
                    {
                        isSameMachine = true;
                    }
                    else
                    {
                        // if any lock file has a different machine name than
                        // the machine this instance is running on we dont allow
                        // running on that folder
                        isSameMachine = false;
                        return;
                    }
                }

                if (isSameMachine)
                {
                    File.WriteAllText(Path.Combine(tbxWatchFolder.Text.Trim(), mAppInstanceID + "." + mMachineName + ".lock"), "locked");
                }
                else
                {
                    MessageBox.Show("Another machine is currently monitoring this location. Please choose a different directory to watch.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EnableControls();
                    return;
                }


                _util.WATCHFOLDER = this.tbxWatchFolder.Text.Trim();
                _util.OUTPUTFOLDER = this.tbxOutputFolder.Text.Trim();
                _util.PROCESSINGTHREADS = this.numProcesses.Value;
                _util.NETWORKTRY = this.numRetryTime.Value;
                _util.PROCESSEXISTING = runExisting;
                _util.SCRIPTFOLDER = Path.GetDirectoryName(this.tbxScript.Text);
                Color clr = Color.FromArgb(_util.WATCHFOLDER.GetHashCode());
                this.menuStrip1.BackColor = clr;
                this.Refresh();


                _log = Path.Combine(_util.LOGFOLDER, DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") +
                    DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") +
                    DateTime.Now.Second.ToString("00") + " _" + Directory.GetParent(_util.WATCHFOLDER).Name + ".txt");
                Logger.WriteLogLine_2(_log, _util.WATCHFOLDER);


                decimal waittime = this.numWaitTime.Value;
                decimal waitinmilliseonds =0;
                if (waittime > 0)
                {
                    decimal waitinseconds = waittime * 60;
                    waitinmilliseonds = waitinseconds * 1000;
                    decimal waitinnanoseconds = waitinmilliseonds * 1000000;
                    Int64 ticks = decimal.ToInt64(waitinnanoseconds / 100);
                    TimeSpan ts = new TimeSpan(ticks);
                    DateTime dt = DateTime.Now;
                    dt = dt.Add(ts);
                    string ques = "Should File Watcher wait " + ts.TotalHours + " hours?\nIf yes, the Storm Troopers will march on " + dt.ToLongDateString() + " at " + dt.ToShortTimeString();
                    DialogResult dr = MessageBox.Show(ques, "T Minus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        dt = DateTime.Now;
                        dt = dt.Add(ts);
                        this.Text = "Will start at " + dt.ToShortTimeString() + " on " + dt.ToShortDateString();
                        this.Refresh();

                    }
                    else
                    {
                        EnableControls();
                        this.cmdStop.Enabled = false;
                        return;
                    }
                }

                // Determine first input file extension to setup file watcher 
                foreach (string entry in mScriptEntries)
                {

                    if (entry.Contains("#INPUT."))
                    {
                        string tempStr = String.Empty;
                        int indexInput = entry.IndexOf("#INPUT.");

                        tempStr = entry.Substring(indexInput, entry.Length - indexInput);

                        tempStr = tempStr.Replace("#INPUT.", "");

                        int indexSpace = tempStr.IndexOf(" ");

                        if (indexSpace != -1)
                        {
                            tempStr = tempStr.Substring(0, indexSpace);
                        }

                        mFileWatchExtension = tempStr;
                        break;
                    }
                }

                int retryTime = (int)numRetryTime.Value;
                int numberOfProcesses = (int)numProcesses.Value;

                mThreadManager = new ThreadManager(this, _util.WATCHFOLDER, "*" + mFileWatchExtension, _util.PROCESSEXISTING, retryTime, numberOfProcesses, 
                    _util.OUTPUTFOLDER, decimal.ToInt32(waitinmilliseonds));
                mProcessThread = new Thread(mThreadManager.Start);
                mProcessThread.Start();

            }
            catch (Exception ex)
            {
                EnableControls();
                this.cmdStop.Enabled = false;
                Logger.WriteLogLine(Path.Combine(tbxWatchFolder.Text.Trim(), LogFileName), "An unhandled exception was thrown while running this tool. ERROR: " + ex.Message);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Handles the click event of the stop button. This will stop monitoring the watch directory and stop processing of any items in the process queue.
        /// Items in the queue that are currently in the running state will finish before the application completely stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStop_Click(object sender, EventArgs e)
        {

            mProcessThread.Interrupt();  //End all life on planet, wait, just interrupt the sleepy guys
            mThreadManager.Stop();  // Will block until all items in queue finish

            this.cmdStart.Enabled = true;

            // Set the status value for any files in the queue to canceled
            for (int i = 0; i < dgvStatus.Rows.Count; i++ )
            {
                if (dgvStatus.Rows[i].Cells[2].Value.ToString() == "Queued")
                {
                    dgvStatus.Rows[i].Cells[2].Value = "Canceled";
                }
            }

            if (File.Exists(Path.Combine(tbxWatchFolder.Text.Trim(), mAppInstanceID + "." + mMachineName + ".lock")))
            {
                try
                {
                    File.Delete(Path.Combine(tbxWatchFolder.Text.Trim(), mAppInstanceID + "." + mMachineName + ".lock"));
                }
                catch(Exception)
                {
                    Logger.WriteLogLine(Path.Combine(tbxWatchFolder.Text.Trim(), LogFileName), "Unable to delete " + Path.Combine(tbxWatchFolder.Text.Trim(), mAppInstanceID + "." + mMachineName + ".lock") + "  lock file. Please delete this file by hand.");
                }
            }

            
            this.EnableControls();
            this.lblNetworkOffline.Visible = false;
            this.cmdStop.Enabled = false;
            Logger.WriteLogLine(_log, "Ended by user");
            this.Text = "File Watcher - Action Kicker";

        }

        /// <summary>
        /// Handles the form load event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileWatcher_Form_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Version: " + this.ProductVersion;
            this.tbxWatchFolder.Text = _util.WATCHFOLDER;
            this.tbxOutputFolder.Text = _util.OUTPUTFOLDER;
            this.numProcesses.Value = _util.PROCESSINGTHREADS;
            this.numRetryTime.Value = _util.NETWORKTRY;
            this.cbRunExisting.Checked = _util.PROCESSEXISTING;
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Disables the UI controls
        /// </summary>
        public void DisableControls()
        {
            tbxWatchFolder.Enabled = false;
            tbxScript.Enabled = false;
            lstScript.Enabled = false;
            cmdStart.Enabled = false;
            btnScript.Enabled = false;
            btnWatchFolder.Enabled = false;
            cbRunExisting.Enabled = false;
            //numRetryTime.Enabled = false;
            //numProcesses.Enabled = false;
            tbxOutputFolder.Enabled = false;
            btnOutputFolder.Enabled = false;
            numWaitTime.Enabled = false;

            lblThreadCount.Visible = true;
            lblProcessing.Visible = true;
        }

        /// <summary>
        /// Enables the UI controls
        /// </summary>
        public void EnableControls()
        {
            tbxWatchFolder.Enabled = true;
            tbxScript.Enabled = true;
            lstScript.Enabled = true;
            cmdStart.Enabled = true;
            btnScript.Enabled = true;
            btnWatchFolder.Enabled = true;
            cbRunExisting.Enabled = true;
            //numRetryTime.Enabled = true;
            //numProcesses.Enabled = true;
            tbxOutputFolder.Enabled = true;
            btnOutputFolder.Enabled = true;
            numWaitTime.Enabled = true;

            lblThreadCount.Visible = false;
            lblProcessing.Visible = false;
        }


        /// <summary>
        /// Validates and parses a script file
        /// </summary>
        /// <param name="filePath"></param>
        private void validateAndParseFile(string filePath)
        {
            mScriptEntries.Clear();
            lstScript.Items.Clear();

            if (this.validateScriptFile(this.tbxScript.Text))
            {
                mScriptEntries = parseScriptFile(this.tbxScript.Text);

                foreach (String entry in mScriptEntries)
                {
                    lstScript.Items.Add(entry);
                }
            }
            else
            {
                MessageBox.Show("Invalid Script file selected. Please choose a different script.", "Validate Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates a script file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool validateScriptFile(string filePath)
        {
            bool validFile = false;

            if (File.Exists(filePath))
            {
                string[] scriptLines = File.ReadAllLines(filePath);
                
                for(int i = 0; i < scriptLines.Count(); i++)
                {
                    string line = scriptLines[i].Trim();

                    if (line != String.Empty && line.Length >1)
                    {
                        string firstChar = line.Substring(0, 1);

                        // if first character in a line is a character A-Z
                        // it's a script line. if its not then its assumed a comment line
                        if (Regex.IsMatch(firstChar, @"^[a-zA-Z]+$"))
                        {
                            if (line.ToUpper().Contains("#INPUT."))
                            {
                                validFile = true;
                            }
                            else
                            {
                                return validFile = false;
                            }

                        }
                    }
                }

            }
            else
            {
                validFile = false;
            }

            return validFile;
        }

        /// <summary>
        /// Parses a script file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private List<string> parseScriptFile(string filePath)
        {
            List<string> processSteps = new List<string>();

            if (File.Exists(filePath))
            {
                string[] scriptLines = File.ReadAllLines(filePath);

                for (int i = 0; i < scriptLines.Count(); i++)
                {
                    string line = scriptLines[i].Trim();

                    if (line != String.Empty && line.Length > 1)
                    {
                        string firstChar = line.Substring(0, 1);

                        // if first character in a line is a character A-Z
                        // it's a script line. if its not then its assumed a comment line
                        if (Regex.IsMatch(firstChar, @"^[a-zA-Z]+$"))
                        {
                            processSteps.Add(line);
                        }
                    }
                }

            }
   
            return processSteps;
        }

        private delegate void AddNewFileStatusDelegate(string threadID, string ID);
        /// <summary>
        /// Adds a new file entry to the data grid. 
        /// </summary>
        /// <param name="threadID"></param>
        /// <param name="fileName"></param>
        public void AddNewFileStatus(string threadID, string fileName)
        {
            if (this.dgvStatus.InvokeRequired)
            {
                this.dgvStatus.Invoke(new AddNewFileStatusDelegate(this.AddNewFileStatus), threadID, fileName);
            }
            else
            {
                DateTime timeStamp = DateTime.Now;

                dgvStatus.Rows.Add(threadID, fileName, "Queued", timeStamp.ToShortTimeString());
            }
            
        }

        private delegate void UpdateProcessingStatusDelegate(string threadID, string statusMessage);
        /// <summary>
        /// Updates a file entry in the data grid.
        /// </summary>
        /// <param name="threadID"></param>
        /// <param name="statusMessage"></param>
        public void UpdateProcessingStatus(string threadID, string statusMessage)
        {

            if (this.dgvStatus.InvokeRequired)
            {
                this.dgvStatus.Invoke(new UpdateProcessingStatusDelegate(this.UpdateProcessingStatus), threadID, statusMessage);
            }
            else
            {
                DateTime timeStamp = DateTime.Now;

                for (int i = 0; i < dgvStatus.Rows.Count; i++)
                {
                    if (dgvStatus.Rows[i].Cells[0].Value.ToString() == threadID)
                    {
                        dgvStatus.Rows[i].Cells[2].Value = statusMessage;
                        dgvStatus.Rows[i].Cells[3].Value = timeStamp.ToShortTimeString();
                    }
                }
            }
        }

        private delegate void UpdateRunningThreadCountDelegate(int runningThreadCount);

        public void UpdateRunningThreadCount(int runningThreadCount)
        {
            if (this.lblThreadCount.InvokeRequired)
            {
                this.lblThreadCount.Invoke(new UpdateRunningThreadCountDelegate(this.UpdateRunningThreadCount), runningThreadCount);
            }
            else
            {
                this.lblThreadCount.Text = runningThreadCount.ToString();
            }
        }

        private delegate void ClearDataGridDelegate();
        public void ClearDataGrid()
        {
            if (this.dgvStatus.InvokeRequired)
            {
                this.dgvStatus.Invoke(new ClearDataGridDelegate(this.ClearDataGrid));
            }
            else
            {
                this.dgvStatus.Rows.Clear();
            }
        }

        private delegate void EnableErrorLabelDelegate(bool enable);
        /// <summary>
        /// Turns on or off the error label. Used when connection to the watch folder is lost.
        /// </summary>
        /// <param name="enable"></param>
        public void EnableErrorLabel(bool enable)
        {
            if (this.lblNetworkOffline.InvokeRequired)
            {
                this.lblNetworkOffline.Invoke(new EnableErrorLabelDelegate(this.EnableErrorLabel), enable);
            }
            else
            {
                if (enable)
                {
                    this.lblNetworkOffline.Visible = true;
                }
                else
                {
                    this.lblNetworkOffline.Visible = false;
                }
            }
        }



        #endregion


    }
}
