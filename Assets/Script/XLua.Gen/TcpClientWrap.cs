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
    public class TcpClientWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TcpClient);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 5, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Handle", _m_Handle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Connect", _m_Connect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Close", _m_Close);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Send", _m_Send);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsConnected", _g_get_IsConnected);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnSocketReceive", _g_get_OnSocketReceive);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnSocketConnected", _g_get_OnSocketConnected);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnSocketClosed", _g_get_OnSocketClosed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnSocketError", _g_get_OnSocketError);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnSocketReceive", _s_set_OnSocketReceive);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnSocketConnected", _s_set_OnSocketConnected);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnSocketClosed", _s_set_OnSocketClosed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnSocketError", _s_set_OnSocketError);
            
			
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
					
					TcpClient gen_ret = new TcpClient();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TcpClient constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Handle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    SuperSocket.ProtoBase.BufferedPackageInfo<ushort> _package = (SuperSocket.ProtoBase.BufferedPackageInfo<ushort>)translator.GetObject(L, 2, typeof(SuperSocket.ProtoBase.BufferedPackageInfo<ushort>));
                    
                    gen_to_be_invoked.Handle( _package );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Connect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _ip_or_host = LuaAPI.lua_tostring(L, 2);
                    int _port = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Connect( _ip_or_host, _port );
                    
                    
                    
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
            
            
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_Close(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Close(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Send(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    byte[] _buf = LuaAPI.lua_tobytes(L, 2);
                    
                    gen_to_be_invoked.Send( _buf );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    ushort _method_id = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    byte[] _data = LuaAPI.lua_tobytes(L, 3);
                    
                    gen_to_be_invoked.Send( _method_id, _data );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    byte[] _buf = LuaAPI.lua_tobytes(L, 2);
                    ushort _length = (ushort)LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Send( _buf, _length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TcpClient.Send!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsConnected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsConnected);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnSocketReceive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnSocketReceive);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnSocketConnected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnSocketConnected);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnSocketClosed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnSocketClosed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnSocketError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnSocketError);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnSocketReceive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnSocketReceive = translator.GetDelegate<GameCloud.Unity.Common.OnSocketReceive>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnSocketConnected(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnSocketConnected = translator.GetDelegate<GameCloud.Unity.Common.OnSocketConnected>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnSocketClosed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnSocketClosed = translator.GetDelegate<GameCloud.Unity.Common.OnSocketClosed>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnSocketError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TcpClient gen_to_be_invoked = (TcpClient)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnSocketError = translator.GetDelegate<GameCloud.Unity.Common.OnSocketError>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
