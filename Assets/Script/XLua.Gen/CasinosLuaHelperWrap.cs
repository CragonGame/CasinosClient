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
    public class CasinosLuaHelperWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.LuaHelper);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 65, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "readAllText", _m_readAllText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "writeFile", _m_writeFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GObjectCastToGCom", _m_GObjectCastToGCom_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetVector2", _m_GetVector2_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetVector3", _m_GetVector3_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getDisObj", _m_getDisObj_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getGameObj", _m_getGameObj_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnityObjectCastToTexture", _m_UnityObjectCastToTexture_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "deleteFile", _m_deleteFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "spliteStr", _m_spliteStr_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "insertToStr", _m_insertToStr_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "formatNumToStr", _m_formatNumToStr_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseSysLanToInt", _m_parseSysLanToInt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "objIsComponent", _m_objIsComponent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "objIsBtn", _m_objIsBtn_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "addFairyGUIPanel", _m_addFairyGUIPanel_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getDeviceUniqueIdentifier", _m_getDeviceUniqueIdentifier_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getDeviceName", _m_getDeviceName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getDeviceModel", _m_getDeviceModel_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getDeviceOperatingSystem", _m_getDeviceOperatingSystem_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getDevicedeviceType", _m_getDevicedeviceType_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GLoaderCastToGLoaderEx", _m_GLoaderCastToGLoaderEx_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EventDispatcherCastToGComponent", _m_EventDispatcherCastToGComponent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getIconName", _m_getIconName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "bytes2StringByDefault", _m_bytes2StringByDefault_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "bytes2StringByUTF8", _m_bytes2StringByUTF8_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "bytes2StringByUnicode", _m_bytes2StringByUnicode_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "bytes2StringByASCII", _m_bytes2StringByASCII_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "string2BytesByDefault", _m_string2BytesByDefault_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "string2BytesByUTF8", _m_string2BytesByUTF8_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "string2BytesByUnicode", _m_string2BytesByUnicode_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "string2BytesByASCII", _m_string2BytesByASCII_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getBytesLenShort", _m_getBytesLenShort_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getBytesLenUShort", _m_getBytesLenUShort_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseShortToUShort", _m_parseShortToUShort_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseShortToUInt", _m_parseShortToUInt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseShortToULong", _m_parseShortToULong_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseShortToInt", _m_parseShortToInt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseShortToLong", _m_parseShortToLong_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseUShortToShort", _m_parseUShortToShort_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseUShortToUInt", _m_parseUShortToUInt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseUShortToULong", _m_parseUShortToULong_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseUShortToInt", _m_parseUShortToInt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseUShortToLong", _m_parseUShortToLong_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseIntToLong", _m_parseIntToLong_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "parseIntToULong", _m_parseIntToULong_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "readBytesWithLength", _m_readBytesWithLength_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DictionaryToLuatable", _m_DictionaryToLuatable_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ListToLuatable", _m_ListToLuatable_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TimeDifferenceNow", _m_TimeDifferenceNow_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNowFormat", _m_GetNowFormat_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DataTimeToString", _m_DataTimeToString_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNewStringStringMap", _m_GetNewStringStringMap_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNewStringMapStringMap", _m_GetNewStringMapStringMap_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNewByteObjMap", _m_GetNewByteObjMap_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetHashTableValue", _m_GetHashTableValue_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNewCardList", _m_GetNewCardList_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ParseHandRankTypeTexasHToStr", _m_ParseHandRankTypeTexasHToStr_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ParseHandRankTypeTexasToStr", _m_ParseHandRankTypeTexasToStr_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetComponentAutoDestroyParticle", _m_GetComponentAutoDestroyParticle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EnumCastToInt", _m_EnumCastToInt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FormatPlayerActorId", _m_FormatPlayerActorId_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FormatTmFromSecondToMinute", _m_FormatTmFromSecondToMinute_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "NewParticleArry", _m_NewParticleArry_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Casinos.LuaHelper does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_readAllText_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = Casinos.LuaHelper.readAllText( _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_writeFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    Casinos.LuaHelper.writeFile( _bytes, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GObjectCastToGCom_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _obj = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    
                        FairyGUI.GComponent gen_ret = Casinos.LuaHelper.GObjectCastToGCom( _obj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetVector2_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 1);
                    float _y = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        UnityEngine.Vector2 gen_ret = Casinos.LuaHelper.GetVector2( _x, _y );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetVector3_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 1);
                    float _y = (float)LuaAPI.lua_tonumber(L, 2);
                    float _z = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        UnityEngine.Vector3 gen_ret = Casinos.LuaHelper.GetVector3( _x, _y, _z );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getDisObj_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _obj = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    
                        FairyGUI.DisplayObject gen_ret = Casinos.LuaHelper.getDisObj( _obj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getGameObj_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _obj = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    
                        UnityEngine.GameObject gen_ret = Casinos.LuaHelper.getGameObj( _obj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnityObjectCastToTexture_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    UnityEngine.Object _obj = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    bool _need_movemipmap = LuaAPI.lua_toboolean(L, 2);
                    
                        UnityEngine.Texture gen_ret = Casinos.LuaHelper.UnityObjectCastToTexture( _obj, _need_movemipmap );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.Object>(L, 1)) 
                {
                    UnityEngine.Object _obj = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                        UnityEngine.Texture gen_ret = Casinos.LuaHelper.UnityObjectCastToTexture( _obj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.LuaHelper.UnityObjectCastToTexture!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_deleteFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    Casinos.LuaHelper.deleteFile( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_spliteStr_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    string _splite_s = LuaAPI.lua_tostring(L, 2);
                    
                        XLua.LuaTable gen_ret = Casinos.LuaHelper.spliteStr( _str, _splite_s );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_insertToStr_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _target = LuaAPI.lua_tostring(L, 1);
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    string _insert_obj = LuaAPI.lua_tostring(L, 3);
                    
                        string gen_ret = Casinos.LuaHelper.insertToStr( _target, _index, _insert_obj );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_formatNumToStr_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _num = (float)LuaAPI.lua_tonumber(L, 1);
                    string _format_info = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = Casinos.LuaHelper.formatNumToStr( _num, _format_info );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseSysLanToInt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = Casinos.LuaHelper.parseSysLanToInt(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_objIsComponent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _obj = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    
                        bool gen_ret = Casinos.LuaHelper.objIsComponent( _obj );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_objIsBtn_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GObject _obj = (FairyGUI.GObject)translator.GetObject(L, 1, typeof(FairyGUI.GObject));
                    
                        bool gen_ret = Casinos.LuaHelper.objIsBtn( _obj );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_addFairyGUIPanel_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _obj = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    
                        FairyGUI.UIPanel gen_ret = Casinos.LuaHelper.addFairyGUIPanel( _obj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getDeviceUniqueIdentifier_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.LuaHelper.getDeviceUniqueIdentifier(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getDeviceName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.LuaHelper.getDeviceName(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getDeviceModel_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.LuaHelper.getDeviceModel(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getDeviceOperatingSystem_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.LuaHelper.getDeviceOperatingSystem(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getDevicedeviceType_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = Casinos.LuaHelper.getDevicedeviceType(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GLoaderCastToGLoaderEx_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.GLoader _loader = (FairyGUI.GLoader)translator.GetObject(L, 1, typeof(FairyGUI.GLoader));
                    
                        Casinos.GLoaderEx gen_ret = Casinos.LuaHelper.GLoaderCastToGLoaderEx( _loader );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EventDispatcherCastToGComponent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.EventDispatcher _ev = (FairyGUI.EventDispatcher)translator.GetObject(L, 1, typeof(FairyGUI.EventDispatcher));
                    
                        FairyGUI.GComponent gen_ret = Casinos.LuaHelper.EventDispatcherCastToGComponent( _ev );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getIconName_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    bool _is_small = LuaAPI.lua_toboolean(L, 1);
                    string _icon_name = LuaAPI.lua_tostring(L, 2);
                    string _icon_resource_name1 = LuaAPI.lua_tostring(L, 3);
                    
                        XLua.LuaTable gen_ret = Casinos.LuaHelper.getIconName( _is_small, _icon_name, _icon_resource_name1 );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_bytes2StringByDefault_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    
                        string gen_ret = Casinos.LuaHelper.bytes2StringByDefault( _bytes );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_bytes2StringByUTF8_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    
                        string gen_ret = Casinos.LuaHelper.bytes2StringByUTF8( _bytes );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_bytes2StringByUnicode_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    
                        string gen_ret = Casinos.LuaHelper.bytes2StringByUnicode( _bytes );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_bytes2StringByASCII_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    
                        string gen_ret = Casinos.LuaHelper.bytes2StringByASCII( _bytes );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_string2BytesByDefault_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] gen_ret = Casinos.LuaHelper.string2BytesByDefault( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_string2BytesByUTF8_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] gen_ret = Casinos.LuaHelper.string2BytesByUTF8( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_string2BytesByUnicode_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] gen_ret = Casinos.LuaHelper.string2BytesByUnicode( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_string2BytesByASCII_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] gen_ret = Casinos.LuaHelper.string2BytesByASCII( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getBytesLenShort_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    
                        short gen_ret = Casinos.LuaHelper.getBytesLenShort( _bytes );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getBytesLenUShort_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    
                        ushort gen_ret = Casinos.LuaHelper.getBytesLenUShort( _bytes );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseShortToUShort_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    short _num = (short)LuaAPI.xlua_tointeger(L, 1);
                    
                        ushort gen_ret = Casinos.LuaHelper.parseShortToUShort( _num );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseShortToUInt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    short _num = (short)LuaAPI.xlua_tointeger(L, 1);
                    
                        uint gen_ret = Casinos.LuaHelper.parseShortToUInt( _num );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseShortToULong_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    short _num = (short)LuaAPI.xlua_tointeger(L, 1);
                    
                        ulong gen_ret = Casinos.LuaHelper.parseShortToULong( _num );
                        LuaAPI.lua_pushuint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseShortToInt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    short _num = (short)LuaAPI.xlua_tointeger(L, 1);
                    
                        int gen_ret = Casinos.LuaHelper.parseShortToInt( _num );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseShortToLong_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    short _num = (short)LuaAPI.xlua_tointeger(L, 1);
                    
                        long gen_ret = Casinos.LuaHelper.parseShortToLong( _num );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseUShortToShort_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    ushort _num = (ushort)LuaAPI.xlua_tointeger(L, 1);
                    
                        short gen_ret = Casinos.LuaHelper.parseUShortToShort( _num );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseUShortToUInt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    ushort _num = (ushort)LuaAPI.xlua_tointeger(L, 1);
                    
                        uint gen_ret = Casinos.LuaHelper.parseUShortToUInt( _num );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseUShortToULong_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    ushort _num = (ushort)LuaAPI.xlua_tointeger(L, 1);
                    
                        ulong gen_ret = Casinos.LuaHelper.parseUShortToULong( _num );
                        LuaAPI.lua_pushuint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseUShortToInt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    ushort _num = (ushort)LuaAPI.xlua_tointeger(L, 1);
                    
                        int gen_ret = Casinos.LuaHelper.parseUShortToInt( _num );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseUShortToLong_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    ushort _num = (ushort)LuaAPI.xlua_tointeger(L, 1);
                    
                        long gen_ret = Casinos.LuaHelper.parseUShortToLong( _num );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseIntToLong_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _num = LuaAPI.xlua_tointeger(L, 1);
                    
                        long gen_ret = Casinos.LuaHelper.parseIntToLong( _num );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_parseIntToULong_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _num = LuaAPI.xlua_tointeger(L, 1);
                    
                        ulong gen_ret = Casinos.LuaHelper.parseIntToULong( _num );
                        LuaAPI.lua_pushuint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_readBytesWithLength_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 1);
                    int _offset = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    
                        byte[] gen_ret = Casinos.LuaHelper.readBytesWithLength( _bytes, _offset, _length );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DictionaryToLuatable_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Collections.IDictionary _obj = (System.Collections.IDictionary)translator.GetObject(L, 1, typeof(System.Collections.IDictionary));
                    
                        XLua.LuaTable gen_ret = Casinos.LuaHelper.DictionaryToLuatable( _obj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ListToLuatable_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Collections.IList _obj = (System.Collections.IList)translator.GetObject(L, 1, typeof(System.Collections.IList));
                    
                        XLua.LuaTable gen_ret = Casinos.LuaHelper.ListToLuatable( _obj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TimeDifferenceNow_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime _time;translator.Get(L, 1, out _time);
                    
                        System.TimeSpan gen_ret = Casinos.LuaHelper.TimeDifferenceNow( _time );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNowFormat_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    string _format = LuaAPI.lua_tostring(L, 1);
                    bool _to_local = LuaAPI.lua_toboolean(L, 2);
                    
                        string gen_ret = Casinos.LuaHelper.GetNowFormat( _format, _to_local );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _format = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = Casinos.LuaHelper.GetNowFormat( _format );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.LuaHelper.GetNowFormat!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DataTimeToString_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime _time;translator.Get(L, 1, out _time);
                    string _format = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = Casinos.LuaHelper.DataTimeToString( _time, _format );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNewStringStringMap_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        System.Collections.Generic.Dictionary<string, string> gen_ret = Casinos.LuaHelper.GetNewStringStringMap(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNewStringMapStringMap_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>> gen_ret = Casinos.LuaHelper.GetNewStringMapStringMap(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNewByteObjMap_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        System.Collections.Generic.Dictionary<byte, object> gen_ret = Casinos.LuaHelper.GetNewByteObjMap(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetHashTableValue_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Collections.Hashtable _t = (System.Collections.Hashtable)translator.GetObject(L, 1, typeof(System.Collections.Hashtable));
                    object _t_key = translator.GetObject(L, 2, typeof(object));
                    
                        object gen_ret = Casinos.LuaHelper.GetHashTableValue( _t, _t_key );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNewCardList_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        System.Collections.Generic.List<Casinos.Card> gen_ret = Casinos.LuaHelper.GetNewCardList(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ParseHandRankTypeTexasHToStr_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Casinos.HandRankTypeTexasH _rank;translator.Get(L, 1, out _rank);
                    
                        string gen_ret = Casinos.LuaHelper.ParseHandRankTypeTexasHToStr( _rank );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ParseHandRankTypeTexasToStr_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Casinos.HandRankTypeTexas _rank;translator.Get(L, 1, out _rank);
                    
                        string gen_ret = Casinos.LuaHelper.ParseHandRankTypeTexasToStr( _rank );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetComponentAutoDestroyParticle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _gameobject = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    
                        Casinos.AutoDestroyParticle gen_ret = Casinos.LuaHelper.GetComponentAutoDestroyParticle( _gameobject );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnumCastToInt_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Enum _e = (System.Enum)translator.GetObject(L, 1, typeof(System.Enum));
                    
                        int gen_ret = Casinos.LuaHelper.EnumCastToInt( _e );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
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
                    
                        string gen_ret = Casinos.LuaHelper.FormatPlayerActorId( _actor_id );
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
                    
                        string gen_ret = Casinos.LuaHelper.FormatTmFromSecondToMinute( _tm, _showhours );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NewParticleArry_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _length = LuaAPI.xlua_tointeger(L, 1);
                    
                        UnityEngine.ParticleSystem.Particle[] gen_ret = Casinos.LuaHelper.NewParticleArry( _length );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
