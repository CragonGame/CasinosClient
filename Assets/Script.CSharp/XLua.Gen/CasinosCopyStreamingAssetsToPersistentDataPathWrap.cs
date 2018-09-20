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
    public class CasinosCopyStreamingAssetsToPersistentDataPathWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.CopyStreamingAssetsToPersistentDataPath);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 3, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "startCopy", _m_startCopy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "update", _m_update);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentDataFile", _g_get_CurrentDataFile);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TotalCount", _g_get_TotalCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentIndex", _g_get_CurrentIndex);
            
			
			
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
					
					Casinos.CopyStreamingAssetsToPersistentDataPath gen_ret = new Casinos.CopyStreamingAssetsToPersistentDataPath();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.CopyStreamingAssetsToPersistentDataPath constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_startCopy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CopyStreamingAssetsToPersistentDataPath gen_to_be_invoked = (Casinos.CopyStreamingAssetsToPersistentDataPath)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<Casinos.StreamingAssetsFileInfo> _list_need_copyfile = (System.Collections.Generic.List<Casinos.StreamingAssetsFileInfo>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<Casinos.StreamingAssetsFileInfo>));
                    System.Action<int, int> _action_pro = translator.GetDelegate<System.Action<int, int>>(L, 3);
                    System.Action _action_copydown = translator.GetDelegate<System.Action>(L, 4);
                    
                    gen_to_be_invoked.startCopy( _list_need_copyfile, _action_pro, _action_copydown );
                    
                    
                    
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
            
            
                Casinos.CopyStreamingAssetsToPersistentDataPath gen_to_be_invoked = (Casinos.CopyStreamingAssetsToPersistentDataPath)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _g_get_CurrentDataFile(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CopyStreamingAssetsToPersistentDataPath gen_to_be_invoked = (Casinos.CopyStreamingAssetsToPersistentDataPath)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.CurrentDataFile);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TotalCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CopyStreamingAssetsToPersistentDataPath gen_to_be_invoked = (Casinos.CopyStreamingAssetsToPersistentDataPath)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.TotalCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CopyStreamingAssetsToPersistentDataPath gen_to_be_invoked = (Casinos.CopyStreamingAssetsToPersistentDataPath)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.CurrentIndex);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
