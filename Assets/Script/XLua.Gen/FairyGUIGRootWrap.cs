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
    public class FairyGUIGRootWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.GRoot);
			Utils.BeginObjectRegister(type, L, translator, 0, 22, 7, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetContentScaleFactor", _m_SetContentScaleFactor);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ApplyContentScaleFactor", _m_ApplyContentScaleFactor);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowWindow", _m_ShowWindow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HideWindow", _m_HideWindow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HideWindowImmediately", _m_HideWindowImmediately);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BringToFront", _m_BringToFront);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowModalWait", _m_ShowModalWait);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseModalWait", _m_CloseModalWait);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseAllExceptModals", _m_CloseAllExceptModals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseAllWindows", _m_CloseAllWindows);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTopWindow", _m_GetTopWindow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisplayObjectToGObject", _m_DisplayObjectToGObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowPopup", _m_ShowPopup);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPoupPosition", _m_GetPoupPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TogglePopup", _m_TogglePopup);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HidePopup", _m_HidePopup);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowTooltips", _m_ShowTooltips);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowTooltipsWin", _m_ShowTooltipsWin);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HideTooltips", _m_HideTooltips);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EnableSound", _m_EnableSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisableSound", _m_DisableSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayOneShotSound", _m_PlayOneShotSound);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "modalLayer", _g_get_modalLayer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hasModalWindow", _g_get_hasModalWindow);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "modalWaiting", _g_get_modalWaiting);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "touchTarget", _g_get_touchTarget);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hasAnyPopup", _g_get_hasAnyPopup);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "focus", _g_get_focus);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "soundVolume", _g_get_soundVolume);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "focus", _s_set_focus);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "soundVolume", _s_set_soundVolume);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 2, 0);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "contentScaleFactor", _g_get_contentScaleFactor);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "inst", _g_get_inst);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					FairyGUI.GRoot gen_ret = new FairyGUI.GRoot();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GRoot constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetContentScaleFactor(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _designResolutionX = LuaAPI.xlua_tointeger(L, 2);
                    int _designResolutionY = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetContentScaleFactor( _designResolutionX, _designResolutionY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<FairyGUI.UIContentScaler.ScreenMatchMode>(L, 4)) 
                {
                    int _designResolutionX = LuaAPI.xlua_tointeger(L, 2);
                    int _designResolutionY = LuaAPI.xlua_tointeger(L, 3);
                    FairyGUI.UIContentScaler.ScreenMatchMode _screenMatchMode;translator.Get(L, 4, out _screenMatchMode);
                    
                    gen_to_be_invoked.SetContentScaleFactor( _designResolutionX, _designResolutionY, _screenMatchMode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GRoot.SetContentScaleFactor!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ApplyContentScaleFactor(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ApplyContentScaleFactor(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowWindow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Window _win = (FairyGUI.Window)translator.GetObject(L, 2, typeof(FairyGUI.Window));
                    
                    gen_to_be_invoked.ShowWindow( _win );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HideWindow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Window _win = (FairyGUI.Window)translator.GetObject(L, 2, typeof(FairyGUI.Window));
                    
                    gen_to_be_invoked.HideWindow( _win );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HideWindowImmediately(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.Window>(L, 2)) 
                {
                    FairyGUI.Window _win = (FairyGUI.Window)translator.GetObject(L, 2, typeof(FairyGUI.Window));
                    
                    gen_to_be_invoked.HideWindowImmediately( _win );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<FairyGUI.Window>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    FairyGUI.Window _win = (FairyGUI.Window)translator.GetObject(L, 2, typeof(FairyGUI.Window));
                    bool _dispose = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.HideWindowImmediately( _win, _dispose );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GRoot.HideWindowImmediately!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BringToFront(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Window _win = (FairyGUI.Window)translator.GetObject(L, 2, typeof(FairyGUI.Window));
                    
                    gen_to_be_invoked.BringToFront( _win );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowModalWait(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ShowModalWait(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseModalWait(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CloseModalWait(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseAllExceptModals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CloseAllExceptModals(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseAllWindows(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CloseAllWindows(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTopWindow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        FairyGUI.Window gen_ret = gen_to_be_invoked.GetTopWindow(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisplayObjectToGObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.DisplayObject _obj = (FairyGUI.DisplayObject)translator.GetObject(L, 2, typeof(FairyGUI.DisplayObject));
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.DisplayObjectToGObject( _obj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowPopup(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GObject>(L, 2)) 
                {
                    FairyGUI.GObject _popup = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.ShowPopup( _popup );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<FairyGUI.GObject>(L, 2)&& translator.Assignable<FairyGUI.GObject>(L, 3)) 
                {
                    FairyGUI.GObject _popup = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 3, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.ShowPopup( _popup, _target );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<FairyGUI.GObject>(L, 2)&& translator.Assignable<FairyGUI.GObject>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    FairyGUI.GObject _popup = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 3, typeof(FairyGUI.GObject));
                    object _downward = translator.GetObject(L, 4, typeof(object));
                    
                    gen_to_be_invoked.ShowPopup( _popup, _target, _downward );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GRoot.ShowPopup!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPoupPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _popup = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 3, typeof(FairyGUI.GObject));
                    object _downward = translator.GetObject(L, 4, typeof(object));
                    
                        UnityEngine.Vector2 gen_ret = gen_to_be_invoked.GetPoupPosition( _popup, _target, _downward );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TogglePopup(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GObject>(L, 2)) 
                {
                    FairyGUI.GObject _popup = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.TogglePopup( _popup );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<FairyGUI.GObject>(L, 2)&& translator.Assignable<FairyGUI.GObject>(L, 3)) 
                {
                    FairyGUI.GObject _popup = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 3, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.TogglePopup( _popup, _target );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<FairyGUI.GObject>(L, 2)&& translator.Assignable<FairyGUI.GObject>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    FairyGUI.GObject _popup = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 3, typeof(FairyGUI.GObject));
                    object _downward = translator.GetObject(L, 4, typeof(object));
                    
                    gen_to_be_invoked.TogglePopup( _popup, _target, _downward );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GRoot.TogglePopup!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HidePopup(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.HidePopup(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GObject>(L, 2)) 
                {
                    FairyGUI.GObject _popup = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.HidePopup( _popup );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GRoot.HidePopup!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowTooltips(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _msg = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.ShowTooltips( _msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowTooltipsWin(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _tooltipWin = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.ShowTooltipsWin( _tooltipWin );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HideTooltips(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.HideTooltips(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnableSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.EnableSound(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisableSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DisableSound(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayOneShotSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.AudioClip>(L, 2)) 
                {
                    UnityEngine.AudioClip _clip = (UnityEngine.AudioClip)translator.GetObject(L, 2, typeof(UnityEngine.AudioClip));
                    
                    gen_to_be_invoked.PlayOneShotSound( _clip );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.AudioClip>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.AudioClip _clip = (UnityEngine.AudioClip)translator.GetObject(L, 2, typeof(UnityEngine.AudioClip));
                    float _volumeScale = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.PlayOneShotSound( _clip, _volumeScale );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GRoot.PlayOneShotSound!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_contentScaleFactor(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, FairyGUI.GRoot.contentScaleFactor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_inst(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, FairyGUI.GRoot.inst);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_modalLayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.modalLayer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hasModalWindow(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.hasModalWindow);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_modalWaiting(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.modalWaiting);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_touchTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.touchTarget);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hasAnyPopup(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.hasAnyPopup);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_focus(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.focus);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_soundVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.soundVolume);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_focus(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.focus = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_soundVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GRoot gen_to_be_invoked = (FairyGUI.GRoot)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.soundVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
