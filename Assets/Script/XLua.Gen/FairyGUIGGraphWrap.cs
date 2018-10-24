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
    public class FairyGUIGGraphWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.GGraph);
			Utils.BeginObjectRegister(type, L, translator, 0, 9, 2, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReplaceMe", _m_ReplaceMe);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddBeforeMe", _m_AddBeforeMe);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddAfterMe", _m_AddAfterMe);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetNativeObject", _m_SetNativeObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DrawRect", _m_DrawRect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DrawRoundRect", _m_DrawRoundRect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DrawEllipse", _m_DrawEllipse);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DrawPolygon", _m_DrawPolygon);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Setup_BeforeAdd", _m_Setup_BeforeAdd);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "color", _g_get_color);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "shape", _g_get_shape);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "color", _s_set_color);
            
			
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
					
					FairyGUI.GGraph gen_ret = new FairyGUI.GGraph();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GGraph constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReplaceMe(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.ReplaceMe( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddBeforeMe(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.AddBeforeMe( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddAfterMe(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.AddAfterMe( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetNativeObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.DisplayObject _obj = (FairyGUI.DisplayObject)translator.GetObject(L, 2, typeof(FairyGUI.DisplayObject));
                    
                    gen_to_be_invoked.SetNativeObject( _obj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DrawRect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _aWidth = (float)LuaAPI.lua_tonumber(L, 2);
                    float _aHeight = (float)LuaAPI.lua_tonumber(L, 3);
                    int _lineSize = LuaAPI.xlua_tointeger(L, 4);
                    UnityEngine.Color _lineColor;translator.Get(L, 5, out _lineColor);
                    UnityEngine.Color _fillColor;translator.Get(L, 6, out _fillColor);
                    
                    gen_to_be_invoked.DrawRect( _aWidth, _aHeight, _lineSize, _lineColor, _fillColor );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DrawRoundRect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _aWidth = (float)LuaAPI.lua_tonumber(L, 2);
                    float _aHeight = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Color _fillColor;translator.Get(L, 4, out _fillColor);
                    float[] _corner = (float[])translator.GetObject(L, 5, typeof(float[]));
                    
                    gen_to_be_invoked.DrawRoundRect( _aWidth, _aHeight, _fillColor, _corner );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DrawEllipse(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _aWidth = (float)LuaAPI.lua_tonumber(L, 2);
                    float _aHeight = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Color _fillColor;translator.Get(L, 4, out _fillColor);
                    
                    gen_to_be_invoked.DrawEllipse( _aWidth, _aHeight, _fillColor );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DrawPolygon(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _aWidth = (float)LuaAPI.lua_tonumber(L, 2);
                    float _aHeight = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Vector2[] _points = (UnityEngine.Vector2[])translator.GetObject(L, 4, typeof(UnityEngine.Vector2[]));
                    UnityEngine.Color _fillColor;translator.Get(L, 5, out _fillColor);
                    
                    gen_to_be_invoked.DrawPolygon( _aWidth, _aHeight, _points, _fillColor );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Setup_BeforeAdd(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Utils.ByteBuffer _buffer = (FairyGUI.Utils.ByteBuffer)translator.GetObject(L, 2, typeof(FairyGUI.Utils.ByteBuffer));
                    int _beginPos = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Setup_BeforeAdd( _buffer, _beginPos );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_color(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, gen_to_be_invoked.color);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_shape(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.shape);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_color(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GGraph gen_to_be_invoked = (FairyGUI.GGraph)translator.FastGetCSObj(L, 1);
                UnityEngine.Color gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.color = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
