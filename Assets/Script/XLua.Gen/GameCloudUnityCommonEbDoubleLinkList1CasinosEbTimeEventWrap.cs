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
    public class GameCloudUnityCommonEbDoubleLinkList_1_CasinosEbTimeEvent_Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>);
			Utils.BeginObjectRegister(type, L, translator, 0, 16, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Destroy", _m_Destroy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddNode", _m_AddNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddTailNode", _m_AddTailNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReplaceNode", _m_ReplaceNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsLast", _m_IsLast);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Empty", _m_Empty);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveList", _m_MoveList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddList", _m_AddList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddTailList", _m_AddTailList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FirstNode", _m_FirstNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LastNode", _m_LastNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Head", _m_Head);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "NextNode", _m_NextNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PrevNode", _m_PrevNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNodeCount", _m_GetNodeCount);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "mpHead", _g_get_mpHead);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "mpHead", _s_set_mpHead);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "DelNode", _m_DelNode_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_ret = new GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Destroy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Destroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> _pnew = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
                    
                    gen_to_be_invoked.AddNode( _pnew );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddTailNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> _pnew = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
                    
                    gen_to_be_invoked.AddTailNode( _pnew );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DelNode_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> _entry = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 1, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
                    
                    GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>.DelNode( _entry );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReplaceNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> _old = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
                    GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> _pnew = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 3, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
                    
                    gen_to_be_invoked.ReplaceNode( _old, _pnew );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsLast(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> _list = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
                    
                        bool gen_ret = gen_to_be_invoked.IsLast( _list );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Empty(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.Empty(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> _pnew_list = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>));
                    
                    gen_to_be_invoked.MoveList( _pnew_list );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> _plist = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>));
                    
                    gen_to_be_invoked.AddList( _plist );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddTailList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> _plist = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>));
                    
                    gen_to_be_invoked.AddTailList( _plist );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FirstNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_ret = gen_to_be_invoked.FirstNode(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LastNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_ret = gen_to_be_invoked.LastNode(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Head(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_ret = gen_to_be_invoked.Head(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NextNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> _pnode = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
                    
                        GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_ret = gen_to_be_invoked.NextNode( _pnode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PrevNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> _pnode = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
                    
                        GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent> gen_ret = gen_to_be_invoked.PrevNode( _pnode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNodeCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        uint gen_ret = gen_to_be_invoked.GetNodeCount(  );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mpHead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mpHead);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mpHead(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent> gen_to_be_invoked = (GameCloud.Unity.Common.EbDoubleLinkList<Casinos.EbTimeEvent>)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mpHead = (GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>)translator.GetObject(L, 2, typeof(GameCloud.Unity.Common.EbDoubleLinkNode<Casinos.EbTimeEvent>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
