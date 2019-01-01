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
    public class CasinosLuaMgrWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.LuaMgr);
			Utils.BeginObjectRegister(type, L, translator, 0, 29, 1, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Release", _m_Release);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Launch", _m_Launch);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadLuaFromAssetBundle", _m_LoadLuaFromAssetBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadLuaFromBytes", _m_LoadLuaFromBytes);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadLuaFromResources", _m_LoadLuaFromResources);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadLuaFromRawDir", _m_LoadLuaFromRawDir);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadLuaFromRawDir2", _m_LoadLuaFromRawDir2);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DoString", _m_DoString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLuaTable", _m_GetLuaTable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadLocalBundleAsync", _m_LoadLocalBundleAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadAllText", _m_ReadAllText);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteFileFromWWW", _m_WriteFileFromWWW);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SpliteStr", _m_SpliteStr);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyGameObject", _m_DestroyGameObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Vibrate", _m_Vibrate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSystemLanguageAsString", _m_GetSystemLanguageAsString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetClipBoard", _m_SetClipBoard);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetClipBoard", _m_GetClipBoard);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateQRCode", _m_CreateQRCode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SaveTextureToFile", _m_SaveTextureToFile);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateWebView", _m_CreateWebView);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateWebView2", _m_CreateWebView2);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToBase64String", _m_ToBase64String);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FromBase64String", _m_FromBase64String);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "_CSharpCallOnAndroidQuitConfirm", _m__CSharpCallOnAndroidQuitConfirm);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "_CSharpCallOnApplicationPause", _m__CSharpCallOnApplicationPause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "_CSharpCallOnApplicationFocus", _m__CSharpCallOnApplicationFocus);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "_CSharpCallOnSocketClose", _m__CSharpCallOnSocketClose);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "LuaEnv", _g_get_LuaEnv);
            
			
			
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
					
					Casinos.LuaMgr gen_ret = new Casinos.LuaMgr();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.LuaMgr constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Release(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Release(  );
                    
                    
                    
                    return 0;
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
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _elapsed_tm = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Update( _elapsed_tm );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Launch(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _is_load_launchlua_from_resources = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Launch( _is_load_launchlua_from_resources );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadLuaFromAssetBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.AssetBundle _ab = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    
                    gen_to_be_invoked.LoadLuaFromAssetBundle( _ab );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadLuaFromBytes(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _file_name = LuaAPI.lua_tostring(L, 2);
                    string _text = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.LoadLuaFromBytes( _file_name, _text );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadLuaFromResources(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _filepath = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.LoadLuaFromResources( _filepath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadLuaFromRawDir(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.LoadLuaFromRawDir( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadLuaFromRawDir2(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string[] _list_path = (string[])translator.GetObject(L, 2, typeof(string[]));
                    
                    gen_to_be_invoked.LoadLuaFromRawDir2( _list_path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _luafile_name = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.DoString( _luafile_name );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLuaTable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        XLua.LuaTable gen_ret = gen_to_be_invoked.GetLuaTable( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadLocalBundleAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    XLua.LuaTable _lua_table = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
                    XLua.LuaTable _need_load_ab_path = (XLua.LuaTable)translator.GetObject(L, 3, typeof(XLua.LuaTable));
                    Casinos.DelegateLua4 _loaded_callback = translator.GetDelegate<Casinos.DelegateLua4>(L, 4);
                    
                        LoaderTicket gen_ret = gen_to_be_invoked.LoadLocalBundleAsync( _lua_table, _need_load_ab_path, _loaded_callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAllText(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _full_filename = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.ReadAllText( _full_filename );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteFileFromWWW(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.WWW _www = (UnityEngine.WWW)translator.GetObject(L, 3, typeof(UnityEngine.WWW));
                    
                    gen_to_be_invoked.WriteFileFromWWW( _path, _www );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SpliteStr(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 2);
                    string _splite_s = LuaAPI.lua_tostring(L, 3);
                    
                        XLua.LuaTable gen_ret = gen_to_be_invoked.SpliteStr( _str, _splite_s );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyGameObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _o = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    gen_to_be_invoked.DestroyGameObject( _o );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Vibrate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Vibrate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSystemLanguageAsString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.GetSystemLanguageAsString(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetClipBoard(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetClipBoard( _text );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetClipBoard(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.GetClipBoard(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateQRCode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _encoding_text = LuaAPI.lua_tostring(L, 2);
                    int _width = LuaAPI.xlua_tointeger(L, 3);
                    int _height = LuaAPI.xlua_tointeger(L, 4);
                    
                        UnityEngine.Color32[] gen_ret = gen_to_be_invoked.CreateQRCode( _encoding_text, _width, _height );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SaveTextureToFile(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Texture2D _texture = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
                    string _file_name = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.SaveTextureToFile( _texture, _file_name );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateWebView(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _go_name = LuaAPI.lua_tostring(L, 2);
                    
                        UniWebView gen_ret = gen_to_be_invoked.CreateWebView( _go_name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateWebView2(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _go_name = LuaAPI.lua_tostring(L, 2);
                    
                        UniWebView gen_ret = gen_to_be_invoked.CreateWebView2( _go_name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToBase64String(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _data = LuaAPI.lua_tobytes(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.ToBase64String( _data );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FromBase64String(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _data = LuaAPI.lua_tostring(L, 2);
                    
                        byte[] gen_ret = gen_to_be_invoked.FromBase64String( _data );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m__CSharpCallOnAndroidQuitConfirm(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked._CSharpCallOnAndroidQuitConfirm(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m__CSharpCallOnApplicationPause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _pause = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked._CSharpCallOnApplicationPause( _pause );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m__CSharpCallOnApplicationFocus(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _focus_status = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked._CSharpCallOnApplicationFocus( _focus_status );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m__CSharpCallOnSocketClose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked._CSharpCallOnSocketClose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.LuaMgr gen_to_be_invoked = (Casinos.LuaMgr)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LuaEnv);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
