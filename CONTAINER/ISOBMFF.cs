using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaMEF
{


    public class ISOBMFF
    {

        private string m_fullname;
        private string m_filename;
        private long m_filesize;

        private FileStream m_stream;
        private BinaryReader m_br;

        private TreeNode tNode;

        private string chunk_list = "";

        public string[] veriBox = {"ftyp", "pdin", "moov", "mvhd", "trak", "tkhd", "tref", "trgr", "edts", "elst", "mdia",
            "mdhd", "elng", "minf", "vmhd", "smhd", "hmhd", "sthd", "nmhd",  "dref", "url ",
            "urn ", "stbl", "stts", "ctts", "cslg", "stsc", "stsz", "stz2", "stco", "co64", "stss",
            "stsh", "padb", "stdp", "sdtp", "sbgp", "sgpd", "subs", "saiz", "saio", "udta", "mvex", "mehd",
            "trex", "leva", "moof", "mfhd", "traf", "tfhd", "trun", "tfdt", "mfra", "tfra", "mfro", "mdat",
            "free", "skip", "cprt", "tsel", "strk", "stri", "strd", "iloc", "ipro", "rinf", "sinf", "frma",
            "schm", "iinf", "xml ", "bxml", "pitm", "fiin", "paen", "fire", "fpar", "fecr", "segr", "gitn",
            "idat", "iref", "meco", "mere", "styp", "sidx", "ssix", "prft" , "wide", "qt  ", "tapt", "clef",
            "prof", "enof", "gmhd", "alis", "avc1", "avcC", "hdlr", "meta", "stsd", "dhlr", "dinf", "uuid"};

        public string FileName { get { return m_filename; } }

        public TreeNode GetTree()
        {
            return tNode;
        }

        public bool SetFile(string fileName, bool simple_mode = false)
        {
            CHUNK rtrValue = new CHUNK();

            FileInfo fi = new FileInfo(fileName);
            m_fullname = fi.FullName;
            m_filename = fi.Name;
            m_filesize = (int)fi.Length;

            tNode = new TreeNode(m_filename); // 트리노드 클리어

            m_stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            m_br = new BinaryReader(m_stream, new ASCIIEncoding());

            return true;
        }

        private void BoxClassify(CHUNK chunk)
        {

            switch (chunk.header)
            {
                case "ftyp":
                    ftypBox(chunk);
                    break;
                case "moov":
                    containerBox(chunk);
                    break;
                case "mvhd":
                    mvhdBox(chunk);
                    break;
                case "tkhd":
                    tkhdBox(chunk);
                    break;
                case "mdhd":
                    mdhdBox(chunk);
                    break;
                case "hdlr":
                    hdlrBox(chunk);
                    break;
                default: // 일부 박스들 제외하고 대부분
                    containerBox(chunk);
                    break;
            }
        }

        private void hdlrBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();
            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "Media Header Box"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.start));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.end));

            rtrVal.Add(String.Format("version : {0}", readInt(1)));
            rtrVal.Add(String.Format("Falgs : {0}", readString(3)));
            rtrVal.Add(String.Format("Component Type : {0}", readInt(4)));
            rtrVal.Add(String.Format("Component SubType : {0}", readString(4)));
            rtrVal.Add(String.Format("Component Manufacture : {0}", readString(4)));
            rtrVal.Add(String.Format("Component Flags Mask : {0}", readString(4)));
            rtrVal.Add(String.Format("Name : {0}", readString(36)));
            moveOffset(chunk.end, SeekOrigin.Begin);
        }

        private void mdhdBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();
            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "Media Header Box"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.start));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.end));

            rtrVal.Add(String.Format("version : {0}", readInt(1)));
            rtrVal.Add(String.Format("Falgs : {0}", readString(3)));
            rtrVal.Add(String.Format("Creation Time (UTC+0) : {0}", GlobalValue.util.ConvertHFSDate(readByte(4))));
            rtrVal.Add(String.Format("Modification Time (UTC+0) : {0}", GlobalValue.util.ConvertHFSDate(readByte(4))));
            rtrVal.Add(String.Format("Time Scale : {0}", readInt(4)));
            rtrVal.Add(String.Format("Duration : {0}", readInt(4)));

            moveOffset(chunk.end, SeekOrigin.Begin);
        }

        private void tkhdBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();
            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "Track Header Box"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.start));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.end));

            rtrVal.Add(String.Format("version : {0}", readInt(1)));
            rtrVal.Add(String.Format("Falgs : {0}", readString(3)));
            rtrVal.Add(String.Format("Creation Time (UTC+0) : {0}", GlobalValue.util.ConvertHFSDate(readByte(4))));
            rtrVal.Add(String.Format("Modification Time (UTC+0) : {0}", GlobalValue.util.ConvertHFSDate(readByte(4))));
            rtrVal.Add(String.Format("Track ID : {0}", readInt(4)));
            rtrVal.Add(String.Format("Reserved : {0}", readInt(4)));
            rtrVal.Add(String.Format("Duration : {0}", readInt(4)));
            rtrVal.Add(String.Format("Reserved : {0}", readInt(8)));
            rtrVal.Add(String.Format("Layer : {0}", readInt(2)));
            rtrVal.Add(String.Format("Alternate Group : {0}", readInt(2)));
            rtrVal.Add(String.Format("Volume : {0}", readInt(2)));


            moveOffset(chunk.end, SeekOrigin.Begin);
        }

        //하위에 청크가 있는지 확인
        private bool child_checkBox(CHUNK chunk)
        {
            try
            {
                byte[] child_data = new byte[4];
                Array.Copy(chunk.data, 4, child_data, 0, 4);
                string child_type = Encoding.Default.GetString(child_data);

                bool rtrValue = Array.Exists(veriBox, x => x == child_type);
                return rtrValue;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                
            }
            return true;

        }

        private void mvhdBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();

            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "File Type Box"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.start));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.end));

            rtrVal.Add(String.Format("version : {0}", readInt(4)));
            rtrVal.Add(String.Format("Creation Time (UTC+0) : {0}", GlobalValue.util.ConvertHFSDate(readByte(4))));
            rtrVal.Add(String.Format("Modification Time (UTC+0) : {0}", GlobalValue.util.ConvertHFSDate(readByte(4))));
            rtrVal.Add(String.Format("TimeScale : {0}", readInt(4)));
            rtrVal.Add(String.Format("Duration : {0}", readInt(4)));
            rtrVal.Add(String.Format("Preferred Rate : {0}", readInt(4)));
            rtrVal.Add(String.Format("Preffered Volume : {0}", readInt(2)));

            moveOffset(chunk.end, SeekOrigin.Begin);
        }

        private void ftypBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();

            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "File Type Box"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.start));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.end));

            rtrVal.Add(String.Format("major brand : {0}", readString(4)));
            rtrVal.Add(String.Format("minor version : {0}", readInt(4)));
            rtrVal.Add(String.Format("Compatible Brand 0 : {0}", readString(4)));
            rtrVal.Add(String.Format("Compatible Brand 1 : {0}", readString(4)));

            moveOffset(chunk.end, SeekOrigin.Begin);
        }

        public bool Parse()
        {
            while (m_stream.Position < m_filesize)
            {
                CHUNK current_chunk = ChunkReader();
                
                //노드 추가
                tNode.Nodes.Add(current_chunk.header + "/" + current_chunk.start + "/" + current_chunk.end, current_chunk.header);

                BoxClassify(current_chunk); // 박스 분류

                if (current_chunk.header == "mdat")
                {
                    moveOffset(current_chunk.size);
                }
            }
            return true;
        }


        private void containerBox(CHUNK chunk)
        {
            long bytes_left = chunk.size - 8;
            if (!child_checkBox(chunk))
            {
                bytes_left = 0;
                moveOffset(chunk.end, SeekOrigin.Begin);
            }

            while (bytes_left > 7)
            {
                CHUNK child_chunk = ChunkReader();
                tNode.Nodes.Find(chunk.header + "/" + chunk.start + "/" + chunk.end, true)[0].Nodes.Add(child_chunk.header + "/" + child_chunk.start + "/" + child_chunk.end, child_chunk.header);
                BoxClassify(child_chunk);
                bytes_left -= child_chunk.size;
                
            }

        }


        private string readString(int size)
        {

            byte[] readdata = m_br.ReadBytes(size);

            return Encoding.Default.GetString(readdata);
        }

        private int readInt(int size)
        {

            byte[] readdata = m_br.ReadBytes(size);
            Array.Reverse(readdata);

            if (size == 4)
            {
                return BitConverter.ToInt32(readdata, 0);
            }
            else if (size == 2)
            {
                return BitConverter.ToInt16(readdata, 0);
            }
            else
            {
                return Convert.ToSByte(readdata[0]);
            }

        }

        private long readInt64(byte[] data)
        {

            byte[] readdata = data;
            Array.Reverse(readdata);

            return BitConverter.ToInt64(readdata, 0);

        }

        private void moveOffset(long size, SeekOrigin origin = SeekOrigin.Current)
        {
            m_stream.Seek(size, origin);
            Console.WriteLine("-- MOVE, NOW OFFSET : " + m_stream.Position);
        }
        private byte[] readByte(int size)
        {

            byte[] readdata = m_br.ReadBytes(size);

            return readdata;
        }

        private CHUNK ChunkReader(int offset = 0)
        {
            
            CHUNK chunk = new CHUNK();
            chunk.start = m_stream.Position;
            chunk.size = readInt(4);
            chunk.end = chunk.start + chunk.size;
            chunk.header = readString(4);
            chunk.data = readByte(8);
            if (chunk.size == 1) //uint64 type
            {
                chunk.size = readInt64(chunk.data);
            }
            moveOffset(-8);
            Console.WriteLine(String.Format("# {0} CHUNK : {1}", chunk.header, chunk.start));
            return chunk;
        }



    }
}
