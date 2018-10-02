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
    
    public class UnityEngineApplicationInstallModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.ApplicationInstallMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.ApplicationInstallMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.ApplicationInstallMode), L, null, 7, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Unknown", UnityEngine.ApplicationInstallMode.Unknown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Store", UnityEngine.ApplicationInstallMode.Store);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DeveloperBuild", UnityEngine.ApplicationInstallMode.DeveloperBuild);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Adhoc", UnityEngine.ApplicationInstallMode.Adhoc);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Enterprise", UnityEngine.ApplicationInstallMode.Enterprise);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Editor", UnityEngine.ApplicationInstallMode.Editor);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.ApplicationInstallMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineApplicationInstallMode(L, (UnityEngine.ApplicationInstallMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Unknown"))
                {
                    translator.PushUnityEngineApplicationInstallMode(L, UnityEngine.ApplicationInstallMode.Unknown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Store"))
                {
                    translator.PushUnityEngineApplicationInstallMode(L, UnityEngine.ApplicationInstallMode.Store);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DeveloperBuild"))
                {
                    translator.PushUnityEngineApplicationInstallMode(L, UnityEngine.ApplicationInstallMode.DeveloperBuild);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Adhoc"))
                {
                    translator.PushUnityEngineApplicationInstallMode(L, UnityEngine.ApplicationInstallMode.Adhoc);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Enterprise"))
                {
                    translator.PushUnityEngineApplicationInstallMode(L, UnityEngine.ApplicationInstallMode.Enterprise);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Editor"))
                {
                    translator.PushUnityEngineApplicationInstallMode(L, UnityEngine.ApplicationInstallMode.Editor);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.ApplicationInstallMode!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.ApplicationInstallMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineApplicationSandboxTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.ApplicationSandboxType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.ApplicationSandboxType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.ApplicationSandboxType), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Unknown", UnityEngine.ApplicationSandboxType.Unknown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NotSandboxed", UnityEngine.ApplicationSandboxType.NotSandboxed);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Sandboxed", UnityEngine.ApplicationSandboxType.Sandboxed);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SandboxBroken", UnityEngine.ApplicationSandboxType.SandboxBroken);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.ApplicationSandboxType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineApplicationSandboxType(L, (UnityEngine.ApplicationSandboxType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Unknown"))
                {
                    translator.PushUnityEngineApplicationSandboxType(L, UnityEngine.ApplicationSandboxType.Unknown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NotSandboxed"))
                {
                    translator.PushUnityEngineApplicationSandboxType(L, UnityEngine.ApplicationSandboxType.NotSandboxed);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Sandboxed"))
                {
                    translator.PushUnityEngineApplicationSandboxType(L, UnityEngine.ApplicationSandboxType.Sandboxed);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SandboxBroken"))
                {
                    translator.PushUnityEngineApplicationSandboxType(L, UnityEngine.ApplicationSandboxType.SandboxBroken);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.ApplicationSandboxType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.ApplicationSandboxType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineNetworkReachabilityWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.NetworkReachability), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.NetworkReachability), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.NetworkReachability), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NotReachable", UnityEngine.NetworkReachability.NotReachable);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ReachableViaCarrierDataNetwork", UnityEngine.NetworkReachability.ReachableViaCarrierDataNetwork);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ReachableViaLocalAreaNetwork", UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.NetworkReachability), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineNetworkReachability(L, (UnityEngine.NetworkReachability)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "NotReachable"))
                {
                    translator.PushUnityEngineNetworkReachability(L, UnityEngine.NetworkReachability.NotReachable);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ReachableViaCarrierDataNetwork"))
                {
                    translator.PushUnityEngineNetworkReachability(L, UnityEngine.NetworkReachability.ReachableViaCarrierDataNetwork);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ReachableViaLocalAreaNetwork"))
                {
                    translator.PushUnityEngineNetworkReachability(L, UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.NetworkReachability!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.NetworkReachability! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineRuntimePlatformWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.RuntimePlatform), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.RuntimePlatform), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.RuntimePlatform), L, null, 35, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OSXEditor", UnityEngine.RuntimePlatform.OSXEditor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OSXPlayer", UnityEngine.RuntimePlatform.OSXPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WindowsPlayer", UnityEngine.RuntimePlatform.WindowsPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WindowsEditor", UnityEngine.RuntimePlatform.WindowsEditor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IPhonePlayer", UnityEngine.RuntimePlatform.IPhonePlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Android", UnityEngine.RuntimePlatform.Android);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinuxPlayer", UnityEngine.RuntimePlatform.LinuxPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinuxEditor", UnityEngine.RuntimePlatform.LinuxEditor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WebGLPlayer", UnityEngine.RuntimePlatform.WebGLPlayer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WSAPlayerX86", UnityEngine.RuntimePlatform.WSAPlayerX86);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WSAPlayerX64", UnityEngine.RuntimePlatform.WSAPlayerX64);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WSAPlayerARM", UnityEngine.RuntimePlatform.WSAPlayerARM);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PSP2", UnityEngine.RuntimePlatform.PSP2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PS4", UnityEngine.RuntimePlatform.PS4);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "XboxOne", UnityEngine.RuntimePlatform.XboxOne);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "tvOS", UnityEngine.RuntimePlatform.tvOS);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Switch", UnityEngine.RuntimePlatform.Switch);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.RuntimePlatform), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineRuntimePlatform(L, (UnityEngine.RuntimePlatform)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "OSXEditor"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.OSXEditor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OSXPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.OSXPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WindowsPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WindowsPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WindowsEditor"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WindowsEditor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "IPhonePlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.IPhonePlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Android"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.Android);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinuxPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.LinuxPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinuxEditor"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.LinuxEditor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WebGLPlayer"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WebGLPlayer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WSAPlayerX86"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WSAPlayerX86);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WSAPlayerX64"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WSAPlayerX64);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WSAPlayerARM"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.WSAPlayerARM);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PSP2"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.PSP2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PS4"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.PS4);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "XboxOne"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.XboxOne);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "tvOS"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.tvOS);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Switch"))
                {
                    translator.PushUnityEngineRuntimePlatform(L, UnityEngine.RuntimePlatform.Switch);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.RuntimePlatform!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.RuntimePlatform! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineSystemLanguageWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.SystemLanguage), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.SystemLanguage), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.SystemLanguage), L, null, 45, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Afrikaans", UnityEngine.SystemLanguage.Afrikaans);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Arabic", UnityEngine.SystemLanguage.Arabic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Basque", UnityEngine.SystemLanguage.Basque);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Belarusian", UnityEngine.SystemLanguage.Belarusian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bulgarian", UnityEngine.SystemLanguage.Bulgarian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Catalan", UnityEngine.SystemLanguage.Catalan);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Chinese", UnityEngine.SystemLanguage.Chinese);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Czech", UnityEngine.SystemLanguage.Czech);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Danish", UnityEngine.SystemLanguage.Danish);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Dutch", UnityEngine.SystemLanguage.Dutch);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "English", UnityEngine.SystemLanguage.English);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Estonian", UnityEngine.SystemLanguage.Estonian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Faroese", UnityEngine.SystemLanguage.Faroese);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Finnish", UnityEngine.SystemLanguage.Finnish);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "French", UnityEngine.SystemLanguage.French);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "German", UnityEngine.SystemLanguage.German);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Greek", UnityEngine.SystemLanguage.Greek);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Hebrew", UnityEngine.SystemLanguage.Hebrew);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Icelandic", UnityEngine.SystemLanguage.Icelandic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Indonesian", UnityEngine.SystemLanguage.Indonesian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Italian", UnityEngine.SystemLanguage.Italian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Japanese", UnityEngine.SystemLanguage.Japanese);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Korean", UnityEngine.SystemLanguage.Korean);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Latvian", UnityEngine.SystemLanguage.Latvian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Lithuanian", UnityEngine.SystemLanguage.Lithuanian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Norwegian", UnityEngine.SystemLanguage.Norwegian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Polish", UnityEngine.SystemLanguage.Polish);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Portuguese", UnityEngine.SystemLanguage.Portuguese);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Romanian", UnityEngine.SystemLanguage.Romanian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Russian", UnityEngine.SystemLanguage.Russian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SerboCroatian", UnityEngine.SystemLanguage.SerboCroatian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Slovak", UnityEngine.SystemLanguage.Slovak);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Slovenian", UnityEngine.SystemLanguage.Slovenian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Spanish", UnityEngine.SystemLanguage.Spanish);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Swedish", UnityEngine.SystemLanguage.Swedish);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Thai", UnityEngine.SystemLanguage.Thai);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Turkish", UnityEngine.SystemLanguage.Turkish);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Ukrainian", UnityEngine.SystemLanguage.Ukrainian);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Vietnamese", UnityEngine.SystemLanguage.Vietnamese);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ChineseSimplified", UnityEngine.SystemLanguage.ChineseSimplified);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ChineseTraditional", UnityEngine.SystemLanguage.ChineseTraditional);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Unknown", UnityEngine.SystemLanguage.Unknown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Hungarian", UnityEngine.SystemLanguage.Hungarian);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.SystemLanguage), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineSystemLanguage(L, (UnityEngine.SystemLanguage)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Afrikaans"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Afrikaans);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Arabic"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Arabic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Basque"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Basque);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Belarusian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Belarusian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Bulgarian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Bulgarian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Catalan"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Catalan);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Chinese"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Chinese);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Czech"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Czech);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Danish"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Danish);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Dutch"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Dutch);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "English"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.English);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Estonian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Estonian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Faroese"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Faroese);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Finnish"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Finnish);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "French"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.French);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "German"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.German);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Greek"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Greek);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Hebrew"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Hebrew);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Icelandic"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Icelandic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Indonesian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Indonesian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Italian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Italian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Japanese"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Japanese);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Korean"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Korean);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Latvian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Latvian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Lithuanian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Lithuanian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Norwegian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Norwegian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Polish"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Polish);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Portuguese"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Portuguese);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Romanian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Romanian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Russian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Russian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SerboCroatian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.SerboCroatian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Slovak"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Slovak);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Slovenian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Slovenian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Spanish"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Spanish);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Swedish"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Swedish);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Thai"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Thai);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Turkish"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Turkish);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Ukrainian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Ukrainian);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Vietnamese"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Vietnamese);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ChineseSimplified"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.ChineseSimplified);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ChineseTraditional"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.ChineseTraditional);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Unknown"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Unknown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Hungarian"))
                {
                    translator.PushUnityEngineSystemLanguage(L, UnityEngine.SystemLanguage.Hungarian);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.SystemLanguage!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.SystemLanguage! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineThreadPriorityWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.ThreadPriority), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.ThreadPriority), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.ThreadPriority), L, null, 5, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Low", UnityEngine.ThreadPriority.Low);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BelowNormal", UnityEngine.ThreadPriority.BelowNormal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", UnityEngine.ThreadPriority.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "High", UnityEngine.ThreadPriority.High);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.ThreadPriority), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineThreadPriority(L, (UnityEngine.ThreadPriority)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Low"))
                {
                    translator.PushUnityEngineThreadPriority(L, UnityEngine.ThreadPriority.Low);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BelowNormal"))
                {
                    translator.PushUnityEngineThreadPriority(L, UnityEngine.ThreadPriority.BelowNormal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushUnityEngineThreadPriority(L, UnityEngine.ThreadPriority.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "High"))
                {
                    translator.PushUnityEngineThreadPriority(L, UnityEngine.ThreadPriority.High);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.ThreadPriority!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.ThreadPriority! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class FairyGUIEaseTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(FairyGUI.EaseType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(FairyGUI.EaseType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(FairyGUI.EaseType), L, null, 33, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Linear", FairyGUI.EaseType.Linear);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SineIn", FairyGUI.EaseType.SineIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SineOut", FairyGUI.EaseType.SineOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SineInOut", FairyGUI.EaseType.SineInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuadIn", FairyGUI.EaseType.QuadIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuadOut", FairyGUI.EaseType.QuadOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuadInOut", FairyGUI.EaseType.QuadInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CubicIn", FairyGUI.EaseType.CubicIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CubicOut", FairyGUI.EaseType.CubicOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CubicInOut", FairyGUI.EaseType.CubicInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuartIn", FairyGUI.EaseType.QuartIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuartOut", FairyGUI.EaseType.QuartOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuartInOut", FairyGUI.EaseType.QuartInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuintIn", FairyGUI.EaseType.QuintIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuintOut", FairyGUI.EaseType.QuintOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "QuintInOut", FairyGUI.EaseType.QuintInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ExpoIn", FairyGUI.EaseType.ExpoIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ExpoOut", FairyGUI.EaseType.ExpoOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ExpoInOut", FairyGUI.EaseType.ExpoInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CircIn", FairyGUI.EaseType.CircIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CircOut", FairyGUI.EaseType.CircOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CircInOut", FairyGUI.EaseType.CircInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ElasticIn", FairyGUI.EaseType.ElasticIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ElasticOut", FairyGUI.EaseType.ElasticOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ElasticInOut", FairyGUI.EaseType.ElasticInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BackIn", FairyGUI.EaseType.BackIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BackOut", FairyGUI.EaseType.BackOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BackInOut", FairyGUI.EaseType.BackInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BounceIn", FairyGUI.EaseType.BounceIn);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BounceOut", FairyGUI.EaseType.BounceOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BounceInOut", FairyGUI.EaseType.BounceInOut);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Custom", FairyGUI.EaseType.Custom);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(FairyGUI.EaseType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushFairyGUIEaseType(L, (FairyGUI.EaseType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Linear"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.Linear);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SineIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.SineIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SineOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.SineOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SineInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.SineInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuadIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuadIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuadOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuadOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuadInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuadInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CubicIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CubicIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CubicOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CubicOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CubicInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CubicInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuartIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuartIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuartOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuartOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuartInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuartInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuintIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuintIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuintOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuintOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "QuintInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.QuintInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ExpoIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ExpoIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ExpoOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ExpoOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ExpoInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ExpoInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CircIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CircIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CircOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CircOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CircInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.CircInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ElasticIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ElasticIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ElasticOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ElasticOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ElasticInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.ElasticInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BackIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BackIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BackOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BackOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BackInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BackInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BounceIn"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BounceIn);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BounceOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BounceOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BounceInOut"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.BounceInOut);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Custom"))
                {
                    translator.PushFairyGUIEaseType(L, FairyGUI.EaseType.Custom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for FairyGUI.EaseType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for FairyGUI.EaseType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class FairyGUIRelationTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(FairyGUI.RelationType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(FairyGUI.RelationType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(FairyGUI.RelationType), L, null, 26, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left_Left", FairyGUI.RelationType.Left_Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left_Center", FairyGUI.RelationType.Left_Center);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left_Right", FairyGUI.RelationType.Left_Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Center_Center", FairyGUI.RelationType.Center_Center);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right_Left", FairyGUI.RelationType.Right_Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right_Center", FairyGUI.RelationType.Right_Center);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right_Right", FairyGUI.RelationType.Right_Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top_Top", FairyGUI.RelationType.Top_Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top_Middle", FairyGUI.RelationType.Top_Middle);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top_Bottom", FairyGUI.RelationType.Top_Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Middle_Middle", FairyGUI.RelationType.Middle_Middle);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom_Top", FairyGUI.RelationType.Bottom_Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom_Middle", FairyGUI.RelationType.Bottom_Middle);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom_Bottom", FairyGUI.RelationType.Bottom_Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Width", FairyGUI.RelationType.Width);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Height", FairyGUI.RelationType.Height);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftExt_Left", FairyGUI.RelationType.LeftExt_Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftExt_Right", FairyGUI.RelationType.LeftExt_Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightExt_Left", FairyGUI.RelationType.RightExt_Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightExt_Right", FairyGUI.RelationType.RightExt_Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopExt_Top", FairyGUI.RelationType.TopExt_Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopExt_Bottom", FairyGUI.RelationType.TopExt_Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomExt_Top", FairyGUI.RelationType.BottomExt_Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomExt_Bottom", FairyGUI.RelationType.BottomExt_Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Size", FairyGUI.RelationType.Size);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(FairyGUI.RelationType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushFairyGUIRelationType(L, (FairyGUI.RelationType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Left_Left"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Left_Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Left_Center"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Left_Center);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Left_Right"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Left_Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Center_Center"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Center_Center);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right_Left"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Right_Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right_Center"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Right_Center);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right_Right"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Right_Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top_Top"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Top_Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top_Middle"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Top_Middle);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top_Bottom"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Top_Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Middle_Middle"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Middle_Middle);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom_Top"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Bottom_Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom_Middle"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Bottom_Middle);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom_Bottom"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Bottom_Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Width"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Width);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Height"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Height);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftExt_Left"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.LeftExt_Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LeftExt_Right"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.LeftExt_Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightExt_Left"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.RightExt_Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightExt_Right"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.RightExt_Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopExt_Top"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.TopExt_Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopExt_Bottom"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.TopExt_Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomExt_Top"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.BottomExt_Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomExt_Bottom"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.BottomExt_Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Size"))
                {
                    translator.PushFairyGUIRelationType(L, FairyGUI.RelationType.Size);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for FairyGUI.RelationType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for FairyGUI.RelationType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class FairyGUITweenPropTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(FairyGUI.TweenPropType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(FairyGUI.TweenPropType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(FairyGUI.TweenPropType), L, null, 18, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", FairyGUI.TweenPropType.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "X", FairyGUI.TweenPropType.X);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Y", FairyGUI.TweenPropType.Y);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Z", FairyGUI.TweenPropType.Z);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "XY", FairyGUI.TweenPropType.XY);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Position", FairyGUI.TweenPropType.Position);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Width", FairyGUI.TweenPropType.Width);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Height", FairyGUI.TweenPropType.Height);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Size", FairyGUI.TweenPropType.Size);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ScaleX", FairyGUI.TweenPropType.ScaleX);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ScaleY", FairyGUI.TweenPropType.ScaleY);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Scale", FairyGUI.TweenPropType.Scale);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Rotation", FairyGUI.TweenPropType.Rotation);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RotationX", FairyGUI.TweenPropType.RotationX);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RotationY", FairyGUI.TweenPropType.RotationY);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alpha", FairyGUI.TweenPropType.Alpha);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Progress", FairyGUI.TweenPropType.Progress);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(FairyGUI.TweenPropType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushFairyGUITweenPropType(L, (FairyGUI.TweenPropType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "X"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.X);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Y"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Y);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Z"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Z);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "XY"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.XY);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Position"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Position);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Width"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Width);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Height"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Height);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Size"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Size);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ScaleX"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.ScaleX);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ScaleY"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.ScaleY);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Scale"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Scale);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Rotation"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Rotation);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RotationX"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.RotationX);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RotationY"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.RotationY);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alpha"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Alpha);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Progress"))
                {
                    translator.PushFairyGUITweenPropType(L, FairyGUI.TweenPropType.Progress);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for FairyGUI.TweenPropType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for FairyGUI.TweenPropType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class Casinos_eProjectItemDisplayNameKeyWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Casinos._eProjectItemDisplayNameKey), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Casinos._eProjectItemDisplayNameKey), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Casinos._eProjectItemDisplayNameKey), L, null, 3, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Gold", Casinos._eProjectItemDisplayNameKey.Gold);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Diamond", Casinos._eProjectItemDisplayNameKey.Diamond);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Casinos._eProjectItemDisplayNameKey), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushCasinos_eProjectItemDisplayNameKey(L, (Casinos._eProjectItemDisplayNameKey)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Gold"))
                {
                    translator.PushCasinos_eProjectItemDisplayNameKey(L, Casinos._eProjectItemDisplayNameKey.Gold);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Diamond"))
                {
                    translator.PushCasinos_eProjectItemDisplayNameKey(L, Casinos._eProjectItemDisplayNameKey.Diamond);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Casinos._eProjectItemDisplayNameKey!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Casinos._eProjectItemDisplayNameKey! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class _eAsyncAssetLoadTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(_eAsyncAssetLoadType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(_eAsyncAssetLoadType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(_eAsyncAssetLoadType), L, null, 7, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalBundle", _eAsyncAssetLoadType.LocalBundle);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WWWBundle", _eAsyncAssetLoadType.WWWBundle);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalBundleAsset", _eAsyncAssetLoadType.LocalBundleAsset);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WWWBundleAsset", _eAsyncAssetLoadType.WWWBundleAsset);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LocalRawAsset", _eAsyncAssetLoadType.LocalRawAsset);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WWWRawAsset", _eAsyncAssetLoadType.WWWRawAsset);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(_eAsyncAssetLoadType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.Push_eAsyncAssetLoadType(L, (_eAsyncAssetLoadType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "LocalBundle"))
                {
                    translator.Push_eAsyncAssetLoadType(L, _eAsyncAssetLoadType.LocalBundle);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WWWBundle"))
                {
                    translator.Push_eAsyncAssetLoadType(L, _eAsyncAssetLoadType.WWWBundle);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LocalBundleAsset"))
                {
                    translator.Push_eAsyncAssetLoadType(L, _eAsyncAssetLoadType.LocalBundleAsset);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WWWBundleAsset"))
                {
                    translator.Push_eAsyncAssetLoadType(L, _eAsyncAssetLoadType.WWWBundleAsset);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LocalRawAsset"))
                {
                    translator.Push_eAsyncAssetLoadType(L, _eAsyncAssetLoadType.LocalRawAsset);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WWWRawAsset"))
                {
                    translator.Push_eAsyncAssetLoadType(L, _eAsyncAssetLoadType.WWWRawAsset);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for _eAsyncAssetLoadType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for _eAsyncAssetLoadType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class _ePayTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(_ePayType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(_ePayType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(_ePayType), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "wx", _ePayType.wx);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "alipay", _ePayType.alipay);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "iap", _ePayType.iap);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(_ePayType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.Push_ePayType(L, (_ePayType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "wx"))
                {
                    translator.Push_ePayType(L, _ePayType.wx);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "alipay"))
                {
                    translator.Push_ePayType(L, _ePayType.alipay);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "iap"))
                {
                    translator.Push_ePayType(L, _ePayType.iap);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for _ePayType!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for _ePayType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class Casinos_eEditorRunSourcePlatformWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Casinos._eEditorRunSourcePlatform), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Casinos._eEditorRunSourcePlatform), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Casinos._eEditorRunSourcePlatform), L, null, 4, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Android", Casinos._eEditorRunSourcePlatform.Android);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IOS", Casinos._eEditorRunSourcePlatform.IOS);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PC", Casinos._eEditorRunSourcePlatform.PC);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Casinos._eEditorRunSourcePlatform), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushCasinos_eEditorRunSourcePlatform(L, (Casinos._eEditorRunSourcePlatform)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "Android"))
                {
                    translator.PushCasinos_eEditorRunSourcePlatform(L, Casinos._eEditorRunSourcePlatform.Android);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "IOS"))
                {
                    translator.PushCasinos_eEditorRunSourcePlatform(L, Casinos._eEditorRunSourcePlatform.IOS);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PC"))
                {
                    translator.PushCasinos_eEditorRunSourcePlatform(L, Casinos._eEditorRunSourcePlatform.PC);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Casinos._eEditorRunSourcePlatform!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Casinos._eEditorRunSourcePlatform! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}