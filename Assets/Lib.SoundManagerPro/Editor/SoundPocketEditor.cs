using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using antilunchbox;

[CustomEditor(typeof(SoundPocket))]
public class SoundPocketEditor : Editor {
	private SoundPocket script;
	
	#region editor variables
	private AudioClip editAddSFX;
	private Dictionary<int, List<int>> clipsByGroup = new Dictionary<int, List<int>>();
	
	private Color softGreen = new Color(.67f,.89f,.67f,1f);
	#endregion
	
	private void OnEnable()
	{
		script = target as SoundPocket;
		
		InitSFX();
	}
	
	private void InitSFX()
	{
		clipsByGroup.Clear();
		for(int i = script.clipToGroupValues.Count-1; i >= 0; i--)
		{
			if(i >= script.clipToGroupKeys.Count)
				continue;
			
			string groupName = script.clipToGroupValues[i];
			string clipName = script.clipToGroupKeys[i];
			int clipIndex = IndexOfClip(clipName);
			int groupIndex = IndexOfGroup(groupName);
			
			if(clipIndex < 0)
			{
				script.clipToGroupValues.RemoveAt(i);
				script.clipToGroupKeys.RemoveAt(i);
				continue;
			}
			
			if(clipsByGroup.ContainsKey(groupIndex))
			{
				clipsByGroup[groupIndex].Add(clipIndex);
			}
			else
			{
				clipsByGroup.Add(groupIndex, new List<int>(){clipIndex});
			}
		}
		while(script.sfxPrePoolAmounts.Count > script.sfxBaseVolumes.Count)
			script.sfxBaseVolumes.Add(1f);
		while(script.sfxPrePoolAmounts.Count > script.sfxVolumeVariations.Count)
			script.sfxVolumeVariations.Add(0f);
		while(script.sfxPrePoolAmounts.Count > script.sfxPitchVariations.Count)
			script.sfxPitchVariations.Add(0f);
	}
	
	public override void OnInspectorGUI()
	{
		EditorGUILayout.Space();
		ShowPocketName();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		ShowPocketType();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		ShowSFXGroupNames();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		ShowSoundFXTitle();
		
		ShowSoundFXList();
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		ShowAddSoundFX();
	}
	
	private void ShowPocketName()
	{
		EditorGUILayout.LabelField(new GUIContent("Pocket Name", "Name of the SoundPocket. If a SoundPocket already exists on the SoundManager, it will not be readded."), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
		
		string pocketName = script.pocketName;
		pocketName = EditorGUILayout.TextField(pocketName);
		if(pocketName != script.pocketName)
		{
			SoundManagerEditorTools.RegisterObjectChange("Change Pocket Name", script);
			script.pocketName = pocketName;
			EditorUtility.SetDirty(script);
		}
	}
	
	private void ShowPocketType()
	{
		EditorGUILayout.LabelField(new GUIContent("Pocket Type", "Determines how this pocket will be added to the SoundManager once the SoundPocket is loaded. If additive, these SFX will be added to the SoundManager. If subtractive, the SFX currently on the SoundManager will be removed before these are added."), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
		
		SoundPocketType pocketType = script.pocketType;
		pocketType = (SoundPocketType)EditorGUILayout.EnumPopup(pocketType);
		if(pocketType != script.pocketType)
		{
			SoundManagerEditorTools.RegisterObjectChange("Change Pocket Type", script);
			script.pocketType = pocketType;
			EditorUtility.SetDirty(script);
		}
	}
	
	private void ShowSFXGroupNames()
	{
		EditorGUILayout.LabelField(new GUIContent("SFXGroup Names", "These are possible group names for SFXs to be applied to. If the group exists on the SoundManager, it'll be added to that group. Otherwise, a new group will be created."), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
		
		for(int i = 0; i < script.sfxGroups.Count; i++)
		{
			string grpName = script.sfxGroups[i];
			grpName = EditorGUILayout.TextField(grpName);
			if(grpName != script.sfxGroups[i])
			{
				SoundManagerEditorTools.RegisterObjectChange("Change Pocket Group Name", script);
				script.sfxGroups[i] = grpName;
				EditorUtility.SetDirty(script);
			}
		}
		EditorGUILayout.BeginHorizontal();
		{
			if(GUILayout.Button("+"))
			{
				SoundManagerEditorTools.RegisterObjectChange("Add Pocket Group Name", script);
				script.sfxGroups.Add("");
				EditorUtility.SetDirty(script);
			}
			if(script.sfxGroups.Count <= 0)
				GUI.enabled = false;
			if(GUILayout.Button("-"))
			{
				SoundManagerEditorTools.RegisterObjectChange("Remove Pocket Group Name", script);
				script.sfxGroups.RemoveAt(script.sfxGroups.Count-1);
				EditorUtility.SetDirty(script);
			}
			GUI.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
	}
	
	string[] toolbarStrings = new string[]{"Sort By Time Added", "Sort By Group"};
	private void ShowSoundFXTitle()
	{
		int toolbarInt = script.showAsGrouped ? 1 : 0;
		EditorGUILayout.LabelField(new GUIContent("Pocket SFX", "These are SFX saved in this pocket. Each SFX has its own set of attributes that you can access by opening their drop downs. At the bottom, you can automatically set these attributes as you add SFX in the 'auto' section."), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarStrings, EditorStyles.toolbarButton, GUILayout.ExpandWidth(true));
		script.showAsGrouped = (toolbarInt == 0) ? false : true;
	}

	private void ShowSoundFXList()
	{
		var expand = '\u2261'.ToString();
		GUIContent expandContent = new GUIContent(expand, "Expand/Collapse");
		
		string[] groups = GetAvailableGroups();
		EditorGUILayout.BeginVertical();
		{
			if(!script.showAsGrouped)
			{
				EditorGUI.indentLevel++;
				for(int i = 0 ; i < script.pocketClips.Count ; i++)
				{
					if(i < script.pocketClips.Count && script.pocketClips[i] != null)
						ShowIndividualSFXAtIndex(i, groups, expandContent);
					else
						RemoveSFX(i);
				}
				EditorGUI.indentLevel--;
				EditorGUILayout.Space();
			}
			else
			{
				// by group
				EditorGUI.indentLevel++;
				for(int j = 0; j < clipsByGroup.Count; j++)
				{
					KeyValuePair<int, List<int>> pair = clipsByGroup.ElementAt(j);
					if(pair.Key >= script.sfxGroups.Count || string.IsNullOrEmpty(script.sfxGroups[pair.Key]))
						continue;
					EditorGUILayout.LabelField(script.sfxGroups[pair.Key] + ":");
					EditorGUI.indentLevel+=2;
					
					{
						for(int i = 0; i < pair.Value.Count; i++)
						{
							int index = pair.Value[i];
							if(index >= 0 && index < script.pocketClips.Count && script.pocketClips[index] != null)
								ShowIndividualSFXAtIndex(index, groups, expandContent);
							else
								RemoveSFX(index);
						}
					}
					EditorGUI.indentLevel-=2;
				}
				
				EditorGUILayout.LabelField("Not Grouped:");
				for(int i = 0; i < script.pocketClips.Count; i++)
				{
					EditorGUI.indentLevel+=2;
					if(i < script.pocketClips.Count && script.pocketClips[i] != null && !script.clipToGroupKeys.Contains(script.pocketClips[i].name))
						ShowIndividualSFXAtIndex(i, groups, expandContent);
					else if (script.pocketClips[i] == null)
						RemoveSFX(i);
					EditorGUI.indentLevel-=2;
				}
				EditorGUI.indentLevel--;
			}
			EditorGUILayout.Space();
		}
		EditorGUILayout.EndVertical();
	}
	
	private void ShowIndividualSFXAtIndex(int i, string[] groups, GUIContent expandContent)
	{
		if(i >= script.pocketClips.Count) return;
		AudioClip obj = script.pocketClips[i];
		if(obj != null)
		{
			EditorGUILayout.BeginHorizontal();
			{
				AudioClip newClip = (AudioClip)EditorGUILayout.ObjectField(obj, typeof(AudioClip), false);
				if(newClip != obj)
				{
					if(newClip == null)
					{
						RemoveSFX(i);
						return;
					}
					else
					{
						SoundManagerEditorTools.RegisterObjectChange("Change SFX", script);
						obj = newClip;
						EditorUtility.SetDirty(script);
					}
				}
				if((script.showSFXDetails[i] && GUILayout.Button("-", GUILayout.Width(30f))) || (!script.showSFXDetails[i] && GUILayout.Button(expandContent, GUILayout.Width(30f))))
				{
					script.showSFXDetails[i] = !script.showSFXDetails[i];
				}
				
				GUI.color = Color.red;
				if(GUILayout.Button("X", GUILayout.Width(20f)))
				{
					RemoveSFX(i);
					return;
				}
				GUI.color = Color.white;
			}
			EditorGUILayout.EndHorizontal();
			
			if(script.showSFXDetails[i])
			{
				EditorGUI.indentLevel+=4;
				string clipName = obj.name;
				int oldIndex = IndexOfKey(clipName);
				if(oldIndex >= 0) // if in a group, find index
				{
					int clipIndex = oldIndex;
					oldIndex = IndexOfGroup(script.clipToGroupValues[oldIndex]);
					if(oldIndex < 0) // if group no longer exists as it was, set it to none
					{
						oldIndex = 0;
						script.clipToGroupKeys.RemoveAt(clipIndex);
						script.clipToGroupValues.RemoveAt(clipIndex);
					}
				}
				if(oldIndex < 0) // if not in a group, set it to none
					oldIndex = 0;
				else //if in a group, add 1 to index to cover for None group type
					oldIndex++;

				int newIndex = EditorGUILayout.Popup("Group:", oldIndex, groups, EditorStyles.popup);
				if(oldIndex != newIndex)
				{						
					string groupName = groups[newIndex];
					ChangeGroup(clipName, oldIndex, newIndex, groupName);
					return;
				}

				int prepoolAmount = script.sfxPrePoolAmounts[i];
				
				prepoolAmount = EditorGUILayout.IntField(new GUIContent("Prepool Amount:", "Minimum amount of SFX objects for this clip waiting in the pool on standby. Good for performance."), prepoolAmount);
				if(prepoolAmount < 0)
					prepoolAmount = 0;
				if(prepoolAmount != script.sfxPrePoolAmounts[i])
				{
					SoundManagerEditorTools.RegisterObjectChange("Change Prepool Amount", script);
					script.sfxPrePoolAmounts[i] = prepoolAmount;
					EditorUtility.SetDirty(script);
				}
				
				float baseVolume = script.sfxBaseVolumes[i];
				
				baseVolume = EditorGUILayout.FloatField(new GUIContent("Base Volume:", "Base volume for this AudioClip. Lower this value if you don't want this AudioClip to play as loud. When in doubt, keep this at 1."), baseVolume);
				if(baseVolume < 0f)
					baseVolume = 0f;
				else if(baseVolume > 1f)
					baseVolume = 1f;
				if(baseVolume != script.sfxBaseVolumes[i])
				{
					SoundManagerEditorTools.RegisterObjectChange("Change Base Volume", script);
					script.sfxBaseVolumes[i] = baseVolume;
					EditorUtility.SetDirty(script);
				}
				
				float volumeVariation = script.sfxVolumeVariations[i];
				
				volumeVariation = EditorGUILayout.FloatField(new GUIContent("Volume Variation:", "Let's you vary the volume of this clip each time it's played. You get a much greater impact if you randomly vary pitch and volume between 3% and 5% each time that sound is played. So keep this value low: ~0.05"), volumeVariation);
				if(volumeVariation < 0f)
					volumeVariation = 0f;
				else if(volumeVariation > 1f)
					volumeVariation = 1f;
				if(volumeVariation != script.sfxVolumeVariations[i])
				{
					SoundManagerEditorTools.RegisterObjectChange("Change Volume Variation", script);
					script.sfxVolumeVariations[i] = volumeVariation;
					EditorUtility.SetDirty(script);
				}
				
				float pitchVariation = script.sfxPitchVariations[i];
				
				pitchVariation = EditorGUILayout.FloatField(new GUIContent("Pitch Variation:", "Let's you vary the pitch of this clip each time it's played. You get a much greater impact if you randomly vary pitch and volume between 3% and 5% each time that sound is played. So keep this value low: ~0.025"), pitchVariation);
				if(pitchVariation < 0f)
					pitchVariation = 0f;
				else if(pitchVariation > 1f)
					pitchVariation = 1f;
				if(pitchVariation != script.sfxPitchVariations[i])
				{
					SoundManagerEditorTools.RegisterObjectChange("Change Pitch Variation", script);
					script.sfxPitchVariations[i] = pitchVariation;
					EditorUtility.SetDirty(script);
				}
				EditorGUI.indentLevel-=4;
			}
		}
	}
	
	private void ShowAddSoundFX()
	{
		ShowAddSoundFXDropGUI();
		
		ShowAddSoundFXSelector();
		
		string[] groups = GetAvailableGroups();
		if(groups.Length > 1)
			script.groupAddIndex = EditorGUILayout.Popup("Auto-Add To Group:", script.groupAddIndex, groups, EditorStyles.popup, GUILayout.Width(100f), GUILayout.ExpandWidth(true));
		else 
			script.groupAddIndex = 0;
		script.autoPrepoolAmount = EditorGUILayout.IntField("Auto-Prepool Amount:", script.autoPrepoolAmount);
		if(script.autoPrepoolAmount < 0)
			script.autoPrepoolAmount = 0;
		script.autoBaseVolume = EditorGUILayout.FloatField("Auto-Base Volume:", script.autoBaseVolume);
		if(script.autoBaseVolume < 0f)
			script.autoBaseVolume = 0f;
		else if(script.autoBaseVolume > 1f)
			script.autoBaseVolume = 1f;
		script.autoVolumeVariation = EditorGUILayout.FloatField("Auto-Volume Variation:", script.autoVolumeVariation);
		if(script.autoVolumeVariation < 0f)
			script.autoVolumeVariation = 0f;
		else if(script.autoVolumeVariation > 1f)
			script.autoVolumeVariation = 1f;
		script.autoPitchVariation = EditorGUILayout.FloatField("Auto-Pitch Variation:", script.autoPitchVariation);
		if(script.autoPitchVariation < 0f)
			script.autoPitchVariation = 0f;
		else if(script.autoPitchVariation > 1f)
			script.autoPitchVariation = 1f;
	}
	
	private void ShowAddSoundFXDropGUI()
	{
		GUI.color = softGreen;
		EditorGUILayout.BeginVertical();
		{
			var evt = Event.current;
			
			var dropArea = GUILayoutUtility.GetRect(0f,50f,GUILayout.ExpandWidth(true));
			GUI.Box (dropArea, "Drag SFX(s) Here");
			
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
						
						if(AlreadyContainsSFX(aC))
							Debug.LogError("You already have that sound effect(" +aC.name+ ") attached, or a sound effect with the same name.");
						else
							AddSFX(aC, script.groupAddIndex, script.autoPrepoolAmount, script.autoBaseVolume, script.autoVolumeVariation, script.autoPitchVariation);
					}
				}
				Event.current.Use();
				break;
			}
		}
		EditorGUILayout.EndVertical();
		GUI.color = Color.white;
	}
	
	private void ShowAddSoundFXSelector()
	{
		EditorGUILayout.BeginHorizontal();
		{
			editAddSFX = EditorGUILayout.ObjectField("Select A SFX:", editAddSFX, typeof(AudioClip), false) as AudioClip;
					
			GUI.color = softGreen;
			if(GUILayout.Button("add", GUILayout.Width(40f)))
			{
				if(AlreadyContainsSFX(editAddSFX))
					Debug.LogError("You already have that sound effect(" +editAddSFX.name+ ") attached, or a sound effect with the same name.");
				else
					AddSFX(editAddSFX, script.groupAddIndex, script.autoPrepoolAmount, script.autoBaseVolume, script.autoVolumeVariation, script.autoPitchVariation);
				editAddSFX = null;
			}
			GUI.color = Color.white;
		}
		EditorGUILayout.EndHorizontal();
	}
	
	private void AddSFX(AudioClip clip, int groupIndex, int prepoolAmount, float baseVolume, float volumeVariation, float pitchVariation)
	{
		if(clip == null) 
			return;
		SoundManagerEditorTools.RegisterObjectChange("Add SFX", script);
		script.pocketClips.Add(clip);
		script.sfxPrePoolAmounts.Add(prepoolAmount);
		script.sfxBaseVolumes.Add(baseVolume);
		script.sfxVolumeVariations.Add(volumeVariation);
		script.sfxPitchVariations.Add(pitchVariation);
		script.showSFXDetails.Add(false);
		if(script.groupAddIndex > 0)
			AddToGroup(clip.name, GetAvailableGroups()[script.groupAddIndex]);
		else
		{
			EditorUtility.SetDirty(script);
		
			SceneView.RepaintAll();
		}
	}
	
	private void RemoveSFX(int index)
	{
		SoundManagerEditorTools.RegisterObjectChange("Remove SFX", script);
		string clipName = "";
		if(script.pocketClips[index] != null)
			clipName = script.pocketClips[index].name;
		script.pocketClips.RemoveAt(index);
		script.sfxPrePoolAmounts.RemoveAt(index);
		script.sfxBaseVolumes.RemoveAt(index);
		script.sfxVolumeVariations.RemoveAt(index);
		script.sfxPitchVariations.RemoveAt(index);
		script.showSFXDetails.RemoveAt(index);
		if(!string.IsNullOrEmpty(clipName))
		{
			if(IsInAGroup(clipName))
				RemoveFromGroup(clipName);
		}
		else
		{
			RemoveTracesOfIndex(index);
		}
		InitSFX();
		EditorUtility.SetDirty(script);
		
		SceneView.RepaintAll();
	}
	
	private void RemoveTracesOfIndex(int index)
	{
		string groupName = "";
		foreach(KeyValuePair<int, List<int>> pair in clipsByGroup)
		{
			if(pair.Value.Contains(index))
			{
				groupName = script.sfxGroups[pair.Key];
				pair.Value.Remove(index);
				break;
			}
		}
		
		if(!string.IsNullOrEmpty(groupName))
		{
			//if it was in a group, lets remove other traces.
			for(int i = 0; i < script.clipToGroupValues.Count; i++)
			{
				if(script.clipToGroupValues[i] == groupName && IndexOfClip(script.clipToGroupKeys[i]) == -1)
				{
					script.clipToGroupKeys.RemoveAt(i);
					script.clipToGroupValues.RemoveAt(i);
					break;
				}
			}					
		}
	}
	
	private string[] GetAvailableGroups()
	{
		List<string> result = new List<string>();
		result.Add("None");
		for( int i = 0; i < script.sfxGroups.Count; i++)
			result.Add(script.sfxGroups[i]);
		return result.ToArray();
	}
	
	private bool IsInAGroup(string clipname)
	{
		for( int i = 0; i < script.clipToGroupKeys.Count; i++)
			if(script.clipToGroupKeys[i] == clipname)
				return true;
		return false;
	}
	
	private int IndexOfKey(string clipname)
	{
		for( int i = 0; i < script.clipToGroupKeys.Count; i++)
			if(script.clipToGroupKeys[i] == clipname)
				return i;
		return -1;
	}
	
	private int IndexOfClip(string clipname)
	{
		for( int i = 0; i < script.pocketClips.Count; i++)
			if(script.pocketClips[i] != null && script.pocketClips[i].name == clipname)
				return i;
		return -1;
	}
	
	private int IndexOfGroup(string groupname)
	{
		return script.sfxGroups.IndexOf(groupname);
	}
	
	private void ChangeGroup(string clipName, int previousIndex, int nextIndex, string groupName)
	{
		SoundManagerEditorTools.RegisterObjectChange("Change Group", script);
		if(previousIndex == 0) //wasnt in group, so add it
		{
			AddToGroup(clipName, groupName);
		}
		else if (nextIndex == 0) //was in group but now doesn't want to be
		{
			RemoveFromGroup(clipName);
		}
		else //just changing groups
		{
			int index = IndexOfKey(clipName);
			int groupIndex = IndexOfGroup(groupName);
			int clipIndex = IndexOfClip(clipName);
			int oldGroupIndex = IndexOfGroup(script.clipToGroupValues[index]);
			
			script.clipToGroupValues[index] = groupName;
			
			if(clipsByGroup.ContainsKey(groupIndex))
			{
				clipsByGroup[oldGroupIndex].Remove(clipIndex);
				clipsByGroup[groupIndex].Add(clipIndex);
			}
			else
			{
				clipsByGroup.Add(groupIndex, new List<int>(){clipIndex});
			}
			
			EditorUtility.SetDirty(script);
		}
		InitSFX();
	}
	
	private void AddToGroup(string clipName, string groupName)
	{
		if(IsInAGroup(clipName) || !AlreadyContainsGroup(groupName))
			return;
		script.clipToGroupKeys.Add(clipName);
		script.clipToGroupValues.Add(groupName);
		
		int groupIndex = IndexOfGroup(groupName);
		int clipIndex = IndexOfClip(clipName);
		if(clipsByGroup.ContainsKey(groupIndex))
			clipsByGroup[groupIndex].Add(clipIndex);
		else
		{
			clipsByGroup.Add(groupIndex, new List<int>(){clipIndex});
		}
		
		EditorUtility.SetDirty(script);
		
		SceneView.RepaintAll();
	}
	
	private void RemoveFromGroup(string clipName)
	{
		if(!IsInAGroup(clipName))
			return;
		int index = IndexOfKey(clipName);
		int clipIndex = IndexOfClip(clipName);
		int groupIndex = IndexOfGroup(script.clipToGroupValues[index]);
		
		script.clipToGroupKeys.RemoveAt(index);
		script.clipToGroupValues.RemoveAt(index);
		
		if(clipsByGroup.ContainsKey(groupIndex))
			clipsByGroup[groupIndex].Remove(clipIndex);

		EditorUtility.SetDirty(script);
		
		SceneView.RepaintAll();
	}
	
	private bool AlreadyContainsSFX(AudioClip clip)
	{
		for(int i = 0 ; i < script.pocketClips.Count ; i++)
		{
			AudioClip obj = script.pocketClips[i];
		
			if(obj == null) continue;
			if(obj.name == clip.name || obj == clip)
				return true;			
		}
		return false;
	}
	
	private bool AlreadyContainsGroup(string grpName)
	{
		return script.sfxGroups.Contains(grpName);
	}
}
