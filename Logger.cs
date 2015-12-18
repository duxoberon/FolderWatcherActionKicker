//****************************************************************************
//Class:     Logger.cs
//Company:   Woolpert, Inc.
//Copyright: Copyright © Woolpert 2011
//Author:    Jonathan Meyer
//Purpose:   Class for logging information from the Filewatcher to the output log
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

namespace FileWatcherActionKicker
{
    public static class Logger
    {

        private static readonly object sync = new object();

        /// <summary>
        /// Writes log message to log file instance.
        /// </summary>
        /// <param name="logPath"></param>
        /// <param name="message"></param>
        public static void WriteLogLine(string logPath, string message)
        {
            try
            {
                lock (sync)
                {
                    using (StreamWriter writer = File.AppendText(logPath))
                    {
                        string fullMessage = DateTime.Now.ToString() + " - " + message;
                        writer.WriteLine(fullMessage);
                        writer.Flush();
                        writer.Close();
                    }
                }

            }
            catch(Exception)
            {
            }

        }

        public static void WriteLogLine_2(string logPath, string message)
        {
            try
            {
                lock (sync)
                {
                    using (StreamWriter writer = File.AppendText(logPath))
                    {
                        writer.WriteLine(message);
                        writer.Flush();
                        writer.Close();
                    }
                }

            }
            catch (Exception)
            {
            }

        }

    }
}
