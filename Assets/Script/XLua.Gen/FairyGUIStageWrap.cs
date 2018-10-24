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
    public class FairyGUIStageWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.Stage);
			Utils.BeginObjectRegister(type, L, translator, 0, 17, 9, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTouchPosition", _m_GetTouchPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAllTouch", _m_GetAllTouch);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResetInputState", _m_ResetInputState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CancelClick", _m_CancelClick);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EnableSound", _m_EnableSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisableSound", _m_DisableSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayOneShotSound", _m_PlayOneShotSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenKeyboard", _m_OpenKeyboard);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseKeyboard", _m_CloseKeyboard);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InputString", _m_InputString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCustomInput", _m_SetCustomInput);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ApplyPanelOrder", _m_ApplyPanelOrder);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SortWorldSpacePanelsByZOrder", _m_SortWorldSpacePanelsByZOrder);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MonitorTexture", _m_MonitorTexture);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddTouchMonitor", _m_AddTouchMonitor);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveTouchMonitor", _m_RemoveTouchMonitor);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "stageHeight", _g_get_stageHeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "stageWidth", _g_get_stageWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "soundVolume", _g_get_soundVolume);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onStageResized", _g_get_onStageResized);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "touchTarget", _g_get_touchTarget);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "focus", _g_get_focus);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "touchPosition", _g_get_touchPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "touchCount", _g_get_touchCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "keyboard", _g_get_keyboard);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "soundVolume", _s_set_soundVolume);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "focus", _s_set_focus);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "keyboard", _s_set_keyboard);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 4, 2);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Instantiate", _m_Instantiate_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "inst", _g_get_inst);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "touchScreen", _g_get_touchScreen);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "keyboardInput", _g_get_keyboardInput);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "isTouchOnUI", _g_get_isTouchOnUI);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "touchScreen", _s_set_touchScreen);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "keyboardInput", _s_set_keyboardInput);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					FairyGUI.Stage gen_ret = new FairyGUI.Stage();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.Stage constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Instantiate_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    FairyGUI.Stage.Instantiate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTouchPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _touchId = LuaAPI.xlua_tointeger(L, 2);
                    
                        UnityEngine.Vector2 gen_ret = gen_to_be_invoked.GetTouchPosition( _touchId );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAllTouch(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int[] _result = (int[])translator.GetObject(L, 2, typeof(int[]));
                    
                        int[] gen_ret = gen_to_be_invoked.GetAllTouch( _result );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResetInputState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ResetInputState(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CancelClick(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _touchId = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.CancelClick( _touchId );
                    
                    
                    
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
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.Stage.PlayOneShotSound!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenKeyboard(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    bool _autocorrection = LuaAPI.lua_toboolean(L, 3);
                    bool _multiline = LuaAPI.lua_toboolean(L, 4);
                    bool _secure = LuaAPI.lua_toboolean(L, 5);
                    bool _alert = LuaAPI.lua_toboolean(L, 6);
                    string _textPlaceholder = LuaAPI.lua_tostring(L, 7);
                    int _keyboardType = LuaAPI.xlua_tointeger(L, 8);
                    bool _hideInput = LuaAPI.lua_toboolean(L, 9);
                    
                    gen_to_be_invoked.OpenKeyboard( _text, _autocorrection, _multiline, _secure, _alert, _textPlaceholder, _keyboardType, _hideInput );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseKeyboard(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CloseKeyboard(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InputString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.InputString( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCustomInput(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector2 _screenPos;translator.Get(L, 2, out _screenPos);
                    bool _buttonDown = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetCustomInput( _screenPos, _buttonDown );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.RaycastHit>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.RaycastHit _hit;translator.Get(L, 2, out _hit);
                    bool _buttonDown = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetCustomInput( ref _hit, _buttonDown );
                    translator.Push(L, _hit);
                        translator.Update(L, 2, _hit);
                        
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector2 _screenPos;translator.Get(L, 2, out _screenPos);
                    bool _buttonDown = LuaAPI.lua_toboolean(L, 3);
                    bool _buttonUp = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.SetCustomInput( _screenPos, _buttonDown, _buttonUp );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.RaycastHit>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.RaycastHit _hit;translator.Get(L, 2, out _hit);
                    bool _buttonDown = LuaAPI.lua_toboolean(L, 3);
                    bool _buttonUp = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.SetCustomInput( ref _hit, _buttonDown, _buttonUp );
                    translator.Push(L, _hit);
                        translator.Update(L, 2, _hit);
                        
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.Stage.SetCustomInput!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ApplyPanelOrder(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Container _target = (FairyGUI.Container)translator.GetObject(L, 2, typeof(FairyGUI.Container));
                    
                    gen_to_be_invoked.ApplyPanelOrder( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SortWorldSpacePanelsByZOrder(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _panelSortingOrder = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SortWorldSpacePanelsByZOrder( _panelSortingOrder );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MonitorTexture(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.NTexture _texture = (FairyGUI.NTexture)translator.GetObject(L, 2, typeof(FairyGUI.NTexture));
                    
                    gen_to_be_invoked.MonitorTexture( _texture );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddTouchMonitor(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _touchId = LuaAPI.xlua_tointeger(L, 2);
                    FairyGUI.EventDispatcher _target = (FairyGUI.EventDispatcher)translator.GetObject(L, 3, typeof(FairyGUI.EventDispatcher));
                    
                    gen_to_be_invoked.AddTouchMonitor( _touchId, _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveTouchMonitor(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.EventDispatcher _target = (FairyGUI.EventDispatcher)translator.GetObject(L, 2, typeof(FairyGUI.EventDispatcher));
                    
                    gen_to_be_invoked.RemoveTouchMonitor( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_stageHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.stageHeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_stageWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.stageWidth);
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
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.soundVolume);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onStageResized(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onStageResized);
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
			    translator.Push(L, FairyGUI.Stage.inst);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_touchScreen(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, FairyGUI.Stage.touchScreen);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_keyboardInput(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, FairyGUI.Stage.keyboardInput);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isTouchOnUI(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, FairyGUI.Stage.isTouchOnUI);
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
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.touchTarget);
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
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.focus);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_touchPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.touchPosition);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_touchCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.touchCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_keyboard(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.keyboard);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_soundVolume(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.soundVolume = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_touchScreen(RealStatePtr L)
        {
		    try {
                
			    FairyGUI.Stage.touchScreen = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_keyboardInput(RealStatePtr L)
        {
		    try {
                
			    FairyGUI.Stage.keyboardInput = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_focus(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.focus = (FairyGUI.DisplayObject)translator.GetObject(L, 2, typeof(FairyGUI.DisplayObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_keyboard(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.Stage gen_to_be_invoked = (FairyGUI.Stage)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.keyboard = (FairyGUI.IKeyboard)translator.GetObject(L, 2, typeof(FairyGUI.IKeyboard));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
