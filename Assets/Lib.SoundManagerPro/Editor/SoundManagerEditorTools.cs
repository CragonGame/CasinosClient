using UnityEngine;
using UnityEditor;
using System.Collections;

public static class SoundManagerEditorTools {

	public static void RegisterObjectChange(string changeName, Object objChanged)
	{
		#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
		Undo.RegisterSceneUndo(changeName);
		#else
		Undo.RecordObject(objChanged, changeName);
		#endif
	}
}
