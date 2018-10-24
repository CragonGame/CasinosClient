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
    public class UnityEngineTexture3DWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Texture3D);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 2, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPixels", _m_GetPixels);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPixels32", _m_GetPixels32);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPixels", _m_SetPixels);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPixels32", _m_SetPixels32);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Apply", _m_Apply);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "depth", _g_get_depth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "format", _g_get_format);
            
			
			
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
				if(LuaAPI.lua_gettop(L) == 6 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && translator.Assignable<UnityEngine.Experimental.Rendering.GraphicsFormat>(L, 5) && translator.Assignable<UnityEngine.Experimental.Rendering.TextureCreationFlags>(L, 6))
				{
					int _width = LuaAPI.xlua_tointeger(L, 2);
					int _height = LuaAPI.xlua_tointeger(L, 3);
					int _depth = LuaAPI.xlua_tointeger(L, 4);
					UnityEngine.Experimental.Rendering.GraphicsFormat _format;translator.Get(L, 5, out _format);
					UnityEngine.Experimental.Rendering.TextureCreationFlags _flags;translator.Get(L, 6, out _flags);
					
					UnityEngine.Texture3D gen_ret = new UnityEngine.Texture3D(_width, _height, _depth, _format, _flags);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 6 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && translator.Assignable<UnityEngine.TextureFormat>(L, 5) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6))
				{
					int _width = LuaAPI.xlua_tointeger(L, 2);
					int _height = LuaAPI.xlua_tointeger(L, 3);
					int _depth = LuaAPI.xlua_tointeger(L, 4);
					UnityEngine.TextureFormat _textureFormat;translator.Get(L, 5, out _textureFormat);
					bool _mipChain = LuaAPI.lua_toboolean(L, 6);
					
					UnityEngine.Texture3D gen_ret = new UnityEngine.Texture3D(_width, _height, _depth, _textureFormat, _mipChain);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Texture3D constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPixels(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Texture3D gen_to_be_invoked = (UnityEngine.Texture3D)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        UnityEngine.Color[] gen_ret = gen_to_be_invoked.GetPixels(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _miplevel = LuaAPI.xlua_tointeger(L, 2);
                    
                        UnityEngine.Color[] gen_ret = gen_to_be_invoked.GetPixels( _miplevel );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Texture3D.GetPixels!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPixels32(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Texture3D gen_to_be_invoked = (UnityEngine.Texture3D)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        UnityEngine.Color32[] gen_ret = gen_to_be_invoked.GetPixels32(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _miplevel = LuaAPI.xlua_tointeger(L, 2);
                    
                        UnityEngine.Color32[] gen_ret = gen_to_be_invoked.GetPixels32( _miplevel );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Texture3D.GetPixels32!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPixels(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Texture3D gen_to_be_invoked = (UnityEngine.Texture3D)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Color[]>(L, 2)) 
                {
                    UnityEngine.Color[] _colors = (UnityEngine.Color[])translator.GetObject(L, 2, typeof(UnityEngine.Color[]));
                    
                    gen_to_be_invoked.SetPixels( _colors );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Color[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Color[] _colors = (UnityEngine.Color[])translator.GetObject(L, 2, typeof(UnityEngine.Color[]));
                    int _miplevel = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetPixels( _colors, _miplevel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Texture3D.SetPixels!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPixels32(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Texture3D gen_to_be_invoked = (UnityEngine.Texture3D)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Color32[]>(L, 2)) 
                {
                    UnityEngine.Color32[] _colors = (UnityEngine.Color32[])translator.GetObject(L, 2, typeof(UnityEngine.Color32[]));
                    
                    gen_to_be_invoked.SetPixels32( _colors );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Color32[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Color32[] _colors = (UnityEngine.Color32[])translator.GetObject(L, 2, typeof(UnityEngine.Color32[]));
                    int _miplevel = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetPixels32( _colors, _miplevel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Texture3D.SetPixels32!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Apply(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Texture3D gen_to_be_invoked = (UnityEngine.Texture3D)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Apply(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _updateMipmaps = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Apply( _updateMipmaps );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    bool _updateMipmaps = LuaAPI.lua_toboolean(L, 2);
                    bool _makeNoLongerReadable = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.Apply( _updateMipmaps, _makeNoLongerReadable );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Texture3D.Apply!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_depth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Texture3D gen_to_be_invoked = (UnityEngine.Texture3D)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.depth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_format(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Texture3D gen_to_be_invoked = (UnityEngine.Texture3D)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.format);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
