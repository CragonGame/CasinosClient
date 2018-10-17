package cn.sharesdk.unity3d;

import java.util.ArrayList;
import java.util.HashMap;

import cn.sharesdk.framework.Platform;
import cn.sharesdk.framework.PlatformActionListener;
import cn.sharesdk.framework.ShareSDK;

import com.mob.tools.utils.Hashon;
import com.unity3d.player.UnityPlayer;

public class Unity3dPlatformActionListener implements PlatformActionListener{
	
	private int reqID;
	private String u3dCallback;
	private String u3dGameObject;
	
	public Unity3dPlatformActionListener(String u3dGameObject, String u3dCallback){
		this.u3dGameObject = u3dGameObject;
		this.u3dCallback = u3dCallback;		
	}
	
	public void setReqID(int reqID) {
		this.reqID = reqID;
	}

	public void onError(Platform platform, int action, Throwable t) {
		t.printStackTrace();
		String resp = javaActionResToCS(platform, action, t);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dCallback, resp);
	}
	
	public void onComplete(Platform platform, int action,
			HashMap<String, Object> res) {
		String resp = javaActionResToCS(platform, action, res);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dCallback, resp);
	}
	
	public void onCancel(Platform platform, int action) {
		String resp = javaActionResToCS(platform, action);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dCallback, resp);
	}
	
	// ==================== java tools =====================

	private String javaActionResToCS(Platform platform, int action, Throwable t) {
		int platformId = ShareSDK.platformNameToId(platform.getName());
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("reqID", reqID);
		map.put("platform", platformId);
		map.put("action", action);
		map.put("status", 2); // Success = 1, Fail = 2, Cancel = 3
		map.put("res", throwableToMap(t));
		Hashon hashon = new Hashon();
		return hashon.fromHashMap(map);
	}

	private String javaActionResToCS(Platform platform, int action,
			HashMap<String, Object> res) {
		int platformId = ShareSDK.platformNameToId(platform.getName());
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("reqID", reqID);
		map.put("platform", platformId);
		map.put("action", action);
		map.put("status", 1); // Success = 1, Fail = 2, Cancel = 3
		map.put("res", res);
		Hashon hashon = new Hashon();
		return hashon.fromHashMap(map);
	}

	private String javaActionResToCS(Platform platform, int action) {
		int platformId = ShareSDK.platformNameToId(platform.getName());
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("reqID", reqID);
		map.put("platform", platformId);
		map.put("action", action);
		map.put("status", 3); // Success = 1, Fail = 2, Cancel = 3
		Hashon hashon = new Hashon();
		return hashon.fromHashMap(map);
	}

	private HashMap<String, Object> throwableToMap(Throwable t) {
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("msg", t.getMessage());
		ArrayList<HashMap<String, Object>> traces = new ArrayList<HashMap<String, Object>>();
		for (StackTraceElement trace : t.getStackTrace()) {
			HashMap<String, Object> element = new HashMap<String, Object>();
			element.put("cls", trace.getClassName());
			element.put("method", trace.getMethodName());
			element.put("file", trace.getFileName());
			element.put("line", trace.getLineNumber());
			traces.add(element);
		}
		map.put("stack", traces);
		Throwable cause = t.getCause();
		if (cause != null) {
			map.put("cause", throwableToMap(cause));
		}
		return map;
	}

}
