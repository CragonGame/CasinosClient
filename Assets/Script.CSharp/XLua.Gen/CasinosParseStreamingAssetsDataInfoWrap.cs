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
    public class CasinosParseStreamingAssetsDataInfoWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.ParseStreamingAssetsDataInfo);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 2, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "startPaseData", _m_startPaseData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "writeStreamingAssetsDataFileList2Persistent", _m_writeStreamingAssetsDataFileList2Persistent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "update", _m_update);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ListPreData", _g_get_ListPreData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ListData", _g_get_ListData);
            
			
			
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
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.Action>(L, 2))
				{
					System.Action _parse_down = translator.GetDelegate<System.Action>(L, 2);
					
					Casinos.ParseStreamingAssetsDataInfo gen_ret = new Casinos.ParseStreamingAssetsDataInfo(_parse_down);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.ParseStreamingAssetsDataInfo constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_startPaseData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.ParseStreamingAssetsDataInfo gen_to_be_invoked = (Casinos.ParseStreamingAssetsDataInfo)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.startPaseData(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_writeStreamingAssetsDataFileList2Persistent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.ParseStreamingAssetsDataInfo gen_to_be_invoked = (Casinos.ParseStreamingAssetsDataInfo)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.writeStreamingAssetsDataFileList2Persistent(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.ParseStreamingAssetsDataInfo gen_to_be_invoked = (Casinos.ParseStreamingAssetsDataInfo)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _tm = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.update( _tm );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ListPreData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.ParseStreamingAssetsDataInfo gen_to_be_invoked = (Casinos.ParseStreamingAssetsDataInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ListPreData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ListData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.ParseStreamingAssetsDataInfo gen_to_be_invoked = (Casinos.ParseStreamingAssetsDataInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ListData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
