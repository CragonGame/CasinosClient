//#define ENCRYPTION

//using UnityEngine;
//using System.Collections;
//using System;
//using System.IO;
//using System.Text;
//using System.Threading;
//using System.Diagnostics;




//public class DemoObject : MonoBehaviour {
	
//    private SQLiteDB db = null;
//    private string log;
//    private string queryDelete = "DROP TABLE IF EXISTS test_values;";
//    private string queryCreate = "CREATE TABLE IF NOT EXISTS test_values (id INTEGER PRIMARY KEY, str_field TEXT, blob_field BLOB);";
//    private string queryInsert = "INSERT INTO test_values (str_field,blob_field) VALUES(?,?);";
//    private string querySelect = "SELECT * FROM test_values;";
//    private string testString = "1231 \n\r \t weqw";
//    private byte[] testBlob = new byte[] {2,3,5,78,98,21,32,255};


//    void Start () {

//    }
	
//    void Update () {
	
//    }
	
//    void OnGUI()
//    {
//        if( db == null )
//        {
//            if ( GUI.Button(new Rect (10,10,150,50), "Run SQLite Test \nat persistentDataPath") ) 
//            {
//                db = new SQLiteDB();
				
//                string filename = Application.persistentDataPath + "/demo_1.db";
				
//                log = "";
				
//                try{
//                    //
//                    // initialize database
//                    //
//                    db.Open(filename);                               log += "Database created! filename:"+filename;
					
//                    Test(db, ref log);
					
//                } catch (Exception e){
//                    log += 	"\nTest Fail with Exception " + e.ToString();
//                    log += 	"\n on WebPlayer it must give an exception, it's normal.";
//                }
//            }
//            if ( GUI.Button(new Rect (10,70,150,70), "Run SQLite Test \nat :memory:\nbest for cache") ) 
//            {
				
//                db = new SQLiteDB();
				
//                log = "";
				
//                try{
					
//                    //
//                    // initialize database
//                    //
//                    db.OpenInMemory();                               log += "Database created! in :memory:";
					
//                    Test(db, ref log);
					
//                } catch (Exception e){
//                    log += 	"\nTest Fail with Exception " + e.ToString();
//                    log += 	"\n";
//                }
//            }
			
//            if ( GUI.Button(new Rect (10,150,150,70), "Run SQLite Test \nwith stream\nbest for WebPlayer") ) 
//            {
				
//                db = new SQLiteDB();
				
//                MemoryStream memStream = new MemoryStream();
				
//                log = "";
				
//                try{
					
//                    //
//                    // initialize database
//                    //
//                    db = new SQLiteDB();
//                    db.OpenStream("stream1",memStream);                               log += "Database created! named stream: stream1";
					
//                    Test(db, ref log);
					
//                } catch (Exception e){
//                    log += 	"\nTest Fail with Exception " + e.ToString();
//                    log += 	"\n";
//                }
//            }
			
//            if ( GUI.Button(new Rect (10,230,150,70), "Run SQLite Test \nwith streamingAssets") ) 
//            {
				
//                StartCoroutine(TestWithStreamingAssets());
				
//            }

			
//            if ( GUI.Button(new Rect (170,10,150,70), "Run SQLite Test \nfor streamingAssets db\nwhich moved to \npersistentDataPath") ) 
//            {
				
//                StartCoroutine(TestWithStreamingAssetsCopiedToPersistentDataPath());
				
//            }

			
//            //
//            // Very common scenario for most products.
//            // 
			
//            if ( GUI.Button(new Rect (170,90,150,70), "Run SQLite\nfor real project scenario") ) 
//            {
				
//                StartCoroutine(TestBestForRealProject());
				
//            }
//#if ENCRYPTION
//            //
//            // ENCRYPTION
//            // 
			
//            if ( GUI.Button(new Rect (170,170,150,70), "Run SQLite\nfor ENCRYPTION scenario") ) 
//            {
				
//                try {
					
//                    db = new SQLiteDB();
					
//                    log = "";
	
//                    // a product persistant database path.
//                    string filename = Application.persistentDataPath + "/demo_from_encryption.db";
					
//                    File.Delete(filename);
					
//                    db.Open(filename);
	
					
//                    //
//                    // set ENCRYPTION
//                    //
//                    SQLiteQuery qr = new SQLiteQuery(db,"PRAGMA hexkey=\"0x0102030405060708090a0b0c0d0e0f10\";");
//                    qr.Step();
//                    qr.Release();
						
//                    //
//                    // create table
//                    //
//                    qr = new SQLiteQuery(db, queryCreate); 
//                    qr.Step();												
//                    qr.Release();                                        log += "\nTable created.";
					
//                    //
//                    // insert string and blob
//                    //
//                    qr = new SQLiteQuery(db, queryInsert); 
//                    qr.Bind(testString);
//                    qr.Bind(testBlob);
//                    qr.Step();
//                    qr.Release();                                        log += "\nInsert test string and blob.";
					
					
//                    // close
//                    db.Close();
										
//                    // and open again
//                    db.Open(filename);
					
//                    //
//                    // set ENCRYPTION AGAIN, you could  try change to see ENCRYPTION works
//                    //
//                    qr = new SQLiteQuery(db,"PRAGMA hexkey=\"0x0102030405060708090a0b0c0d0e0f10\";");
//                    qr.Step();
//                    qr.Release();
					
//                    // do test
//                    Test2( db, ref log );
					
//                } catch (Exception e){
//                    log += 	"\nTest Fail with Exception " + e.ToString();
//                    log += 	"\n on WebPlayer it must give an exception, it's normal.";
//                }
//            }
//#endif
			
//            //
//            // PlayerPrefs
//            // 
			
//            if ( GUI.Button(new Rect (170,250,150,50), "Copy db to\n PlayerPrefs") ) 
//            {
				
//                StartCoroutine(CopyDB2PlayerPrefs());
								
//            }
			
			
			
//            if ( GUI.Button(new Rect (170,310,150,50), "Run SQLite Test \nfor PlayerPrefs") ) 
//            {
				
//                db = new SQLiteDB();
				
//                MemoryStream memStream = new MemoryStream();
				
//                log = "";
				
//                string testdbz = PlayerPrefs.GetString("sqlitedb");
				
//                log += "Read Database from PlayerPrefs! : " + testdbz.Substring(0,64) + "...\n";
				
//                if(testdbz.Length > 0)
//                {
					
//                    for(int i=0; i*2<testdbz.Length; i++)
//                    {
//                        memStream.WriteByte(Convert.ToByte(testdbz.Substring(i*2,2),16));
//                    }
					
//                    try{
						
//                        //
//                        // initialize database
//                        //
//                        db = new SQLiteDB();
//                        db.OpenStream("stream3",memStream);                               log += "Database created from PlayerPrefs! named stream: stream3";
						
//                        Test2(db, ref log);
						
//                    } catch (Exception e){
//                        log += 	"\nTest Fail with Exception " + e.ToString();
//                        log += 	"\n";
//                    }
					
//                }
//                else
//                {
//                    log += "Please push button above to put database into PlayerPrefs!\n";
//                }
//            }

			
			
			
//        }
//        else
//        {
//            if ( GUI.Button(new Rect (10,10,150,50), "Back") ) 
//            {
//                db = null;
//            }
//            GUI.Label (new Rect (10,70,600,600), log);
//        }
//    }
	
//    IEnumerator TestWithStreamingAssets()
//    {
//        db = new SQLiteDB();
		
//        log = "";
		
//        string dbfilename = "test.db";

//        byte[] bytes = null;				
		
		
//#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
//        string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; log += "asset path is: " + dbpath;
//        WWW www = new WWW(dbpath);
//        yield return www;
//        bytes = www.bytes;
//#elif UNITY_WEBPLAYER
//        string dbpath = "StreamingAssets/" + dbfilename;								log += "asset path is: " + dbpath;
//        WWW www = new WWW(dbpath);
//        yield return www;
//        bytes = www.bytes;
//#elif UNITY_IPHONE
//        string dbpath = Application.dataPath + "/Raw/" + dbfilename;					log += "asset path is: " + dbpath;					
//        try{	
//            using ( FileStream fs = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
//                bytes = new byte[fs.Length];
//                fs.Read(bytes,0,(int)fs.Length);
//            }			
//        } catch (Exception e){
//            log += 	"\nTest Fail with Exception " + e.ToString();
//            log += 	"\n";
//        }
//#elif UNITY_ANDROID
//        string dbpath = Application.streamingAssetsPath + "/" + dbfilename;	            log += "asset path is: " + dbpath;
//        WWW www = new WWW(dbpath);
//        yield return www;
//        bytes = www.bytes;
//#endif
//        if ( bytes != null )
//        {
			
//            try{	
//                //
//                // create and write the asset into memory stream
//                //
//                MemoryStream memStream = new MemoryStream();
				
//                memStream.Write(bytes,0,bytes.Length);
				
//                //
//                // initialize database
//                //
//                db.OpenStream("stream2",memStream);                               log += "\nDatabase created! named stream: stream2";
				
//                Test2(db, ref log);
				
//            } catch (Exception e){
//                log += 	"\nTest Fail with Exception " + e.ToString();
//                log += 	"\n\n Did you copy test.db into StreamingAssets ?\n";
//            }
//        }
		
//    }	
	
	
//    IEnumerator TestWithStreamingAssetsCopiedToPersistentDataPath()
//    {
//        db = new SQLiteDB();
		
//        log = "";
		
//        string dbfilename = "test.db";

//        byte[] bytes = null;				
		
		
//#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
//        string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; log += "asset path is: " + dbpath;
//        WWW www = new WWW(dbpath);
//        yield return www;
//        bytes = www.bytes;
//#elif UNITY_WEBPLAYER
//        string dbpath = "StreamingAssets/" + dbfilename;								log += "asset path is: " + dbpath;
//        WWW www = new WWW(dbpath);
//        yield return www;
//        bytes = www.bytes;
//#elif UNITY_IPHONE
//        string dbpath = Application.dataPath + "/Raw/" + dbfilename;					log += "asset path is: " + dbpath;					
//        try{	
//            using ( FileStream fs = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
//                bytes = new byte[fs.Length];
//                fs.Read(bytes,0,(int)fs.Length);
//            }			
//        } catch (Exception e){
//            log += 	"\nTest Fail with Exception " + e.ToString();
//            log += 	"\n";
//        }
//#elif UNITY_ANDROID
//        string dbpath = Application.streamingAssetsPath + "/" + dbfilename;	            log += "asset path is: " + dbpath;
//        WWW www = new WWW(dbpath);
//        yield return www;
//        bytes = www.bytes;
//#endif
//        if ( bytes != null )
//        {
//            try{	
				
//                string filename = Application.persistentDataPath + "/demo_from_streamingAssets.db";

//                //
//                //
//                // copy database to real file into cache folder
//                using( FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write) )
//                {
//                    fs.Write(bytes,0,bytes.Length);             log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
//                }
				
//                //
//                // initialize database
//                //
//                db.Open(filename);                               log += "\nDatabase created! filename: " + filename;
				
//                Test2(db, ref log);
				
//            } catch (Exception e){
//                log += 	"\nTest Fail with Exception " + e.ToString();
//                log += 	"\n\n Did you copy test.db into StreamingAssets ?\n";
//            }
//        }
//    }
	
	
//    IEnumerator TestBestForRealProject()
//    {
//        db = new SQLiteDB();
				
//        log = "";

//        // a product persistant database path.
//        string filename = Application.persistentDataPath + "/demo_from_streamingAssets2.db";
		
//        // check if database already exists.
		
//        if(!File.Exists(filename))
//        {
			
//            // ok , this is first time application start!
//            // so lets copy prebuild dtabase from StreamingAssets and load store to persistancePath with Test2
							
//            string dbfilename = "test.db";

//            byte[] bytes = null;				
			
			
//#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
//            string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; log += "asset path is: " + dbpath;
//            WWW www = new WWW(dbpath);
//            yield return www;
//            bytes = www.bytes;
//#elif UNITY_WEBPLAYER
//            string dbpath = "StreamingAssets/" + dbfilename;								log += "asset path is: " + dbpath;
//            WWW www = new WWW(dbpath);
//            yield return www;
//            bytes = www.bytes;
//#elif UNITY_IPHONE
//            string dbpath = Application.dataPath + "/Raw/" + dbfilename;					log += "asset path is: " + dbpath;					
//            try{	
//                using ( FileStream fs = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
//                    bytes = new byte[fs.Length];
//                    fs.Read(bytes,0,(int)fs.Length);
//                }			
//            } catch (Exception e){
//                log += 	"\nTest Fail with Exception " + e.ToString();
//                log += 	"\n";
//            }
//#elif UNITY_ANDROID
//            string dbpath = Application.streamingAssetsPath + "/" + dbfilename;	            log += "asset path is: " + dbpath;
//            WWW www = new WWW(dbpath);
//            yield return www;
//            bytes = www.bytes;
//#endif
//            if ( bytes != null )
//            {
//                try{	
					
//                    //
//                    //
//                    // copy database to real file into cache folder
//                    using( FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write) )
//                    {
//                        fs.Write(bytes,0,bytes.Length);             log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
//                    }
					
//                    //
//                    // initialize database
//                    //
//                    db.Open(filename);                               log += "\nDatabase created! filename: " + filename;
					
//                    Test2(db, ref log);
					
//                } catch (Exception e){
//                    log += 	"\nTest Fail with Exception " + e.ToString();
//                    log += 	"\n\n Did you copy test.db into StreamingAssets ?\n";
//                }
//            }
//        }
//        else
//        {
//            // it mean we already download prebuild data base and store into persistantPath
//            // lest update, I will call Test
			
//            try{
//                //
//                // initialize database
//                //
//                db.Open(filename);                               log += "Database created! filename:"+filename;
				
//                Test(db, ref log);
				
//            } catch (Exception e){
//                log += 	"\nTest Fail with Exception " + e.ToString();
//                log += 	"\n on WebPlayer it must give an exception, it's normal.";
//            }
			
//        }
//    }
	
	
//    IEnumerator CopyDB2PlayerPrefs()
//    {
		
//        log = "";

//        // a product persistant database path.
//        string filename = Application.persistentDataPath + "/test.db";
		
//        // check if database already exists.
		
//        if(!File.Exists(filename))
//        {
			
//            // ok , this is first time application start!
//            // so lets copy prebuild dtabase from StreamingAssets and load store to persistancePath with Test2
							
//            string dbfilename = "test.db";

//            byte[] bytes = null;				
			
			
//#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
//            string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; log += "asset path is: " + dbpath;
//            WWW www = new WWW(dbpath);
//            yield return www;
//            bytes = www.bytes;
//#elif UNITY_WEBPLAYER
//            string dbpath = "StreamingAssets/" + dbfilename;								log += "asset path is: " + dbpath;
//            WWW www = new WWW(dbpath);
//            yield return www;
//            bytes = www.bytes;
//#elif UNITY_IPHONE
//            string dbpath = Application.dataPath + "/Raw/" + dbfilename;					log += "asset path is: " + dbpath;					
//            try{	
//                using ( FileStream fs = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
//                    bytes = new byte[fs.Length];
//                    fs.Read(bytes,0,(int)fs.Length);
//                }			
//            } catch (Exception e){
//                log += 	"\nTest Fail with Exception " + e.ToString();
//                log += 	"\n";
//            }
//#elif UNITY_ANDROID
//            string dbpath = Application.streamingAssetsPath + "/" + dbfilename;	            log += "asset path is: " + dbpath;
//            WWW www = new WWW(dbpath);
//            yield return www;
//            bytes = www.bytes;
//#endif
//            if ( bytes != null )
//            {
//                try{	
					
//                    StringBuilder sb = new StringBuilder(bytes.Length*2);
					
//                    foreach(byte b in bytes)
//                    {
//                        string bz = System.Convert.ToString(b,16);
//                        if(bz.Length == 1)
//                            bz = "0"+bz;
//                        sb.Append(bz);
//                    }
			
//                    string sqlitedb = sb.ToString();
					
//                    PlayerPrefs.SetString("sqlitedb", sqlitedb);
					
//                } catch (Exception e){
//                    log += 	"\nTest Fail with Exception " + e.ToString();
//                    log += 	"\n\n Did you copy test.db into StreamingAssets ?\n";
//                }
//            }
//        }	
//    }
	
	
//    void Test( SQLiteDB db, ref string log )
//    {
//        SQLiteQuery qr;
		
		
//        //
//        // delete table if exists
//        //
//        qr = new SQLiteQuery(db, queryDelete);
//        qr.Step();												
//        qr.Release();                                        log += "\nTable deleted.";
		
//        //
//        // create table
//        //
//        qr = new SQLiteQuery(db, queryCreate); 
//        qr.Step();												
//        qr.Release();                                        log += "\nTable created.";
		
//        //
//        // insert string and blob
//        //
//        qr = new SQLiteQuery(db, queryInsert); 
//        qr.Bind(testString);
//        qr.Bind(testBlob);
//        qr.Step();
//        qr.Release();                                        log += "\nInsert test string and blob.";
		
//        //
//        // read strings
//        //
//        string testStringFromSelect = "";
//        qr = new SQLiteQuery(db, querySelect); 
//        while( qr.Step() )
//        {
//            testStringFromSelect = qr.GetString("str_field");
//            if( testStringFromSelect != testString )
//            {
//                throw new Exception( "Test string are not equal!" );
//            }
			
//            byte[] testBlobFromSelect = qr.GetBlob("blob_field");
			
//            if( testBlobFromSelect.Length != testBlob.Length )
//            {
//                throw new Exception( "Test blobs are not equal!" );
//            }
			
//            for (int i = 0; i < testBlobFromSelect.Length; i++)
//            {
//                if( testBlobFromSelect[i] != testBlob[i] )
//                {
//                    throw new Exception( "Test blobs are not equal!" );
//                }
//            }
//        }
//        if( testStringFromSelect == "" )
//        {
//            throw new Exception( "Unknowm problem!" );
//        }
//        qr.Release();                                        log += "\nRead and test strings and blobs.";

//        //
//        //
//        // delete table
//        //
//        qr = new SQLiteQuery(db, queryDelete);
//        qr.Step();												
//        qr.Release();                                        log += "\nTable deleted.";
		
//        //
//        // if we reach that point it's mean we pass the test!
//        db.Close();                                           log += "\nDatabase closed!\nTest succeeded!";

//    }

	
	
//    void Test2( SQLiteDB db, ref string log )
//    {
//        SQLiteQuery qr;
		
//        //
//        // read strings
//        //
//        string testStringFromSelect = "";
//        qr = new SQLiteQuery(db, querySelect); 
//        while( qr.Step() )
//        {
//            testStringFromSelect = qr.GetString("str_field");
//            if( testStringFromSelect != testString )
//            {
//                throw new Exception( "Test string are not equal!" );
//            }
			
//            byte[] testBlobFromSelect = qr.GetBlob("blob_field");
			
//            if( testBlobFromSelect.Length != testBlob.Length )
//            {
//                throw new Exception( "Test blobs are not equal!" );
//            }
			
//            for (int i = 0; i < testBlobFromSelect.Length; i++)
//            {
//                if( testBlobFromSelect[i] != testBlob[i] )
//                {
//                    throw new Exception( "Test blobs are not equal!" );
//                }
//            }
//        }
//        if( testStringFromSelect == "" )
//        {
//            throw new Exception( "Unknowm problem!" );
//        }
//        qr.Release();                                        log += "\nRead and test strings and blobs.";

//        //
//        // if we reach that point it's mean we pass the test!
//        db.Close();                                           log += "\nDatabase closed!\nTest succeeded!";

//    }

	
	
	
	
	
	
	
//}
