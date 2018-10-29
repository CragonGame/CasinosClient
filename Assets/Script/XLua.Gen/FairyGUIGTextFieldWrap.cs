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
    public class FairyGUIGTextFieldWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.GTextField);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 14, 12);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetVar", _m_SetVar);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FlushVars", _m_FlushVars);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasCharacter", _m_HasCharacter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Setup_BeforeAdd", _m_Setup_BeforeAdd);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Setup_AfterAdd", _m_Setup_AfterAdd);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "text", _g_get_text);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "templateVars", _g_get_templateVars);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "textFormat", _g_get_textFormat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "color", _g_get_color);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "align", _g_get_align);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "verticalAlign", _g_get_verticalAlign);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "singleLine", _g_get_singleLine);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "stroke", _g_get_stroke);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "strokeColor", _g_get_strokeColor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "shadowOffset", _g_get_shadowOffset);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UBBEnabled", _g_get_UBBEnabled);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "autoSize", _g_get_autoSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "textWidth", _g_get_textWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "textHeight", _g_get_textHeight);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "text", _s_set_text);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "templateVars", _s_set_templateVars);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "textFormat", _s_set_textFormat);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "color", _s_set_color);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "align", _s_set_align);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "verticalAlign", _s_set_verticalAlign);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "singleLine", _s_set_singleLine);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "stroke", _s_set_stroke);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "strokeColor", _s_set_strokeColor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "shadowOffset", _s_set_shadowOffset);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UBBEnabled", _s_set_UBBEnabled);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "autoSize", _s_set_autoSize);
            
			
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
					
					FairyGUI.GTextField gen_ret = new FairyGUI.GTextField();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GTextField constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetVar(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    string _value = LuaAPI.lua_tostring(L, 3);
                    
                        FairyGUI.GTextField gen_ret = gen_to_be_invoked.SetVar( _name, _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FlushVars(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.FlushVars(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasCharacter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    char _ch = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.HasCharacter( _ch );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
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
            
            
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_Setup_AfterAdd(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Utils.ByteBuffer _buffer = (FairyGUI.Utils.ByteBuffer)translator.GetObject(L, 2, typeof(FairyGUI.Utils.ByteBuffer));
                    int _beginPos = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Setup_AfterAdd( _buffer, _beginPos );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_text(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.text);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_templateVars(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.templateVars);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_textFormat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.textFormat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_color(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, gen_to_be_invoked.color);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_align(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.align);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_verticalAlign(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.verticalAlign);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_singleLine(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.singleLine);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_stroke(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.stroke);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_strokeColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineColor(L, gen_to_be_invoked.strokeColor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_shadowOffset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.shadowOffset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UBBEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UBBEnabled);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_autoSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.autoSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_textWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.textWidth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_textHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.textHeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_text(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.text = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_templateVars(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.templateVars = (System.Collections.Generic.Dictionary<string, string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, string>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_textFormat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.textFormat = (FairyGUI.TextFormat)translator.GetObject(L, 2, typeof(FairyGUI.TextFormat));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_color(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                UnityEngine.Color gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.color = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_align(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                FairyGUI.AlignType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.align = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_verticalAlign(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                FairyGUI.VertAlignType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.verticalAlign = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_singleLine(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.singleLine = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_stroke(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.stroke = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_strokeColor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                UnityEngine.Color gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.strokeColor = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_shadowOffset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.shadowOffset = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UBBEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UBBEnabled = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_autoSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GTextField gen_to_be_invoked = (FairyGUI.GTextField)translator.FastGetCSObj(L, 1);
                FairyGUI.AutoSizeType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.autoSize = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
