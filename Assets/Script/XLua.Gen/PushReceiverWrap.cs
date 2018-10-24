#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class PushReceiverWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(PushReceiver);
			Utils.BeginObjectRegister(type, L, translator, 0, 8, 7, 7);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "onReceiveClientId", _m_onReceiveClientId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "onReceiveMessage", _m_onReceiveMessage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "onNotificationMessageArrived", _m_onNotificationMessageArrived);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "onNotificationMessageClicked", _m_onNotificationMessageClicked);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GeTuiSdkDidSetPushMode", _m_GeTuiSdkDidSetPushMode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GeTuiSdkDidOccurError", _m_GeTuiSdkDidOccurError);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GeTuiSDkDidNotifySdkState", _m_GeTuiSDkDidNotifySdkState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GeTuiSdkDidAliasAction", _m_GeTuiSdkDidAliasAction);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnReceiveMessage", _g_get_OnReceiveMessage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnGeTuiSDkDidNotifySdkState", _g_get_OnGeTuiSDkDidNotifySdkState);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnGeTuiSdkDidAliasAction", _g_get_OnGeTuiSdkDidAliasAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnNotificationMessageArrived", _g_get_OnNotificationMessageArrived);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnNotificationMessageClicked", _g_get_OnNotificationMessageClicked);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnGeTuiSdkDidSetPushMode", _g_get_OnGeTuiSdkDidSetPushMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnGeTuiSdkDidOccurError", _g_get_OnGeTuiSdkDidOccurError);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnReceiveMessage", _s_set_OnReceiveMessage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnGeTuiSDkDidNotifySdkState", _s_set_OnGeTuiSDkDidNotifySdkState);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnGeTuiSdkDidAliasAction", _s_set_OnGeTuiSdkDidAliasAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnNotificationMessageArrived", _s_set_OnNotificationMessageArrived);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnNotificationMessageClicked", _s_set_OnNotificationMessageClicked);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnGeTuiSdkDidSetPushMode", _s_set_OnGeTuiSdkDidSetPushMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnGeTuiSdkDidOccurError", _s_set_OnGeTuiSdkDidOccurError);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "instance", _m_instance_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					PushReceiver gen_ret = new PushReceiver();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to PushReceiver constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_instance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        PushReceiver gen_ret = PushReceiver.instance(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_onReceiveClientId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _clientId = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.onReceiveClientId( _clientId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_onReceiveMessage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _payloadJsonData = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.onReceiveMessage( _payloadJsonData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_onNotificationMessageArrived(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _msg = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.onNotificationMessageArrived( _msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_onNotificationMessageClicked(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _msg = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.onNotificationMessageClicked( _msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GeTuiSdkDidSetPushMode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _isModeOn = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.GeTuiSdkDidSetPushMode( _isModeOn );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GeTuiSdkDidOccurError(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _error = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.GeTuiSdkDidOccurError( _error );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GeTuiSDkDidNotifySdkState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _state = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.GeTuiSDkDidNotifySdkState( _state );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GeTuiSdkDidAliasAction(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _message = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.GeTuiSdkDidAliasAction( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnReceiveMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnReceiveMessage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGeTuiSDkDidNotifySdkState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnGeTuiSDkDidNotifySdkState);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGeTuiSdkDidAliasAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnGeTuiSdkDidAliasAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnNotificationMessageArrived(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnNotificationMessageArrived);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnNotificationMessageClicked(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnNotificationMessageClicked);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGeTuiSdkDidSetPushMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnGeTuiSdkDidSetPushMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGeTuiSdkDidOccurError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnGeTuiSdkDidOccurError);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnReceiveMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnReceiveMessage = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGeTuiSDkDidNotifySdkState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnGeTuiSDkDidNotifySdkState = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGeTuiSdkDidAliasAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnGeTuiSdkDidAliasAction = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnNotificationMessageArrived(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnNotificationMessageArrived = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnNotificationMessageClicked(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnNotificationMessageClicked = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGeTuiSdkDidSetPushMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnGeTuiSdkDidSetPushMode = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGeTuiSdkDidOccurError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PushReceiver gen_to_be_invoked = (PushReceiver)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnGeTuiSdkDidOccurError = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
