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
    public class OpenInstallReceiverWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(OpenInstallReceiver);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 2, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenInstallWakeUpResult", _m_OpenInstallWakeUpResult);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenInstallInstallResult", _m_OpenInstallInstallResult);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "OpenInstallReceiverListener", _g_get_OpenInstallReceiverListener);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OpenInstallResultCallBack", _g_get_OpenInstallResultCallBack);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "OpenInstallReceiverListener", _s_set_OpenInstallReceiverListener);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OpenInstallResultCallBack", _s_set_OpenInstallResultCallBack);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "instance", _m_instance_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					OpenInstallReceiver gen_ret = new OpenInstallReceiver();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to OpenInstallReceiver constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_instance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        OpenInstallReceiver gen_ret = OpenInstallReceiver.instance(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenInstallWakeUpResult(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                OpenInstallReceiver gen_to_be_invoked = (OpenInstallReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _result = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.OpenInstallWakeUpResult( _result );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenInstallInstallResult(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                OpenInstallReceiver gen_to_be_invoked = (OpenInstallReceiver)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _result = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.OpenInstallInstallResult( _result );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OpenInstallReceiverListener(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                OpenInstallReceiver gen_to_be_invoked = (OpenInstallReceiver)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.OpenInstallReceiverListener);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OpenInstallResultCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                OpenInstallReceiver gen_to_be_invoked = (OpenInstallReceiver)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OpenInstallResultCallBack);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OpenInstallReceiverListener(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                OpenInstallReceiver gen_to_be_invoked = (OpenInstallReceiver)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OpenInstallReceiverListener = (IOpenInstallReceiverListener)translator.GetObject(L, 2, typeof(IOpenInstallReceiverListener));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OpenInstallResultCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                OpenInstallReceiver gen_to_be_invoked = (OpenInstallReceiver)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OpenInstallResultCallBack = translator.GetDelegate<System.Action<string, bool>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
