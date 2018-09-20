using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class SMPAutorun
{
	private static int reviewPromptIntervalFull = 50;
	private static int reviewPromptInitialFull = 100;
	
	public static GameObject versionChecker;
	
	static SMPAutorun()
	{
		EditorApplication.update += Reviews;
	}
	
	static void Reviews()
	{
		if(EditorPrefs.GetBool("SMPAutomaticUpdates", true))
			LookForUpdates();
		else if(versionChecker != null)
			versionChecker.GetComponent<EditorUpdateCheck>().DestroyMe();
		EvaluateTimesOpened();
		CheckPrompts();
		EditorApplication.update -= Reviews;
	}
	
	static void LookForUpdates()
	{
		if(Application.internetReachability != NetworkReachability.NotReachable)
		{
			if(!(versionChecker && versionChecker.GetComponent<EditorUpdateCheck>().querying))
			{
				versionChecker = new GameObject("versionChecker", typeof(EditorUpdateCheck));
				versionChecker.hideFlags = HideFlags.HideAndDontSave;
				
				versionChecker.GetComponent<EditorUpdateCheck>().QueryUpdates(false);
			}
		}
	}
	
	static void EvaluateTimesOpened()
	{
		EditorPrefs.SetInt("SoundManagerOpened", EditorPrefs.GetInt("SoundManagerOpened", 0) + 1);

		if(EditorPrefs.GetInt("SoundManagerOpened", 0) > 5000)
			EditorPrefs.SetInt("SoundManagerOpened", EditorPrefs.GetInt("SoundManagerOpened", 0) -5000 + reviewPromptInitialFull);

		string todaysDate = System.DateTime.Now.ToString("M/d/yyyy");
		if(EditorPrefs.GetString("WasLastPrompted", todaysDate) != todaysDate)
		{
			if(EditorPrefs.GetInt("SoundManagerOpened", 0) >= reviewPromptInitialFull)
			{
				if(!EditorPrefs.GetBool("PassedInitialPrompt", false))
				{
					EditorPrefs.SetBool("PassedInitialPrompt", true);
					EditorPrefs.SetBool("PromptReview", true);
				}
				else if(EditorPrefs.GetBool("ReviewPromptActivated", true) && EditorPrefs.GetInt("SoundManagerOpened", 0) % reviewPromptIntervalFull == 0)
					EditorPrefs.SetBool("PromptReview", true);
			}
		}
	}

	static void CheckPrompts()
	{
		if(Application.isPlaying)
			return;
		bool wasPrompted = false;
		if(EditorPrefs.GetBool("PromptReviewFree", false))
		{
			wasPrompted = true;
			EditorPrefs.SetBool("PromptReviewFree", false);
			int option = EditorUtility.DisplayDialogComplex ("Found SoundManagerPro Free! Useful?", "Please rate us in the Unity Asset Store (hopefully a good rating!).\n\nIt won't take more than a minute.  We'll make it easy and take you directly there!\nThanks for your support!\n\nP.S. - Keep in mind that reviews aren't sent to the developer so keep bug reports to the forums please! We'll be able to respond in a timely manner there.", "Rate Now!", "Remind Me Later", "No Thanks");
			switch(option)
			{
			case 0: //YES
				Application.OpenURL("com.unity3d.kharma:content/9582");
				EditorPrefs.SetBool("ReviewPromptActivatedFree", false);
				break;
			case 1: //Later
				EditorPrefs.SetBool("ReviewPromptActivatedFree", true);
				break;
			case 2: //NO
				EditorPrefs.SetBool("ReviewPromptActivatedFree", false);
				break;
			default: //unrecognized option
				EditorPrefs.SetBool("ReviewPromptActivatedFree", false);
				break;
			}
		}

		if(EditorPrefs.GetBool("PromptReview", false))
		{
			wasPrompted = true;
			EditorPrefs.SetBool("PromptReview", false);
			int option = EditorUtility.DisplayDialogComplex ("Found SoundManagerPro Useful?", "Please rate us in the Unity Asset Store (hopefully a good rating!).\n\nIt won't take more than a minute.  We'll make it easy and take you directly there!\nThanks for your support!\n\nP.S. - Keep in mind that reviews aren't sent to the developer so keep bug reports to the forums please! We'll be able to respond in a timely manner there.", "Rate Now!", "Remind Me Later", "No Thanks");
			switch(option)
			{
				case 0: //YES
				Application.OpenURL("com.unity3d.kharma:content/9209");
				EditorPrefs.SetBool("ReviewPromptActivated", false);
				break;
				case 1: //Later
				EditorPrefs.SetBool("ReviewPromptActivated", true);
				break;
				case 2: //NO
				EditorPrefs.SetBool("ReviewPromptActivated", false);
				break;
				default: //unrecognized option
				EditorPrefs.SetBool("ReviewPromptActivated", false);
				break;
			}
		}

		if(EditorPrefs.GetBool("PromptUpgrade", false))
		{
			wasPrompted = true;
			EditorPrefs.SetBool("PromptUpgrade", false);
			int option = EditorUtility.DisplayDialogComplex ("SoundManagerPro Upgrade", "Please consider upgrading for no watermark, a more recent version, frequent bug fixes, and new exciting features!\n\nWe'll make it easy and take you directly there!\n\nThanks for your support!", "Upgrade Now!", "Remind Me Later", "No Thanks");
			switch(option)
			{
				case 0: //YES
				Application.OpenURL("com.unity3d.kharma:content/9209");
				EditorPrefs.SetBool("UpgradePromptActivated", false);
				break;
				case 1: //Later
				EditorPrefs.SetBool("UpgradePromptActivated", true);
				break;
				case 2: //NO
				EditorPrefs.SetBool("UpgradePromptActivated", false);
				break;
				default: //unrecognized option
				EditorPrefs.SetBool("UpgradePromptActivated", false);
				break;
			}
		}

		if(wasPrompted)
			EditorPrefs.SetString("WasLastPrompted", System.DateTime.Now.ToString("M/d/yyyy"));
	}
}
