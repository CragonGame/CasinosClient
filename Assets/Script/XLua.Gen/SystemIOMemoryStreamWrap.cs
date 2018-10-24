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
    public class SystemIOMemoryStreamWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.IO.MemoryStream);
			Utils.BeginObjectRegister(type, L, translator, 0, 15, 6, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Flush", _m_Flush);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FlushAsync", _m_FlushAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBuffer", _m_GetBuffer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TryGetBuffer", _m_TryGetBuffer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Read", _m_Read);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadAsync", _m_ReadAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadByte", _m_ReadByte);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CopyToAsync", _m_CopyToAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Seek", _m_Seek);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLength", _m_SetLength);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToArray", _m_ToArray);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Write", _m_Write);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteAsync", _m_WriteAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteByte", _m_WriteByte);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteTo", _m_WriteTo);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanRead", _g_get_CanRead);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanSeek", _g_get_CanSeek);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanWrite", _g_get_CanWrite);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Capacity", _g_get_Capacity);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Length", _g_get_Length);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Position", _g_get_Position);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Capacity", _s_set_Capacity);
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
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					System.IO.MemoryStream gen_ret = new System.IO.MemoryStream();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					int _capacity = LuaAPI.xlua_tointeger(L, 2);
					
					System.IO.MemoryStream gen_ret = new System.IO.MemoryStream(_capacity);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))
				{
					byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
					
					System.IO.MemoryStream gen_ret = new System.IO.MemoryStream(_buffer);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3))
				{
					byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
					bool _writable = LuaAPI.lua_toboolean(L, 3);
					
					System.IO.MemoryStream gen_ret = new System.IO.MemoryStream(_buffer, _writable);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4))
				{
					byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
					int _index = LuaAPI.xlua_tointeger(L, 3);
					int _count = LuaAPI.xlua_tointeger(L, 4);
					
					System.IO.MemoryStream gen_ret = new System.IO.MemoryStream(_buffer, _index, _count);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5))
				{
					byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
					int _index = LuaAPI.xlua_tointeger(L, 3);
					int _count = LuaAPI.xlua_tointeger(L, 4);
					bool _writable = LuaAPI.lua_toboolean(L, 5);
					
					System.IO.MemoryStream gen_ret = new System.IO.MemoryStream(_buffer, _index, _count, _writable);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 6 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6))
				{
					byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
					int _index = LuaAPI.xlua_tointeger(L, 3);
					int _count = LuaAPI.xlua_tointeger(L, 4);
					bool _writable = LuaAPI.lua_toboolean(L, 5);
					bool _publiclyVisible = LuaAPI.lua_toboolean(L, 6);
					
					System.IO.MemoryStream gen_ret = new System.IO.MemoryStream(_buffer, _index, _count, _writable, _publiclyVisible);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.MemoryStream constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Flush(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Flush(  );
                    
                    
                    
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
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_GetBuffer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        byte[] gen_ret = gen_to_be_invoked.GetBuffer(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TryGetBuffer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.ArraySegment<byte> _buffer;
                    
                        bool gen_ret = gen_to_be_invoked.TryGetBuffer( out _buffer );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _buffer);
                        
                    
                    
                    
                    return 2;
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
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.Read( _buffer, _offset, _count );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 2;
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
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_ReadByte(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_CopyToAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.IO.Stream _destination = (System.IO.Stream)translator.GetObject(L, 2, typeof(System.IO.Stream));
                    int _bufferSize = LuaAPI.xlua_tointeger(L, 3);
                    System.Threading.CancellationToken _cancellationToken;translator.Get(L, 4, out _cancellationToken);
                    
                        System.Threading.Tasks.Task gen_ret = gen_to_be_invoked.CopyToAsync( _destination, _bufferSize, _cancellationToken );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
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
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    long _offset = LuaAPI.lua_toint64(L, 2);
                    System.IO.SeekOrigin _loc;translator.Get(L, 3, out _loc);
                    
                        long gen_ret = gen_to_be_invoked.Seek( _offset, _loc );
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
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_ToArray(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        byte[] gen_ret = gen_to_be_invoked.ToArray(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
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
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.Write( _buffer, _offset, _count );
                    
                    
                    
                    return 0;
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
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_WriteByte(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_WriteTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.IO.Stream _stream = (System.IO.Stream)translator.GetObject(L, 2, typeof(System.IO.Stream));
                    
                    gen_to_be_invoked.WriteTo( _stream );
                    
                    
                    
                    return 0;
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
			
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanRead);
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
			
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanSeek);
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
			
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanWrite);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Capacity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Capacity);
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
			
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
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
			
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushint64(L, gen_to_be_invoked.Position);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Capacity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Capacity = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Position(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.IO.MemoryStream gen_to_be_invoked = (System.IO.MemoryStream)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Position = LuaAPI.lua_toint64(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
