//using UnityEngine;
//using System;
//using System.IO;
//using System.Collections;


///*
// * 
// *  this demo show asinchronous inserting and reading 1000 records, yahoo...
// * 
// *  Please be pasient if you really need read or write to SSD durring gameplay
// *  you need to be pasient and focus on ditails!
// * 
// */


//public class DemoObject2 : MonoBehaviour {
	
	
	
	
//    /*
//     *   TestCallbackData represent process state. 
//     *      normally it good idea to keep here something 
//     * 		to know that operation appomplished or canceled
//     */
//    class TestCallbackData
//    {
//        private object lockobj = new object(); // empty object just to do safe thread lock
		
//        private int count;  // this is record counter
		
//        public int Count // thread safe count getter
//        { 
			
//            get { 
//                int rv = 0;
//                lock(lockobj)
//                {
//                    rv = count;
//                }
//                return rv; 
//            } 
//        }
		
//        public void Increment()  // thread safe increment
//        {
			
//            lock(lockobj)
//            {
//                count++;
//            }
//        }

//        public void Decriment()  // thread safe decriment
//        {
			
//            lock(lockobj)
//            {
//                count--;
//            }
//        }

		
//        public TestCallbackData()
//        {
//            count = 0;
//        }
//    }
	
	
//    private SQLiteAsync asyncDB = null;
//    private string queryCreate = "CREATE TABLE IF NOT EXISTS test_values (id INTEGER PRIMARY KEY, str_field TEXT, blob_field BLOB);";
//    private string queryInsert = "INSERT INTO test_values (str_field,blob_field) VALUES(?,?);";
//    private string testString = "1231 \n\r \t weqw";
//    private byte[] testBlob = new byte[] {2,3,5,78,98,21,32,255};
//    private int recordsNum = 1000;
//    private int frameCount = 0;
//    private TestCallbackData demoData = null;
	
//    // Use this for initialization
//    void Start () {
		
//        asyncDB = new SQLiteAsync();
		
//        // open database
//        asyncDB.Open(Application.persistentDataPath + "/demo_for_async_db.db",null,null);
		
//        // create test table.
//        asyncDB.Query(queryCreate, CreateQueryCreated, null);
//    }
	

//    // Update is called once per frame
//    void Update () {
//        frameCount ++;
//    }
	
//    void OnGUI()
//    {
//        if( demoData == null ) 
//        {
//            if ( GUI.Button(new Rect (10,10,150,70), "Run asynchronous") ) 
//            {
//                // that just show how to pass an object to callback.
//                demoData = new TestCallbackData();
				
//                // start from creation of 1000 records
//                asyncDB.Query(queryInsert, InsertQueryCreated, demoData);
//            }
//        }
		
		
//        // display frame count and records count - to show that game don't stop while we reading or writing.
//        string log = "Please, press the button to run test.";
//        if( demoData != null )
//        {
//            log = "Frame: " + frameCount + ", Record Count: " + demoData.Count;
//        }
//        GUI.Label (new Rect (10,80,600,600), log);
//    }
	
	
	
//    //
//    //   CREATE CALLBACKS
//    //
	
//    void CreateQueryCreated(SQLiteQuery qr, object state)
//    {
//        // call step asynchronously 
//        asyncDB.Step(qr, CreateStepInvoked, state);
//    }

	
//    void CreateStepInvoked(SQLiteQuery qr, bool step, object state)
//    {
		
//        // call release asynchronously
//        asyncDB.Release(qr, CreateQueryReleased, state);
//    }

//    void CreateQueryReleased(object state)
//    {
//        // nothing to do here
//    }
	
	
	
	
	
	
	
//    // 
//    //   INSERT CALLBACKS
//    //
//    //

//    void InsertQueryCreated(SQLiteQuery qr, object state)
//    {
//        //
//        // insert string and blob
//        //
//        qr.Bind(testString);
//        qr.Bind(testBlob);
		
//        // call step asynchronously 
//        asyncDB.Step(qr, InsertStepInvoked, state);
//    }

	
//    void InsertStepInvoked(SQLiteQuery qr, bool step, object state)
//    {
		
//        // call release asynchronously
//        asyncDB.Release(qr, InsertQueryReleased, state);
//    }


//    void InsertQueryReleased(object state)
//    {
//        // well done, we just added one more row!
//        // now check 1000 counter!
		
//        TestCallbackData data = state as TestCallbackData;
		
//        // increment counter
//        data.Increment();
		
//        // check what to do next?
//        if( data.Count < recordsNum )
//        {
//            // ok, add more please :)
//            asyncDB.Query(queryInsert, InsertQueryCreated, data); 
//        }
//        else
//        {
//            // we just added 1000 records, now read them!
//            asyncDB.Query("SELECT * FROM test_values", SelectQueryCreated, data);
//        }
//    }


	
//    //
//    //
//    // SELECT CALLBACKS
//    //
//    //
	
//    void SelectQueryCreated(SQLiteQuery qr, object state)
//    {
//        // we don't want modify query here, so do read.
//        asyncDB.Step(qr, SelectStepCallback, state);
//    }
	
	
//    void SelectStepCallback(SQLiteQuery qr, bool step, object state)
//    {
		
//        TestCallbackData data = state as TestCallbackData;
		
//        if(step) 
//        {
//            // we have something to read!
//            // do test readed values...
//            string testStringFromSelect = qr.GetString("str_field");
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
			
//            data.Decriment();
			
//            // do next read.
//            asyncDB.Step(qr, SelectStepCallback, state);
//        }
//        else
//        {
//            // we reach limit or finish reading.
//            asyncDB.Release(qr, SelectQueryReleased, null);
//        }
//    }

	
//    void SelectQueryReleased(object state)
//    {
//        // finish
//        demoData = null;
//    }



//}
