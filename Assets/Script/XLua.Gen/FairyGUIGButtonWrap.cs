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
    public class FairyGUIGButtonWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.GButton);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 17, 15);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FireClick", _m_FireClick);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTextField", _m_GetTextField);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HandleControllerChanged", _m_HandleControllerChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Setup_AfterAdd", _m_Setup_AfterAdd);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "pageOption", _g_get_pageOption);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onChanged", _g_get_onChanged);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "icon", _g_get_icon);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "title", _g_get_title);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "text", _g_get_text);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "selectedIcon", _g_get_selectedIcon);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "selectedTitle", _g_get_selectedTitle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "titleColor", _g_get_titleColor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "color", _g_get_color);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "titleFontSize", _g_get_titleFontSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "selected", _g_get_selected);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mode", _g_get_mode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "relatedController", _g_get_relatedController);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sound", _g_get_sound);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "soundVolumeScale", _g_get_soundVolumeScale);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "changeStateOnClick", _g_get_changeStateOnClick);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "linkedPopup", _g_get_linkedPopup);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "icon", _s_set_icon);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "title", _s_set_title);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "text", _s_set_text);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "selectedIcon", _s_set_selectedIcon);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "selectedTitle", _s_set_selectedTitle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "titleColor", _s_set_titleColor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "color", _s_set_color);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "titleFontSize", _s_set_titleFontSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "selected", _s_set_selected);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mode", _s_set_mode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "relatedController", _s_set_relatedController);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sound", _s_set_sound);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "soundVolumeScale", _s_set_soundVolumeScale);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "changeStateOnClick", _s_set_changeStateOnClick);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "linkedPopup", _s_set_linkedPopup);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 7, 0, 0);
			
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UP", FairyGUI.GButton.UP);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DOWN", FairyGUI.GButton.DOWN);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OVER", FairyGUI.GButton.OVER);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SELECTED_OVER", FairyGUI.GButton.SELECTED_OVER);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DISABLED", FairyGUI.GButton.DISABLED);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SELECTED_DISABLED", FairyGUI.GButton.SELECTED_DISABLED);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					FairyGUI.GButton gen_ret = new FairyGUI.GButton();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GButton constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FireClick(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _downEffect = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.FireClick( _downEffect );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTextField(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        FairyGUI.GTextField gen_ret = gen_to_be_invoked.GetTextField(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HandleControllerChanged(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Controller _c = (FairyGUI.Controller)translator.GetObject(L, 2, typeof(FairyGUI.Controller));
                    
                    gen_to_be_invoked.HandleControllerChanged( _c );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Setup_AfterAdd(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Utils.ByteBuffer _buffer = (FairyGUI.Utils.ByteBuffer)translator.GetObject(L, 2, typeof(FairyGUI.Utils.ByteBuffer));
                    int _beginPos = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Setup_AfterAdd( _buffer, _beginPos );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_pageOption(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.pageOption);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onChanged);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_icon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.icon);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_title(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.title);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_text(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.text);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_selectedIcon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.selectedIcon);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_selectedTitle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.selectedTitle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_titleColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, gen_to_be_invoked.titleColor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_color(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, gen_to_be_invoked.color);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_titleFontSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.titleFontSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_selected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.selected);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_relatedController(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.relatedController);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sound(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.sound);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_soundVolumeScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.soundVolumeScale);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_changeStateOnClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.changeStateOnClick);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_linkedPopup(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.linkedPopup);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_icon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.icon = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_title(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.title = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_text(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.text = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_selectedIcon(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.selectedIcon = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_selectedTitle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.selectedTitle = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_titleColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                UnityEngine.Color gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.titleColor = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_color(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                UnityEngine.Color gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.color = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_titleFontSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.titleFontSize = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_selected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.selected = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                FairyGUI.ButtonMode gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.mode = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_relatedController(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.relatedController = (FairyGUI.Controller)translator.GetObject(L, 2, typeof(FairyGUI.Controller));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sound(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.sound = (FairyGUI.NAudioClip)translator.GetObject(L, 2, typeof(FairyGUI.NAudioClip));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_soundVolumeScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.soundVolumeScale = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_changeStateOnClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.changeStateOnClick = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_linkedPopup(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GButton gen_to_be_invoked = (FairyGUI.GButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.linkedPopup = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
