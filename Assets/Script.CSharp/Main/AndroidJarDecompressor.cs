// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Threading;
    using UnityEngine;
    using ICSharpCode.SharpZipLib.Zip;
    using ICSharpCode.SharpZipLib.Core;

    public class AndroidJarDecompressor
    {
        //-------------------------------------------------------------------------
        public bool IsDone { get { return mThreadEnd; } }
        public float Progress { get { return mProgress; } }

        //-------------------------------------------------------------------------
        Thread mUnzipThread;
        string mDataPath;
        string mPersistentDataPath;
        byte[] mWriteBuf = new byte[4096];
        volatile float mProgress = 0;
        volatile bool mThreadEnd = false;

        //-------------------------------------------------------------------------
        public void create(string data_path, string persistent_data_path)
        {
            _deleteExtractFiles("assets", "assets");

            mDataPath = data_path;
            mPersistentDataPath = persistent_data_path;
            mUnzipThread = new Thread(new ThreadStart(_doUnzip));
            mUnzipThread.IsBackground = true;
            mUnzipThread.Start();
        }

        //-------------------------------------------------------------------------
        public void destroy()
        {
            if (mUnzipThread != null)
            {
                mUnzipThread.Join();
                mUnzipThread = null;
            }
        }

        //-------------------------------------------------------------------------
        void _doUnzip()
        {
            string zip_file_path = mDataPath;

            ZipFile zip_file = new ZipFile(zip_file_path);
            long total_file_size = 0;
            foreach (ZipEntry e in zip_file)
            {
                if (e.IsFile)
                {
                    total_file_size += e.Size;
                }
            }

            if (total_file_size <= 0) total_file_size = 200;

            using (ZipInputStream zip_stream = new ZipInputStream(File.OpenRead(zip_file_path)))
            {
                long total_read_size = 0;
                ZipEntry current_reading_entry;

                while ((current_reading_entry = zip_stream.GetNextEntry()) != null)
                {
                    total_read_size += _doWriteFileFromZipStream(zip_stream, current_reading_entry);
                    mProgress = _clamp01((float)total_read_size / (float)total_file_size);
                }
            }

            mThreadEnd = true;
        }

        //-------------------------------------------------------------------------
        long _doWriteFileFromZipStream(ZipInputStream zip_stream, ZipEntry current_reading_entry)
        {
            string directory_name = Path.GetDirectoryName(current_reading_entry.Name);
            string file_name = Path.GetFileName(current_reading_entry.Name);
            if (!Directory.Exists(mPersistentDataPath + "/" + directory_name))
            {
                Directory.CreateDirectory(mPersistentDataPath + "/" + directory_name);
            }

            if (string.IsNullOrEmpty(file_name))
            {
                return 0;
            }

            using (FileStream streamWriter = File.Create(mPersistentDataPath + "/" + current_reading_entry.Name))
            {
                long total_size = 0;
                while (true)
                {
                    int read_size = zip_stream.Read(mWriteBuf, 0, mWriteBuf.Length);
                    total_size += read_size;
                    if (read_size > 0)
                    {
                        streamWriter.Write(mWriteBuf, 0, read_size);
                    }
                    else
                    {
                        return total_size;
                    }
                }
            }
        }

        //-------------------------------------------------------------------------
        float _clamp01(float value)
        {
            if (value < 0) return 0;
            if (value > 1) return 1;
            return value;
        }

        //-------------------------------------------------------------------------
        void _deleteExtractFiles(string dir, string delname)
        {
            if (Directory.Exists(dir))
            {
                foreach (string file_entry_name in Directory.GetFileSystemEntries(dir))
                {
                    string tmpd = file_entry_name.Substring(0, file_entry_name.LastIndexOf("/")) + "/" + delname;
                    if (Directory.Exists(file_entry_name))
                    {
                        if (file_entry_name == tmpd) Directory.Delete(file_entry_name, true);
                        else _deleteExtractFiles(file_entry_name, delname);
                    }
                    else if (File.Exists(file_entry_name))
                    {
                        if (file_entry_name == tmpd) File.Delete(file_entry_name);
                    }
                }
            }
        }
    }
}
