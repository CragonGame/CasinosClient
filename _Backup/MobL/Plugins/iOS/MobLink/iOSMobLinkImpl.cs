using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace com.moblink.unity3d
{
	#if UNITY_IPHONE
	public class iOSMobLinkImpl : MobLinkImpl 
	{
		[DllImport("__Internal")]
		private static extern void __iosMobLinkGetMobId (string path, string source, string customParamsStr);

		public override void GetMobId (MobLinkScene scene)
		{
			string customParamsStr = null;
			customParamsStr = MiniJSON.jsonEncode(scene.customParams);
			__iosMobLinkGetMobId (scene.path, scene.source, customParamsStr);
		}
			
	}
	#endif
}


