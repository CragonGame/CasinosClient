// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class DataInfo
    {
        public string data_name;
        public int data_type;
        public object data_value;
    }

    public class TbDataInfo
    {
        public int Id;
        public List<DataInfo> ListDataInfo;
    }

    public abstract class ISqlite
    {
        //---------------------------------------------------------------------
        public abstract bool openDb(string file_path);

        //---------------------------------------------------------------------
        public abstract bool openDb(string db_name, Stream stream);

        //---------------------------------------------------------------------
        public abstract void closeDb();

        //---------------------------------------------------------------------
        public abstract List<string> getAllTableName(string sqlite_query);

        //---------------------------------------------------------------------
        public abstract Dictionary<int, object> getAllTableData(string sqlite_query);

        //---------------------------------------------------------------------
        public abstract List<TbDataInfo> getTableData(string sqlite_query);

        //---------------------------------------------------------------------
        public abstract Dictionary<string, object> selectFromDb(string sqlite_query);

        //---------------------------------------------------------------------
        public bool fileExists(string file_path)
        {
            return System.IO.File.Exists(file_path);
        }
    }

    public class SqliteUnity : ISqlite
    {
        //---------------------------------------------------------------------
        private const int SQLITE_OK = 0;
        private const int SQLITE_ROW = 100;
        private const int SQLITE_DONE = 101;
        private const int SQLITE_INTEGER = 1;
        private const int SQLITE_FLOAT = 2;
        private const int SQLITE_TEXT = 3;
        private const int SQLITE_BLOB = 4;
        private const int SQLITE_NULL = 5;
        //Connection handle
        private IntPtr connection;
        private string mFilePath;

        //---------------------------------------------------------------------
        public SqliteUnity()
        {
        }

        //---------------------------------------------------------------------
        public override bool openDb(string file_path)
        {
            mFilePath = file_path;

            if (!fileExists(mFilePath))
            {
                return false;
            }

            if (sqlite3_open(mFilePath, out connection) != SQLITE_OK)
            {
                EbLog.Note("Could not open database file:" + mFilePath);
                return false;
            }

            return true;
        }

        //---------------------------------------------------------------------
        public override bool openDb(string db_name, Stream stream)
        {
            if (sqlite3_open(mFilePath, out connection) != SQLITE_OK)
            {
                EbLog.Note("Could not open database file:" + mFilePath);
                return false;
            }

            return true;
        }

        //---------------------------------------------------------------------
        public override void closeDb()
        {
            sqlite3_close(connection);
        }

        //---------------------------------------------------------------------
        public override List<string> getAllTableName(string sqlite_query)
        {
            List<string> list_tablename = new List<string>();
            IntPtr stmHandle = Prepare(sqlite_query);
            int columnCount = sqlite3_column_count(stmHandle);
            while (sqlite3_step(stmHandle) == SQLITE_ROW)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    string s = Marshal.PtrToStringAnsi(sqlite3_column_text(stmHandle, i));
                    string name = Marshal.PtrToStringAnsi(sqlite3_column_name(stmHandle, i));
                    if (!name.Equals("name"))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(s) || s.Contains("sqlite_")) continue;
                    if (list_tablename.Contains(s))
                    {
                        continue;
                    }
                    list_tablename.Add(s);
                }
            }

            return list_tablename;
        }

        //---------------------------------------------------------------------
        public override Dictionary<int, object> getAllTableData(string sqlite_query)
        {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------
        public override List<TbDataInfo> getTableData(string sqlite_query)
        {
            List<TbDataInfo> list_table_data = new List<TbDataInfo>();
            IntPtr stmHandle = Prepare(sqlite_query);
            int columnCount = sqlite3_column_count(stmHandle);
            while (sqlite3_step(stmHandle) == SQLITE_ROW)
            {
                TbDataInfo tbdata_info = new TbDataInfo();
                List<DataInfo> list_data = new List<DataInfo>();
                int id = -1;
                for (int i = 0; i < columnCount; i++)
                {
                    int data_type = sqlite3_column_type(stmHandle, i);
                    string name = Marshal.PtrToStringAnsi(sqlite3_column_name(stmHandle, i));

                    DataInfo data_info = new DataInfo();
                    data_info.data_type = data_type;
                    data_info.data_name = name;
                    object data_value = "";
                    switch (data_type)
                    {
                        case 1:// SQLITE_INTEGER
                            {
                                data_value = sqlite3_column_int(stmHandle, i);
                            }
                            break;
                        case 2:// SQLITE_FLOAT
                            {
                                data_value = sqlite3_column_double(stmHandle, i);
                            }
                            break;
                        case 3:// SQLITE_TEXT
                            {
                                data_value = Marshal.PtrToStringAnsi(sqlite3_column_text(stmHandle, i));
                            }
                            break;
                    }
                    data_info.data_value = data_value;
                    list_data.Add(data_info);                  
                    if (name.Equals("Id"))
                    {
                        id = (int)data_value;
                    }
                }
                tbdata_info.Id = id;
                tbdata_info.ListDataInfo = list_data;
                list_table_data.Add(tbdata_info);
            }

            return list_table_data;
        }

        //---------------------------------------------------------------------
        public override Dictionary<string, object> selectFromDb(string sqlite_query)
        {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// This will prepare a certain query where stmHandle is pointing or handling
        /// </summary>
        private IntPtr Prepare(string query)
        {
            IntPtr stmHandle;

            if (sqlite3_prepare_v2(connection, query, -1, out stmHandle, IntPtr.Zero) != SQLITE_OK)
            {
                IntPtr errorMsg = sqlite3_errmsg(connection);
                EbLog.Note(Marshal.PtrToStringAnsi(errorMsg) + ":" + query);
            }

            return stmHandle;
        }

        //---------------------------------------------------------------------
        //Importing needed dll for sqlite3
        [DllImport("sqlite3", EntryPoint = "sqlite3_open")]
        private static extern int sqlite3_open(string filename, out IntPtr db);

        [DllImport("sqlite3", EntryPoint = "sqlite3_close")]
        private static extern int sqlite3_close(IntPtr db);

        [DllImport("sqlite3", EntryPoint = "sqlite3_prepare_v2")]
        private static extern int sqlite3_prepare_v2(IntPtr db, string zSql, int nByte, out IntPtr ppStmpt, IntPtr pzTail);

        [DllImport("sqlite3", EntryPoint = "sqlite3_step")]
        private static extern int sqlite3_step(IntPtr stmHandle);

        [DllImport("sqlite3", EntryPoint = "sqlite3_finalize")]
        private static extern int sqlite3_finalize(IntPtr stmHandle);

        [DllImport("sqlite3", EntryPoint = "sqlite3_errmsg")]
        private static extern IntPtr sqlite3_errmsg(IntPtr db);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_count")]
        private static extern int sqlite3_column_count(IntPtr stmHandle);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_name")]
        private static extern IntPtr sqlite3_column_name(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_type")]
        private static extern int sqlite3_column_type(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_int")]
        private static extern int sqlite3_column_int(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_text")]
        private static extern IntPtr sqlite3_column_text(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_double")]
        private static extern double sqlite3_column_double(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_blob")]
        private static extern IntPtr sqlite3_column_blob(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3", EntryPoint = "sqlite3_column_bytes")]
        private static extern int sqlite3_column_bytes(IntPtr stmHandle, int iCol);
        //#if UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_STANDALONE_LINUX || UNITY_WEBPLAYER
        //#endif
    }
    public class SqliteWin : ISqlite
    {
        private string mFilePath;
        private SQLiteDB mSQLiteDB;

        //---------------------------------------------------------------------
        public SqliteWin()
        {
            mSQLiteDB = new SQLiteDB();
        }

        //---------------------------------------------------------------------
        public override bool openDb(string file_path)
        {
            mFilePath = file_path;

            if (!fileExists(mFilePath))
            {
                return false;
            }

            mSQLiteDB.Open(mFilePath);
            return true;
        }

        //---------------------------------------------------------------------
        public override bool openDb(string db_name, Stream stream)
        {
            mSQLiteDB.OpenStream(db_name, stream);
            return true;
        }

        //---------------------------------------------------------------------
        public override void closeDb()
        {
            mSQLiteDB.Close();
        }

        //---------------------------------------------------------------------
        public override List<string> getAllTableName(string sqlite_query)
        {
            List<string> list_tablename = new List<string>();
            SQLiteQuery qr = new SQLiteQuery(mSQLiteDB, sqlite_query);
            while (qr.Step())
            {
                string s = qr.GetString("name");
                if (string.IsNullOrEmpty(s) || s.Contains("sqlite_")) continue;
                if (list_tablename.Contains(s))
                {
                    continue;
                }
                list_tablename.Add(s);
            }
            qr.Release();

            return list_tablename;
        }

        //---------------------------------------------------------------------
        public override Dictionary<int, object> getAllTableData(string sqlite_query)
        {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------
        public override List<TbDataInfo> getTableData(string sqlite_query)
        {
            SQLiteQuery qr = new SQLiteQuery(mSQLiteDB, sqlite_query);
            List<TbDataInfo> list_table_data = new List<TbDataInfo>();

            while (qr.Step())
            {
                TbDataInfo tbdata_info = new TbDataInfo();
                string[] data_names = qr.Names;
                List<DataInfo> list_data = new List<DataInfo>();
                int id = -1;
                foreach (var i in data_names)
                {
                    bool is_null = qr.IsNULL(i);
                    if (is_null)
                    {
                        continue;
                    }

                    int field_type = qr.GetFieldType(i);
                    DataInfo data_info = new DataInfo();
                    data_info.data_type = field_type;
                    data_info.data_name = i;
                    object data_value = "";
                    switch (field_type)
                    {
                        case 1:// SQLITE_INTEGER
                            {
                                data_value = qr.GetInteger(i);
                            }
                            break;
                        case 2:// SQLITE_FLOAT
                            {
                                data_value = qr.GetDouble(i);
                            }
                            break;
                        case 3:// SQLITE_TEXT
                            {
                                data_value = qr.GetString(i);
                            }
                            break;
                    }
                    data_info.data_value = data_value;
                    list_data.Add(data_info);
                    if (i.Equals("Id"))
                    {
                        id = (int)data_value;
                    }                
                }

                tbdata_info.Id = id;
                tbdata_info.ListDataInfo = list_data;
                list_table_data.Add(tbdata_info);
            }

            return list_table_data;
        }

        //---------------------------------------------------------------------
        public override Dictionary<string, object> selectFromDb(string sqlite_query)
        {
            throw new NotImplementedException();
        }
    }
}
