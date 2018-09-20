using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using antilunchbox;

[Serializable]
/// <summary>
/// Class that contains all the data needed to bind an AudioSourceAction to an event. AudioSubscriptions are bound at run-time to keep serialization intact.
/// </summary>
public class AudioSubscription {
	/// <summary>
	/// The owner.
	/// </summary>
	public AudioSourcePro owner;
	
	/// <summary>
	/// Whether this is a standard event binding.
	/// </summary>
	public bool isStandardEvent = true;
	/// <summary>
	/// The standard event to bind to. Only used if isStandardEvent is <c>true</c>.
	/// </summary>
	public AudioSourceStandardEvent standardEvent;
	
	/// <summary>
	/// The source component if it's a custom event binding.
	/// </summary>
	public Component sourceComponent;
	/// <summary>
	/// The name of the method if it's a custom event binding.
	/// </summary>
	public string methodName = "";
	private bool isBound = false;
	
	/// <summary>
	/// The action to take when the event is triggered.
	/// </summary>
	public AudioSourceAction actionType = AudioSourceAction.None;
	/// <summary>
	/// If the action is to play a capped SFX, then this is the cap name.
	/// </summary>
	public string cappedName;
	/// <summary>
	/// Whether triggers will be filtered by layer.
	/// </summary>
	public bool filterLayers;
	/// <summary>
	/// Whether triggers will be filtered by tags.
	/// </summary>
	public bool filterTags;
	/// <summary>
	/// Whether triggers will be filtered by gameObject name.
	/// </summary>
	public bool filterNames;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public int tagMask;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public int nameMask;
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public string nameToAdd = "";
	/// <summary>
	/// Editor variable -- IGNORE AND DO NOT MODIFY
	/// </summary>
	public List<string> allNames = new List<string>();
	/// <summary>
	/// If filterLayers is <c>true</c>, only trigger for this layer mask.
	/// </summary>
	public int layerMask;
	/// <summary>
	/// If filterTags is <c>true</c>, only trigger for these tags.
	/// </summary>
	public List<string> tags = new List<string>() { "Default" };
	/// <summary>
	/// If filterNames is <c>true</c>, only trigger for these gameObject names.
	/// </summary>
	public List<string> names = new List<string>();
	
#if !(UNITY_WP8 || UNITY_METRO)
	private Component targetComponent;
#endif
	private FieldInfo eventField;
	private Delegate eventDelegate;
	private MethodInfo handlerProxy;
	private ParameterInfo[] handlerParameters;
	
	/// <summary>
	/// Binds event and action on the specified AudioSourcePro.
	/// </summary>
	/// <param name='sourcePro'>
	/// The AudioSourcePro.
	/// </param>
	public void Bind(AudioSourcePro sourcePro)
	{
		if(isBound || isStandardEvent || sourceComponent == null)
			return;
#if !(UNITY_WP8 || UNITY_METRO)		
		owner = sourcePro;

		if(!componentIsValid)
		{
			Debug.LogError(string.Format( "Invalid binding configuration - Source:{0}", sourceComponent));
			return;
		}
		
		MethodInfo eventHandlerMethodInfo = getMethodInfoForAction(actionType);
		
		targetComponent = owner;
		
		eventField = getField(sourceComponent, methodName);
		if(eventField == null)
		{
			Debug.LogError( "Event definition not found: " + sourceComponent.GetType().Name + "." + methodName );
			return;
		}
		
		try
		{
			var eventMethod = eventField.FieldType.GetMethod("Invoke");
			var eventParams = eventMethod.GetParameters();
			
			eventDelegate = createProxyEventDelegate(targetComponent, eventField.FieldType, eventParams, eventHandlerMethodInfo);
		}
		catch(Exception err)
		{
			Debug.LogError("Event binding failed - Failed to create event handler: " + err.ToString());
			return;
		}
		
		var combinedDelegate = Delegate.Combine( eventDelegate, (Delegate)eventField.GetValue( sourceComponent ) );
		eventField.SetValue( sourceComponent, combinedDelegate );
#else
		Debug.LogError("Windows Store and Windows Phone apps don't support automatic custom binding. You must do it yourself in code.");
#endif
		isBound = true;
	}
	/// <summary>
	/// Unbind this instance.
	/// </summary>
	public void Unbind()
	{
		if(!isBound)
			return;

		isBound = false;
#if !(UNITY_WP8 || UNITY_METRO)		
		var currentDelegate = (Delegate)eventField.GetValue(sourceComponent);
		var newDelegate = Delegate.Remove(currentDelegate, eventDelegate);
		eventField.SetValue(sourceComponent, newDelegate);

		eventField = null;
		eventDelegate = null;
		handlerProxy = null;

		targetComponent = null;
#endif
	}
	/// <summary>
	/// Gets a value indicating whether this <see cref="AudioSubscription"/> component is valid.
	/// </summary>
	/// <value>
	/// <c>true</c> if component is valid; otherwise, <c>false</c>.
	/// </value>
	public bool componentIsValid {
		get {
			if(standardEventIsValid)
				return true;
			
			var propertiesSet =
				sourceComponent != null &&
				!string.IsNullOrEmpty( methodName );
			
			if(!propertiesSet)
				return false;
#if !(UNITY_WP8 || UNITY_METRO)
			var member = sourceComponent.GetType().GetMember(methodName).FirstOrDefault();
			
			if(member == null)
				return false;
			
			return true;
#else
			return false;
#endif
		}
	}
	/// <summary>
	/// Gets a value indicating whether this <see cref="AudioSubscription"/> standard event is valid.
	/// </summary>
	/// <value>
	/// <c>true</c> if standard event is valid; otherwise, <c>false</c>.
	/// </value>
	public bool standardEventIsValid {
		get {
			if(isStandardEvent && Enum.IsDefined(typeof(AudioSourceStandardEvent), methodName))
				return true;
			return false;
		}
	}
#if !(UNITY_WP8 || UNITY_METRO)	
	private FieldInfo getField(Component sourceComponent, string fieldName)
	{
		return sourceComponent.GetType()
			.GetAllFieldInfos()
			.Where(f => f.Name == fieldName)
			.FirstOrDefault();
	}
#endif	
	private bool signatureIsCompatible(ParameterInfo[] lhs, ParameterInfo[] rhs)
	{
		if(lhs == null || rhs == null)
			return false;

		if(lhs.Length != rhs.Length)
			return false;

		for(int i = 0; i < lhs.Length; i++)
		{
			if(!areTypesCompatible(lhs[i], rhs[i]))
				return false;
		}

		return true;
	}

	private bool areTypesCompatible(ParameterInfo lhs, ParameterInfo rhs)
	{
		if(lhs.ParameterType.Equals(rhs.ParameterType))
			return true;
#if !(UNITY_WP8 || UNITY_METRO)		
		if(lhs.ParameterType.IsAssignableFrom(rhs.ParameterType))
			return true;
#endif
		return false;
	}
#if !(UNITY_WP8 || UNITY_METRO)	
	[ProxyEvent]
	private void CallbackProxy()
	{
		callProxyEventHandler();
	}
	
	private Delegate createProxyEventDelegate(object target, Type delegateType, ParameterInfo[] eventParams, MethodInfo eventHandler)
	{
		var proxyMethod = typeof(AudioSubscription)
			.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
			.Where(m =>
				m.IsDefined(typeof(ProxyEventAttribute), true ) &&
				signatureIsCompatible(eventParams, m.GetParameters())
			)
			.FirstOrDefault();
		
		if(proxyMethod == null)
			return null;
		
		handlerProxy = eventHandler;
		handlerParameters = eventHandler.GetParameters();

		var eventDelegate = Delegate.CreateDelegate( delegateType, this, proxyMethod, true );
		
		return eventDelegate;
	}
	
	private void callProxyEventHandler(params object[] arguments)
	{
		if(handlerProxy == null)
			return;

		if(handlerParameters.Length == 0)
			arguments = null;

		var result = new object();
		switch(actionType)
		{
		case AudioSourceAction.Play:
			result = handlerProxy.Invoke(targetComponent, new object[] {});
			break;
		case AudioSourceAction.PlayCapped:
			result = handlerProxy.Invoke(targetComponent, new object[] {cappedName});
			break;
		case AudioSourceAction.PlayLoop:
			result = handlerProxy.Invoke(targetComponent, new object[] {});
			break;
		case AudioSourceAction.Stop:
			result = handlerProxy.Invoke(targetComponent, new object[] {});
			break;
		case AudioSourceAction.None:
		default:
			break;
		}
		
		if(result is IEnumerator)
		{
			if(targetComponent is MonoBehaviour)
			{
				((MonoBehaviour)targetComponent).StartCoroutine((IEnumerator)result);
			}
		}
	}
	
	private MethodInfo getMethodInfoForAction(AudioSourceAction act)
	{
		MethodInfo methodinfo = null;
		switch(act)
		{
		case AudioSourceAction.Play:
			methodinfo = typeof(AudioSourcePro).GetMethod( "PlayHandler", 
												BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
												Type.DefaultBinder, 
												new Type[] {}, 
												null) as MethodInfo;
			break;
		case AudioSourceAction.PlayCapped:
			methodinfo = typeof(AudioSourcePro).GetMethod( "PlayCappedHandler", 
												BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
												Type.DefaultBinder, 
												new[] {typeof(string)}, 
												null) as MethodInfo;
			break;
		case AudioSourceAction.PlayLoop:
			methodinfo = typeof(AudioSourcePro).GetMethod( "PlayLoopHandler", 
												BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
												Type.DefaultBinder, 
												new Type[] {}, 
												null) as MethodInfo;
			break;
		case AudioSourceAction.Stop:
			methodinfo = typeof(AudioSourcePro).GetMethod( "StopHandler", 
												BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
												Type.DefaultBinder, 
												new Type[] {}, 
												null) as MethodInfo;
			break;
		case AudioSourceAction.None:
		default:
			break;
		}
		return methodinfo;
	}
#endif
}