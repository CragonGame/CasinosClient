using UnityEngine;
using System.Collections;
using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using antilunchbox;

[AddComponentMenu( "AntiLunchBox/AudioSourcePro" )]
[Serializable]
[ExecuteInEditMode()]
/// <summary>
/// SoundManagerPro's version of an AudioSource with additional features.
/// </summary>
public class AudioSourcePro : MonoBehaviour {
	#region clip info
	/// <summary>
	/// The underlying <a href="http://docs.unity3d.com/ScriptReference/AudioSource.html">AudioSource</a>
	/// </summary>
	public AudioSource audioSource;
	/// <summary>
	/// Specifies how the <a href="http://docs.unity3d.com/ScriptReference/AudioClip.html">AudioClip</a> should be loaded.
	/// </summary>
	public ClipType clipType = ClipType.AudioClip;
	/// <summary>
	/// The name of the <a href="http://docs.unity3d.com/ScriptReference/AudioClip.html">AudioClip</a> if you are loading by clip name. The clip should live on the SoundManager.
	/// </summary>
	public string clipName = "";
	/// <summary>
	/// The name of the SFXGroup if you are loading by group name. The group should live on the SoundManager.
	/// </summary>
	public string groupName = "";
	#endregion
	
	#region private fields
	private bool isBound = false;	
	private bool isVisible;
	#endregion
	
	#region event activation variables
	/// <summary>
	/// Is there a trigger on start?
	/// </summary>
	public bool OnStartActivated = false;
	/// <summary>
	/// Is there a trigger on visible?
	/// </summary>
	public bool OnVisibleActivated = false;
	/// <summary>
	/// Is there a trigger on invisible?
	/// </summary>
	public bool OnInvisibleActivated = false;
	/// <summary>
	/// Is there a trigger on collision enter?
	/// </summary>
	public bool OnCollisionEnterActivated = false;
	/// <summary>
	/// Is there a trigger on collision exit?
	/// </summary>
	public bool OnCollisionExitActivated = false;
	/// <summary>
	/// Is there a trigger on trigger enter?
	/// </summary>
	public bool OnTriggerEnterActivated = false;
	/// <summary>
	/// Is there a trigger on trigger exit?
	/// </summary>
	public bool OnTriggerExitActivated = false;
	/// <summary>
	/// Is there a trigger on mouse enter?
	/// </summary>
	public bool OnMouseEnterActivated = false;
	/// <summary>
	/// Is there a trigger on mouse click?
	/// </summary>
	public bool OnMouseClickActivated = false;
	/// <summary>
	/// Is there a trigger on enable?
	/// </summary>
	public bool OnEnableActivated = false;
	/// <summary>
	/// Is there a trigger on disable?
	/// </summary>
	public bool OnDisableActivated = false;
	/// <summary>
	/// Is there a trigger on 2D collision enter?
	/// </summary>
	public bool OnCollision2dEnterActivated = false;
	/// <summary>
	/// Is there a trigger on 2D collision exit?
	/// </summary>
	public bool OnCollision2dExitActivated = false;
	/// <summary>
	/// Is there a trigger on 2D trigger enter?
	/// </summary>
	public bool OnTriggerEnter2dActivated = false;
	/// <summary>
	/// Is there a trigger on 2D trigger exit?
	/// </summary>
	public bool OnTriggerExit2dActivated = false;
	/// <summary>
	/// Is there a trigger on particle collision?
	/// </summary>
	public bool OnParticleCollisionActivated = false;
	#endregion
	
	#region activate/deactivate behavior
	void Awake()
	{
		if(audioSource == null)
			audioSource = gameObject.AddComponent<AudioSource>();
		if(!Application.isPlaying) return;
		if(audioIsValid && (clipType == ClipType.ClipFromSoundManager || clipType == ClipType.ClipFromGroup))
			Play();
		this.isVisible = false;
	}
	
	void OnDestroy()
	{
		if(Application.isPlaying) Unbind();
		if(audioSource != null)
		{
			audioSource.clip = null;
		}
	}
	#endregion
	
	#region binding
	/// <summary>
	/// Bind all events. Fires automatically on activate.
	/// </summary>
	public void Bind()
	{
		if(isBound)
			return;
		
		foreach(AudioSubscription sub in audioSubscriptions)
		{
			if(!sub.isStandardEvent)
				sub.Bind(this);
			else
				BindStandardEvent(sub.standardEvent, true);
		}
		
		isBound = true;
	}
	
	/// <summary>
	/// Unbind all events. Fires automatically on deactivate.
	/// </summary>
	public void Unbind()
	{
		if(!isBound)
			return;

		isBound = false;
		
		foreach(AudioSubscription sub in audioSubscriptions)
		{
			if(!sub.isStandardEvent)
				sub.Unbind();
			else
				BindStandardEvent(sub.standardEvent, false);
		}
	}
	
	/// <summary>
	/// Binds or unbinds an AudioSourceStandardEvent.
	/// </summary>
	/// <param name='evt'>
	/// The AudioSourceStandardEvent to bind or unbind.
	/// </param>
	/// <param name='activated'>
	/// Whether to bind or unbind.
	/// </param>
	public void BindStandardEvent(AudioSourceStandardEvent evt, bool activated)
	{
		switch(evt)
		{
		case AudioSourceStandardEvent.OnStart:
			OnStartActivated = activated;
			break;
		case AudioSourceStandardEvent.OnVisible:
			OnVisibleActivated = activated;
			break;
		case AudioSourceStandardEvent.OnInvisible:
			OnInvisibleActivated = activated;
			break;
		case AudioSourceStandardEvent.OnCollisionEnter:
			OnCollisionEnterActivated = activated;
			break;
		case AudioSourceStandardEvent.OnCollisionExit:
			OnCollisionExitActivated = activated;
			break;
		case AudioSourceStandardEvent.OnTriggerEnter:
			OnTriggerEnterActivated = activated;
			break;
		case AudioSourceStandardEvent.OnTriggerExit:
			OnTriggerExitActivated = activated;
			break;
		case AudioSourceStandardEvent.OnMouseEnter:
			OnMouseEnterActivated = activated;
			break;
		case AudioSourceStandardEvent.OnMouseClick:
			OnMouseClickActivated = activated;
			break;
		case AudioSourceStandardEvent.OnEnable:
			OnEnableActivated = activated;
			break;
		case AudioSourceStandardEvent.OnDisable:
			OnDisableActivated = activated;
			break;
		case AudioSourceStandardEvent.OnCollisionEnter2D:
			OnCollision2dEnterActivated = activated;
			break;
		case AudioSourceStandardEvent.OnCollisionExit2D:
			OnCollision2dExitActivated = activated;
			break;
		case AudioSourceStandardEvent.OnTriggerEnter2D:
			OnTriggerEnter2dActivated = activated;
			break;
		case AudioSourceStandardEvent.OnTriggerExit2D:
			OnTriggerExit2dActivated = activated;
			break;
		case AudioSourceStandardEvent.OnParticleCollision:
			OnParticleCollisionActivated = activated;
			break;
		default:
			break;
		}
	}
	#endregion
	
	#region internal play
	
	/// <summary>
	/// Calls the correct play handler by AudioSourceStandardEvent.
	/// </summary>
	void PlaySoundInternal(AudioSourceStandardEvent evt)
	{
		AudioSubscription sub = FindSubscriptionForEvent(evt);
		
		if(sub == null)
			return;
		
		switch(sub.actionType)
		{
		case AudioSourceAction.Play:
			PlayHandler();
			break;
		case AudioSourceAction.PlayLoop:
			PlayLoopHandler();
			break;
		case AudioSourceAction.PlayCapped:
			PlayCappedHandler(sub.cappedName);
			break;
		case AudioSourceAction.Stop:
			StopHandler();
			break;
		case AudioSourceAction.None:
		default:
			return;
		}
	}
	
	/// <summary>
	/// Finds the subscription for event.
	/// </summary>
	AudioSubscription FindSubscriptionForEvent(AudioSourceStandardEvent evt)
	{
		return audioSubscriptions.Find(delegate(AudioSubscription obj) {
			return (obj.isStandardEvent && obj.standardEvent == evt);
		});
	}
	
	/// <summary>
	/// Handler for AudioSourceAction.Play
	/// </summary>
	void PlayHandler()
	{
		Play();
	}
	
	/// <summary>
	/// Handler for AudioSourceAction.PlayCapped
	/// </summary>
	void PlayCappedHandler(string cappedID)
	{
		SoundManager.PlayCappedSFX(audioSource, clip, cappedID, volume, pitch);
	}
	
	/// <summary>
	/// Handler for AudioSourceAction.PlayLoop
	/// </summary>
	void PlayLoopHandler()
	{
		SoundManager.PlaySFX(audioSource, clip, true, 0f, volume, pitch);
	}
	
	/// <summary>
	/// Handler for AudioSourceAction.Stop
	/// </summary>
	void StopHandler()
	{
		Stop();
	}
	#endregion
	
	#region standard events
	void Start()
	{
		if(!Application.isPlaying) 
			return;
		
		if(!isBound && componentsAreValid && audioIsValid)
		{
			Bind();
		}
		
		if (OnStartActivated) 
		{
			PlaySoundInternal(AudioSourceStandardEvent.OnStart);
		}
	}
	
	void OnBecameVisible() 
	{
		if (OnVisibleActivated && !isVisible) 
		{
			isVisible = true;
			PlaySoundInternal(AudioSourceStandardEvent.OnVisible);
		}
	}
	
	void OnBecameInvisible() 
	{
		if (OnInvisibleActivated) 
		{
			isVisible = false;
			PlaySoundInternal(AudioSourceStandardEvent.OnInvisible);
		}
	}
	
	void OnEnable()
	{
		if(!Application.isPlaying) 
			return;
		
		if(!isBound && componentsAreValid && audioIsValid)
		{
			Bind();
		}
		
		if (OnEnableActivated) 
		{
			PlaySoundInternal(AudioSourceStandardEvent.OnEnable);
		}
	}
	
	void OnDisable() 
	{
		if(!Application.isPlaying)
			return;
		else
		{
			if (OnDisableActivated) 
			{
				PlaySoundInternal(AudioSourceStandardEvent.OnDisable);
			}
			Unbind();
		}		
	}
	
	#if !(UNITY_3_5 || UNITY_4_0 || UNITY_4_1 || UNITY_4_2)
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (!OnTriggerEnter2dActivated) 
			return;
		
		AudioSubscription sub = FindSubscriptionForEvent(AudioSourceStandardEvent.OnTriggerEnter2D);
		if(sub == null) 
			return;
	
		if(sub.filterLayers && (sub.layerMask & 1 << other.gameObject.layer) != 0)
			return;
		if(sub.filterTags && !sub.tags.Contains(other.gameObject.tag))
			return;
		if(sub.filterNames && !sub.names.Contains(other.gameObject.name))
			return;
		
		PlaySoundInternal(AudioSourceStandardEvent.OnTriggerEnter2D);
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (!OnTriggerExit2dActivated) 
			return;
		
		AudioSubscription sub = FindSubscriptionForEvent(AudioSourceStandardEvent.OnTriggerExit2D);
		if(sub == null) 
			return;
	
		if(sub.filterLayers && (sub.layerMask & 1 << other.gameObject.layer) != 0)
			return;
		if(sub.filterTags && !sub.tags.Contains(other.gameObject.tag))
			return;
		if(sub.filterNames && !sub.names.Contains(other.gameObject.name))
			return;
		
		PlaySoundInternal(AudioSourceStandardEvent.OnTriggerExit2D);
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (!OnCollision2dEnterActivated) 
			return;
		
		AudioSubscription sub = FindSubscriptionForEvent(AudioSourceStandardEvent.OnCollisionEnter2D);
		if(sub == null) 
			return;
	
		if(sub.filterLayers && (sub.layerMask & 1 << collision.gameObject.layer) != 0)
			return;
		if(sub.filterTags && !sub.tags.Contains(collision.gameObject.tag))
			return;
		if(sub.filterNames && !sub.names.Contains(collision.gameObject.name))
			return;
		
		PlaySoundInternal(AudioSourceStandardEvent.OnCollisionEnter2D);
	}
	
	void OnCollisionExit2D(Collision2D collision) 
	{
		if (!OnCollision2dExitActivated) 
			return;
		
		AudioSubscription sub = FindSubscriptionForEvent(AudioSourceStandardEvent.OnCollisionExit2D);
		if(sub == null) 
			return;
	
		if(sub.filterLayers && (sub.layerMask & 1 << collision.gameObject.layer) != 0)
			return;
		if(sub.filterTags && !sub.tags.Contains(collision.gameObject.tag))
			return;
		if(sub.filterNames && !sub.names.Contains(collision.gameObject.name))
			return;
		
		PlaySoundInternal(AudioSourceStandardEvent.OnCollisionExit2D);
	}
	#endif

	void OnCollisionEnter(Collision collision) 
	{
		if (!OnCollisionEnterActivated) 
			return;
		
		AudioSubscription sub = FindSubscriptionForEvent(AudioSourceStandardEvent.OnCollisionEnter);
		if(sub == null) 
			return;
	
		if(sub.filterLayers && (sub.layerMask & 1 << collision.gameObject.layer) != 0)
			return;
		if(sub.filterTags && !sub.tags.Contains(collision.gameObject.tag))
			return;
		if(sub.filterNames && !sub.names.Contains(collision.gameObject.name))
			return;
		
		PlaySoundInternal(AudioSourceStandardEvent.OnCollisionEnter);
	}
	
	void OnCollisionExit(Collision collision) 
	{
		if (!OnCollisionExitActivated) 
			return;
		
		AudioSubscription sub = FindSubscriptionForEvent(AudioSourceStandardEvent.OnCollisionExit);
		if(sub == null) 
			return;
	
		if(sub.filterLayers && (sub.layerMask & 1 << collision.gameObject.layer) != 0)
			return;
		if(sub.filterTags && !sub.tags.Contains(collision.gameObject.tag))
			return;
		if(sub.filterNames && !sub.names.Contains(collision.gameObject.name))
			return;
		
		PlaySoundInternal(AudioSourceStandardEvent.OnCollisionExit);
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (!OnTriggerEnterActivated) 
			return;
		
		AudioSubscription sub = FindSubscriptionForEvent(AudioSourceStandardEvent.OnTriggerEnter);
		if(sub == null) 
			return;
	
		if(sub.filterLayers && (sub.layerMask & 1 << other.gameObject.layer) != 0)
			return;
		if(sub.filterTags && !sub.tags.Contains(other.gameObject.tag))
			return;
		if(sub.filterNames && !sub.names.Contains(other.gameObject.name))
			return;
		
		PlaySoundInternal(AudioSourceStandardEvent.OnTriggerEnter);
	}

	void OnTriggerExit(Collider other) 
	{
		if (!OnTriggerExitActivated) 
			return;
		
		AudioSubscription sub = FindSubscriptionForEvent(AudioSourceStandardEvent.OnTriggerExit);
		if(sub == null) 
			return;
	
		if(sub.filterLayers && (sub.layerMask & 1 << other.gameObject.layer) != 0)
			return;
		if(sub.filterTags && !sub.tags.Contains(other.gameObject.tag))
			return;
		if(sub.filterNames && !sub.names.Contains(other.gameObject.name))
			return;
		
		PlaySoundInternal(AudioSourceStandardEvent.OnTriggerExit);
	}
	
	void OnParticleCollision(GameObject other) 
	{
		if (!OnParticleCollisionActivated) 
			return;
		
		AudioSubscription sub = FindSubscriptionForEvent(AudioSourceStandardEvent.OnParticleCollision);
		if(sub == null) 
			return;
		
		if(sub.filterLayers && (sub.layerMask & 1 << other.layer) != 0)
			return;
		if(sub.filterTags && !sub.tags.Contains(other.tag))
			return;
		if(sub.filterNames && !sub.names.Contains(other.name))
			return;
		
		PlaySoundInternal(AudioSourceStandardEvent.OnParticleCollision);
	}
	
	void OnMouseEnter() 
	{
		if (OnMouseEnterActivated) 
		{
			PlaySoundInternal(AudioSourceStandardEvent.OnMouseEnter);
		}
	}
	
	void OnMouseDown() 
	{
		if (OnMouseClickActivated) 
		{
			PlaySoundInternal(AudioSourceStandardEvent.OnMouseClick);
		}
	}	
	#endregion
	
	#region valiidation
	/// <summary>
	/// Gets a value indicating whether this <see cref="AudioSourcePro"/> components are valid.
	/// </summary>
	/// <value>
	/// <c>true</c> if components are valid; otherwise, <c>false</c>.
	/// </value>
	public bool componentsAreValid
	{
		get {
			for(int i = 0; i < audioSubscriptions.Count; i++)
				if(!audioSubscriptions[i].isStandardEvent && !audioSubscriptions[i].componentIsValid)
					return false;
			return true;
		}
	}
	/// <summary>
	/// Gets a value indicating whether this <see cref="AudioSourcePro"/> audio setup is valid.
	/// </summary>
	/// <value>
	/// <c>true</c> if audio setup is valid; otherwise, <c>false</c>.
	/// </value>
	public bool audioIsValid
	{
		get {	
			bool valid = false;
			switch(clipType)
			{
			case ClipType.AudioClip:
				valid = (audioSource.clip != null);
				break;
			case ClipType.ClipFromSoundManager:
				valid = SoundManager.ClipNameIsValid(clipName);
				break;
			case ClipType.ClipFromGroup:
				valid = SoundManager.GroupNameIsValid(groupName);
				break;
			default:
				break;
			}
			if(!valid)
				Debug.LogError("AudioClip setup is not valid.");
			return valid;
		}
	}
	#endregion	
	
	#region audiosource variable access
	/// <summary>
	/// Bypass effects (Applied from filter components or global listener filters).
	/// </summary>
	/// <value>
	/// <c>true</c> if bypass effects; otherwise, <c>false</c>.
	/// </value>
	public bool bypassEffects {
		get {
			return audioSource.bypassEffects;
		} set {
			audioSource.bypassEffects = value;
		}
	}
	
	/// <summary>
	/// The default <a href="http://docs.unity3d.com/ScriptReference/AudioClip.html">AudioClip</a> to play.
	/// </summary>
	/// <value>
	/// The clip.
	/// </value>
	public AudioClip clip {
		get {
			switch(clipType)
			{
			case ClipType.ClipFromSoundManager:
				return SoundManager.Load(clipName);
			case ClipType.ClipFromGroup:
				return SoundManager.LoadFromGroup(groupName);
			case ClipType.AudioClip:
			default:
				return audioSource.clip;
			}
		} set {
			switch(clipType)
			{
			case ClipType.ClipFromSoundManager:
			case ClipType.ClipFromGroup:
				Debug.LogWarning("Assigning a clip only works for the AudioClip ClipType. It is automatically changed for you.");
				clipType = ClipType.AudioClip;
				audioSource.clip = value;
				break;
			case ClipType.AudioClip:
			default:
				audioSource.clip = value;
				break;
			}
		}
	}
	/// <summary>
	/// Gets or sets the Doppler scale for this <a href="http://docs.unity3d.com/ScriptReference/AudioSource.html">AudioSource</a>.
	/// </summary>
	/// <value>
	/// The doppler level.
	/// </value>
	public float dopplerLevel {
		get {
			return audioSource.dopplerLevel;
		} set {
			audioSource.dopplerLevel = value;
		}
	}
	
#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_0_1
	/// <summary>
	/// Allows <a href="http://docs.unity3d.com/ScriptReference/AudioSource.html">AudioSource</a> to play even though <a href="http://docs.unity3d.com/ScriptReference/AudioListener-pause.html">AudioListener.pause</a> is set to true. This is useful for the menu element sounds or background music in pause menus.
	/// </summary>
	/// <value>
	/// <c>true</c> ignoring listener pause; otherwise, <c>false</c>. This property can only be set via the script and is not serialized.
	/// </value>
	public bool ignoreListenerPause {
		get {
			return audioSource.ignoreListenerPause;
		} set {
			audioSource.ignoreListenerPause = value;
		}
	}
#endif
	/// <summary>
	/// This makes the <a href="http://docs.unity3d.com/ScriptReference/AudioSource.html">AudioSource</a> not take into account the volume of the <a href="http://docs.unity3d.com/ScriptReference/AudioListener.html">AudioListenter</a>.
	/// </summary>
	/// <value>
	/// <c>true</c> if ignore listener volume; otherwise, <c>false</c>.
	/// </value>
	public bool ignoreListenerVolume {
		get {
			return audioSource.ignoreListenerVolume;
		} set {
			audioSource.ignoreListenerVolume = value;
		}
	}
	/// <summary>
	/// Gets a value indicating whether this <see cref="AudioSourcePro"/> is playing.
	/// </summary>
	/// <value>
	/// <c>true</c> if is playing; otherwise, <c>false</c>.
	/// </value>
	public bool isPlaying {
		get {
			return audioSource.isPlaying;
		}
	}
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="AudioSourcePro"/> is set to loop.
	/// </summary>
	/// <value>
	/// <c>true</c> if looping; otherwise, <c>false</c>.
	/// </value>
	public bool loop {
		get {
			return audioSource.loop;
		} set {
			audioSource.loop = value;
		}
	}
	/// <summary>
	/// (Logarithmic rolloff) MaxDistance is the distance a sound stops attenuating at.
	/// </summary>
	/// <value>
	/// The max distance.
	/// </value>
	public float maxDistance {
		get {
			return audioSource.maxDistance;
		} set {
			audioSource.maxDistance = value;
		}
	}
	/// <summary>
	/// Within the Min distance the AudioSourcePro will cease to grow louder in volume.
	/// </summary>
	/// <value>
	/// The minimum distance.
	/// </value>
	public float minDistance {
		get {
			return audioSource.minDistance;
		} set {
			audioSource.minDistance = value;
		}
	}
	/// <summary>
	/// Un- / Mutes the AudioSource. Mute sets the volume=0, Un-Mute restore the original volume.
	/// </summary>
	/// <value>
	/// <c>true</c> if mute; otherwise, <c>false</c>.
	/// </value>
	public bool mute {
		get {
			return audioSource.mute;
		} set {
			audioSource.mute = value;
		}
	}
	/// <summary>
	/// Sets a channels pan position linearly. Only works for 2D clips.
	/// </summary>
	/// <value>
	/// The pan.
	/// </value>
	public float pan {
		get {
			return audioSource.panStereo;
		} set {
			audioSource.panStereo = value;
		}
	}
	/// <summary>
	/// Sets how much the 3d engine has an effect on the channel.
	/// </summary>
	/// <value>
	/// The pan level.
	/// </value>
	public float panLevel {
		get {
			return audioSource.spatialBlend;
		} set {
			audioSource.spatialBlend = value;
		}
	}
	/// <summary>
	/// The pitch of the AudioSourcePro.
	/// </summary>
	/// <value>
	/// The pitch.
	/// </value>
	public float pitch {
		get {
			return audioSource.pitch;
		} set {
			audioSource.pitch = value;
		}
	}
	/// <summary>
	/// If set to true, the AudioSourcePro will automatically start playing on awake.
	/// </summary>
	/// <value>
	/// <c>true</c> if play on awake; otherwise, <c>false</c>.
	/// </value>
	public bool playOnAwake {
		get {
			return audioSource.playOnAwake;
		} set {
			audioSource.playOnAwake = value;
		}
	}
	/// <summary>
	/// Gets or sets the priority.
	/// </summary>
	/// <value>
	/// The priority.
	/// </value>
	public int priority {
		get {
			return audioSource.priority;
		} set {
			audioSource.priority = value;
		}
	}
	/// <summary>
	/// Gets or sets the rolloff mode, how the AudioSourcePro attenuates over distance.
	/// </summary>
	/// <value>
	/// The rolloff mode.
	/// </value>
	public AudioRolloffMode rolloffMode {
		get {
			return audioSource.rolloffMode;
		} set {
			audioSource.rolloffMode = value;
		}
	}
	/// <summary>
	/// Gets or sets the spread angle a 3D stereo or multichannel sound in speaker space.
	/// </summary>
	/// <value>
	/// The spread.
	/// </value>
	public float spread {
		get {
			return audioSource.spread;
		} set {
			audioSource.spread = value;
		}
	}
	/// <summary>
	/// Gets or sets the time.
	/// </summary>
	/// <value>
	/// Playback position in seconds.
	/// </value>
	public float time {
		get {
			return audioSource.time;
		} set {
			audioSource.time = value;
		}
	}
	/// <summary>
	/// Gets or sets the time samples.
	/// </summary>
	/// <value>
	/// Playback position in PCM samples.
	/// </value>
	public int timeSamples {
		get {
			return audioSource.timeSamples;
		} set {
			audioSource.timeSamples = value;
		}
	}
	/// <summary>
	/// Gets or sets the velocity update mode.
	/// </summary>
	/// <value>
	/// Whether the Audio Source should be updated in the fixed or dynamic update.
	/// </value>
	public AudioVelocityUpdateMode velocityUpdateMode {
		get {
			return audioSource.velocityUpdateMode;
		} set {
			audioSource.velocityUpdateMode = value;
		}
	}
	/// <summary>
	/// Gets or sets the volume.
	/// </summary>
	/// <value>
	/// The volume of the audio source (0.0 to 1.0).
	/// </value>
	public float volume {
		get {
			return audioSource.volume;
		} set {
			audioSource.volume = value;
		}
	}
	#endregion
	
	#region audiosource function access
	/// <summary>
	/// Returns a block of the currently playing source's output data.
	/// </summary>
	/// <param name='samples'>
	/// Samples.
	/// </param>
	/// <param name='channel'>
	/// Channel.
	/// </param>
	public void GetOutputData(float[] samples, int channel)
	{
		audioSource.GetOutputData(samples, channel);
	}
	/// <summary>
	/// Returns a block of the currently playing source's spectrum data.
	/// </summary>
	/// <param name='samples'>
	/// Samples.
	/// </param>
	/// <param name='channel'>
	/// Channel.
	/// </param>
	/// <param name='window'>
	/// Window.
	/// </param>
	public void GetSpectrumData(float[] samples, int channel, FFTWindow window)
	{
		audioSource.GetSpectrumData(samples, channel, window);
	}
	/// <summary>
	/// Pauses playing the clip.
	/// </summary>
	public void Pause()
	{
		audioSource.Pause();
	}
	/// <summary>
	/// Plays the clip with an optional certain delay.
	/// </summary>
	/// <param name='delay'>
	/// Delay.
	/// </param>
	public void Play(ulong delay=0)
	{
		switch(clipType)
		{
		case ClipType.ClipFromSoundManager:
		case ClipType.ClipFromGroup:
			SoundManager.PlaySFX(audioSource, clip, loop, Convert.ToSingle(delay), volume, pitch);
			break;
		case ClipType.AudioClip:
		default:
			audioSource.Play(delay);
			break;
		}
	}
	
#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_0_1
	/// <summary>
	/// Plays the clip with a delay specified in seconds. Users are advised to use this function instead of the old Play(delay) function that took a delay specified in samples relative to a reference rate of 44.1 kHz as an argument.
	/// </summary>
	/// <param name='delay'>
	/// Delay.
	/// </param>
	public void PlayDelayed(float delay)
	{
		switch(clipType)
		{
		case ClipType.ClipFromSoundManager:
			StartCoroutine(PlayDelayedHelper(delay, clipName));
			break;
		case ClipType.ClipFromGroup:
			StartCoroutine(PlayDelayedHelper(delay, groupName));
			break;
		case ClipType.AudioClip:
		default:
			audioSource.PlayDelayed(delay);
			break;
		}
	}
	
	IEnumerator PlayDelayedHelper(float delay, string nameValue)
	{
		yield return new WaitForSeconds(delay);
		switch(clipType)
		{
		case ClipType.ClipFromSoundManager:
			SoundManager.PlaySFX(audioSource, SoundManager.Load(nameValue), loop, 0f, volume, pitch);
			break;
		case ClipType.ClipFromGroup:
			SoundManager.PlaySFX(audioSource, SoundManager.LoadFromGroup(nameValue), loop, 0f, volume, pitch);
			break;
		default:
			break;
		}		
	}
#endif
	/// <summary>
	/// Plays an <a href="http://docs.unity3d.com/ScriptReference/AudioClip.html">AudioClip</a>, and scales the AudioSourcePro volume by volumeScale.
	/// </summary>
	/// <param name='clip'>
	/// Clip.
	/// </param>
	/// <param name='volumeScale'>
	/// Volume scale.
	/// </param>
	public void PlayOneShot(AudioClip clip, float volumeScale)
	{
		audioSource.PlayOneShot(clip, volumeScale);
	}

#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_0_1
	/// <summary>
	/// Plays the clip at a specific time on the absolute time-line that <a href="http://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html">AudioSettings.dspTime</a> reads from.
	/// </summary>
	/// <param name='time'>
	/// Time to play.
	/// </param>
	public void PlayScheduled(double time)
	{
		switch(clipType)
		{
		case ClipType.ClipFromSoundManager:
		case ClipType.ClipFromGroup:
			Debug.LogError("PlayScheduled does not work for this ClipType yet. Use ClipType.AudioClip");
			break;
		case ClipType.AudioClip:
		default:
			audioSource.PlayScheduled(time);
			break;
		}
	}
	/// <summary>
	/// Changes the time at which a sound that has already been scheduled to play will end. Notice that depending on the timing not all rescheduling requests can be fulfilled.
	/// </summary>
	/// <param name='time'>
	/// Time to play.
	/// </param>
	public void SetScheduledEndTime(double time)
	{
		switch(clipType)
		{
		case ClipType.ClipFromSoundManager:
		case ClipType.ClipFromGroup:
			Debug.LogError("SetScheduledEndTime does not work for this ClipType yet. Use ClipType.AudioClip");
			break;
		case ClipType.AudioClip:
		default:
			audioSource.SetScheduledEndTime(time);
			break;
		}
	}
	/// <summary>
	/// Changes the time at which a sound that has already been scheduled to play will start.
	/// </summary>
	/// <param name='time'>
	/// Time to play.
	/// </param>
	public void SetScheduledStartTime(double time)
	{
		switch(clipType)
		{
		case ClipType.ClipFromSoundManager:
		case ClipType.ClipFromGroup:
			Debug.LogError("SetScheduledStartTime does not work for this ClipType yet. Use ClipType.AudioClip");
			break;
		case ClipType.AudioClip:
		default:
			audioSource.SetScheduledStartTime(time);
			break;
		}
	}
#endif
	/// <summary>
	/// Stops playing the clip.
	/// </summary>
	public void Stop()
	{
		switch(clipType)
		{
		case ClipType.ClipFromSoundManager:
		case ClipType.ClipFromGroup:
			SoundManager.StopSFXObject(audioSource);
			break;
		case ClipType.AudioClip:
		default:
			audioSource.Stop();
			break;
		}
	}
	/// <summary>
	/// Plays an <a href="http://docs.unity3d.com/ScriptReference/AudioClip.html">AudioClip</a> at a given position in world space.
	/// </summary>
	/// <param name='clip'>
	/// Clip.
	/// </param>
	/// <param name='position'>
	/// Position.
	/// </param>
	/// <param name='volume'>
	/// Volume.
	/// </param>
	public static void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume=1f)
	{
		SoundManager.PlaySFX(clip, false, 0f, volume, SoundManager.GetPitchSFX(), position);
	}
	#endregion
	
	#region editor related - DO NOT MODIFY
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool ShowEditor3D = false;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool ShowEditor2D = false;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool ShowEventTriggers = false;
	/// <summary>
	/// The number of AudioSubscription's are on this instance. The editor modifies this value. It is not recommended to modify this unless you know what you're doing.
	/// </summary>
	public int numSubscriptions = 0;
	/// <summary>
	/// The AudioSubscriptions on this instance. The editor modifies this value. It is not recommended to modify this unless you know what you're doing.
	/// </summary>
	public List<AudioSubscription> audioSubscriptions = new List<AudioSubscription>();
	#endregion
}

namespace antilunchbox
{
	/// <summary>
	/// Specifies how to load <a href="http://docs.unity3d.com/ScriptReference/AudioClip.html">AudioClip</a>s.
	/// </summary>
	public enum ClipType
	{
		AudioClip,
		ClipFromSoundManager,
		ClipFromGroup
	}
	/// <summary>
	/// Specifies what an AudioSubscription should do when an event is fired.
	/// </summary>
	public enum AudioSourceAction
	{
		None,
		Play,
		PlayLoop,
		PlayCapped,
		Stop
	}
	/// <summary>
	/// Standard events to bind to that are automatically provided by the Unity Engine.
	/// </summary>
	public enum AudioSourceStandardEvent
	{
		OnStart,
		OnVisible,
		OnInvisible,
		OnCollisionEnter,
		OnCollisionExit,
		OnTriggerEnter,
		OnTriggerExit,
		OnMouseEnter,
		OnMouseClick,
		OnEnable,
		OnDisable,
		OnCollisionEnter2D,
		OnCollisionExit2D,
		OnTriggerEnter2D,
		OnTriggerExit2D,
		OnParticleCollision
	}
}