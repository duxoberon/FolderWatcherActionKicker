//****************************************************************************
//Class:     ThreadManager.cs
//Company:   Woolpert, Inc.
//Copyright: Copyright © Woolpert 2011
//Author:    Jonathan Meyer
//Purpose:   Class responsible for handling creation of file watch thread and
//           file processing threads.
//Comments:  

//History:   
// 
//*****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace FileWatcherActionKicker
{
    public class ThreadManager
    {
        private Object ctrLock = new Object();
        private Queue<KeyValuePair<string, string>> mFilesToProcess = new Queue<KeyValuePair<string, string>>();
        private bool mIsRunning = false;
        private bool mProcessExisting = false;
        private int mThreadCount = 0;
        private int mMaxThreadCount = 3;
        private List<string> mInputFileNames = new List<string>();
        private FileWatcher_Form mParentUI = null;
        private int mNetworkRetryMSeconds = 0;
        private string mWatchDirectory = String.Empty;
        private string mFileWatchType = String.Empty;
        private string mOutputDirectory = String.Empty;
        private int _wait;

        private FileMonitor mFileMonitor;

        /// <summary>
        /// Responsible for managing all of all the threads requried by the application. This will create the worker threads and also the directory monitor thread.
        /// </summary>
        /// <param name="parentUI">Reference to the file watcher UI creating the thread manager.</param>
        /// <param name="watchDirectory">Path to the directory where files will be monitored.</param>
        /// <param name="fileWatchType">File extension to be monitoring for.</param>
        /// <param name="processExisting">True to process files that already exist in the monitor directory.</param>
        /// <param name="networkRetryMSeconds">Number of milliseconds to wait for retry if the monitor directory is missing.</param>
        /// <param name="numberProcesses">Number of concurrent processes to be running at at time.</param>
        public ThreadManager(FileWatcher_Form parentUI, string watchDirectory, string fileWatchType, bool processExisting, int networkRetryMSeconds, 
            int numberProcesses, string outputDirectory, int wait)
        {
            mParentUI = parentUI;
            mWatchDirectory = watchDirectory;
            mFileWatchType = fileWatchType;
            mProcessExisting = processExisting;
            mNetworkRetryMSeconds = networkRetryMSeconds;
            mMaxThreadCount = numberProcesses;
            mOutputDirectory = outputDirectory;
            _wait = wait;
        }

        /// <summary>
        /// Handles the starting of the threads.
        /// </summary>
        public void Start()
        {
            try
            {
                Thread.Sleep(_wait);
                Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "Monitoring of directory " + mWatchDirectory + " started.");

                mIsRunning = true;
                mInputFileNames.Clear();
                mFilesToProcess.Clear();


                mParentUI.ClearDataGrid();

                mFileMonitor = new FileMonitor(this, mWatchDirectory, "*" + mFileWatchType, mProcessExisting, mNetworkRetryMSeconds);
                Thread mgrThread = new Thread(mFileMonitor.Start);
                mgrThread.Start();


                // Main processing code
                // will process any items in the file list queue or wait if none exist or 3 threads are already started
                while (mIsRunning)
                {
                    // Must test if the directory is missing before we can pull a queued file from the queue.
                    if (!Directory.Exists(mWatchDirectory))
                    {
                        try
                        {
                            Thread.Sleep(mNetworkRetryMSeconds * 10);  //sleep until network comes back up 
                        }
                        catch (Exception ex) { string message = ex.Message; }
                    }
                    else
                    {

                        if (mFilesToProcess.Count == 0)
                        {
                            try { Thread.Sleep(mNetworkRetryMSeconds); }
                            catch (Exception ex) { string message = ex.Message; }
                        }
                        else if (mThreadCount >= mMaxThreadCount)
                        {
                            try { Thread.Sleep(mNetworkRetryMSeconds); }
                            catch (Exception ex) { string message = ex.Message; }
                        }
                        else
                        {
                            KeyValuePair<string, string> fileToProcess = mFilesToProcess.Dequeue();

                            mParentUI.UpdateProcessingStatus(fileToProcess.Key, "Running...");

                            if (this.WriteBatchFile(mWatchDirectory, fileToProcess.Key, fileToProcess.Value) == true)
                            {
                                // Start worker thread to call script file for a given file
                                ProcessThread processThread = new ProcessThread(this, fileToProcess.Key, fileToProcess.Value, fileToProcess.Value + ".bat", mWatchDirectory);
                                Thread thread = new Thread(processThread.Start);
                                //Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "Processing of " + fileToProcess.Value + " started.");
                                this.IncreaseThreadCount();
                                thread.Start();
                            }
                            else
                                mParentUI.UpdateProcessingStatus(fileToProcess.Key, "Skipped...");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        /// <summary>
        /// Handles the stopping of the threads.
        /// </summary>
        public void Stop()
        {

            if (!mIsRunning) return;

            Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "Monitoring of directory " + mWatchDirectory + " stopping.");

            // Setting these to Stop watching for new files and stop processing of files in the queue
            mIsRunning = false;
            if(mFileMonitor != null)
            {
                mFileMonitor.Stop();
            }

            while (mThreadCount != 0)
            {
                //Threads still running
                Application.DoEvents();
            }

            Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "Monitoring of directory " + mWatchDirectory + " stopped.");
        }


        /// <summary>
        /// Adds a given file to the processing queue. Each file added is given a GUID that is used to
        /// identify the file while its being processed.
        /// </summary>
        /// <param name="fileName">File name, including extension, of the file to process.</param>
        /// <param name="fullPath">Full path to the file to process.</param>
        public void AddToProcessQueue(string fileName, string fullPath)
        {
            if (!mInputFileNames.Contains(fileName))
            {
                // Wait for file to complete its write
                FileInfo inputFile = new FileInfo(fullPath);
                int retryLimit = 10;
                int retryCounter = 0;
                bool fileSkipped = false;


                while (IsFileAvailable(inputFile.FullName) == false && retryCounter >= retryLimit)
                {
                    Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "The file " + fileName + " is locked by another process and cannot be processed. Thread will sleep and retry. ");
                    try
                    {
                        Thread.Sleep(mNetworkRetryMSeconds * 10);   //TODO maybe add a retry limit and log if limit is hit
                    }
                    catch (Exception ex) { string message = ex.Message; }


                    if (retryCounter == retryLimit)
                    {
                        Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "The file " + fileName + " was skipped as file was locked and retry limit reached.");
                        fileSkipped = true;
                    }

                    retryCounter++;
                }

                if (fileSkipped == false)
                {

                    //Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "File " + fileName + " added to processing queue.");

                    mInputFileNames.Add(fileName);

                    Guid threadID = Guid.NewGuid();

                    KeyValuePair<string, string> addedFile = new KeyValuePair<string, string>(threadID.ToString(), Path.GetFileNameWithoutExtension(fullPath));

                    mParentUI.AddNewFileStatus(addedFile.Key, addedFile.Value);

                    mFilesToProcess.Enqueue(addedFile);
                }

            }
        }


        /// <summary>
        /// Called when the file watcher can't find the directory it's processing against
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void NotifyNetworkIssue(bool networkDown)
        {
            if(networkDown)
            {
                mParentUI.EnableErrorLabel(true);
            }
            else
            {
                mParentUI.EnableErrorLabel(false);
            }
                    
        }

        /// <summary>
        /// Checks to see if a file is locked
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool IsFileAvailable(string s)
        {
            if (!File.Exists(s)) return false;
            try
            {
                using (FileStream inputStream = File.Open(s, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Increases the active thread count
        /// </summary>
        public void IncreaseThreadCount()
        {
            lock (ctrLock)
            {
                mThreadCount++;
            }

            mParentUI.UpdateRunningThreadCount(mThreadCount);
        }

        /// <summary>
        /// Decreases the active thread count
        /// </summary>
        public void DecreaseThreadCount()
        {
            lock (ctrLock)
            {
                mThreadCount--;
            }
            mParentUI.UpdateRunningThreadCount(mThreadCount);
        }

        /// <summary>
        /// Callback method from worker threads to notify processing has completed for a file.
        /// </summary>
        /// <param name="threadID">Thread ID of completed thread.</param>
        /// <param name="fileName">Name of file that was processed.</param>
        public void ThreadFinishCallback(string threadID, string fileName)
        {
            mParentUI.UpdateProcessingStatus(threadID, "Finished");
            Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "Processing of " + fileName + " finished.");
            
            try
            {
                File.Delete(Path.Combine(mWatchDirectory, fileName + ".bat"));
            }
            catch(Exception ex)
            {
                Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "Failed to delete " + fileName + ".bat . Error:" +ex.Message);
            }
            this.DecreaseThreadCount();
        }

        /// <summary>
        /// Callback method from the worker threads to notify processing failed for a file.
        /// </summary>
        /// <param name="threadID">Thread ID of completed thread.</param>
        /// <param name="fileName">Name of file that was processed.</param>
        /// <param name="errorMessage">Message explaining processing failure.</param>
        public void ThreadErrorCallback(string threadID, string fileName, string errorMessage)
        {
            mParentUI.UpdateProcessingStatus(threadID, "Failed");
            Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "Processing of " + fileName + " failed. Error: " + errorMessage);
            this.DecreaseThreadCount();
        }


        /// <summary>
        /// Creates the script file to be run with the correct input and output parameters replaced
        /// with the file name of the file to be processed.
        /// </summary>
        /// <param name="fileDirectory"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool WriteBatchFile(string fileDirectory, string threadID, string fileName)
        {

            int indexof16bits = fileName.IndexOf("16bits_", StringComparison.CurrentCultureIgnoreCase);
            if (indexof16bits == 0) return false;

            bool success = false;

            //Create batch file
            StringBuilder batchFileText = new StringBuilder();

            for (int i = 0; i < mParentUI.ScriptEntries.Count; i++)
            {
                string entry = mParentUI.ScriptEntries[i];

                entry = entry.Replace("#INPUT", fileName);
                int indexofmove = entry.IndexOf("move", StringComparison.CurrentCultureIgnoreCase);
                int indexofcopy = entry.IndexOf("copy", StringComparison.CurrentCultureIgnoreCase);
                if (indexofmove == 0 || indexofcopy == 0)
                    entry = entry.Replace("#OUTPUT", Path.Combine(mOutputDirectory, fileName));
                else
                    entry = entry.Replace("#OUTPUT", fileName);
                batchFileText.AppendLine(entry);
            }

            batchFileText.AppendLine("exit");   // attempting to see if this will force the batch file to close after completion

            string batchFilePath = Path.Combine(fileDirectory, fileName + ".bat");

            if (File.Exists(batchFilePath))
            {
                try
                {
                    File.Delete(batchFilePath);
                }
                catch (Exception)
                {
                    Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "Failed to create processing batch file for " + fileName + ". Unable to delete existing batch file. File will not be processed.");
                    mParentUI.UpdateProcessingStatus(threadID, "Failed");
                    return success = false;
                }
            }

            try
            {
                File.WriteAllText(batchFilePath, batchFileText.ToString());
            }
            catch(Exception ex)
            {
                Logger.WriteLogLine(Path.Combine(mWatchDirectory, mParentUI.LogFileName), "Failed to create processing batch file for " + fileName + ". File will not be processed. Error: " + ex.Message);
                mParentUI.UpdateProcessingStatus(threadID, "Failed");
                return success = false;
            }

            success = true;

            return success;
        }

        public bool ThreadMgrIsRunning
        {
            get
            {
                return mIsRunning;
            }
            set
            {
                mIsRunning = value;
            }
        }

        //public int NumberOfProcess
        //{
        //    set { mMaxThreadCount = value; }
        //}

        public void SetNumberOfProcess(int numofproc)
        {
            lock (ctrLock)
            {
                mMaxThreadCount = numofproc;
            }
        }

        public int NetworkRetry
        {
            set
            {
                mNetworkRetryMSeconds = value;
                if (mFileMonitor != null)
                    mFileMonitor.NetworkRetry = mNetworkRetryMSeconds;
            }
        }

    }
}
