//****************************************************************************
//Class:     FrmAddressTool.cs
//Company:   Woolpert, Inc.
//Copyright: Copyright © Woolpert 2011
//Author:    Jonathan Meyer
//Purpose:   Class used for executing a script against an input file found by
//           the monitor service.
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
    public class ProcessThread
    {
        private ThreadManager mThreadManager;
        private string mScriptFileName;
        private string mThreadID;
        private string mInputFileName;
        private string mWatchDirectory;


        /// <summary>
        /// This class is used to execute the input script file against an input file. Each input file will have
        /// a batch file created and then called by this class.
        /// </summary>
        /// <param name="threadManager"></param>
        /// <param name="threadID"></param>
        /// <param name="inputFileName"></param>
        /// <param name="scriptFilePath"></param>
        public ProcessThread(ThreadManager threadManager, string threadID, string inputFileName, string scriptFileName, string monitorDir)
        {
            mThreadManager = threadManager;
            mScriptFileName = scriptFileName;
            mThreadID = threadID;
            mInputFileName = inputFileName;
            mWatchDirectory = monitorDir;
        }

        /// <summary>
        /// Handles calling the batch file for an input file
        /// </summary>
        public void Start()
        {
            try
            {
                using (Process startInfo = new Process())
                {

                    startInfo.StartInfo.FileName = Path.Combine(mWatchDirectory, mScriptFileName);
                    startInfo.StartInfo.WorkingDirectory = mWatchDirectory;
                    startInfo.EnableRaisingEvents = true;
                    startInfo.StartInfo.UseShellExecute = false;
                    startInfo.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    startInfo.StartInfo.CreateNoWindow = true;
                    //startInfo.StartInfo.RedirectStandardOutput = true;
                    //startInfo.StartInfo.RedirectStandardError = true;

                    startInfo.Start();

                    //string errorMessages = startInfo.StandardError.ReadToEnd();
                    startInfo.WaitForExit();  // blocks until finished
                }
                mThreadManager.ThreadFinishCallback(mThreadID, mInputFileName);

            }
            catch (Exception ex)
            {
                mThreadManager.ThreadErrorCallback(mThreadID, mInputFileName, ex.Message);
            }
        }

        //public void Start()
        //{
        //    try
        //    {
        //        Process startInfo = new Process();
        //        startInfo.StartInfo.FileName = mScriptFileName;
        //        startInfo.StartInfo.WorkingDirectory = mWatchDirectory;
        //        startInfo.EnableRaisingEvents = true;
        //        startInfo.StartInfo.UseShellExecute = true;
        //        startInfo.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        //        startInfo.StartInfo.CreateNoWindow = true;

        //        startInfo.Start();

        //        startInfo.WaitForExit();  // blocks until finished

        //        mThreadManager.ThreadFinishCallback(mThreadID, mInputFileName);

        //    }
        //    catch (Exception ex)
        //    {
        //        mThreadManager.ThreadErrorCallback(mThreadID, mInputFileName, ex.Message);
        //    }

        //}
    }
}
