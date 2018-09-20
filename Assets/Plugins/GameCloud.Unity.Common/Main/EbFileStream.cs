// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;

    public abstract class EbFileStream
    {
        //---------------------------------------------------------------------
        protected byte[] mData;
        protected string mDataStr;

        //---------------------------------------------------------------------
        public abstract bool load(string file_name);

        //---------------------------------------------------------------------
        public abstract bool save(string file_name, byte[] data);

        //---------------------------------------------------------------------
        public byte[] getData()
        {
            return mData;
        }

        //---------------------------------------------------------------------
        public string getDataAsString()
        {
            return mDataStr;
        }
    }

    public class EbFileStreamDefault : EbFileStream
    {
        //---------------------------------------------------------------------
        public override bool load(string file_name)
        {
            FileInfo fi = new FileInfo(file_name);
            if (fi.Exists)
            {
                using (FileStream fs = File.OpenRead(file_name))
                {
                    mData = new byte[fi.Length];
                    if (fs.Read(mData, 0, mData.Length) > 0)
                    {
                        mDataStr = System.Text.Encoding.Default.GetString(mData);
                        return true;
                    }
                }
            }

            return false;
        }

        //---------------------------------------------------------------------
        public override bool save(string file_name, byte[] data)
        {
            using (FileStream stream = new FileStream(file_name, FileMode.OpenOrCreate))
            {
                stream.Write(data, 0, data.Length);
            }

            return true;
        }
    }
}