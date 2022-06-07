using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaMEF
{
    public static class GlobalValue
    {
        public static DataSet MasterDS = new DataSet();
        public static UTIL util = new UTIL();

        public static ISOBMFF isobmff = new ISOBMFF();
        public static NMEA nmea = new NMEA();
        public static RIFF riff = new RIFF();

        public static Metadata_Analysis MA = new Metadata_Analysis();
        public static string selectedFile = "";

        public const bool DEBUG_MODE = true;

    }
}
