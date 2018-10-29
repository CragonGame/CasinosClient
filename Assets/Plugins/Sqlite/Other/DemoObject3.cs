//using UnityEngine;
//using System.Collections;
//using System;
//using System.IO;
//using System.Threading;
//using System.Diagnostics;
//using Community.CsharpSqlite;




//public class DemoObject3 : MonoBehaviour {
//    private static string FILE = "";
//    //private string queryDelete = "DROP TABLE IF EXISTS test_values;";
//    private string queryCreate = "CREATE TABLE IF NOT EXISTS test_values (id INTEGER PRIMARY KEY, str_field TEXT, blob_field BLOB);";
//    private string queryCreate2 = "CREATE TABLE IF NOT EXISTS asdasdasd (id INTEGER PRIMARY KEY, str_field TEXT, blob_field BLOB);";
//    private string queryCreate3 = "CREATE TABLE IF NOT EXISTS user2 (sno INTEGER PRIMARY KEY);";
//    private string queryKey = "PRAGMA key='0x0102030405060708090a0b0c0d0e0f10';";
	
	
	
////	private string indexQuery = "CREATE INDEX IF NOT EXISTS idx_test on test_values(str_field);";
//    private string queryInsert = "INSERT INTO test_values (str_field,blob_field) VALUES(?,?);";
//    private string querySelect = "SELECT * FROM test_values;";
//    private string testString = "1231 \n\r \t weqw";
//    private byte[] testBlob = new byte[] {2,3,5,78,98,21,32,255};

	 
//    bool test = false; 
	
//    void Start () {
//        FILE = Application.persistentDataPath + "/demo4.db";
//    }
	
//    void Update () {
	
//        if(test == true)		
//        {
			
//            test = false;
			
//            SQLiteQuery qr = null;
//            SQLiteDB db = null;
			
//            try{
//                db = new SQLiteDB();
//                db.Open(FILE);
				
//                db.Key("0x0102030405060708090a0b0c0d0e0f10");
//                // or
//                //qr = new SQLiteQuery(db, queryKey); 
//                //qr.Step();
//                //qr.Release();
//                //qr = null;
				
//                qr = new SQLiteQuery(db,queryCreate);
//                qr.Step();
//                qr.Release();
//                qr = null;
				
				
//                qr = new SQLiteQuery(db,queryCreate2);
//                qr.Step();
//                qr.Release();
//                qr = null;
					
//                qr = new SQLiteQuery(db, queryInsert); 
//                qr.Bind(testString);
//                qr.Bind(testBlob);
//                qr.Step();
//                qr.Step();
//                qr.Step();
//                qr.Release();
//                qr = null;

				
//                db.Close();
//                db = null;
				
				
//                UnityEngine.Debug.Log("Close and open again.");
	
				
//                db = new SQLiteDB();
//                db.Open(FILE);
				
//                db.Key("0x0102030405060708090a0b0c0d0e0f10");
//                // or
//                //qr = new SQLiteQuery(db, queryKey); 
//                //qr.Step();
//                //qr.Release();
//                //qr = null;
					
//                qr = new SQLiteQuery(db,queryCreate3);
//                qr.Step();
//                qr.Release();
//                qr = null;
					
//                qr = new SQLiteQuery(db, queryInsert); 
//                qr.Bind(testString);
//                qr.Bind(testBlob);
//                qr.Step();
//                qr.Step();
//                qr.Step();
//                qr.Release();
//                qr = null;
				
//                String testString2;
//                qr = new SQLiteQuery(db, querySelect); 
//                while(qr.Step())
//                {
//                    testString2 = qr.GetString("str_field");
//                    testString2.ToString();
//                }
//                qr.Release();
//                qr = null;
	
//                db.Close();
				
//                db = null;
				
//                UnityEngine.Debug.Log("Done");
//            }catch(Exception e)
//            {
//                UnityEngine.Debug.LogError(e.Message);
				
//                if(qr!=null)
//                    qr.Release();
//                qr = null;
//                if(db!=null)
//                    db.Close();
//                db=null;
//            }

//        }
		
//    }
	
//    void OnGUI()
//    {
//        if ( GUI.Button(new Rect (170,10,150,50), "Delete DB") ) 
//        {
//            UnityEngine.Debug.Log("DB Deleted");
//            File.Delete(FILE);
//        }
		
//        if ( GUI.Button(new Rect (10,10,150,50), "Encryption Test") ) 
//        {
//            test = true;
//        }
//    }

//}
