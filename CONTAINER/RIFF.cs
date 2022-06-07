using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaMEF
{
    struct CHUNK
    {
        public long size;
        public long start;
        public long end;
        public string header;
        public byte[] data;
    }

    public class RIFF
    {

        private string m_fullname;
        private string m_filename;
        private long m_filesize;

        private FileStream m_stream;
        private BinaryReader m_br;

        private TreeNode tNode;
        private ListViewItem vItem;

        int nodeNumber;

        private string chunk_list = "";

        public string[] veriBox = {"RIFF", "LIST", "strl", "strh", "strf", "strn", "avih", "hdrl", "JUNK", "movi", "wb", "dc", "tx", "idx1",
                                    "00dc", "01dc", "02dc", "03dc", "00wb", "01wb", "02wb", "03wb", "00tx", "01tx", "02tx", "03tx",
                                    "00sb", "01sb", "02sb", "03sb", "fmt ", "data", "id3", "INFO", "IPRD", "ICRD", "IGNR", "ISFT"};

        public string FileName { get { return m_filename; } }

        public TreeNode GetTree()
        {
            return tNode;
        }

        public ListViewItem GetListView()
        {
            return vItem;
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
                case "RIFF":
                    riffBox(chunk);
                    break;
                case "avih":
                    avihBox(chunk);
                    break;
                case "strh":
                    strhBox(chunk);
                    break;
                case "strf":
                    strfBox(chunk);
                    break;
                case "LIST":
                    containerBox(chunk);
                    break;
                default: // 일부 박스들 제외하고 대부분
                    containerBox(chunk);
                    break;
            }
        }

        private void strfBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();

            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "Stream Format Data"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.start));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.end));

            rtrVal.Add(String.Format("Size : {0}", readInt(4)));
            rtrVal.Add(String.Format("Width : {0}", readInt(4)));
            rtrVal.Add(String.Format("Height : {0}", readInt(4)));
            rtrVal.Add(String.Format("Planes : {0}", readInt(2)));
            rtrVal.Add(String.Format("BitCount : {0}", readInt(2)));
            rtrVal.Add(String.Format("Compression : {0}", readInt(4)));
            rtrVal.Add(String.Format("SizeImage : {0}", readInt(4)));
            rtrVal.Add(String.Format("XPelsPerMeter : {0}", readInt(4)));
            rtrVal.Add(String.Format("YPelsPerMeter : {0}", readInt(4)));
            rtrVal.Add(String.Format("ClrUsed : {0}", readInt(4)));
            rtrVal.Add(String.Format("ClrImportant : {0}", readInt(4)));

            moveOffset(chunk.end, SeekOrigin.Begin);
        }

        private void strhBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();

            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "Stream Header Data"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.start));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.end));

            rtrVal.Add(String.Format("Type : {0}", readString(4)));
            rtrVal.Add(String.Format("Handler : {0}", readString(4)));
            rtrVal.Add(String.Format("Flags : {0}", readInt(4)));
            rtrVal.Add(String.Format("Reserved : {0}", readInt(4)));
            rtrVal.Add(String.Format("InitialFrames : {0}", readInt(4)));
            rtrVal.Add(String.Format("Scale : {0}", readInt(4)));
            rtrVal.Add(String.Format("Rate : {0}", readInt(4)));
            rtrVal.Add(String.Format("Start : {0}", readInt(4)));
            rtrVal.Add(String.Format("Length : {0}", readInt(4)));
            rtrVal.Add(String.Format("SuggestedBufferSize : {0}", readInt(4)));
            rtrVal.Add(String.Format("Quality : {0}", readInt(4)));
            rtrVal.Add(String.Format("SampleSize : {0}", readInt(4)));
            rtrVal.Add(String.Format("Quality : {0}", readInt(4)));
            rtrVal.Add(String.Format("SampleSize : {0}", readInt(4)));

            moveOffset(chunk.end, SeekOrigin.Begin);
        }

        private void avihBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();

            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "Main AVI header data"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.start));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.end));

            rtrVal.Add(String.Format("MicroSecPerFrame : {0}", readInt(4)));
            rtrVal.Add(String.Format("MaxBytesPerSec : {0}", readInt(4)));
            rtrVal.Add(String.Format("Reserved : {0}", readInt(4)));
            rtrVal.Add(String.Format("Flags : {0}", readInt(4)));
            rtrVal.Add(String.Format("TotalFrames : {0}", readInt(4)));
            rtrVal.Add(String.Format("InitialFrames : {0}", readInt(4)));
            rtrVal.Add(String.Format("Streams : {0}", readInt(4)));
            rtrVal.Add(String.Format("SuggestedBufferSize : {0}", readInt(4)));
            rtrVal.Add(String.Format("Width : {0}", readInt(4)));
            rtrVal.Add(String.Format("Height : {0}", readInt(4)));
            rtrVal.Add(String.Format("Scale : {0}", readInt(4)));
            rtrVal.Add(String.Format("Rate : {0}", readInt(4)));
            rtrVal.Add(String.Format("Start : {0}", readInt(4)));
            rtrVal.Add(String.Format("Length : {0}", readInt(4)));

            moveOffset(chunk.end, SeekOrigin.Begin);
        }

        private void tkhdBox(CHUNK chunk)
        {
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
            moveOffset(chunk.end, SeekOrigin.Begin);
        }

        private void riffBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();

            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "RIFF Chunk"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.start));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.end));

            rtrVal.Add(String.Format("Format : {0}", readString(4)));


            moveOffset(12, SeekOrigin.Begin);
        }

        public bool Parse()
        {
            int nodeCnt = 0;
            while (m_stream.Position < m_filesize)
            {
                CHUNK current_chunk = ChunkReader();
                
                // 노드 추가
                tNode.Nodes.Add(current_chunk.header + "/" + current_chunk.start + "/" + current_chunk.end, current_chunk.header);

                BoxClassify(current_chunk); // 박스 분류
            }
            return true;
        }


        private void containerBox(CHUNK chunk)
        {
            //long bytes_left = chunk.size - 8;
            long bytes_left = chunk.size;
            if (chunk.header.Contains("LIST"))
            {
                moveOffset(4);
            }
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
                if (child_chunk.size == 0)
                {
                    bytes_left -= 8;
                }
                if (child_chunk.header.Contains("LIST"))
                {
                    bytes_left -= (child_chunk.size + 8);
                }
                else
                {
                    bytes_left -= (child_chunk.size);
                }
                

            }

        }


        private string readString(int size)
        {

            byte[] readdata = m_br.ReadBytes(size);

            return Encoding.Default.GetString(readdata);
        }

        private int readInt(int size= 4)
        {

            byte[] readdata = m_br.ReadBytes(size);
            // Array.Reverse(readdata);

            if (size == 4)
            {
                return BitConverter.ToInt32(readdata, 0);
            }
            else
            {
                return BitConverter.ToInt16(readdata, 0);
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

        private CHUNK ChunkReader(int offset = 0, string type = "")
        {

            CHUNK chunk = new CHUNK();
            chunk.start = m_stream.Position;
            chunk.header = readString(4);
            if (!(chunk.header == "LIST"))
            {
                chunk.size = readInt(4) + 8;
            }
            else
            {
                chunk.size = readInt(4);
            }
            
            chunk.end = chunk.start + chunk.size;
            chunk.data = readByte(8);

            if (chunk.header == "LIST")
            {
                byte[] tmp = new byte[4];
                Array.Copy(chunk.data, 0, tmp, 0, 4);
                chunk.header = "LIST-" + Encoding.Default.GetString(tmp);

            }
            moveOffset(-8);
            Console.WriteLine(String.Format("# {0} CHUNK : {1}", chunk.header, chunk.start));
            return chunk;
        }



    }
}
