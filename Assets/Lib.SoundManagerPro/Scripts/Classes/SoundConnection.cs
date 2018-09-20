using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
/// <summary>
/// Contains information on sound connections, meant for background music.
/// </summary>
public class SoundConnection {
	/// <summary>
	/// Name of the scene the SoundConnection is attached to, or the name of the custom SoundConnection.
	/// </summary>
	public string level;
	/// <summary>
	/// Whether it is a custom SoundConnection.
	/// </summary>
	public bool isCustomLevel;
	/// <summary>
	/// The clips in the SoundConnection.
	/// </summary>
	public List<AudioClip> soundsToPlay;
	/// <summary>
	/// The base volumes for each clip in the SoundConnection.
	/// </summary>
	public List<float> baseVolumes = new List<float>();
	/// <summary>
	/// The play method.
	/// </summary>
	public SoundManager.PlayMethod playMethod;
	/// <summary>
	/// The minimum delay for any play methods that have delay in range.
	/// </summary>
	public float minDelay;
	/// <summary>
	/// The maximum delay for any play methods that have delay in range.
	/// </summary>
	public float maxDelay;
	/// <summary>
	/// The delay for any play methods with an exact delay.
	/// </summary>
	public float delay;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="SoundConnection"/> class. Ties the level name to a list of AudioClips. It defaults to continuous play through with no delay between clips.
	/// </summary>
	/// <param name='lvl'>
	/// Level name.
	/// </param>
	/// <param name='audioList'>
	/// Audio list.
	/// </param>
	public SoundConnection(string lvl, params AudioClip[] audioList)
	{
		level = lvl;
		isCustomLevel = false;
		playMethod = SoundManager.PlayMethod.ContinuousPlayThrough;
		minDelay = 0f;
		maxDelay = 0f;
		delay = 0f;
		soundsToPlay = new List<AudioClip>();
		baseVolumes = new List<float>();
		foreach(AudioClip audio in audioList)
		{
			if(!soundsToPlay.Contains(audio))
			{
				soundsToPlay.Add(audio);
				baseVolumes.Add(1f);
			}
		}
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="SoundConnection"/> class.
	/// </summary>
	/// <param name='lvl'>
	/// Level name.
	/// </param>
	/// <param name='method'>
	/// PlayMethod.
	/// </param>
	/// <param name='audioList'>
	/// Audio list.
	/// </param>
	public SoundConnection(string lvl, SoundManager.PlayMethod method, params AudioClip[] audioList)
	{
		level = lvl;
		isCustomLevel = false;
		playMethod = method;
		switch(playMethod)
		{
		case SoundManager.PlayMethod.ContinuousPlayThrough:
		case SoundManager.PlayMethod.OncePlayThrough:
		case SoundManager.PlayMethod.ShufflePlayThrough:
			break;
		default:
			Debug.LogWarning("No delay was set in the constructor so there will be none.");
			break;
		}
		minDelay = 0f;
		maxDelay = 0f;
		delay = 0f;
		soundsToPlay = new List<AudioClip>();
		baseVolumes = new List<float>();
		foreach(AudioClip audio in audioList)
		{
			if(!soundsToPlay.Contains(audio))
			{
				soundsToPlay.Add(audio);
				baseVolumes.Add(1f);
			}
		}
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="SoundConnection"/> class. Sets the exact delay for appropriate play methods.
	/// </summary>
	/// <param name='lvl'>
	/// Level name.
	/// </param>
	/// <param name='method'>
	/// PlayMethod.
	/// </param>
	/// <param name='delayPlay'>
	/// Delay.
	/// </param>
	/// <param name='audioList'>
	/// Audio list.
	/// </param>
	public SoundConnection(string lvl, SoundManager.PlayMethod method, float delayPlay, params AudioClip[] audioList)
	{
		level = lvl;
		isCustomLevel = false;
		playMethod = method;
		minDelay = 0f;
		maxDelay = delayPlay;
		delay = delayPlay;
		soundsToPlay = new List<AudioClip>();
		baseVolumes = new List<float>();
		foreach(AudioClip audio in audioList)
		{
			if(!soundsToPlay.Contains(audio))
			{
				soundsToPlay.Add(audio);
				baseVolumes.Add(1f);
			}
		}
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="SoundConnection"/> class. Sets the min and max delay for appropriate play methods.
	/// </summary>
	/// <param name='lvl'>
	/// Level name.
	/// </param>
	/// <param name='method'>
	/// PlayMethod.
	/// </param>
	/// <param name='minDelayPlay'>
	/// Minimum delay.
	/// </param>
	/// <param name='maxDelayPlay'>
	/// Maximum delay.
	/// </param>
	/// <param name='audioList'>
	/// Audio list.
	/// </param>
	public SoundConnection(string lvl, SoundManager.PlayMethod method, float minDelayPlay, float maxDelayPlay, params AudioClip[] audioList)
	{
		level = lvl;
		isCustomLevel = false;
		playMethod = method;
		minDelay = minDelayPlay;
		maxDelay = maxDelayPlay;
		delay = (maxDelayPlay+minDelayPlay) / 2f;
		soundsToPlay = new List<AudioClip>();
		baseVolumes = new List<float>();
		foreach(AudioClip audio in audioList)
		{
			if(!soundsToPlay.Contains(audio))
			{
				soundsToPlay.Add(audio);
				baseVolumes.Add(1f);
			}
		}
	}
	
	/// <summary>
	/// Sets this SoundConnection to a Custom SoundConnection.  Is not tied to a scene.  Must be called with SoundManager.CustomEvent
	/// Be careful not to use a level name or SoundManager will get confused.  Call this after initializing a SoundConnection.
	/// </summary>
	public void SetToCustom()
	{
		isCustomLevel = true;
	}
}
