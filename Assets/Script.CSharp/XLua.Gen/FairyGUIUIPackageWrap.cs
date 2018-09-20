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
    public class FairyGUIUIPackageWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.UIPackage);
			Utils.BeginObjectRegister(type, L, translator, 0, 9, 5, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadAllAssets", _m_LoadAllAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnloadAssets", _m_UnloadAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReloadAssets", _m_ReloadAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateObject", _m_CreateObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateObjectAsync", _m_CreateObjectAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetItemAsset", _m_GetItemAsset);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetItems", _m_GetItems);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetItem", _m_GetItem);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetItemByName", _m_GetItemByName);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "id", _g_get_id);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "name", _g_get_name);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetPath", _g_get_assetPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "customId", _g_get_customId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "resBundle", _g_get_resBundle);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "customId", _s_set_customId);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 17, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetById", _m_GetById_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetByName", _m_GetByName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddPackage", _m_AddPackage_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemovePackage", _m_RemovePackage_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveAllPackages", _m_RemoveAllPackages_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPackages", _m_GetPackages_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateObject", _m_CreateObject_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateObjectFromURL", _m_CreateObjectFromURL_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateObjectAsync", _m_CreateObjectAsync_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetItemAsset", _m_GetItemAsset_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetItemAssetByURL", _m_GetItemAssetByURL_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetItemURL", _m_GetItemURL_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetItemByURL", _m_GetItemByURL_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "NormalizeURL", _m_NormalizeURL_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetStringsSource", _m_SetStringsSource_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "URL_PREFIX", FairyGUI.UIPackage.URL_PREFIX);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					FairyGUI.UIPackage gen_ret = new FairyGUI.UIPackage();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.UIPackage constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetById_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _id = LuaAPI.lua_tostring(L, 1);
                    
                        FairyGUI.UIPackage gen_ret = FairyGUI.UIPackage.GetById( _id );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetByName_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 1);
                    
                        FairyGUI.UIPackage gen_ret = FairyGUI.UIPackage.GetByName( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddPackage_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.AssetBundle>(L, 1)) 
                {
                    UnityEngine.AssetBundle _bundle = (UnityEngine.AssetBundle)translator.GetObject(L, 1, typeof(UnityEngine.AssetBundle));
                    
                        FairyGUI.UIPackage gen_ret = FairyGUI.UIPackage.AddPackage( _bundle );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _descFilePath = LuaAPI.lua_tostring(L, 1);
                    
                        FairyGUI.UIPackage gen_ret = FairyGUI.UIPackage.AddPackage( _descFilePath );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.AssetBundle>(L, 1)&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)) 
                {
                    UnityEngine.AssetBundle _desc = (UnityEngine.AssetBundle)translator.GetObject(L, 1, typeof(UnityEngine.AssetBundle));
                    UnityEngine.AssetBundle _res = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    
                        FairyGUI.UIPackage gen_ret = FairyGUI.UIPackage.AddPackage( _desc, _res );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<FairyGUI.UIPackage.LoadResource>(L, 2)) 
                {
                    string _assetPath = LuaAPI.lua_tostring(L, 1);
                    FairyGUI.UIPackage.LoadResource _loadFunc = translator.GetDelegate<FairyGUI.UIPackage.LoadResource>(L, 2);
                    
                        FairyGUI.UIPackage gen_ret = FairyGUI.UIPackage.AddPackage( _assetPath, _loadFunc );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.AssetBundle>(L, 1)&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityEngine.AssetBundle _desc = (UnityEngine.AssetBundle)translator.GetObject(L, 1, typeof(UnityEngine.AssetBundle));
                    UnityEngine.AssetBundle _res = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    string _mainAssetName = LuaAPI.lua_tostring(L, 3);
                    
                        FairyGUI.UIPackage gen_ret = FairyGUI.UIPackage.AddPackage( _desc, _res, _mainAssetName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<FairyGUI.UIPackage.LoadResource>(L, 3)) 
                {
                    byte[] _descData = LuaAPI.lua_tobytes(L, 1);
                    string _assetNamePrefix = LuaAPI.lua_tostring(L, 2);
                    FairyGUI.UIPackage.LoadResource _loadFunc = translator.GetDelegate<FairyGUI.UIPackage.LoadResource>(L, 3);
                    
                        FairyGUI.UIPackage gen_ret = FairyGUI.UIPackage.AddPackage( _descData, _assetNamePrefix, _loadFunc );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.UIPackage.AddPackage!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemovePackage_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _packageIdOrName = LuaAPI.lua_tostring(L, 1);
                    
                    FairyGUI.UIPackage.RemovePackage( _packageIdOrName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAllPackages_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    FairyGUI.UIPackage.RemoveAllPackages(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPackages_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        System.Collections.Generic.List<FairyGUI.UIPackage> gen_ret = FairyGUI.UIPackage.GetPackages(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateObject_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _pkgName = LuaAPI.lua_tostring(L, 1);
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.GObject gen_ret = FairyGUI.UIPackage.CreateObject( _pkgName, _resName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Type>(L, 3)) 
                {
                    string _pkgName = LuaAPI.lua_tostring(L, 1);
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    System.Type _userClass = (System.Type)translator.GetObject(L, 3, typeof(System.Type));
                    
                        FairyGUI.GObject gen_ret = FairyGUI.UIPackage.CreateObject( _pkgName, _resName, _userClass );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.UIPackage.CreateObject!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateObjectFromURL_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    
                        FairyGUI.GObject gen_ret = FairyGUI.UIPackage.CreateObjectFromURL( _url );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Type>(L, 2)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    System.Type _userClass = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        FairyGUI.GObject gen_ret = FairyGUI.UIPackage.CreateObjectFromURL( _url, _userClass );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<FairyGUI.UIPackage.CreateObjectCallback>(L, 2)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    FairyGUI.UIPackage.CreateObjectCallback _callback = translator.GetDelegate<FairyGUI.UIPackage.CreateObjectCallback>(L, 2);
                    
                    FairyGUI.UIPackage.CreateObjectFromURL( _url, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.UIPackage.CreateObjectFromURL!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateObjectAsync_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _pkgName = LuaAPI.lua_tostring(L, 1);
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    FairyGUI.UIPackage.CreateObjectCallback _callback = translator.GetDelegate<FairyGUI.UIPackage.CreateObjectCallback>(L, 3);
                    
                    FairyGUI.UIPackage.CreateObjectAsync( _pkgName, _resName, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemAsset_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _pkgName = LuaAPI.lua_tostring(L, 1);
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    
                        object gen_ret = FairyGUI.UIPackage.GetItemAsset( _pkgName, _resName );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemAssetByURL_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    
                        object gen_ret = FairyGUI.UIPackage.GetItemAssetByURL( _url );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemURL_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _pkgName = LuaAPI.lua_tostring(L, 1);
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = FairyGUI.UIPackage.GetItemURL( _pkgName, _resName );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemByURL_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    
                        FairyGUI.PackageItem gen_ret = FairyGUI.UIPackage.GetItemByURL( _url );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NormalizeURL_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = FairyGUI.UIPackage.NormalizeURL( _url );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetStringsSource_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.Utils.XML _source = (FairyGUI.Utils.XML)translator.GetObject(L, 1, typeof(FairyGUI.Utils.XML));
                    
                    FairyGUI.UIPackage.SetStringsSource( _source );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAllAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.LoadAllAssets(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnloadAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UnloadAssets(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReloadAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.ReloadAssets(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)) 
                {
                    UnityEngine.AssetBundle _resBundle = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    
                    gen_to_be_invoked.ReloadAssets( _resBundle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.UIPackage.ReloadAssets!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.CreateObject( _resName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Type>(L, 3)) 
                {
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    System.Type _userClass = (System.Type)translator.GetObject(L, 3, typeof(System.Type));
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.CreateObject( _resName, _userClass );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.UIPackage.CreateObject!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateObjectAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    FairyGUI.UIPackage.CreateObjectCallback _callback = translator.GetDelegate<FairyGUI.UIPackage.CreateObjectCallback>(L, 3);
                    
                    gen_to_be_invoked.CreateObjectAsync( _resName, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemAsset(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _resName = LuaAPI.lua_tostring(L, 2);
                    
                        object gen_ret = gen_to_be_invoked.GetItemAsset( _resName );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.PackageItem>(L, 2)) 
                {
                    FairyGUI.PackageItem _item = (FairyGUI.PackageItem)translator.GetObject(L, 2, typeof(FairyGUI.PackageItem));
                    
                        object gen_ret = gen_to_be_invoked.GetItemAsset( _item );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.UIPackage.GetItemAsset!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItems(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Generic.List<FairyGUI.PackageItem> gen_ret = gen_to_be_invoked.GetItems(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItem(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _itemId = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.PackageItem gen_ret = gen_to_be_invoked.GetItem( _itemId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemByName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _itemName = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.PackageItem gen_ret = gen_to_be_invoked.GetItemByName( _itemName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.id);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.name);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.assetPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_customId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.customId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_resBundle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.resBundle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_customId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPackage gen_to_be_invoked = (FairyGUI.UIPackage)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.customId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
