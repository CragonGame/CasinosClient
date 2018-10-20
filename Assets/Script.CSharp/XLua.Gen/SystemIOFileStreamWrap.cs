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
    public class SystemIOFileStreamWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.IO.FileStream);
			Utils.BeginObjectRegister(type, L, translator, 0, 18, 8, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadByte", _m_ReadByte);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteByte", _m_WriteByte);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Read", _m_Read);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeginRead", _m_BeginRead);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EndRead", _m_EndRead);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Write", _m_Write);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeginWrite", _m_BeginWrite);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EndWrite", _m_EndWrite);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Seek", _m_Seek);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLength", _m_SetLength);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Flush", _m_Flush);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Lock", _m_Lock);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Unlock", _m_Unlock);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAccessControl", _m_GetAccessControl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAccessControl", _m_SetAccessControl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FlushAsync", _m_FlushAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadAsync", _m_ReadAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteAsync", _m_WriteAsync);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanRead", _g_get_CanRead);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanWrite", _g_get_CanWrite);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanSeek", _g_get_CanSeek);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsAsync", _g_get_IsAsync);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Name", _g_get_Name);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Length", _g_get_Length);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Position", _g_get_Position);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SafeFileHandle", _g_get_SafeFileHandle);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Position", _s_set_Position);
            
			
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
				if(LuaAPI.lua_gettop(L) == 3 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.IO.FileMode>(L, 3))
				{
					string _path = LuaAPI.lua_tostring(L, 2);
					System.IO.FileMode _mode;translator.Get(L, 3, out _mode);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_path, _mode);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.IO.FileMode>(L, 3) && translator.Assignable<System.IO.FileAccess>(L, 4))
				{
					string _path = LuaAPI.lua_tostring(L, 2);
					System.IO.FileMode _mode;translator.Get(L, 3, out _mode);
					System.IO.FileAccess _access;translator.Get(L, 4, out _access);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_path, _mode, _access);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.IO.FileMode>(L, 3) && translator.Assignable<System.IO.FileAccess>(L, 4) && translator.Assignable<System.IO.FileShare>(L, 5))
				{
					string _path = LuaAPI.lua_tostring(L, 2);
					System.IO.FileMode _mode;translator.Get(L, 3, out _mode);
					System.IO.FileAccess _access;translator.Get(L, 4, out _access);
					System.IO.FileShare _share;translator.Get(L, 5, out _share);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_path, _mode, _access, _share);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 6 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.IO.FileMode>(L, 3) && translator.Assignable<System.IO.FileAccess>(L, 4) && translator.Assignable<System.IO.FileShare>(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6))
				{
					string _path = LuaAPI.lua_tostring(L, 2);
					System.IO.FileMode _mode;translator.Get(L, 3, out _mode);
					System.IO.FileAccess _access;translator.Get(L, 4, out _access);
					System.IO.FileShare _share;translator.Get(L, 5, out _share);
					int _bufferSize = LuaAPI.xlua_tointeger(L, 6);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_path, _mode, _access, _share, _bufferSize);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 7 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.IO.FileMode>(L, 3) && translator.Assignable<System.IO.FileAccess>(L, 4) && translator.Assignable<System.IO.FileShare>(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7))
				{
					string _path = LuaAPI.lua_tostring(L, 2);
					System.IO.FileMode _mode;translator.Get(L, 3, out _mode);
					System.IO.FileAccess _access;translator.Get(L, 4, out _access);
					System.IO.FileShare _share;translator.Get(L, 5, out _share);
					int _bufferSize = LuaAPI.xlua_tointeger(L, 6);
					bool _useAsync = LuaAPI.lua_toboolean(L, 7);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_path, _mode, _access, _share, _bufferSize, _useAsync);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 7 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.IO.FileMode>(L, 3) && translator.Assignable<System.IO.FileAccess>(L, 4) && translator.Assignable<System.IO.FileShare>(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && translator.Assignable<System.IO.FileOptions>(L, 7))
				{
					string _path = LuaAPI.lua_tostring(L, 2);
					System.IO.FileMode _mode;translator.Get(L, 3, out _mode);
					System.IO.FileAccess _access;translator.Get(L, 4, out _access);
					System.IO.FileShare _share;translator.Get(L, 5, out _share);
					int _bufferSize = LuaAPI.xlua_tointeger(L, 6);
					System.IO.FileOptions _options;translator.Get(L, 7, out _options);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_path, _mode, _access, _share, _bufferSize, _options);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<Microsoft.Win32.SafeHandles.SafeFileHandle>(L, 2) && translator.Assignable<System.IO.FileAccess>(L, 3))
				{
					Microsoft.Win32.SafeHandles.SafeFileHandle _handle = (Microsoft.Win32.SafeHandles.SafeFileHandle)translator.GetObject(L, 2, typeof(Microsoft.Win32.SafeHandles.SafeFileHandle));
					System.IO.FileAccess _access;translator.Get(L, 3, out _access);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_handle, _access);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<Microsoft.Win32.SafeHandles.SafeFileHandle>(L, 2) && translator.Assignable<System.IO.FileAccess>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4))
				{
					Microsoft.Win32.SafeHandles.SafeFileHandle _handle = (Microsoft.Win32.SafeHandles.SafeFileHandle)translator.GetObject(L, 2, typeof(Microsoft.Win32.SafeHandles.SafeFileHandle));
					System.IO.FileAccess _access;translator.Get(L, 3, out _access);
					int _bufferSize = LuaAPI.xlua_tointeger(L, 4);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_handle, _access, _bufferSize);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<Microsoft.Win32.SafeHandles.SafeFileHandle>(L, 2) && translator.Assignable<System.IO.FileAccess>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5))
				{
					Microsoft.Win32.SafeHandles.SafeFileHandle _handle = (Microsoft.Win32.SafeHandles.SafeFileHandle)translator.GetObject(L, 2, typeof(Microsoft.Win32.SafeHandles.SafeFileHandle));
					System.IO.FileAccess _access;translator.Get(L, 3, out _access);
					int _bufferSize = LuaAPI.xlua_tointeger(L, 4);
					bool _isAsync = LuaAPI.lua_toboolean(L, 5);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_handle, _access, _bufferSize, _isAsync);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 7 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.IO.FileMode>(L, 3) && translator.Assignable<System.Security.AccessControl.FileSystemRights>(L, 4) && translator.Assignable<System.IO.FileShare>(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && translator.Assignable<System.IO.FileOptions>(L, 7))
				{
					string _path = LuaAPI.lua_tostring(L, 2);
					System.IO.FileMode _mode;translator.Get(L, 3, out _mode);
					System.Security.AccessControl.FileSystemRights _rights;translator.Get(L, 4, out _rights);
					System.IO.FileShare _share;translator.Get(L, 5, out _share);
					int _bufferSize = LuaAPI.xlua_tointeger(L, 6);
					System.IO.FileOptions _options;translator.Get(L, 7, out _options);
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_path, _mode, _rights, _share, _bufferSize, _options);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 8 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.IO.FileMode>(L, 3) && translator.Assignable<System.Security.AccessControl.FileSystemRights>(L, 4) && translator.Assignable<System.IO.FileShare>(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && translator.Assignable<System.IO.FileOptions>(L, 7) && translator.Assignable<System.Security.AccessControl.FileSecurity>(L, 8))
				{
					string _path = LuaAPI.lua_tostring(L, 2);
					System.IO.FileMode _mode;translator.Get(L, 3, out _mode);
					System.Security.AccessControl.FileSystemRights _rights;translator.Get(L, 4, out _rights);
					System.IO.FileShare _share;translator.Get(L, 5, out _share);
					int _bufferSize = LuaAPI.xlua_tointeger(L, 6);
					System.IO.FileOptions _options;translator.Get(L, 7, out _options);
					System.Security.AccessControl.FileSecurity _fileSecurity = (System.Security.AccessControl.FileSecurity)translator.GetObject(L, 8, typeof(System.Security.AccessControl.FileSecurity));
					
					System.IO.FileStream gen_ret = new System.IO.FileStream(_path, _mode, _rights, _share, _bufferSize, _options, _fileSecurity);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.FileStream constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadByte(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.ReadByte(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteByte(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte _value = (byte)LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.WriteByte( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Read(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _array = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.Read( _array, _offset, _count );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeginRead(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _array = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _numBytes = LuaAPI.xlua_tointeger(L, 4);
                    System.AsyncCallback _userCallback = translator.GetDelegate<System.AsyncCallback>(L, 5);
                    object _stateObject = translator.GetObject(L, 6, typeof(object));
                    
                        System.IAsyncResult gen_ret = gen_to_be_invoked.BeginRead( _array, _offset, _numBytes, _userCallback, _stateObject );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EndRead(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.IAsyncResult _asyncResult = (System.IAsyncResult)translator.GetObject(L, 2, typeof(System.IAsyncResult));
                    
                        int gen_ret = gen_to_be_invoked.EndRead( _asyncResult );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Write(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _array = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.Write( _array, _offset, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeginWrite(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _array = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _numBytes = LuaAPI.xlua_tointeger(L, 4);
                    System.AsyncCallback _userCallback = translator.GetDelegate<System.AsyncCallback>(L, 5);
                    object _stateObject = translator.GetObject(L, 6, typeof(object));
                    
                        System.IAsyncResult gen_ret = gen_to_be_invoked.BeginWrite( _array, _offset, _numBytes, _userCallback, _stateObject );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EndWrite(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.IAsyncResult _asyncResult = (System.IAsyncResult)translator.GetObject(L, 2, typeof(System.IAsyncResult));
                    
                    gen_to_be_invoked.EndWrite( _asyncResult );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Seek(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    long _offset = LuaAPI.lua_toint64(L, 2);
                    System.IO.SeekOrigin _origin;translator.Get(L, 3, out _origin);
                    
                        long gen_ret = gen_to_be_invoked.Seek( _offset, _origin );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLength(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    long _value = LuaAPI.lua_toint64(L, 2);
                    
                    gen_to_be_invoked.SetLength( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Flush(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Flush(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _flushToDisk = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Flush( _flushToDisk );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.FileStream.Flush!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Lock(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    long _position = LuaAPI.lua_toint64(L, 2);
                    long _length = LuaAPI.lua_toint64(L, 3);
                    
                    gen_to_be_invoked.Lock( _position, _length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Unlock(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    long _position = LuaAPI.lua_toint64(L, 2);
                    long _length = LuaAPI.lua_toint64(L, 3);
                    
                    gen_to_be_invoked.Unlock( _position, _length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAccessControl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Security.AccessControl.FileSecurity gen_ret = gen_to_be_invoked.GetAccessControl(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAccessControl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Security.AccessControl.FileSecurity _fileSecurity = (System.Security.AccessControl.FileSecurity)translator.GetObject(L, 2, typeof(System.Security.AccessControl.FileSecurity));
                    
                    gen_to_be_invoked.SetAccessControl( _fileSecurity );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FlushAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Threading.CancellationToken _cancellationToken;translator.Get(L, 2, out _cancellationToken);
                    
                        System.Threading.Tasks.Task gen_ret = gen_to_be_invoked.FlushAsync( _cancellationToken );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    System.Threading.CancellationToken _cancellationToken;translator.Get(L, 5, out _cancellationToken);
                    
                        System.Threading.Tasks.Task<int> gen_ret = gen_to_be_invoked.ReadAsync( _buffer, _offset, _count, _cancellationToken );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    System.Threading.CancellationToken _cancellationToken;translator.Get(L, 5, out _cancellationToken);
                    
                        System.Threading.Tasks.Task gen_ret = gen_to_be_invoked.WriteAsync( _buffer, _offset, _count, _cancellationToken );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanRead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanRead);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanWrite(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanWrite);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanSeek(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanSeek);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsAsync(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsAsync);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.Name);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Length(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushint64(L, gen_to_be_invoked.Length);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Position(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushint64(L, gen_to_be_invoked.Position);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SafeFileHandle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.SafeFileHandle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Position(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.FileStream gen_to_be_invoked = (System.IO.FileStream)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Position = LuaAPI.lua_toint64(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
