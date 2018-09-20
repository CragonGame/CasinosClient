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
    public class FairyGUIGTweenWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.GTween);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 9, 1, 1);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "To", _m_To_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ToDouble", _m_ToDouble_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DelayedCall", _m_DelayedCall_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Shake", _m_Shake_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsTweening", _m_IsTweening_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Kill", _m_Kill_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTween", _m_GetTween_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Clean", _m_Clean_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "catchCallbackExceptions", _g_get_catchCallbackExceptions);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "catchCallbackExceptions", _s_set_catchCallbackExceptions);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					FairyGUI.GTween gen_ret = new FairyGUI.GTween();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTween constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_To_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 1);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.To( _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector2>(L, 1)&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector2 _startValue;translator.Get(L, 1, out _startValue);
                    UnityEngine.Vector2 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.To( _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _startValue;translator.Get(L, 1, out _startValue);
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.To( _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector4>(L, 1)&& translator.Assignable<UnityEngine.Vector4>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector4 _startValue;translator.Get(L, 1, out _startValue);
                    UnityEngine.Vector4 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.To( _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Color>(L, 1)&& translator.Assignable<UnityEngine.Color>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Color _startValue;translator.Get(L, 1, out _startValue);
                    UnityEngine.Color _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.To( _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTween.To!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToDouble_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    double _startValue = LuaAPI.lua_tonumber(L, 1);
                    double _endValue = LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.ToDouble( _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DelayedCall_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    float _delay = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.DelayedCall( _delay );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Shake_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Vector3 _startValue;translator.Get(L, 1, out _startValue);
                    float _amplitude = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.Shake( _startValue, _amplitude, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsTweening_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    
                        bool gen_ret = FairyGUI.GTween.IsTweening( _target );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& translator.Assignable<FairyGUI.TweenPropType>(L, 2)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    FairyGUI.TweenPropType _propType;translator.Get(L, 2, out _propType);
                    
                        bool gen_ret = FairyGUI.GTween.IsTweening( _target, _propType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTween.IsTweening!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Kill_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    
                    FairyGUI.GTween.Kill( _target );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    bool _complete = LuaAPI.lua_toboolean(L, 2);
                    
                    FairyGUI.GTween.Kill( _target, _complete );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 1)&& translator.Assignable<FairyGUI.TweenPropType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    FairyGUI.TweenPropType _propType;translator.Get(L, 2, out _propType);
                    bool _complete = LuaAPI.lua_toboolean(L, 3);
                    
                    FairyGUI.GTween.Kill( _target, _propType, _complete );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTween.Kill!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTween_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.GetTween( _target );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& translator.Assignable<FairyGUI.TweenPropType>(L, 2)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    FairyGUI.TweenPropType _propType;translator.Get(L, 2, out _propType);
                    
                        FairyGUI.GTweener gen_ret = FairyGUI.GTween.GetTween( _target, _propType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTween.GetTween!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clean_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    FairyGUI.GTween.Clean(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_catchCallbackExceptions(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, FairyGUI.GTween.catchCallbackExceptions);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_catchCallbackExceptions(RealStatePtr L)
        {
		    try {
                
			    FairyGUI.GTween.catchCallbackExceptions = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
