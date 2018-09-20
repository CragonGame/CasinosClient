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
    public class DGTweeningTweenWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DG.Tweening.Tween);
			Utils.BeginObjectRegister(type, L, translator, 0, 56, 21, 17);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Complete", _m_Complete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Flip", _m_Flip);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ForceInit", _m_ForceInit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Goto", _m_Goto);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Kill", _m_Kill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Pause", _m_Pause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Play", _m_Play);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayBackwards", _m_PlayBackwards);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayForward", _m_PlayForward);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Restart", _m_Restart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Rewind", _m_Rewind);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SmoothRewind", _m_SmoothRewind);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TogglePause", _m_TogglePause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GotoWaypoint", _m_GotoWaypoint);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForCompletion", _m_WaitForCompletion);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForRewind", _m_WaitForRewind);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForKill", _m_WaitForKill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForElapsedLoops", _m_WaitForElapsedLoops);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForPosition", _m_WaitForPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForStart", _m_WaitForStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CompletedLoops", _m_CompletedLoops);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Delay", _m_Delay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Duration", _m_Duration);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Elapsed", _m_Elapsed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ElapsedPercentage", _m_ElapsedPercentage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ElapsedDirectionalPercentage", _m_ElapsedDirectionalPercentage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsActive", _m_IsActive);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsBackwards", _m_IsBackwards);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsComplete", _m_IsComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsInitialized", _m_IsInitialized);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsPlaying", _m_IsPlaying);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Loops", _m_Loops);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PathGetPoint", _m_PathGetPoint);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PathGetDrawPoints", _m_PathGetDrawPoints);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PathLength", _m_PathLength);
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
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDelay", _m_SetDelay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRelative", _m_SetRelative);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpeedBased", _m_SetSpeedBased);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOTimeScale", _m_DOTimeScale);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "isRelative", _g_get_isRelative);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "active", _g_get_active);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fullPosition", _g_get_fullPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "playedOnce", _g_get_playedOnce);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "position", _g_get_position);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "timeScale", _g_get_timeScale);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isBackwards", _g_get_isBackwards);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "id", _g_get_id);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "stringId", _g_get_stringId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "intId", _g_get_intId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "target", _g_get_target);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPlay", _g_get_onPlay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onPause", _g_get_onPause);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onRewind", _g_get_onRewind);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onUpdate", _g_get_onUpdate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onStepComplete", _g_get_onStepComplete);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onComplete", _g_get_onComplete);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onKill", _g_get_onKill);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onWaypointChange", _g_get_onWaypointChange);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "easeOvershootOrAmplitude", _g_get_easeOvershootOrAmplitude);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "easePeriod", _g_get_easePeriod);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "fullPosition", _s_set_fullPosition);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "timeScale", _s_set_timeScale);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isBackwards", _s_set_isBackwards);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "id", _s_set_id);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "stringId", _s_set_stringId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "intId", _s_set_intId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "target", _s_set_target);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPlay", _s_set_onPlay);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onPause", _s_set_onPause);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onRewind", _s_set_onRewind);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onUpdate", _s_set_onUpdate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onStepComplete", _s_set_onStepComplete);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onComplete", _s_set_onComplete);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onKill", _s_set_onKill);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onWaypointChange", _s_set_onWaypointChange);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "easeOvershootOrAmplitude", _s_set_easeOvershootOrAmplitude);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "easePeriod", _s_set_easePeriod);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "DG.Tweening.Tween does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Complete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Complete(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _withCallbacks = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Complete( _withCallbacks );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.Complete!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Flip(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Flip(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ForceInit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ForceInit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Goto(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    float _to = (float)LuaAPI.lua_tonumber(L, 2);
                    bool _andPlay = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.Goto( _to, _andPlay );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float _to = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Goto( _to );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.Goto!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Kill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _complete = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Kill( _complete );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Kill(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.Kill!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_PlayBackwards(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PlayBackwards(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayForward(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PlayForward(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Restart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 2);
                    float _changeDelayTo = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.Restart( _includeDelay, _changeDelayTo );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Restart( _includeDelay );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Restart(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.Restart!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Rewind(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Rewind( _includeDelay );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Rewind(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.Rewind!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SmoothRewind(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SmoothRewind(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TogglePause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.TogglePause(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GotoWaypoint(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    int _waypointIndex = LuaAPI.xlua_tointeger(L, 2);
                    bool _andPlay = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.GotoWaypoint( _waypointIndex, _andPlay );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _waypointIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.GotoWaypoint( _waypointIndex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.GotoWaypoint!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForCompletion(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.YieldInstruction gen_ret = gen_to_be_invoked.WaitForCompletion(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForRewind(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.YieldInstruction gen_ret = gen_to_be_invoked.WaitForRewind(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForKill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.YieldInstruction gen_ret = gen_to_be_invoked.WaitForKill(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForElapsedLoops(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _elapsedLoops = LuaAPI.xlua_tointeger(L, 2);
                    
                        UnityEngine.YieldInstruction gen_ret = gen_to_be_invoked.WaitForElapsedLoops( _elapsedLoops );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _position = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        UnityEngine.YieldInstruction gen_ret = gen_to_be_invoked.WaitForPosition( _position );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.Coroutine gen_ret = gen_to_be_invoked.WaitForStart(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CompletedLoops(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.CompletedLoops(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Delay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        float gen_ret = gen_to_be_invoked.Delay(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Duration(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _includeLoops = LuaAPI.lua_toboolean(L, 2);
                    
                        float gen_ret = gen_to_be_invoked.Duration( _includeLoops );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        float gen_ret = gen_to_be_invoked.Duration(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.Duration!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Elapsed(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _includeLoops = LuaAPI.lua_toboolean(L, 2);
                    
                        float gen_ret = gen_to_be_invoked.Elapsed( _includeLoops );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        float gen_ret = gen_to_be_invoked.Elapsed(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.Elapsed!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ElapsedPercentage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _includeLoops = LuaAPI.lua_toboolean(L, 2);
                    
                        float gen_ret = gen_to_be_invoked.ElapsedPercentage( _includeLoops );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        float gen_ret = gen_to_be_invoked.ElapsedPercentage(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.ElapsedPercentage!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ElapsedDirectionalPercentage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        float gen_ret = gen_to_be_invoked.ElapsedDirectionalPercentage(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsActive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsActive(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsBackwards(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsBackwards(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsComplete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsComplete(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsInitialized(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsInitialized(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsPlaying(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsPlaying(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Loops(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.Loops(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PathGetPoint(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _pathPercentage = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        UnityEngine.Vector3 gen_ret = gen_to_be_invoked.PathGetPoint( _pathPercentage );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PathGetDrawPoints(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _subdivisionsXSegment = LuaAPI.xlua_tointeger(L, 2);
                    
                        UnityEngine.Vector3[] gen_ret = gen_to_be_invoked.PathGetDrawPoints( _subdivisionsXSegment );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        UnityEngine.Vector3[] gen_ret = gen_to_be_invoked.PathGetDrawPoints(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.PathGetDrawPoints!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PathLength(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        float gen_ret = gen_to_be_invoked.PathLength(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.SetAutoKill!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.SetId!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.SetLoops!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEase(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.SetEase!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRecyclable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.SetRecyclable!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.SetUpdate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.SetAs!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDelay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.SetRelative!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpeedBased(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to DG.Tweening.Tween.SetSpeedBased!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOTimeScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        DG.Tweening.Tweener gen_ret = gen_to_be_invoked.DOTimeScale( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isRelative(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isRelative);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_active(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.active);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fullPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.fullPosition);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_playedOnce(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.playedOnce);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_position(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.position);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_timeScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.timeScale);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isBackwards(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isBackwards);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.id);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_stringId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.stringId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_intId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.intId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_target(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.target);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPlay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onPlay);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onPause(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onPause);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onRewind(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onRewind);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onUpdate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onStepComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onStepComplete);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onComplete);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onKill(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onKill);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onWaypointChange(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onWaypointChange);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_easeOvershootOrAmplitude(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.easeOvershootOrAmplitude);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_easePeriod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.easePeriod);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fullPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fullPosition = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_timeScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.timeScale = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isBackwards(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isBackwards = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.id = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_stringId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.stringId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_intId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.intId = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_target(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.target = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPlay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onPlay = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onPause(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onPause = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onRewind(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onRewind = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onUpdate = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onStepComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onStepComplete = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onComplete = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onKill(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onKill = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onWaypointChange(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onWaypointChange = translator.GetDelegate<DG.Tweening.TweenCallback<int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_easeOvershootOrAmplitude(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.easeOvershootOrAmplitude = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_easePeriod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DG.Tweening.Tween gen_to_be_invoked = (DG.Tweening.Tween)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.easePeriod = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
