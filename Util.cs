using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileWatcherActionKicker
{
    class Util
    {

        public const string APPFOLDER = "FileWatcher";

        private static class SETTINGSTAG
        {
            public const string WATCHFOLDER = "inputfolder";
            public const string OUTPUTFOLDER = "outputfolder";
            public const string SCRIPTFOLDER = "scriptfolder";
            public const string PROCESSEXISTING = "processexisting";
            public const string NETWORKTRY = "networktry";
            public const string PROCESSINGTHREADS = "processingthreads";
            public const string LOGFOLDER = "logfolder";
        }

        protected string userFolder = string.Empty;

        protected string watchFolder = string.Empty;
        protected string outputFolder = string.Empty;
        protected string scriptFolder = string.Empty;
        protected bool processexisting = false;
        protected decimal networktry = 10;
        protected decimal processingthreads = 3;
        protected string logFolder = string.Empty;
        private const string settings = ".settings";

        public string WATCHFOLDER
        {
            set { watchFolder = value; }
            get { return watchFolder; }
        }
        public string OUTPUTFOLDER
        {
            set { outputFolder = value; }
            get
            {
                if (outputFolder.Length == 0) return watchFolder;
                if (!Directory.Exists(outputFolder)) return watchFolder;
                return outputFolder;
            }
        }
        public string SCRIPTFOLDER
        {
            set { scriptFolder = value; }
            get { return scriptFolder; }
        }
        public bool PROCESSEXISTING
        {
            set { processexisting = value; }
            get { return processexisting; }
        }
        public decimal NETWORKTRY
        {
            set { networktry = value; }
            get { return networktry; }
        }
        public decimal PROCESSINGTHREADS
        {
            set { processingthreads = value; }
            get { return processingthreads; }
        }
        public string LOGFOLDER
        {
            set { logFolder = value; }
            get { return logFolder; }
        }

        public Util()
        {
            userFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), APPFOLDER);
            if (!Directory.Exists(userFolder)) Directory.CreateDirectory(userFolder);
            GetFolderPath();
        }

        internal void SaveSettings()
        {
            setSetting(settings, watchFolder, SETTINGSTAG.WATCHFOLDER);
            setSetting(settings, outputFolder, SETTINGSTAG.OUTPUTFOLDER);
            setSetting(settings, scriptFolder, SETTINGSTAG.SCRIPTFOLDER);
            setSetting(settings, processexisting.ToString(), SETTINGSTAG.PROCESSEXISTING);
            setSetting(settings, networktry.ToString(), SETTINGSTAG.NETWORKTRY);
            setSetting(settings, processingthreads.ToString(), SETTINGSTAG.PROCESSINGTHREADS);
            setSetting(settings, logFolder, SETTINGSTAG.LOGFOLDER);
        }

        protected void GetFolderPath()
        {
            watchFolder = getSetting(settings, SETTINGSTAG.WATCHFOLDER);
            outputFolder = getSetting(settings, SETTINGSTAG.OUTPUTFOLDER);
            scriptFolder = getSetting(settings, SETTINGSTAG.SCRIPTFOLDER);

            string s = getSetting(settings, SETTINGSTAG.PROCESSEXISTING);
            if (s.Length > 0)
                bool.TryParse(s, out processexisting);
            s = getSetting(settings, SETTINGSTAG.NETWORKTRY);
            if (s.Length > 0) 
                decimal.TryParse(s, out networktry);
            s = getSetting(settings, SETTINGSTAG.PROCESSINGTHREADS);
            if (s.Length > 0) 
                decimal.TryParse(s, out processingthreads);

            logFolder = getSetting(settings, SETTINGSTAG.LOGFOLDER);
            if (logFolder.Length == 0)
            {
                logFolder = System.IO.Path.Combine(Path.GetTempPath(), APPFOLDER);
                Directory.CreateDirectory(logFolder);
            }
        }

        private string getSetting(string filename, string tag)
        {
            string filepath = System.IO.Path.Combine(userFolder, filename);
            if (File.Exists(filepath))
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] split = line.Split(';');
                        if (split.Length > 1)
                        {
                            if (string.Compare(split[0], tag, true) == 0)
                                return split[1];
                        }
                    }
                }
            }
            return string.Empty;
        }

        private void setSetting(string filename, string setting, string tag)
        {
            string tempfilepath = System.IO.Path.Combine(userFolder, "x_" + filename);
            string filepath = System.IO.Path.Combine(userFolder, filename);
            using (StreamWriter sw = new StreamWriter(tempfilepath, false))
            {
                bool found = false;
                if (System.IO.File.Exists(filepath))
                {
                    using (StreamReader sr = new StreamReader(filepath))
                    {
                        string line = string.Empty;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] split = line.Split(';');
                            if (split.Length > 1)
                            {
                                if (string.Compare(split[0], tag, true) == 0)
                                {
                                    sw.WriteLine(tag + ";" + setting);
                                    found = true;
                                }
                                else
                                    sw.WriteLine(line);
                            }
                        }
                    }
                }
                if (!found)
                    sw.WriteLine(tag + ";" + setting);
            }
            System.IO.File.Copy(tempfilepath, filepath, true);
        }

    }
}
