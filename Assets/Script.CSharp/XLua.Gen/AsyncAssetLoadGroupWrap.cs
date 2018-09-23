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
    public class AsyncAssetLoadGroupWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(AsyncAssetLoadGroup);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 1, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "asyncLoadBundle", _m_asyncLoadBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "asyncLoadAsset", _m_asyncLoadAsset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadWWWAsync", _m_LoadWWWAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "asyncLoadLocalBundle", _m_asyncLoadLocalBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "releaseAssetBundle", _m_releaseAssetBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "destroy", _m_destroy);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsCancel", _g_get_IsCancel);
            
			
			
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
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<AsyncAssetLoaderMgr>(L, 2))
				{
					AsyncAssetLoaderMgr _mgr = (AsyncAssetLoaderMgr)translator.GetObject(L, 2, typeof(AsyncAssetLoaderMgr));
					
					AsyncAssetLoadGroup gen_ret = new AsyncAssetLoadGroup(_mgr);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to AsyncAssetLoadGroup constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_asyncLoadBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AsyncAssetLoadGroup gen_to_be_invoked = (AsyncAssetLoadGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _bundle_path = LuaAPI.lua_tostring(L, 2);
                    _eAsyncAssetLoadType _loader_type;translator.Get(L, 3, out _loader_type);
                    System.Action<string, UnityEngine.AssetBundle> _loaded_action = translator.GetDelegate<System.Action<string, UnityEngine.AssetBundle>>(L, 4);
                    
                    gen_to_be_invoked.asyncLoadBundle( _bundle_path, _loader_type, _loaded_action );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_asyncLoadAsset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AsyncAssetLoadGroup gen_to_be_invoked = (AsyncAssetLoadGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _bundle_path = LuaAPI.lua_tostring(L, 2);
                    string _asset_name = LuaAPI.lua_tostring(L, 3);
                    _eAsyncAssetLoadType _loader_type;translator.Get(L, 4, out _loader_type);
                    System.Action<LoaderTicket, string, UnityEngine.Object> _loaded_action = translator.GetDelegate<System.Action<LoaderTicket, string, UnityEngine.Object>>(L, 5);
                    
                        LoaderTicket gen_ret = gen_to_be_invoked.asyncLoadAsset( _bundle_path, _asset_name, _loader_type, _loaded_action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadWWWAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AsyncAssetLoadGroup gen_to_be_invoked = (AsyncAssetLoadGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    System.Action<string, UnityEngine.WWW> _loaded_action = translator.GetDelegate<System.Action<string, UnityEngine.WWW>>(L, 3);
                    
                        LoaderTicket gen_ret = gen_to_be_invoked.LoadWWWAsync( _url, _loaded_action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_asyncLoadLocalBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AsyncAssetLoadGroup gen_to_be_invoked = (AsyncAssetLoadGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<string> _list_bundle_path = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
                    _eAsyncAssetLoadType _loader_type;translator.Get(L, 3, out _loader_type);
                    System.Action<System.Collections.Generic.List<UnityEngine.AssetBundle>> _loaded_action = translator.GetDelegate<System.Action<System.Collections.Generic.List<UnityEngine.AssetBundle>>>(L, 4);
                    
                        LoaderTicket gen_ret = gen_to_be_invoked.asyncLoadLocalBundle( _list_bundle_path, _loader_type, _loaded_action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_releaseAssetBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AsyncAssetLoadGroup gen_to_be_invoked = (AsyncAssetLoadGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.releaseAssetBundle(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_destroy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AsyncAssetLoadGroup gen_to_be_invoked = (AsyncAssetLoadGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.destroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsCancel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AsyncAssetLoadGroup gen_to_be_invoked = (AsyncAssetLoadGroup)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsCancel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
