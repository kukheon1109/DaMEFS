using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaMEF
{
    public struct RunInfo
    {
        public string version;

        public RunInfo(string version)
        {
            this.version = version;
        }
    }


    public struct workPath
    {
        public string evidencePath;
        public string logPath;
        public string exportPath;
    }

    public class RunData
    {
        public static RunInfo _runInfo = new RunInfo("0.1");
        public static workPath _workPath = new workPath();
        public static GridData _gridData = new GridData();

        public static string _targetFile = "";
        /* VERSION 정보 */
        public static string GetVersion()
        {
            return _runInfo.version;
        }
    }
        
}


