using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SoundManager))]
public partial class SoundManagerEditor : Editor {	
	private SoundManager script;
	
	#region editor variables
	private List<AudioClip> soundsToAdd = new List<AudioClip>();
	private string levelToAdd;
	private int levelIndex = 0;
	private bool isCustom;
	private SoundManager.PlayMethod playMethodToAdd;
	private float minDelayToAdd;
	private float maxDelayToAdd;
	private float delayToAdd;
	private bool isEditing = false;
	private AudioClip editAddClip;
	private AudioClip editAddClipFromSelector;
	private float minDelayToEdit;
	private float maxDelayToEdit;
	private float delayToEdit;
	private Texture2D titleBar;
	private Texture2D footer;
	private Texture2D icon;
	
	private bool listeningForGuiChanges;
    private bool guiChanged;
	
	private Color softGreen = new Color(.67f,.89f,.67f,1f);
	private Color hardGreen = Color.green;
	private Color inactiveColor = new Color(.65f,.65f,.65f);
	
	private string defaultName = "-enter name-";
	private bool repaintNextFrame = false;
	private bool indent4_3_up = false;
	#endregion
	
	private void OnEnable()
	{
		script = target as SoundManager;
		
#if !(UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2)
		indent4_3_up = true;
#endif
		
		HideTransform();

		Init();
		InitSFX();
		
		if(!titleBar)
			titleBar = AssetDatabase.LoadAssetAtPath ("Assets/Gizmos/TitleBar.png", typeof(Texture2D)) as Texture2D;
		if(!footer)
			footer = AssetDatabase.LoadAssetAtPath ("Assets/Gizmos/AntiLunchBox Logo.png", typeof(Texture2D)) as Texture2D;
		if(!icon)
			icon = AssetDatabase.LoadAssetAtPath ("Assets/Gizmos/SoundManager Icon.png", typeof(Texture2D)) as Texture2D;
	}
	
	private void OnDisable()
	{
		HideTransform();
	}
	
	private void HideTransform()
	{
		if(script != null)
		{
			if(script.GetComponent<Transform>().hideFlags != HideFlags.HideInInspector)
				script.GetComponent<Transform>().hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
		}
	}
	
	private void Init()
	{
		if(!Application.isPlaying)
		{
			if(script.audios == null)
			{
				if(script.GetComponents<AudioSource>().Length == 2)
				{
					script.audios = script.GetComponents<AudioSource>();
				} 
				else
				{
					script.audios = new AudioSource[2];
					script.audios[0] = script.gameObject.AddComponent<AudioSource>(); 
					script.audios[1] = script.gameObject.AddComponent<AudioSource>();
				}
			}
			else
			{
				if(script.audios.Length != 2)
				{
					for(int i = 0; i < script.audios.Length; i++)
					{
						DestroyImmediate(script.audios[i]);
					}
					script.audios = new AudioSource[2];
					script.audios[0] = script.gameObject.AddComponent<AudioSource>(); 
					script.audios[1] = script.gameObject.AddComponent<AudioSource>();
				}
			}
			
			script.audios[0].hideFlags = HideFlags.HideInInspector;
			script.audios[1].hideFlags = HideFlags.HideInInspector;
			
			SoundManagerTools.make2D(ref script.audios[0]);
			SoundManagerTools.make2D(ref script.audios[1]);
			
			script.audios[0].volume = 0f;
			script.audios[1].volume = 0f;
			
			script.audios[0].ignoreListenerVolume = true;
			script.audios[1].ignoreListenerVolume = true;
		}
		
		for(int i = 0; i < script.soundConnections.Count ; i++)
		{
			string levelName = script.soundConnections[i].level;
			if(!script.songStatus.ContainsKey(levelName))
				script.songStatus.Add(levelName, SoundManager.HIDE);
			
			while(script.soundConnections[i].soundsToPlay.Count > script.soundConnections[i].baseVolumes.Count)
				script.soundConnections[i].baseVolumes.Add(1f);
		}
	}
	
	private GUIStyle CreateFoldoutGUI()
	{
		GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
		Color myStyleColor = Color.white;
		myFoldoutStyle.fontStyle = FontStyle.Bold;
		myFoldoutStyle.normal.textColor = myStyleColor;
		myFoldoutStyle.onNormal.textColor = myStyleColor;
		myFoldoutStyle.hover.textColor = myStyleColor;
		myFoldoutStyle.onHover.textColor = myStyleColor;
		myFoldoutStyle.focused.textColor = myStyleColor;
		myFoldoutStyle.onFocused.textColor = myStyleColor;
		myFoldoutStyle.active.textColor = myStyleColor;
		myFoldoutStyle.onActive.textColor = myStyleColor;
		
		return myFoldoutStyle;
	}

	public override void OnInspectorGUI()
	{
		GUI.color = Color.white;
		GUIStyle foldoutStyle = CreateFoldoutGUI();
		
		var expand = '\u2261'.ToString();
		GUIContent expandContent = new GUIContent(expand, "Expand/Collapse");
		
		if(!script)
			return;

		if(titleBar != null)
		{
			GUILayout.BeginHorizontal();
			{
	    		GUILayout.FlexibleSpace();
				{
	    			GUILayout.Label(titleBar);
				}
	    		GUILayout.FlexibleSpace();
			}
			GUILayout.EndHorizontal();
		}

		if(script.showInfo)
			GUI.color = inactiveColor;
		if(indent4_3_up)
			EditorGUI.indentLevel++;
		EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb, GUILayout.ExpandWidth(true));
		{
			EditorGUILayout.BeginHorizontal();
			{
				GUI.color = Color.white;
				script.showInfo = EditorGUILayout.Foldout(script.showInfo, new GUIContent("Info", "Information about the current scene and tracks playing."), foldoutStyle);
				script.showInfo = GUILayout.Toggle(script.showInfo, expandContent, EditorStyles.toolbarButton, GUILayout.Width(50f));
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();
		if(indent4_3_up)
			EditorGUI.indentLevel--;
		
		if(script.showInfo)
		{
			EditorGUILayout.BeginVertical();
			{
				ShowInfo();
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.Separator();

		if(script.showDev)
			GUI.color = inactiveColor;
		if(indent4_3_up)
			EditorGUI.indentLevel++;
		EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb, GUILayout.ExpandWidth(true));
		{
			EditorGUILayout.BeginHorizontal();
			{
				GUI.color = Color.white;
				script.showDev = EditorGUILayout.Foldout(script.showDev, new GUIContent("Developer Settings", "Settings to customize how SoundManager behaves."), foldoutStyle);
				script.showDev = GUILayout.Toggle(script.showDev, expandContent, EditorStyles.toolbarButton, GUILayout.Width(50f));
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();
		if(indent4_3_up)
			EditorGUI.indentLevel--;
		
		if(script.showDev)
		{
			EditorGUILayout.BeginVertical();
			{
				ShowDeveloperSettings();
			}
			EditorGUILayout.EndVertical();	
		}
		EditorGUILayout.Separator();
		
		if(script.showList)
			GUI.color = inactiveColor;
		if(indent4_3_up)
			EditorGUI.indentLevel++;
		EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb, GUILayout.ExpandWidth(true));
		{
			EditorGUILayout.BeginHorizontal();
			{
				GUI.color = Color.white;
				script.showList = EditorGUILayout.Foldout(script.showList, new GUIContent("SoundConnections", "List of existing SoundConnections that tie a group of clips, a play method, and various other variables to a scene."), foldoutStyle);	
				GUILayout.FlexibleSpace();
				if(!script.viewAll && GUILayout.Button(new GUIContent("view all", "show all SoundConnections"), EditorStyles.toolbarButton, GUILayout.Width(75f)))
				{
					script.showList = true;
					script.viewAll = true;
				} else if (script.viewAll && GUILayout.Button(new GUIContent("hide all", "hide all SoundConnections"), EditorStyles.toolbarButton, GUILayout.Width(75f))) {
					script.viewAll = false;
				}
				EditorGUILayout.Space();
				script.showList = GUILayout.Toggle(script.showList, expandContent, EditorStyles.toolbarButton, GUILayout.Width(50f));
			}
			EditorGUILayout.EndHorizontal();		
		}
		EditorGUILayout.EndVertical();
		if(indent4_3_up)
			EditorGUI.indentLevel--;
		
		if(script.showList)
		{
			EditorGUILayout.BeginVertical();
			{
				ShowSoundConnectionList();
			}
			EditorGUILayout.EndVertical();	
		}
		EditorGUILayout.Separator();

		if(script.showAdd)
			GUI.color = inactiveColor;
		if(indent4_3_up)
			EditorGUI.indentLevel++;
		EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb, GUILayout.ExpandWidth(true));
		{
			EditorGUILayout.BeginHorizontal();
			{
				GUI.color = Color.white;
				script.showAdd = EditorGUILayout.Foldout(script.showAdd, new GUIContent("Add SoundConnection(s)", "Create SoundConnections here. Choose a level, play method, and selection of songs to make one. Choose <custom> to make a custom SoundConnection that isn't tied to a level. REMEMBER: Levels will appear only if they are added to the build settings."), foldoutStyle);
				script.showAdd = GUILayout.Toggle(script.showAdd, expandContent, EditorStyles.toolbarButton, GUILayout.Width(50f));
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();
		if(indent4_3_up)
			EditorGUI.indentLevel--;
		
		if(script.showAdd)
		{
			EditorGUILayout.BeginVertical();
			{
				ShowAddSoundConnection();
			}
			EditorGUILayout.EndVertical();	
		}
		EditorGUILayout.Separator();

		if(script.showSFX)
			GUI.color = inactiveColor;
		if(indent4_3_up)
			EditorGUI.indentLevel++;
		EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb, GUILayout.ExpandWidth(true));
		{
			EditorGUILayout.BeginHorizontal();
			{
				GUI.color = Color.white;
				script.showSFX = EditorGUILayout.Foldout(script.showSFX, new GUIContent("SFX", "Section for handling SFX, and SFX groups--along with the attributes that accompany them."), foldoutStyle);
				script.showSFX = GUILayout.Toggle(script.showSFX, expandContent, EditorStyles.toolbarButton, GUILayout.Width(50f));
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();
		if(indent4_3_up)
			EditorGUI.indentLevel--;
		
		if(script.showSFX)
		{
			EditorGUILayout.BeginVertical();
			{
				ShowSoundFX();
			}
			EditorGUILayout.EndVertical();
		}
		
		if(footer != null)
		{
			GUILayout.BeginHorizontal();
			{
	    		GUILayout.FlexibleSpace();
				{
	    			GUILayout.Label(footer, GUI.skin.label);
				}
	    		GUILayout.FlexibleSpace();
			}
			GUILayout.EndHorizontal();
		}
	}
	
	#region GUI functions
	private void ShowInfo()
	{
		EditorGUI.indentLevel++;
		{
            EditorGUILayout.LabelField("Current Scene:", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);//Application.loadedLevelName);

            float crossDuration = script.crossDuration;
			crossDuration = EditorGUILayout.FloatField(new GUIContent("Cross Duration:","Duration of crossfades for music."),crossDuration);
			if(crossDuration < 0) 
				crossDuration = 0f;
			if(crossDuration != script.crossDuration)
			{
				SoundManagerEditorTools.RegisterObjectChange("Change Cross Duration", script);
				script.crossDuration = crossDuration;
				EditorUtility.SetDirty(script);
			}
			
			int audioSize = 0;
			if(script.audios != null && script.audios[0] != null && script.audios[1] != null)
				audioSize = script.audios.Length;
			if(audioSize != 0)
			{			
				string name1, name2;
				name1 = name2 = "No Song";
				
				GUI.color = hardGreen;
				if(script.audios[0] && script.audios[0].clip)
					name1 = script.audios[0].clip.name;
				else
					GUI.color = Color.red;
				
				Rect rect = GUILayoutUtility.GetRect (28, 28, "TextField");
				if(name1 != "No Song")
					name1 += "\n" + System.TimeSpan.FromSeconds(script.audios[0].time).ToString().Split('.')[0] + "/" + System.TimeSpan.FromSeconds(script.audios[0].clip.length).ToString().Split('.')[0];
				EditorGUI.ProgressBar(rect,script.audios[0].volume,name1);
				
				GUI.color = hardGreen;
				if(script.audios[1] && script.audios[1].clip)
					name2 = script.audios[1].clip.name;
				else
					GUI.color = Color.red;
				
				Rect rect2 = GUILayoutUtility.GetRect(28, 28, "TextField");
				if(name2 != "No Song")
					name2 += "\n" + System.TimeSpan.FromSeconds(script.audios[1].time).ToString().Split('.')[0] + "/" + System.TimeSpan.FromSeconds(script.audios[1].clip.length).ToString().Split('.')[0];
				EditorGUI.ProgressBar(rect2,script.audios[1].volume,name2);
				
				GUI.color = Color.white;
				Repaint();
			} else {
				GUI.color = Color.red;
				Rect rect = GUILayoutUtility.GetRect (28, 28, "TextField");
				EditorGUI.ProgressBar(rect,0f,"Standing By...");
				
				Rect rect2 = GUILayoutUtility.GetRect(28, 28, "TextField");
				EditorGUI.ProgressBar(rect2,0f,"Standing By...");
				GUI.color = Color.white;
			}
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.Space();
	}
	
	private void ShowSoundConnectionList()
	{
		EditorGUI.indentLevel++;
		{
			for(int i = 0 ; i < script.soundConnections.Count ; i++)
			{
				ShowSoundConnection(i);
			}
			GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.Space();
	}
	
	private void ShowSoundConnection(int index)
	{
		GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
		
		string status = SoundManager.HIDE;
		SoundConnection sc = script.soundConnections[index];
		
		EditorGUILayout.BeginVertical();
		{
			EditorGUILayout.BeginHorizontal();
			{
				status = script.songStatus[sc.level] as string;
				
				if(status == SoundManager.HIDE) GUI.enabled = false;
				if(icon)
					GUILayout.Label(icon, GUILayout.ExpandWidth(false));
				GUI.enabled = true;
				
				if(sc.isCustomLevel)
					EditorGUILayout.LabelField("<custom>"+sc.level);
				else
					EditorGUILayout.LabelField(sc.level);
				
				if(status == SoundManager.HIDE && GUILayout.Button("view", GUILayout.Width(40f)))
				{
					script.songStatus[sc.level] = SoundManager.VIEW;
				} else if(status == SoundManager.VIEW) {
					GUI.color = Color.white;
					if(GUILayout.Button("hide", GUILayout.Width(40f)))
					{
						script.songStatus[sc.level] = SoundManager.HIDE;
					}
				}
				GUI.color = Color.cyan;
				if(status != SoundManager.EDIT && isEditing) GUI.enabled = false;
				if(status == SoundManager.HIDE && GUILayout.Button("edit", GUILayout.Width(40f)))
				{
					isEditing = true;
					script.songStatus[sc.level] = SoundManager.EDIT;
					delayToEdit = sc.delay;
					minDelayToEdit = sc.minDelay;
					maxDelayToEdit = sc.maxDelay;
				} else if (status == SoundManager.EDIT) {
					GUI.color = Color.white;
					if(GUILayout.Button("done", GUILayout.Width(40f)))
					{
						isEditing = false;
						script.songStatus[sc.level] = SoundManager.HIDE;
					}
				}
				GUI.enabled = true;
				GUI.color = Color.red;
				if(status == SoundManager.HIDE && GUILayout.Button("X", GUILayout.Width(25f)))
				{
					RemoveSoundConnection(index);
					return;
				}
				GUI.color = Color.white;
			}
			EditorGUILayout.EndHorizontal();
			
			if(status != SoundManager.HIDE)
			{
				EditorGUI.indentLevel++;
				if(status == SoundManager.VIEW)
				{
					ShowSoundConnectionInfo(sc,false);	
				}
				else if (status == SoundManager.EDIT)
				{
					ShowSoundConnectionInfo(sc,true);
				}
				EditorGUI.indentLevel--;
			}
		}
		EditorGUILayout.EndVertical();
	}
	
	private void ShowSoundConnectionInfo(SoundConnection obj, bool editable)
	{
		EditorGUILayout.BeginVertical();
		{
			SoundManager.PlayMethod playMethod = obj.playMethod;
			if(!editable)
			{
				EditorGUILayout.LabelField("Play Method: ", playMethod.ToString());
			} else {
				playMethod = (SoundManager.PlayMethod)EditorGUILayout.EnumPopup(new GUIContent("Play Method:", "Dictates how the SoundConnection will play through the sound list."), playMethod, EditorStyles.popup);
				if(playMethod != obj.playMethod)
				{
					SoundManagerEditorTools.RegisterObjectChange("Change Play Method", script);
					obj.playMethod = playMethod;
					EditorUtility.SetDirty(script);
				}
			}
			ShowMethodDetails(obj, editable, playMethod);
			EditorGUILayout.HelpBox(GetPlayMethodDescription(playMethod), MessageType.Info);
			ShowSongList(obj,editable);
		}
		EditorGUILayout.EndVertical();
	}
	
	private void ShowMethodDetails(SoundConnection obj, bool editable, SoundManager.PlayMethod method)
	{
		CheckUndo();
		EditorGUI.indentLevel++;
		switch (method)
		{
		case SoundManager.PlayMethod.ContinuousPlayThrough:
		case SoundManager.PlayMethod.OncePlayThrough:
		case SoundManager.PlayMethod.ShufflePlayThrough:
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithDelay:
		case SoundManager.PlayMethod.OncePlayThroughWithDelay:
		case SoundManager.PlayMethod.ShufflePlayThroughWithDelay:
			if(!editable)
				EditorGUILayout.LabelField("Delay:" + obj.delay + " second(s)");
			else {
				delayToEdit = obj.delay;
				#if !(UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2)
				EditorGUI.BeginChangeCheck();
				#endif
				delayToEdit = EditorGUILayout.FloatField("Delay:", delayToEdit);
				if(delayToEdit < 0) 
					delayToEdit = 0f;

				#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
				obj.delay = delayToEdit;
				#else
				if(EditorGUI.EndChangeCheck())
				{
					Undo.RecordObjects(new Object[]{script}, "Modify Delay");
					obj.delay = delayToEdit;
					EditorUtility.SetDirty(script);
				}
				#endif

				#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
				if(GUI.changed)
				{
					if(!guiChanged && listeningForGuiChanges)
					{
						guiChanged = true;
						CheckUndo();
					}
					guiChanged = true;
					EditorUtility.SetDirty(script);
				}
				#endif
			}
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.OncePlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.ShufflePlayThroughWithRandomDelayInRange:
			if(!editable)
			{
				EditorGUILayout.LabelField("Min Delay:" + obj.minDelay + " second(s)");
				EditorGUILayout.LabelField("Max Delay:" + obj.maxDelay + " second(s)");
			} else {
				minDelayToEdit = obj.minDelay;
				maxDelayToEdit = obj.maxDelay;

				#if !(UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2)
				EditorGUI.BeginChangeCheck();
				#endif

				minDelayToEdit = EditorGUILayout.FloatField("Minimum Delay:",minDelayToEdit);
				if(minDelayToEdit < 0) 
					minDelayToEdit = 0f;

				#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
				obj.minDelay = minDelayToEdit;
				#else
				if(EditorGUI.EndChangeCheck())
				{
					Undo.RecordObjects(new Object[]{script}, "Modify Minimum Delay");
					obj.minDelay = minDelayToEdit;
					EditorUtility.SetDirty(script);
				}
				#endif
				
				if(maxDelayToEdit < minDelayToEdit) 
					maxDelayToEdit = minDelayToEdit;
				maxDelayToEdit = EditorGUILayout.FloatField("Maximum Delay:",maxDelayToEdit);
				if(maxDelayToEdit < 0) 
					maxDelayToEdit = 0f;

				#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
				obj.maxDelay = maxDelayToEdit;
				#else
				if(EditorGUI.EndChangeCheck())
				{
					Undo.RecordObjects(new Object[]{script}, "Modify Maximum Delay");
					obj.maxDelay = maxDelayToEdit;
					EditorUtility.SetDirty(script);
				}
				#endif

				#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
				if(GUI.changed)
				{
					if(!guiChanged && listeningForGuiChanges)
					{
						guiChanged = true;
						CheckUndo();
					}
					guiChanged = true;
					EditorUtility.SetDirty(script);
				}
				#endif
			}
			break;
		}
		EditorGUI.indentLevel--;
	}
	
	private void ShowSongList(SoundConnection obj, bool editable)
	{
		int size = obj.soundsToPlay.Count;
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField(new GUIContent("Sound List:", "List of sounds in the SoundConnection. How they play is determined by the SoundConnection's PlayMethod. You can also assign the base volume of each sound."));
			if(editable)
			{
				GUILayout.FlexibleSpace();
				EditorGUILayout.LabelField(new GUIContent("B-Vol:", "Base volume for an AudioClip in this SoundConnection. Lower this value if you don't want a particular AudioClip to play as loud. When in doubt, keep this at 1."), GUILayout.Width(65f));
				EditorGUILayout.LabelField("", GUILayout.Width(90f));
			}
		}
		EditorGUILayout.EndHorizontal();
		EditorGUI.indentLevel++;
		
		for(int i = 0; i < size ; i++)
		{
			EditorGUILayout.BeginVertical();
			{
				EditorGUILayout.BeginHorizontal();
				{
					if(!editable)
					{
						EditorGUILayout.LabelField(obj.soundsToPlay[i].name);
					} else {
						if(obj.soundsToPlay[i] == null) 
							return;
						
						AudioClip newClip = (AudioClip)EditorGUILayout.ObjectField(obj.soundsToPlay[i], typeof(AudioClip), false, GUILayout.ExpandWidth(true));
						if(newClip != null)
							obj.soundsToPlay[i] = newClip;
						
						if(GUI.changed)
						{
							if(newClip == null)
							{
								RemoveSound(obj, i);
								return;
							}
						}
						
						float baseVolume = obj.baseVolumes[i];
						baseVolume = EditorGUILayout.FloatField(baseVolume, GUILayout.Width(55f));
						if(baseVolume < 0f) 
							baseVolume = 0f;
						else if (baseVolume > 1f)
							baseVolume = 1f;
						if(baseVolume != obj.baseVolumes[i])
						{
							SoundManagerEditorTools.RegisterObjectChange("Change Base Volume", script);
							obj.baseVolumes[i] = baseVolume;
							EditorUtility.SetDirty(script);
						}
						
						bool oldEnabled = GUI.enabled;
						if(i == 0) GUI.enabled = false;
						if(GUILayout.Button("U", GUILayout.Width(35f)))
						{
							SwapSounds(obj, i, i-1);
						}
						GUI.enabled = oldEnabled;
						
						if(i == size-1) GUI.enabled = false;
						if(GUILayout.Button("D", GUILayout.Width(35f)))
						{
							SwapSounds(obj, i, i+1);
						}
						GUI.enabled = oldEnabled;
						
						GUI.color = Color.red;
						if(GUILayout.Button("-", GUILayout.Width(20f)))
						{
							RemoveSound(obj, i);
							return;
						}
						GUI.color = Color.white;
					}
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
		}
		
		if(editable)
		{
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUI.indentLevel++;
				editAddClip = EditorGUILayout.ObjectField("Add A Sound", editAddClip, typeof(AudioClip), false) as AudioClip;
				
				GUI.color = softGreen;
				if(GUILayout.Button("add", GUILayout.Width(40f)))
				{
					AddSound(obj, editAddClip);
					editAddClip = null;
				}
				GUI.color = Color.white;
				EditorGUI.indentLevel--;
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUI.indentLevel--;
	}
	
	private void ShowAddSoundConnection()
	{
		ShowAddSoundConnectionArea();
	}
	
	private void ShowAddSoundConnectionArea()
	{
		bool full = ShowRequiredSoundConnectionInfo();
		if(full)
		{
			ShowAddSongList();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			DropAreaGUI();
			ShowAddClipSelector();
		}
		
		if(!full) 
			GUI.enabled = false;
		ShowAddSoundConnectionButton();
		GUI.enabled = true;
	}
	
	private void ShowAddSongList()
	{
		int soundToRemove = -1;
		EditorGUILayout.LabelField("Sound List:");
		EditorGUI.indentLevel++;
		{
			foreach(AudioClip soundToAdd in soundsToAdd)
			{
				EditorGUILayout.BeginVertical();
				{
					EditorGUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField(soundToAdd.name);
						bool oldEnabled = GUI.enabled;
						
						if(!SoundCanMoveUp(soundToAdd, soundsToAdd)) 
							GUI.enabled = false;
						if(GUILayout.Button("U", GUILayout.Width(35f)))
						{
							int thisIndex = soundsToAdd.IndexOf(soundToAdd);
							SwapSounds(soundToAdd, soundsToAdd[thisIndex-1], soundsToAdd);
						}
						GUI.enabled = oldEnabled;
						
						if(!SoundCanMoveDown(soundToAdd, soundsToAdd)) GUI.enabled = false;
						if(GUILayout.Button("D", GUILayout.Width(35f)))
						{
							int thisIndex = soundsToAdd.IndexOf(soundToAdd);
							SwapSounds(soundToAdd, soundsToAdd[thisIndex+1], soundsToAdd);
						}
						GUI.enabled = oldEnabled;
						GUI.color = Color.red;
						if(GUILayout.Button("-", GUILayout.Width(20f)))
						{
							soundToRemove = soundsToAdd.IndexOf(soundToAdd);
						}
						GUI.color = Color.white;
					}
					EditorGUILayout.EndHorizontal();
				}
				EditorGUILayout.EndVertical();
			}
			if(soundToRemove >= 0)
			{
				soundsToAdd.RemoveAt(soundToRemove);
				return;
			}
		}
		EditorGUI.indentLevel--;
	}
	
	private bool ShowRequiredSoundConnectionInfo()
	{
		bool canMoveOn = true, forceRepaint = false;
		if(canMoveOn)
		{
			canMoveOn = false;
			string[] availableNames = GetAvailableLevelNamesForAddition();
			if(availableNames.Length == 1)
			{
				EditorGUILayout.LabelField("All enabled scenes have SoundConnections already.");
			}
			if(levelIndex >= availableNames.Length)
				levelIndex = availableNames.Length-1;
			
			levelIndex = EditorGUILayout.Popup("Choose Level:", levelIndex, availableNames, EditorStyles.popup);
			
			if(levelIndex == availableNames.Length-1) //must be custom
			{
				if(!isCustom)
				{
					if(levelToAdd != defaultName)
					{
						levelToAdd = defaultName;
						forceRepaint = true;
						GUIUtility.keyboardControl = 0;
					}
				}
				
				isCustom = true;
				bool isValidName = true, isEmptyName = false;
				
				if(string.IsNullOrEmpty(levelToAdd) || levelToAdd == defaultName)
					isEmptyName = true;
				if(isEmptyName || IsStringSceneName(levelToAdd) || IsStringAlreadyTaken(levelToAdd))
					isValidName = false;
				
				if(isValidName)
					GUI.color = Color.green;
				else if(!isEmptyName)
					GUI.color = Color.red;
				
				EditorGUILayout.BeginHorizontal();
				{
					levelToAdd = EditorGUILayout.TextField("Custom Level Name:", levelToAdd, GUILayout.MinWidth(100), GUILayout.ExpandWidth(true));
					if(isValidName)
						EditorGUILayout.LabelField("OK", GUILayout.Width(35));
					else if(!isEmptyName)
						EditorGUILayout.LabelField("BAD", GUILayout.Width(35));
					canMoveOn = isValidName;
				}
				EditorGUILayout.EndHorizontal();
				GUI.color = Color.white;
			}
			else
			{
				if(isCustom)
				{
					forceRepaint = repaintNextFrame = true;
					GUIUtility.keyboardControl = 0;
				}
					
				isCustom = false;
				levelToAdd = availableNames[levelIndex];
				canMoveOn = true;
			}
			
			if(canMoveOn)
			{
				canMoveOn = false;
				playMethodToAdd = (SoundManager.PlayMethod)EditorGUILayout.EnumPopup("Play Method:", playMethodToAdd, EditorStyles.popup);
				
				canMoveOn = ShowPlayMethodParameters(playMethodToAdd);
			}
		}
		
		if(forceRepaint)
			SceneView.RepaintAll();
		else if(repaintNextFrame)
		{
			SceneView.RepaintAll();
			repaintNextFrame = false;
		}
		return canMoveOn;
	}
	
	private bool ShowPlayMethodParameters(SoundManager.PlayMethod method)
	{
		bool canMoveOn = true;
		switch (method)
		{
		case SoundManager.PlayMethod.ContinuousPlayThrough:
		case SoundManager.PlayMethod.OncePlayThrough:
		case SoundManager.PlayMethod.ShufflePlayThrough:
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithDelay:
		case SoundManager.PlayMethod.OncePlayThroughWithDelay:
		case SoundManager.PlayMethod.ShufflePlayThroughWithDelay:
			canMoveOn = false;
			delayToAdd = EditorGUILayout.FloatField("Delay:", delayToAdd);
			if(delayToAdd < 0) 
				delayToAdd = 0f;
			canMoveOn = true;
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.OncePlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.ShufflePlayThroughWithRandomDelayInRange:
			canMoveOn = false;
			minDelayToAdd = EditorGUILayout.FloatField("Minimum Delay:", minDelayToAdd);
			if(minDelayToAdd < 0) 
				minDelayToAdd = 0f;
			if(maxDelayToAdd < minDelayToAdd) 
				maxDelayToAdd = minDelayToAdd;
			maxDelayToAdd = EditorGUILayout.FloatField("Maximum Delay:", maxDelayToAdd);
			if(maxDelayToAdd < 0) 
				maxDelayToAdd = 0f;
			canMoveOn = true;
			break;
		}
		return canMoveOn;
	}
	
	private void ShowAddSoundConnectionButton()
	{
		GUILayout.BeginHorizontal();
		{
			GUILayout.FlexibleSpace();
			{
				GUI.color = softGreen;
				if(GUILayout.Button("Finish and Add SoundConnection", GUILayout.MaxWidth(250f)))
				{
					AddSoundConnection();
				}
				GUI.color = Color.white;
			}
			GUILayout.FlexibleSpace();
		}
		GUILayout.EndHorizontal();
	}
	
	private void ShowDeveloperSettings()
	{
		EditorGUI.indentLevel++;
		{
			bool showDebug = script.showDebug;
			showDebug = EditorGUILayout.Toggle(new GUIContent("Show Debug Info:", "Debug to the console what's happening with the music. Track changes, scene changes, etc.") , showDebug);
			if(showDebug != script.showDebug)
			{
				SoundManagerEditorTools.RegisterObjectChange("Change Show Debug", script);
				script.showDebug = showDebug;
				EditorUtility.SetDirty(script);
			}
			
			bool ignoreLevelLoad = script.ignoreLevelLoad;
			ignoreLevelLoad = EditorGUILayout.Toggle(new GUIContent("Ignore Level Load:", "Sets whether the SoundManager uses the level loading behavior. Set this to true to control when SoundConnections change completely yourself.") , ignoreLevelLoad);
			if(ignoreLevelLoad != script.ignoreLevelLoad)
			{
				SoundManagerEditorTools.RegisterObjectChange("Set Ignore Level Load", script);
				script.ignoreLevelLoad = ignoreLevelLoad;
				EditorUtility.SetDirty(script);
			}
		
			bool offTheBGM = script.offTheBGM;
			offTheBGM = EditorGUILayout.Toggle(new GUIContent("Music Always Off:", "Turns off and won't play any music. Recommended for use during development so you aren't always hearing game music!") , offTheBGM);
			if(offTheBGM != script.offTheBGM)
			{
				SoundManagerEditorTools.RegisterObjectChange("Toggle BGM", script);
				script.offTheBGM = offTheBGM;
				EditorUtility.SetDirty(script);
			}
		
			bool offTheSFX = script.offTheSFX;
			offTheSFX = EditorGUILayout.Toggle(new GUIContent("SFX Always Off:", "Turns off and won't play any sound effects. Recommended for use during development so you aren't always hearing game sounds!") , offTheSFX);
			if(offTheSFX != script.offTheSFX)
			{
				SoundManagerEditorTools.RegisterObjectChange("Toggle SFX", script);
				script.offTheSFX = offTheSFX;
				EditorUtility.SetDirty(script);
			}
		
			int capAmount = script.capAmount;
			capAmount = EditorGUILayout.IntField(new GUIContent("SFX Cap Amount:", "Sets the automatic cap amount whenever playing capped sound effects. Capped sound effects won't play more than this amount at a time. Useful for sound effects that can get played many times at once."), capAmount, GUILayout.Width(3f*Screen.width/4f));
			if(capAmount < 0) 
				capAmount = 0;
			if(capAmount != script.capAmount)
			{
				SoundManagerEditorTools.RegisterObjectChange("Change SFX Cap Amount", script);
				script.capAmount = capAmount;
				EditorUtility.SetDirty(script);
			}
			
			float SFXObjectLifetime = script.SFXObjectLifetime;
			SFXObjectLifetime = EditorGUILayout.FloatField(new GUIContent("SFX Object Lifetime:", "This sets how long sound effect objects live in the pool if they exceed the prepool amount (in seconds)."), SFXObjectLifetime, GUILayout.Width(3f*Screen.width/4f));
			if(SFXObjectLifetime < 1f) 
				SFXObjectLifetime = 1f;
			if(SFXObjectLifetime != script.SFXObjectLifetime)
			{
				SoundManagerEditorTools.RegisterObjectChange("Change SFX Object Lifetime", script);
				script.SFXObjectLifetime = SFXObjectLifetime;
				EditorUtility.SetDirty(script);
			}
		
			string resourcesPath = script.resourcesPath;
			resourcesPath = EditorGUILayout.TextField(new GUIContent("Default Load Path:", "This is the default path that the SoundManager will try to load sound effects from if they do not live on the SoundManager."), resourcesPath, GUILayout.Width(3f*Screen.width/4f));
			if(resourcesPath != script.resourcesPath)
			{
				SoundManagerEditorTools.RegisterObjectChange("Change Default Load Path", script);
				script.resourcesPath = resourcesPath;
				EditorUtility.SetDirty(script);
			}
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.Space();
	}
	
	private void ShowAddClipSelector()
	{
		EditorGUILayout.BeginHorizontal();
		{
			editAddClipFromSelector = EditorGUILayout.ObjectField("Select An AudioClip:", editAddClipFromSelector, typeof(AudioClip), false) as AudioClip;
			GUI.color = softGreen;
			if(GUILayout.Button("add", GUILayout.Width(40f)))
			{
				if(editAddClipFromSelector != null)
				{
					soundsToAdd.Add(editAddClipFromSelector);
					editAddClipFromSelector = null;
				}
			}
			GUI.color = Color.white;
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
	}
	
	private void DropAreaGUI()
	{
		GUI.color = softGreen;
		EditorGUILayout.BeginVertical();
		{
			var evt = Event.current;
			
			var dropArea = GUILayoutUtility.GetRect(0f,50f,GUILayout.ExpandWidth(true));
			GUI.Box (dropArea, "Drag AudioClip(s) Here");
			
			switch (evt.type)
			{
			case EventType.DragUpdated:
			case EventType.DragPerform:
				if(!dropArea.Contains (evt.mousePosition))
					break;
				
				DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
				
				if( evt.type == EventType.DragPerform)
				{
					DragAndDrop.AcceptDrag();
					
					foreach (var draggedObject in DragAndDrop.objectReferences)
					{
						var aC = draggedObject as AudioClip;
						if(!aC || aC.GetType() != typeof(AudioClip))
							continue;
						
						//Add audioclip to arsenal of SoundConnection
						soundsToAdd.Add(aC);
					}
				}
				Event.current.Use();
				break;
			}
		}
		EditorGUILayout.EndVertical();
		GUI.color = Color.white;
	}
	#endregion
	
	
	#region Functional
	private void AddSoundConnection()
	{
		SoundManagerEditorTools.RegisterObjectChange("Add SoundConnection", script);
		
		SoundConnection sc = null;
		switch (playMethodToAdd)
		{
		case SoundManager.PlayMethod.ContinuousPlayThrough:
		case SoundManager.PlayMethod.OncePlayThrough:
		case SoundManager.PlayMethod.ShufflePlayThrough:
			sc = new SoundConnection(levelToAdd, playMethodToAdd, soundsToAdd.ToArray());
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithDelay:
		case SoundManager.PlayMethod.OncePlayThroughWithDelay:
		case SoundManager.PlayMethod.ShufflePlayThroughWithDelay:
			sc = new SoundConnection(levelToAdd, playMethodToAdd, delayToAdd, soundsToAdd.ToArray());
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.OncePlayThroughWithRandomDelayInRange:
		case SoundManager.PlayMethod.ShufflePlayThroughWithRandomDelayInRange:
			sc = new SoundConnection(levelToAdd, playMethodToAdd, minDelayToAdd, maxDelayToAdd, soundsToAdd.ToArray());
			break;
		}
		
		if(isCustom)
		{
			sc.SetToCustom();
			levelToAdd = defaultName;
			repaintNextFrame = true;
		}
		
		script.soundConnections.Add(sc);
		
		RecalculateBools();
		EditorUtility.SetDirty(script);
		SceneView.RepaintAll();
	}
	
	private void RemoveSoundConnection(int index)
	{
		SoundManagerEditorTools.RegisterObjectChange("Remove SoundConnection", script);
		script.soundConnections.RemoveAt(index);
		RecalculateBools();
		EditorUtility.SetDirty(script);
		SceneView.RepaintAll();
	}
	
	private void RecalculateBools()
	{
		for(int i = 0; i < script.soundConnections.Count ; i++)
		{
			string levelName = script.soundConnections[i].level;
			if(!script.songStatus.ContainsKey(levelName))
			{
				script.songStatus.Add(levelName, SoundManager.HIDE);
			}
		}
	}
	
	private string[] GetAvailableLevelNamesForAddition()
	{
		List<string> availableScenes = new List<string>();
		foreach ( EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
		{
			if(scene.enabled)
			{
				string sceneNameBare = scene.path.Substring(scene.path.LastIndexOf('/') + 1, (scene.path.LastIndexOf('.') - scene.path.LastIndexOf('/')) - 1);
				if(script.soundConnections.Find(delegate(SoundConnection obj) { return obj.level == sceneNameBare; }) == null)
				{
					availableScenes.Add(sceneNameBare);
				}	
			}
		}
		availableScenes.Add("<custom>");
		return availableScenes.ToArray();
	}
	
	private bool IsStringSceneName(string nameToTest)
	{
		foreach ( EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
		{
			if(scene.enabled)
			{
				string sceneNameBare = scene.path.Substring(scene.path.LastIndexOf('/') + 1, (scene.path.LastIndexOf('.') - scene.path.LastIndexOf('/')) - 1);
				if(nameToTest == sceneNameBare)
					return true;
			}
		}
		return false;
	}
	
	private bool IsStringAlreadyTaken(string nameToTest)
	{
		if(script.soundConnections.Find(delegate(SoundConnection obj) { return obj.level == nameToTest; }) != null)
		{
			return true;
		}
		return false;
	}
	
	private bool SoundCanMoveUp(AudioClip sound, List<AudioClip> list)
	{
		if(!list.Contains(sound)) return false;
		int index = list.IndexOf(sound);
		if(index == 0) return false;
		return true;
	}
	
	private bool SoundCanMoveDown(AudioClip sound, List<AudioClip> list)
	{
		if(!list.Contains(sound)) return false;
		int index = list.IndexOf(sound);
		if(index == list.Count-1) return false;
		return true;
	}
	
	private void SwapSounds(AudioClip sound1, AudioClip sound2, List<AudioClip> list)
	{
		if(!(list.Contains(sound1) && list.Contains(sound2))) return;
		int index1 = list.IndexOf(sound1);
		int index2 = list.IndexOf(sound2);
		list[index1] = sound2;
		list[index2] = sound1;
	}
	
	private void SwapSounds(SoundConnection obj, int index1, int index2)
	{
		if(obj.soundsToPlay[index1] == null || obj.soundsToPlay[index2] == null)
			return;
		
		SoundManagerEditorTools.RegisterObjectChange("Move Sounds", script);
		AudioClip tempClip = obj.soundsToPlay[index1];
		obj.soundsToPlay[index1] = obj.soundsToPlay[index2];
		obj.soundsToPlay[index2] = tempClip;
		
		float tempBase = obj.baseVolumes[index1];
		obj.baseVolumes[index1] = obj.baseVolumes[index2];
		obj.baseVolumes[index2] = tempBase;
		EditorUtility.SetDirty(script);
		
		SceneView.RepaintAll();
	}
	
	private void RemoveSound(SoundConnection obj, int index)
	{
		if(obj.soundsToPlay[index] == null)
			return;
		
		SoundManagerEditorTools.RegisterObjectChange("Remove Sound", script);
		obj.soundsToPlay.RemoveAt(index);
		obj.baseVolumes.RemoveAt(index);
		EditorUtility.SetDirty(script);
		
		SceneView.RepaintAll();
	}
	
	private void AddSound(SoundConnection obj, AudioClip clip)
	{
		if(clip == null)
			return;
		
		SoundManagerEditorTools.RegisterObjectChange("Add Sound", script);
		obj.soundsToPlay.Add(clip);
		obj.baseVolumes.Add(1f);
		EditorUtility.SetDirty(script);
		
		SceneView.RepaintAll();
	}
	
	private void CheckUndo()
    {
		#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
        Event e = Event.current;
 
        if ( e.type == EventType.MouseDown && e.button == 0 || e.type == EventType.KeyUp && ( e.keyCode == KeyCode.Tab ) ) {
            Undo.SetSnapshotTarget( new Object[]{target, this}, "Modify Delay" );
            Undo.CreateSnapshot();
            Undo.ClearSnapshotTarget();
            listeningForGuiChanges = true;
            guiChanged = false;
        }
 
        if ( listeningForGuiChanges && guiChanged ) {
            Undo.SetSnapshotTarget( new Object[]{target, this}, "Modify Delay" );
            Undo.RegisterSnapshot();
            Undo.ClearSnapshotTarget();
            listeningForGuiChanges = false;
        }
		#endif
    }
	#endregion
					
	#region Informational
	string GetPlayMethodDescription(SoundManager.PlayMethod method)	
	{
		string desc = "";
		switch(method)
		{
		case SoundManager.PlayMethod.ContinuousPlayThrough:
			desc = "Repeat All In Order";
			break;
		case SoundManager.PlayMethod.OncePlayThrough:
			desc = "Play All In Order Once";
			break;
		case SoundManager.PlayMethod.ShufflePlayThrough:
			desc = "Repeat All Shuffled";
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithDelay:
			desc = "Repeat All In Order With Delay Between Songs";
			break;
		case SoundManager.PlayMethod.OncePlayThroughWithDelay:
			desc = "Play All In Order Once With Delay Between Songs";
			break;
		case SoundManager.PlayMethod.ShufflePlayThroughWithDelay:
			desc = "Repeat All Shuggled With Delay Between Songs";
			break;
		case SoundManager.PlayMethod.ContinuousPlayThroughWithRandomDelayInRange:
			desc = "Repeat All In Order With Delay Between Songs Within A Range";
			break;
		case SoundManager.PlayMethod.OncePlayThroughWithRandomDelayInRange:
			desc = "Play All In Order Once With Delay Between Songs Within A Range";
			break;
		case SoundManager.PlayMethod.ShufflePlayThroughWithRandomDelayInRange:
			desc = "Repeat All Shuffled With Delay Between Songs Within A Range";
			break;
		default:
			break;
		}
		return desc;
	}
	#endregion
}

