using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using antilunchbox;

[CustomEditor(typeof(AudioSourcePro))]
public class AudioSourceProEditor : Editor {
	private AudioSourcePro script;
	private AudioSource source;
	private AnimationCurve curve;
	private AnimationCurve panCurve;
	private AnimationCurve spreadCurve;
	private float rollOffScale = 1f;
	private bool notAvailable = false;
	private string nullEvent = "--None--";
	
	private void OnEnable()
	{
		script = target as AudioSourcePro;
		if(script.audioSource == null)
		{
			source = CheckForHiddenAudioSources();
			if(source != null)
				script.audioSource = source;
			if(script.audioSource == null)
			{
				source = script.gameObject.AddComponent<AudioSource>();
				script.audioSource = source;
			}
		}
		else
			source = script.audioSource;
		
		source.hideFlags = (HideFlags.HideInInspector | HideFlags.NotEditable);
		CalculateCurve();
	}
	
	private AudioSource CheckForHiddenAudioSources()
	{
		AudioSource[] sources = script.gameObject.GetComponents<AudioSource>();
		List<AudioSource> eligibleSources = new List<AudioSource>();
		foreach(AudioSource aSource in sources)
			if(aSource.hideFlags == (HideFlags.HideInInspector | HideFlags.NotEditable))
				eligibleSources.Add(aSource);
		
		if(eligibleSources.Count == 0)
			return null;
		
		AudioSourcePro[] pros = script.gameObject.GetComponents<AudioSourcePro>();
		
		foreach(AudioSource eligibleSource in eligibleSources)
		{
			AudioSourcePro tempPro = ArrayUtility.Find<AudioSourcePro>(pros, delegate(AudioSourcePro obj) {
				return (obj.audioSource == eligibleSource);
			});
			
			if(tempPro == null)
				return eligibleSource;
		}
		return null;
	}
	
	private void CalculateCurve()
	{
		if(source == null)
			return;
		if(source.rolloffMode == AudioRolloffMode.Logarithmic)
		{
			curve = AnimationCurve.EaseInOut(source.minDistance, (1f/(1f+rollOffScale*(source.minDistance-1f))), source.maxDistance, (1f/(1f+rollOffScale*(source.maxDistance-1f))));
		}
		else if(source.rolloffMode == AudioRolloffMode.Linear)
		{
			curve = AnimationCurve.Linear(source.minDistance, (1f/(1f+rollOffScale*(source.minDistance-1f))), source.maxDistance, (1f/(1f+rollOffScale*(source.maxDistance-1f))));
		}
		panCurve = AnimationCurve.Linear(0f,source.spatialBlend,0f,source.spatialBlend);
		spreadCurve = AnimationCurve.Linear(0f,source.spread,0f,source.spread);
	}
	
	private bool IsColliderEvent(AudioSourceStandardEvent evt)
	{
		switch(evt)
		{
		case AudioSourceStandardEvent.OnCollisionEnter:
		case AudioSourceStandardEvent.OnCollisionExit:
		case AudioSourceStandardEvent.OnTriggerEnter:
		case AudioSourceStandardEvent.OnTriggerExit:
		case AudioSourceStandardEvent.OnCollisionEnter2D:
		case AudioSourceStandardEvent.OnCollisionExit2D:
		case AudioSourceStandardEvent.OnTriggerEnter2D:
		case AudioSourceStandardEvent.OnTriggerExit2D:
		case AudioSourceStandardEvent.OnParticleCollision:
			return true;
		default:
			return false;
		}
	}
	
	public override void OnInspectorGUI()
	{
		if(source == null)
			return;
		#region ClipType handler
		ClipType clipType = script.clipType;
		clipType = (ClipType)EditorGUILayout.EnumPopup("Clip Type", clipType);
		if(clipType != script.clipType)
		{
			SoundManagerEditorTools.RegisterObjectChange("Change Clip Type", script);
			script.clipType = clipType;
			if(script.clipType != ClipType.AudioClip)
				source.clip = null;
			EditorUtility.SetDirty(script);
		}
		
		switch(script.clipType)
		{
		case ClipType.ClipFromSoundManager:
			string clipName = script.clipName;
			clipName = EditorGUILayout.TextField("Audio Clip Name", clipName);
			if(clipName != script.clipName)
			{
				SoundManagerEditorTools.RegisterObjectChange("Change Clip Name", script);
				script.clipName = clipName;
				EditorUtility.SetDirty(script);
			}
			break;
		case ClipType.ClipFromGroup:
			string groupName = script.groupName;
			groupName = EditorGUILayout.TextField("SFXGroup Name", groupName);
			if(groupName != script.groupName)
			{
				SoundManagerEditorTools.RegisterObjectChange("Change SFXGroup Name", script);
				script.groupName = groupName;
				EditorUtility.SetDirty(script);
			}
			break;
		case ClipType.AudioClip:
		default:
			AudioClip clip = source.clip;
			clip = EditorGUILayout.ObjectField("Audio Clip", clip, typeof(AudioClip),false) as AudioClip;
			if(clip != source.clip)
			{
				SoundManagerEditorTools.RegisterObjectChange("Change Audio Clip", script);
				source.clip = clip;
				EditorUtility.SetDirty(script);
			}
			if(source.clip != null)
			{
				//AudioImporter importerInfo = AudioImporter.GetAtPath(AssetDatabase.GetAssetPath(source.clip)) as AudioImporter;
				//EditorGUILayout.HelpBox("This is a " + (importerInfo.threeD ? "3D" : "2D") + " Sound.",MessageType.None, false);
			}
			break;
		}
		#endregion
		
		#region audio source settings
		EditorGUILayout.Space();
		source.mute = EditorGUILayout.Toggle("Mute", source.mute);
		source.bypassEffects = EditorGUILayout.Toggle("Bypass Effects", source.bypassEffects);
		source.playOnAwake = EditorGUILayout.Toggle("Play On Awake", source.playOnAwake);
		source.loop = EditorGUILayout.Toggle("Loop", source.loop);
		EditorGUILayout.Space();
		source.priority = EditorGUILayout.IntSlider("Priority",source.priority,0,255);
		EditorGUILayout.Space();
		source.volume = EditorGUILayout.Slider("Volume",source.volume,0f,1f);
		source.pitch = EditorGUILayout.Slider("Pitch",source.pitch,-3f,3f);
		EditorGUILayout.Space();
		
		script.ShowEditor3D = EditorGUILayout.Foldout(script.ShowEditor3D, "3D Sound Settings");
		if(script.ShowEditor3D)
		{
			EditorGUI.indentLevel++;
			{
				source.dopplerLevel = EditorGUILayout.Slider("Doppler Level",source.dopplerLevel,0f,5f);
				EditorGUILayout.Space();
				AudioRolloffMode mode = source.rolloffMode;
				mode = (AudioRolloffMode)EditorGUILayout.EnumPopup("Volume Rolloff",mode);
				if(mode != AudioRolloffMode.Custom)
				{
					source.rolloffMode = mode;
					if(GUI.changed && notAvailable)
						notAvailable = false;
				}
				else
					notAvailable = true;
				
				if(notAvailable)
				{
					GUI.color = Color.red;
					EditorGUI.indentLevel+=2;
					{
						EditorGUILayout.LabelField("Custom Volume Rolloff not available", EditorStyles.whiteLabel);
					}
					EditorGUI.indentLevel-=2;
					GUI.color = Color.white;
				}
				EditorGUI.indentLevel++;
				{
					float minD = source.minDistance;
					minD = EditorGUILayout.FloatField("Min Distance", minD);
					if(minD < 0f)
						minD = 0f;
					source.minDistance = minD;
				}
				EditorGUI.indentLevel--;
				source.spatialBlend = EditorGUILayout.Slider("Pan Level",source.spatialBlend,0f,1f);
				source.spread = EditorGUILayout.Slider("Spread",source.spread,0f,360f);
				
				float maxD = source.maxDistance;
				maxD = EditorGUILayout.FloatField("Max Distance", maxD);
				if(maxD < source.minDistance+3f)
					maxD = source.minDistance+3f;
				source.maxDistance = maxD;
				
				if(GUI.changed)
					CalculateCurve();
				
				GUI.enabled = false;
				EditorGUILayout.BeginHorizontal();
				{
					curve = EditorGUILayout.CurveField(curve, Color.red,new Rect(0f,0f,source.maxDistance,1f), GUILayout.Height(100f), GUILayout.ExpandWidth(true));
					panCurve = EditorGUILayout.CurveField(panCurve,Color.green,new Rect(0f,0f,source.maxDistance,1f), GUILayout.Height(100f), GUILayout.ExpandWidth(true));
					spreadCurve = EditorGUILayout.CurveField(spreadCurve,Color.blue,new Rect(0f,0f,source.maxDistance,360f), GUILayout.Height(100f), GUILayout.ExpandWidth(true));
				}
				EditorGUILayout.EndHorizontal();
				GUI.enabled = true;
				
				EditorGUILayout.BeginHorizontal();
				{
					GUI.color = Color.red;
					EditorGUILayout.LabelField("Volume", EditorStyles.whiteLabel, GUILayout.ExpandWidth(true));
					GUI.color = Color.green;
					EditorGUILayout.LabelField("Pan", EditorStyles.whiteLabel, GUILayout.ExpandWidth(true));
					GUI.color = Color.blue;
					EditorGUILayout.LabelField("Spread", EditorStyles.whiteLabel, GUILayout.ExpandWidth(true));
					GUI.color = Color.white;
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.Space();
		script.ShowEditor2D = EditorGUILayout.Foldout(script.ShowEditor2D, "2D Sound Settings");
		if(script.ShowEditor2D)
		{
			EditorGUI.indentLevel++;
			{
				source.panStereo = EditorGUILayout.Slider("Pan 2D",source.panStereo,-1f,1f);
				EditorGUILayout.Space();
			}
			EditorGUI.indentLevel--;
		}
		#endregion
		
		#region events
		EditorGUILayout.Space();
		script.ShowEventTriggers = EditorGUILayout.Foldout(script.ShowEventTriggers, "Event Trigger Settings");
		if(script.ShowEventTriggers)
		{
			for(int i = 0; i < script.numSubscriptions; i++)
			{
				EditorGUI.indentLevel++;
				{
					EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb, GUILayout.ExpandWidth(true));
					{
						if(script.audioSubscriptions[i].sourceComponent == null)
						{
							EditorGUILayout.HelpBox("Drag a Component from THIS GameObject Below (Optional)", MessageType.None);
						}
						
						EditorGUILayout.BeginHorizontal();
							var sourceComponent = ComponentField("Component", script.audioSubscriptions[i].sourceComponent);
							if(GUILayout.Button("Clear"))
								sourceComponent = null;
							GUI.color = Color.red;
							if(GUILayout.Button("Remove"))
							{
								RemoveEvent(i);
								return;
							}
							GUI.color = Color.white;
							if(sourceComponent != script.audioSubscriptions[i].sourceComponent)
							{
								SoundManagerEditorTools.RegisterObjectChange("Change Event Component", script);
								script.audioSubscriptions[i].sourceComponent = sourceComponent;
								EditorUtility.SetDirty(script);
							}
						EditorGUILayout.EndHorizontal();
						
						int skippedStandardEvents = 0;
			
						List<string> sourceComponentMembers = new List<string>();
						if(sourceComponent != null)
						{
							sourceComponentMembers = 
							sourceComponent.GetType()
								.GetEvents(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
								.Where(f => f.EventHandlerType.GetMethod("Invoke").GetParameters().Length == 0 && f.EventHandlerType.GetMethod("Invoke").ReturnType == typeof(void) && !IsAlreadyBound(f.Name, i))
								.Select(m => m.Name).ToList();
						}
						
						AudioSourceStandardEvent[] standardEvents = Enum.GetValues(typeof(AudioSourceStandardEvent)).Cast<AudioSourceStandardEvent>().ToArray();
						foreach(AudioSourceStandardEvent standardEvent in standardEvents)
						{
							if(!IsAlreadyBound(standardEvent.ToString(), i))
								sourceComponentMembers.Add(standardEvent.ToString());
							else
							{
								skippedStandardEvents++;
							}
						}
						sourceComponentMembers.Add(nullEvent);
						
						int numStandardEvents = standardEvents.Length-skippedStandardEvents;
						
						string[] sourceComponentMembersArray = sourceComponentMembers.ToArray();
						var memberIndex = findIndex(sourceComponentMembersArray, script.audioSubscriptions[i].methodName);
						
						if(memberIndex == -1)
						{
							memberIndex = sourceComponentMembers.Count-1;
							script.audioSubscriptions[i].methodName = "";
						}
			
						var selectedIndex = EditorGUILayout.Popup( "Compatible Events", memberIndex, sourceComponentMembersArray );
						if(selectedIndex >= 0 && selectedIndex < sourceComponentMembersArray.Length)
						{
							var memberName = sourceComponentMembersArray[selectedIndex];
							if(memberName != script.audioSubscriptions[i].methodName)
							{
								SoundManagerEditorTools.RegisterObjectChange("Change Event", script);
								script.audioSubscriptions[i].methodName = memberName;
								EditorUtility.SetDirty(script);
							}
						}
						if (selectedIndex == sourceComponentMembersArray.Length-1)
						{
							EditorGUILayout.HelpBox("No event configuration selected.", MessageType.None);
						}
						else if(selectedIndex < sourceComponentMembersArray.Length-numStandardEvents-1 && !script.audioSubscriptions[i].componentIsValid)
						{
#if !(UNITY_WP8 || UNITY_METRO)							
							EditorGUILayout.HelpBox("Configuration is invalid.", MessageType.Error);
#else
							EditorGUILayout.HelpBox("Configuration is invalid. Keep in mind that custom event configurations are not supported in the Win8Phone and WinStore platforms.", MessageType.Error);
#endif
						}
						else
						{
							if(selectedIndex+numStandardEvents < sourceComponentMembersArray.Length-1)
							{
								if(script.audioSubscriptions[i].isStandardEvent)
									script.BindStandardEvent(script.audioSubscriptions[i].standardEvent, false);
								script.audioSubscriptions[i].isStandardEvent = false;
							}
							else
							{
								script.audioSubscriptions[i].isStandardEvent = true;
								script.audioSubscriptions[i].standardEvent = (AudioSourceStandardEvent)Enum.Parse(typeof(AudioSourceStandardEvent), sourceComponentMembersArray[selectedIndex]);
								script.BindStandardEvent(script.audioSubscriptions[i].standardEvent, true);
								
								if(IsColliderEvent(script.audioSubscriptions[i].standardEvent))
								{
									EditorGUI.indentLevel+=2;
									{
										//tags
										EditorGUILayout.BeginHorizontal();
										
										bool filterTags = script.audioSubscriptions[i].filterTags;
										filterTags = EditorGUILayout.Toggle(filterTags, GUILayout.Width(40f));
										if(filterTags != script.audioSubscriptions[i].filterTags)
										{
											SoundManagerEditorTools.RegisterObjectChange("Filter Tags", script);
											script.audioSubscriptions[i].filterTags = filterTags;
											EditorUtility.SetDirty(script);
										}
										EditorGUILayout.LabelField("Filter Tags:",GUILayout.Width(110f));
										
										GUI.enabled = filterTags;
										int tagMask = script.audioSubscriptions[i].tagMask;
										tagMask = EditorGUILayout.MaskField (tagMask, UnityEditorInternal.InternalEditorUtility.tags, GUILayout.ExpandWidth(true));
										if(tagMask != script.audioSubscriptions[i].tagMask)
										{
											SoundManagerEditorTools.RegisterObjectChange("Change Tag Filter", script);
											script.audioSubscriptions[i].tagMask = tagMask;
											script.audioSubscriptions[i].tags.Clear();
											for(int t = 0; t < UnityEditorInternal.InternalEditorUtility.tags.Length; t++)
											{
												if((tagMask & 1<<t) != 0)
												{
													script.audioSubscriptions[i].tags.Add(UnityEditorInternal.InternalEditorUtility.tags[t]);
												}
											}
											EditorUtility.SetDirty(script);
										}
										GUI.enabled = true;
										
										EditorGUILayout.EndHorizontal();
										
										
										//layers
										EditorGUILayout.BeginHorizontal();
										
										bool filterLayers = script.audioSubscriptions[i].filterLayers;
										filterLayers = EditorGUILayout.Toggle(filterLayers, GUILayout.Width(40f));
										if(filterLayers != script.audioSubscriptions[i].filterLayers)
										{
											SoundManagerEditorTools.RegisterObjectChange("Filter Layers", script);
											script.audioSubscriptions[i].filterLayers = filterLayers;
											EditorUtility.SetDirty(script);
										}
										EditorGUILayout.LabelField("Filter Layers:",GUILayout.Width(110f));
										
										GUI.enabled = filterLayers;
										int layerMask = script.audioSubscriptions[i].layerMask;
										layerMask = EditorGUILayout.LayerField (layerMask, GUILayout.ExpandWidth(true));
										if(layerMask != script.audioSubscriptions[i].layerMask)
										{
											SoundManagerEditorTools.RegisterObjectChange("Change Layer Filter", script);
											script.audioSubscriptions[i].layerMask = layerMask;
											EditorUtility.SetDirty(script);
										}
										GUI.enabled = true;
										
										EditorGUILayout.EndHorizontal();
										
										
										//names
										EditorGUILayout.BeginHorizontal();
										
										bool filterNames = script.audioSubscriptions[i].filterNames;
										filterNames = EditorGUILayout.Toggle(filterNames, GUILayout.Width(40f));
										if(filterNames != script.audioSubscriptions[i].filterNames)
										{
											SoundManagerEditorTools.RegisterObjectChange("Filter Names", script);
											script.audioSubscriptions[i].filterNames = filterNames;
											EditorUtility.SetDirty(script);
										}
										
										EditorGUILayout.LabelField("Filter Names:",GUILayout.Width(110f));										
										
										GUI.enabled = filterNames;
										int nameMask = script.audioSubscriptions[i].nameMask;
										nameMask = EditorGUILayout.MaskField (nameMask, script.audioSubscriptions[i].allNames.ToArray(), GUILayout.ExpandWidth(true));
										if(nameMask != script.audioSubscriptions[i].nameMask)
										{
											SoundManagerEditorTools.RegisterObjectChange("Change Name Filter", script);
											script.audioSubscriptions[i].nameMask = nameMask;
											script.audioSubscriptions[i].names.Clear();
											for(int n = 0; n < script.audioSubscriptions[i].allNames.Count; n++)
											{
												if((nameMask & 1<<n) != 0)
												{
													script.audioSubscriptions[i].names.Add(script.audioSubscriptions[i].allNames[n]);
												}
											}
											EditorUtility.SetDirty(script);
										}
										GUI.enabled = true;
										
										EditorGUILayout.EndHorizontal();
										
										if(filterNames)
										{
											EditorGUI.indentLevel+=2;
											{
												EditorGUILayout.BeginHorizontal();
												
												script.audioSubscriptions[i].nameToAdd = EditorGUILayout.TextField(script.audioSubscriptions[i].nameToAdd);
												if(GUILayout.Button("Add Name"))
												{
													if(!string.IsNullOrEmpty(script.audioSubscriptions[i].nameToAdd) && !script.audioSubscriptions[i].names.Contains(script.audioSubscriptions[i].nameToAdd))
													{
														SoundManagerEditorTools.RegisterObjectChange("Add Name Filter", script);
														script.audioSubscriptions[i].allNames.Add(script.audioSubscriptions[i].nameToAdd);
														script.audioSubscriptions[i].nameToAdd = "";
														GUIUtility.keyboardControl = 0;
														EditorUtility.SetDirty(script);
													}
												}
												if(GUILayout.Button("Clear Names"))
												{
													SoundManagerEditorTools.RegisterObjectChange("Clear Name Filter", script);
													script.audioSubscriptions[i].allNames.Clear();
													script.audioSubscriptions[i].names.Clear();
													EditorUtility.SetDirty(script);
												}
												EditorGUILayout.EndHorizontal();
											}
											EditorGUI.indentLevel-=2;
										}
									}
									EditorGUI.indentLevel-=2;
								}
							}
							if(sourceComponent != null && sourceComponentMembersArray.Length-numStandardEvents == 1)
							{
								EditorGUILayout.HelpBox( "There are no compatible custom events on this Component. Only void parameterless events are allowed.", MessageType.None);
							}
							AudioSourceAction actionType = script.audioSubscriptions[i].actionType;
							actionType = (AudioSourceAction)EditorGUILayout.EnumPopup("AudioSource Action", actionType);
							if(actionType != script.audioSubscriptions[i].actionType)
							{
								SoundManagerEditorTools.RegisterObjectChange("Change AudioSource Action", script);
								script.audioSubscriptions[i].actionType = actionType;
								EditorUtility.SetDirty(script);
							}
							if(actionType == AudioSourceAction.PlayCapped)
							{
								string cappedName = script.audioSubscriptions[i].cappedName;
								cappedName = EditorGUILayout.TextField("Cap ID", cappedName);
								if(cappedName != script.audioSubscriptions[i].cappedName)
								{
									SoundManagerEditorTools.RegisterObjectChange("Change Cap ID", script);
									script.audioSubscriptions[i].cappedName = cappedName;
									EditorUtility.SetDirty(script);
								}
							}
						}
					}
					EditorGUILayout.EndVertical();
				}
				EditorGUI.indentLevel--;
			}
			
			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.FlexibleSpace();
				if(GUILayout.Button("Add Event Trigger"))
				{
					AddEvent();
				}
				GUILayout.FlexibleSpace();
			}
			EditorGUILayout.EndHorizontal();
		}
		#endregion
	}
	
	private bool IsAlreadyBound(string eventName, int index)
	{
		if(eventName == nullEvent)
			return false;
		
		AudioSubscription boundSubscription = script.audioSubscriptions.Find(delegate(AudioSubscription obj) {
			return ((script.audioSubscriptions.IndexOf(obj) != index) && (obj.isStandardEvent ? (Enum.IsDefined(typeof(AudioSourceStandardEvent),obj.methodName) && obj.methodName == eventName) : obj.methodName == eventName));
		});
		
		if(boundSubscription == null)
			return false;
		return true;
	}
	
	private void AddEvent()
	{
		SoundManagerEditorTools.RegisterObjectChange("Add Event", script);
		script.numSubscriptions++;
		script.audioSubscriptions.Add(new AudioSubscription());
		if(script.numSubscriptions == 1)
			script.audioSubscriptions[script.audioSubscriptions.Count-1].sourceComponent = null;
		else
			script.audioSubscriptions[script.audioSubscriptions.Count-1].sourceComponent = script.audioSubscriptions[script.audioSubscriptions.Count-2].sourceComponent;
		EditorUtility.SetDirty(script);
	}
	
	private void RemoveEvent(int index)
	{
		if(index >= script.numSubscriptions || index < 0)
			return;
		SoundManagerEditorTools.RegisterObjectChange("Remove Event", script);
		script.numSubscriptions--;
		script.audioSubscriptions.RemoveAt(index);
		EditorUtility.SetDirty(script);
	}
	
	private Component ComponentField(string label, Component value, Type componentType = null)
	{
		componentType = componentType ?? typeof(MonoBehaviour);

		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField(label, "", GUILayout.Width(90));

			GUILayout.Space(5);

			var displayText = value == null ? "[none] - Drag Component Here" : value.ToString();
			GUILayout.Label(displayText, "TextField", GUILayout.ExpandWidth(true), GUILayout.MinWidth(100));

			var evt = Event.current;
			if(evt != null)
			{
				var textRect = GUILayoutUtility.GetLastRect();
				if(evt.type == EventType.MouseDown && evt.clickCount == 2)
				{
					if(textRect.Contains(evt.mousePosition))
					{
						if(GUI.enabled && value != null)
						{
							Selection.activeObject = value;
							EditorGUIUtility.PingObject( value );
							GUIUtility.hotControl = value.GetInstanceID();
						}
					}
				}
				else if(evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform)
				{
					if(textRect.Contains(evt.mousePosition))
					{

						var reference = DragAndDrop.objectReferences.First();
						var draggedComponent = (Component)null;
						if(reference is Transform)
						{
							draggedComponent = (Transform)reference;
						}
						else if(reference is GameObject)
						{
							draggedComponent =
								( (GameObject)reference )
								.GetComponents( componentType )
								.FirstOrDefault();
						}
						else if(reference is Component)
						{
							draggedComponent = reference as Component;
							if(draggedComponent == null)
							{
								draggedComponent =
									((Component)reference)
									.GetComponents(componentType)
									.FirstOrDefault();
							}
						}

						DragAndDrop.visualMode = (draggedComponent == null) ? DragAndDropVisualMode.None : DragAndDropVisualMode.Copy;

						if(evt.type == EventType.DragPerform)
						{
							value = draggedComponent;
						}
						
						evt.Use();
					}
				}
			}
		}
		EditorGUILayout.EndHorizontal();

		GUILayout.Space(3);

		return value;
	}

	private int findIndex(string[] list, string methodname)
	{
		if(string.IsNullOrEmpty(methodname))
			return -1;
		for(int i = 0; i < list.Length; i++)
		{
			if(list[i] == methodname)
				return i;
		}

		return -1;
	}
}