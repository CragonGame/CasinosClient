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
    public class AsyncAssetLoaderMgrWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(AsyncAssetLoaderMgr);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 5, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "createAsyncAssetLoadGroup", _m_createAsyncAssetLoadGroup);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "WWWAsyncLoader", _g_get_WWWAsyncLoader);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "WWWBundleAsyncLoader", _g_get_WWWBundleAsyncLoader);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "WWWAssetAsyncLoader", _g_get_WWWAssetAsyncLoader);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LocalBundleAsyncLoader", _g_get_LocalBundleAsyncLoader);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LocalAssetAsyncLoader", _g_get_LocalAssetAsyncLoader);
            
			
			
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
					
					AsyncAssetLoaderMgr gen_ret = new AsyncAssetLoaderMgr();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to AsyncAssetLoaderMgr constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_createAsyncAssetLoadGroup(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AsyncAssetLoaderMgr gen_to_be_invoked = (AsyncAssetLoaderMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        AsyncAssetLoadGroup gen_ret = gen_to_be_invoked.createAsyncAssetLoadGroup(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AsyncAssetLoaderMgr gen_to_be_invoked = (AsyncAssetLoaderMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Update( _time );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_WWWAsyncLoader(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AsyncAssetLoaderMgr gen_to_be_invoked = (AsyncAssetLoaderMgr)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.WWWAsyncLoader);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_WWWBundleAsyncLoader(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AsyncAssetLoaderMgr gen_to_be_invoked = (AsyncAssetLoaderMgr)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.WWWBundleAsyncLoader);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_WWWAssetAsyncLoader(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AsyncAssetLoaderMgr gen_to_be_invoked = (AsyncAssetLoaderMgr)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.WWWAssetAsyncLoader);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LocalBundleAsyncLoader(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AsyncAssetLoaderMgr gen_to_be_invoked = (AsyncAssetLoaderMgr)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LocalBundleAsyncLoader);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LocalAssetAsyncLoader(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AsyncAssetLoaderMgr gen_to_be_invoked = (AsyncAssetLoaderMgr)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LocalAssetAsyncLoader);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
