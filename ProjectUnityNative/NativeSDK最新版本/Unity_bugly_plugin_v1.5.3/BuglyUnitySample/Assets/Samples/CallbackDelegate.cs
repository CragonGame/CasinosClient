// ----------------------------------------
//
//  CallbackHandler.cs
//
//  Author:
//       Yeelik, <bugly@tencent.com>
//
//  Copyright (c) 2015 Bugly, Tencent.  All rights reserved.
//
// ----------------------------------------
//
using UnityEngine;
using System.Collections;
using System.Threading;

public class CallbackDelegate : BuglyCallback
{
	private static readonly CallbackDelegate _instance = new CallbackDelegate();

	static CallbackDelegate(){
	}

	private CallbackDelegate(){
	}

	public static CallbackDelegate Instance {
		get {
			return _instance;
		}
	}

	/// <summary>
	/// Raises the application log callback handler event.
	/// </summary>
	/// <param name="condition">Condition.</param>
	/// <param name="stackTrace">Stack trace.</param>
	/// <param name="type">Type.</param>
	public override void OnApplicationLogCallbackHandler (string condition, string stackTrace, LogType type)
	{
        //// only for test and check the callback handler called

//		System.Console.Write ("--------- OnApplicationLogCallbackHandler ---------\n");
//
//		System.Console.Write ("Current thread: {0}", Thread.CurrentThread.ManagedThreadId);
//		System.Console.WriteLine ();
//
//		System.Console.Write ("[{0}] - {1}\n{2}",type.ToString(), condition, stackTrace);
//
//		System.Console.Write ("--------- OnApplicationLogCallbackHandler ---------");
//        System.Console.WriteLine ();
	}
}

