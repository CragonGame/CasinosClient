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
    public class FairyGUIEventDispatcherWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.EventDispatcher);
			Utils.BeginObjectRegister(type, L, translator, 0, 8, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddEventListener", _m_AddEventListener);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveEventListener", _m_RemoveEventListener);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveEventListeners", _m_RemoveEventListeners);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "hasEventListeners", _m_hasEventListeners);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "isDispatching", _m_isDispatching);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DispatchEvent", _m_DispatchEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BubbleEvent", _m_BubbleEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BroadcastEvent", _m_BroadcastEvent);
			
			
			
			
			
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
					
					FairyGUI.EventDispatcher gen_ret = new FairyGUI.EventDispatcher();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.EventDispatcher constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEventListener(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.EventDispatcher gen_to_be_invoked = (FairyGUI.EventDispatcher)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<FairyGUI.EventCallback1>(L, 3)) 
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    FairyGUI.EventCallback1 _callback = translator.GetDelegate<FairyGUI.EventCallback1>(L, 3);
                    
                    gen_to_be_invoked.AddEventListener( _strType, _callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<FairyGUI.EventCallback0>(L, 3)) 
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    FairyGUI.EventCallback0 _callback = translator.GetDelegate<FairyGUI.EventCallback0>(L, 3);
                    
                    gen_to_be_invoked.AddEventListener( _strType, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.EventDispatcher.AddEventListener!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveEventListener(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.EventDispatcher gen_to_be_invoked = (FairyGUI.EventDispatcher)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<FairyGUI.EventCallback1>(L, 3)) 
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    FairyGUI.EventCallback1 _callback = translator.GetDelegate<FairyGUI.EventCallback1>(L, 3);
                    
                    gen_to_be_invoked.RemoveEventListener( _strType, _callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<FairyGUI.EventCallback0>(L, 3)) 
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    FairyGUI.EventCallback0 _callback = translator.GetDelegate<FairyGUI.EventCallback0>(L, 3);
                    
                    gen_to_be_invoked.RemoveEventListener( _strType, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.EventDispatcher.RemoveEventListener!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveEventListeners(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.EventDispatcher gen_to_be_invoked = (FairyGUI.EventDispatcher)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.RemoveEventListeners(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RemoveEventListeners( _strType );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.EventDispatcher.RemoveEventListeners!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_hasEventListeners(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.EventDispatcher gen_to_be_invoked = (FairyGUI.EventDispatcher)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.hasEventListeners( _strType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_isDispatching(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.EventDispatcher gen_to_be_invoked = (FairyGUI.EventDispatcher)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.isDispatching( _strType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.EventDispatcher gen_to_be_invoked = (FairyGUI.EventDispatcher)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.DispatchEvent( _strType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.EventContext>(L, 2)) 
                {
                    FairyGUI.EventContext _context = (FairyGUI.EventContext)translator.GetObject(L, 2, typeof(FairyGUI.EventContext));
                    
                        bool gen_ret = gen_to_be_invoked.DispatchEvent( _context );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    object _data = translator.GetObject(L, 3, typeof(object));
                    
                        bool gen_ret = gen_to_be_invoked.DispatchEvent( _strType, _data );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    object _data = translator.GetObject(L, 3, typeof(object));
                    object _initiator = translator.GetObject(L, 4, typeof(object));
                    
                        bool gen_ret = gen_to_be_invoked.DispatchEvent( _strType, _data, _initiator );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.EventDispatcher.DispatchEvent!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BubbleEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.EventDispatcher gen_to_be_invoked = (FairyGUI.EventDispatcher)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    object _data = translator.GetObject(L, 3, typeof(object));
                    
                        bool gen_ret = gen_to_be_invoked.BubbleEvent( _strType, _data );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BroadcastEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.EventDispatcher gen_to_be_invoked = (FairyGUI.EventDispatcher)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _strType = LuaAPI.lua_tostring(L, 2);
                    object _data = translator.GetObject(L, 3, typeof(object));
                    
                        bool gen_ret = gen_to_be_invoked.BroadcastEvent( _strType, _data );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
