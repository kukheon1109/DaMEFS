using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaMEF
{
    public class Metadata_Analysis
    {
        public void analysis_module_classifire(DataRow row)
        {
            // 공통적으로 파일 정보 등록하는 프로세스가 있어야함

            switch (row["FILE_EXT"].ToString().ToUpper())
            {
                case ".MP4":
                    GlobalValue.isobmff.SetFile(row["FILE_PATH"].ToString());
                    GlobalValue.isobmff.Parse();
                    break;
                case ".NMEA":
                    GlobalValue.nmea.SetFile(row["FILE_PATH"].ToString());
                    break;
                case ".AVI":
                    GlobalValue.riff.SetFile(row["FILE_PATH"].ToString());
                    GlobalValue.riff.Parse();
                    break;
                default:
                    break;
            }
            Console.WriteLine("HERE");
        }

        //파일 정보 등록
        public bool insert_file_info()
        {
            return true;
        }

        //폴더 정보 등록
        public bool insert_directory_info()
        {
            return true;
        }
    }
}
