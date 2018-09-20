/*				ANTILUNCHBOX				|
* SoundManager to manage music in your game
* in just one place.  No need to litter 
* scenes with code to play music.  Make it
* easy.				
* ----------------------------------------*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using antilunchbox;

[AddComponentMenu("AntiLunchBox/SoundManager")]
/// <summary>
/// SoundManager is where most functions will be called from. This is the main controller of sound activity in your app.
/// </summary>
public partial class SoundManager : antilunchbox.Singleton<SoundManager> {
	#region level load handling
	// Handle SFX on update
	void Update () 
	{
		HandleSFX();
	}

	/// <summary>
	/// Raises the level was loaded event, which handles the level loading behavior of SMP.
	/// </summary>
	/// <param name='level'>
	/// Level.
	/// </param>
	//public void OnLevelWasLoaded(int level)
	//{
	//	if(Instance == this)
	//		if(!ignoreLevelLoad)
	//			HandleLevel(level);
	//}

	// Handle app focus
	private void OnApplicationPause(bool pause)
	{
		CommonHandleApplicationFocus(pause); //In Unity 4,may be !pause. have to check by changing window focus
	}

	// Handle app focus
	/*private void OnApplicationFocus(bool focus)
	{
		CommonHandleApplicationFocus(!focus);
	}*/
	
	/// <summary>
	/// Commonly handles application focus.  Unity stops music when focus is lost, so we ignore.
	/// </summary>
	private void CommonHandleApplicationFocus(bool applicationFocus)
	{
		if(applicationFocus)
			ignoreFromLosingFocus = true;
		else
			ignoreFromLosingFocus = false;
	}
	#endregion
	
	/// <summary>
	/// Adds the SoundConnection to the manager.  It will not add multiple SoundConnections to one level.  
	/// </summary>
	/// <param name='sc'>
	/// The SoundConnection.
	/// </param>
	public static void AddSoundConnection(SoundConnection sc) {
		int _indexOf = SoundConnectionsContainsThisLevel(sc.level);
		if(_indexOf == SOUNDMANAGER_FALSE) {
			if(!Application.CanStreamedLevelBeLoaded(sc.level))
				sc.isCustomLevel = true;
			Instance.soundConnections.Add(sc);
		} else {
			Debug.LogWarning("A SoundConnection for this level already exists.  Remove it first.");
		}
	}
	
	/// <summary>
	/// Removes the SoundConnection for level.  It will not remove anything if that level does not exist.
	/// </summary>
	/// <param name='lvl'>
	/// Level name of the SoundConnection.
	/// </param>
	public static void RemoveSoundConnectionForLevel(string lvl) {
		int _indexOf = SoundConnectionsContainsThisLevel(lvl);
		if(_indexOf == SOUNDMANAGER_FALSE) {
			Debug.LogWarning("A SoundConnection for this level doesn't exist.  The level name may be incorrect or the soundconnection is still being added.");
		} else {
			Instance.soundConnections.RemoveAt(_indexOf);
		}
	}
	
	/// <summary>
	/// Replaces the SoundConnection at that level.  If a SoundConnection doesn't exist, it will just add it.
	/// </summary>
	/// <param name='sc'>
	/// The SoundConnection.
	/// </param>
	public static void ReplaceSoundConnection(SoundConnection sc) {
		int _indexOf = SoundConnectionsContainsThisLevel(sc.level);
		if(_indexOf != SOUNDMANAGER_FALSE) {
			RemoveSoundConnectionForLevel(sc.level);
		}
		AddSoundConnection(sc);
	}
	
	/// <summary>
	/// Checks if a level has a SoundConnection.  If it does, it returns the index.  If it doesn't it returns constant SOUNDMANAGER_FALSE(-1)
	/// </summary>
	/// <returns>
	/// Index of the SoundConnection or SOUNDMANAGER_FALSE(-1)
	/// </returns>
	/// <param name='lvl'>
	/// The level name of the SoundConnection.
	/// </param>
	public static int SoundConnectionsContainsThisLevel(string lvl) {
		return Instance.soundConnections.FindIndex(
				delegate(SoundConnection sc)
				{
					return (sc.level == lvl);
				}
			);
	}
	
	/// <summary>
	/// Gets the sound connection for this level.
	/// </summary>
	/// <returns>
	/// The sound connection for this level.
	/// </returns>
	/// <param name='lvl'>
	/// The level name of the SoundConnection.
	/// </param>
	public static SoundConnection GetSoundConnectionForThisLevel(string lvl) {
		int index = SoundConnectionsContainsThisLevel(lvl);
		if(index == SOUNDMANAGER_FALSE)
			return null;
		return Instance.soundConnections[index];
	}
	
	/// <summary>
	/// Mutes/Unmutes all sounds.  Returns a bool if it's muted or not.
	/// </summary>
	/// <returns>
	/// If the sound is NOW muted or not.
	/// </returns>
	public static bool MuteMusic()
	{
		return MuteMusic(!Instance.mutedMusic);
	}
	
	/// <summary>
	/// Sets Mute to all sounds
	/// </summary>
	/// <returns>
	/// If the sound is NOW muted or not.
	/// </returns>
	/// <param name='toggle'>
	/// The mute to set.
	/// </param>
	public static bool MuteMusic(bool toggle)
	{
		Instance.mutedMusic = toggle;
		return Instance.mutedMusic;
	}
	
	/// <summary>
	/// Determines whether this instance is music muted.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is music muted; otherwise, <c>false</c>.
	/// </returns>
	public static bool IsMusicMuted()
	{
		return Instance.mutedMusic;
	}
	
	/// <summary>
	/// Mutes/Unmutes all sounds.  Returns a bool if it's muted or not, priority is given to the music mute
	/// </summary>
	/// <returns>
	/// If all sounds are NOW muted or not.
	/// </returns>
	public static bool Mute()
	{
		bool muted = MuteMusic();
		MuteSFX(muted);
		return muted;
	}
	
	/// <summary>
	/// Sets Mute to all sounds
	/// </summary>
	/// <returns>
	/// If all sounds are NOW muted or not.
	/// </returns>
	/// <param name='toggle'>
	/// If set to <c>true</c> toggle.
	/// </param>
	public static bool Mute(bool toggle)
	{
		return MuteMusic(MuteSFX(toggle));
	}
	
	/// <summary>
	/// Determines whether this instance is muted.
	/// </summary>
	/// <returns>
	/// If all sounds are muted or not.
	/// </returns>
	public static bool IsMuted()
	{
		return Instance.muted;
	}
	
	/// <summary>
	/// Sets the maximum volume of music in the game relative to the global volume.
	/// </summary>
	/// <param name='setVolume'>
	/// Set volume.
	/// </param>
	public static void SetVolumeMusic(float setVolume)
	{
		setVolume = Mathf.Clamp01(setVolume);
		
		float currentPercentageOfVolume1, currentPercentageOfVolume2;
		currentPercentageOfVolume1 = Instance.volume1 / Instance.maxMusicVolume;
		currentPercentageOfVolume2 = Instance.volume2 / Instance.maxMusicVolume;
		
		Instance.maxMusicVolume = setVolume * Instance.maxVolume;
		
		if(float.IsNaN(currentPercentageOfVolume1) || float.IsInfinity(currentPercentageOfVolume1))
			if(Instance.audios[0].isPlaying)
				currentPercentageOfVolume1 = 1f;
			else
				currentPercentageOfVolume1 = 0f;
		if(float.IsNaN(currentPercentageOfVolume2) || float.IsInfinity(currentPercentageOfVolume2))
			if(Instance.audios[1].isPlaying)
				currentPercentageOfVolume2 = 1f;
			else
				currentPercentageOfVolume2 = 0f;
		
		Instance.volume1 = Instance.maxMusicVolume * currentPercentageOfVolume1;
		Instance.volume2 = Instance.maxMusicVolume * currentPercentageOfVolume2;
	}
	
	/// <summary>
	/// Gets the music volume.
	/// </summary>
	/// <returns>
	/// The music volume.
	/// </returns>
	public static float GetVolumeMusic()
	{
		return Instance.maxMusicVolume;
	}
	
	/// <summary>
	/// Sets the pitch of music in the game.
	/// </summary>
	/// <param name='setPitch'>
	/// Set pitch.
	/// </param>
	public static void SetPitchMusic(float setPitch)
	{
		Instance.audios[0].pitch = Instance.audios[1].pitch = setPitch;
	}
	
	/// <summary>
	/// Gets the music pitch. Prioritizes music.
	/// </summary>
	/// <returns>
	/// The music pitch.
	/// </returns>
	public static float GetPitchMusic()
	{
		return Instance.audios[0].pitch;
	}
	
	/// <summary>
	/// Sets the maximum volume of all sound in the game.
	/// </summary>
	/// <param name='setVolume'>
	/// Set volume.
	/// </param>
	public static void SetVolume(float setVolume)
	{
		setVolume = Mathf.Clamp01(setVolume);
		
		float currentPercentageOfVolumeMusic, currentPercentageOfVolumeSFX;
		currentPercentageOfVolumeMusic = Instance.maxMusicVolume / Instance.maxVolume;
		currentPercentageOfVolumeSFX = Instance.maxSFXVolume / Instance.maxVolume;
		
		Instance.maxVolume = setVolume;
		
		if(float.IsNaN(currentPercentageOfVolumeMusic) || float.IsInfinity(currentPercentageOfVolumeMusic))
			currentPercentageOfVolumeMusic = 1f;
		if(float.IsNaN(currentPercentageOfVolumeSFX) || float.IsInfinity(currentPercentageOfVolumeSFX))
			currentPercentageOfVolumeSFX = 1f;
		
		SetVolumeMusic(currentPercentageOfVolumeMusic);
		SetVolumeSFX(currentPercentageOfVolumeSFX);
	}
	
	/// <summary>
	/// Gets the volume.
	/// </summary>
	/// <returns>
	/// The volume.
	/// </returns>
	public static float GetVolume()
	{
		return Instance.maxVolume;
	}
	
	/// <summary>
	/// Sets the pitch of all sound in the game.
	/// </summary>
	/// <param name='setPitch'>
	/// Set pitch.
	/// </param>
	public static void SetPitch(float setPitch)
	{
		SetPitchMusic(setPitch);
		SetPitchSFX(setPitch);
	}
	
	/// <summary>
	/// Gets the pitch. Prioritizes music.
	/// </summary>
	/// <returns>
	/// The pitch.
	/// </returns>
	public static float GetPitch()
	{
		return GetPitchMusic();
	}
	
	/// <summary>
	/// Gets the duration of the bgm crossfade.
	/// </summary>
	/// <returns>
	/// The cross duration.
	/// </returns>
	public static float GetCrossDuration()
	{
		return Instance.crossDuration;
	}
	
	/// <summary>
	/// Sets the duration of the bgm crossfade.
	/// </summary>
	/// <param name='duration'>
	/// Cross duration.
	/// </param>
	public static void SetCrossDuration(float duration)
	{
		Instance.crossDuration = duration;
	}
	
	/// <summary>
	/// Gets the default resources path. Used in SoundManager.Load
	/// </summary>
	/// <returns>
	/// The default resources path.
	/// </returns>
	public static string GetDefaultResourcesPath()
	{
		return Instance.resourcesPath;
	}
	
	/// <summary>
	/// Sets the default resources path. Used in SoundManager.Load
	/// </summary>
	/// <param name='path'>
	/// Path.
	/// </param>
	public static void SetDefaultResourcesPath(string path)
	{
		Instance.resourcesPath = path;
	}
	
	/// <summary>
	/// Gets the current SoundConnection
	/// </summary>
	/// <returns>
	/// The current sound connection.
	/// </returns>
	public static SoundConnection GetCurrentSoundConnection()
	{
		return Instance.currentSoundConnection;
	}
	
	/// <summary>
	/// Sets the current SoundConnection
	/// </summary>
	/// <param name='connection'>
	/// SoundConnection.
	/// </param>
	public static void SetCurrentSoundConnection(SoundConnection connection)
	{
		Instance.currentSoundConnection = connection;
	}
	
	/// <summary>
	/// Sets whether the background music is disabled
	/// </summary>
	/// <param name='disabled'>
	/// Disabled.
	/// </param>
	public static void SetDisableBGM(bool disabled)
	{
		Instance.offTheBGM = disabled;
	}
	
	/// <summary>
	/// Sets whether the sfx is disabled
	/// </summary>
	/// <param name='disabled'>
	/// Disabled.
	/// </param>
	public static void SetDisableSFX(bool disabled)
	{
		Instance.offTheSFX = disabled;
	}
	
	/// <summary>
	/// Plays the SoundConnection right then, regardless of what you put at the level parameter of the SoundConnection.
	/// </summary>
	/// <param name='sc'>
	/// The SoundConnection.
	/// </param>
	/// <param name='syncPlaybackTime'>
	/// Whether the playback times should be synced.
	/// </param>
	/// <param name='trackNumber'>
	/// Track number to skip to, starting at 0.
	/// </param>
	public static void PlayConnection(SoundConnection sc, bool syncPlaybackTime=false, int trackNumber=0)
	{
		int currentTimeSamples = 0;
		AudioSource currentSource = null;
		if(syncPlaybackTime)
		{
			currentSource = GetCurrentAudioSource();
			if(currentSource != null)
				currentTimeSamples = currentSource.timeSamples;
		}
		
		for(int i = 0; i < trackNumber; i++)
			Next();
		Instance._PlayConnection(sc);
		
		if(syncPlaybackTime)
		{
			currentSource = GetCurrentAudioSource();
			if(currentTimeSamples > currentSource.clip.samples)
			{
				Debug.LogWarning("Your clips are not of equal length. SyncPlayback has restarted the new track");
				currentTimeSamples = 0;
			}
			currentSource.timeSamples = currentTimeSamples;
		}
	}
	
	/// <summary>
	/// Plays a SoundConnection on SoundManager that matches the level name.
	/// </summary>
	/// <param name='levelName'>
	/// The levelName of the SoundConnection.
	/// </param>
	/// <param name='syncPlaybackTime'>
	/// Whether the playback times should be synced.
	/// </param>
	/// <param name='trackNumber'>
	/// Track number to skip to, starting at 0.
	/// </param>
	public static void PlayConnection(string levelName, bool syncPlaybackTime=false, int trackNumber=0)
	{
		int currentTimeSamples = 0;
		AudioSource currentSource = null;
		if(syncPlaybackTime)
		{
			currentSource = GetCurrentAudioSource();
			if(currentSource != null)
				currentTimeSamples = currentSource.timeSamples;
		}
		
		for(int i = 0; i < trackNumber; i++)
			Next();
		Instance._PlayConnection(levelName);
		
		if(syncPlaybackTime)
		{
			currentSource = GetCurrentAudioSource();
			if(currentTimeSamples > currentSource.clip.samples)
			{
				Debug.LogWarning("Your clips are not of equal length. SyncPlayback has restarted the new track");
				currentTimeSamples = 0;
			}
			currentSource.timeSamples = currentTimeSamples;
		}
	}
	
	/// <summary>
	/// Creates a SoundConnection.  You can use a scene name or a custom name.  Will default to ContinousPlayThrough.
	/// </summary>
	/// <returns>
	/// The SoundConnection.
	/// </returns>
	/// <param name='lvl'>
	/// The level name of the SoundConnection.
	/// </param>
	/// <param name='audioList'>
	/// Audio list.
	/// </param>
	public static SoundConnection CreateSoundConnection(string lvl, params AudioClip[] audioList)
	{
		SoundConnection sc = new SoundConnection(lvl, SoundManager.PlayMethod.ContinuousPlayThrough, audioList);
		if(!Application.CanStreamedLevelBeLoaded(lvl))
			sc.SetToCustom();
		return sc;
	}
	
	/// <summary>
	/// Creates a SoundConnection.  You can use a scene name or a custom name.
	/// </summary>
	/// <returns>
	/// The SoundConnection.
	/// </returns>
	/// <param name='lvl'>
	/// The level name of the SoundConnection.
	/// </param>
	/// <param name='method'>
	/// The PlayMethod.
	/// </param>
	/// <param name='audioList'>
	/// Audio list.
	/// </param>
	public static SoundConnection CreateSoundConnection(string lvl, SoundManager.PlayMethod method, params AudioClip[] audioList)
	{
		SoundConnection sc = new SoundConnection(lvl, method, audioList);
		if(!Application.CanStreamedLevelBeLoaded(lvl))
			sc.SetToCustom();
		return sc;
	}
	
	/// <summary>
	/// Creates a SoundConnection.  You can use a scene name or a custom name.  This overload is used for PlayMethods that use Delay.
	/// </summary>
	/// <returns>
	/// The SoundConnection.
	/// </returns>
	/// <param name='lvl'>
	/// The level name of the SoundConnection.
	/// </param>
	/// <param name='method'>
	/// The PlayMethod.
	/// </param>
	/// <param name='delayPlay'>
	/// The exact delay.
	/// </param>
	/// <param name='audioList'>
	/// Audio list.
	/// </param>
	public static SoundConnection CreateSoundConnection(string lvl, SoundManager.PlayMethod method, float delayPlay, params AudioClip[] audioList)
	{
		SoundConnection sc = new SoundConnection(lvl, method, delayPlay, audioList);
		if(!Application.CanStreamedLevelBeLoaded(lvl))
			sc.SetToCustom();
		return sc;
	}
	
	/// <summary>
	/// Creates a SoundConnection.  You can use a scene name or a custom name.  This overload is used for PlayMethods that use Random Delay In Range.
	/// </summary>
	/// <returns>
	/// The SoundConnection.
	/// </returns>
	/// <param name='lvl'>
	/// The level name of the SoundConnection.
	/// </param>
	/// <param name='method'>
	/// The PlayMethod.
	/// </param>
	/// <param name='minDelayPlay'>
	/// The minimum delay.
	/// </param>
	/// <param name='maxDelayPlay'>
	/// The maximum delay.
	/// </param>
	/// <param name='audioList'>
	/// Audio list.
	/// </param>
	public static SoundConnection CreateSoundConnection(string lvl, SoundManager.PlayMethod method, float minDelayPlay, float maxDelayPlay, params AudioClip[] audioList)
	{
		SoundConnection sc = new SoundConnection(lvl, method, minDelayPlay, maxDelayPlay, audioList);
		if(!Application.CanStreamedLevelBeLoaded(lvl))
			sc.SetToCustom();
		return sc;
	}

	/// <summary>
	/// Plays the clip immediately regardless of a playing SoundConnection, with an option to loop.  Calls an event once the clip is done.
	/// You can resume a SoundConnection afterwards if you so choose, using Instance.currentSoundConnection.  However, it will not resume on it's own.
	/// Callbacks will only fire once.
	/// </summary>
	/// <param name='clip2play'>
	/// The clip to play.
	/// </param>
	/// <param name='runOnEndFunction'>
	/// Function to run once the clip is done.  Is added to OnSongEnd
	/// </param>
	/// <param name='loop'>
	/// Whether the clip should loop
	/// </param>
	public static void PlayImmediately(AudioClip clip2play, bool loop=false, SongCallBack runOnEndFunction=null)
	{
		Instance._PlayImmediately(clip2play, loop, runOnEndFunction);
	}
	
	/// <summary>
	/// Plays the clip by crossing out what's currently playing regardless of a playing SoundConnection, with an option to loop.  Calls an event once the clip is done.
	/// You can resume a SoundConnection afterwards if you so choose, using Instance.currentSoundConnection.  However, it will not resume on it's own.
	/// Callbacks will only fire once.
	/// </summary>
	/// <param name='clip2play'>
	/// The clip to play.
	/// </param>
	/// <param name='runOnEndFunction'>
	/// Function to run once the clip is done.  Is added to OnSongEnd
	/// </param>
	/// <param name='loop'>
	/// Whether the clip should loop
	/// </param>
	public static void Play(AudioClip clip2play, bool loop=false, SongCallBack runOnEndFunction=null)
	{
		Instance._Play(clip2play, loop, runOnEndFunction);
	}
	
	/// <summary>
	/// Stops all music immediately.
	/// </summary>
	public static void StopMusicImmediately()
	{
		Instance._StopMusicImmediately();
	}
	
	/// <summary>
	/// Crosses out all AudioSources.
	/// </summary>
	public static void StopMusic()
	{
		Instance._StopMusic();
	}
	
	/// <summary>
	/// Stops all sound, music and sfx, immediately.
	/// </summary>
	public static void Stop()
	{
		StopMusicImmediately();
		StopSFX();
	}	
	

	/// <summary>
	/// Pauses all music and SFX.  Call UnPause to unpause
	/// </summary>
	public static void Pause()
	{
		Instance._Pause();
	}

	/// <summary>
	/// Un-Pauses all music and SFX.
	/// </summary>
	public static void UnPause()
	{
		Instance._UnPause();
	}

	/// <summary>
	/// Toggles sound paused and unpaused.
	/// </summary>
	public static void PauseToggle()
	{
		if(Instance.isPaused)
			UnPause();
		else
			Pause();
	}


	/// <summary>
	/// Determines if the sound is paused.
	/// </summary>
	/// <returns><c>true</c> if is paused; otherwise, <c>false</c>.</returns>
	public static bool IsPaused()
	{
		return Instance.isPaused;
	}
	
	/// <summary>
	/// Sets ignore level load, which will decide whether or not to use SoundManagerPro's level load AI.
	/// </summary>
	/// <param name='ignore'>
	/// Ignore.
	/// </param>
	public static void SetIgnoreLevelLoad(bool ignore)
	{
		Instance.ignoreLevelLoad = ignore;
	}
	
	/// <summary>
	/// Gets the current song list.
	/// </summary>
	/// <returns>
	/// The current song list in the current SoundConnection.
	/// </returns>
	public static List<AudioClip> GetCurrentSongList()
	{
		if(Instance.currentSoundConnection == null)
			return null;
		return Instance.currentSoundConnection.soundsToPlay;
	}
	
	/// <summary>
	/// Gets the current song.
	/// </summary>
	/// <returns>
	/// The current song.
	/// </returns>
	public static AudioClip GetCurrentSong()
	{
		if(!Instance.IsPlaying() || Instance.currentSource.clip == null)
			return null;

		return Instance.currentSource.clip;
	}
	
	/// <summary>
	/// Goes to the Next song in the song list queue. Becareful using this in OncePlayThrough methods.
	/// </summary>
	public static void Next()
	{
		Instance.skipAmount++;
		Instance.skipSongs = true;
	}
	
	/// <summary>
	/// Goes to the Previous song in the song list queue. Becareful using this in OncePlayThrough methods.
	/// </summary>
	public static void Prev()
	{
		Instance.skipAmount--;
		Instance.skipSongs = true;
	}
	
	/// <summary>
	/// Checks if the clip name is valid.
	/// </summary>
	/// <returns>
	/// If the name is valid.
	/// </returns>
	/// <param name='clipName'>
	/// The clip name to check.
	/// </param>
	public static bool ClipNameIsValid(string clipName) 
	{
		if(string.IsNullOrEmpty(clipName)) return false;
		return Instance.allClips.ContainsKey(clipName) && Instance.allClips[clipName] != null;
	}
	
	/// <summary>
	/// Checks if the group name is valid.
	/// </summary>
	/// <returns>
	/// If the group name is valid.
	/// </returns>
	/// <param name='groupName'>
	/// The SFXGroup name to check.
	/// </param>
	public static bool GroupNameIsValid(string groupName)
	{
		if(string.IsNullOrEmpty(groupName)) return false;
		return Instance.groups.ContainsKey(groupName) && Instance.groups[groupName] != null;
	}
	
	/// <summary>
	/// Gets the current audio source playing BGM. Will return null if not playing anything at all.
	/// </summary>
	/// <returns>
	/// The current audio source.
	/// </returns>
	public static AudioSource GetCurrentAudioSource()
	{
		return Instance.IsPlaying(GetCurrentSong());
	}
	
	/// <summary>
	/// Gets the track number in the playlist. Otherwise returns -1 if not in playlist.
	/// </summary>
	/// <returns>
	/// The track number.
	/// </returns>
	/// <param name='clip'>
	/// Clip.
	/// </param>
	public static int GetTrackNumber(AudioClip clip)
	{
		return GetCurrentSongList().IndexOf(clip);
	}
}
/// <summary>
/// Sound ducking setting. Can not duck, duck only SFX, duck only Music, or duck everything.
/// </summary>
public enum SoundDuckingSetting
{
	DoNotDuck,
	OnlyDuckSFX,
	OnlyDuckMusic,
	DuckAll
}