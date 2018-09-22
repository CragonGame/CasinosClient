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
    public class GameCloudUnityCommonEbTimeWheelWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GameCloud.Unity.Common.EbTimeWheel);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 3, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Destroy", _m_Destroy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSpoke", _m_GetSpoke);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSpokeHead", _m_GetSpokeHead);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "mListTimerSpoke", _g_get_mListTimerSpoke);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mSpokeCount", _g_get_mSpokeCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mLastSpokeIndex", _g_get_mLastSpokeIndex);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "mListTimerSpoke", _s_set_mListTimerSpoke);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mSpokeCount", _s_set_mSpokeCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mLastSpokeIndex", _s_set_mLastSpokeIndex);
            
			
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
				if(LuaAPI.lua_gettop(L) == 2 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					int _spoke_count = LuaAPI.xlua_tointeger(L, 2);
					
					GameCloud.Unity.Common.EbTimeWheel gen_ret = new GameCloud.Unity.Common.EbTimeWheel(_spoke_count);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to GameCloud.Unity.Common.EbTimeWheel constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Destroy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbTimeWheel gen_to_be_invoked = (GameCloud.Unity.Common.EbTimeWheel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Destroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSpoke(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbTimeWheel gen_to_be_invoked = (GameCloud.Unity.Common.EbTimeWheel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        GameCloud.Unity.Common.EbDoubleLinkList<GameCloud.Unity.Common.EbTimeEvent> gen_ret = gen_to_be_invoked.GetSpoke( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSpokeHead(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbTimeWheel gen_to_be_invoked = (GameCloud.Unity.Common.EbTimeWheel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        GameCloud.Unity.Common.EbDoubleLinkNode<GameCloud.Unity.Common.EbTimeEvent> gen_ret = gen_to_be_invoked.GetSpokeHead( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mListTimerSpoke(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbTimeWheel gen_to_be_invoked = (GameCloud.Unity.Common.EbTimeWheel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mListTimerSpoke);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mSpokeCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbTimeWheel gen_to_be_invoked = (GameCloud.Unity.Common.EbTimeWheel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.mSpokeCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mLastSpokeIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbTimeWheel gen_to_be_invoked = (GameCloud.Unity.Common.EbTimeWheel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.mLastSpokeIndex);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mListTimerSpoke(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbTimeWheel gen_to_be_invoked = (GameCloud.Unity.Common.EbTimeWheel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mListTimerSpoke = (System.Collections.Generic.List<GameCloud.Unity.Common.EbDoubleLinkList<GameCloud.Unity.Common.EbTimeEvent>>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<GameCloud.Unity.Common.EbDoubleLinkList<GameCloud.Unity.Common.EbTimeEvent>>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mSpokeCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbTimeWheel gen_to_be_invoked = (GameCloud.Unity.Common.EbTimeWheel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mSpokeCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mLastSpokeIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbTimeWheel gen_to_be_invoked = (GameCloud.Unity.Common.EbTimeWheel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mLastSpokeIndex = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
