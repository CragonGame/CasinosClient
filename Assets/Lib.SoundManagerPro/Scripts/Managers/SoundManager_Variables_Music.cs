using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using antilunchbox;

public partial class SoundManager : antilunchbox.Singleton<SoundManager> {

	/// <summary>
	/// The sound connections.
	/// </summary>
	public List<SoundConnection> soundConnections = new List<SoundConnection>();

	[SerializeField]
	/// <summary>
	/// The 2 tracks for background music.
	/// </summary>
	public AudioSource[] audios;
	private bool[] audiosPaused;
	/// <summary>
	/// The current level.
	/// </summary>
	public string currentLevel;
	/// <summary>
	/// The current SoundConnection.
	/// </summary>
	public SoundConnection currentSoundConnection;
	private AudioSource currentSource;
	/// <summary>
	/// The crossfade duration.
	/// </summary>
	public float crossDuration = 5f;
	private float modifiedCrossDuration;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool showDebug = true;
	/// <summary>
	/// Turn off the background music.
	/// </summary>
	public bool offTheBGM = false;
	private bool ignoreCrossDuration = false;

	private int currentPlaying = 0;

	private bool silentLevel = false;
	/// <summary>
	/// Whether sound is paused.
	/// </summary>
	public bool isPaused = false;
	private bool skipSongs = false;
	private int skipAmount = 0;

	private bool[] inCrossing = new bool[] { false, false };
	private bool[] outCrossing = new bool[] { false, false };
	/// <summary>
	/// Whether the background music is being forced to move on to the next song. It is recommended to not modify this value.
	/// </summary>
	public bool movingOnFromSong = false;

	float lastLevelLoad = 0f;

	public const int SOUNDMANAGER_FALSE = -1;
	/// <summary>
	/// Song callback delegate.
	/// </summary>
	public delegate void SongCallBack();
	/// <summary>
	/// Called when a song ends, AFTER crossfade out ends as well.
	/// </summary>
	public SongCallBack OnSongEnd;
	/// <summary>
	/// Called when a song begins, AFTER crossfade in ends.
	/// </summary>
	public SongCallBack OnSongBegin;
	/// <summary>
	/// Called when crossfade out begins.
	/// </summary>
	public SongCallBack OnCrossOutBegin;
	/// <summary>
	/// Called when crossfade in begins.
	/// </summary>
	public SongCallBack OnCrossInBegin;	
	private SongCallBack InternalCallback;
	
	private int currentSongIndex = -1;

	private bool ignoreFromLosingFocus = false;
	/// <summary>
	/// Set this if you wish to ignore the level loading functionality.
	/// </summary>
	public bool ignoreLevelLoad = false;
	/// <summary>
	/// Gets or sets the volume of BGM track 1.
	/// </summary>
	/// <value>
	/// The volume.
	/// </value>
	public float volume1 {
		get {
			return audios[0].volume;
		} set {
			audios[0].volume = value;
		}
	}
	/// <summary>
	/// Gets or sets the volume of BGM track 2.
	/// </summary>
	/// <value>
	/// The volume.
	/// </value>
	public float volume2 {
		get{
			return audios[1].volume;
		} set {
			audios[1].volume = value;
		}
	}
	/// <summary>
	/// Gets or sets the max music volume.
	/// </summary>
	/// <value>
	/// The max music volume.
	/// </value>
	public float maxMusicVolume {
		get{
			return _maxMusicVolume;
		} set {
			_maxMusicVolume = value;
		}
	}
	private float _maxMusicVolume = 1f;
	/// <summary>
	/// Gets or sets the max volume.
	/// </summary>
	/// <value>
	/// The max volume.
	/// </value>
	public float maxVolume {
		get{
			return _maxVolume;
		} set {
			_maxVolume = value;
		}
	}
	private float _maxVolume = 1f;
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="SoundManager"/> muted music.
	/// </summary>
	/// <value>
	/// <c>true</c> if muted music; otherwise, <c>false</c>.
	/// </value>
	public bool mutedMusic {
		get {
			return _mutedMusic;
		} set {
			audios[0].mute = audios[1].mute = value;
			_mutedMusic = value;
		}
	}
	private bool _mutedMusic = false;
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="SoundManager"/> is muted.
	/// </summary>
	/// <value>
	/// <c>true</c> if muted; otherwise, <c>false</c>.
	/// </value>
	public bool muted {
		get {
			return (mutedMusic || mutedSFX);
		} set {
			mutedMusic = mutedSFX = value;
		}
	}

	private bool crossingIn {
		get {
			return (inCrossing[0] || inCrossing[1]);
		}
	}

	private bool crossingOut {
		get {
			return (outCrossing[0] || outCrossing[1]);
		}
	}
}
