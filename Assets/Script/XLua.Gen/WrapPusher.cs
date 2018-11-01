#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;


namespace XLua
{
    public partial class ObjectTranslator
    {
        
        class IniterAdderUnityEngineVector2
        {
            static IniterAdderUnityEngineVector2()
            {
                LuaEnv.AddIniter(Init);
            }
			
			static void Init(LuaEnv luaenv, ObjectTranslator translator)
			{
			
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector2>(translator.PushUnityEngineVector2, translator.Get, translator.UpdateUnityEngineVector2);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector3>(translator.PushUnityEngineVector3, translator.Get, translator.UpdateUnityEngineVector3);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector4>(translator.PushUnityEngineVector4, translator.Get, translator.UpdateUnityEngineVector4);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Color>(translator.PushUnityEngineColor, translator.Get, translator.UpdateUnityEngineColor);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Quaternion>(translator.PushUnityEngineQuaternion, translator.Get, translator.UpdateUnityEngineQuaternion);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray>(translator.PushUnityEngineRay, translator.Get, translator.UpdateUnityEngineRay);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Bounds>(translator.PushUnityEngineBounds, translator.Get, translator.UpdateUnityEngineBounds);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray2D>(translator.PushUnityEngineRay2D, translator.Get, translator.UpdateUnityEngineRay2D);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.ApplicationInstallMode>(translator.PushUnityEngineApplicationInstallMode, translator.Get, translator.UpdateUnityEngineApplicationInstallMode);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.ApplicationSandboxType>(translator.PushUnityEngineApplicationSandboxType, translator.Get, translator.UpdateUnityEngineApplicationSandboxType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.CameraType>(translator.PushUnityEngineCameraType, translator.Get, translator.UpdateUnityEngineCameraType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.CameraClearFlags>(translator.PushUnityEngineCameraClearFlags, translator.Get, translator.UpdateUnityEngineCameraClearFlags);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.NetworkReachability>(translator.PushUnityEngineNetworkReachability, translator.Get, translator.UpdateUnityEngineNetworkReachability);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.RuntimePlatform>(translator.PushUnityEngineRuntimePlatform, translator.Get, translator.UpdateUnityEngineRuntimePlatform);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.SystemLanguage>(translator.PushUnityEngineSystemLanguage, translator.Get, translator.UpdateUnityEngineSystemLanguage);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.ThreadPriority>(translator.PushUnityEngineThreadPriority, translator.Get, translator.UpdateUnityEngineThreadPriority);
				translator.RegisterPushAndGetAndUpdate<FairyGUI.EaseType>(translator.PushFairyGUIEaseType, translator.Get, translator.UpdateFairyGUIEaseType);
				translator.RegisterPushAndGetAndUpdate<FairyGUI.RelationType>(translator.PushFairyGUIRelationType, translator.Get, translator.UpdateFairyGUIRelationType);
				translator.RegisterPushAndGetAndUpdate<FairyGUI.TweenPropType>(translator.PushFairyGUITweenPropType, translator.Get, translator.UpdateFairyGUITweenPropType);
				translator.RegisterPushAndGetAndUpdate<_eAsyncAssetLoadType>(translator.Push_eAsyncAssetLoadType, translator.Get, translator.Update_eAsyncAssetLoadType);
				translator.RegisterPushAndGetAndUpdate<Casinos._eChatItemType>(translator.PushCasinos_eChatItemType, translator.Get, translator.UpdateCasinos_eChatItemType);
				translator.RegisterPushAndGetAndUpdate<Casinos._eLoginType>(translator.PushCasinos_eLoginType, translator.Get, translator.UpdateCasinos_eLoginType);
				translator.RegisterPushAndGetAndUpdate<_ePayType>(translator.Push_ePayType, translator.Get, translator.Update_ePayType);
				translator.RegisterPushAndGetAndUpdate<Casinos._eProjectItemDisplayNameKey>(translator.PushCasinos_eProjectItemDisplayNameKey, translator.Get, translator.UpdateCasinos_eProjectItemDisplayNameKey);
				translator.RegisterPushAndGetAndUpdate<ZXing.BarcodeFormat>(translator.PushZXingBarcodeFormat, translator.Get, translator.UpdateZXingBarcodeFormat);
				translator.RegisterPushAndGetAndUpdate<Casinos._eEditorRunSourcePlatform>(translator.PushCasinos_eEditorRunSourcePlatform, translator.Get, translator.UpdateCasinos_eEditorRunSourcePlatform);
				translator.RegisterPushAndGetAndUpdate<Casinos.DirType>(translator.PushCasinosDirType, translator.Get, translator.UpdateCasinosDirType);
			
			}
        }
        
        static IniterAdderUnityEngineVector2 s_IniterAdderUnityEngineVector2_dumb_obj = new IniterAdderUnityEngineVector2();
        static IniterAdderUnityEngineVector2 IniterAdderUnityEngineVector2_dumb_obj {get{return s_IniterAdderUnityEngineVector2_dumb_obj;}}
        
        
        int UnityEngineVector2_TypeID = -1;
        public void PushUnityEngineVector2(RealStatePtr L, UnityEngine.Vector2 val)
        {
            if (UnityEngineVector2_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector2_TypeID = getTypeId(L, typeof(UnityEngine.Vector2), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 8, UnityEngineVector2_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector2 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector2 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector2");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector2)objectCasters.GetCaster(typeof(UnityEngine.Vector2))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector2(RealStatePtr L, int index, UnityEngine.Vector2 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector2 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector3_TypeID = -1;
        public void PushUnityEngineVector3(RealStatePtr L, UnityEngine.Vector3 val)
        {
            if (UnityEngineVector3_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector3_TypeID = getTypeId(L, typeof(UnityEngine.Vector3), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 12, UnityEngineVector3_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector3 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector3 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector3");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector3)objectCasters.GetCaster(typeof(UnityEngine.Vector3))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector3(RealStatePtr L, int index, UnityEngine.Vector3 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector3 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector4_TypeID = -1;
        public void PushUnityEngineVector4(RealStatePtr L, UnityEngine.Vector4 val)
        {
            if (UnityEngineVector4_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector4_TypeID = getTypeId(L, typeof(UnityEngine.Vector4), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineVector4_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector4 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector4 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector4");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector4)objectCasters.GetCaster(typeof(UnityEngine.Vector4))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector4(RealStatePtr L, int index, UnityEngine.Vector4 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector4 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineColor_TypeID = -1;
        public void PushUnityEngineColor(RealStatePtr L, UnityEngine.Color val)
        {
            if (UnityEngineColor_TypeID == -1)
            {
			    bool is_first;
                UnityEngineColor_TypeID = getTypeId(L, typeof(UnityEngine.Color), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineColor_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Color ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Color val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Color");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Color)objectCasters.GetCaster(typeof(UnityEngine.Color))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineColor(RealStatePtr L, int index, UnityEngine.Color val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Color ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineQuaternion_TypeID = -1;
        public void PushUnityEngineQuaternion(RealStatePtr L, UnityEngine.Quaternion val)
        {
            if (UnityEngineQuaternion_TypeID == -1)
            {
			    bool is_first;
                UnityEngineQuaternion_TypeID = getTypeId(L, typeof(UnityEngine.Quaternion), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineQuaternion_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Quaternion ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Quaternion val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Quaternion");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Quaternion)objectCasters.GetCaster(typeof(UnityEngine.Quaternion))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineQuaternion(RealStatePtr L, int index, UnityEngine.Quaternion val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Quaternion ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay_TypeID = -1;
        public void PushUnityEngineRay(RealStatePtr L, UnityEngine.Ray val)
        {
            if (UnityEngineRay_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay_TypeID = getTypeId(L, typeof(UnityEngine.Ray), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineRay_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray)objectCasters.GetCaster(typeof(UnityEngine.Ray))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay(RealStatePtr L, int index, UnityEngine.Ray val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineBounds_TypeID = -1;
        public void PushUnityEngineBounds(RealStatePtr L, UnityEngine.Bounds val)
        {
            if (UnityEngineBounds_TypeID == -1)
            {
			    bool is_first;
                UnityEngineBounds_TypeID = getTypeId(L, typeof(UnityEngine.Bounds), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineBounds_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Bounds ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Bounds val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Bounds");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Bounds)objectCasters.GetCaster(typeof(UnityEngine.Bounds))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineBounds(RealStatePtr L, int index, UnityEngine.Bounds val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Bounds ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay2D_TypeID = -1;
        public void PushUnityEngineRay2D(RealStatePtr L, UnityEngine.Ray2D val)
        {
            if (UnityEngineRay2D_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay2D_TypeID = getTypeId(L, typeof(UnityEngine.Ray2D), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineRay2D_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray2D ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray2D val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray2D");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray2D)objectCasters.GetCaster(typeof(UnityEngine.Ray2D))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay2D(RealStatePtr L, int index, UnityEngine.Ray2D val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray2D ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineApplicationInstallMode_TypeID = -1;
		int UnityEngineApplicationInstallMode_EnumRef = -1;
        
        public void PushUnityEngineApplicationInstallMode(RealStatePtr L, UnityEngine.ApplicationInstallMode val)
        {
            if (UnityEngineApplicationInstallMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineApplicationInstallMode_TypeID = getTypeId(L, typeof(UnityEngine.ApplicationInstallMode), out is_first);
				
				if (UnityEngineApplicationInstallMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.ApplicationInstallMode));
				    UnityEngineApplicationInstallMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineApplicationInstallMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineApplicationInstallMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.ApplicationInstallMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineApplicationInstallMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.ApplicationInstallMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineApplicationInstallMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.ApplicationInstallMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.ApplicationInstallMode");
                }
				val = (UnityEngine.ApplicationInstallMode)e;
                
            }
            else
            {
                val = (UnityEngine.ApplicationInstallMode)objectCasters.GetCaster(typeof(UnityEngine.ApplicationInstallMode))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineApplicationInstallMode(RealStatePtr L, int index, UnityEngine.ApplicationInstallMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineApplicationInstallMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.ApplicationInstallMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.ApplicationInstallMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineApplicationSandboxType_TypeID = -1;
		int UnityEngineApplicationSandboxType_EnumRef = -1;
        
        public void PushUnityEngineApplicationSandboxType(RealStatePtr L, UnityEngine.ApplicationSandboxType val)
        {
            if (UnityEngineApplicationSandboxType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineApplicationSandboxType_TypeID = getTypeId(L, typeof(UnityEngine.ApplicationSandboxType), out is_first);
				
				if (UnityEngineApplicationSandboxType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.ApplicationSandboxType));
				    UnityEngineApplicationSandboxType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineApplicationSandboxType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineApplicationSandboxType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.ApplicationSandboxType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineApplicationSandboxType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.ApplicationSandboxType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineApplicationSandboxType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.ApplicationSandboxType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.ApplicationSandboxType");
                }
				val = (UnityEngine.ApplicationSandboxType)e;
                
            }
            else
            {
                val = (UnityEngine.ApplicationSandboxType)objectCasters.GetCaster(typeof(UnityEngine.ApplicationSandboxType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineApplicationSandboxType(RealStatePtr L, int index, UnityEngine.ApplicationSandboxType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineApplicationSandboxType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.ApplicationSandboxType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.ApplicationSandboxType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineCameraType_TypeID = -1;
		int UnityEngineCameraType_EnumRef = -1;
        
        public void PushUnityEngineCameraType(RealStatePtr L, UnityEngine.CameraType val)
        {
            if (UnityEngineCameraType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineCameraType_TypeID = getTypeId(L, typeof(UnityEngine.CameraType), out is_first);
				
				if (UnityEngineCameraType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.CameraType));
				    UnityEngineCameraType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineCameraType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineCameraType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.CameraType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineCameraType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.CameraType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineCameraType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.CameraType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.CameraType");
                }
				val = (UnityEngine.CameraType)e;
                
            }
            else
            {
                val = (UnityEngine.CameraType)objectCasters.GetCaster(typeof(UnityEngine.CameraType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineCameraType(RealStatePtr L, int index, UnityEngine.CameraType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineCameraType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.CameraType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.CameraType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineCameraClearFlags_TypeID = -1;
		int UnityEngineCameraClearFlags_EnumRef = -1;
        
        public void PushUnityEngineCameraClearFlags(RealStatePtr L, UnityEngine.CameraClearFlags val)
        {
            if (UnityEngineCameraClearFlags_TypeID == -1)
            {
			    bool is_first;
                UnityEngineCameraClearFlags_TypeID = getTypeId(L, typeof(UnityEngine.CameraClearFlags), out is_first);
				
				if (UnityEngineCameraClearFlags_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.CameraClearFlags));
				    UnityEngineCameraClearFlags_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineCameraClearFlags_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineCameraClearFlags_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.CameraClearFlags ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineCameraClearFlags_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.CameraClearFlags val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineCameraClearFlags_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.CameraClearFlags");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.CameraClearFlags");
                }
				val = (UnityEngine.CameraClearFlags)e;
                
            }
            else
            {
                val = (UnityEngine.CameraClearFlags)objectCasters.GetCaster(typeof(UnityEngine.CameraClearFlags))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineCameraClearFlags(RealStatePtr L, int index, UnityEngine.CameraClearFlags val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineCameraClearFlags_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.CameraClearFlags");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.CameraClearFlags ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineNetworkReachability_TypeID = -1;
		int UnityEngineNetworkReachability_EnumRef = -1;
        
        public void PushUnityEngineNetworkReachability(RealStatePtr L, UnityEngine.NetworkReachability val)
        {
            if (UnityEngineNetworkReachability_TypeID == -1)
            {
			    bool is_first;
                UnityEngineNetworkReachability_TypeID = getTypeId(L, typeof(UnityEngine.NetworkReachability), out is_first);
				
				if (UnityEngineNetworkReachability_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.NetworkReachability));
				    UnityEngineNetworkReachability_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineNetworkReachability_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineNetworkReachability_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.NetworkReachability ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineNetworkReachability_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.NetworkReachability val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineNetworkReachability_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.NetworkReachability");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.NetworkReachability");
                }
				val = (UnityEngine.NetworkReachability)e;
                
            }
            else
            {
                val = (UnityEngine.NetworkReachability)objectCasters.GetCaster(typeof(UnityEngine.NetworkReachability))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineNetworkReachability(RealStatePtr L, int index, UnityEngine.NetworkReachability val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineNetworkReachability_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.NetworkReachability");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.NetworkReachability ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRuntimePlatform_TypeID = -1;
		int UnityEngineRuntimePlatform_EnumRef = -1;
        
        public void PushUnityEngineRuntimePlatform(RealStatePtr L, UnityEngine.RuntimePlatform val)
        {
            if (UnityEngineRuntimePlatform_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRuntimePlatform_TypeID = getTypeId(L, typeof(UnityEngine.RuntimePlatform), out is_first);
				
				if (UnityEngineRuntimePlatform_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.RuntimePlatform));
				    UnityEngineRuntimePlatform_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineRuntimePlatform_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineRuntimePlatform_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.RuntimePlatform ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineRuntimePlatform_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.RuntimePlatform val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRuntimePlatform_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RuntimePlatform");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.RuntimePlatform");
                }
				val = (UnityEngine.RuntimePlatform)e;
                
            }
            else
            {
                val = (UnityEngine.RuntimePlatform)objectCasters.GetCaster(typeof(UnityEngine.RuntimePlatform))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRuntimePlatform(RealStatePtr L, int index, UnityEngine.RuntimePlatform val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRuntimePlatform_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RuntimePlatform");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.RuntimePlatform ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineSystemLanguage_TypeID = -1;
		int UnityEngineSystemLanguage_EnumRef = -1;
        
        public void PushUnityEngineSystemLanguage(RealStatePtr L, UnityEngine.SystemLanguage val)
        {
            if (UnityEngineSystemLanguage_TypeID == -1)
            {
			    bool is_first;
                UnityEngineSystemLanguage_TypeID = getTypeId(L, typeof(UnityEngine.SystemLanguage), out is_first);
				
				if (UnityEngineSystemLanguage_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.SystemLanguage));
				    UnityEngineSystemLanguage_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineSystemLanguage_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineSystemLanguage_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.SystemLanguage ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineSystemLanguage_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.SystemLanguage val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineSystemLanguage_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.SystemLanguage");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.SystemLanguage");
                }
				val = (UnityEngine.SystemLanguage)e;
                
            }
            else
            {
                val = (UnityEngine.SystemLanguage)objectCasters.GetCaster(typeof(UnityEngine.SystemLanguage))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineSystemLanguage(RealStatePtr L, int index, UnityEngine.SystemLanguage val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineSystemLanguage_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.SystemLanguage");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.SystemLanguage ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineThreadPriority_TypeID = -1;
		int UnityEngineThreadPriority_EnumRef = -1;
        
        public void PushUnityEngineThreadPriority(RealStatePtr L, UnityEngine.ThreadPriority val)
        {
            if (UnityEngineThreadPriority_TypeID == -1)
            {
			    bool is_first;
                UnityEngineThreadPriority_TypeID = getTypeId(L, typeof(UnityEngine.ThreadPriority), out is_first);
				
				if (UnityEngineThreadPriority_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.ThreadPriority));
				    UnityEngineThreadPriority_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineThreadPriority_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineThreadPriority_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.ThreadPriority ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineThreadPriority_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.ThreadPriority val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineThreadPriority_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.ThreadPriority");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.ThreadPriority");
                }
				val = (UnityEngine.ThreadPriority)e;
                
            }
            else
            {
                val = (UnityEngine.ThreadPriority)objectCasters.GetCaster(typeof(UnityEngine.ThreadPriority))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineThreadPriority(RealStatePtr L, int index, UnityEngine.ThreadPriority val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineThreadPriority_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.ThreadPriority");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.ThreadPriority ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int FairyGUIEaseType_TypeID = -1;
		int FairyGUIEaseType_EnumRef = -1;
        
        public void PushFairyGUIEaseType(RealStatePtr L, FairyGUI.EaseType val)
        {
            if (FairyGUIEaseType_TypeID == -1)
            {
			    bool is_first;
                FairyGUIEaseType_TypeID = getTypeId(L, typeof(FairyGUI.EaseType), out is_first);
				
				if (FairyGUIEaseType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(FairyGUI.EaseType));
				    FairyGUIEaseType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, FairyGUIEaseType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, FairyGUIEaseType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for FairyGUI.EaseType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, FairyGUIEaseType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out FairyGUI.EaseType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIEaseType_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.EaseType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for FairyGUI.EaseType");
                }
				val = (FairyGUI.EaseType)e;
                
            }
            else
            {
                val = (FairyGUI.EaseType)objectCasters.GetCaster(typeof(FairyGUI.EaseType))(L, index, null);
            }
        }
		
        public void UpdateFairyGUIEaseType(RealStatePtr L, int index, FairyGUI.EaseType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIEaseType_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.EaseType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for FairyGUI.EaseType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int FairyGUIRelationType_TypeID = -1;
		int FairyGUIRelationType_EnumRef = -1;
        
        public void PushFairyGUIRelationType(RealStatePtr L, FairyGUI.RelationType val)
        {
            if (FairyGUIRelationType_TypeID == -1)
            {
			    bool is_first;
                FairyGUIRelationType_TypeID = getTypeId(L, typeof(FairyGUI.RelationType), out is_first);
				
				if (FairyGUIRelationType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(FairyGUI.RelationType));
				    FairyGUIRelationType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, FairyGUIRelationType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, FairyGUIRelationType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for FairyGUI.RelationType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, FairyGUIRelationType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out FairyGUI.RelationType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIRelationType_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.RelationType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for FairyGUI.RelationType");
                }
				val = (FairyGUI.RelationType)e;
                
            }
            else
            {
                val = (FairyGUI.RelationType)objectCasters.GetCaster(typeof(FairyGUI.RelationType))(L, index, null);
            }
        }
		
        public void UpdateFairyGUIRelationType(RealStatePtr L, int index, FairyGUI.RelationType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIRelationType_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.RelationType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for FairyGUI.RelationType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int FairyGUITweenPropType_TypeID = -1;
		int FairyGUITweenPropType_EnumRef = -1;
        
        public void PushFairyGUITweenPropType(RealStatePtr L, FairyGUI.TweenPropType val)
        {
            if (FairyGUITweenPropType_TypeID == -1)
            {
			    bool is_first;
                FairyGUITweenPropType_TypeID = getTypeId(L, typeof(FairyGUI.TweenPropType), out is_first);
				
				if (FairyGUITweenPropType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(FairyGUI.TweenPropType));
				    FairyGUITweenPropType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, FairyGUITweenPropType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, FairyGUITweenPropType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for FairyGUI.TweenPropType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, FairyGUITweenPropType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out FairyGUI.TweenPropType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUITweenPropType_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.TweenPropType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for FairyGUI.TweenPropType");
                }
				val = (FairyGUI.TweenPropType)e;
                
            }
            else
            {
                val = (FairyGUI.TweenPropType)objectCasters.GetCaster(typeof(FairyGUI.TweenPropType))(L, index, null);
            }
        }
		
        public void UpdateFairyGUITweenPropType(RealStatePtr L, int index, FairyGUI.TweenPropType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUITweenPropType_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.TweenPropType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for FairyGUI.TweenPropType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int _eAsyncAssetLoadType_TypeID = -1;
		int _eAsyncAssetLoadType_EnumRef = -1;
        
        public void Push_eAsyncAssetLoadType(RealStatePtr L, _eAsyncAssetLoadType val)
        {
            if (_eAsyncAssetLoadType_TypeID == -1)
            {
			    bool is_first;
                _eAsyncAssetLoadType_TypeID = getTypeId(L, typeof(_eAsyncAssetLoadType), out is_first);
				
				if (_eAsyncAssetLoadType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(_eAsyncAssetLoadType));
				    _eAsyncAssetLoadType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, _eAsyncAssetLoadType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, _eAsyncAssetLoadType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for _eAsyncAssetLoadType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, _eAsyncAssetLoadType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out _eAsyncAssetLoadType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != _eAsyncAssetLoadType_TypeID)
				{
				    throw new Exception("invalid userdata for _eAsyncAssetLoadType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for _eAsyncAssetLoadType");
                }
				val = (_eAsyncAssetLoadType)e;
                
            }
            else
            {
                val = (_eAsyncAssetLoadType)objectCasters.GetCaster(typeof(_eAsyncAssetLoadType))(L, index, null);
            }
        }
		
        public void Update_eAsyncAssetLoadType(RealStatePtr L, int index, _eAsyncAssetLoadType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != _eAsyncAssetLoadType_TypeID)
				{
				    throw new Exception("invalid userdata for _eAsyncAssetLoadType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for _eAsyncAssetLoadType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int Casinos_eChatItemType_TypeID = -1;
		int Casinos_eChatItemType_EnumRef = -1;
        
        public void PushCasinos_eChatItemType(RealStatePtr L, Casinos._eChatItemType val)
        {
            if (Casinos_eChatItemType_TypeID == -1)
            {
			    bool is_first;
                Casinos_eChatItemType_TypeID = getTypeId(L, typeof(Casinos._eChatItemType), out is_first);
				
				if (Casinos_eChatItemType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Casinos._eChatItemType));
				    Casinos_eChatItemType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, Casinos_eChatItemType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, Casinos_eChatItemType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Casinos._eChatItemType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, Casinos_eChatItemType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Casinos._eChatItemType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != Casinos_eChatItemType_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos._eChatItemType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Casinos._eChatItemType");
                }
				val = (Casinos._eChatItemType)e;
                
            }
            else
            {
                val = (Casinos._eChatItemType)objectCasters.GetCaster(typeof(Casinos._eChatItemType))(L, index, null);
            }
        }
		
        public void UpdateCasinos_eChatItemType(RealStatePtr L, int index, Casinos._eChatItemType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != Casinos_eChatItemType_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos._eChatItemType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Casinos._eChatItemType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int Casinos_eLoginType_TypeID = -1;
		int Casinos_eLoginType_EnumRef = -1;
        
        public void PushCasinos_eLoginType(RealStatePtr L, Casinos._eLoginType val)
        {
            if (Casinos_eLoginType_TypeID == -1)
            {
			    bool is_first;
                Casinos_eLoginType_TypeID = getTypeId(L, typeof(Casinos._eLoginType), out is_first);
				
				if (Casinos_eLoginType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Casinos._eLoginType));
				    Casinos_eLoginType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, Casinos_eLoginType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, Casinos_eLoginType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Casinos._eLoginType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, Casinos_eLoginType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Casinos._eLoginType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != Casinos_eLoginType_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos._eLoginType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Casinos._eLoginType");
                }
				val = (Casinos._eLoginType)e;
                
            }
            else
            {
                val = (Casinos._eLoginType)objectCasters.GetCaster(typeof(Casinos._eLoginType))(L, index, null);
            }
        }
		
        public void UpdateCasinos_eLoginType(RealStatePtr L, int index, Casinos._eLoginType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != Casinos_eLoginType_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos._eLoginType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Casinos._eLoginType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int _ePayType_TypeID = -1;
		int _ePayType_EnumRef = -1;
        
        public void Push_ePayType(RealStatePtr L, _ePayType val)
        {
            if (_ePayType_TypeID == -1)
            {
			    bool is_first;
                _ePayType_TypeID = getTypeId(L, typeof(_ePayType), out is_first);
				
				if (_ePayType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(_ePayType));
				    _ePayType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, _ePayType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, _ePayType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for _ePayType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, _ePayType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out _ePayType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != _ePayType_TypeID)
				{
				    throw new Exception("invalid userdata for _ePayType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for _ePayType");
                }
				val = (_ePayType)e;
                
            }
            else
            {
                val = (_ePayType)objectCasters.GetCaster(typeof(_ePayType))(L, index, null);
            }
        }
		
        public void Update_ePayType(RealStatePtr L, int index, _ePayType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != _ePayType_TypeID)
				{
				    throw new Exception("invalid userdata for _ePayType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for _ePayType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int Casinos_eProjectItemDisplayNameKey_TypeID = -1;
		int Casinos_eProjectItemDisplayNameKey_EnumRef = -1;
        
        public void PushCasinos_eProjectItemDisplayNameKey(RealStatePtr L, Casinos._eProjectItemDisplayNameKey val)
        {
            if (Casinos_eProjectItemDisplayNameKey_TypeID == -1)
            {
			    bool is_first;
                Casinos_eProjectItemDisplayNameKey_TypeID = getTypeId(L, typeof(Casinos._eProjectItemDisplayNameKey), out is_first);
				
				if (Casinos_eProjectItemDisplayNameKey_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Casinos._eProjectItemDisplayNameKey));
				    Casinos_eProjectItemDisplayNameKey_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, Casinos_eProjectItemDisplayNameKey_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, Casinos_eProjectItemDisplayNameKey_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Casinos._eProjectItemDisplayNameKey ,value="+val);
            }
			
			LuaAPI.lua_getref(L, Casinos_eProjectItemDisplayNameKey_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Casinos._eProjectItemDisplayNameKey val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != Casinos_eProjectItemDisplayNameKey_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos._eProjectItemDisplayNameKey");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Casinos._eProjectItemDisplayNameKey");
                }
				val = (Casinos._eProjectItemDisplayNameKey)e;
                
            }
            else
            {
                val = (Casinos._eProjectItemDisplayNameKey)objectCasters.GetCaster(typeof(Casinos._eProjectItemDisplayNameKey))(L, index, null);
            }
        }
		
        public void UpdateCasinos_eProjectItemDisplayNameKey(RealStatePtr L, int index, Casinos._eProjectItemDisplayNameKey val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != Casinos_eProjectItemDisplayNameKey_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos._eProjectItemDisplayNameKey");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Casinos._eProjectItemDisplayNameKey ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int ZXingBarcodeFormat_TypeID = -1;
		int ZXingBarcodeFormat_EnumRef = -1;
        
        public void PushZXingBarcodeFormat(RealStatePtr L, ZXing.BarcodeFormat val)
        {
            if (ZXingBarcodeFormat_TypeID == -1)
            {
			    bool is_first;
                ZXingBarcodeFormat_TypeID = getTypeId(L, typeof(ZXing.BarcodeFormat), out is_first);
				
				if (ZXingBarcodeFormat_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(ZXing.BarcodeFormat));
				    ZXingBarcodeFormat_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, ZXingBarcodeFormat_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, ZXingBarcodeFormat_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for ZXing.BarcodeFormat ,value="+val);
            }
			
			LuaAPI.lua_getref(L, ZXingBarcodeFormat_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out ZXing.BarcodeFormat val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != ZXingBarcodeFormat_TypeID)
				{
				    throw new Exception("invalid userdata for ZXing.BarcodeFormat");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for ZXing.BarcodeFormat");
                }
				val = (ZXing.BarcodeFormat)e;
                
            }
            else
            {
                val = (ZXing.BarcodeFormat)objectCasters.GetCaster(typeof(ZXing.BarcodeFormat))(L, index, null);
            }
        }
		
        public void UpdateZXingBarcodeFormat(RealStatePtr L, int index, ZXing.BarcodeFormat val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != ZXingBarcodeFormat_TypeID)
				{
				    throw new Exception("invalid userdata for ZXing.BarcodeFormat");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for ZXing.BarcodeFormat ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int Casinos_eEditorRunSourcePlatform_TypeID = -1;
		int Casinos_eEditorRunSourcePlatform_EnumRef = -1;
        
        public void PushCasinos_eEditorRunSourcePlatform(RealStatePtr L, Casinos._eEditorRunSourcePlatform val)
        {
            if (Casinos_eEditorRunSourcePlatform_TypeID == -1)
            {
			    bool is_first;
                Casinos_eEditorRunSourcePlatform_TypeID = getTypeId(L, typeof(Casinos._eEditorRunSourcePlatform), out is_first);
				
				if (Casinos_eEditorRunSourcePlatform_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Casinos._eEditorRunSourcePlatform));
				    Casinos_eEditorRunSourcePlatform_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, Casinos_eEditorRunSourcePlatform_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, Casinos_eEditorRunSourcePlatform_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Casinos._eEditorRunSourcePlatform ,value="+val);
            }
			
			LuaAPI.lua_getref(L, Casinos_eEditorRunSourcePlatform_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Casinos._eEditorRunSourcePlatform val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != Casinos_eEditorRunSourcePlatform_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos._eEditorRunSourcePlatform");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Casinos._eEditorRunSourcePlatform");
                }
				val = (Casinos._eEditorRunSourcePlatform)e;
                
            }
            else
            {
                val = (Casinos._eEditorRunSourcePlatform)objectCasters.GetCaster(typeof(Casinos._eEditorRunSourcePlatform))(L, index, null);
            }
        }
		
        public void UpdateCasinos_eEditorRunSourcePlatform(RealStatePtr L, int index, Casinos._eEditorRunSourcePlatform val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != Casinos_eEditorRunSourcePlatform_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos._eEditorRunSourcePlatform");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Casinos._eEditorRunSourcePlatform ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int CasinosDirType_TypeID = -1;
		int CasinosDirType_EnumRef = -1;
        
        public void PushCasinosDirType(RealStatePtr L, Casinos.DirType val)
        {
            if (CasinosDirType_TypeID == -1)
            {
			    bool is_first;
                CasinosDirType_TypeID = getTypeId(L, typeof(Casinos.DirType), out is_first);
				
				if (CasinosDirType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Casinos.DirType));
				    CasinosDirType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, CasinosDirType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, CasinosDirType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Casinos.DirType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, CasinosDirType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Casinos.DirType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CasinosDirType_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos.DirType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Casinos.DirType");
                }
				val = (Casinos.DirType)e;
                
            }
            else
            {
                val = (Casinos.DirType)objectCasters.GetCaster(typeof(Casinos.DirType))(L, index, null);
            }
        }
		
        public void UpdateCasinosDirType(RealStatePtr L, int index, Casinos.DirType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != CasinosDirType_TypeID)
				{
				    throw new Exception("invalid userdata for Casinos.DirType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Casinos.DirType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        
		// table cast optimze
		
        
    }
	
	public partial class StaticLuaCallbacks
    {
	    internal static bool __tryArrayGet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int index)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.PushUnityEngineVector2(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.PushUnityEngineVector3(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.PushUnityEngineVector4(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.PushUnityEngineColor(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.PushUnityEngineQuaternion(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.PushUnityEngineRay(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.PushUnityEngineBounds(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.PushUnityEngineRay2D(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.ApplicationInstallMode[]))
			{
			    UnityEngine.ApplicationInstallMode[] array = obj as UnityEngine.ApplicationInstallMode[];
				translator.PushUnityEngineApplicationInstallMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.ApplicationSandboxType[]))
			{
			    UnityEngine.ApplicationSandboxType[] array = obj as UnityEngine.ApplicationSandboxType[];
				translator.PushUnityEngineApplicationSandboxType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.CameraType[]))
			{
			    UnityEngine.CameraType[] array = obj as UnityEngine.CameraType[];
				translator.PushUnityEngineCameraType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.CameraClearFlags[]))
			{
			    UnityEngine.CameraClearFlags[] array = obj as UnityEngine.CameraClearFlags[];
				translator.PushUnityEngineCameraClearFlags(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.NetworkReachability[]))
			{
			    UnityEngine.NetworkReachability[] array = obj as UnityEngine.NetworkReachability[];
				translator.PushUnityEngineNetworkReachability(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.RuntimePlatform[]))
			{
			    UnityEngine.RuntimePlatform[] array = obj as UnityEngine.RuntimePlatform[];
				translator.PushUnityEngineRuntimePlatform(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.SystemLanguage[]))
			{
			    UnityEngine.SystemLanguage[] array = obj as UnityEngine.SystemLanguage[];
				translator.PushUnityEngineSystemLanguage(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.ThreadPriority[]))
			{
			    UnityEngine.ThreadPriority[] array = obj as UnityEngine.ThreadPriority[];
				translator.PushUnityEngineThreadPriority(L, array[index]);
				return true;
			}
			else if (type == typeof(FairyGUI.EaseType[]))
			{
			    FairyGUI.EaseType[] array = obj as FairyGUI.EaseType[];
				translator.PushFairyGUIEaseType(L, array[index]);
				return true;
			}
			else if (type == typeof(FairyGUI.RelationType[]))
			{
			    FairyGUI.RelationType[] array = obj as FairyGUI.RelationType[];
				translator.PushFairyGUIRelationType(L, array[index]);
				return true;
			}
			else if (type == typeof(FairyGUI.TweenPropType[]))
			{
			    FairyGUI.TweenPropType[] array = obj as FairyGUI.TweenPropType[];
				translator.PushFairyGUITweenPropType(L, array[index]);
				return true;
			}
			else if (type == typeof(_eAsyncAssetLoadType[]))
			{
			    _eAsyncAssetLoadType[] array = obj as _eAsyncAssetLoadType[];
				translator.Push_eAsyncAssetLoadType(L, array[index]);
				return true;
			}
			else if (type == typeof(Casinos._eChatItemType[]))
			{
			    Casinos._eChatItemType[] array = obj as Casinos._eChatItemType[];
				translator.PushCasinos_eChatItemType(L, array[index]);
				return true;
			}
			else if (type == typeof(Casinos._eLoginType[]))
			{
			    Casinos._eLoginType[] array = obj as Casinos._eLoginType[];
				translator.PushCasinos_eLoginType(L, array[index]);
				return true;
			}
			else if (type == typeof(_ePayType[]))
			{
			    _ePayType[] array = obj as _ePayType[];
				translator.Push_ePayType(L, array[index]);
				return true;
			}
			else if (type == typeof(Casinos._eProjectItemDisplayNameKey[]))
			{
			    Casinos._eProjectItemDisplayNameKey[] array = obj as Casinos._eProjectItemDisplayNameKey[];
				translator.PushCasinos_eProjectItemDisplayNameKey(L, array[index]);
				return true;
			}
			else if (type == typeof(ZXing.BarcodeFormat[]))
			{
			    ZXing.BarcodeFormat[] array = obj as ZXing.BarcodeFormat[];
				translator.PushZXingBarcodeFormat(L, array[index]);
				return true;
			}
			else if (type == typeof(Casinos._eEditorRunSourcePlatform[]))
			{
			    Casinos._eEditorRunSourcePlatform[] array = obj as Casinos._eEditorRunSourcePlatform[];
				translator.PushCasinos_eEditorRunSourcePlatform(L, array[index]);
				return true;
			}
			else if (type == typeof(Casinos.DirType[]))
			{
			    Casinos.DirType[] array = obj as Casinos.DirType[];
				translator.PushCasinosDirType(L, array[index]);
				return true;
			}
            return false;
		}
		
		internal static bool __tryArraySet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int array_idx, int obj_idx)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.ApplicationInstallMode[]))
			{
			    UnityEngine.ApplicationInstallMode[] array = obj as UnityEngine.ApplicationInstallMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.ApplicationSandboxType[]))
			{
			    UnityEngine.ApplicationSandboxType[] array = obj as UnityEngine.ApplicationSandboxType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.CameraType[]))
			{
			    UnityEngine.CameraType[] array = obj as UnityEngine.CameraType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.CameraClearFlags[]))
			{
			    UnityEngine.CameraClearFlags[] array = obj as UnityEngine.CameraClearFlags[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.NetworkReachability[]))
			{
			    UnityEngine.NetworkReachability[] array = obj as UnityEngine.NetworkReachability[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.RuntimePlatform[]))
			{
			    UnityEngine.RuntimePlatform[] array = obj as UnityEngine.RuntimePlatform[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.SystemLanguage[]))
			{
			    UnityEngine.SystemLanguage[] array = obj as UnityEngine.SystemLanguage[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.ThreadPriority[]))
			{
			    UnityEngine.ThreadPriority[] array = obj as UnityEngine.ThreadPriority[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(FairyGUI.EaseType[]))
			{
			    FairyGUI.EaseType[] array = obj as FairyGUI.EaseType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(FairyGUI.RelationType[]))
			{
			    FairyGUI.RelationType[] array = obj as FairyGUI.RelationType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(FairyGUI.TweenPropType[]))
			{
			    FairyGUI.TweenPropType[] array = obj as FairyGUI.TweenPropType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(_eAsyncAssetLoadType[]))
			{
			    _eAsyncAssetLoadType[] array = obj as _eAsyncAssetLoadType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Casinos._eChatItemType[]))
			{
			    Casinos._eChatItemType[] array = obj as Casinos._eChatItemType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Casinos._eLoginType[]))
			{
			    Casinos._eLoginType[] array = obj as Casinos._eLoginType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(_ePayType[]))
			{
			    _ePayType[] array = obj as _ePayType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Casinos._eProjectItemDisplayNameKey[]))
			{
			    Casinos._eProjectItemDisplayNameKey[] array = obj as Casinos._eProjectItemDisplayNameKey[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(ZXing.BarcodeFormat[]))
			{
			    ZXing.BarcodeFormat[] array = obj as ZXing.BarcodeFormat[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Casinos._eEditorRunSourcePlatform[]))
			{
			    Casinos._eEditorRunSourcePlatform[] array = obj as Casinos._eEditorRunSourcePlatform[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Casinos.DirType[]))
			{
			    Casinos.DirType[] array = obj as Casinos.DirType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
            return false;
		}
	}
}