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
    public class GameCloudUnityCommonEbTimerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GameCloud.Unity.Common.EbTimer);
			Utils.BeginObjectRegister(type, L, translator, 0, 1, 3, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Close", _m_Close);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "TimerShaft", _g_get_TimerShaft);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TimeEvent", _g_get_TimeEvent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TimeNode", _g_get_TimeNode);
            
			
			
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
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<GameCloud.Unity.Common.TimerShaft>(L, 2) && (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isuint64(L, 3)) && translator.Assignable<System.Action>(L, 4))
				{
					GameCloud.Unity.Common.TimerShaft _timer_shaft = (GameCloud.Unity.Common.TimerShaft)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.TimerShaft));
					ulong _tm = LuaAPI.lua_touint64(L, 3);
					System.Action _cb = translator.GetDelegate<System.Action>(L, 4);
					
					GameCloud.Unity.Common.EbTimer gen_ret = new GameCloud.Unity.Common.EbTimer(_timer_shaft, _tm, _cb);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to GameCloud.Unity.Common.EbTimer constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Close(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbTimer gen_to_be_invoked = (GameCloud.Unity.Common.EbTimer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Close(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TimerShaft(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbTimer gen_to_be_invoked = (GameCloud.Unity.Common.EbTimer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TimerShaft);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TimeEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbTimer gen_to_be_invoked = (GameCloud.Unity.Common.EbTimer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TimeEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TimeNode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbTimer gen_to_be_invoked = (GameCloud.Unity.Common.EbTimer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TimeNode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
