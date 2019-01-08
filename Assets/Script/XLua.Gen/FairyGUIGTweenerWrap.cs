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
    public class FairyGUIGTweenerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.GTweener);
			Utils.BeginObjectRegister(type, L, translator, 0, 20, 12, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDelay", _m_SetDelay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDuration", _m_SetDuration);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetBreakpoint", _m_SetBreakpoint);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEase", _m_SetEase);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEasePeriod", _m_SetEasePeriod);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEaseOvershootOrAmplitude", _m_SetEaseOvershootOrAmplitude);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRepeat", _m_SetRepeat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTimeScale", _m_SetTimeScale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIgnoreEngineTimeScale", _m_SetIgnoreEngineTimeScale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSnapping", _m_SetSnapping);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPath", _m_SetPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTarget", _m_SetTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUserData", _m_SetUserData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnUpdate", _m_OnUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnStart", _m_OnStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnComplete", _m_OnComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetListener", _m_SetListener);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPaused", _m_SetPaused);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Seek", _m_Seek);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Kill", _m_Kill);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "delay", _g_get_delay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "duration", _g_get_duration);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "repeat", _g_get_repeat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "target", _g_get_target);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "userData", _g_get_userData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "startValue", _g_get_startValue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "endValue", _g_get_endValue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "value", _g_get_value);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "deltaValue", _g_get_deltaValue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "normalizedTime", _g_get_normalizedTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "completed", _g_get_completed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "allCompleted", _g_get_allCompleted);
            
			
			
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
					
					FairyGUI.GTweener gen_ret = new FairyGUI.GTweener();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTweener constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDelay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetDelay( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDuration(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetDuration( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetBreakpoint(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetBreakpoint( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEase(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.EaseType _value;translator.Get(L, 2, out _value);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetEase( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEasePeriod(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetEasePeriod( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEaseOvershootOrAmplitude(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetEaseOvershootOrAmplitude( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRepeat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    int _repeat = LuaAPI.xlua_tointeger(L, 2);
                    bool _yoyo = LuaAPI.lua_toboolean(L, 3);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetRepeat( _repeat, _yoyo );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _repeat = LuaAPI.xlua_tointeger(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetRepeat( _repeat );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTweener.SetRepeat!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTimeScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetTimeScale( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIgnoreEngineTimeScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _value = LuaAPI.lua_toboolean(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetIgnoreEngineTimeScale( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSnapping(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _value = LuaAPI.lua_toboolean(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetSnapping( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GPath _value = (FairyGUI.GPath)translator.GetObject(L, 2, typeof(FairyGUI.GPath));
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetPath( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetTarget( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& translator.Assignable<FairyGUI.TweenPropType>(L, 3)) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    FairyGUI.TweenPropType _propType;translator.Get(L, 3, out _propType);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetTarget( _value, _propType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTweener.SetTarget!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUserData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetUserData( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GTweenCallback>(L, 2)) 
                {
                    FairyGUI.GTweenCallback _callback = translator.GetDelegate<FairyGUI.GTweenCallback>(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.OnUpdate( _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GTweenCallback1>(L, 2)) 
                {
                    FairyGUI.GTweenCallback1 _callback = translator.GetDelegate<FairyGUI.GTweenCallback1>(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.OnUpdate( _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTweener.OnUpdate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GTweenCallback>(L, 2)) 
                {
                    FairyGUI.GTweenCallback _callback = translator.GetDelegate<FairyGUI.GTweenCallback>(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.OnStart( _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GTweenCallback1>(L, 2)) 
                {
                    FairyGUI.GTweenCallback1 _callback = translator.GetDelegate<FairyGUI.GTweenCallback1>(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.OnStart( _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTweener.OnStart!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnComplete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GTweenCallback>(L, 2)) 
                {
                    FairyGUI.GTweenCallback _callback = translator.GetDelegate<FairyGUI.GTweenCallback>(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.OnComplete( _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GTweenCallback1>(L, 2)) 
                {
                    FairyGUI.GTweenCallback1 _callback = translator.GetDelegate<FairyGUI.GTweenCallback1>(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.OnComplete( _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTweener.OnComplete!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetListener(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.ITweenListener _value = (FairyGUI.ITweenListener)translator.GetObject(L, 2, typeof(FairyGUI.ITweenListener));
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetListener( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPaused(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _paused = LuaAPI.lua_toboolean(L, 2);
                    
                        FairyGUI.GTweener gen_ret = gen_to_be_invoked.SetPaused( _paused );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Seek(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Seek( _time );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Kill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _complete = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Kill( _complete );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Kill(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTweener.Kill!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_delay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.delay);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_duration(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.duration);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_repeat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.repeat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_target(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.target);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_userData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.userData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_startValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.startValue);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_endValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.endValue);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_value(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.value);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_deltaValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.deltaValue);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_normalizedTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.normalizedTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_completed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.completed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_allCompleted(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTweener gen_to_be_invoked = (FairyGUI.GTweener)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.allCompleted);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
