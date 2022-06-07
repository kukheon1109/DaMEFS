using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BACKUP
{
    struct CHUNK
    {
        public long size;
        public long start;
        public long end;
        public string header;
        public long offset;
    }

    public class ISOBMFF
    {

        private string m_fullname;
        private string m_filename;
        private int m_filesize;
        private int m_datasize;
        private long now_offset;
        private bool simple_mode;

        private FileStream m_stream;
        private BinaryReader m_br;

        private string chunk_list = "";


        public int DataSize { get { return m_datasize; } }
        public string FileName { get { return m_filename; } }

        public bool SetFile(string fileName, bool simple_mode = false)
        {
            this.simple_mode = simple_mode;
            CHUNK rtrValue = new CHUNK();
            
            FileInfo fi = new FileInfo(fileName);
            m_fullname = fi.FullName;
            m_filename = fi.Name;
            m_filesize = (int)fi.Length;
            now_offset = 0;

            m_stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            m_br = new BinaryReader(m_stream, new ASCIIEncoding());

            return true;
        }

        public bool ParseISOBMFF()
        {
            while(m_stream.Position < m_filesize)
            {
                CHUNK current_chunk = ChunkReader();

                BoxClassify(current_chunk);

            }
            return true;
        }



        private void containerBox(CHUNK chunk)
        {
            long bytes_left = chunk.size - 8; // header size 8

            while ( bytes_left > 7 )
            {
                if (chunk.header != "moov")
                {
                    moveOffset(chunk.end, SeekOrigin.End);
                }
                
                CHUNK child_chunk = ChunkReader();

                BoxClassify(child_chunk);
               
                bytes_left -= child_chunk.size;
                
            }

        }

        private List<string> ftypBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();
            byte[] tmp;

            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "File Type Box"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.size));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.size));
            rtrVal.Add(String.Format("major brand : {0}", readString(4)));
            rtrVal.Add(String.Format("minor version : {0}", readInt(4)));
            rtrVal.Add(String.Format("Compatible Brand 0 : {0}", readString(4)));
            rtrVal.Add(String.Format("Compatible Brand 1 : {0}", readString(4)));

            return rtrVal;
        }

        private string readString(int size)
        {
            if (GlobalValue.DEBUG_MODE)
            {
                Console.WriteLine(String.Format("READ STRING: {0}", m_stream.Position));
            }
            byte[] readdata = m_br.ReadBytes(size);

            return Encoding.Default.GetString(readdata);
        }

        private int readInt(int size)
        {
            if (GlobalValue.DEBUG_MODE)
            {
                Console.WriteLine(String.Format("READ INT: {0}", m_stream.Position));
            }
            byte[] readdata = m_br.ReadBytes(size);
            Array.Reverse(readdata);

            if (size == 4)
            {
                return BitConverter.ToInt32(readdata, 0);
            }
            else
            {
                return BitConverter.ToInt16(readdata, 0);
            }
            
        }

        private void moveOffset(long size, SeekOrigin origin = SeekOrigin.Current)
        {
            m_stream.Seek(size, origin);
            if (GlobalValue.DEBUG_MODE)
            {
                Console.WriteLine(String.Format("MOVE OFFSET: {0}", m_stream.Position));
            }
        }
        private byte[] readByte(int size)
        {
            if (GlobalValue.DEBUG_MODE)
            {
                Console.WriteLine(String.Format("READ BYTE: {0}", m_stream.Position));
            }
            byte[] readdata = m_br.ReadBytes(size);

            return readdata;
        }

        private CHUNK ChunkReader(int offset = 0)
        {
            if (GlobalValue.DEBUG_MODE)
            {
                Console.WriteLine(String.Format("READ CHUNK: {0}", m_stream.Position));
            }

            CHUNK chunk = new CHUNK();
            chunk.start = m_stream.Position;
            chunk.size = readInt(4);
            chunk.end = chunk.start + chunk.size;
            chunk.header = readString(4);

            if (GlobalValue.DEBUG_MODE)
            {
                Console.WriteLine(String.Format("******* CHUNK TYPE: {0}", chunk.header));
            }
            return chunk;
        }

        private bool BoxClassify(CHUNK chunk)
        {

            switch (chunk.header)
            {
                case "ftyp":
                    ftypBox(chunk);
                    return false;
                    break;
                case "moov":
                    containerBox(chunk);
                    return false;
                    break;
                case "mvhd":
                    mvhdBox(chunk);
                    return false;
                    break;
                default : // 일부 박스들 제외하고 대부분
                    containerBox(chunk);
                    return true;
                    break;
            }
        }

        private List<string> mvhdBox(CHUNK chunk)
        {
            List<string> rtrVal = new List<string>();
            byte[] tmp;

            rtrVal.Add(String.Format("Type : {0}", chunk.header));
            rtrVal.Add(String.Format("Description : {0}", "Movie Header Box"));
            rtrVal.Add(String.Format("Size : {0}", chunk.size));
            rtrVal.Add(String.Format("Start Offset : {0}", chunk.size));
            rtrVal.Add(String.Format("End Offset : {0}", chunk.size));
            rtrVal.Add(String.Format("version : {0}", readInt(4)));
            rtrVal.Add(String.Format("Creation Time (UTC+0) : {0}", GlobalValue.DP.ConvertHFSDate(readByte(4))));
            rtrVal.Add(String.Format("Modification Time (UTC+0) : {0}", GlobalValue.DP.ConvertHFSDate(readByte(4))));
            rtrVal.Add(String.Format("TimeScale : {0}", readInt(4)));
            rtrVal.Add(String.Format("Duration : {0}", readInt(4)));
            rtrVal.Add(String.Format("Preferred Rate : {0}", readInt(4)));
            rtrVal.Add(String.Format("Preffered Volume : {0}", readInt(2)));
            moveOffset(74);
                        
            return rtrVal;
        }
    }
}
