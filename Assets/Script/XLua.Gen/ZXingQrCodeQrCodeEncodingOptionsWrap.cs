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
    public class ZXingQrCodeQrCodeEncodingOptionsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(ZXing.QrCode.QrCodeEncodingOptions);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 4, 4);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ErrorCorrection", _g_get_ErrorCorrection);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CharacterSet", _g_get_CharacterSet);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DisableECI", _g_get_DisableECI);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "QrVersion", _g_get_QrVersion);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "ErrorCorrection", _s_set_ErrorCorrection);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CharacterSet", _s_set_CharacterSet);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DisableECI", _s_set_DisableECI);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "QrVersion", _s_set_QrVersion);
            
			
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
					
					ZXing.QrCode.QrCodeEncodingOptions gen_ret = new ZXing.QrCode.QrCodeEncodingOptions();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to ZXing.QrCode.QrCodeEncodingOptions constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ErrorCorrection(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ZXing.QrCode.QrCodeEncodingOptions gen_to_be_invoked = (ZXing.QrCode.QrCodeEncodingOptions)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ErrorCorrection);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CharacterSet(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ZXing.QrCode.QrCodeEncodingOptions gen_to_be_invoked = (ZXing.QrCode.QrCodeEncodingOptions)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.CharacterSet);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DisableECI(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ZXing.QrCode.QrCodeEncodingOptions gen_to_be_invoked = (ZXing.QrCode.QrCodeEncodingOptions)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.DisableECI);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_QrVersion(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ZXing.QrCode.QrCodeEncodingOptions gen_to_be_invoked = (ZXing.QrCode.QrCodeEncodingOptions)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.QrVersion);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ErrorCorrection(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ZXing.QrCode.QrCodeEncodingOptions gen_to_be_invoked = (ZXing.QrCode.QrCodeEncodingOptions)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ErrorCorrection = (ZXing.QrCode.Internal.ErrorCorrectionLevel)translator.GetObject(L, 2, typeof(ZXing.QrCode.Internal.ErrorCorrectionLevel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CharacterSet(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ZXing.QrCode.QrCodeEncodingOptions gen_to_be_invoked = (ZXing.QrCode.QrCodeEncodingOptions)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CharacterSet = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DisableECI(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ZXing.QrCode.QrCodeEncodingOptions gen_to_be_invoked = (ZXing.QrCode.QrCodeEncodingOptions)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DisableECI = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_QrVersion(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ZXing.QrCode.QrCodeEncodingOptions gen_to_be_invoked = (ZXing.QrCode.QrCodeEncodingOptions)translator.FastGetCSObj(L, 1);
                System.Nullable<int> gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.QrVersion = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
