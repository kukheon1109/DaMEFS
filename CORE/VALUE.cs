using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaMEF
{
    public class VALUE
    {
        // FOR source_list
        public const int FILESYSTEM = 1;
        public const int FILE_NAME = 2;
        public const int MULTIMEDIA_FILE = 3;
        public const int RELATED_FILE = 4;
        public const int LOCATION_FILE = 5;
        public const int LOG_FILE = 6;
        public const int USER_FILE = 7;
        public const int UNKNOWN_FILE = 8;
        public const int UNUSED = 999;

        // FOR metadata_type_list
        public const int CHUNK = 10;
        public const int DATA = 20;
        public const int INFORMATION = 30;
        public const int LOCATION = 40;
        public const int RECORDING_MODE = 50;
        public const int STATUS = 60;
        public const int TIME = 70;
        public const int USER_PREFERENCE = 80;


    }
}
