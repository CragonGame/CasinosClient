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
    public class GameCloudUnityCommonEbDoubleLinkNode_1_CasinosEbTimeEvent_Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 3, 3);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "next", _g_get_next);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "prev", _g_get_prev);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mObject", _g_get_mObject);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "next", _s_set_next);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "prev", _s_set_prev);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mObject", _s_set_mObject);
            
			
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
					
					GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_ret = new GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_next(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.next);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_prev(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.prev);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mObject);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_next(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.next = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_prev(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.prev = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mObject = (Casinos.EbTimeEvent)translator.GetObject(L, 2, typeof(Casinos.EbTimeEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
