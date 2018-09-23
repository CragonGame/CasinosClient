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
    public class CasinosUiHelperCasinosWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.UiHelperCasinos);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 15, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "FormatePackageImagePath", _m_FormatePackageImagePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetMaJiangCardResName", _m_GetMaJiangCardResName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetRandomShootingTextColor", _m_GetRandomShootingTextColor_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "setParticle", _m_setParticle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCommonBgParticle", _m_SetCommonBgParticle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLoadingBgParticle", _m_SetLoadingBgParticle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetParticle", _m_SetParticle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FormatTmFromSecondToMinute", _m_FormatTmFromSecondToMinute_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FormatPlayerActorId", _m_FormatPlayerActorId_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getABCardResourceTitlePath", _m_getABCardResourceTitlePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getABAudioResourceTitlePath", _m_getABAudioResourceTitlePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getABItemResourceTitlePath", _m_getABItemResourceTitlePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getABParticleResourceTitlePath", _m_getABParticleResourceTitlePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "subStrToTargetLength", _m_subStrToTargetLength_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Casinos.UiHelperCasinos does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FormatePackageImagePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _package_name = LuaAPI.lua_tostring(L, 1);
                    string _image_name = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = Casinos.UiHelperCasinos.FormatePackageImagePath( _package_name, _image_name );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMaJiangCardResName_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Casinos.CardData _card_data = (Casinos.CardData)translator.GetObject(L, 1, typeof(Casinos.CardData));
                    
                        string gen_ret = Casinos.UiHelperCasinos.GetMaJiangCardResName( _card_data );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRandomShootingTextColor_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.UiHelperCasinos.GetRandomShootingTextColor(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_setParticle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GComponent _g = (FairyGUI.GComponent)translator.GetObject(L, 1, typeof(FairyGUI.GComponent));
                    string _particle_resouce_path = LuaAPI.lua_tostring(L, 2);
                    
                    Casinos.UiHelperCasinos.setParticle( _g, _particle_resouce_path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCommonBgParticle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GComponent _co_commonbg = (FairyGUI.GComponent)translator.GetObject(L, 1, typeof(FairyGUI.GComponent));
                    
                    Casinos.UiHelperCasinos.SetCommonBgParticle( _co_commonbg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLoadingBgParticle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GComponent _co_commonbg = (FairyGUI.GComponent)translator.GetObject(L, 1, typeof(FairyGUI.GComponent));
                    
                    Casinos.UiHelperCasinos.SetLoadingBgParticle( _co_commonbg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetParticle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GGraph _graph = (FairyGUI.GGraph)translator.GetObject(L, 1, typeof(FairyGUI.GGraph));
                    string _particle_name = LuaAPI.lua_tostring(L, 2);
                    
                    Casinos.UiHelperCasinos.SetParticle( _graph, _particle_name );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FormatTmFromSecondToMinute_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _tm = (float)LuaAPI.lua_tonumber(L, 1);
                    bool _showhours = LuaAPI.lua_toboolean(L, 2);
                    
                        string gen_ret = Casinos.UiHelperCasinos.FormatTmFromSecondToMinute( _tm, _showhours );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FormatPlayerActorId_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    long _actor_id = LuaAPI.lua_toint64(L, 1);
                    
                        string gen_ret = Casinos.UiHelperCasinos.FormatPlayerActorId( _actor_id );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getABCardResourceTitlePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.UiHelperCasinos.getABCardResourceTitlePath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getABAudioResourceTitlePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.UiHelperCasinos.getABAudioResourceTitlePath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getABItemResourceTitlePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.UiHelperCasinos.getABItemResourceTitlePath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getABParticleResourceTitlePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.UiHelperCasinos.getABParticleResourceTitlePath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_subStrToTargetLength_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    int _target_length = LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = Casinos.UiHelperCasinos.subStrToTargetLength( _str, _target_length );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
