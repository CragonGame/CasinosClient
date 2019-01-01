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
    public class UniWebViewWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UniWebView);
			Utils.BeginObjectRegister(type, L, translator, 0, 47, 7, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateFrame", _m_UpdateFrame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Load", _m_Load);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadHTMLString", _m_LoadHTMLString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Reload", _m_Reload);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Stop", _m_Stop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GoBack", _m_GoBack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GoForward", _m_GoForward);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetOpenLinksInExternalBrowser", _m_SetOpenLinksInExternalBrowser);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Show", _m_Show);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Hide", _m_Hide);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AnimateTo", _m_AnimateTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddJavaScript", _m_AddJavaScript);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EvaluateJavaScript", _m_EvaluateJavaScript);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddUrlScheme", _m_AddUrlScheme);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveUrlScheme", _m_RemoveUrlScheme);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddSslExceptionDomain", _m_AddSslExceptionDomain);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveSslExceptionDomain", _m_RemoveSslExceptionDomain);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetHeaderField", _m_SetHeaderField);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUserAgent", _m_SetUserAgent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUserAgent", _m_GetUserAgent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CleanCache", _m_CleanCache);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetShowSpinnerWhileLoading", _m_SetShowSpinnerWhileLoading);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpinnerText", _m_SetSpinnerText);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetHorizontalScrollBarEnabled", _m_SetHorizontalScrollBarEnabled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetVerticalScrollBarEnabled", _m_SetVerticalScrollBarEnabled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetBouncesEnabled", _m_SetBouncesEnabled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetZoomEnabled", _m_SetZoomEnabled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddPermissionTrustDomain", _m_AddPermissionTrustDomain);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemovePermissionTrustDomain", _m_RemovePermissionTrustDomain);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetBackButtonEnabled", _m_SetBackButtonEnabled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUseWideViewPort", _m_SetUseWideViewPort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLoadWithOverviewMode", _m_SetLoadWithOverviewMode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetImmersiveModeEnabled", _m_SetImmersiveModeEnabled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetShowToolbar", _m_SetShowToolbar);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetToolbarDoneButtonText", _m_SetToolbarDoneButtonText);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetWindowUserResizeEnabled", _m_SetWindowUserResizeEnabled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetHTMLContent", _m_GetHTMLContent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAllowFileAccessFromFileURLs", _m_SetAllowFileAccessFromFileURLs);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Print", _m_Print);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPageStarted", _e_OnPageStarted);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPageFinished", _e_OnPageFinished);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPageErrorReceived", _e_OnPageErrorReceived);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnMessageReceived", _e_OnMessageReceived);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnShouldClose", _e_OnShouldClose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnKeyCodeReceived", _e_OnKeyCodeReceived);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnOrientationChanged", _e_OnOrientationChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnWebContentProcessTerminated", _e_OnWebContentProcessTerminated);
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Frame", _g_get_Frame);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ReferenceRectTransform", _g_get_ReferenceRectTransform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Url", _g_get_Url);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanGoBack", _g_get_CanGoBack);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanGoForward", _g_get_CanGoForward);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BackgroundColor", _g_get_BackgroundColor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Alpha", _g_get_Alpha);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Frame", _s_set_Frame);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ReferenceRectTransform", _s_set_ReferenceRectTransform);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BackgroundColor", _s_set_BackgroundColor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Alpha", _s_set_Alpha);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 10, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAllowAutoPlay", _m_SetAllowAutoPlay_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAllowInlinePlay", _m_SetAllowInlinePlay_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetJavaScriptEnabled", _m_SetJavaScriptEnabled_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAllowJavaScriptOpenWindow", _m_SetAllowJavaScriptOpenWindow_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearCookies", _m_ClearCookies_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCookie", _m_SetCookie_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCookie", _m_GetCookie_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearHttpAuthUsernamePassword", _m_ClearHttpAuthUsernamePassword_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetWebContentsDebuggingEnabled", _m_SetWebContentsDebuggingEnabled_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UniWebView gen_ret = new UniWebView();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateFrame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpdateFrame(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Load(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    bool _skipEncoding = LuaAPI.lua_toboolean(L, 3);
                    string _readAccessURL = LuaAPI.lua_tostring(L, 4);
                    
                    gen_to_be_invoked.Load( _url, _skipEncoding, _readAccessURL );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    bool _skipEncoding = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.Load( _url, _skipEncoding );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.Load( _url );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.Load!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadHTMLString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _htmlString = LuaAPI.lua_tostring(L, 2);
                    string _baseUrl = LuaAPI.lua_tostring(L, 3);
                    bool _skipEncoding = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.LoadHTMLString( _htmlString, _baseUrl, _skipEncoding );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _htmlString = LuaAPI.lua_tostring(L, 2);
                    string _baseUrl = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.LoadHTMLString( _htmlString, _baseUrl );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.LoadHTMLString!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Reload(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Reload(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Stop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Stop(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GoBack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.GoBack(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GoForward(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.GoForward(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetOpenLinksInExternalBrowser(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _flag = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetOpenLinksInExternalBrowser( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Show(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniWebViewTransitionEdge>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action>(L, 5)) 
                {
                    bool _fade = LuaAPI.lua_toboolean(L, 2);
                    UniWebViewTransitionEdge _edge;translator.Get(L, 3, out _edge);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action _completionHandler = translator.GetDelegate<System.Action>(L, 5);
                    
                        bool gen_ret = gen_to_be_invoked.Show( _fade, _edge, _duration, _completionHandler );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniWebViewTransitionEdge>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    bool _fade = LuaAPI.lua_toboolean(L, 2);
                    UniWebViewTransitionEdge _edge;translator.Get(L, 3, out _edge);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        bool gen_ret = gen_to_be_invoked.Show( _fade, _edge, _duration );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniWebViewTransitionEdge>(L, 3)) 
                {
                    bool _fade = LuaAPI.lua_toboolean(L, 2);
                    UniWebViewTransitionEdge _edge;translator.Get(L, 3, out _edge);
                    
                        bool gen_ret = gen_to_be_invoked.Show( _fade, _edge );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _fade = LuaAPI.lua_toboolean(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.Show( _fade );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        bool gen_ret = gen_to_be_invoked.Show(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.Show!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Hide(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniWebViewTransitionEdge>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action>(L, 5)) 
                {
                    bool _fade = LuaAPI.lua_toboolean(L, 2);
                    UniWebViewTransitionEdge _edge;translator.Get(L, 3, out _edge);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action _completionHandler = translator.GetDelegate<System.Action>(L, 5);
                    
                        bool gen_ret = gen_to_be_invoked.Hide( _fade, _edge, _duration, _completionHandler );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniWebViewTransitionEdge>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    bool _fade = LuaAPI.lua_toboolean(L, 2);
                    UniWebViewTransitionEdge _edge;translator.Get(L, 3, out _edge);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        bool gen_ret = gen_to_be_invoked.Hide( _fade, _edge, _duration );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<UniWebViewTransitionEdge>(L, 3)) 
                {
                    bool _fade = LuaAPI.lua_toboolean(L, 2);
                    UniWebViewTransitionEdge _edge;translator.Get(L, 3, out _edge);
                    
                        bool gen_ret = gen_to_be_invoked.Hide( _fade, _edge );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _fade = LuaAPI.lua_toboolean(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.Hide( _fade );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        bool gen_ret = gen_to_be_invoked.Hide(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.Hide!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AnimateTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Rect>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action>(L, 5)) 
                {
                    UnityEngine.Rect _frame;translator.Get(L, 2, out _frame);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    float _delay = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action _completionHandler = translator.GetDelegate<System.Action>(L, 5);
                    
                        bool gen_ret = gen_to_be_invoked.AnimateTo( _frame, _duration, _delay, _completionHandler );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Rect>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Rect _frame;translator.Get(L, 2, out _frame);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    float _delay = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        bool gen_ret = gen_to_be_invoked.AnimateTo( _frame, _duration, _delay );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Rect>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Rect _frame;translator.Get(L, 2, out _frame);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        bool gen_ret = gen_to_be_invoked.AnimateTo( _frame, _duration );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.AnimateTo!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddJavaScript(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<UniWebViewNativeResultPayload>>(L, 3)) 
                {
                    string _jsString = LuaAPI.lua_tostring(L, 2);
                    System.Action<UniWebViewNativeResultPayload> _completionHandler = translator.GetDelegate<System.Action<UniWebViewNativeResultPayload>>(L, 3);
                    
                    gen_to_be_invoked.AddJavaScript( _jsString, _completionHandler );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _jsString = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.AddJavaScript( _jsString );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.AddJavaScript!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EvaluateJavaScript(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<UniWebViewNativeResultPayload>>(L, 3)) 
                {
                    string _jsString = LuaAPI.lua_tostring(L, 2);
                    System.Action<UniWebViewNativeResultPayload> _completionHandler = translator.GetDelegate<System.Action<UniWebViewNativeResultPayload>>(L, 3);
                    
                    gen_to_be_invoked.EvaluateJavaScript( _jsString, _completionHandler );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _jsString = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.EvaluateJavaScript( _jsString );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.EvaluateJavaScript!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddUrlScheme(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _scheme = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.AddUrlScheme( _scheme );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveUrlScheme(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _scheme = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RemoveUrlScheme( _scheme );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddSslExceptionDomain(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _domain = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.AddSslExceptionDomain( _domain );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveSslExceptionDomain(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _domain = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RemoveSslExceptionDomain( _domain );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetHeaderField(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 2);
                    string _value = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.SetHeaderField( _key, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUserAgent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _agent = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetUserAgent( _agent );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUserAgent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.GetUserAgent(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAllowAutoPlay_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    bool _flag = LuaAPI.lua_toboolean(L, 1);
                    
                    UniWebView.SetAllowAutoPlay( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAllowInlinePlay_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    bool _flag = LuaAPI.lua_toboolean(L, 1);
                    
                    UniWebView.SetAllowInlinePlay( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetJavaScriptEnabled_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    bool _enabled = LuaAPI.lua_toboolean(L, 1);
                    
                    UniWebView.SetJavaScriptEnabled( _enabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAllowJavaScriptOpenWindow_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    bool _flag = LuaAPI.lua_toboolean(L, 1);
                    
                    UniWebView.SetAllowJavaScriptOpenWindow( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CleanCache(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CleanCache(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearCookies_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UniWebView.ClearCookies(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCookie_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    string _cookie = LuaAPI.lua_tostring(L, 2);
                    bool _skipEncoding = LuaAPI.lua_toboolean(L, 3);
                    
                    UniWebView.SetCookie( _url, _cookie, _skipEncoding );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    string _cookie = LuaAPI.lua_tostring(L, 2);
                    
                    UniWebView.SetCookie( _url, _cookie );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.SetCookie!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCookie_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    string _key = LuaAPI.lua_tostring(L, 2);
                    bool _skipEncoding = LuaAPI.lua_toboolean(L, 3);
                    
                        string gen_ret = UniWebView.GetCookie( _url, _key, _skipEncoding );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = UniWebView.GetCookie( _url, _key );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.GetCookie!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearHttpAuthUsernamePassword_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _host = LuaAPI.lua_tostring(L, 1);
                    string _realm = LuaAPI.lua_tostring(L, 2);
                    
                    UniWebView.ClearHttpAuthUsernamePassword( _host, _realm );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetShowSpinnerWhileLoading(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _flag = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetShowSpinnerWhileLoading( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpinnerText(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetSpinnerText( _text );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetHorizontalScrollBarEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enabled = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetHorizontalScrollBarEnabled( _enabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetVerticalScrollBarEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enabled = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetVerticalScrollBarEnabled( _enabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetBouncesEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enabled = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetBouncesEnabled( _enabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetZoomEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enabled = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetZoomEnabled( _enabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddPermissionTrustDomain(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _domain = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.AddPermissionTrustDomain( _domain );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemovePermissionTrustDomain(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _domain = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RemovePermissionTrustDomain( _domain );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetBackButtonEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enabled = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetBackButtonEnabled( _enabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUseWideViewPort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _flag = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetUseWideViewPort( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLoadWithOverviewMode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _flag = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetLoadWithOverviewMode( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetImmersiveModeEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enabled = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetImmersiveModeEnabled( _enabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetShowToolbar(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    bool _show = LuaAPI.lua_toboolean(L, 2);
                    bool _animated = LuaAPI.lua_toboolean(L, 3);
                    bool _onTop = LuaAPI.lua_toboolean(L, 4);
                    bool _adjustInset = LuaAPI.lua_toboolean(L, 5);
                    
                    gen_to_be_invoked.SetShowToolbar( _show, _animated, _onTop, _adjustInset );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    bool _show = LuaAPI.lua_toboolean(L, 2);
                    bool _animated = LuaAPI.lua_toboolean(L, 3);
                    bool _onTop = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.SetShowToolbar( _show, _animated, _onTop );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    bool _show = LuaAPI.lua_toboolean(L, 2);
                    bool _animated = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetShowToolbar( _show, _animated );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _show = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetShowToolbar( _show );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UniWebView.SetShowToolbar!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetToolbarDoneButtonText(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetToolbarDoneButtonText( _text );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetWebContentsDebuggingEnabled_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    bool _enabled = LuaAPI.lua_toboolean(L, 1);
                    
                    UniWebView.SetWebContentsDebuggingEnabled( _enabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetWindowUserResizeEnabled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enabled = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetWindowUserResizeEnabled( _enabled );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetHTMLContent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<string> _handler = translator.GetDelegate<System.Action<string>>(L, 2);
                    
                    gen_to_be_invoked.GetHTMLContent( _handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAllowFileAccessFromFileURLs(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _flag = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetAllowFileAccessFromFileURLs( _flag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Print(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Print(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Frame(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Frame);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ReferenceRectTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ReferenceRectTransform);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Url(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.Url);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanGoBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanGoBack);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanGoForward(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanGoForward);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BackgroundColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, gen_to_be_invoked.BackgroundColor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Alpha(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.Alpha);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Frame(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UnityEngine.Rect gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.Frame = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ReferenceRectTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ReferenceRectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 2, typeof(UnityEngine.RectTransform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BackgroundColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UnityEngine.Color gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.BackgroundColor = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Alpha(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Alpha = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnPageStarted(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UniWebView.PageStartedDelegate gen_delegate = translator.GetDelegate<UniWebView.PageStartedDelegate>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need UniWebView.PageStartedDelegate!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnPageStarted += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnPageStarted -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UniWebView.OnPageStarted!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnPageFinished(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UniWebView.PageFinishedDelegate gen_delegate = translator.GetDelegate<UniWebView.PageFinishedDelegate>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need UniWebView.PageFinishedDelegate!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnPageFinished += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnPageFinished -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UniWebView.OnPageFinished!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnPageErrorReceived(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UniWebView.PageErrorReceivedDelegate gen_delegate = translator.GetDelegate<UniWebView.PageErrorReceivedDelegate>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need UniWebView.PageErrorReceivedDelegate!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnPageErrorReceived += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnPageErrorReceived -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UniWebView.OnPageErrorReceived!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnMessageReceived(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UniWebView.MessageReceivedDelegate gen_delegate = translator.GetDelegate<UniWebView.MessageReceivedDelegate>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need UniWebView.MessageReceivedDelegate!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnMessageReceived += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnMessageReceived -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UniWebView.OnMessageReceived!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnShouldClose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UniWebView.ShouldCloseDelegate gen_delegate = translator.GetDelegate<UniWebView.ShouldCloseDelegate>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need UniWebView.ShouldCloseDelegate!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnShouldClose += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnShouldClose -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UniWebView.OnShouldClose!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnKeyCodeReceived(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UniWebView.KeyCodeReceivedDelegate gen_delegate = translator.GetDelegate<UniWebView.KeyCodeReceivedDelegate>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need UniWebView.KeyCodeReceivedDelegate!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnKeyCodeReceived += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnKeyCodeReceived -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UniWebView.OnKeyCodeReceived!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnOrientationChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UniWebView.OrientationChangedDelegate gen_delegate = translator.GetDelegate<UniWebView.OrientationChangedDelegate>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need UniWebView.OrientationChangedDelegate!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnOrientationChanged += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnOrientationChanged -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UniWebView.OnOrientationChanged!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_OnWebContentProcessTerminated(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UniWebView gen_to_be_invoked = (UniWebView)translator.FastGetCSObj(L, 1);
                UniWebView.OnWebContentProcessTerminatedDelegate gen_delegate = translator.GetDelegate<UniWebView.OnWebContentProcessTerminatedDelegate>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need UniWebView.OnWebContentProcessTerminatedDelegate!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.OnWebContentProcessTerminated += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.OnWebContentProcessTerminated -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UniWebView.OnWebContentProcessTerminated!");
            return 0;
        }
        
		
		
    }
}
