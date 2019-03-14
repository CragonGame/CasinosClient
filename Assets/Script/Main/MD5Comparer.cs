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
        StringBuilder Sb { get; set; } = new StringBuilder(256);

        //---------------------------------------------------------------------
        public MD5Comparer()
        {
            Instance = this;
            MD5 = new MD5CryptoServiceProvider();
        }

        //---------------------------------------------------------------------
        public bool IsSameFile(string local_filepath, string remote_filemd5)
        {
            Sb.Clear();
            using (FileStream sr = File.OpenRead(local_filepath))
            {
                byte[] new_bytes = MD5.ComputeHash(sr);
                foreach (var bytes in new_bytes)
                {
                    Sb.Append(bytes.ToString("X2"));
                }
            }

            bool is_same = Sb.ToString().Equals(remote_filemd5);
            return is_same;
        }
    }
}
