using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
/// <summary>
/// Some useful extension functions to use in the SoundManager.
/// </summary>
public static class SoundManagerTools {
	static readonly System.Random random = new System.Random();
	/// <summary>
	/// Shuffle the specified list.
	/// </summary>
	/// <param name='theList'>
	/// The list.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public static void Shuffle<T> ( ref List<T> theList )
	{
		int n = theList.Count;
		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			T val = theList[k];
			theList[k] = theList[n];
			theList[n] = val;
		}
	}
	/// <summary>
	/// Shuffles two lists together identically.
	/// </summary>
	/// <param name='theList'>
	/// The list.
	/// </param>
	/// <param name='otherList'>
	/// The second list.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	/// <typeparam name='K'>
	/// The 2nd type parameter.
	/// </typeparam>
	public static void ShuffleTwo<T, K> ( ref List<T> theList, ref List<K> otherList)
	{
		int n = theList.Count;
		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			T val = theList[k];
			theList[k] = theList[n];
			theList[n] = val;
			
			if(otherList.Count != theList.Count)
			{
				Debug.LogError("Can't shuffle both lists because this " + typeof(T).ToString() + " list doesn't have the same amount of items.");
				continue;
			}
			K otherVal = otherList[k];
			otherList[k] = otherList[n];
			otherList[n] = otherVal;
		}
	}
	/// <summary>
	/// Make an <a href="http://docs.unity3d.com/ScriptReference/AudioSource.html">AudioSource</a> play any clip like it's 2D.
	/// </summary>
	/// <param name='theAudioSource'>
	/// The audio source.
	/// </param>
	public static void make2D ( ref AudioSource theAudioSource )
	{
		theAudioSource.spatialBlend = 0f;
	}
	/// <summary>
	/// Make an <a href="http://docs.unity3d.com/ScriptReference/AudioSource.html">AudioSource</a> play any clip like it's 3D.
	/// </summary>
	/// <param name='theAudioSource'>
	/// The audio source.
	/// </param>
	public static void make3D ( ref AudioSource theAudioSource )
	{
		theAudioSource.spatialBlend = 1f;
	}
	/// <summary>
	/// Vary a float with restrictions.
	/// </summary>
	/// <returns>
	/// The varied float.
	/// </returns>
	/// <param name='theFloat'>
	/// The float.
	/// </param>
	/// <param name='variance'>
	/// Variance.
	/// </param>
	/// <param name='minimum'>
	/// Minimum value.
	/// </param>
	/// <param name='maximum'>
	/// Maximum value.
	/// </param>
	public static float VaryWithRestrictions ( this float theFloat, float variance, float minimum=0f, float maximum=1f)
	{
		float max = theFloat * (1f+variance);
		float min = theFloat * (1f-variance);
		
		if(max > maximum)
			max = maximum;
		if(min < minimum)
			min = minimum;
		
		return UnityEngine.Random.Range(min, max);
	}
	/// <summary>
	/// Vary a float.
	/// </summary>
	/// <returns>
	/// The varied float.
	/// </returns>
	/// <param name='theFloat'>
	/// The float.
	/// </param>
	/// <param name='variance'>
	/// Variance.
	/// </param>
	public static float Vary ( this float theFloat, float variance)
	{
		float max = theFloat * (1f+variance);
		float min = theFloat * (1f-variance);
		
		return UnityEngine.Random.Range(min, max);
	}
	
#if !(UNITY_WP8 || UNITY_METRO)
	/// <summary>
	/// Returns all instance fields on an object, including inherited fields
	/// http://stackoverflow.com/a/1155549/154165
	/// </summary>
	public static FieldInfo[] GetAllFieldInfos(this Type type)
	{
		if(type == null)
			return new FieldInfo[0];

		BindingFlags flags = 
			BindingFlags.Public | 
			BindingFlags.NonPublic | 
			BindingFlags.Instance | 
			BindingFlags.DeclaredOnly;

		return type.GetFields(flags)
			.Concat(GetAllFieldInfos(type.BaseType))
			.Where(f => !f.IsDefined(typeof(HideInInspector), true))
			.ToArray();
	}
#endif
}
