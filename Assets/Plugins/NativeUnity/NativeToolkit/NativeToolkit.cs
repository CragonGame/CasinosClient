#pragma warning disable 0219

using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MiniJSON1;

public class NativeToolkit : MonoBehaviour {

	enum ImageType { IMAGE, SCREENSHOT };
	enum SaveStatus { NOTSAVED, SAVED, DENIED, TIMEOUT };

	public static event Action<Texture2D> OnScreenshotTaken;
	public static event Action<string> OnScreenshotSaved;
	public static event Action<string> OnImageSaved;
	public static event Action<Texture2D, string> OnImagePicked;
	public static event Action<bool> OnDialogComplete;
	public static event Action<string> OnRateComplete;
	public static event Action<Texture2D, string> OnCameraShotComplete;
	public static event Action<string, string, string> OnContactPicked;
	
	static NativeToolkit instance = null;
	static GameObject go; 
	
	#if UNITY_IOS
	
	[DllImport("__Internal")]
	private static extern int saveToGallery(string path);

	[DllImport("__Internal")]
	private static extern void pickImage();

	[DllImport("__Internal")]
	private static extern void openCamera();

	[DllImport("__Internal")]
	private static extern void pickContact();

	[DllImport("__Internal")]
	private static extern string getLocale();

	[DllImport("__Internal")]
	private static extern void sendEmail(string to, string cc, string bcc, string subject, string body, string imagePath);

	[DllImport("__Internal")]
	private static extern void scheduleLocalNotification(string id, string title, string message, int delayInMinutes, string sound);

	[DllImport("__Internal")]
	private static extern void clearLocalNotification(string id);

	[DllImport("__Internal")]
	private static extern void clearAllLocalNotifications();

	[DllImport("__Internal")]
	private static extern bool wasLaunchedFromNotification();

	[DllImport("__Internal")]
	private static extern void rateApp(string title, string message, string positiveBtnText, string neutralBtnText, string negativeBtnText, string appleId);

	[DllImport("__Internal")]
	private static extern void showConfirm(string title, string message, string positiveBtnText, string negativeBtnText);

	[DllImport("__Internal")]
	private static extern void showAlert(string title, string message, string confirmBtnText);

    [DllImport("__Internal")]
    private static extern void startLocation();

    [DllImport("__Internal")]
    private static extern double getLongitude();

    [DllImport("__Internal")]
    private static extern double getLatitude();

#elif UNITY_ANDROID

	static AndroidJavaClass obj;

#endif


    //=============================================================================
    // Init singleton
    //=============================================================================

    public static NativeToolkit Instance 
	{
		get {
			if(instance == null)
			{
				go = new GameObject();
				go.name = "NativeToolkit";
				instance = go.AddComponent<NativeToolkit>();

				#if UNITY_ANDROID

				if(Application.platform == RuntimePlatform.Android)
					obj = new AndroidJavaClass("com.secondfury.nativetoolkit.Main");

				#endif
			}
			
			return instance; 
		}
	}

	void Awake() 
	{
		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
		}
	}


	//=============================================================================
	// Grab and save screenshot
	//=============================================================================

	public static void SaveScreenshot(string fileName, string albumName = "MyScreenshots", string fileType = "jpg", Rect screenArea = default(Rect))
	{
		Debug.Log("Save screenshot to gallery " + fileName);

		if(screenArea == default(Rect))
			screenArea = new Rect(0, 0, Screen.width, Screen.height);

		Instance.StartCoroutine(Instance.GrabScreenshot(fileName, albumName, fileType, screenArea));
	}
	
	IEnumerator GrabScreenshot(string fileName, string albumName, string fileType, Rect screenArea)
	{
		yield return new WaitForEndOfFrame();

		Texture2D texture = new Texture2D ((int)screenArea.width, (int)screenArea.height, TextureFormat.RGB24, false);
		texture.ReadPixels (screenArea, 0, 0);
		texture.Apply ();
		
		byte[] bytes;
		string fileExt;
		
		if(fileType == "png")
		{
			bytes = texture.EncodeToPNG();
			fileExt = ".png";
		}
		else
		{
			bytes = texture.EncodeToJPG();
			fileExt = ".jpg";
		}

		if (OnScreenshotTaken != null)
			OnScreenshotTaken (texture);
		else
			Destroy (texture);
		
		string date = System.DateTime.Now.ToString("hh-mm-ss_dd-MM-yy");
		string screenshotFilename = fileName + "_" + date + fileExt;
		string path = Application.persistentDataPath + "/" + screenshotFilename;

		#if UNITY_ANDROID

		if(Application.platform == RuntimePlatform.Android) 
		{
			string androidPath = Path.Combine(albumName, screenshotFilename);
			path = Path.Combine(Application.persistentDataPath, androidPath);
			string pathonly = Path.GetDirectoryName(path);
			Directory.CreateDirectory(pathonly);
		}

		#endif
		
		Instance.StartCoroutine(Instance.Save(bytes, fileName, path, ImageType.SCREENSHOT));
	}


	//=============================================================================
	// Save texture
	//=============================================================================

	public static void SaveImage(Texture2D texture, string fileName, string fileType = "jpg")
	{
		Debug.Log("Save image to gallery " + fileName);

		Instance.Awake();

		byte[] bytes;
		string fileExt;
		
		if(fileType == "png")
		{
			bytes = texture.EncodeToPNG();
			fileExt = ".png";
		}
		else
		{
			bytes = texture.EncodeToJPG();
			fileExt = ".jpg";
		}

		string path = Application.persistentDataPath + "/" + fileName + fileExt;

		Instance.StartCoroutine(Instance.Save(bytes, fileName, path, ImageType.IMAGE));
	}
	
	
	IEnumerator Save(byte[] bytes, string fileName, string path, ImageType imageType)
	{
		int count = 0;
		SaveStatus saved = SaveStatus.NOTSAVED;
		
		#if UNITY_IOS
		
		if(Application.platform == RuntimePlatform.IPhonePlayer) 
		{
			System.IO.File.WriteAllBytes(path, bytes);
			
			while(saved == SaveStatus.NOTSAVED)
			{
				count++;
				if(count > 30) 
					saved = SaveStatus.TIMEOUT;
				else
					saved = (SaveStatus)saveToGallery(path);
				
				yield return Instance.StartCoroutine(Instance.Wait(.5f));
			}
			
			UnityEngine.iOS.Device.SetNoBackupFlag(path);
		}
		
		
		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
		{
			System.IO.File.WriteAllBytes(path, bytes);
			
			while(saved == SaveStatus.NOTSAVED) 
			{
				count++;
				if(count > 30) 
					saved = SaveStatus.TIMEOUT;
				else
					saved = (SaveStatus)obj.CallStatic<int>("addImageToGallery", path);
				
				yield return Instance.StartCoroutine(Instance.Wait(.5f));
			}
		}
		
		#else
			
		Debug.Log("Native Toolkit: Save file only available in iOS/Android modes");
			
		saved = SaveStatus.SAVED;

		yield return null;
		
		#endif
		
		switch(saved)
		{
			case SaveStatus.DENIED:
				path = "DENIED";
				break;
				
			case SaveStatus.TIMEOUT:
				path = "TIMEOUT";
				break;
		}
		
		switch(imageType)
		{
			case ImageType.IMAGE:
				if(OnImageSaved != null) 
					OnImageSaved(path);
				break;
				
			case ImageType.SCREENSHOT:
				if(OnScreenshotSaved != null) 
					OnScreenshotSaved(path);
				break;
		}
	}


	//=============================================================================
	// Image Picker
	//=============================================================================

	public static void PickImage()
	{
		Instance.Awake ();

		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			pickImage();

		#elif UNITY_ANDROID	

		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("pickImageFromGallery");

		#endif
	}
	
	public void OnPickImage(string path)
	{
        Texture2D texture = LoadImageFromFile(path);

        if(OnImagePicked != null)
            OnImagePicked(texture, path);
	}


	//=============================================================================
	// Camera
	//=============================================================================
	
	public static void TakeCameraShot()
	{
		Instance.Awake ();
		
		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			openCamera();
		
		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("takeCameraShot");

		#endif
	}

	public void OnCameraFinished(string path)
	{
        Texture2D texture = LoadImageFromFile(path);

        if(OnCameraShotComplete != null)
            OnCameraShotComplete(texture, path);
	}


	//=============================================================================
	// Contacts
	//=============================================================================
	
	public static void PickContact()
	{
		Instance.Awake ();
		
		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			pickContact();
		
		#elif UNITY_ANDROID

		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("pickContact");
		
		#endif
	}

	public void OnPickContactFinished(string data)
	{
		Dictionary<string, object> details = Json.Deserialize(data) as Dictionary<string, object>;
		string name = "";
		string number = "";
		string email = "";

		if(details.ContainsKey("name")) name = details["name"].ToString();
		if(details.ContainsKey("number")) number = details["number"].ToString();
		if(details.ContainsKey("email")) email = details["email"].ToString();

		if(OnContactPicked != null)
			OnContactPicked(name, number, email);
	}
	

	//=============================================================================
	// Email with optional attachment
	//=============================================================================

	public static void SendEmail(string subject, string body, string pathToImageAttachment = "", string to = "", string cc = "", string bcc = "")
	{
		Instance.Awake ();
		
		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			sendEmail(to, cc, bcc, subject, body, pathToImageAttachment);

		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("sendEmail", new object[] { to, cc, bcc, subject, body, pathToImageAttachment } );
		
		#endif
	}


	//=============================================================================
	// Confirm Dialog / Alert
	//=============================================================================
	
	public static void ShowConfirm(string title, string message, Action<bool> callback = null, string positiveBtnText = "Ok", string negativeBtnText = "Cancel")
	{
		Instance.Awake ();

		OnDialogComplete = callback;

		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			showConfirm (title, message, positiveBtnText, negativeBtnText);
		
		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("showConfirm", new object[] { title, message, positiveBtnText, negativeBtnText } );
		
		#endif
	}

	public static void ShowAlert(string title, string message, Action<bool> callback = null, string btnText = "Ok")
	{
		Instance.Awake ();
		
		OnDialogComplete = callback;
		
		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			showAlert (title, message, btnText);
		
		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("showAlert", new object[] { title, message, btnText } );

		#endif
	}

	public void OnDialogPress(string result)
	{
		if(OnDialogComplete != null)
		{
			if(result == "Yes")
				OnDialogComplete(true);
			else if(result == "No")
				OnDialogComplete(false);
		}
	}


	//=============================================================================
	// Rate this app
	//=============================================================================
	
	public static void RateApp(string title = "Rate This App", string message = "Please take a moment to rate this App", 
	                           string positiveBtnText = "Rate Now", string neutralBtnText = "Later", string negativeBtnText = "No, Thanks",
	                           string appleId = "", Action<string> callback = null)
	{
		Instance.Awake ();
		
		OnRateComplete = callback;

		#if UNITY_IOS
		
		if(Application.platform == RuntimePlatform.IPhonePlayer)
			if(appleId != "")
				rateApp(title, message, positiveBtnText, neutralBtnText, negativeBtnText, appleId);
		
		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("rateThisApp", new object[] { title, message, positiveBtnText, neutralBtnText, negativeBtnText } );
		
		#endif
	}

	public void OnRatePress(string result)
	{
		if(OnRateComplete != null)
		{
			OnRateComplete(result);
		}
	}


	//=============================================================================
	// Location / Locale
	//=============================================================================

	public static bool StartLocation()
	{
        Instance.Awake();

        if(!Input.location.isEnabledByUser)
		{
			Debug.Log ("Location service disabled");
			return false;
		}

        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            startLocation();
        }

        #elif UNITY_ANDROID
        
        if(Application.platform == RuntimePlatform.Android)
        {
            obj.CallStatic("startLocation");
        }
        
        #endif

        return true;
	}

	public static double GetLongitude()
	{
        Instance.Awake();

        if(!Input.location.isEnabledByUser)
        {
            return 0;
        }

        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return getLongitude();
        }

        #elif UNITY_ANDROID

        if(Application.platform == RuntimePlatform.Android)
        {
            return obj.CallStatic<double>("getLongitude");
        }
        
        #endif

        return 0;
	}
	
	public static double GetLatitude()
	{
        Instance.Awake();

        if(!Input.location.isEnabledByUser)
        {
            return 0;
        }

        #if UNITY_IOS

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return getLatitude();
        }

        #elif UNITY_ANDROID

        if(Application.platform == RuntimePlatform.Android)
        {
            return obj.CallStatic<double>("getLatitude");
        }
        
        #endif

        return 0;
    }

	public static string GetCountryCode()
	{
		Instance.Awake ();

		string locale = null;
		
		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			locale = getLocale ();
		
		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
			locale = obj.CallStatic<string>("getLocale");
		
		#endif
		
		return locale;
	}


	//=============================================================================
	// Local notifications
	//=============================================================================

	public static void ScheduleLocalNotification(string title, string message, int id = 0, int delayInMinutes = 0, string sound = "default_sound", 
	                                         bool vibrate = false, string smallIcon = "ic_notification", string largeIcon = "ic_notification_large")
	{
		Instance.Awake();

		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer) 
			scheduleLocalNotification(id.ToString(), title, message, delayInMinutes, sound);
		
		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("scheduleLocalNotification", new object[] { title, message, id, delayInMinutes, sound, vibrate, smallIcon, largeIcon } );
		
		#endif
	}

	public static void ClearLocalNotification(int id)
	{
		Instance.Awake ();
		
		#if UNITY_IOS
		
		if(Application.platform == RuntimePlatform.IPhonePlayer) 
			clearLocalNotification(id.ToString());
		
		#elif UNITY_ANDROID
		
		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("clearLocalNotification", new object[] { id });
		
		#endif
	}

	public static void ClearAllLocalNotifications()
	{
		Instance.Awake ();

		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer) 
			clearAllLocalNotifications();

		#elif UNITY_ANDROID

		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("clearAllLocalNotifications");

		#endif
	}

	public static bool WasLaunchedFromNotification()
	{
		Instance.Awake ();

		#if UNITY_IOS
		
		if(Application.platform == RuntimePlatform.IPhonePlayer) 
			return wasLaunchedFromNotification();

		#elif UNITY_ANDROID
		
		if(Application.platform == RuntimePlatform.Android) 
			return obj.CallStatic<bool>("wasLaunchedFromNotification");
		
		#endif
		
		return false;
	}


	//=============================================================================
	// General functions
	//=============================================================================

	public static Texture2D LoadImageFromFile(string path)
	{
		if(path == "Cancelled") return null;

		byte[] bytes;
		Texture2D texture = new Texture2D(128, 128, TextureFormat.RGB24, false);

		#if UNITY_WINRT

		bytes = UnityEngine.Windows.File.ReadAllBytes(path);
		texture.LoadImage(bytes);

		#else

		bytes = System.IO.File.ReadAllBytes(path);
		texture.LoadImage(bytes);

		#endif

		return texture;
	}
	
	
	IEnumerator Wait(float delay)
	{
		float pauseTarget = Time.realtimeSinceStartup + delay;
		
		while(Time.realtimeSinceStartup < pauseTarget)
		{
			yield return null;	
		}
	}
}