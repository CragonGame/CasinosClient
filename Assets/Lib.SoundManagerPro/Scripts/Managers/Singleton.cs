using UnityEngine;
using System.Collections;

namespace antilunchbox 
{
	/// <summary>
	/// Singleton base class that will cause any inheriting class to create itself when referenced in any way at all.
	/// </summary>
	public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
		/*
		 * 		This is required in every script that inherits from Singleton for it to work properly:
		 * 
		 * */
		/*
		// Singleton required initialization
		public static YOURTYPE Instance {
			get {
				return ((YOURTYPE)mInstance);
			} set {
				mInstance = value;
			}
		}
		*/
		/// <summary>
		/// Gets or sets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		protected static Singleton<T> mInstance {
			get {
				if(!_mInstance)
				{
					T [] managers = GameObject.FindObjectsOfType(typeof(T)) as T[];
					if(managers.Length != 0)
					{
						if(managers.Length == 1)
						{
							_mInstance = managers[0];
							_mInstance.gameObject.name = typeof(T).Name;
							return _mInstance;
						} else {
							Debug.LogError("You have more than one " + typeof(T).Name + " in the scene. You only need 1, it's a singleton!");
							foreach(T manager in managers)
							{
								Destroy(manager.gameObject);
							}
						}
					}
					GameObject gO = new GameObject(typeof(T).Name, typeof(T));
					_mInstance = gO.GetComponent<T>();
					DontDestroyOnLoad(gO);
				}
				return _mInstance;
			} set {
				_mInstance = value as T;
			}
		}
		private static T _mInstance;
	}
}
