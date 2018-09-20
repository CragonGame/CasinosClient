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
    public class CasinosUiDoTweenHelperWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.UiDoTweenHelper);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 12, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenMove", _m_TweenMove_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenMoveX", _m_TweenMoveX_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenMoveY", _m_TweenMoveY_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenScale", _m_TweenScale_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenScaleX", _m_TweenScaleX_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenScaleY", _m_TweenScaleY_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenResize", _m_TweenResize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenFade", _m_TweenFade_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenRotate", _m_TweenRotate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenRotateX", _m_TweenRotateX_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweenRotateY", _m_TweenRotateY_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Casinos.UiDoTweenHelper does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenMove_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    UnityEngine.Vector2 _startValue;translator.Get(L, 2, out _startValue);
                    UnityEngine.Vector2 _endValue;translator.Get(L, 3, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    bool _is_snapping = LuaAPI.lua_toboolean(L, 5);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenMove( _target, _startValue, _endValue, _duration, _is_snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenMoveX_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    bool _is_snapping = LuaAPI.lua_toboolean(L, 5);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenMoveX( _target, _startValue, _endValue, _duration, _is_snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenMoveY_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    bool _is_snapping = LuaAPI.lua_toboolean(L, 5);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenMoveY( _target, _startValue, _endValue, _duration, _is_snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenScale_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    UnityEngine.Vector2 _startValue;translator.Get(L, 2, out _startValue);
                    UnityEngine.Vector2 _endValue;translator.Get(L, 3, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenScale( _target, _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenScaleX_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenScaleX( _target, _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenScaleY_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenScaleY( _target, _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenResize_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    UnityEngine.Vector2 _startValue;translator.Get(L, 2, out _startValue);
                    UnityEngine.Vector2 _endValue;translator.Get(L, 3, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    bool _is_snapping = LuaAPI.lua_toboolean(L, 5);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenResize( _target, _startValue, _endValue, _duration, _is_snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenFade_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenFade( _target, _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenRotate_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenRotate( _target, _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenRotateX_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenRotateX( _target, _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweenRotateY_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _target = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = Casinos.UiDoTweenHelper.TweenRotateY( _target, _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
