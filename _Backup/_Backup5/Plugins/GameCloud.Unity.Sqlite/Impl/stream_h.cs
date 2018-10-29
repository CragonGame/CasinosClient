#define SQLITE_ASCII
#define SQLITE_DISABLE_LFS
#define SQLITE_ENABLE_OVERSIZE_CELL_CHECK
#define SQLITE_MUTEX_OMIT
#define SQLITE_OMIT_AUTHORIZATION
#define SQLITE_OMIT_DEPRECATED
#define SQLITE_OMIT_GET_TABLE
#define SQLITE_OMIT_INCRBLOB
#define SQLITE_OMIT_LOOKASIDE
#define SQLITE_OMIT_SHARED_CACHE
#define SQLITE_OMIT_UTF16
#define SQLITE_OMIT_WAL
#define SQLITE_OS_WIN
#define SQLITE_SYSTEM_MALLOC
#define VDBE_PROFILE_OFF
#define WINDOWS_MOBILE
#define NDEBUG
#define _MSC_VER
#define YYFALLBACK



using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using DWORD = System.UInt64;
using HANDLE = System.IntPtr;
using i64 = System.Int64;
using sqlite3_int64 = System.Int64;
using u32 = System.UInt32;
using u8 = System.Byte;

namespace Community.CsharpSqlite
{
  public partial class Sqlite3
  {

    public partial class sqlite3_stream
    {
      public sqlite3_stream pNext;
      public Stream pIO;             /* Stream access to this file*/
      public string zName;           /* Stream name */
    };


    static sqlite3_stream stmList;
	

    /*
    ** Create sqlite
    */
    public static sqlite3_stream sqlite3_stream_create( string zName, Stream pIO )
    {
#if !SQLITE_OMIT_AUTOINIT
      int rc = sqlite3_initialize();
      if ( rc != 0 )
        return null;
#endif

  	  if(pIO == null)
      {
  		  return null;
      }

      sqlite3_stream pFile = sqlite3_stream_find(zName);
      if(pFile == null)
      {
        pFile = new sqlite3_stream();
      }

      pFile.zName = zName;
      pFile.pIO = pIO;
      pFile.pNext = null;

      return pFile;
    }


    public static sqlite3_stream sqlite3_stream_find( string zStm )
    {
      sqlite3_stream pStm = null;
#if SQLITE_THREADSAFE
      sqlite3_mutex mutex;
#endif
#if !SQLITE_OMIT_AUTOINIT
      int rc = sqlite3_initialize();
      if ( rc != 0 )
        return null;
#endif
#if SQLITE_THREADSAFE
      mutex = sqlite3MutexAlloc( SQLITE_MUTEX_STATIC_MASTER );
#endif
      sqlite3_mutex_enter( mutex );
      for ( pStm = stmList; pStm != null; pStm = pStm.pNext )
      {
        if ( zStm == pStm.zName )
          break;
      }
      sqlite3_mutex_leave( mutex );
      return pStm;
    }

    /*
    ** Unlink a VFS from the linked list
    */
    static void stmUnlink( sqlite3_stream pStm )
    {
      Debug.Assert( sqlite3_mutex_held( sqlite3MutexAlloc( SQLITE_MUTEX_STATIC_MASTER ) ) );
      if ( pStm == null )
      {
        /* No-op */
      }
      else if ( stmList == pStm )
      {
        stmList = pStm.pNext;
      }
      else if ( stmList != null )
      {
        sqlite3_stream p = stmList;
        while ( p.pNext != null && p.pNext != pStm )
        {
          p = p.pNext;
        }
        if ( p.pNext == pStm )
        {
          p.pNext = pStm.pNext;
        }
      }
    }

    /*
    ** Register a VFS with the system.  It is harmless to register the same
    ** VFS multiple times.  The new VFS becomes the default if makeDflt is
    ** true.
    */
    public static int sqlite3_stream_register( sqlite3_stream pStm )
    {
      sqlite3_mutex mutex;
#if !SQLITE_OMIT_AUTOINIT
      int rc = sqlite3_initialize();
      if ( rc != 0 )
        return rc;
#endif
      mutex = sqlite3MutexAlloc( SQLITE_MUTEX_STATIC_MASTER );
      sqlite3_mutex_enter( mutex );
      stmUnlink( pStm );
      if ( stmList == null )
      {
        pStm.pNext = stmList;
        stmList = pStm;
      }
      else
      {
        pStm.pNext = stmList.pNext;
        stmList.pNext = pStm;
      }
      Debug.Assert( stmList != null );
      sqlite3_mutex_leave( mutex );
      return SQLITE_OK;
    }

    /*
    ** Unregister a VFS so that it is no longer accessible.
    */
    public static int sqlite3_stream_unregister( sqlite3_stream pStm )
    {
#if SQLITE_THREADSAFE
      sqlite3_mutex mutex = sqlite3MutexAlloc( SQLITE_MUTEX_STATIC_MASTER );
#endif
      sqlite3_mutex_enter( mutex );
      stmUnlink( pStm );
      sqlite3_mutex_leave( mutex );
      return SQLITE_OK;
    }
  }
}
