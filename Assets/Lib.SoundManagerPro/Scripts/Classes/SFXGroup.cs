using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
/// <summary>
/// Used to group SFX together with certain attributes to share.
/// </summary>
public class SFXGroup {
	/// <summary>
	/// The name of the group.
	/// </summary>
	public string groupName;
	/// <summary>
	/// The specific cap amount. If set to -1, it will use the default global cap amount. If set to 0, the group will not use a specific cap amount at all. This amount will only be respected when using SoundManager.PlayCappedSFX
	/// </summary>
	public int specificCapAmount;
	/// <summary>
	/// The clips in the group.
	/// </summary>
	public List<AudioClip> clips = new List<AudioClip>();
	/// <summary>
	/// NOT IMPLEMENTED YET.
	/// </summary>
	public bool independentVolume;
	/// <summary>
	/// NOT IMPLEMENTED YET.
	/// </summary>
	public bool independentPitch;
	/// <summary>
	/// NOT IMPLEMENTED YET.
	/// </summary>
	public float volume;
	/// <summary>
	/// NOT IMPLEMENTED YET.
	/// </summary>
	public float pitch;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="SFXGroup"/> class.
	/// </summary>
	/// <param name='name'>
	/// Name of the group.
	/// </param>
	/// <param name='capAmount'>
	/// The specificCapAmount.
	/// </param>
	/// <param name='audioclips'>
	/// Audioclips in the group.
	/// </param>
	public SFXGroup(string name, int capAmount, params AudioClip[] audioclips)
	{
		groupName = name;
		specificCapAmount = capAmount;
		clips = new List<AudioClip>(audioclips);
		
		independentVolume = false;
		independentPitch = false;
		volume = 1f;
		pitch = 1f;
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="SFXGroup"/> class.
	/// </summary>
	/// <param name='name'>
	/// Name of the group.
	/// </param>
	/// <param name='audioclips'>
	/// Audioclips in the group.
	/// </param>
	public SFXGroup(string name, params AudioClip[] audioclips)
	{
		groupName = name;
		specificCapAmount = 0;
		clips = new List<AudioClip>(audioclips);
		
		independentVolume = false;
		independentPitch = false;
		volume = 1f;
		pitch = 1f;
	}
}
