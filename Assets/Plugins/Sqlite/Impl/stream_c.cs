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

    public partial class sqlite3_file
    {
      public Stream pIO;             /* Stream access to this file*/
      public string zName;           /* Stream name */
    };


    static int stmClose( sqlite3_file id )
    {

      Debug.Assert( id != null );

#if SQLITE_TEST
      OSTRACE( "CLOSE %d %s\n", pFile.pIO.GetHashCode(), rc ? "ok" : "failed" );
      OpenCounter( -1 );
#endif
      return SQLITE_OK;
    }


    /*
    ** Read data from a file into a buffer.  Return SQLITE_OK if all
    ** bytes were read successfully and SQLITE_IOERR if anything goes
    ** wrong.
    */
    static int stmRead(
    sqlite3_file id,           /* File to read from */
    byte[] pBuf,               /* Write content into this buffer */
    int amt,                   /* Number of bytes to read */
    sqlite3_int64 offset       /* Begin reading at this offset */
    )
    {

      //long rc;
      sqlite3_file pFile = id;
      int nRead;                    /* Number of bytes actually read from file */

      Debug.Assert( id != null );
#if SQLITE_TEST
      if ( SimulateIOError() )
        return SQLITE_IOERR_READ;
#endif
#if SQLITE_DEBUG
      OSTRACE( "READ %d lock=%d\n", pFile.pIO.GetHashCode(), pFile.locktype );
#endif

      if ( pFile.pIO.Seek(offset, SeekOrigin.Begin) != offset )
      {
        return SQLITE_FULL;
      }

      try
      {
        nRead = pFile.pIO.Read( pBuf, 0, amt ); // i  if( null==ReadFile(pFile->h, pBuf, amt, &nRead, 0) ){
      }
      catch ( Exception )
      {

		pFile.lastErrno = 1;
        return winLogError(SQLITE_IOERR_READ, "stmRead", pFile.zPath);
      }
      if ( nRead < amt )
      {
        /* Unread parts of the buffer must be zero-filled */
        Array.Clear( pBuf, (int)nRead, (int)( amt - nRead ) ); // memset(&((char)pBuf)[nRead], 0, amt-nRead);
        return SQLITE_IOERR_SHORT_READ;
      }
      return SQLITE_OK;
    }

    /*
    ** Write data from a buffer into a file.  Return SQLITE_OK on success
    ** or some other error code on failure.
    */
    static int stmWrite(
    sqlite3_file id,          /* File to write into */
    byte[] pBuf,              /* The bytes to be written */
    int amt,                  /* Number of bytes to write */
    sqlite3_int64 offset      /* Offset into the file to begin writing at */
    )
    {
      int rc = 0;                         /* True if error has occured, else false */
      sqlite3_file pFile = id;        /* File handle */

      Debug.Assert( amt > 0 );
      Debug.Assert( pFile != null );

#if SQLITE_TEST
      if ( SimulateIOError() )
        return SQLITE_IOERR_WRITE;
      if ( SimulateDiskfullError() )
        return SQLITE_FULL;
#endif
#if SQLITE_DEBUG
      OSTRACE( "WRITE %d lock=%d\n", id.pIO.GetHashCode(), id.locktype );
#endif

      pFile.pIO.Seek(offset, SeekOrigin.Begin);

      long wrote = pFile.pIO.Position;
      try
      {
        Debug.Assert( pBuf.Length >= amt );
        id.pIO.Write( pBuf, 0, amt );
        rc = 1;// Success
        wrote = pFile.pIO.Position - wrote;
      }
      catch ( IOException )
      {
        return SQLITE_READONLY;
      }

      if ( rc == 0 || amt > (int)wrote )
      {
		id.lastErrno  = 1;

        if (( id.lastErrno == ERROR_HANDLE_DISK_FULL )
        || ( id.lastErrno == ERROR_DISK_FULL ))
        {
          return SQLITE_FULL;
        }
        else
        {
          return winLogError(SQLITE_IOERR_WRITE, "winWrite", pFile.zPath);
        }
      }
      return SQLITE_OK;
    }

    /*
    ** Truncate an open file to a specified size
    */
    static int stmTruncate( sqlite3_file id, sqlite3_int64 nByte )
    {
      sqlite3_file pFile = id;        /* File handle object */
      int rc = SQLITE_OK;             /* Return code for this function */

      Debug.Assert( pFile != null );
#if SQLITE_DEBUG
      OSTRACE( "TRUNCATE %d %lld\n", id.zName, nByte );
#endif
#if SQLITE_TEST
      if ( SimulateIOError() )
        return SQLITE_IOERR_TRUNCATE;
      if ( SimulateIOError() )
        return SQLITE_IOERR_TRUNCATE;
#endif

      /* If the user has configured a chunk-size for this file, truncate the
** file so that it consists of an integer number of chunks (i.e. the
** actual file size after the operation may be larger than the requested
** size).
*/

      if ( pFile.szChunk != 0 )
      {
        nByte = ( ( nByte + pFile.szChunk - 1 ) / pFile.szChunk ) * pFile.szChunk;
      }

      /* SetEndOfFile() returns non-zero when successful, or zero when it fails. */
      //if ( seekWinFile( pFile, nByte ) )
      //{
      //  rc = winLogError(SQLITE_IOERR_TRUNCATE, "winTruncate1", pFile->zPath);
      //}
      //else if( 0==SetEndOfFile(pFile->h) ){
      //  pFile->lastErrno = GetLastError();
      //  rc = winLogError(SQLITE_IOERR_TRUNCATE, "winTruncate2", pFile->zPath);
      //}
      try
      {
        id.pIO.SetLength( nByte );
        rc = SQLITE_OK;
      }
      catch ( IOException )
      {
		id.lastErrno  = 1;
        rc = winLogError(SQLITE_IOERR_TRUNCATE, "stmTruncate", pFile.zPath);
      }
      OSTRACE( "TRUNCATE %d %lld %s\n", id.pIO.GetHashCode(), nByte, rc == SQLITE_OK ? "ok" : "failed" );
      return rc;
    }

#if SQLITE_TEST
    /*
** Count the number of fullsyncs and normal syncs.  This is used to test
** that syncs and fullsyncs are occuring at the right times.
*/
#if !TCLSH
    static int sqlite3_sync_count = 0;
    static int sqlite3_fullsync_count = 0;
#else
    static tcl.lang.Var.SQLITE3_GETSET sqlite3_sync_count = new tcl.lang.Var.SQLITE3_GETSET( "sqlite3_sync_count" );
    static tcl.lang.Var.SQLITE3_GETSET sqlite3_fullsync_count = new tcl.lang.Var.SQLITE3_GETSET( "sqlite_fullsync_count" );
#endif
#endif

    /*
** Make sure all writes to a particular file are committed to disk.
*/
    static int stmSync( sqlite3_file id, int flags )
    {
#if !(NDEBUG) || !(SQLITE_NO_SYNC) || (SQLITE_DEBUG)
      sqlite3_file pFile = (sqlite3_file)id;
      //bool rc;
#else
UNUSED_PARAMETER(id);
#endif
      Debug.Assert( pFile != null );
      /* Check that one of SQLITE_SYNC_NORMAL or FULL was passed */
      Debug.Assert( ( flags & 0x0F ) == SQLITE_SYNC_NORMAL
      || ( flags & 0x0F ) == SQLITE_SYNC_FULL
      );

      OSTRACE( "SYNC %d lock=%d\n", pFile.pIO.GetHashCode(), pFile.locktype );

  /* Unix cannot, but some systems may return SQLITE_FULL from here. This
  ** line is to test that doing so does not cause any problems.
  */
#if SQLITE_TEST
        if ( SimulateDiskfullError() )
        return SQLITE_FULL;
#endif
#if !SQLITE_TEST
UNUSED_PARAMETER(flags);
#else
      if ( (flags&0x0F)==SQLITE_SYNC_FULL )
      {
#if !TCLSH
        sqlite3_fullsync_count++;
      }
      sqlite3_sync_count++;
#else
        sqlite3_fullsync_count.iValue++;
      }
      sqlite3_sync_count.iValue++;
#endif
#endif


      /* If we compiled with the SQLITE_NO_SYNC flag, then syncing is a
** no-op
*/
#if SQLITE_NO_SYNC
return SQLITE_OK;
#else
      //pFile.fs.Flush();
      return SQLITE_OK;
  //rc = FlushFileBuffers(pFile->h);
  //SimulateIOError( rc=FALSE );
  //if( rc ){
  //  return SQLITE_OK;
  //}else{
  //  pFile->lastErrno = GetLastError();
  //  return winLogError(SQLITE_IOERR_FSYNC, "winSync", pFile->zPath);
  //}

#endif
    }

    /*
    ** Determine the current size of a file in bytes
    */
    static int stmFileSize( sqlite3_file id, ref long pSize )
    {
      //DWORD upperBits;
      //DWORD lowerBits;
      //  sqlite3_file pFile = (sqlite3_file)id;
      //  DWORD error;
      Debug.Assert( id != null );
#if SQLITE_TEST
      if ( SimulateIOError() )
        return SQLITE_IOERR_FSTAT;
#endif
      pSize = id.pIO.Length;
      return SQLITE_OK;
    }

    /*
    ** Lock the file with the lock specified by parameter locktype - one
    ** of the following:
    **
    **     (1) SHARED_LOCK
    **     (2) RESERVED_LOCK
    **     (3) PENDING_LOCK
    **     (4) EXCLUSIVE_LOCK
    **
    ** Sometimes when requesting one lock state, additional lock states
    ** are inserted in between.  The locking might fail on one of the later
    ** transitions leaving the lock state different from what it started but
    ** still short of its goal.  The following chart shows the allowed
    ** transitions and the inserted intermediate states:
    **
    **    UNLOCKED . SHARED
    **    SHARED . RESERVED
    **    SHARED . (PENDING) . EXCLUSIVE
    **    RESERVED . (PENDING) . EXCLUSIVE
    **    PENDING . EXCLUSIVE
    **
    ** This routine will only increase a lock.  The winUnlock() routine
    ** erases all locks at once and returns us immediately to locking level 0.
    ** It is not possible to lower the locking level one step at a time.  You
    ** must go straight to locking level 0.
    */
    static int stmLock( sqlite3_file id, int locktype )
    {
      int rc = SQLITE_OK;         /* Return code from subroutines */
      int res = 1;                /* Result of a windows lock call */
      int newLocktype;            /* Set pFile.locktype to this value before exiting */
      bool gotPendingLock = false;/* True if we acquired a PENDING lock this time */
      sqlite3_file pFile = (sqlite3_file)id;
      DWORD error = NO_ERROR;

      Debug.Assert( id != null );
#if SQLITE_DEBUG
      OSTRACE( "LOCK %d %d was %d(%d)\n",
      pFile.pIO.GetHashCode(), locktype, pFile.locktype, pFile.sharedLockByte );
#endif
      /* If there is already a lock of this type or more restrictive on the
** OsFile, do nothing. Don't use the end_lock: exit path, as
** sqlite3OsEnterMutex() hasn't been called yet.
*/
      if ( pFile.locktype >= locktype )
      {
        return SQLITE_OK;
      }

      /* Make sure the locking sequence is correct
      */
      Debug.Assert( pFile.locktype != NO_LOCK || locktype == SHARED_LOCK );
      Debug.Assert( locktype != PENDING_LOCK );
      Debug.Assert( locktype != RESERVED_LOCK || pFile.locktype == SHARED_LOCK );

      /* Lock the PENDING_LOCK byte if we need to acquire a PENDING lock or
      ** a SHARED lock.  If we are acquiring a SHARED lock, the acquisition of
      ** the PENDING_LOCK byte is temporary.
      */
      newLocktype = pFile.locktype;
      if ( pFile.locktype == NO_LOCK
      || ( ( locktype == EXCLUSIVE_LOCK )
      && ( pFile.locktype == RESERVED_LOCK ) )
      )
      {
        int cnt = 3;
        res = 0;
        while ( cnt-- > 0 && res == 0 )//(res = LockFile(pFile.fs.SafeFileHandle.DangerousGetHandle().ToInt32(), PENDING_BYTE, 0, 1, 0)) == 0)
        {
          try
          {
            //lockingStrategy.LockFile( pFile, PENDING_BYTE, 1 );
            res = 1;
          }
          catch ( Exception )
          {
            /* Try 3 times to get the pending lock.  The pending lock might be
            ** held by another reader process who will release it momentarily.
            */
#if SQLITE_DEBUG
            OSTRACE( "could not get a PENDING lock. cnt=%d\n", cnt );
#endif
            Thread.Sleep( 1 );
          }
        }
        gotPendingLock = ( res != 0 );
        if ( 0 == res )
        {
			error = 1;
	    }
      }

      /* Acquire a shared lock
      */
      if ( locktype == SHARED_LOCK && res != 0 )
      {
        Debug.Assert( pFile.locktype == NO_LOCK );
        newLocktype = SHARED_LOCK;
      }

      /* Acquire a RESERVED lock
      */
      if ( ( locktype == RESERVED_LOCK ) && res != 0 )
      {
        Debug.Assert( pFile.locktype == SHARED_LOCK );
        try
        {
          //lockingStrategy.LockFile( pFile, RESERVED_BYTE, 1 );//res = LockFile(pFile.fs.SafeFileHandle.DangerousGetHandle().ToInt32(), RESERVED_BYTE, 0, 1, 0);
          newLocktype = RESERVED_LOCK;
          res = 1;
        }
        catch ( Exception )
        {
          res = 0;
          error = 1;
        }
        if ( res != 0 )
        {
          newLocktype = RESERVED_LOCK;
        }
        else
        {
			error = 1;
        }
      }

      /* Acquire a PENDING lock
      */
      if ( locktype == EXCLUSIVE_LOCK && res != 0 )
      {
        newLocktype = PENDING_LOCK;
        gotPendingLock = false;
      }

      /* Acquire an EXCLUSIVE lock
      */
      if ( locktype == EXCLUSIVE_LOCK && res != 0 )
      {
        Debug.Assert( pFile.locktype >= SHARED_LOCK );
#if SQLITE_DEBUG
        OSTRACE( "unreadlock = %d\n", res );
#endif
        //res = LockFile(pFile.fs.SafeFileHandle.DangerousGetHandle().ToInt32(), SHARED_FIRST, 0, SHARED_SIZE, 0);
        try
        {
          //lockingStrategy.LockFile( pFile, SHARED_FIRST, SHARED_SIZE );
          newLocktype = EXCLUSIVE_LOCK;
          res = 1;
        }
        catch ( Exception )
        {
          res = 0;
        }
        if ( res != 0 )
        {
          newLocktype = EXCLUSIVE_LOCK;
        }
        else
        {
          error = 1;
#if SQLITE_DEBUG
          OSTRACE( "error-code = %d\n", error );
#endif
        }
      }

      /* If we are holding a PENDING lock that ought to be released, then
      ** release it now.
      */
      if ( gotPendingLock && locktype == SHARED_LOCK )
      {
        //lockingStrategy.UnlockFile( pFile, PENDING_BYTE, 1 );
      }

      /* Update the state of the lock has held in the file descriptor then
      ** return the appropriate result code.
      */
      if ( res != 0 )
      {
        rc = SQLITE_OK;
      }
      else
      {
#if SQLITE_DEBUG
        OSTRACE( "LOCK FAILED %d trying for %d but got %d\n", pFile.pIO.GetHashCode(),
        locktype, newLocktype );
#endif
        pFile.lastErrno = error;
        rc = SQLITE_BUSY;
      }
      pFile.locktype = (u8)newLocktype;
      return rc;
    }

    /*
    ** This routine checks if there is a RESERVED lock held on the specified
    ** file by this or any other process. If such a lock is held, return
    ** non-zero, otherwise zero.
    */
    static int stmCheckReservedLock( sqlite3_file id, ref int pResOut )
    {
      int rc;
      sqlite3_file pFile = (sqlite3_file)id;

      if ( SimulateIOError() )
        return SQLITE_IOERR_CHECKRESERVEDLOCK;

      Debug.Assert( id != null );
      if ( pFile.locktype >= RESERVED_LOCK )
      {
        rc = 1;
#if SQLITE_DEBUG
        OSTRACE( "TEST WR-LOCK %d %d (local)\n", pFile.zName, rc );
#endif
      }
      else
      {
        try
        {
          rc = 1;
        }
        catch ( IOException )
        {
          rc = 0;
        }
        rc = 1 - rc; // !rc
#if SQLITE_DEBUG
        OSTRACE( "TEST WR-LOCK %d %d (remote)\n", pFile.pIO.GetHashCode(), rc );
#endif
      }
      pResOut = rc;
      return SQLITE_OK;
    }

    /*
    ** Lower the locking level on file descriptor id to locktype.  locktype
    ** must be either NO_LOCK or SHARED_LOCK.
    **
    ** If the locking level of the file descriptor is already at or below
    ** the requested locking level, this routine is a no-op.
    **
    ** It is not possible for this routine to fail if the second argument
    ** is NO_LOCK.  If the second argument is SHARED_LOCK then this routine
    ** might return SQLITE_IOERR;
    */
    static int stmUnlock( sqlite3_file id, int locktype )
    {
      int type;
      sqlite3_file pFile = (sqlite3_file)id;
      int rc = SQLITE_OK;
      Debug.Assert( pFile != null );
      Debug.Assert( locktype <= SHARED_LOCK );

#if SQLITE_DEBUG
      OSTRACE( "UNLOCK %d to %d was %d(%d)\n", pFile.pIO.GetHashCode(), locktype,
      pFile.locktype, pFile.sharedLockByte );
#endif
      type = pFile.locktype;
      if ( type >= EXCLUSIVE_LOCK )
      {
      }
      if ( type >= RESERVED_LOCK )
      {

      }
      if ( locktype == NO_LOCK && type >= SHARED_LOCK )
      {
        
      }
      if ( type >= PENDING_LOCK )
      {

      }
      pFile.locktype = (u8)locktype;
      return rc;
    }

    /*
    ** Control and query of the open file handle.
    */
    static int stmFileControl( sqlite3_file id, int op, ref sqlite3_int64 pArg )
    {
      switch ( op )
      {
        case SQLITE_FCNTL_LOCKSTATE:
          {
            pArg = (int)( (sqlite3_file)id ).locktype;
            return SQLITE_OK;
          }
        case SQLITE_LAST_ERRNO:
          {
            pArg = (int)( (sqlite3_file)id ).lastErrno;
            return SQLITE_OK;
          }
        case SQLITE_FCNTL_CHUNK_SIZE:
          {
            ( (sqlite3_file)id ).szChunk = (int)pArg;
            return SQLITE_OK;
          }
        case SQLITE_FCNTL_SIZE_HINT:
          {
            sqlite3_int64 sz = (sqlite3_int64)pArg;
            SimulateIOErrorBenign( 1 );
            stmTruncate( id, sz );
            SimulateIOErrorBenign( 0 );
            return SQLITE_OK;
          }
        case SQLITE_FCNTL_SYNC_OMITTED:
          {
            return SQLITE_OK;
          }
      }
      return SQLITE_NOTFOUND;
    }

    /*
    ** Return the sector size in bytes of the underlying block device for
    ** the specified file. This is almost always 512 bytes, but may be
    ** larger for some devices.
    **
    ** SQLite code assumes this function cannot fail. It also assumes that
    ** if two files are created in the same file-system directory (i.e.
    ** a database and its journal file) that the sector size will be the
    ** same for both.
    */
    static int stmSectorSize( sqlite3_file id )
    {
      Debug.Assert( id != null );
      return SQLITE_DEFAULT_SECTOR_SIZE;
    }

    /*
    ** Return a vector of device characteristics.
    */
    static int stmDeviceCharacteristics( sqlite3_file id )
    {
      UNUSED_PARAMETER( id );
      return 0;
    }


    static int stmShmMap(
    sqlite3_file fd,                /* Handle open on database file */
    int iRegion,                    /* Region to retrieve */
    int szRegion,                   /* Size of regions */
    int isWrite,                    /* True to extend file if necessary */
    out object pp                   /* OUT: Mapped memory */
    )
    {
      pp = null;
      return 0;
    }

    
    static int stmShmLock(
    sqlite3_file fd,           /* Database file holding the shared memory */
    int ofst,                  /* First lock to acquire or release */
    int n,                     /* Number of locks to acquire or release */
    int flags                  /* What to do with the lock */
    )
    {
      return 0;
    }

    
    static void stmShmBarrier(
    sqlite3_file fd          /* Database holding the shared memory */
    )
    {
    }

    
    static int stmShmUnmap(
    sqlite3_file fd,           /* Database holding shared memory */
    int deleteFlag             /* Delete after closing if true */
    )
    {
      return 0;
    }

   
    /*
    ** Open a file.
    */
    static int stmOpen (
    sqlite3_vfs pVfs,       /* Not used */
    string zName,           /* Name of the file (UTF-8) */
    sqlite3_file pFile,   /* Write the SQLite file handle here */
    int flags,              /* Open mode flags */
    out int pOutFlags       /* Status return flags */
    )
    {
      sqlite3_stream stm = sqlite3_stream_find (zName);
      if (null == stm) {
        stm = sqlite3_stream_create(zName,new MemoryStream());
        sqlite3_stream_register(stm);
      }

      pFile.Clear();
      pFile.pMethods = stmIoMethod;
      pFile.zName = zName;
      pFile.pIO = stm.pIO;
      pFile.lastErrno = NO_ERROR;
      pOutFlags = SQLITE_OPEN_READWRITE;
      return SQLITE_OK;
    }

    /*
    ** Delete the named file.
    **
    ** Note that windows does not allow a file to be deleted if some other
    ** process has it open.  Sometimes a virus scanner or indexing program
    ** will open a journal file shortly after it is created in order to do
    ** whatever it does.  While this other process is holding the
    ** file open, we will be unable to delete it.  To work around this
    ** problem, we delay 100 milliseconds and try to delete again.  Up
    ** to MX_DELETION_ATTEMPTs deletion attempts are run before giving
    ** up and returning an error.
    */
    static int stmDelete(
    sqlite3_vfs pVfs,         /* Not used on win32 */
    string zName,             /* Name of file to delete */
    int syncDir               /* Not used on win32 */
    )
    {
      UNUSED_PARAMETER( pVfs );
      UNUSED_PARAMETER( syncDir );
      sqlite3_stream stm = sqlite3_stream_find (zName);
      if (null != stm) {
        sqlite3_stream_unregister(stm);
      }
      return SQLITE_OK;
    }

    /*
    ** Check the existence and status of a file.
    */
    static int stmAccess(
    sqlite3_vfs pVfs,       /* Not used on win32 */
    string zFilename,       /* Name of file to check */
    int flags,              /* Type of test to make on this file */
    out int pResOut         /* OUT: Result */
    )
    {
      UNUSED_PARAMETER( pVfs );

#if SQLITE_TEST
      if ( SimulateIOError() )
      {
        pResOut = -1;
        return SQLITE_IOERR_ACCESS;
      }
#endif
      pResOut = sqlite3_stream_find(zFilename) != null ? 1 : 0;
      return SQLITE_OK;
    }

    /*
    ** Turn a relative pathname into a full pathname.  Write the full
    ** pathname into zOut[].  zOut[] will be at least pVfs.mxPathname
    ** bytes in size.
    */
    static int stmFullPathname(
    sqlite3_vfs pVfs,             /* Pointer to vfs object */
    string zRelative,             /* Possibly relative input path */
    int nFull,                    /* Size of output buffer in bytes */
    StringBuilder zFull           /* Output buffer */
    )
    {
      string zOut = zRelative;
      if ( zOut != null )
      {
        // sqlite3_snprintf(pVfs.mxPathname, zFull, "%s", zOut);
        if ( zFull.Length > pVfs.mxPathname )
          zFull.Length = pVfs.mxPathname;
        zFull.Append( zOut );

        // will happen on exit; was   free(zOut);
        return SQLITE_OK;
      }
      else
      {
        return SQLITE_NOMEM;
      }
    }


    /*
    ** Get the sector size of the device used to store
    ** file.
    */
    static int getSectorSize2(
    sqlite3_vfs pVfs,
    string zRelative     /* UTF-8 file name */
    )
    {
      return SQLITE_DEFAULT_SECTOR_SIZE;
    }

    static HANDLE stmDlOpen( sqlite3_vfs vfs, string zFilename )
    {
      return new HANDLE();
    }
    static int stmDlError( sqlite3_vfs vfs, int nByte, string zErrMsg )
    {
      return 0;
    }
    static HANDLE stmDlSym( sqlite3_vfs vfs, HANDLE data, string zSymbol )
    {
      return new HANDLE();
    }
    static int stmDlClose( sqlite3_vfs vfs, HANDLE data )
    {
      return 0;
    }


    /*
** Write up to nBuf bytes of randomness into zBuf.
*/

    //[StructLayout( LayoutKind.Explicit, Size = 16, CharSet = CharSet.Ansi )]
    //public class _SYSTEMTIME
    //{
    //  [FieldOffset( 0 )]
    //  public u32 byte_0_3;
    //  [FieldOffset( 4 )]
    //  public u32 byte_4_7;
    //  [FieldOffset( 8 )]
    //  public u32 byte_8_11;
    //  [FieldOffset( 12 )]
    //  public u32 byte_12_15;
    //}
    //[DllImport( "Kernel32.dll" )]
    //private static extern bool QueryPerformanceCounter( out long lpPerformanceCount );

    static int stmRandomness( sqlite3_vfs pVfs, int nBuf, byte[] zBuf )
    {
      int n = 0;
      UNUSED_PARAMETER( pVfs );
#if (SQLITE_TEST)
      n = nBuf;
      Array.Clear( zBuf, 0, n );// memset( zBuf, 0, nBuf );
#else
byte[] sBuf = BitConverter.GetBytes(System.DateTime.Now.Ticks);
zBuf[0] = sBuf[0];
zBuf[1] = sBuf[1];
zBuf[2] = sBuf[2];
zBuf[3] = sBuf[3];
;// memcpy(&zBuf[n], x, sizeof(x))
n += 16;// sizeof(x);
if ( sizeof( DWORD ) <= nBuf - n )
{
//DWORD pid = GetCurrentProcessId();
u32 processId;
#if !SQLITE_SILVERLIGHT
processId = (u32)Process.GetCurrentProcess().Id; 
#else
processId = 28376023;
#endif
put32bits( zBuf, n, processId);//(memcpy(&zBuf[n], pid, sizeof(pid));
n += 4;// sizeof(pid);
}
if ( sizeof( DWORD ) <= nBuf - n )
{
//DWORD cnt = GetTickCount();
System.DateTime dt = new System.DateTime();
put32bits( zBuf, n, (u32)dt.Ticks );// memcpy(&zBuf[n], cnt, sizeof(cnt));
n += 4;// cnt.Length;
}
if ( sizeof( long ) <= nBuf - n )
{
long i;
i = System.DateTime.UtcNow.Millisecond;// QueryPerformanceCounter(out i);
put32bits( zBuf, n, (u32)( i & 0xFFFFFFFF ) );//memcpy(&zBuf[n], i, sizeof(i));
put32bits( zBuf, n, (u32)( i >> 32 ) );
n += sizeof( long );
}
#endif
      return n;
    }


    /*
    ** Sleep for a little while.  Return the amount of time slept.
    */
    static int stmSleep( sqlite3_vfs pVfs, int microsec )
    {
      Thread.Sleep( (( microsec + 999 ) / 1000 ));
      UNUSED_PARAMETER( pVfs );
      return ( ( microsec + 999 ) / 1000 ) * 1000;
    }

    /*
    ** The following variable, if set to a non-zero value, is interpreted as
    ** the number of seconds since 1970 and is used to set the result of
    ** sqlite3OsCurrentTime() during testing.
    */
#if SQLITE_TEST
#if !TCLSH
    static int sqlite3_current_time = 0;//  /* Fake system time in seconds since 1970. */
#else
    static tcl.lang.Var.SQLITE3_GETSET sqlite3_current_time = new tcl.lang.Var.SQLITE3_GETSET( "sqlite3_current_time" );
#endif
#endif

    /*
** Find the current time (in Universal Coordinated Time).  Write into *piNow
** the current time and date as a Julian Day number times 86_400_000.  In
** other words, write into *piNow the number of milliseconds since the Julian
** epoch of noon in Greenwich on November 24, 4714 B.C according to the
** proleptic Gregorian calendar.
**
** On success, return 0.  Return 1 if the time and date cannot be found.
*/
    static int stmCurrentTimeInt64( sqlite3_vfs pVfs, ref sqlite3_int64 piNow )
    {
      /* FILETIME structure is a 64-bit value representing the number of
      100-nanosecond intervals since January 1, 1601 (= JD 2305813.5).
      */
      //var ft = new FILETIME();
      const sqlite3_int64 winFiletimeEpoch = 23058135 * (sqlite3_int64)8640000;
#if SQLITE_TEST
      const sqlite3_int64 unixEpoch = 24405875 * (sqlite3_int64)8640000;
#endif

      ///* 2^32 - to avoid use of LL and warnings in gcc */
      //const sqlite3_int64 max32BitValue =
      //(sqlite3_int64)2000000000 + (sqlite3_int64)2000000000 + (sqlite3_int64)294967296;

      //#if SQLITE_OS_WINCE
      //SYSTEMTIME time;
      //GetSystemTime(&time);
      ///* if SystemTimeToFileTime() fails, it returns zero. */
      //if (!SystemTimeToFileTime(&time,&ft)){
      //return 1;
      //}
      //#else
      //      GetSystemTimeAsFileTime( ref ft );
      //      ft = System.DateTime.UtcNow.ToFileTime();
      //#endif
      //sqlite3_int64 ft = System.DateTime.UtcNow.ToFileTime();
      //piNow = winFiletimeEpoch + ft;
      //((((sqlite3_int64)ft.dwHighDateTime)*max32BitValue) + 
      //   (sqlite3_int64)ft.dwLowDateTime)/(sqlite3_int64)10000;
      piNow = winFiletimeEpoch + System.DateTime.UtcNow.ToFileTimeUtc() / (sqlite3_int64)10000;
#if SQLITE_TEST
#if !TCLSH
      if ( ( sqlite3_current_time) != 0 )
      {
        piNow = 1000 * (sqlite3_int64)sqlite3_current_time + unixEpoch;
      }
#else
      if ( ( sqlite3_current_time.iValue ) != 0 )
      {
        piNow = 1000 * (sqlite3_int64)sqlite3_current_time.iValue + unixEpoch;
      }
#endif
#endif
        UNUSED_PARAMETER( pVfs );
      return 0;
    }


    /*
    ** Find the current time (in Universal Coordinated Time).  Write the
    ** current time and date as a Julian Day number into *prNow and
    ** return 0.  Return 1 if the time and date cannot be found.
    */
    static int stmCurrentTime( sqlite3_vfs pVfs, ref double prNow )
    {
      int rc;
      sqlite3_int64 i = 0;
      rc = stmCurrentTimeInt64( pVfs, ref i );
      if ( 0 == rc )
      {
        prNow = i / 86400000.0;
      }
      return rc;
    }

    /*
    ** The idea is that this function works like a combination of
    ** GetLastError() and FormatMessage() on windows (or errno and
    ** strerror_r() on unix). After an error is returned by an OS
    ** function, SQLite calls this function with zBuf pointing to
    ** a buffer of nBuf bytes. The OS layer should populate the
    ** buffer with a nul-terminated UTF-8 encoded error message
    ** describing the last IO error to have occurred within the calling
    ** thread.
    **
    ** If the error message is too large for the supplied buffer,
    ** it should be truncated. The return value of xGetLastError
    ** is zero if the error message fits in the buffer, or non-zero
    ** otherwise (if the message was truncated). If non-zero is returned,
    ** then it is not necessary to include the nul-terminator character
    ** in the output buffer.
    **
    ** Not supplying an error message will have no adverse effect
    ** on SQLite. It is fine to have an implementation that never
    ** returns an error message:
    **
    **   int xGetLastError(sqlite3_vfs pVfs, int nBuf, string zBuf){
    **     Debug.Assert(zBuf[0]=='\0');
    **     return 0;
    **   }
    **
    ** However if an error message is supplied, it will be incorporated
    ** by sqlite into the error message available to the user using
    ** sqlite3_errmsg(), possibly making IO errors easier to debug.
    */
    static int stmGetLastError( sqlite3_vfs pVfs, int nBuf, ref string zBuf )
    {
      UNUSED_PARAMETER( pVfs );
      return getLastErrorMsg( nBuf, ref zBuf );
    }

    static sqlite3_vfs streamVfs = new sqlite3_vfs(
    3,                              /* iVersion */
    -1, //sqlite3_file.Length,      /* szOsFile */
    MAX_PATH,                       /* mxPathname */
    null,                           /* pNext */
    "stream",                        /* zName */
    0,                              /* pAppData */

    (dxOpen)stmOpen,                /* xOpen */
    (dxDelete)stmDelete,            /* xDelete */
    (dxAccess)stmAccess,            /* xAccess */
    (dxFullPathname)stmFullPathname,/* xFullPathname */
    (dxDlOpen)stmDlOpen,            /* xDlOpen */
    (dxDlError)stmDlError,          /* xDlError */
    (dxDlSym)stmDlSym,              /* xDlSym */
    (dxDlClose)stmDlClose,          /* xDlClose */
    (dxRandomness)stmRandomness,    /* xRandomness */
    (dxSleep)stmSleep,              /* xSleep */
    (dxCurrentTime)stmCurrentTime,  /* xCurrentTime */
    (dxGetLastError)stmGetLastError,/* xGetLastError */
    (dxCurrentTimeInt64)stmCurrentTimeInt64, /* xCurrentTimeInt64 */
    null,                           /* xSetSystemCall */
    null,                           /* xGetSystemCall */
    null                            /* xNextSystemCall */
    );

    /*
    ** This vector defines all the methods that can operate on an
    ** sqlite3_file for win32.
    */
    static sqlite3_io_methods stmIoMethod = new sqlite3_io_methods(
    2,                                                  /* iVersion */
    (dxClose)stmClose,                                  /* xClose */
    (dxRead)stmRead,                                    /* xRead */
    (dxWrite)stmWrite,                                  /* xWrite */
    (dxTruncate)stmTruncate,                            /* xTruncate */
    (dxSync)stmSync,                                    /* xSync */
    (dxFileSize)stmFileSize,                            /* xFileSize */
    (dxLock)stmLock,                                    /* xLock */
    (dxUnlock)stmUnlock,                                /* xUnlock */
    (dxCheckReservedLock)stmCheckReservedLock,          /* xCheckReservedLock */
    (dxFileControl)stmFileControl,                      /* xFileControl */
    (dxSectorSize)stmSectorSize,                        /* xSectorSize */
    (dxDeviceCharacteristics)stmDeviceCharacteristics,  /* xDeviceCharacteristics */
    (dxShmMap)stmShmMap,                                /* xShmMap */
    (dxShmLock)stmShmLock,                              /* xShmLock */
    (dxShmBarrier)stmShmBarrier,                        /* xShmBarrier */
    (dxShmUnmap)stmShmUnmap                             /* xShmUnmap */
    );

  }
}

