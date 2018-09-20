using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class SoundManagerEditor : Editor {
	#region editor variables
	private AudioClip editAddSFX;
	private string groupToAdd = "";
	private Dictionary<int, List<int>> clipsByGroup = new Dictionary<int, List<int>>();
	private bool debugDict = false;
	#endregion
	
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
		QueryDict("InitSFX");
	}
	
	private void ShowSoundFX()
	{
		ShowSoundFXGroupsTitle();
		
		ShowSoundFXGroupsList();
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		ShowSoundFXTitle();
		
		ShowSoundFXList();
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		ShowAddSoundFX();
	}
	
	string[] toolbarStrings = new string[]{"Sort By Time Added", "Sort By Group"};
	private void ShowSoundFXTitle()
	{
		int toolbarInt = script.showAsGrouped ? 1 : 0;
		EditorGUILayout.LabelField(new GUIContent("Stored SFX", "These are SFX saved on the SoundManager. Each SFX has its own set of attributes that you can access by opening their drop downs. At the bottom, you can automatically set these attributes as you add SFX in the 'auto' section."), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarStrings, EditorStyles.toolbarButton, GUILayout.ExpandWidth(true));
		script.showAsGrouped = (toolbarInt == 0) ? false : true;
	}
	
	private void ShowSoundFXGroupsTitle()
	{
		EditorGUILayout.LabelField(new GUIContent("SFX Groups", "SFX Groups let you randomly load a clip from them and they can have their own personalized SFX Cap Amount for capped sounds. Click the '?' to know more."), EditorStyles.boldLabel);
	}

	private void ShowSoundFXGroupsList()
	{
		EditorGUILayout.BeginVertical();
		{
			EditorGUI.indentLevel++;
			if(script.helpOn)
				EditorGUILayout.HelpBox("SFX Groups are used to:\n1. Access random clips in a set.\n2. Apply specific cap amounts to clips when using SoundManager.PlayCappedSFX." +
					"\n\n-Setting the cap amount to -1 will make a group use the default SFX Cap Amount\n\n-Setting the cap amount to 0 will make a group not use a cap at all.",MessageType.Info);
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.LabelField("Group Name:", GUILayout.ExpandWidth(true));
				EditorGUILayout.LabelField("Cap:", GUILayout.Width(40f));
				bool helpOn = script.helpOn;
				helpOn = GUILayout.Toggle(helpOn, "?", GUI.skin.button, GUILayout.Width(35f));
				if(helpOn != script.helpOn)
				{
					SoundManagerEditorTools.RegisterObjectChange("Change SFXGroup Help", script);
					script.helpOn = helpOn;
					EditorUtility.SetDirty(script);
				}
			}
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.LabelField("", GUILayout.Width(10f));
				GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
			}
			EditorGUILayout.EndHorizontal();
			
			for(int i = 0 ; i < script.sfxGroups.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				{
					SFXGroup grp = script.sfxGroups[i];
					
					if(grp != null)
					{					
						EditorGUILayout.LabelField(grp.groupName, GUILayout.ExpandWidth(true));
						int specificCapAmount = grp.specificCapAmount;
						bool isAuto = false, isNo = false;
						
						if(specificCapAmount == -1)
							isAuto = true;
						else if(specificCapAmount == 0)
							isNo = true;
						
						bool newAuto = GUILayout.Toggle(isAuto, "Auto Cap", GUI.skin.button, GUILayout.ExpandWidth(false));
						bool newNo = GUILayout.Toggle(isNo, "No Cap", GUI.skin.button, GUILayout.ExpandWidth(false));
						
						if(newAuto != isAuto && newAuto)
						{
							specificCapAmount = -1;
						}
						if(newNo != isNo && newNo)
						{
							specificCapAmount = 0;
						}
						
						specificCapAmount = EditorGUILayout.IntField(specificCapAmount, GUILayout.Width(40f));
						if(specificCapAmount < -1) specificCapAmount = -1;
						
						if(specificCapAmount != grp.specificCapAmount)
						{
							SoundManagerEditorTools.RegisterObjectChange("Change Group Cap", script);
							grp.specificCapAmount = specificCapAmount;
							EditorUtility.SetDirty(script);
						}

						EditorGUILayout.LabelField("", GUILayout.Width(10f));
						GUI.color = Color.red;
						if(GUILayout.Button("X", GUILayout.Width(20f)))
						{
							RemoveGroup(i);
							return;
						}
						GUI.color = Color.white;
					}
				}
				EditorGUILayout.EndHorizontal();				
			}
			ShowAddGroup();
			EditorGUI.indentLevel--;
			EditorGUILayout.Space();
		}
		EditorGUILayout.EndVertical();
	}
	
	private void ShowAddGroup()
	{
		EditorGUI.indentLevel += 2;
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Add a Group:");
			groupToAdd = EditorGUILayout.TextField(groupToAdd, GUILayout.ExpandWidth(true));
			GUI.color = softGreen;
			if(GUILayout.Button("add", GUILayout.Width(40f)))
			{
				AddGroup(groupToAdd, -1);
				groupToAdd = "";
				GUIUtility.keyboardControl = 0;
			}
			GUI.color = Color.white;
		}
		EditorGUILayout.EndHorizontal();
		EditorGUI.indentLevel -= 2;
	}
	
	private void QueryDict(string comingfrom)
	{
		if(!debugDict) return;
		string result = comingfrom + "\n";
		
		for(int i = 0; i < clipsByGroup.Count; i++)
		{
			KeyValuePair<int, List<int>> pair = clipsByGroup.ElementAt(i);
			result += pair.Key + ":";
			if(script.sfxGroups.Count > pair.Key)
				result += script.sfxGroups[pair.Key].groupName;
			else
				result += "ERR";
			result += "\n";
			for(int j = 0; j < pair.Value.Count; j++)
			{
				result += "    ";
				result += pair.Value[j] + ":";
				if(script.storedSFXs.Count > pair.Value[j])
					result += script.storedSFXs[pair.Value[j]].name + "\n";
				else
					result += "ERR";
			}
		}
		Debug.Log(result);
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
				for(int i = 0 ; i < script.storedSFXs.Count ; i++)
				{
					if(i < script.storedSFXs.Count && script.storedSFXs[i] != null)
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
					if(pair.Key >= script.sfxGroups.Count || script.sfxGroups[pair.Key] == null)
						continue;
					EditorGUILayout.LabelField(script.sfxGroups[pair.Key].groupName + ":");
					EditorGUI.indentLevel+=2;
					
					{
						for(int i = 0; i < pair.Value.Count; i++)
						{
							int index = pair.Value[i];
							if(index >= 0 && index < script.storedSFXs.Count && script.storedSFXs[index] != null)
								ShowIndividualSFXAtIndex(index, groups, expandContent);
							else
								RemoveSFX(index);
						}
					}
					EditorGUI.indentLevel-=2;
				}
				
				EditorGUILayout.LabelField("Not Grouped:");
				for(int i = 0; i < script.storedSFXs.Count; i++)
				{
					EditorGUI.indentLevel+=2;
					if(i < script.storedSFXs.Count && script.storedSFXs[i] != null && !script.clipToGroupKeys.Contains(script.storedSFXs[i].name))
						ShowIndividualSFXAtIndex(i, groups, expandContent);
					else if (script.storedSFXs[i] == null)
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
		if(i >= script.storedSFXs.Count) return;
		AudioClip obj = script.storedSFXs[i];
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
					oldIndex = IndexOfGroup(script.clipToGroupValues[oldIndex]);
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
		script.storedSFXs.Add(clip);
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
		QueryDict("AddSFX");
	}
	
	private void RemoveSFX(int index)
	{
		SoundManagerEditorTools.RegisterObjectChange("Remove SFX", script);
		string clipName = "";
		if(script.storedSFXs[index] != null)
			clipName = script.storedSFXs[index].name;
		script.storedSFXs.RemoveAt(index);
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
		QueryDict("RemoveSFX");
	}
	
	private void RemoveTracesOfIndex(int index)
	{
		string groupName = "";
		foreach(KeyValuePair<int, List<int>> pair in clipsByGroup)
		{
			if(pair.Value.Contains(index))
			{
				groupName = script.sfxGroups[pair.Key].groupName;
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
	
	private void AddGroup(string groupName, int capAmount)
	{
		if(AlreadyContainsGroup(groupName))
			return;
		
		SoundManagerEditorTools.RegisterObjectChange("Add Group", script);
		script.sfxGroups.Add(new SFXGroup(groupName, capAmount));
		if(!clipsByGroup.ContainsKey(script.sfxGroups.Count-1))
		{
			clipsByGroup.Add(script.sfxGroups.Count-1, new List<int>());
		}

		EditorUtility.SetDirty(script);
		
		SceneView.RepaintAll();
		QueryDict("AddGroup");
	}
	
	private void RemoveGroup(int index)
	{
		SoundManagerEditorTools.RegisterObjectChange("Remove Group", script);
		string groupName = script.sfxGroups[index].groupName;
		RemoveAllInGroup(groupName);
		script.sfxGroups.RemoveAt(index);
		if(clipsByGroup.ContainsKey(index))
			clipsByGroup.Remove(index);
		InitSFX();
		EditorUtility.SetDirty(script);
		SceneView.RepaintAll();
		QueryDict("RemoveGroup");
	}
	
	private void RemoveAllInGroup(string groupName)
	{
		for( int i = script.clipToGroupKeys.Count-1; i >= 0; i--)
		{
			if(script.clipToGroupValues[i] == groupName)
				RemoveFromGroup(script.clipToGroupKeys[i]);
		}
	}
	
	private string[] GetAvailableGroups()
	{
		List<string> result = new List<string>();
		result.Add("None");
		for( int i = 0; i < script.sfxGroups.Count; i++)
			result.Add(script.sfxGroups[i].groupName);
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
		for( int i = 0; i < script.storedSFXs.Count; i++)
			if(script.storedSFXs[i] != null && script.storedSFXs[i].name == clipname)
				return i;
		return -1;
	}
	
	private int IndexOfGroup(string groupname)
	{
		for( int i = 0; i < script.sfxGroups.Count; i++)
			if(script.sfxGroups[i].groupName == groupname)
				return i;
		return -1;
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
		QueryDict("ChangeGroup");
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
		QueryDict("AddToGroup");
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
		QueryDict("RemoveFromGroup");
	}
	
	private bool AlreadyContainsSFX(AudioClip clip)
	{
		for(int i = 0 ; i < script.storedSFXs.Count ; i++)
		{
			AudioClip obj = script.storedSFXs[i];
		
			if(obj == null) continue;
			if(obj.name == clip.name || obj == clip)
				return true;			
		}
		return false;
	}
	
	private bool AlreadyContainsGroup(string grpName)
	{
		for(int i = 0 ; i < script.sfxGroups.Count ; i++)
		{
			SFXGroup grp = script.sfxGroups[i];
		
			if(grp == null) continue;
			if(grp.groupName == grpName)
				return true;
		}
		return false;
	}
}
