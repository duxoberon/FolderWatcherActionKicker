//****************************************************************************
//Class:     FileMonitor.cs
//Company:   Woolpert, Inc.
//Copyright: Copyright © Woolpert 2011
//Author:    Jonathan Meyer
//Purpose:   Class for assisting in monitoring a directory for a certain file type.
//Comments:  

//History:   
// 
//*****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileWatcherActionKicker
{
    public class FileMonitor
    {
        private ThreadManager mThreadMgr;
        private string mMonitorPath = String.Empty;
        private string mFileFilterExtension = String.Empty;
        private bool isRunning = false;
        private int mRetryTime = 10000;
        private List<string> mAllFileListing = new List<string>();
        private bool mProcessExisting = false;

        private Dictionary<string, int> mIsReallyUnlocked;

        /// <summary>
        /// This class will be used in replacement of the .net FileSystemWatcher. It will be launched from the thread manager as a single thread and used
        /// to monitor the watch directory.  It will continuously check the watch directory for new files of the file type specified. Any files found
        /// to be new will processed by being added to the queue the thread manager is managing
        /// </summary>
        /// <param name="threadManager">Reference to the thread manager</param>
        /// <param name="monitorPath">Full path to the directory to monitor</param>
        /// <param name="monitorFileExtension">File extension to be monitoring for.</param>
        /// <param name="processExisting">True to process existing file of the file extension specified to be found in the monitor directory.</param>
        /// <param name="retryTime">Amount of time in milliseconds to wait for retying connection to the monitor directory if it should become unavailable.</param>
        public FileMonitor(ThreadManager threadManager, string monitorPath, string monitorFileExtension, bool processExisting, int retryTime)
        {
            mThreadMgr = threadManager;
            mMonitorPath = monitorPath;
            mFileFilterExtension = monitorFileExtension;
            mProcessExisting = processExisting;
            mRetryTime = retryTime;
            mIsReallyUnlocked = new Dictionary<string, int>();
        }

        /// <summary>
        /// Handles the start command when the thread is started.
        /// This will start monitoring the watch directory
        /// </summary>
        public void Start()
        {
            mAllFileListing.Clear();
            isRunning = true;

            // Must get a list of all existing files in the watch folder before starting the running loop.
            // If use selected process existing we will add these files to the process list. Otherwise
            // these files will be ignored and not processed.
            if (Directory.Exists(mMonitorPath))
            {
                string[] existingFiles = Directory.GetFiles(mMonitorPath, mFileFilterExtension);


                foreach (string fullFileName in existingFiles)
                {
                    if (IsFileAvailable(fullFileName))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(Path.Combine(mMonitorPath, fullFileName));

                        if (mProcessExisting == true)
                        {
                            mThreadMgr.AddToProcessQueue(fileName, Path.Combine(mMonitorPath, fullFileName));
                            mAllFileListing.Add(fullFileName);
                        }
                        else
                        {
                            mAllFileListing.Add(fullFileName);
                        }
                    }
                }
            }


            while (isRunning)
            {

                if (Directory.Exists(mMonitorPath))
                {
                    mThreadMgr.NotifyNetworkIssue(false);

                    string[] updatedFileList;

                    try
                    {
                        updatedFileList = Directory.GetFiles(mMonitorPath, mFileFilterExtension);
                    }
                    catch
                    {
                        updatedFileList = new string[0];
                    }

                    // Any files found on the file system of the file type we are monitoring that do
                    // not already exist in the list of files will be added to the process queue
                    foreach (string file in updatedFileList)
                    {
                        if (IsFileAvailable(file))
                        {
                            if (!mAllFileListing.Contains(file))
                            {
                                mAllFileListing.Add(file);
                                mThreadMgr.AddToProcessQueue(file, Path.Combine(mMonitorPath, file));
                            }
                        }
                    }

                }
                else
                {
                    // If the watch directory becomes unavailable we sleep and then retry
                    mThreadMgr.NotifyNetworkIssue(true);
                    System.Threading.Thread.Sleep(mRetryTime);
                }
            }
        }

        /// <summary>
        /// Stops the thread from watching the watch folder for new files.
        /// </summary>
        public void Stop()
        {
            isRunning = false;
        }

        /// <summary>
        /// Check to see if file is locked
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool IsFileAvailable(string s)
        {
            if (!File.Exists(s)) return false;
            try
            {
                using (FileStream inputStream = File.Open(s, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    if (mIsReallyUnlocked.ContainsKey(s))
                    {
                        if (mIsReallyUnlocked[s] == 1)
                        {
                            mIsReallyUnlocked[s]++;
                            System.Threading.Thread.Sleep(250);
                            return false;
                        }
                        else return true;
                    }
                    else
                        mIsReallyUnlocked.Add(s, 1);
                }
            }
            catch 
            {
                return false;
            }
            return false;
        }

        public int NetworkRetry
        {
            set { mRetryTime = value; }
        }

    }

}
