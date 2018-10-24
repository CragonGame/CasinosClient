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

		    Utils.BeginClassRegister(type, L, __CreateInstance, 4, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "FormatePackageImagePath", _m_FormatePackageImagePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FormatTmFromSecondToMinute", _m_FormatTmFromSecondToMinute_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FormatPlayerActorId", _m_FormatPlayerActorId_xlua_st_);
            
			
            
			
			
			
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
        
        
        
        
        
        
		
		
		
		
    }
}
