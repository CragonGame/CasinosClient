// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public abstract class EbData
    {
        //---------------------------------------------------------------------
        public int Id { get; internal set; }

        //---------------------------------------------------------------------
        public abstract void load(EbTableBuffer table_buf);
    }

    public class EbTableBuffer
    {
        //---------------------------------------------------------------------
        MemoryStream MemoryStream { get; set; }
        byte[] Buffer { get; set; }
        string mTableName;
        int mReadPos;
        int mWriteLen;

        //---------------------------------------------------------------------
        public string TableName { get { return mTableName; } }

        //---------------------------------------------------------------------
        public EbTableBuffer(string tb_name)
        {
            MemoryStream = new MemoryStream();
            Buffer = new byte[1024];
            mTableName = tb_name;
            mReadPos = 0;
            mWriteLen = 0;
        }

        //---------------------------------------------------------------------
        public EbTableBuffer(byte[] buf, string tb_name)
        {
            MemoryStream = new MemoryStream(buf);
            Buffer = new byte[1024];
            mTableName = tb_name;
            mReadPos = 0;
            mWriteLen = buf.Length;
        }

        //---------------------------------------------------------------------
        public void Close()
        {
            if (MemoryStream != null)
            {
                MemoryStream.Close();
                MemoryStream = null;
            }
            Buffer = null;
            mReadPos = 0;
            mWriteLen = 0;
        }

        //---------------------------------------------------------------------
        public byte[] GetTableData()
        {
            return MemoryStream.ToArray();
        }

        //---------------------------------------------------------------------
        public bool IsReadEnd()
        {
            return mReadPos >= mWriteLen;
        }

        //---------------------------------------------------------------------
        public void WriteInt(int value)
        {
            mWriteLen += sizeof(int);
            var data = BitConverter.GetBytes(value);
            MemoryStream.Write(data, 0, data.Length);
        }

        //---------------------------------------------------------------------
        public void WriteFloat(float value)
        {
            mWriteLen += sizeof(float);
            var data = BitConverter.GetBytes(value);
            MemoryStream.Write(data, 0, data.Length);
        }

        //---------------------------------------------------------------------
        public void WriteString(string value)
        {
            byte[] str_data = null;
            short str_len = 0;
            if (!string.IsNullOrEmpty(value))
            {
                str_data = System.Text.Encoding.UTF8.GetBytes(value);
                str_len = (short)str_data.Length;
            }

            mWriteLen += sizeof(short);
            var data = BitConverter.GetBytes(str_len);
            MemoryStream.Write(data, 0, data.Length);

            if (str_len > 0)
            {
                mWriteLen += str_len;
                MemoryStream.Write(str_data, 0, str_data.Length);
            }
        }

        //---------------------------------------------------------------------
        public void WriteEnd()
        {
            MemoryStream.Seek(0, SeekOrigin.Begin);
        }

        //---------------------------------------------------------------------
        public int ReadInt()
        {
            mReadPos += sizeof(int);
            MemoryStream.Read(Buffer, 0, sizeof(int));
            return BitConverter.ToInt32(Buffer, 0);
        }

        //---------------------------------------------------------------------
        public float ReadFloat()
        {
            mReadPos += sizeof(float);
            MemoryStream.Read(Buffer, 0, sizeof(float));
            return BitConverter.ToSingle(Buffer, 0);
        }

        //---------------------------------------------------------------------
        public string ReadString()
        {
            mReadPos += sizeof(short);
            MemoryStream.Read(Buffer, 0, sizeof(short));
            short str_len = BitConverter.ToInt16(Buffer, 0);
            if (str_len > 0)
            {
                if (str_len > Buffer.Length)
                {
                    Buffer = new byte[str_len + 128];
                }

                mReadPos += str_len;
                MemoryStream.Read(Buffer, 0, str_len);

                return System.Text.Encoding.UTF8.GetString(Buffer, 0, (int)str_len);
            }
            else
            {
                return "";
            }
        }
    }
}
