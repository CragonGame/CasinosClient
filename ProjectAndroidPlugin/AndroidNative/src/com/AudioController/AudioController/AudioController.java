package com.AudioController.AudioController;

import android.app.Activity;
import android.content.Context;
import android.media.AudioManager;
import android.util.Log;

import com.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge;

public class AudioController {
	// -------------------------------------------------------------------------
	private static AudioController mAudioControl;
	private static AndroidToUnityMsgBridge mAndroidToUnityMsgBridge;
	private Activity mUnityActivity;
	private AudioManager mAudioManager;
	private String mAudioChangeMsgName;
	private int mMaxVolume;
	private int mMinVolume;
	private float mCurrentVolumeProgress;
	private int mCurrentVolume;
	private Boolean mIsMaxVolume;
	private Boolean mIsSilence;

	// -------------------------------------------------------------------------
	public static AudioController Instantce(String result_receiver,
			String audio_changemsgname) {
		if (mAudioControl == null) {
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge
					.Instance();
			mAudioControl = new AudioController(audio_changemsgname);
		}

		return mAudioControl;
	}

	// -------------------------------------------------------------------------
	private AudioController(String audio_changemsgname) {
		this.mAudioChangeMsgName = audio_changemsgname;
		this.mUnityActivity = mAndroidToUnityMsgBridge.getActivity();
		this.mAudioManager = (AudioManager) this.mUnityActivity
				.getSystemService(Context.AUDIO_SERVICE);
		this.mIsMaxVolume = false;
		this.mIsSilence = false;
		if (this.mAudioManager != null) {
			this.mMaxVolume = this.mAudioManager
					.getStreamMaxVolume(AudioManager.STREAM_MUSIC);
			// Log.e("AudioController", "mMaxVolume" + this.mMaxVolume);
			this.mMinVolume = this.mAudioManager
					.getStreamVolume(AudioManager.STREAM_MUSIC);
			// Log.e("AudioController", "mMinVolume" + this.mMinVolume);
		}
	}

	// -------------------------------------------------------------------------
	public void setMusicSilence() {
		this.mIsSilence = true;
		this.mIsMaxVolume = false;
		this.mAudioManager.setStreamVolume(AudioManager.STREAM_MUSIC, 0, 0);
		// Log.e("AudioController",
		// "setMusicSilence:: "
		// + this.mAudioManager
		// .getStreamVolume(AudioManager.STREAM_MUSIC));
	}

	// -------------------------------------------------------------------------
	public void cancelMusicSilence() {
		this.mIsSilence = false;
		setCurrentMusicVolume(this.mCurrentVolumeProgress);
		// Log.e("AudioController",
		// "cancelMusicSilence:: "
		// + this.mAudioManager
		// .getStreamVolume(AudioManager.STREAM_MUSIC));
	}

	// -------------------------------------------------------------------------
	public int getIsSilence() {
		return this.mIsSilence ? 1 : 0;
	}

	// -------------------------------------------------------------------------
	public void setMusicMax() {
		this.mIsMaxVolume = true;
		this.mIsSilence = false;
		this.mAudioManager.setStreamVolume(AudioManager.STREAM_MUSIC,
				this.mMaxVolume, 0);
		// Log.e("AudioController",
		// "setMusicMax:: "
		// + this.mAudioManager
		// .getStreamVolume(AudioManager.STREAM_MUSIC));
	}

	// -------------------------------------------------------------------------
	public void cancelMusicMax() {
		this.mIsMaxVolume = false;
		setCurrentMusicVolume(this.mCurrentVolumeProgress);
		// Log.e("AudioController",
		// "cancelMusicMax:: "
		// + this.mAudioManager
		// .getStreamVolume(AudioManager.STREAM_MUSIC));
	}

	// -------------------------------------------------------------------------
	public int getIsMaxVolume() {
		return this.mIsMaxVolume ? 1 : 0;
	}

	// -------------------------------------------------------------------------
	public void setCurrentMusicVolume(float current_muiscvolume) {
		if (this.mIsSilence || this.mIsMaxVolume) {
			return;
		}

		this.mCurrentVolumeProgress = current_muiscvolume;
		// Log.e("AudioController",
		// "setCurrentMusicVolume:: mCurrentVolumeProgress::"
		// + mCurrentVolumeProgress);
		this.mCurrentVolume = (int) (this.mMaxVolume * this.mCurrentVolumeProgress);
		// Log.e("AudioController", "setCurrentMusicVolume:: mCurrentVolume::"
		// + mCurrentVolume);
		this.mAudioManager.setStreamVolume(AudioManager.STREAM_MUSIC,
				this.mCurrentVolume, 0);
		// Log.e("AudioController", "setCurrentMusicVolume:: "
		// + this.mAudioManager.getStreamVolume(AudioManager.STREAM_MUSIC));
	}

	// -------------------------------------------------------------------------
	public float getCurrentMusicVolume() {
		return this.mAudioManager
				.getStreamVolume(AudioManager.STREAM_MUSIC);
	}
}
