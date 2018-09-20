using UnityEngine;
using System;
using System.Collections;

namespace com.moblink.unity3d
{
	public class MobLinkScene
	{
		public string path;
		public string source;
		public Hashtable customParams;

		public MobLinkScene(string scenePath, string sceneSource, Hashtable sceneCustomParams)
		{
			try
			{
				this.path = scenePath;
				this.source = sceneSource;
				this.customParams = sceneCustomParams;
			} catch(Exception e) 
			{
				Console.WriteLine("{0} Exception caught.", e);
			}
		}

	}
}


