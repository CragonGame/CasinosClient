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
    public class CasinosEbTimeEventWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.EbTimeEvent);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 3, 3);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "mExpires", _g_get_mExpires);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onTime", _g_get_onTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mData", _g_get_mData);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "mExpires", _s_set_mExpires);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onTime", _s_set_onTime);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mData", _s_set_mData);
            
			
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
					
					Casinos.EbTimeEvent gen_ret = new Casinos.EbTimeEvent();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.EbTimeEvent constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mExpires(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.EbTimeEvent gen_to_be_invoked = (Casinos.EbTimeEvent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushuint64(L, gen_to_be_invoked.mExpires);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.EbTimeEvent gen_to_be_invoked = (Casinos.EbTimeEvent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.EbTimeEvent gen_to_be_invoked = (Casinos.EbTimeEvent)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.mData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mExpires(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.EbTimeEvent gen_to_be_invoked = (Casinos.EbTimeEvent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mExpires = LuaAPI.lua_touint64(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.EbTimeEvent gen_to_be_invoked = (Casinos.EbTimeEvent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onTime = translator.GetDelegate<Casinos.EbTimeEvent.del>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.EbTimeEvent gen_to_be_invoked = (Casinos.EbTimeEvent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mData = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
