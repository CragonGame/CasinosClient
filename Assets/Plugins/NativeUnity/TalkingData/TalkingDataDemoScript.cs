using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class TalkingDataDemoScript : MonoBehaviour {
	
	int index = 1;
	int level = 1;
	string gameserver = "";
	TDGAAccount account;
	
	const int left = 90;
	const int height = 50;
	const int top = 120;
	int width = Screen.width - left * 2;
	const int step = 60;
	
	void OnGUI() {
		
		int i = 0;
		GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Demo Menu");
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Create User")) {
			account = TDGAAccount.SetAccount("User" + index);
			index++;
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Set Account Type")) {
			if (account != null) {
				account.SetAccountType (AccountType.WEIXIN);
			}
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Account Level +1")) {
			if (account != null) {
				account.SetLevel(level++);
			}
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Chagen Game Server + 'a'")) {
			if (account != null) {
				gameserver += "a";
				account.SetGameServer(gameserver);
			}
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Charge Request 10")) {
			TDGAVirtualCurrency.OnChargeRequest("order01", "iap", 10, "CH", 10, "PT");
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Charge Success 10")) {
			TDGAVirtualCurrency.OnChargeSuccess("order01");
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Reward 100")) {
			TDGAVirtualCurrency.OnReward(100, "reason");
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Mission Begin")) {
			TDGAMission.OnBegin("miss001");
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Mission Completed")) {
			TDGAMission.OnCompleted("miss001");
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Item Purchase 10")) {
			TDGAItem.OnPurchase("itemid001", 10, 10);
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Item Use 1")) {
			TDGAItem.OnUse("itemid001", 1);
		}
		
		if (GUI.Button(new Rect(left, top + step * i++, width, height), "Custome Event")) {
			Dictionary<string, object> dic = new Dictionary<string, object>();
			dic.Add("StartApp"+"StartAppTime", "startAppMac"+"#"+"02/01/2013 09:52:24");
			dic.Add("IntValue", 1);
			TalkingDataGA.OnEvent("action_id", dic);
		}
	}
	
	void Start () {
		Debug.Log("start...!!!!!!!!!!");
#if UNITY_IPHONE
#if UNITY_5 || UNITY_5_6_OR_NEWER
		UnityEngine.iOS.NotificationServices.RegisterForNotifications(
			UnityEngine.iOS.NotificationType.Alert |
			UnityEngine.iOS.NotificationType.Badge |
			UnityEngine.iOS.NotificationType.Sound);
#else
		NotificationServices.RegisterForRemoteNotificationTypes(
			RemoteNotificationType.Alert |
			RemoteNotificationType.Badge |
			RemoteNotificationType.Sound);
#endif
#endif
		TalkingDataGA.OnStart("0A33A9FA393A4EC898A788FC293DDD94", "your_channel_id");
		account = TDGAAccount.SetAccount("User01");
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
#if UNITY_IPHONE
		TalkingDataGA.SetDeviceToken();
		TalkingDataGA.HandlePushMessage();
#endif
	}
	
	void OnDestroy (){
		TalkingDataGA.OnEnd();
		Debug.Log("onDestroy");
	}
	
	void Awake () {
		Debug.Log("Awake");
	}
	
	void OnEnable () {
		Debug.Log("OnEnable");
	}
	
	void OnDisable () {
		Debug.Log("OnDisable");
	}
}
