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
using DG.Tweening;

namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class DGTweeningTweenerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DG.Tweening.Tweener);
			Utils.BeginObjectRegister(type, L, translator, 0, 26, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeStartValue", _m_ChangeStartValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeEndValue", _m_ChangeEndValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeValues", _m_ChangeValues);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Pause", _m_Pause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Play", _m_Play);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAutoKill", _m_SetAutoKill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetId", _m_SetId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTarget", _m_SetTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLoops", _m_SetLoops);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEase", _m_SetEase);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRecyclable", _m_SetRecyclable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUpdate", _m_SetUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnStart", _m_OnStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPlay", _m_OnPlay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPause", _m_OnPause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnRewind", _m_OnRewind);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnUpdate", _m_OnUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnStepComplete", _m_OnStepComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnComplete", _m_OnComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnKill", _m_OnKill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnWaypointChange", _m_OnWaypointChange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAs", _m_SetAs);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "From", _m_From);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDelay", _m_SetDelay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRelative", _m_SetRelative);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpeedBased", _m_SetSpeedBased);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "DG.Tweening.Tweener does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeStartValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    object _newStartValue = translator.GetObject(L, 2, typeof(object));
                    float _newDuration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.ChangeStartValue( _newStartValue, _newDuration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _newStartValue = translator.GetObject(L, 2, typeof(object));
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.ChangeStartValue( _newStartValue );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.ChangeStartValue!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeEndValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    object _newEndValue = translator.GetObject(L, 2, typeof(object));
                    bool _snapStartValue = LuaAPI.lua_toboolean(L, 3);
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.ChangeEndValue( _newEndValue, _snapStartValue );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    object _newEndValue = translator.GetObject(L, 2, typeof(object));
                    float _newDuration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapStartValue = LuaAPI.lua_toboolean(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.ChangeEndValue( _newEndValue, _newDuration, _snapStartValue );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    object _newEndValue = translator.GetObject(L, 2, typeof(object));
                    float _newDuration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.ChangeEndValue( _newEndValue, _newDuration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _newEndValue = translator.GetObject(L, 2, typeof(object));
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.ChangeEndValue( _newEndValue );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.ChangeEndValue!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeValues(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<object>(L, 2)&& translator.Assignable<object>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    object _newStartValue = translator.GetObject(L, 2, typeof(object));
                    object _newEndValue = translator.GetObject(L, 3, typeof(object));
                    float _newDuration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.ChangeValues( _newStartValue, _newEndValue, _newDuration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    object _newStartValue = translator.GetObject(L, 2, typeof(object));
                    object _newEndValue = translator.GetObject(L, 3, typeof(object));
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.ChangeValues( _newStartValue, _newEndValue );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.ChangeValues!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.Pause(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Play(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.Play(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAutoKill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetAutoKill(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _autoKillOnCompletion = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetAutoKill( _autoKillOnCompletion );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.SetAutoKill!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _intId = LuaAPI.xlua_tointeger(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetId( _intId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _objectId = translator.GetObject(L, 2, typeof(object));
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetId( _objectId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _stringId = LuaAPI.lua_tostring(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetId( _stringId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.SetId!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _target = translator.GetObject(L, 2, typeof(object));
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetTarget( _target );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLoops(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _loops = LuaAPI.xlua_tointeger(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetLoops( _loops );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<DG.Tweening.LoopType>(L, 3)) 
                {
                    int _loops = LuaAPI.xlua_tointeger(L, 2);
                    DG.Tweening.LoopType _loopType;translator.Get(L, 3, out _loopType);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetLoops( _loops, _loopType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.SetLoops!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEase(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<DG.Tweening.Ease>(L, 2)) 
                {
                    DG.Tweening.Ease _ease;translator.Get(L, 2, out _ease);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetEase( _ease );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.AnimationCurve>(L, 2)) 
                {
                    UnityEngine.AnimationCurve _animCurve = (UnityEngine.AnimationCurve)translator.GetObject(L, 2, typeof(UnityEngine.AnimationCurve));
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetEase( _animCurve );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<DG.Tweening.EaseFunction>(L, 2)) 
                {
                    DG.Tweening.EaseFunction _customEase = translator.GetDelegate<DG.Tweening.EaseFunction>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetEase( _customEase );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<DG.Tweening.Ease>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    DG.Tweening.Ease _ease;translator.Get(L, 2, out _ease);
                    float _overshoot = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetEase( _ease, _overshoot );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Ease>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Ease _ease;translator.Get(L, 2, out _ease);
                    float _amplitude = (float)LuaAPI.lua_tonumber(L, 3);
                    float _period = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetEase( _ease, _amplitude, _period );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.SetEase!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRecyclable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetRecyclable(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _recyclable = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetRecyclable( _recyclable );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.SetRecyclable!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _isIndependentUpdate = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetUpdate( _isIndependentUpdate );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<DG.Tweening.UpdateType>(L, 2)) 
                {
                    DG.Tweening.UpdateType _updateType;translator.Get(L, 2, out _updateType);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetUpdate( _updateType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<DG.Tweening.UpdateType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    DG.Tweening.UpdateType _updateType;translator.Get(L, 2, out _updateType);
                    bool _isIndependentUpdate = LuaAPI.lua_toboolean(L, 3);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetUpdate( _updateType, _isIndependentUpdate );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.SetUpdate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback _action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.OnStart( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPlay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback _action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.OnPlay( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback _action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.OnPause( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnRewind(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback _action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.OnRewind( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback _action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.OnUpdate( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnStepComplete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback _action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.OnStepComplete( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnComplete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback _action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.OnComplete( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnKill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback _action = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.OnKill( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnWaypointChange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    DG.Tweening.TweenCallback<int> _action = translator.GetDelegate<DG.Tweening.TweenCallback<int>>(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.OnWaypointChange( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAs(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<DG.Tweening.Tween>(L, 2)) 
                {
                    DG.Tweening.Tween _asTween = (DG.Tweening.Tween)translator.GetObject(L, 2, typeof(DG.Tweening.Tween));
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetAs( _asTween );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<DG.Tweening.TweenParams>(L, 2)) 
                {
                    DG.Tweening.TweenParams _tweenParams = (DG.Tweening.TweenParams)translator.GetObject(L, 2, typeof(DG.Tweening.TweenParams));
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetAs( _tweenParams );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.SetAs!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_From(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.From(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _isRelative = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.From( _isRelative );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.From!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDelay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _delay = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetDelay( _delay );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRelative(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetRelative(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _isRelative = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetRelative( _isRelative );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.SetRelative!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpeedBased(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tweener gen_to_be_invoked = (DG.Tweening.Tweener)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetSpeedBased(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _isSpeedBased = LuaAPI.lua_toboolean(L, 2);
                    
                        DG.Tweening.Tween gen_ret = gen_to_be_invoked.SetSpeedBased( _isSpeedBased );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tweener.SetSpeedBased!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
