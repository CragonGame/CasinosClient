using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using antilunchbox;

public partial class SoundManager : antilunchbox.Singleton<SoundManager> {

	/// <summary>
	/// Path to folder where SFX are held in resources
	/// </summary>
	public string resourcesPath = "Sounds/SFX";

	/// <summary>
	/// List of local AudioClip SFXs added in inspector or through SaveSFX()
	/// </summary>
	public List<AudioClip> storedSFXs = new List<AudioClip>();

	/// <summary>
	/// List of other unowned gameobjects with SFX attached.
	/// </summary>
	public List<GameObject> unOwnedSFXObjects = new List<GameObject>();

	/// <summary>
	/// Dictionary of instance ID to cappedID to keep track of capped SFX
	/// </summary>
	public Dictionary<int, string> cappedSFXObjects = new Dictionary<int, string>();
	
	/// <summary>
	/// Dictionary of delayed AudioSources
	/// </summary>
	public Dictionary<AudioSource, float> delayedAudioSources = new Dictionary<AudioSource, float>();
	
	/// <summary>
	/// Dictionary of sfx with runonendfunctions
	/// </summary>
	public Dictionary<AudioSource, SongCallBack> runOnEndFunctions = new Dictionary<AudioSource, SongCallBack>();
	
	private AudioSource duckSource;
	private SongCallBack duckFunction;
	private bool isDucking = false;
	private int duckNumber = 0;
	private float preDuckVolume = 1f;
	private float preDuckVolumeMusic = 1f;
	private float preDuckVolumeSFX = 1f;
	private float preDuckPitch = 1f;
	private float preDuckPitchMusic = 1f;
	private float preDuckPitchSFX = 1f;
	
	/// <summary>
	/// The start speed of the ducking effect.
	/// </summary>
	public static float duckStartSpeed = .1f;
	/// <summary>
	/// The end speed of the ducking effect.
	/// </summary>
	public static float duckEndSpeed = .5f;
	
	/// <summary>
	/// List of SFXGroups. At runtime, this is NOT used, so don't modify this.
	/// </summary>
	public List<SFXGroup> sfxGroups = new List<SFXGroup>();
	
	// Map of clip names to group names (dictionaries and hashtables are not supported for serialization)
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public List<string> clipToGroupKeys = new List<string>();
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public List<string> clipToGroupValues = new List<string>();
	
	private Dictionary<string, SFXGroup> groups = new Dictionary<string, SFXGroup>();
	private Dictionary<string, string> clipsInGroups = new Dictionary<string, string>();
	private Dictionary<string, AudioClip> allClips = new Dictionary<string, AudioClip>();
	private Dictionary<string, int> prepools = new Dictionary<string, int>();
	private Dictionary<string, float> baseVolumes = new Dictionary<string, float>();
	private Dictionary<string, float> volumeVariations = new Dictionary<string, float>();
	private Dictionary<string, float> pitchVariations = new Dictionary<string, float>();
	/// <summary>
	/// Turn off the SFX.
	/// </summary>
	public bool offTheSFX = false;
	/// <summary>
	/// The default cap amount.
	/// </summary>
	public int capAmount = 3;
	/// <summary>
	/// Gets or sets the SFX volume.
	/// </summary>
	/// <value>
	/// The SFX volume.
	/// </value>
	public float volumeSFX {
		get{
			return _volumeSFX;
		} set {
			foreach(KeyValuePair<AudioClip, SFXPoolInfo> pair in Instance.ownedPools)
			{
				foreach(GameObject ownedSFXObject in pair.Value.ownedAudioClipPool)
				{
					if(ownedSFXObject != null)
						if(ownedSFXObject.GetComponent<AudioSource>() != null && (!isDucking || ownedSFXObject.GetComponent<AudioSource>() != duckSource))
							ownedSFXObject.GetComponent<AudioSource>().volume = value;
				}
			}
			foreach(GameObject unOwnedSFXObject in Instance.unOwnedSFXObjects)
			{
				if(unOwnedSFXObject != null)
					if(unOwnedSFXObject.GetComponent<AudioSource>() != null && (!isDucking || unOwnedSFXObject.GetComponent<AudioSource>() != duckSource))
						unOwnedSFXObject.GetComponent<AudioSource>().volume = value;
			}
			_volumeSFX = value;
		}
	}
	private float _volumeSFX = 1f;
	/// <summary>
	/// Gets or sets the SFX pitch.
	/// </summary>
	/// <value>
	/// The SFX pitch.
	/// </value>
	public float pitchSFX {
		get{
			return _pitchSFX;
		} set {
			foreach(KeyValuePair<AudioClip, SFXPoolInfo> pair in Instance.ownedPools)
			{
				foreach(GameObject ownedSFXObject in pair.Value.ownedAudioClipPool)
				{
					if(ownedSFXObject != null)
						if(ownedSFXObject.GetComponent<AudioSource>() != null && (!isDucking || ownedSFXObject.GetComponent<AudioSource>() != duckSource))
							ownedSFXObject.GetComponent<AudioSource>().pitch = value;
				}
			}
			foreach(GameObject unOwnedSFXObject in Instance.unOwnedSFXObjects)
			{
				if(unOwnedSFXObject != null)
					if(unOwnedSFXObject.GetComponent<AudioSource>() != null && (!isDucking || unOwnedSFXObject.GetComponent<AudioSource>() != duckSource))
						unOwnedSFXObject.GetComponent<AudioSource>().pitch = value;
			}
			_pitchSFX = value;
		}
	}
	private float _pitchSFX = 1f;
	/// <summary>
	/// Gets or sets the max SFX volume.
	/// </summary>
	/// <value>
	/// The max SFX volume.
	/// </value>
	public float maxSFXVolume {
		get{
			return _maxSFXVolume;
		} set {
			_maxSFXVolume = value;
		}
	}
	private float _maxSFXVolume = 1f;
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="SoundManager"/> muted SFX.
	/// </summary>
	/// <value>
	/// <c>true</c> if muted SFX; otherwise, <c>false</c>.
	/// </value>
	public bool mutedSFX {
		get {
			return _mutedSFX;
		} set {
			foreach(KeyValuePair<AudioClip, SFXPoolInfo> pair in Instance.ownedPools)
			{
				foreach(GameObject ownedSFXObject in pair.Value.ownedAudioClipPool)
				{
					if(ownedSFXObject != null)
						if(ownedSFXObject.GetComponent<AudioSource>() != null)
							if(value)
								ownedSFXObject.GetComponent<AudioSource>().mute = value;
							else
								if(!Instance.offTheSFX)
									ownedSFXObject.GetComponent<AudioSource>().mute = value;
				}
			}
			foreach(GameObject unOwnedSFXObject in Instance.unOwnedSFXObjects)
			{
				if(unOwnedSFXObject != null)
					if(unOwnedSFXObject.GetComponent<AudioSource>() != null)
						if(value)
							unOwnedSFXObject.GetComponent<AudioSource>().mute = value;
						else
							if(!Instance.offTheSFX)
								unOwnedSFXObject.GetComponent<AudioSource>().mute = value;
			}
			_mutedSFX = value;
		}
	}
	private bool _mutedSFX = false;
	
	private Dictionary<AudioClip, SFXPoolInfo> ownedPools = new Dictionary<AudioClip, SFXPoolInfo>();
	/// <summary>
	/// The sfx pre pool amounts. At runtime, this is NOT used, so don't modify this.
	/// </summary>
	public List<int> sfxPrePoolAmounts = new List<int>();
	/// <summary>
	/// The sfx base volumes. At runtime, this is NOT used, so don't modify this.
	/// </summary>
	public List<float> sfxBaseVolumes = new List<float>();
	/// <summary>
	/// The sfx volume variations. At runtime, this is NOT used, so don't modify this.
	/// </summary>
	public List<float> sfxVolumeVariations = new List<float>();
	/// <summary>
	/// The sfx pitch variations. At runtime, this is NOT used, so don't modify this.
	/// </summary>
	public List<float> sfxPitchVariations = new List<float>();
	/// <summary>
	/// The SFX object lifetime for objects outside of the prepool amount.
	/// </summary>
	public float SFXObjectLifetime = 10f;
	/// <summary>
	/// The current SoundPocket s by name.
	/// </summary>
	public List<string> currentPockets = new List<string>() { "Default" };
}
