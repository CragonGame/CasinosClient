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
    public class CasinosPathMgrWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.PathMgr);
			Utils.BeginObjectRegister(type, L, translator, 0, 8, 17, 14);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getWWWPersistentDataPath", _m_getWWWPersistentDataPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "combineWWWPersistentDataPath", _m_combineWWWPersistentDataPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getPersistentDataPath", _m_getPersistentDataPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "combinePersistentDataPath", _m_combinePersistentDataPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getStreamingAssetsPath", _m_getStreamingAssetsPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "combineStreamingAssetsPath", _m_combineStreamingAssetsPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getWWWStreamingAssetsPath", _m_getWWWStreamingAssetsPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "combineWWWStreamingAssetsPath", _m_combineWWWStreamingAssetsPath);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ForceUseDirResourcesLaunch", _g_get_ForceUseDirResourcesLaunch);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ForceUseDirDataOss", _g_get_ForceUseDirDataOss);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PathAssets", _g_get_PathAssets);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PathSettings", _g_get_PathSettings);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PathSettingsUser", _g_get_PathSettingsUser);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirLaunchLua", _g_get_DirLaunchLua);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirLaunchLuaType", _g_get_DirLaunchLuaType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirLaunchAb", _g_get_DirLaunchAb);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirLaunchAbType", _g_get_DirLaunchAbType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirLuaRoot", _g_get_DirLuaRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirRawRoot", _g_get_DirRawRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirAbRoot", _g_get_DirAbRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirAbUi", _g_get_DirAbUi);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirAbCard", _g_get_DirAbCard);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirAbAudio", _g_get_DirAbAudio);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirAbItem", _g_get_DirAbItem);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DirAbParticle", _g_get_DirAbParticle);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "ForceUseDirResourcesLaunch", _s_set_ForceUseDirResourcesLaunch);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ForceUseDirDataOss", _s_set_ForceUseDirDataOss);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirLaunchLua", _s_set_DirLaunchLua);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirLaunchLuaType", _s_set_DirLaunchLuaType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirLaunchAb", _s_set_DirLaunchAb);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirLaunchAbType", _s_set_DirLaunchAbType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirLuaRoot", _s_set_DirLuaRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirRawRoot", _s_set_DirRawRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirAbRoot", _s_set_DirAbRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirAbUi", _s_set_DirAbUi);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirAbCard", _s_set_DirAbCard);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirAbAudio", _s_set_DirAbAudio);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirAbItem", _s_set_DirAbItem);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DirAbParticle", _s_set_DirAbParticle);
            
			
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
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<Casinos._eEditorRunSourcePlatform>(L, 2) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4))
				{
					Casinos._eEditorRunSourcePlatform _editor_mode_runsources_platform;translator.Get(L, 2, out _editor_mode_runsources_platform);
					bool _force_use_resouceslaunch = LuaAPI.lua_toboolean(L, 3);
					bool _force_use_dataoss = LuaAPI.lua_toboolean(L, 4);
					
					Casinos.PathMgr gen_ret = new Casinos.PathMgr(_editor_mode_runsources_platform, _force_use_resouceslaunch, _force_use_dataoss);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.PathMgr constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getWWWPersistentDataPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.getWWWPersistentDataPath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_combineWWWPersistentDataPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.combineWWWPersistentDataPath( _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getPersistentDataPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.getPersistentDataPath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_combinePersistentDataPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    bool _for_lua = LuaAPI.lua_toboolean(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.combinePersistentDataPath( _path, _for_lua );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.combinePersistentDataPath( _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.PathMgr.combinePersistentDataPath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getStreamingAssetsPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.getStreamingAssetsPath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_combineStreamingAssetsPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.combineStreamingAssetsPath( _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getWWWStreamingAssetsPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.getWWWStreamingAssetsPath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_combineWWWStreamingAssetsPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.combineWWWStreamingAssetsPath( _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ForceUseDirResourcesLaunch(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.ForceUseDirResourcesLaunch);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ForceUseDirDataOss(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.ForceUseDirDataOss);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PathAssets(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.PathAssets);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PathSettings(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.PathSettings);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PathSettingsUser(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.PathSettingsUser);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirLaunchLua(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirLaunchLua);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirLaunchLuaType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                translator.PushCasinosDirType(L, gen_to_be_invoked.DirLaunchLuaType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirLaunchAb(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirLaunchAb);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirLaunchAbType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                translator.PushCasinosDirType(L, gen_to_be_invoked.DirLaunchAbType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirLuaRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirLuaRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirRawRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirRawRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirAbRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirAbRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirAbUi(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirAbUi);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirAbCard(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirAbCard);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirAbAudio(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirAbAudio);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirAbItem(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirAbItem);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DirAbParticle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DirAbParticle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ForceUseDirResourcesLaunch(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ForceUseDirResourcesLaunch = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ForceUseDirDataOss(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ForceUseDirDataOss = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirLaunchLua(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirLaunchLua = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirLaunchLuaType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                Casinos.DirType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.DirLaunchLuaType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirLaunchAb(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirLaunchAb = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirLaunchAbType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                Casinos.DirType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.DirLaunchAbType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirLuaRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirLuaRoot = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirRawRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirRawRoot = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirAbRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirAbRoot = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirAbUi(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirAbUi = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirAbCard(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirAbCard = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirAbAudio(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirAbAudio = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirAbItem(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirAbItem = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DirAbParticle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.PathMgr gen_to_be_invoked = (Casinos.PathMgr)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DirAbParticle = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
