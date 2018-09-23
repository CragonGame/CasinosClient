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
    public class LoaderTicketWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LoaderTicket);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 3, 3);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "UserData", _g_get_UserData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MapNeedLoadAssetBundle", _g_get_MapNeedLoadAssetBundle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MapLoadedAssetBundle", _g_get_MapLoadedAssetBundle);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "UserData", _s_set_UserData);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MapNeedLoadAssetBundle", _s_set_MapNeedLoadAssetBundle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MapLoadedAssetBundle", _s_set_MapLoadedAssetBundle);
            
			
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
					
					LoaderTicket gen_ret = new LoaderTicket();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to LoaderTicket constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UserData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoaderTicket gen_to_be_invoked = (LoaderTicket)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.UserData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MapNeedLoadAssetBundle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoaderTicket gen_to_be_invoked = (LoaderTicket)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MapNeedLoadAssetBundle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MapLoadedAssetBundle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoaderTicket gen_to_be_invoked = (LoaderTicket)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MapLoadedAssetBundle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UserData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoaderTicket gen_to_be_invoked = (LoaderTicket)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UserData = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MapNeedLoadAssetBundle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoaderTicket gen_to_be_invoked = (LoaderTicket)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MapNeedLoadAssetBundle = (System.Collections.Generic.Dictionary<string, UnityEngine.AssetBundle>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, UnityEngine.AssetBundle>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MapLoadedAssetBundle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LoaderTicket gen_to_be_invoked = (LoaderTicket)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MapLoadedAssetBundle = (System.Collections.Generic.Dictionary<string, UnityEngine.AssetBundle>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, UnityEngine.AssetBundle>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
