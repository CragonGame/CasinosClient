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
    public class GameCloudUnityCommonTimerShaftWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GameCloud.Unity.Common.TimerShaft);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 7, 7);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterTimer", _m_RegisterTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddTimer", _m_AddTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DelTimer", _m_DelTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ModTimer", _m_ModTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ProcessTimer", _m_ProcessTimer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTimeJeffies", _m_GetTimeJeffies);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "mWheel1", _g_get_mWheel1);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mWheel2", _g_get_mWheel2);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mWheel3", _g_get_mWheel3);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mWheel4", _g_get_mWheel4);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mWheel5", _g_get_mWheel5);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mTimeJeffies", _g_get_mTimeJeffies);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mLastJeffies", _g_get_mLastJeffies);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "mWheel1", _s_set_mWheel1);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mWheel2", _s_set_mWheel2);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mWheel3", _s_set_mWheel3);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mWheel4", _s_set_mWheel4);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mWheel5", _s_set_mWheel5);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mTimeJeffies", _s_set_mTimeJeffies);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mLastJeffies", _s_set_mLastJeffies);
            
			
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
					
					GameCloud.Unity.Common.TimerShaft gen_ret = new GameCloud.Unity.Common.TimerShaft();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to GameCloud.Unity.Common.TimerShaft constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ulong _tm = LuaAPI.lua_touint64(L, 2);
                    System.Action _cb = translator.GetDelegate<System.Action>(L, 3);
                    
                        GameCloud.Unity.Common.EbTimer gen_ret = gen_to_be_invoked.RegisterTimer( _tm, _cb );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbTimeEvent _time_event = (GameCloud.Unity.Common.EbTimeEvent)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbTimeEvent));
                    
                    gen_to_be_invoked.AddTimer( _time_event );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DelTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbTimeEvent _time_event = (GameCloud.Unity.Common.EbTimeEvent)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbTimeEvent));
                    
                        int gen_ret = gen_to_be_invoked.DelTimer( _time_event );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ModTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbTimeEvent _time_event = (GameCloud.Unity.Common.EbTimeEvent)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbTimeEvent));
                    ulong _expires = LuaAPI.lua_touint64(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.ModTimer( _time_event, _expires );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ProcessTimer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ulong _jeffies = LuaAPI.lua_touint64(L, 2);
                    
                    gen_to_be_invoked.ProcessTimer( _jeffies );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTimeJeffies(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        ulong gen_ret = gen_to_be_invoked.GetTimeJeffies(  );
                        LuaAPI.lua_pushuint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mWheel1(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mWheel1);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mWheel2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mWheel2);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mWheel3(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mWheel3);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mWheel4(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mWheel4);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mWheel5(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mWheel5);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mTimeJeffies(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushuint64(L, gen_to_be_invoked.mTimeJeffies);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mLastJeffies(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushuint64(L, gen_to_be_invoked.mLastJeffies);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mWheel1(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mWheel1 = (GameCloud.Unity.Common.EbTimeWheel)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbTimeWheel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mWheel2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mWheel2 = (GameCloud.Unity.Common.EbTimeWheel)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbTimeWheel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mWheel3(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mWheel3 = (GameCloud.Unity.Common.EbTimeWheel)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbTimeWheel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mWheel4(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mWheel4 = (GameCloud.Unity.Common.EbTimeWheel)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbTimeWheel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mWheel5(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mWheel5 = (GameCloud.Unity.Common.EbTimeWheel)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbTimeWheel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mTimeJeffies(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mTimeJeffies = LuaAPI.lua_touint64(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mLastJeffies(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.TimerShaft gen_to_be_invoked = (GameCloud.Unity.Common.TimerShaft)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mLastJeffies = LuaAPI.lua_touint64(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
