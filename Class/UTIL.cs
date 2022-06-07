using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DaMEF
{
    public class UTIL
    {
        public Boolean CreateFolder(string folPath)
        {
            if (folPath == "")
            {
                // GlobalValue.log.Error("생성할 폴더를 지정하지 않았음");
                return false;
            }

            DirectoryInfo di = new DirectoryInfo(folPath);

            try
            {
                if (!di.Exists)
                {
                    di.Create();
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                // GlobalValue.log.Error(e.ToString());
                return false;
            }
            return true;
        }
        public DateTime ConvertHFSDate(byte[] bytes)
        {
            if (bytes.Length != 4) throw new ArgumentException();
            Array.Reverse(bytes);
            return new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds(
                      BitConverter.ToUInt32(bytes, 0));
        }

        public string HashGenerator(string hashMode, string filePath)
        {
            string hashValue = "";
            FileInfo file = new FileInfo(filePath);
            file.IsReadOnly = false;
            StringBuilder sb = new StringBuilder();

            try
            {
                if (hashMode == "SHA1") //SHA1  인경우면 처리
                {
                    SHA1 sha1Hash = SHA1.Create();
                    using (FileStream fS = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        foreach (Byte b in sha1Hash.ComputeHash(fS))
                        {
                            sb.Append(b.ToString("X2").ToUpper());
                        }
                        hashValue = sb.ToString();
                        fS.Close();//20170329
                    }
                }
                else
                {
                    MD5 md5Hash = MD5.Create();
                    using (FileStream fS = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        foreach (Byte b in md5Hash.ComputeHash(fS))
                        {
                            sb.Append(b.ToString("X2").ToUpper());
                        }
                        hashValue = sb.ToString();
                        fS.Close();//20170329
                    }
                }
            }
            catch (Exception e)
            {
                //GlobalValue.log.Error(e.ToString() + "\n" + filePath + ": HASH Calc Error");
                throw;
            }

            return hashValue;
        }

    }
}
