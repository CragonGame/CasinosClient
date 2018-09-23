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
    public class CasinosCasinosContextWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.CasinosContext);
			Utils.BeginObjectRegister(type, L, translator, 0, 21, 70, 55);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FixedUpdate", _m_FixedUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Close", _m_Close);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Launch", _m_Launch);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRandomTips", _m_GetRandomTips);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPlatformName", _m_GetPlatformName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReportLogWithDeviceId", _m_ReportLogWithDeviceId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReportLogWithPlayerId", _m_ReportLogWithPlayerId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetNativeOperate", _m_SetNativeOperate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLuaTable", _m_GetLuaTable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearSB", _m_ClearSB);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AppendStrWithSB", _m_AppendStrWithSB);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LuaAsyncLoadLocalUiBundle", _m_LuaAsyncLoadLocalUiBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Disconnect", _m_Disconnect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayBackgroundSound", _m_PlayBackgroundSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Play", _m_Play);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BgVolumeChange", _m_BgVolumeChange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAudioSource", _m_GetAudioSource);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyAllSceneSound", _m_DestroyAllSceneSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopAllSceneSound", _m_StopAllSceneSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FreeAudioSource", _m_FreeAudioSource);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "UCenterDomain", _g_get_UCenterDomain);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UCenterAppId", _g_get_UCenterAppId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TimerShaft", _g_get_TimerShaft);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MemoryStream", _g_get_MemoryStream);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SB", _g_get_SB);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AsyncAssetLoaderMgr", _g_get_AsyncAssetLoaderMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PathMgr", _g_get_PathMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlayerPrefs", _g_get_PlayerPrefs);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Config", _g_get_Config);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Listener", _g_get_Listener);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TextureMgr", _g_get_TextureMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CasinosLua", _g_get_CasinosLua);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetBridge", _g_get_NetBridge);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NativeMgr", _g_get_NativeMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsDev", _g_get_IsDev);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Pause", _g_get_Pause);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LoadDataDone", _g_get_LoadDataDone);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ABResourcePathTitle", _g_get_ABResourcePathTitle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UiPathRoot", _g_get_UiPathRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ResourcesRowPathRoot", _g_get_ResourcesRowPathRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LoginType", _g_get_LoginType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "WeiChatIsInstalled", _g_get_WeiChatIsInstalled);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ShowWeiChat", _g_get_ShowWeiChat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ServerIsInvalid", _g_get_ServerIsInvalid);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ServerStateInfo", _g_get_ServerStateInfo);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UseHttps", _g_get_UseHttps);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UseBindPhoneAndWeiChat", _g_get_UseBindPhoneAndWeiChat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NeedHideClientUi", _g_get_NeedHideClientUi);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ClientShowFirstRecharge", _g_get_ClientShowFirstRecharge);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DesktopHSysBankShowDBValue", _g_get_DesktopHSysBankShowDBValue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanReportLog", _g_get_CanReportLog);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanReportLogDeviceId", _g_get_CanReportLogDeviceId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanReportLogPlayerId", _g_get_CanReportLogPlayerId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ShootingTextShowVIPLimit", _g_get_ShootingTextShowVIPLimit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DesktopHCanChatVIPLimit", _g_get_DesktopHCanChatVIPLimit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DesktopCanChatVIPLimit", _g_get_DesktopCanChatVIPLimit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ShowGoldTree", _g_get_ShowGoldTree);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UseWeiChatPay", _g_get_UseWeiChatPay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UseALiPay", _g_get_UseALiPay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BuglyAppId", _g_get_BuglyAppId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PinggPPAppId", _g_get_PinggPPAppId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "WeChatAppId", _g_get_WeChatAppId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "WeChatState", _g_get_WeChatState);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DataEyeId", _g_get_DataEyeId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PushAppId", _g_get_PushAppId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PushAppKey", _g_get_PushAppKey);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PushAppSecret", _g_get_PushAppSecret);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ShareSDKAppKey", _g_get_ShareSDKAppKey);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ShareSDKAppSecret", _g_get_ShareSDKAppSecret);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MainCLua", _g_get_MainCLua);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ProjectListener", _g_get_ProjectListener);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TbDataMgrLua", _g_get_TbDataMgrLua);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PreViewMgr", _g_get_PreViewMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UseLan", _g_get_UseLan);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UseDefaultLan", _g_get_UseDefaultLan);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DefaultLan", _g_get_DefaultLan);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentLan", _g_get_CurrentLan);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UnityAndroid", _g_get_UnityAndroid);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UnityIOS", _g_get_UnityIOS);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsEditor", _g_get_IsEditor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LotteryTicketFactoryName", _g_get_LotteryTicketFactoryName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AccountId", _g_get_AccountId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsSqliteUnity", _g_get_IsSqliteUnity);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CSharpLastMethodId", _g_get_CSharpLastMethodId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ViewMgr", _g_get_ViewMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ViewHelper", _g_get_ViewHelper);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UiEndWaiting", _g_get_UiEndWaiting);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ActionOnApplicationPause", _g_get_ActionOnApplicationPause);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AsyncAssetLoadGroup", _g_get_AsyncAssetLoadGroup);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NativeAPIMsgReceiverListner", _g_get_NativeAPIMsgReceiverListner);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "NativeMgr", _s_set_NativeMgr);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsDev", _s_set_IsDev);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Pause", _s_set_Pause);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LoadDataDone", _s_set_LoadDataDone);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ABResourcePathTitle", _s_set_ABResourcePathTitle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UiPathRoot", _s_set_UiPathRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ResourcesRowPathRoot", _s_set_ResourcesRowPathRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LoginType", _s_set_LoginType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "WeiChatIsInstalled", _s_set_WeiChatIsInstalled);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ShowWeiChat", _s_set_ShowWeiChat);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ServerIsInvalid", _s_set_ServerIsInvalid);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ServerStateInfo", _s_set_ServerStateInfo);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UseHttps", _s_set_UseHttps);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NeedHideClientUi", _s_set_NeedHideClientUi);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ClientShowFirstRecharge", _s_set_ClientShowFirstRecharge);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DesktopHSysBankShowDBValue", _s_set_DesktopHSysBankShowDBValue);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CanReportLog", _s_set_CanReportLog);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CanReportLogDeviceId", _s_set_CanReportLogDeviceId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CanReportLogPlayerId", _s_set_CanReportLogPlayerId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ShootingTextShowVIPLimit", _s_set_ShootingTextShowVIPLimit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DesktopHCanChatVIPLimit", _s_set_DesktopHCanChatVIPLimit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DesktopCanChatVIPLimit", _s_set_DesktopCanChatVIPLimit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ShowGoldTree", _s_set_ShowGoldTree);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UseWeiChatPay", _s_set_UseWeiChatPay);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UseALiPay", _s_set_UseALiPay);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BuglyAppId", _s_set_BuglyAppId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PinggPPAppId", _s_set_PinggPPAppId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "WeChatAppId", _s_set_WeChatAppId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "WeChatState", _s_set_WeChatState);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DataEyeId", _s_set_DataEyeId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PushAppId", _s_set_PushAppId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PushAppKey", _s_set_PushAppKey);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PushAppSecret", _s_set_PushAppSecret);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ShareSDKAppKey", _s_set_ShareSDKAppKey);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ShareSDKAppSecret", _s_set_ShareSDKAppSecret);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MainCLua", _s_set_MainCLua);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ProjectListener", _s_set_ProjectListener);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "TbDataMgrLua", _s_set_TbDataMgrLua);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PreViewMgr", _s_set_PreViewMgr);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UseLan", _s_set_UseLan);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UseDefaultLan", _s_set_UseDefaultLan);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DefaultLan", _s_set_DefaultLan);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CurrentLan", _s_set_CurrentLan);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UnityAndroid", _s_set_UnityAndroid);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UnityIOS", _s_set_UnityIOS);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsEditor", _s_set_IsEditor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LotteryTicketFactoryName", _s_set_LotteryTicketFactoryName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AccountId", _s_set_AccountId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsSqliteUnity", _s_set_IsSqliteUnity);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CSharpLastMethodId", _s_set_CSharpLastMethodId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ViewMgr", _s_set_ViewMgr);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ViewHelper", _s_set_ViewHelper);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UiEndWaiting", _s_set_UiEndWaiting);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ActionOnApplicationPause", _s_set_ActionOnApplicationPause);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NativeAPIMsgReceiverListner", _s_set_NativeAPIMsgReceiverListner);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 4, 1, 0);
			
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalDataVersionKey", Casinos.CasinosContext.LocalDataVersionKey);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PreDataVersionKey", Casinos.CasinosContext.PreDataVersionKey);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LanKey", Casinos.CasinosContext.LanKey);
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Instance", _g_get_Instance);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 8 && translator.Assignable<Casinos.CasinosListener>(L, 2) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3) && (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 5) || LuaAPI.lua_type(L, 5) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 6) || LuaAPI.lua_type(L, 6) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 7) || LuaAPI.lua_type(L, 7) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 8) || LuaAPI.lua_type(L, 8) == LuaTypes.LUA_TSTRING))
				{
					Casinos.CasinosListener _listener = (Casinos.CasinosListener)translator.GetObject(L, 2, typeof(Casinos.CasinosListener));
					bool _use_persistent = LuaAPI.lua_toboolean(L, 3);
					string _lua_project_listener_name = LuaAPI.lua_tostring(L, 4);
					string _ui_pathroot = LuaAPI.lua_tostring(L, 5);
					string _resourcesrow_pathroot = LuaAPI.lua_tostring(L, 6);
					string _ab_resource_title = LuaAPI.lua_tostring(L, 7);
					string _lotteryticket_factoryname = LuaAPI.lua_tostring(L, 8);
					
					Casinos.CasinosContext gen_ret = new Casinos.CasinosContext(_listener, _use_persistent, _lua_project_listener_name, _ui_pathroot, _resourcesrow_pathroot, _ab_resource_title, _lotteryticket_factoryname);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.CasinosContext constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _elapsed_tm = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Update( _elapsed_tm );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FixedUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.FixedUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Close(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Close(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Launch(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Launch(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRandomTips(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.GetRandomTips(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPlatformName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _to_lower = LuaAPI.lua_toboolean(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.GetPlatformName( _to_lower );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReportLogWithDeviceId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    string _message = LuaAPI.lua_tostring(L, 3);
                    string _stackTrace = LuaAPI.lua_tostring(L, 4);
                    
                    gen_to_be_invoked.ReportLogWithDeviceId( _name, _message, _stackTrace );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReportLogWithPlayerId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    string _player_id = LuaAPI.lua_tostring(L, 3);
                    string _message = LuaAPI.lua_tostring(L, 4);
                    string _stackTrace = LuaAPI.lua_tostring(L, 5);
                    
                    gen_to_be_invoked.ReportLogWithPlayerId( _name, _player_id, _message, _stackTrace );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetNativeOperate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _native_operate = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetNativeOperate( _native_operate );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLuaTable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        XLua.LuaTable gen_ret = gen_to_be_invoked.GetLuaTable( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearSB(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ClearSB(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendStrWithSB(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string[] _strs = translator.GetParams<string>(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.AppendStrWithSB( _strs );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LuaAsyncLoadLocalUiBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action _loaddone_callback = translator.GetDelegate<System.Action>(L, 2);
                    string[] _need_load_ab_path = translator.GetParams<string>(L, 3);
                    
                    gen_to_be_invoked.LuaAsyncLoadLocalUiBundle( _loaddone_callback, _need_load_ab_path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Disconnect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Disconnect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayBackgroundSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _snd_name = LuaAPI.lua_tostring(L, 2);
                    bool _is_loop = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.PlayBackgroundSound( _snd_name, _is_loop );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _snd_name = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.PlayBackgroundSound( _snd_name );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.CasinosContext.PlayBackgroundSound!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Play(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _file_name = LuaAPI.lua_tostring(L, 2);
                    Casinos._eSoundLayer _sound_layer;translator.Get(L, 3, out _sound_layer);
                    
                    gen_to_be_invoked.Play( _file_name, _sound_layer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BgVolumeChange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _volume = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.BgVolumeChange( _volume );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAudioSource(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Generic.List<UnityEngine.AudioSource> gen_ret = gen_to_be_invoked.GetAudioSource(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyAllSceneSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DestroyAllSceneSound(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopAllSceneSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StopAllSceneSound(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FreeAudioSource(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.AudioSource _audio_src = (UnityEngine.AudioSource)translator.GetObject(L, 2, typeof(UnityEngine.AudioSource));
                    
                    gen_to_be_invoked.FreeAudioSource( _audio_src );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Casinos.CasinosContext.Instance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UCenterDomain(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.UCenterDomain);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UCenterAppId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.UCenterAppId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TimerShaft(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TimerShaft);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MemoryStream(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MemoryStream);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SB(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.SB);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AsyncAssetLoaderMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.AsyncAssetLoaderMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PathMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.PathMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlayerPrefs(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.PlayerPrefs);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Config(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Config);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Listener(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Listener);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TextureMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TextureMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CasinosLua(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.CasinosLua);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetBridge(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NetBridge);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NativeMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NativeMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsDev(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsDev);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Pause(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.Pause);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LoadDataDone(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.LoadDataDone);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ABResourcePathTitle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.ABResourcePathTitle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UiPathRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.UiPathRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ResourcesRowPathRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.ResourcesRowPathRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LoginType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LoginType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_WeiChatIsInstalled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.WeiChatIsInstalled);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ShowWeiChat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.ShowWeiChat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ServerIsInvalid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.ServerIsInvalid);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ServerStateInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.ServerStateInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseHttps(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UseHttps);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseBindPhoneAndWeiChat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UseBindPhoneAndWeiChat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NeedHideClientUi(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.NeedHideClientUi);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ClientShowFirstRecharge(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.ClientShowFirstRecharge);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DesktopHSysBankShowDBValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.DesktopHSysBankShowDBValue);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanReportLog(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanReportLog);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanReportLogDeviceId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.CanReportLogDeviceId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanReportLogPlayerId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.CanReportLogPlayerId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ShootingTextShowVIPLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.ShootingTextShowVIPLimit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DesktopHCanChatVIPLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.DesktopHCanChatVIPLimit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DesktopCanChatVIPLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.DesktopCanChatVIPLimit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ShowGoldTree(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.ShowGoldTree);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseWeiChatPay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UseWeiChatPay);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseALiPay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UseALiPay);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BuglyAppId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.BuglyAppId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PinggPPAppId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.PinggPPAppId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_WeChatAppId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.WeChatAppId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_WeChatState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.WeChatState);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DataEyeId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DataEyeId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PushAppId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.PushAppId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PushAppKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.PushAppKey);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PushAppSecret(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.PushAppSecret);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ShareSDKAppKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.ShareSDKAppKey);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ShareSDKAppSecret(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.ShareSDKAppSecret);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MainCLua(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MainCLua);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ProjectListener(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ProjectListener);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TbDataMgrLua(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TbDataMgrLua);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PreViewMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.PreViewMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseLan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UseLan);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseDefaultLan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UseDefaultLan);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DefaultLan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DefaultLan);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentLan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.CurrentLan);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UnityAndroid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UnityAndroid);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UnityIOS(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UnityIOS);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsEditor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsEditor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LotteryTicketFactoryName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.LotteryTicketFactoryName);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AccountId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.AccountId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsSqliteUnity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsSqliteUnity);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CSharpLastMethodId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.CSharpLastMethodId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ViewMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ViewMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ViewHelper(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ViewHelper);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UiEndWaiting(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.UiEndWaiting);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ActionOnApplicationPause(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ActionOnApplicationPause);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AsyncAssetLoadGroup(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.AsyncAssetLoadGroup);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NativeAPIMsgReceiverListner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NativeAPIMsgReceiverListner);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NativeMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NativeMgr = (Casinos.NativeMgr)translator.GetObject(L, 2, typeof(Casinos.NativeMgr));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsDev(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsDev = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Pause(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Pause = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LoadDataDone(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LoadDataDone = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ABResourcePathTitle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ABResourcePathTitle = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UiPathRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UiPathRoot = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ResourcesRowPathRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ResourcesRowPathRoot = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LoginType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                Casinos._eLoginType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.LoginType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_WeiChatIsInstalled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.WeiChatIsInstalled = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ShowWeiChat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ShowWeiChat = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ServerIsInvalid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ServerIsInvalid = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ServerStateInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ServerStateInfo = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseHttps(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UseHttps = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NeedHideClientUi(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NeedHideClientUi = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ClientShowFirstRecharge(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ClientShowFirstRecharge = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DesktopHSysBankShowDBValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DesktopHSysBankShowDBValue = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CanReportLog(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CanReportLog = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CanReportLogDeviceId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CanReportLogDeviceId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CanReportLogPlayerId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CanReportLogPlayerId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ShootingTextShowVIPLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ShootingTextShowVIPLimit = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DesktopHCanChatVIPLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DesktopHCanChatVIPLimit = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DesktopCanChatVIPLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DesktopCanChatVIPLimit = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ShowGoldTree(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ShowGoldTree = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseWeiChatPay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UseWeiChatPay = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseALiPay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UseALiPay = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BuglyAppId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BuglyAppId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PinggPPAppId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.PinggPPAppId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_WeChatAppId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.WeChatAppId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_WeChatState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.WeChatState = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DataEyeId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DataEyeId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PushAppId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.PushAppId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PushAppKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.PushAppKey = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PushAppSecret(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.PushAppSecret = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ShareSDKAppKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ShareSDKAppKey = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ShareSDKAppSecret(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ShareSDKAppSecret = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MainCLua(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MainCLua = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ProjectListener(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ProjectListener = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_TbDataMgrLua(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.TbDataMgrLua = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PreViewMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.PreViewMgr = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseLan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UseLan = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseDefaultLan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UseDefaultLan = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DefaultLan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DefaultLan = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CurrentLan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CurrentLan = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UnityAndroid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UnityAndroid = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UnityIOS(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UnityIOS = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsEditor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsEditor = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LotteryTicketFactoryName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LotteryTicketFactoryName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AccountId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.AccountId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsSqliteUnity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsSqliteUnity = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CSharpLastMethodId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CSharpLastMethodId = (ushort)LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ViewMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ViewMgr = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ViewHelper(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ViewHelper = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UiEndWaiting(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UiEndWaiting = translator.GetDelegate<System.Action<XLua.LuaTable>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ActionOnApplicationPause(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ActionOnApplicationPause = translator.GetDelegate<System.Action<bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NativeAPIMsgReceiverListner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NativeAPIMsgReceiverListner = (Casinos.NativeAPIMsgReceiverListener)translator.GetObject(L, 2, typeof(Casinos.NativeAPIMsgReceiverListener));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
