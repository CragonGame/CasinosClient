using System;
using System.Collections;
using UnityEngine;

namespace com.moblink.unity3d
{
	[Serializable]
	public class MobLinkConfig
	{
		public string appKey;
		public string appSecret;

		public MobLinkConfig()
		{
			this.appKey = "1b8898cb51ccb";
			this.appSecret = "fa16fc57a2a639c7b4980085a3e7e0b2";
		}
	}		

}
