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
    public class SystemIOFileWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.IO.File);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 40, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "AppendAllText", _m_AppendAllText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AppendText", _m_AppendText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Copy", _m_Copy_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Create", _m_Create_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateText", _m_CreateText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Delete", _m_Delete_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Exists", _m_Exists_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAccessControl", _m_GetAccessControl_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAttributes", _m_GetAttributes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCreationTime", _m_GetCreationTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCreationTimeUtc", _m_GetCreationTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastAccessTime", _m_GetLastAccessTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastAccessTimeUtc", _m_GetLastAccessTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastWriteTime", _m_GetLastWriteTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastWriteTimeUtc", _m_GetLastWriteTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Move", _m_Move_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Open", _m_Open_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenRead", _m_OpenRead_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenText", _m_OpenText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenWrite", _m_OpenWrite_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Replace", _m_Replace_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAccessControl", _m_SetAccessControl_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAttributes", _m_SetAttributes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCreationTime", _m_SetCreationTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCreationTimeUtc", _m_SetCreationTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastAccessTime", _m_SetLastAccessTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastAccessTimeUtc", _m_SetLastAccessTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastWriteTime", _m_SetLastWriteTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastWriteTimeUtc", _m_SetLastWriteTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadAllBytes", _m_ReadAllBytes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadAllLines", _m_ReadAllLines_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadAllText", _m_ReadAllText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteAllBytes", _m_WriteAllBytes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteAllLines", _m_WriteAllLines_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteAllText", _m_WriteAllText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Encrypt", _m_Encrypt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Decrypt", _m_Decrypt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadLines", _m_ReadLines_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AppendAllLines", _m_AppendAllLines_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.IO.File does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendAllText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _contents = LuaAPI.lua_tostring(L, 2);
                    
                    System.IO.File.AppendAllText( _path, _contents );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _contents = LuaAPI.lua_tostring(L, 2);
                    System.Text.Encoding _encoding = (System.Text.Encoding)translator.GetObject(L, 3, typeof(System.Text.Encoding));
                    
                    System.IO.File.AppendAllText( _path, _contents, _encoding );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.AppendAllText!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.StreamWriter gen_ret = System.IO.File.AppendText( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Copy_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string _destFileName = LuaAPI.lua_tostring(L, 2);
                    
                    System.IO.File.Copy( _sourceFileName, _destFileName );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string _destFileName = LuaAPI.lua_tostring(L, 2);
                    bool _overwrite = LuaAPI.lua_toboolean(L, 3);
                    
                    System.IO.File.Copy( _sourceFileName, _destFileName, _overwrite );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.Copy!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Create_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.FileStream gen_ret = System.IO.File.Create( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    int _bufferSize = LuaAPI.xlua_tointeger(L, 2);
                    
                        System.IO.FileStream gen_ret = System.IO.File.Create( _path, _bufferSize );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.IO.FileOptions>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    int _bufferSize = LuaAPI.xlua_tointeger(L, 2);
                    System.IO.FileOptions _options;translator.Get(L, 3, out _options);
                    
                        System.IO.FileStream gen_ret = System.IO.File.Create( _path, _bufferSize, _options );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.IO.FileOptions>(L, 3)&& translator.Assignable<System.Security.AccessControl.FileSecurity>(L, 4)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    int _bufferSize = LuaAPI.xlua_tointeger(L, 2);
                    System.IO.FileOptions _options;translator.Get(L, 3, out _options);
                    System.Security.AccessControl.FileSecurity _fileSecurity = (System.Security.AccessControl.FileSecurity)translator.GetObject(L, 4, typeof(System.Security.AccessControl.FileSecurity));
                    
                        System.IO.FileStream gen_ret = System.IO.File.Create( _path, _bufferSize, _options, _fileSecurity );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.Create!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.StreamWriter gen_ret = System.IO.File.CreateText( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Delete_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    System.IO.File.Delete( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Exists_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = System.IO.File.Exists( _path );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAccessControl_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.Security.AccessControl.FileSecurity gen_ret = System.IO.File.GetAccessControl( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Security.AccessControl.AccessControlSections>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Security.AccessControl.AccessControlSections _includeSections;translator.Get(L, 2, out _includeSections);
                    
                        System.Security.AccessControl.FileSecurity gen_ret = System.IO.File.GetAccessControl( _path, _includeSections );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.GetAccessControl!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAttributes_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.FileAttributes gen_ret = System.IO.File.GetAttributes( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCreationTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime gen_ret = System.IO.File.GetCreationTime( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCreationTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime gen_ret = System.IO.File.GetCreationTimeUtc( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLastAccessTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime gen_ret = System.IO.File.GetLastAccessTime( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLastAccessTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime gen_ret = System.IO.File.GetLastAccessTimeUtc( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLastWriteTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime gen_ret = System.IO.File.GetLastWriteTime( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLastWriteTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime gen_ret = System.IO.File.GetLastWriteTimeUtc( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Move_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string _destFileName = LuaAPI.lua_tostring(L, 2);
                    
                    System.IO.File.Move( _sourceFileName, _destFileName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Open_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.FileMode>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.IO.FileMode _mode;translator.Get(L, 2, out _mode);
                    
                        System.IO.FileStream gen_ret = System.IO.File.Open( _path, _mode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.FileMode>(L, 2)&& translator.Assignable<System.IO.FileAccess>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.IO.FileMode _mode;translator.Get(L, 2, out _mode);
                    System.IO.FileAccess _access;translator.Get(L, 3, out _access);
                    
                        System.IO.FileStream gen_ret = System.IO.File.Open( _path, _mode, _access );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.FileMode>(L, 2)&& translator.Assignable<System.IO.FileAccess>(L, 3)&& translator.Assignable<System.IO.FileShare>(L, 4)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.IO.FileMode _mode;translator.Get(L, 2, out _mode);
                    System.IO.FileAccess _access;translator.Get(L, 3, out _access);
                    System.IO.FileShare _share;translator.Get(L, 4, out _share);
                    
                        System.IO.FileStream gen_ret = System.IO.File.Open( _path, _mode, _access, _share );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.Open!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenRead_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.FileStream gen_ret = System.IO.File.OpenRead( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.StreamReader gen_ret = System.IO.File.OpenText( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenWrite_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.FileStream gen_ret = System.IO.File.OpenWrite( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Replace_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string _destinationFileName = LuaAPI.lua_tostring(L, 2);
                    string _destinationBackupFileName = LuaAPI.lua_tostring(L, 3);
                    
                    System.IO.File.Replace( _sourceFileName, _destinationFileName, _destinationBackupFileName );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string _destinationFileName = LuaAPI.lua_tostring(L, 2);
                    string _destinationBackupFileName = LuaAPI.lua_tostring(L, 3);
                    bool _ignoreMetadataErrors = LuaAPI.lua_toboolean(L, 4);
                    
                    System.IO.File.Replace( _sourceFileName, _destinationFileName, _destinationBackupFileName, _ignoreMetadataErrors );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.Replace!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAccessControl_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Security.AccessControl.FileSecurity _fileSecurity = (System.Security.AccessControl.FileSecurity)translator.GetObject(L, 2, typeof(System.Security.AccessControl.FileSecurity));
                    
                    System.IO.File.SetAccessControl( _path, _fileSecurity );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAttributes_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.IO.FileAttributes _fileAttributes;translator.Get(L, 2, out _fileAttributes);
                    
                    System.IO.File.SetAttributes( _path, _fileAttributes );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCreationTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime _creationTime;translator.Get(L, 2, out _creationTime);
                    
                    System.IO.File.SetCreationTime( _path, _creationTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCreationTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime _creationTimeUtc;translator.Get(L, 2, out _creationTimeUtc);
                    
                    System.IO.File.SetCreationTimeUtc( _path, _creationTimeUtc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLastAccessTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime _lastAccessTime;translator.Get(L, 2, out _lastAccessTime);
                    
                    System.IO.File.SetLastAccessTime( _path, _lastAccessTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLastAccessTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime _lastAccessTimeUtc;translator.Get(L, 2, out _lastAccessTimeUtc);
                    
                    System.IO.File.SetLastAccessTimeUtc( _path, _lastAccessTimeUtc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLastWriteTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime _lastWriteTime;translator.Get(L, 2, out _lastWriteTime);
                    
                    System.IO.File.SetLastWriteTime( _path, _lastWriteTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLastWriteTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime _lastWriteTimeUtc;translator.Get(L, 2, out _lastWriteTimeUtc);
                    
                    System.IO.File.SetLastWriteTimeUtc( _path, _lastWriteTimeUtc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAllBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] gen_ret = System.IO.File.ReadAllBytes( _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAllLines_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string[] gen_ret = System.IO.File.ReadAllLines( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Text.Encoding _encoding = (System.Text.Encoding)translator.GetObject(L, 2, typeof(System.Text.Encoding));
                    
                        string[] gen_ret = System.IO.File.ReadAllLines( _path, _encoding );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.ReadAllLines!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAllText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.IO.File.ReadAllText( _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Text.Encoding _encoding = (System.Text.Encoding)translator.GetObject(L, 2, typeof(System.Text.Encoding));
                    
                        string gen_ret = System.IO.File.ReadAllText( _path, _encoding );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.ReadAllText!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteAllBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 2);
                    
                    System.IO.File.WriteAllBytes( _path, _bytes );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteAllLines_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<string[]>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string[] _contents = (string[])translator.GetObject(L, 2, typeof(string[]));
                    
                    System.IO.File.WriteAllLines( _path, _contents );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.IEnumerable<string>>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.IEnumerable<string> _contents = (System.Collections.Generic.IEnumerable<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEnumerable<string>));
                    
                    System.IO.File.WriteAllLines( _path, _contents );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<string[]>(L, 2)&& translator.Assignable<System.Text.Encoding>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string[] _contents = (string[])translator.GetObject(L, 2, typeof(string[]));
                    System.Text.Encoding _encoding = (System.Text.Encoding)translator.GetObject(L, 3, typeof(System.Text.Encoding));
                    
                    System.IO.File.WriteAllLines( _path, _contents, _encoding );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.IEnumerable<string>>(L, 2)&& translator.Assignable<System.Text.Encoding>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.IEnumerable<string> _contents = (System.Collections.Generic.IEnumerable<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEnumerable<string>));
                    System.Text.Encoding _encoding = (System.Text.Encoding)translator.GetObject(L, 3, typeof(System.Text.Encoding));
                    
                    System.IO.File.WriteAllLines( _path, _contents, _encoding );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.WriteAllLines!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteAllText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _contents = LuaAPI.lua_tostring(L, 2);
                    
                    System.IO.File.WriteAllText( _path, _contents );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _contents = LuaAPI.lua_tostring(L, 2);
                    System.Text.Encoding _encoding = (System.Text.Encoding)translator.GetObject(L, 3, typeof(System.Text.Encoding));
                    
                    System.IO.File.WriteAllText( _path, _contents, _encoding );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.WriteAllText!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Encrypt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    System.IO.File.Encrypt( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Decrypt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    System.IO.File.Decrypt( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadLines_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.File.ReadLines( _path );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Text.Encoding _encoding = (System.Text.Encoding)translator.GetObject(L, 2, typeof(System.Text.Encoding));
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.File.ReadLines( _path, _encoding );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.ReadLines!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendAllLines_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.IEnumerable<string>>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.IEnumerable<string> _contents = (System.Collections.Generic.IEnumerable<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEnumerable<string>));
                    
                    System.IO.File.AppendAllLines( _path, _contents );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.IEnumerable<string>>(L, 2)&& translator.Assignable<System.Text.Encoding>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.IEnumerable<string> _contents = (System.Collections.Generic.IEnumerable<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEnumerable<string>));
                    System.Text.Encoding _encoding = (System.Text.Encoding)translator.GetObject(L, 3, typeof(System.Text.Encoding));
                    
                    System.IO.File.AppendAllLines( _path, _contents, _encoding );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.AppendAllLines!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
