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
    public class DGTweeningDOTweenWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DG.Tweening.DOTween);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 45, 16, 16);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Init", _m_Init_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetTweensCapacity", _m_SetTweensCapacity_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Clear", _m_Clear_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearCachedTweens", _m_ClearCachedTweens_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Validate", _m_Validate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ManualUpdate", _m_ManualUpdate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "To", _m_To_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ToAxis", _m_ToAxis_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ToAlpha", _m_ToAlpha_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Punch", _m_Punch_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Shake", _m_Shake_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ToArray", _m_ToArray_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Sequence", _m_Sequence_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CompleteAll", _m_CompleteAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Complete", _m_Complete_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FlipAll", _m_FlipAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Flip", _m_Flip_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GotoAll", _m_GotoAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Goto", _m_Goto_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "KillAll", _m_KillAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Kill", _m_Kill_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PauseAll", _m_PauseAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Pause", _m_Pause_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PlayAll", _m_PlayAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Play", _m_Play_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PlayBackwardsAll", _m_PlayBackwardsAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PlayBackwards", _m_PlayBackwards_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PlayForwardAll", _m_PlayForwardAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PlayForward", _m_PlayForward_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RestartAll", _m_RestartAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Restart", _m_Restart_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RewindAll", _m_RewindAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Rewind", _m_Rewind_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SmoothRewindAll", _m_SmoothRewindAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SmoothRewind", _m_SmoothRewind_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TogglePauseAll", _m_TogglePauseAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TogglePause", _m_TogglePause_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsTweening", _m_IsTweening_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TotalPlayingTweens", _m_TotalPlayingTweens_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PlayingTweens", _m_PlayingTweens_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PausedTweens", _m_PausedTweens_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweensById", _m_TweensById_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TweensByTarget", _m_TweensByTarget_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Version", DG.Tweening.DOTween.Version);
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "logBehaviour", _g_get_logBehaviour);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "useSafeMode", _g_get_useSafeMode);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "showUnityEditorReport", _g_get_showUnityEditorReport);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "timeScale", _g_get_timeScale);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "useSmoothDeltaTime", _g_get_useSmoothDeltaTime);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "maxSmoothUnscaledTime", _g_get_maxSmoothUnscaledTime);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "drawGizmos", _g_get_drawGizmos);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultUpdateType", _g_get_defaultUpdateType);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultTimeScaleIndependent", _g_get_defaultTimeScaleIndependent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultAutoPlay", _g_get_defaultAutoPlay);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultAutoKill", _g_get_defaultAutoKill);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultLoopType", _g_get_defaultLoopType);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultRecyclable", _g_get_defaultRecyclable);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultEaseType", _g_get_defaultEaseType);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultEaseOvershootOrAmplitude", _g_get_defaultEaseOvershootOrAmplitude);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultEasePeriod", _g_get_defaultEasePeriod);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "logBehaviour", _s_set_logBehaviour);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "useSafeMode", _s_set_useSafeMode);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "showUnityEditorReport", _s_set_showUnityEditorReport);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "timeScale", _s_set_timeScale);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "useSmoothDeltaTime", _s_set_useSmoothDeltaTime);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "maxSmoothUnscaledTime", _s_set_maxSmoothUnscaledTime);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "drawGizmos", _s_set_drawGizmos);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "defaultUpdateType", _s_set_defaultUpdateType);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "defaultTimeScaleIndependent", _s_set_defaultTimeScaleIndependent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "defaultAutoPlay", _s_set_defaultAutoPlay);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "defaultAutoKill", _s_set_defaultAutoKill);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "defaultLoopType", _s_set_defaultLoopType);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "defaultRecyclable", _s_set_defaultRecyclable);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "defaultEaseType", _s_set_defaultEaseType);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "defaultEaseOvershootOrAmplitude", _s_set_defaultEaseOvershootOrAmplitude);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "defaultEasePeriod", _s_set_defaultEasePeriod);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					DG.Tweening.DOTween gen_ret = new DG.Tweening.DOTween();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Nullable<bool>>(L, 1)&& translator.Assignable<System.Nullable<bool>>(L, 2)&& translator.Assignable<System.Nullable<DG.Tweening.LogBehaviour>>(L, 3)) 
                {
                    System.Nullable<bool> _recycleAllByDefault;translator.Get(L, 1, out _recycleAllByDefault);
                    System.Nullable<bool> _useSafeMode;translator.Get(L, 2, out _useSafeMode);
                    System.Nullable<DG.Tweening.LogBehaviour> _logBehaviour;translator.Get(L, 3, out _logBehaviour);
                    
                        DG.Tweening.IDOTweenInit gen_ret = DG.Tweening.DOTween.Init( _recycleAllByDefault, _useSafeMode, _logBehaviour );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Nullable<bool>>(L, 1)&& translator.Assignable<System.Nullable<bool>>(L, 2)) 
                {
                    System.Nullable<bool> _recycleAllByDefault;translator.Get(L, 1, out _recycleAllByDefault);
                    System.Nullable<bool> _useSafeMode;translator.Get(L, 2, out _useSafeMode);
                    
                        DG.Tweening.IDOTweenInit gen_ret = DG.Tweening.DOTween.Init( _recycleAllByDefault, _useSafeMode );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Nullable<bool>>(L, 1)) 
                {
                    System.Nullable<bool> _recycleAllByDefault;translator.Get(L, 1, out _recycleAllByDefault);
                    
                        DG.Tweening.IDOTweenInit gen_ret = DG.Tweening.DOTween.Init( _recycleAllByDefault );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 0) 
                {
                    
                        DG.Tweening.IDOTweenInit gen_ret = DG.Tweening.DOTween.Init(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Init!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTweensCapacity_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _tweenersCapacity = LuaAPI.xlua_tointeger(L, 1);
                    int _sequencesCapacity = LuaAPI.xlua_tointeger(L, 2);
                    
                    DG.Tweening.DOTween.SetTweensCapacity( _tweenersCapacity, _sequencesCapacity );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)) 
                {
                    bool _destroy = LuaAPI.lua_toboolean(L, 1);
                    
                    DG.Tweening.DOTween.Clear( _destroy );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 0) 
                {
                    
                    DG.Tweening.DOTween.Clear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Clear!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearCachedTweens_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    DG.Tweening.DOTween.ClearCachedTweens(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Validate_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = DG.Tweening.DOTween.Validate(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ManualUpdate_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _deltaTime = (float)LuaAPI.lua_tonumber(L, 1);
                    float _unscaledDeltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    DG.Tweening.DOTween.ManualUpdate( _deltaTime, _unscaledDeltaTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_To_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOSetter<float>>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOSetter<float> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<float>>(L, 1);
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = DG.Tweening.DOTween.To( _setter, _startValue, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<float>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<float>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<float> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<float>>(L, 1);
                    DG.Tweening.Core.DOSetter<float> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<float>>(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<double>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<double>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<double> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<double>>(L, 1);
                    DG.Tweening.Core.DOSetter<double> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<double>>(L, 2);
                    double _endValue = LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<double, double, DG.Tweening.Plugins.Options.NoOptions> gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<int>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<int>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<int> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<int>>(L, 1);
                    DG.Tweening.Core.DOSetter<int> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<int>>(L, 2);
                    int _endValue = LuaAPI.xlua_tointeger(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<uint>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<uint>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<uint> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<uint>>(L, 1);
                    DG.Tweening.Core.DOSetter<uint> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<uint>>(L, 2);
                    uint _endValue = LuaAPI.xlua_touint(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<long>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<long>>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<long> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<long>>(L, 1);
                    DG.Tweening.Core.DOSetter<long> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<long>>(L, 2);
                    long _endValue = LuaAPI.lua_toint64(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<ulong>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<ulong>>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isuint64(L, 3))&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<ulong> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<ulong>>(L, 1);
                    DG.Tweening.Core.DOSetter<ulong> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<ulong>>(L, 2);
                    ulong _endValue = LuaAPI.lua_touint64(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<string>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<string>>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<string> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<string>>(L, 1);
                    DG.Tweening.Core.DOSetter<string> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<string>>(L, 2);
                    string _endValue = LuaAPI.lua_tostring(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions> gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector2>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector2>>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector2> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector2>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector2> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector2>>(L, 2);
                    UnityEngine.Vector2 _endValue;translator.Get(L, 3, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    UnityEngine.Vector3 _endValue;translator.Get(L, 3, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector4>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector4>>(L, 2)&& translator.Assignable<UnityEngine.Vector4>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector4> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector4>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector4> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector4>>(L, 2);
                    UnityEngine.Vector4 _endValue;translator.Get(L, 3, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Quaternion>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Quaternion>>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Quaternion> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Quaternion>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Quaternion> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Quaternion>>(L, 2);
                    UnityEngine.Vector3 _endValue;translator.Get(L, 3, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Color>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Color>>(L, 2)&& translator.Assignable<UnityEngine.Color>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Color> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Color>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Color> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Color>>(L, 2);
                    UnityEngine.Color _endValue;translator.Get(L, 3, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Rect>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Rect>>(L, 2)&& translator.Assignable<UnityEngine.Rect>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Rect> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Rect>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Rect> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Rect>>(L, 2);
                    UnityEngine.Rect _endValue;translator.Get(L, 3, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions> gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.RectOffset>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.RectOffset>>(L, 2)&& translator.Assignable<UnityEngine.RectOffset>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.RectOffset> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.RectOffset>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.RectOffset> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.RectOffset>>(L, 2);
                    UnityEngine.RectOffset _endValue = (UnityEngine.RectOffset)translator.GetObject(L, 3, typeof(UnityEngine.RectOffset));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = DG.Tweening.DOTween.To( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.To!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToAxis_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<DG.Tweening.AxisConstraint>(L, 5)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    DG.Tweening.AxisConstraint _axisConstraint;translator.Get(L, 5, out _axisConstraint);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> gen_ret = DG.Tweening.DOTween.ToAxis( _getter, _setter, _endValue, _duration, _axisConstraint );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> gen_ret = DG.Tweening.DOTween.ToAxis( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.ToAxis!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToAlpha_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Color> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Color>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Color> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Color>>(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Tweener gen_ret = DG.Tweening.DOTween.ToAlpha( _getter, _setter, _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Punch_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    UnityEngine.Vector3 _direction;translator.Get(L, 3, out _direction);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 5);
                    float _elasticity = (float)LuaAPI.lua_tonumber(L, 6);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Punch( _getter, _setter, _direction, _duration, _vibrato, _elasticity );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    UnityEngine.Vector3 _direction;translator.Get(L, 3, out _direction);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 5);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Punch( _getter, _setter, _direction, _duration, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    UnityEngine.Vector3 _direction;translator.Get(L, 3, out _direction);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Punch( _getter, _setter, _direction, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Punch!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Shake_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 8&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 8)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 4);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 5);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 6);
                    bool _ignoreZAxis = LuaAPI.lua_toboolean(L, 7);
                    bool _fadeOut = LuaAPI.lua_toboolean(L, 8);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration, _strength, _vibrato, _randomness, _ignoreZAxis, _fadeOut );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 4);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 5);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 6);
                    bool _ignoreZAxis = LuaAPI.lua_toboolean(L, 7);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration, _strength, _vibrato, _randomness, _ignoreZAxis );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 4);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 5);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 6);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration, _strength, _vibrato, _randomness );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 4);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 5);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration, _strength, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration, _strength );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector3>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Vector3 _strength;translator.Get(L, 4, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 5);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 6);
                    bool _fadeOut = LuaAPI.lua_toboolean(L, 7);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration, _strength, _vibrato, _randomness, _fadeOut );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector3>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Vector3 _strength;translator.Get(L, 4, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 5);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 6);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration, _strength, _vibrato, _randomness );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector3>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Vector3 _strength;translator.Get(L, 4, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 5);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration, _strength, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1)&& translator.Assignable<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector3>(L, 4)) 
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Vector3 _strength;translator.Get(L, 4, out _strength);
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.Shake( _getter, _setter, _duration, _strength );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Shake!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToArray_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    DG.Tweening.Core.DOGetter<UnityEngine.Vector3> _getter = translator.GetDelegate<DG.Tweening.Core.DOGetter<UnityEngine.Vector3>>(L, 1);
                    DG.Tweening.Core.DOSetter<UnityEngine.Vector3> _setter = translator.GetDelegate<DG.Tweening.Core.DOSetter<UnityEngine.Vector3>>(L, 2);
                    UnityEngine.Vector3[] _endValues = (UnityEngine.Vector3[])translator.GetObject(L, 3, typeof(UnityEngine.Vector3[]));
                    float[] _durations = (float[])translator.GetObject(L, 4, typeof(float[]));
                    
                        DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> gen_ret = DG.Tweening.DOTween.ToArray( _getter, _setter, _endValues, _durations );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Sequence_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        DG.Tweening.Sequence gen_ret = DG.Tweening.DOTween.Sequence(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CompleteAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)) 
                {
                    bool _withCallbacks = LuaAPI.lua_toboolean(L, 1);
                    
                        int gen_ret = DG.Tweening.DOTween.CompleteAll( _withCallbacks );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 0) 
                {
                    
                        int gen_ret = DG.Tweening.DOTween.CompleteAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.CompleteAll!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Complete_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    bool _withCallbacks = LuaAPI.lua_toboolean(L, 2);
                    
                        int gen_ret = DG.Tweening.DOTween.Complete( _targetOrId, _withCallbacks );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.Complete( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Complete!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FlipAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = DG.Tweening.DOTween.FlipAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Flip_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.Flip( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GotoAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    float _to = (float)LuaAPI.lua_tonumber(L, 1);
                    bool _andPlay = LuaAPI.lua_toboolean(L, 2);
                    
                        int gen_ret = DG.Tweening.DOTween.GotoAll( _to, _andPlay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)) 
                {
                    float _to = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        int gen_ret = DG.Tweening.DOTween.GotoAll( _to );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.GotoAll!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Goto_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    float _to = (float)LuaAPI.lua_tonumber(L, 2);
                    bool _andPlay = LuaAPI.lua_toboolean(L, 3);
                    
                        int gen_ret = DG.Tweening.DOTween.Goto( _targetOrId, _to, _andPlay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    float _to = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        int gen_ret = DG.Tweening.DOTween.Goto( _targetOrId, _to );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Goto!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_KillAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)) 
                {
                    bool _complete = LuaAPI.lua_toboolean(L, 1);
                    
                        int gen_ret = DG.Tweening.DOTween.KillAll( _complete );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 0) 
                {
                    
                        int gen_ret = DG.Tweening.DOTween.KillAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2))) 
                {
                    bool _complete = LuaAPI.lua_toboolean(L, 1);
                    object[] _idsOrTargetsToExclude = translator.GetParams<object>(L, 2);
                    
                        int gen_ret = DG.Tweening.DOTween.KillAll( _complete, _idsOrTargetsToExclude );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.KillAll!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Kill_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    bool _complete = LuaAPI.lua_toboolean(L, 2);
                    
                        int gen_ret = DG.Tweening.DOTween.Kill( _targetOrId, _complete );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.Kill( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Kill!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PauseAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = DG.Tweening.DOTween.PauseAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pause_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.Pause( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = DG.Tweening.DOTween.PlayAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Play_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.Play( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    object _id = translator.GetObject(L, 2, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.Play( _target, _id );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Play!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayBackwardsAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = DG.Tweening.DOTween.PlayBackwardsAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayBackwards_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.PlayBackwards( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    object _id = translator.GetObject(L, 2, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.PlayBackwards( _target, _id );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.PlayBackwards!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayForwardAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = DG.Tweening.DOTween.PlayForwardAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayForward_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.PlayForward( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    object _id = translator.GetObject(L, 2, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.PlayForward( _target, _id );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.PlayForward!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RestartAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)) 
                {
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 1);
                    
                        int gen_ret = DG.Tweening.DOTween.RestartAll( _includeDelay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 0) 
                {
                    
                        int gen_ret = DG.Tweening.DOTween.RestartAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.RestartAll!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Restart_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 2);
                    float _changeDelayTo = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        int gen_ret = DG.Tweening.DOTween.Restart( _targetOrId, _includeDelay, _changeDelayTo );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 2);
                    
                        int gen_ret = DG.Tweening.DOTween.Restart( _targetOrId, _includeDelay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.Restart( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<object>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    object _id = translator.GetObject(L, 2, typeof(object));
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 3);
                    float _changeDelayTo = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        int gen_ret = DG.Tweening.DOTween.Restart( _target, _id, _includeDelay, _changeDelayTo );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    object _id = translator.GetObject(L, 2, typeof(object));
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 3);
                    
                        int gen_ret = DG.Tweening.DOTween.Restart( _target, _id, _includeDelay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    object _id = translator.GetObject(L, 2, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.Restart( _target, _id );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Restart!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RewindAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)) 
                {
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 1);
                    
                        int gen_ret = DG.Tweening.DOTween.RewindAll( _includeDelay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 0) 
                {
                    
                        int gen_ret = DG.Tweening.DOTween.RewindAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.RewindAll!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Rewind_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 2);
                    
                        int gen_ret = DG.Tweening.DOTween.Rewind( _targetOrId, _includeDelay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.Rewind( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.Rewind!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SmoothRewindAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = DG.Tweening.DOTween.SmoothRewindAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SmoothRewind_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.SmoothRewind( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TogglePauseAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = DG.Tweening.DOTween.TogglePauseAll(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TogglePause_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        int gen_ret = DG.Tweening.DOTween.TogglePause( _targetOrId );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsTweening_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    bool _alsoCheckIfIsPlaying = LuaAPI.lua_toboolean(L, 2);
                    
                        bool gen_ret = DG.Tweening.DOTween.IsTweening( _targetOrId, _alsoCheckIfIsPlaying );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _targetOrId = translator.GetObject(L, 1, typeof(object));
                    
                        bool gen_ret = DG.Tweening.DOTween.IsTweening( _targetOrId );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.IsTweening!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TotalPlayingTweens_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = DG.Tweening.DOTween.TotalPlayingTweens(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayingTweens_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.Collections.Generic.List<DG.Tweening.Tween>>(L, 1)) 
                {
                    System.Collections.Generic.List<DG.Tweening.Tween> _fillableList = (System.Collections.Generic.List<DG.Tweening.Tween>)translator.GetObject(L, 1, typeof(System.Collections.Generic.List<DG.Tweening.Tween>));
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.PlayingTweens( _fillableList );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 0) 
                {
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.PlayingTweens(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.PlayingTweens!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PausedTweens_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.Collections.Generic.List<DG.Tweening.Tween>>(L, 1)) 
                {
                    System.Collections.Generic.List<DG.Tweening.Tween> _fillableList = (System.Collections.Generic.List<DG.Tweening.Tween>)translator.GetObject(L, 1, typeof(System.Collections.Generic.List<DG.Tweening.Tween>));
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.PausedTweens( _fillableList );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 0) 
                {
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.PausedTweens(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.PausedTweens!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweensById_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.List<DG.Tweening.Tween>>(L, 3)) 
                {
                    object _id = translator.GetObject(L, 1, typeof(object));
                    bool _playingOnly = LuaAPI.lua_toboolean(L, 2);
                    System.Collections.Generic.List<DG.Tweening.Tween> _fillableList = (System.Collections.Generic.List<DG.Tweening.Tween>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<DG.Tweening.Tween>));
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.TweensById( _id, _playingOnly, _fillableList );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _id = translator.GetObject(L, 1, typeof(object));
                    bool _playingOnly = LuaAPI.lua_toboolean(L, 2);
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.TweensById( _id, _playingOnly );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _id = translator.GetObject(L, 1, typeof(object));
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.TweensById( _id );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.TweensById!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TweensByTarget_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.List<DG.Tweening.Tween>>(L, 3)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    bool _playingOnly = LuaAPI.lua_toboolean(L, 2);
                    System.Collections.Generic.List<DG.Tweening.Tween> _fillableList = (System.Collections.Generic.List<DG.Tweening.Tween>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<DG.Tweening.Tween>));
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.TweensByTarget( _target, _playingOnly, _fillableList );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    bool _playingOnly = LuaAPI.lua_toboolean(L, 2);
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.TweensByTarget( _target, _playingOnly );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _target = translator.GetObject(L, 1, typeof(object));
                    
                        System.Collections.Generic.List<DG.Tweening.Tween> gen_ret = DG.Tweening.DOTween.TweensByTarget( _target );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.DOTween.TweensByTarget!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_logBehaviour(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, DG.Tweening.DOTween.logBehaviour);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_useSafeMode(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, DG.Tweening.DOTween.useSafeMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_showUnityEditorReport(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, DG.Tweening.DOTween.showUnityEditorReport);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_timeScale(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, DG.Tweening.DOTween.timeScale);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_useSmoothDeltaTime(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, DG.Tweening.DOTween.useSmoothDeltaTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maxSmoothUnscaledTime(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, DG.Tweening.DOTween.maxSmoothUnscaledTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_drawGizmos(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, DG.Tweening.DOTween.drawGizmos);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultUpdateType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, DG.Tweening.DOTween.defaultUpdateType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultTimeScaleIndependent(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, DG.Tweening.DOTween.defaultTimeScaleIndependent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultAutoPlay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, DG.Tweening.DOTween.defaultAutoPlay);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultAutoKill(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, DG.Tweening.DOTween.defaultAutoKill);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultLoopType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushDGTweeningLoopType(L, DG.Tweening.DOTween.defaultLoopType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultRecyclable(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, DG.Tweening.DOTween.defaultRecyclable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultEaseType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushDGTweeningEase(L, DG.Tweening.DOTween.defaultEaseType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultEaseOvershootOrAmplitude(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, DG.Tweening.DOTween.defaultEaseOvershootOrAmplitude);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultEasePeriod(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, DG.Tweening.DOTween.defaultEasePeriod);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_logBehaviour(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			DG.Tweening.LogBehaviour gen_value;translator.Get(L, 1, out gen_value);
				DG.Tweening.DOTween.logBehaviour = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_useSafeMode(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.useSafeMode = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_showUnityEditorReport(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.showUnityEditorReport = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_timeScale(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.timeScale = (float)LuaAPI.lua_tonumber(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_useSmoothDeltaTime(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.useSmoothDeltaTime = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_maxSmoothUnscaledTime(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.maxSmoothUnscaledTime = (float)LuaAPI.lua_tonumber(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_drawGizmos(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.drawGizmos = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultUpdateType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			DG.Tweening.UpdateType gen_value;translator.Get(L, 1, out gen_value);
				DG.Tweening.DOTween.defaultUpdateType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultTimeScaleIndependent(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.defaultTimeScaleIndependent = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultAutoPlay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			DG.Tweening.AutoPlay gen_value;translator.Get(L, 1, out gen_value);
				DG.Tweening.DOTween.defaultAutoPlay = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultAutoKill(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.defaultAutoKill = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultLoopType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			DG.Tweening.LoopType gen_value;translator.Get(L, 1, out gen_value);
				DG.Tweening.DOTween.defaultLoopType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultRecyclable(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.defaultRecyclable = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultEaseType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			DG.Tweening.Ease gen_value;translator.Get(L, 1, out gen_value);
				DG.Tweening.DOTween.defaultEaseType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultEaseOvershootOrAmplitude(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.defaultEaseOvershootOrAmplitude = (float)LuaAPI.lua_tonumber(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultEasePeriod(RealStatePtr L)
        {
		    try {
                
			    DG.Tweening.DOTween.defaultEasePeriod = (float)LuaAPI.lua_tonumber(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
