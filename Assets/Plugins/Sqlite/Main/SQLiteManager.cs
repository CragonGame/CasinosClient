//using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;

public class SQLiteManager //: MonoBehaviour
{
	
	#region Public
	
	public SQLiteAsync GetSQLiteAsync(string name)
	{
		if( !dbs.ContainsKey(name) )
		{
			dbs[name] = new SQLiteAsync();
		}
		return dbs[name];
	}

	#endregion
	
    #region Singleton
	
	static SQLiteManager instance = null;
	
	/*
	 *   the excuse for initialized variable because follow error happned on if(instance == false) at non main thread
	 * 
		CompareBaseObjects  can only be called from the main thread.
		Constructors and field initializers will be executed from the loading thread when loading a scene.
		Don't use this function in the constructor or field initializers, instead move initialization code to the Awake or Start function.

	 * */
	
    public static SQLiteManager Instance
    {
        get
        {
			if(instance == null)
			{
                //GameObject obj = GameObject.Find("SQLiteManager");
                //if( obj == null )
                //{
                //    obj = new GameObject("SQLiteManager");
                //}
				
                //// paranoia code :)
                //instance = obj.GetComponent<SQLiteManager>();
				
                //if(instance == null)
                //{
                //    instance = obj.AddComponent("SQLiteManager") as SQLiteManager;
                //}

                instance = new SQLiteManager();
				
			}
			
            return instance;
        }
    }
    #endregion
	
	#region Implementation
	
	Dictionary<string,SQLiteAsync> dbs = new Dictionary<string, SQLiteAsync>();
	
	private SQLiteManager()
	{
	}
	
	void Start() 
	{
		//DontDestroyOnLoad(gameObject);
	}
	
	#endregion
}
