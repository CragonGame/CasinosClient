using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using antilunchbox;

public partial class SoundManager : antilunchbox.Singleton<SoundManager> {
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool viewAll {
		get {
			return _viewAll;
		} set {
			_viewAll = value;
			List<string> keys = new List<string>();
	        foreach (DictionaryEntry de in songStatus)
	            keys.Add(de.Key.ToString());
	
	        foreach(string key in keys)
	        {
				if(_viewAll)
				{
	            	songStatus[key] = VIEW;
				} else {
					songStatus[key] = HIDE;
				}
	        }
		}
	}
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public const string VIEW = "view";
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public const string EDIT = "edit";
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public const string HIDE = "hide";
	private bool _viewAll = false;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	[SerializeField]
	public Hashtable songStatus = new Hashtable();
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool helpOn = false;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool showInfo = true;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool showDev = true;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool showList = true;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool showAdd = true;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool showSFX = true;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public List<bool> showSFXDetails = new List<bool>();
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public int groupAddIndex = 0;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public int autoPrepoolAmount = 0;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public float autoBaseVolume = 1f;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public float autoVolumeVariation = 0f;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public float autoPitchVariation = 0f;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public bool showAsGrouped = false;
}
