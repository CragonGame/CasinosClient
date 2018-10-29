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
    public class cnsharesdkunity3dShareSDKWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(cn.sharesdk.unity3d.ShareSDK);
			Utils.BeginObjectRegister(type, L, translator, 0, 20, 9, 9);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnError", _m_OnError);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnComplete", _m_OnComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnCancel", _m_OnCancel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitSDK", _m_InitSDK);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPlatformConfig", _m_SetPlatformConfig);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Authorize", _m_Authorize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CancelAuthorize", _m_CancelAuthorize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsAuthorized", _m_IsAuthorized);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsClientValid", _m_IsClientValid);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUserInfo", _m_GetUserInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShareContent", _m_ShareContent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowPlatformList", _m_ShowPlatformList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowShareContentEditor", _m_ShowShareContentEditor);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShareWithContentName", _m_ShareWithContentName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowPlatformListWithContentName", _m_ShowPlatformListWithContentName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowShareContentEditorWithContentName", _m_ShowShareContentEditorWithContentName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetFriendList", _m_GetFriendList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddFriend", _m_AddFriend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAuthInfo", _m_GetAuthInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisableSSO", _m_DisableSSO);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "appKey", _g_get_appKey);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "appSecret", _g_get_appSecret);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "devInfo", _g_get_devInfo);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "shareSDKUtils", _g_get_shareSDKUtils);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "authHandler", _g_get_authHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "shareHandler", _g_get_shareHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "showUserHandler", _g_get_showUserHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "getFriendsHandler", _g_get_getFriendsHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "followFriendHandler", _g_get_followFriendHandler);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "appKey", _s_set_appKey);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "appSecret", _s_set_appSecret);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "devInfo", _s_set_devInfo);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "shareSDKUtils", _s_set_shareSDKUtils);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "authHandler", _s_set_authHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "shareHandler", _s_set_shareHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "showUserHandler", _s_set_showUserHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "getFriendsHandler", _s_set_getFriendsHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "followFriendHandler", _s_set_followFriendHandler);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					cn.sharesdk.unity3d.ShareSDK gen_ret = new cn.sharesdk.unity3d.ShareSDK();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to cn.sharesdk.unity3d.ShareSDK constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnError(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _reqID = LuaAPI.xlua_tointeger(L, 2);
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 3, out _platform);
                    int _action = LuaAPI.xlua_tointeger(L, 4);
                    System.Collections.Hashtable _throwable = (System.Collections.Hashtable)translator.GetObject(L, 5, typeof(System.Collections.Hashtable));
                    
                    gen_to_be_invoked.OnError( _reqID, _platform, _action, _throwable );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnComplete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _reqID = LuaAPI.xlua_tointeger(L, 2);
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 3, out _platform);
                    int _action = LuaAPI.xlua_tointeger(L, 4);
                    System.Collections.Hashtable _res = (System.Collections.Hashtable)translator.GetObject(L, 5, typeof(System.Collections.Hashtable));
                    
                    gen_to_be_invoked.OnComplete( _reqID, _platform, _action, _res );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnCancel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _reqID = LuaAPI.xlua_tointeger(L, 2);
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 3, out _platform);
                    int _action = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.OnCancel( _reqID, _platform, _action );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitSDK(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _appKey = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.InitSDK( _appKey );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _appKey = LuaAPI.lua_tostring(L, 2);
                    string _appSecret = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.InitSDK( _appKey, _appSecret );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to cn.sharesdk.unity3d.ShareSDK.InitSDK!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPlatformConfig(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Hashtable _configInfo = (System.Collections.Hashtable)translator.GetObject(L, 2, typeof(System.Collections.Hashtable));
                    
                    gen_to_be_invoked.SetPlatformConfig( _configInfo );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Authorize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    
                        int gen_ret = gen_to_be_invoked.Authorize( _platform );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CancelAuthorize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    
                    gen_to_be_invoked.CancelAuthorize( _platform );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsAuthorized(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    
                        bool gen_ret = gen_to_be_invoked.IsAuthorized( _platform );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsClientValid(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    
                        bool gen_ret = gen_to_be_invoked.IsClientValid( _platform );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUserInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    
                        int gen_ret = gen_to_be_invoked.GetUserInfo( _platform );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShareContent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<cn.sharesdk.unity3d.PlatformType>(L, 2)&& translator.Assignable<cn.sharesdk.unity3d.ShareContent>(L, 3)) 
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    cn.sharesdk.unity3d.ShareContent _content = (cn.sharesdk.unity3d.ShareContent)translator.GetObject(L, 3, typeof(cn.sharesdk.unity3d.ShareContent));
                    
                        int gen_ret = gen_to_be_invoked.ShareContent( _platform, _content );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<cn.sharesdk.unity3d.PlatformType[]>(L, 2)&& translator.Assignable<cn.sharesdk.unity3d.ShareContent>(L, 3)) 
                {
                    cn.sharesdk.unity3d.PlatformType[] _platforms = (cn.sharesdk.unity3d.PlatformType[])translator.GetObject(L, 2, typeof(cn.sharesdk.unity3d.PlatformType[]));
                    cn.sharesdk.unity3d.ShareContent _content = (cn.sharesdk.unity3d.ShareContent)translator.GetObject(L, 3, typeof(cn.sharesdk.unity3d.ShareContent));
                    
                        int gen_ret = gen_to_be_invoked.ShareContent( _platforms, _content );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to cn.sharesdk.unity3d.ShareSDK.ShareContent!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowPlatformList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType[] _platforms = (cn.sharesdk.unity3d.PlatformType[])translator.GetObject(L, 2, typeof(cn.sharesdk.unity3d.PlatformType[]));
                    cn.sharesdk.unity3d.ShareContent _content = (cn.sharesdk.unity3d.ShareContent)translator.GetObject(L, 3, typeof(cn.sharesdk.unity3d.ShareContent));
                    int _x = LuaAPI.xlua_tointeger(L, 4);
                    int _y = LuaAPI.xlua_tointeger(L, 5);
                    
                        int gen_ret = gen_to_be_invoked.ShowPlatformList( _platforms, _content, _x, _y );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowShareContentEditor(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    cn.sharesdk.unity3d.ShareContent _content = (cn.sharesdk.unity3d.ShareContent)translator.GetObject(L, 3, typeof(cn.sharesdk.unity3d.ShareContent));
                    
                        int gen_ret = gen_to_be_invoked.ShowShareContentEditor( _platform, _content );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShareWithContentName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    string _contentName = LuaAPI.lua_tostring(L, 3);
                    System.Collections.Hashtable _customFields = (System.Collections.Hashtable)translator.GetObject(L, 4, typeof(System.Collections.Hashtable));
                    
                        int gen_ret = gen_to_be_invoked.ShareWithContentName( _platform, _contentName, _customFields );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowPlatformListWithContentName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _contentName = LuaAPI.lua_tostring(L, 2);
                    System.Collections.Hashtable _customFields = (System.Collections.Hashtable)translator.GetObject(L, 3, typeof(System.Collections.Hashtable));
                    cn.sharesdk.unity3d.PlatformType[] _platforms = (cn.sharesdk.unity3d.PlatformType[])translator.GetObject(L, 4, typeof(cn.sharesdk.unity3d.PlatformType[]));
                    int _x = LuaAPI.xlua_tointeger(L, 5);
                    int _y = LuaAPI.xlua_tointeger(L, 6);
                    
                        int gen_ret = gen_to_be_invoked.ShowPlatformListWithContentName( _contentName, _customFields, _platforms, _x, _y );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowShareContentEditorWithContentName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    string _contentName = LuaAPI.lua_tostring(L, 3);
                    System.Collections.Hashtable _customFields = (System.Collections.Hashtable)translator.GetObject(L, 4, typeof(System.Collections.Hashtable));
                    
                        int gen_ret = gen_to_be_invoked.ShowShareContentEditorWithContentName( _platform, _contentName, _customFields );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFriendList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    int _page = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.GetFriendList( _platform, _count, _page );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddFriend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    string _account = LuaAPI.lua_tostring(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.AddFriend( _platform, _account );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAuthInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    
                        System.Collections.Hashtable gen_ret = gen_to_be_invoked.GetAuthInfo( _platform );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisableSSO(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _open = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.DisableSSO( _open );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_appKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.appKey);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_appSecret(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.appSecret);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_devInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.devInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_shareSDKUtils(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.shareSDKUtils);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_authHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.authHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_shareHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.shareHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_showUserHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.showUserHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_getFriendsHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.getFriendsHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_followFriendHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.followFriendHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_appKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.appKey = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_appSecret(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.appSecret = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_devInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.devInfo = (cn.sharesdk.unity3d.DevInfoSet)translator.GetObject(L, 2, typeof(cn.sharesdk.unity3d.DevInfoSet));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_shareSDKUtils(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.shareSDKUtils = (cn.sharesdk.unity3d.ShareSDKImpl)translator.GetObject(L, 2, typeof(cn.sharesdk.unity3d.ShareSDKImpl));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_authHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.authHandler = translator.GetDelegate<cn.sharesdk.unity3d.ShareSDK.EventHandler>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_shareHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.shareHandler = translator.GetDelegate<cn.sharesdk.unity3d.ShareSDK.EventHandler>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_showUserHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.showUserHandler = translator.GetDelegate<cn.sharesdk.unity3d.ShareSDK.EventHandler>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_getFriendsHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.getFriendsHandler = translator.GetDelegate<cn.sharesdk.unity3d.ShareSDK.EventHandler>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_followFriendHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                cn.sharesdk.unity3d.ShareSDK gen_to_be_invoked = (cn.sharesdk.unity3d.ShareSDK)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.followFriendHandler = translator.GetDelegate<cn.sharesdk.unity3d.ShareSDK.EventHandler>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
