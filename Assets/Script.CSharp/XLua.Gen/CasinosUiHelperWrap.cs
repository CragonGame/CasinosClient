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
    public class CasinosUiHelperWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.UiHelper);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 21, 2, 2);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "setGObjectVisible", _m_setGObjectVisible_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "uiColorText", _m_uiColorText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "resetActiveState", _m_resetActiveState_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "setActiveState", _m_setActiveState_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "switchActiveState", _m_switchActiveState_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "switchUi", _m_switchUi_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "switchInclusiveUi", _m_switchInclusiveUi_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "checkTime", _m_checkTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "resetObj", _m_resetObj_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "checkTextEmpty", _m_checkTextEmpty_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "checkTextLength", _m_checkTextLength_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetUiCamera", _m_GetUiCamera_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ConvertFloatToInt", _m_ConvertFloatToInt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getOnLineTimeDays", _m_getOnLineTimeDays_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "timeIsSameMinute", _m_timeIsSameMinute_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getLocalTm", _m_getLocalTm_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getLocalTmToString", _m_getLocalTmToString_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "formateAndoridIOSUrl", _m_formateAndoridIOSUrl_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "addEllipsisToStr", _m_addEllipsisToStr_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "createGoldFallParticle", _m_createGoldFallParticle_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "mDefaultHeight", _g_get_mDefaultHeight);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "mDefaultWidth", _g_get_mDefaultWidth);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "mDefaultHeight", _s_set_mDefaultHeight);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "mDefaultWidth", _s_set_mDefaultWidth);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Casinos.UiHelper does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_setGObjectVisible_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)&& translator.Assignable<FairyGUI.GObject>(L, 2)) 
                {
                    bool _is_visible = LuaAPI.lua_toboolean(L, 1);
                    FairyGUI.GObject _obj = (FairyGUI.GObject)translator.GetObject(L, 2, typeof(FairyGUI.GObject));
                    
                    Casinos.UiHelper.setGObjectVisible( _is_visible, _obj );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count >= 1&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<FairyGUI.GObject>(L, 2))) 
                {
                    bool _is_visible = LuaAPI.lua_toboolean(L, 1);
                    FairyGUI.GObject[] _objs = translator.GetParams<FairyGUI.GObject>(L, 2);
                    
                    Casinos.UiHelper.setGObjectVisible( _is_visible, _objs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.UiHelper.setGObjectVisible!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_uiColorText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Color _color;translator.Get(L, 1, out _color);
                    string _text = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = Casinos.UiHelper.uiColorText( _color, _text );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_resetActiveState_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject[] _objs = translator.GetParams<UnityEngine.GameObject>(L, 1);
                    
                    Casinos.UiHelper.resetActiveState( _objs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_setActiveState_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    bool _is_active = LuaAPI.lua_toboolean(L, 1);
                    UnityEngine.GameObject[] _objs = translator.GetParams<UnityEngine.GameObject>(L, 2);
                    
                    Casinos.UiHelper.setActiveState( _is_active, _objs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_switchActiveState_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject[] _objs = translator.GetParams<UnityEngine.GameObject>(L, 1);
                    
                    Casinos.UiHelper.switchActiveState( _objs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_switchUi_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject[] _uis = translator.GetParams<UnityEngine.GameObject>(L, 1);
                    
                    Casinos.UiHelper.switchUi( _uis );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_switchInclusiveUi_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _cur = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    UnityEngine.GameObject[] _uis = translator.GetParams<UnityEngine.GameObject>(L, 2);
                    
                    Casinos.UiHelper.switchInclusiveUi( _cur, _uis );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_checkTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _time = (float)LuaAPI.lua_tonumber(L, 1);
                    bool _time_out = LuaAPI.lua_toboolean(L, 2);
                    
                        string gen_ret = Casinos.UiHelper.checkTime( _time, ref _time_out );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    LuaAPI.lua_pushboolean(L, _time_out);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_resetObj_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _obj = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    
                    Casinos.UiHelper.resetObj( _obj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_checkTextEmpty_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = Casinos.UiHelper.checkTextEmpty( _text );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_checkTextLength_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    int _max_length = LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = Casinos.UiHelper.checkTextLength( _text, _max_length );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUiCamera_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        UnityEngine.Camera gen_ret = Casinos.UiHelper.GetUiCamera(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConvertFloatToInt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _num = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        int gen_ret = Casinos.UiHelper.ConvertFloatToInt( _num );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getOnLineTimeDays_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime _online_time;translator.Get(L, 1, out _online_time);
                    
                        string gen_ret = Casinos.UiHelper.getOnLineTimeDays( _online_time );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_timeIsSameMinute_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime _time1;translator.Get(L, 1, out _time1);
                    System.DateTime _time2;translator.Get(L, 2, out _time2);
                    
                        bool gen_ret = Casinos.UiHelper.timeIsSameMinute( _time1, _time2 );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getLocalTm_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime _tm;translator.Get(L, 1, out _tm);
                    
                        System.DateTime gen_ret = Casinos.UiHelper.getLocalTm( _tm );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getLocalTmToString_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime _tm;translator.Get(L, 1, out _tm);
                    
                        string gen_ret = Casinos.UiHelper.getLocalTmToString( _tm );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_formateAndoridIOSUrl_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = Casinos.UiHelper.formateAndoridIOSUrl( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_addEllipsisToStr_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    int _max_length = LuaAPI.xlua_tointeger(L, 2);
                    int _show_length = LuaAPI.xlua_tointeger(L, 3);
                    
                        string gen_ret = Casinos.UiHelper.addEllipsisToStr( _str, _max_length, _show_length );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_createGoldFallParticle_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Casinos.UiHelper.createGoldFallParticle(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mDefaultHeight(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, Casinos.UiHelper.mDefaultHeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mDefaultWidth(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, Casinos.UiHelper.mDefaultWidth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mDefaultHeight(RealStatePtr L)
        {
		    try {
                
			    Casinos.UiHelper.mDefaultHeight = (float)LuaAPI.lua_tonumber(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mDefaultWidth(RealStatePtr L)
        {
		    try {
                
			    Casinos.UiHelper.mDefaultWidth = (float)LuaAPI.lua_tonumber(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
