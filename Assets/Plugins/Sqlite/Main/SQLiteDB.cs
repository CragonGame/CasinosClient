using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Community.CsharpSqlite;


public class SQLiteDB 
{
	
    
	private Sqlite3.sqlite3        db;
	private Sqlite3.sqlite3_stream stream;
	
#region Query Registration
	private List<SQLiteQuery>      queries = new List<SQLiteQuery>();
	
    public void RegisterQuery(SQLiteQuery qr)
    {
        queries.Add(qr);
    }

    public void UnregisterQuery(SQLiteQuery qr)
    {
        queries.Remove(qr);
    }

    public void ReleaseAllQueries()
    {
        SQLiteQuery[] qrs = queries.ToArray();
        foreach (SQLiteQuery q in qrs)
        {
            q.Release();
        }
        queries.Clear();
    }
#endregion
	
	public SQLiteDB()
	{
		db = null;
		stream = null;
	}
	
	public void Open(string filename)
	{
		if( db != null )
		{
			throw new Exception( "Error database already open!" );
		}
		
		if ( Sqlite3.sqlite3_open( filename, out db ) != Sqlite3.SQLITE_OK )
		{
			db = null;
			throw new IOException( "Error with opening database " + filename + " !" );
		}
	}

	public void OpenInMemory() 
	{
		if( db != null )
		{
			throw new Exception( "Error database already open!" );
		}
		
		if ( Sqlite3.sqlite3_open( ":memory:", out db ) != Sqlite3.SQLITE_OK )
		{
			db = null;
			throw new IOException( "Error with opening database :memory:!" );
		}
	}
	
	public void OpenStream(string name, Stream io) 
	{
		if( db != null )
		{
			throw new Exception( "Error database already open!" );
		}
		
		stream = Sqlite3.sqlite3_stream_create(name, io);

        if ( Sqlite3.sqlite3_stream_register(stream) != Sqlite3.SQLITE_OK )
        {
            throw new IOException("Error with opening database with stream " + name + "!");
        }

        if (Sqlite3.sqlite3_open_v2(name, out db, Sqlite3.SQLITE_OPEN_READWRITE, "stream") != Sqlite3.SQLITE_OK)
		{
			db = null;
			throw new IOException( "Error with opening database with stream " + name + "!" );
		}
	}
	
	public void Key(string hexkey)
	{
		Sqlite3.sqlite3_key(db,hexkey,hexkey.Length);
	}

	public void Rekey(string hexkey)
	{
		Sqlite3.sqlite3_rekey(db,hexkey,hexkey.Length);
	}
	
	public Sqlite3.sqlite3 Connection()
	{
		return db;
	}
	
	public long LastInsertRowId()
	{
		if( db == null )
		{
			throw new Exception( "Error database not ready!" );
		}
		
		return Sqlite3.sqlite3_last_insert_rowid(db);
	}
	
	public void Close()
	{
		
		ReleaseAllQueries();
		
		if( db != null )
		{
			Sqlite3.sqlite3_close( db );
			db = null;
		}
		
		if( stream != null )
		{
			Sqlite3.sqlite3_stream_unregister(stream);
		}
	}
	
}
