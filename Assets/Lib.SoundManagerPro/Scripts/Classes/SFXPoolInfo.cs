using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Contains information on SFX Pools.
/// </summary>
public class SFXPoolInfo {
	/// <summary>
	/// The current index in pool.
	/// </summary>
	public int currentIndexInPool = 0;
	/// <summary>
	/// The prepool amount.
	/// </summary>
	public int prepoolAmount = 0;
	/// <summary>
	/// The base volume.
	/// </summary>
	public float baseVolume = 1f;
	/// <summary>
	/// The volume variation.
	/// </summary>
	public float volumeVariation = 0f;
	/// <summary>
	/// The pitch variation.
	/// </summary>
	public float pitchVariation = 0f;
	/// <summary>
	/// The times of death for SFX objects over the prepoolAmount.
	/// </summary>
	public List<float> timesOfDeath = new List<float>();
	/// <summary>
	/// The owned audio clip pool.
	/// </summary>
	public List<GameObject> ownedAudioClipPool = new List<GameObject>();
	/// <summary>
	/// Initializes a new instance of the <see cref="SFXPoolInfo"/> class.
	/// </summary>
	/// <param name='index'>
	/// Index.
	/// </param>
	/// <param name='minAmount'>
	/// Prepool amount.
	/// </param>
	/// <param name='times'>
	/// Times of death.
	/// </param>
	/// <param name='pool'>
	/// Pool of SFX objects.
	/// </param>
	/// <param name='baseVol'>
	/// Base volume.
	/// </param>
	/// <param name='volVar'>
	/// Volume variation
	/// </param>
	/// <param name='pitchVar'>
	/// Pitch variation.
	/// </param>
	public SFXPoolInfo(int index, int minAmount, List<float> times, List<GameObject> pool, float baseVol=1f, float volVar=0f, float pitchVar=0f)
	{
		currentIndexInPool = index;
		prepoolAmount = minAmount;
		timesOfDeath = times;
		ownedAudioClipPool = pool;
		baseVolume = baseVol;
		volumeVariation = volVar;
		pitchVariation = pitchVar;
	}
}
