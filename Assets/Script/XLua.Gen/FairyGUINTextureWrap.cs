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
    public class FairyGUINTextureWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.NTexture);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 13, 5);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetDrawRect", _m_GetDrawRect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUV", _m_GetUV);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMaterialManager", _m_GetMaterialManager);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyMaterialManager", _m_DestroyMaterialManager);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Unload", _m_Unload);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Reload", _m_Reload);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "width", _g_get_width);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "height", _g_get_height);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "offset", _g_get_offset);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "originalSize", _g_get_originalSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "root", _g_get_root);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "disposed", _g_get_disposed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "nativeTexture", _g_get_nativeTexture);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "alphaTexture", _g_get_alphaTexture);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uvRect", _g_get_uvRect);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rotated", _g_get_rotated);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "refCount", _g_get_refCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "lastActive", _g_get_lastActive);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "destroyMethod", _g_get_destroyMethod);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "uvRect", _s_set_uvRect);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rotated", _s_set_rotated);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "refCount", _s_set_refCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "lastActive", _s_set_lastActive);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "destroyMethod", _s_set_destroyMethod);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 1, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "DisposeEmpty", _m_DisposeEmpty_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Empty", _g_get_Empty);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<UnityEngine.Texture>(L, 2))
				{
					UnityEngine.Texture _texture = (UnityEngine.Texture)translator.GetObject(L, 2, typeof(UnityEngine.Texture));
					
					FairyGUI.NTexture gen_ret = new FairyGUI.NTexture(_texture);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<UnityEngine.Texture>(L, 2) && translator.Assignable<UnityEngine.Texture>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5))
				{
					UnityEngine.Texture _texture = (UnityEngine.Texture)translator.GetObject(L, 2, typeof(UnityEngine.Texture));
					UnityEngine.Texture _alphaTexture = (UnityEngine.Texture)translator.GetObject(L, 3, typeof(UnityEngine.Texture));
					float _xScale = (float)LuaAPI.lua_tonumber(L, 4);
					float _yScale = (float)LuaAPI.lua_tonumber(L, 5);
					
					FairyGUI.NTexture gen_ret = new FairyGUI.NTexture(_texture, _alphaTexture, _xScale, _yScale);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<UnityEngine.Texture>(L, 2) && translator.Assignable<UnityEngine.Rect>(L, 3))
				{
					UnityEngine.Texture _texture = (UnityEngine.Texture)translator.GetObject(L, 2, typeof(UnityEngine.Texture));
					UnityEngine.Rect _region;translator.Get(L, 3, out _region);
					
					FairyGUI.NTexture gen_ret = new FairyGUI.NTexture(_texture, _region);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<FairyGUI.NTexture>(L, 2) && translator.Assignable<UnityEngine.Rect>(L, 3) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4))
				{
					FairyGUI.NTexture _root = (FairyGUI.NTexture)translator.GetObject(L, 2, typeof(FairyGUI.NTexture));
					UnityEngine.Rect _region;translator.Get(L, 3, out _region);
					bool _rotated = LuaAPI.lua_toboolean(L, 4);
					
					FairyGUI.NTexture gen_ret = new FairyGUI.NTexture(_root, _region, _rotated);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 6 && translator.Assignable<FairyGUI.NTexture>(L, 2) && translator.Assignable<UnityEngine.Rect>(L, 3) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4) && translator.Assignable<UnityEngine.Vector2>(L, 5) && translator.Assignable<UnityEngine.Vector2>(L, 6))
				{
					FairyGUI.NTexture _root = (FairyGUI.NTexture)translator.GetObject(L, 2, typeof(FairyGUI.NTexture));
					UnityEngine.Rect _region;translator.Get(L, 3, out _region);
					bool _rotated = LuaAPI.lua_toboolean(L, 4);
					UnityEngine.Vector2 _originalSize;translator.Get(L, 5, out _originalSize);
					UnityEngine.Vector2 _offset;translator.Get(L, 6, out _offset);
					
					FairyGUI.NTexture gen_ret = new FairyGUI.NTexture(_root, _region, _rotated, _originalSize, _offset);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.NTexture constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisposeEmpty_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    FairyGUI.NTexture.DisposeEmpty(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDrawRect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Rect _drawRect;translator.Get(L, 2, out _drawRect);
                    
                        UnityEngine.Rect gen_ret = gen_to_be_invoked.GetDrawRect( _drawRect );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUV(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector2[] _uv = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
                    
                    gen_to_be_invoked.GetUV( _uv );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMaterialManager(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _shaderName = LuaAPI.lua_tostring(L, 2);
                    string[] _keywords = (string[])translator.GetObject(L, 3, typeof(string[]));
                    
                        FairyGUI.MaterialManager gen_ret = gen_to_be_invoked.GetMaterialManager( _shaderName, _keywords );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyMaterialManager(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.MaterialManager _manager = (FairyGUI.MaterialManager)translator.GetObject(L, 2, typeof(FairyGUI.MaterialManager));
                    
                    gen_to_be_invoked.DestroyMaterialManager( _manager );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Unload(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Unload(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _destroyMaterials = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Unload( _destroyMaterials );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.NTexture.Unload!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Reload(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Texture _nativeTexture = (UnityEngine.Texture)translator.GetObject(L, 2, typeof(UnityEngine.Texture));
                    UnityEngine.Texture _alphaTexture = (UnityEngine.Texture)translator.GetObject(L, 3, typeof(UnityEngine.Texture));
                    
                    gen_to_be_invoked.Reload( _nativeTexture, _alphaTexture );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Empty(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, FairyGUI.NTexture.Empty);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_width(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.width);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_height(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.height);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_offset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.offset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_originalSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.originalSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_root(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.root);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_disposed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.disposed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_nativeTexture(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.nativeTexture);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_alphaTexture(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.alphaTexture);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uvRect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uvRect);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rotated(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.rotated);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_refCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.refCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_lastActive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.lastActive);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_destroyMethod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.destroyMethod);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uvRect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                UnityEngine.Rect gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.uvRect = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rotated(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rotated = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_refCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.refCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_lastActive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.lastActive = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_destroyMethod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.NTexture gen_to_be_invoked = (FairyGUI.NTexture)translator.FastGetCSObj(L, 1);
                FairyGUI.DestroyMethod gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.destroyMethod = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
