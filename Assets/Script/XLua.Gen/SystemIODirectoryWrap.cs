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
    public class SystemIODirectoryWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.IO.Directory);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 30, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFiles", _m_GetFiles_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetDirectories", _m_GetDirectories_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileSystemEntries", _m_GetFileSystemEntries_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EnumerateDirectories", _m_EnumerateDirectories_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EnumerateFiles", _m_EnumerateFiles_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EnumerateFileSystemEntries", _m_EnumerateFileSystemEntries_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetDirectoryRoot", _m_GetDirectoryRoot_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateDirectory", _m_CreateDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Delete", _m_Delete_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Exists", _m_Exists_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastAccessTime", _m_GetLastAccessTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastAccessTimeUtc", _m_GetLastAccessTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastWriteTime", _m_GetLastWriteTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastWriteTimeUtc", _m_GetLastWriteTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCreationTime", _m_GetCreationTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCreationTimeUtc", _m_GetCreationTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCurrentDirectory", _m_GetCurrentDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLogicalDrives", _m_GetLogicalDrives_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetParent", _m_GetParent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Move", _m_Move_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAccessControl", _m_SetAccessControl_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCreationTime", _m_SetCreationTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCreationTimeUtc", _m_SetCreationTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCurrentDirectory", _m_SetCurrentDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastAccessTime", _m_SetLastAccessTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastAccessTimeUtc", _m_SetLastAccessTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastWriteTime", _m_SetLastWriteTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastWriteTimeUtc", _m_SetLastWriteTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAccessControl", _m_GetAccessControl_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.IO.Directory does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFiles_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string[] gen_ret = System.IO.Directory.GetFiles( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    
                        string[] gen_ret = System.IO.Directory.GetFiles( _path, _searchPattern );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.SearchOption>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    System.IO.SearchOption _searchOption;translator.Get(L, 3, out _searchOption);
                    
                        string[] gen_ret = System.IO.Directory.GetFiles( _path, _searchPattern, _searchOption );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Directory.GetFiles!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDirectories_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string[] gen_ret = System.IO.Directory.GetDirectories( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    
                        string[] gen_ret = System.IO.Directory.GetDirectories( _path, _searchPattern );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.SearchOption>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    System.IO.SearchOption _searchOption;translator.Get(L, 3, out _searchOption);
                    
                        string[] gen_ret = System.IO.Directory.GetDirectories( _path, _searchPattern, _searchOption );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Directory.GetDirectories!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileSystemEntries_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string[] gen_ret = System.IO.Directory.GetFileSystemEntries( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    
                        string[] gen_ret = System.IO.Directory.GetFileSystemEntries( _path, _searchPattern );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.SearchOption>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    System.IO.SearchOption _searchOption;translator.Get(L, 3, out _searchOption);
                    
                        string[] gen_ret = System.IO.Directory.GetFileSystemEntries( _path, _searchPattern, _searchOption );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Directory.GetFileSystemEntries!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnumerateDirectories_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.Directory.EnumerateDirectories( _path );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.Directory.EnumerateDirectories( _path, _searchPattern );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.SearchOption>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    System.IO.SearchOption _searchOption;translator.Get(L, 3, out _searchOption);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.Directory.EnumerateDirectories( _path, _searchPattern, _searchOption );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Directory.EnumerateDirectories!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnumerateFiles_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.Directory.EnumerateFiles( _path );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.Directory.EnumerateFiles( _path, _searchPattern );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.SearchOption>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    System.IO.SearchOption _searchOption;translator.Get(L, 3, out _searchOption);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.Directory.EnumerateFiles( _path, _searchPattern, _searchOption );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Directory.EnumerateFiles!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnumerateFileSystemEntries_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.Directory.EnumerateFileSystemEntries( _path );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.Directory.EnumerateFileSystemEntries( _path, _searchPattern );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.SearchOption>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _searchPattern = LuaAPI.lua_tostring(L, 2);
                    System.IO.SearchOption _searchOption;translator.Get(L, 3, out _searchOption);
                    
                        System.Collections.Generic.IEnumerable<string> gen_ret = System.IO.Directory.EnumerateFileSystemEntries( _path, _searchPattern, _searchOption );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Directory.EnumerateFileSystemEntries!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDirectoryRoot_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.IO.Directory.GetDirectoryRoot( _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.DirectoryInfo gen_ret = System.IO.Directory.CreateDirectory( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Security.AccessControl.DirectorySecurity>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Security.AccessControl.DirectorySecurity _directorySecurity = (System.Security.AccessControl.DirectorySecurity)translator.GetObject(L, 2, typeof(System.Security.AccessControl.DirectorySecurity));
                    
                        System.IO.DirectoryInfo gen_ret = System.IO.Directory.CreateDirectory( _path, _directorySecurity );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Directory.CreateDirectory!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Delete_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    System.IO.Directory.Delete( _path );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    bool _recursive = LuaAPI.lua_toboolean(L, 2);
                    
                    System.IO.Directory.Delete( _path, _recursive );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Directory.Delete!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Exists_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = System.IO.Directory.Exists( _path );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
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
                    
                        System.DateTime gen_ret = System.IO.Directory.GetLastAccessTime( _path );
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
                    
                        System.DateTime gen_ret = System.IO.Directory.GetLastAccessTimeUtc( _path );
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
                    
                        System.DateTime gen_ret = System.IO.Directory.GetLastWriteTime( _path );
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
                    
                        System.DateTime gen_ret = System.IO.Directory.GetLastWriteTimeUtc( _path );
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
                    
                        System.DateTime gen_ret = System.IO.Directory.GetCreationTime( _path );
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
                    
                        System.DateTime gen_ret = System.IO.Directory.GetCreationTimeUtc( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCurrentDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = System.IO.Directory.GetCurrentDirectory(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLogicalDrives_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        string[] gen_ret = System.IO.Directory.GetLogicalDrives(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetParent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.DirectoryInfo gen_ret = System.IO.Directory.GetParent( _path );
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
                    string _sourceDirName = LuaAPI.lua_tostring(L, 1);
                    string _destDirName = LuaAPI.lua_tostring(L, 2);
                    
                    System.IO.Directory.Move( _sourceDirName, _destDirName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAccessControl_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Security.AccessControl.DirectorySecurity _directorySecurity = (System.Security.AccessControl.DirectorySecurity)translator.GetObject(L, 2, typeof(System.Security.AccessControl.DirectorySecurity));
                    
                    System.IO.Directory.SetAccessControl( _path, _directorySecurity );
                    
                    
                    
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
                    
                    System.IO.Directory.SetCreationTime( _path, _creationTime );
                    
                    
                    
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
                    
                    System.IO.Directory.SetCreationTimeUtc( _path, _creationTimeUtc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCurrentDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    System.IO.Directory.SetCurrentDirectory( _path );
                    
                    
                    
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
                    
                    System.IO.Directory.SetLastAccessTime( _path, _lastAccessTime );
                    
                    
                    
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
                    
                    System.IO.Directory.SetLastAccessTimeUtc( _path, _lastAccessTimeUtc );
                    
                    
                    
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
                    
                    System.IO.Directory.SetLastWriteTime( _path, _lastWriteTime );
                    
                    
                    
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
                    
                    System.IO.Directory.SetLastWriteTimeUtc( _path, _lastWriteTimeUtc );
                    
                    
                    
                    return 0;
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
                    
                        System.Security.AccessControl.DirectorySecurity gen_ret = System.IO.Directory.GetAccessControl( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Security.AccessControl.AccessControlSections>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Security.AccessControl.AccessControlSections _includeSections;translator.Get(L, 2, out _includeSections);
                    
                        System.Security.AccessControl.DirectorySecurity gen_ret = System.IO.Directory.GetAccessControl( _path, _includeSections );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Directory.GetAccessControl!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
