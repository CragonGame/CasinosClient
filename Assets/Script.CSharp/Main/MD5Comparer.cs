// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class MD5Comparer
    {
        //---------------------------------------------------------------------
        public static MD5Comparer Instance { get; private set; }
        MD5 MD5 { get; set; }

        //---------------------------------------------------------------------
        public MD5Comparer()
        {
            Instance = this;
            MD5 = new MD5CryptoServiceProvider();
        }

        //---------------------------------------------------------------------
        public bool isSameFile(string local_filepath, string remote_filemd5)
        {
            StringBuilder sb = new StringBuilder();
            using (FileStream sr = File.OpenRead(local_filepath))
            {
                byte[] new_bytes = MD5.ComputeHash(sr);
                foreach (var bytes in new_bytes)
                {
                    sb.Append(bytes.ToString("X2"));
                }
            }

            bool is_same = sb.ToString().Equals(remote_filemd5);
            return is_same;
        }
    }
}