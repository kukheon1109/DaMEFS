using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// REF: https://gist.github.com/smetronic/abca3319ba73fdd8dae377d181717a10

namespace DaMEF
{
    public class NMEA
    {
        public static CultureInfo NmeaCultureInfo = new CultureInfo("en-US");
        public static double MPHPerKnot = double.Parse("1.150779",
          NmeaCultureInfo);

        private string m_fullname;
        private string m_filename;
        private long m_filesize;

        private FileStream m_stream;
        private BinaryReader m_br;

        private string chunk_list = "";

        public string FileName { get { return m_filename; } }

        public bool SetFile(string fileName, bool simple_mode = false)
        {
            CHUNK rtrValue = new CHUNK();

            FileInfo fi = new FileInfo(fileName);
            m_fullname = fi.FullName;
            m_filename = fi.Name;
            m_filesize = (int)fi.Length;

            m_stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            return true;
        }

        public bool Parse()
        {
            using ( StreamReader sr = new StreamReader(m_fullname, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string tmpST = sr.ReadLine();
                    string type = GetWords(tmpST)[0];
                    switch (type)
                    {
                        case "$GPRMC":
                            ParseGPRMC(tmpST);
                            break;
                        case "$GSENSOR":
                            ParseGSENSOR(tmpST);
                            break;
                        default:
                            break;
                    }


                }
            }

            return true;
        }

        private string ParseGSENSOR(string sentence) // # NMEA : $GSENSOR,-219,-174,421
        {
            string[] data = GetWords(sentence);
            string rtrValue = String.Format("{0}, {1}, {2}", data[1], data[2], data[3]);
            return rtrValue;
        }

        //
        // $GPRMC,051949.00,A,3728.770523,N,12721.884153,E,0.0,280.3,030819,6.1,W,A*24\r\r\n;CAR,0,0,0,0.0,0,0,0,0,0,0,0,0\x00\x00'
        public bool ParseGPRMC(string sentence)
        {
            sentence = @"$GPRMC,051949.00,A,3728.770523,N,12721.884153,E,0.0,280.3,030819,6.1,W,A*24\r\r\n;CAR,0,0,0,0.0,0,0,0,0,0,0,0,0\x00\x00'";
            string[] Words = GetWords(sentence);
            if (Words[3] != "" && Words[4] != "" && Words[5] != "" && Words[6] != "")
            {
                string Latitude = Words[3].Substring(0, 2) + "°";
                Latitude = Latitude + Words[3].Substring(2) + "\"";
                Latitude = Latitude + Words[4]; // Append the hemisphere
                string Longitude = Words[5].Substring(0, 3) + "°";
                Longitude = Longitude + Words[5].Substring(3) + "\"";
                Longitude = Longitude + Words[6];

            }
            if (Words[1] != "")
            {
                int UtcHours = Convert.ToInt32(Words[1].Substring(0, 2));
                int UtcMinutes = Convert.ToInt32(Words[1].Substring(2, 2));
                int UtcSeconds = Convert.ToInt32(Words[1].Substring(4, 2));
                int UtcMilliseconds = 0;
                if (Words[1].Length > 7)
                {
                    UtcMilliseconds = Convert.ToInt32(float.Parse(Words[1].Substring(6), NmeaCultureInfo) * 1000);
                }
                System.DateTime Today = System.DateTime.Now.ToUniversalTime();
                System.DateTime SatelliteTime = new System.DateTime(Today.Year, Today.Month, Today.Day, UtcHours, UtcMinutes, UtcSeconds, UtcMilliseconds);
            }
            if (Words[7] != "")
            {
                double Speed = double.Parse(Words[7], NmeaCultureInfo) * MPHPerKnot;
            }
            if (Words[8] != "")
            {
                double Bearing = double.Parse(Words[8], NmeaCultureInfo);
            }

            return true;
        }



        private string[] GetWords(string sentence)
        {
            return sentence.Split(',');
        }
    }
}
