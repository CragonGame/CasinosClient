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
    public class FairyGUIGComponentWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.GComponent);
			Utils.BeginObjectRegister(type, L, translator, 0, 32, 17, 10);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InvalidateBatchingState", _m_InvalidateBatchingState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddChild", _m_AddChild);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddChildAt", _m_AddChildAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveChild", _m_RemoveChild);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveChildAt", _m_RemoveChildAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveChildren", _m_RemoveChildren);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetChildAt", _m_GetChildAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetChild", _m_GetChild);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetVisibleChild", _m_GetVisibleChild);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetChildInGroup", _m_GetChildInGroup);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetChildren", _m_GetChildren);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetChildIndex", _m_GetChildIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetChildIndex", _m_SetChildIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetChildIndexBefore", _m_SetChildIndexBefore);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SwapChildren", _m_SwapChildren);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SwapChildrenAt", _m_SwapChildrenAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsAncestorOf", _m_IsAncestorOf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddController", _m_AddController);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetControllerAt", _m_GetControllerAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetController", _m_GetController);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveController", _m_RemoveController);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTransitionAt", _m_GetTransitionAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTransition", _m_GetTransition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsChildInView", _m_IsChildInView);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetFirstChildInView", _m_GetFirstChildInView);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HandleControllerChanged", _m_HandleControllerChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetBoundsChangedFlag", _m_SetBoundsChangedFlag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EnsureBoundsCorrect", _m_EnsureBoundsCorrect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ConstructFromResource", _m_ConstructFromResource);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ConstructFromXML", _m_ConstructFromXML);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Setup_AfterAdd", _m_Setup_AfterAdd);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "rootContainer", _g_get_rootContainer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "container", _g_get_container);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "scrollPane", _g_get_scrollPane);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onDrop", _g_get_onDrop);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fairyBatching", _g_get_fairyBatching);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "opaque", _g_get_opaque);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "margin", _g_get_margin);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "childrenRenderOrder", _g_get_childrenRenderOrder);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "apexIndex", _g_get_apexIndex);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "numChildren", _g_get_numChildren);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Controllers", _g_get_Controllers);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "clipSoftness", _g_get_clipSoftness);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mask", _g_get_mask);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reversedMask", _g_get_reversedMask);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "baseUserData", _g_get_baseUserData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "viewWidth", _g_get_viewWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "viewHeight", _g_get_viewHeight);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "fairyBatching", _s_set_fairyBatching);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "opaque", _s_set_opaque);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "margin", _s_set_margin);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "childrenRenderOrder", _s_set_childrenRenderOrder);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "apexIndex", _s_set_apexIndex);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "clipSoftness", _s_set_clipSoftness);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mask", _s_set_mask);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reversedMask", _s_set_reversedMask);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "viewWidth", _s_set_viewWidth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "viewHeight", _s_set_viewHeight);
            
			
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
					
					FairyGUI.GComponent gen_ret = new FairyGUI.GComponent();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GComponent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InvalidateBatchingState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _childChanged = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.InvalidateBatchingState( _childChanged );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddChild(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _child = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.AddChild( _child );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddChildAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _child = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.AddChildAt( _child, _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveChild(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<FairyGUI.GObject>(L, 2)) 
                {
                    FairyGUI.GObject _child = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.RemoveChild( _child );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<FairyGUI.GObject>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    FairyGUI.GObject _child = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    bool _dispose = LuaAPI.lua_toboolean(L, 3);
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.RemoveChild( _child, _dispose );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GComponent.RemoveChild!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveChildAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.RemoveChildAt( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    bool _dispose = LuaAPI.lua_toboolean(L, 3);
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.RemoveChildAt( _index, _dispose );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GComponent.RemoveChildAt!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveChildren(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.RemoveChildren(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    int _beginIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _endIndex = LuaAPI.xlua_tointeger(L, 3);
                    bool _dispose = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.RemoveChildren( _beginIndex, _endIndex, _dispose );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.GComponent.RemoveChildren!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetChildAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.GetChildAt( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetChild(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.GetChild( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetVisibleChild(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.GetVisibleChild( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetChildInGroup(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GGroup _group = (FairyGUI.GGroup)translator.GetObject(L, 2, typeof(FairyGUI.GGroup));
                    string _name = LuaAPI.lua_tostring(L, 3);
                    
                        FairyGUI.GObject gen_ret = gen_to_be_invoked.GetChildInGroup( _group, _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetChildren(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        FairyGUI.GObject[] gen_ret = gen_to_be_invoked.GetChildren(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetChildIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _child = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                        int gen_ret = gen_to_be_invoked.GetChildIndex( _child );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetChildIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _child = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetChildIndex( _child, _index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetChildIndexBefore(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _child = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.SetChildIndexBefore( _child, _index );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SwapChildren(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _child1 = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    FairyGUI.GObject _child2 = (FairyGUI.GObject)translator.GetObject(L, 3, typeof(FairyGUI.GObject));
                    
                    gen_to_be_invoked.SwapChildren( _child1, _child2 );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SwapChildrenAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index1 = LuaAPI.xlua_tointeger(L, 2);
                    int _index2 = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SwapChildrenAt( _index1, _index2 );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsAncestorOf(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _obj = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                        bool gen_ret = gen_to_be_invoked.IsAncestorOf( _obj );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddController(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Controller _controller = (FairyGUI.Controller)translator.GetObject(L, 2, typeof(FairyGUI.Controller));
                    
                    gen_to_be_invoked.AddController( _controller );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetControllerAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        FairyGUI.Controller gen_ret = gen_to_be_invoked.GetControllerAt( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetController(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.Controller gen_ret = gen_to_be_invoked.GetController( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveController(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Controller _c = (FairyGUI.Controller)translator.GetObject(L, 2, typeof(FairyGUI.Controller));
                    
                    gen_to_be_invoked.RemoveController( _c );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTransitionAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        FairyGUI.Transition gen_ret = gen_to_be_invoked.GetTransitionAt( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTransition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.Transition gen_ret = gen_to_be_invoked.GetTransition( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsChildInView(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.GObject _child = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                        bool gen_ret = gen_to_be_invoked.IsChildInView( _child );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFirstChildInView(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.GetFirstChildInView(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HandleControllerChanged(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Controller _c = (FairyGUI.Controller)translator.GetObject(L, 2, typeof(FairyGUI.Controller));
                    
                    gen_to_be_invoked.HandleControllerChanged( _c );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetBoundsChangedFlag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetBoundsChangedFlag(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnsureBoundsCorrect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.EnsureBoundsCorrect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConstructFromResource(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ConstructFromResource(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConstructFromXML(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Utils.XML _xml = (FairyGUI.Utils.XML)translator.GetObject(L, 2, typeof(FairyGUI.Utils.XML));
                    
                    gen_to_be_invoked.ConstructFromXML( _xml );
                    
                    
                    
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
            
            
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _g_get_rootContainer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rootContainer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_container(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.container);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_scrollPane(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.scrollPane);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDrop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onDrop);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fairyBatching(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.fairyBatching);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_opaque(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.opaque);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_margin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.margin);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_childrenRenderOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.childrenRenderOrder);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_apexIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.apexIndex);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_numChildren(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.numChildren);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Controllers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Controllers);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_clipSoftness(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.clipSoftness);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mask);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reversedMask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.reversedMask);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_baseUserData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.baseUserData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_viewWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.viewWidth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_viewHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.viewHeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fairyBatching(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fairyBatching = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_opaque(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.opaque = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_margin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                FairyGUI.Margin gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.margin = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_childrenRenderOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                FairyGUI.ChildrenRenderOrder gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.childrenRenderOrder = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_apexIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.apexIndex = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_clipSoftness(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.clipSoftness = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mask = (FairyGUI.DisplayObject)translator.GetObject(L, 2, typeof(FairyGUI.DisplayObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reversedMask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reversedMask = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_viewWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.viewWidth = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_viewHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.GComponent gen_to_be_invoked = (FairyGUI.GComponent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.viewHeight = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
