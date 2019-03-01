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
    public class UnityEngineNetworkingUnityWebRequestWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Networking.UnityWebRequest);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 23, 13);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendWebRequest", _m_SendWebRequest);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Abort", _m_Abort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRequestHeader", _m_GetRequestHeader);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRequestHeader", _m_SetRequestHeader);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetResponseHeader", _m_GetResponseHeader);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetResponseHeaders", _m_GetResponseHeaders);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "disposeCertificateHandlerOnDispose", _g_get_disposeCertificateHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "disposeDownloadHandlerOnDispose", _g_get_disposeDownloadHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "disposeUploadHandlerOnDispose", _g_get_disposeUploadHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "method", _g_get_method);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "error", _g_get_error);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "useHttpContinue", _g_get_useHttpContinue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "url", _g_get_url);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uri", _g_get_uri);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "responseCode", _g_get_responseCode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uploadProgress", _g_get_uploadProgress);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isModifiable", _g_get_isModifiable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isDone", _g_get_isDone);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isNetworkError", _g_get_isNetworkError);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isHttpError", _g_get_isHttpError);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "downloadProgress", _g_get_downloadProgress);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uploadedBytes", _g_get_uploadedBytes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "downloadedBytes", _g_get_downloadedBytes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "redirectLimit", _g_get_redirectLimit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "chunkedTransfer", _g_get_chunkedTransfer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uploadHandler", _g_get_uploadHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "downloadHandler", _g_get_downloadHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "certificateHandler", _g_get_certificateHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "timeout", _g_get_timeout);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "disposeCertificateHandlerOnDispose", _s_set_disposeCertificateHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "disposeDownloadHandlerOnDispose", _s_set_disposeDownloadHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "disposeUploadHandlerOnDispose", _s_set_disposeUploadHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "method", _s_set_method);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "useHttpContinue", _s_set_useHttpContinue);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "url", _s_set_url);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uri", _s_set_uri);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "redirectLimit", _s_set_redirectLimit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "chunkedTransfer", _s_set_chunkedTransfer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uploadHandler", _s_set_uploadHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "downloadHandler", _s_set_downloadHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "certificateHandler", _s_set_certificateHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "timeout", _s_set_timeout);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 18, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearCookieCache", _m_ClearCookieCache_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Get", _m_Get_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Delete", _m_Delete_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Head", _m_Head_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Put", _m_Put_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Post", _m_Post_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EscapeURL", _m_EscapeURL_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnEscapeURL", _m_UnEscapeURL_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SerializeFormSections", _m_SerializeFormSections_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GenerateBoundary", _m_GenerateBoundary_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SerializeSimpleForm", _m_SerializeSimpleForm_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbGET", UnityEngine.Networking.UnityWebRequest.kHttpVerbGET);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbHEAD", UnityEngine.Networking.UnityWebRequest.kHttpVerbHEAD);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbPOST", UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbPUT", UnityEngine.Networking.UnityWebRequest.kHttpVerbPUT);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbCREATE", UnityEngine.Networking.UnityWebRequest.kHttpVerbCREATE);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbDELETE", UnityEngine.Networking.UnityWebRequest.kHttpVerbDELETE);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UnityEngine.Networking.UnityWebRequest gen_ret = new UnityEngine.Networking.UnityWebRequest();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))
				{
					string _url = LuaAPI.lua_tostring(L, 2);
					
					UnityEngine.Networking.UnityWebRequest gen_ret = new UnityEngine.Networking.UnityWebRequest(_url);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.Uri>(L, 2))
				{
					System.Uri _uri = (System.Uri)translator.GetObject(L, 2, typeof(System.Uri));
					
					UnityEngine.Networking.UnityWebRequest gen_ret = new UnityEngine.Networking.UnityWebRequest(_uri);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING))
				{
					string _url = LuaAPI.lua_tostring(L, 2);
					string _method = LuaAPI.lua_tostring(L, 3);
					
					UnityEngine.Networking.UnityWebRequest gen_ret = new UnityEngine.Networking.UnityWebRequest(_url, _method);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<System.Uri>(L, 2) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING))
				{
					System.Uri _uri = (System.Uri)translator.GetObject(L, 2, typeof(System.Uri));
					string _method = LuaAPI.lua_tostring(L, 3);
					
					UnityEngine.Networking.UnityWebRequest gen_ret = new UnityEngine.Networking.UnityWebRequest(_uri, _method);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<UnityEngine.Networking.DownloadHandler>(L, 4) && translator.Assignable<UnityEngine.Networking.UploadHandler>(L, 5))
				{
					string _url = LuaAPI.lua_tostring(L, 2);
					string _method = LuaAPI.lua_tostring(L, 3);
					UnityEngine.Networking.DownloadHandler _downloadHandler = (UnityEngine.Networking.DownloadHandler)translator.GetObject(L, 4, typeof(UnityEngine.Networking.DownloadHandler));
					UnityEngine.Networking.UploadHandler _uploadHandler = (UnityEngine.Networking.UploadHandler)translator.GetObject(L, 5, typeof(UnityEngine.Networking.UploadHandler));
					
					UnityEngine.Networking.UnityWebRequest gen_ret = new UnityEngine.Networking.UnityWebRequest(_url, _method, _downloadHandler, _uploadHandler);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<System.Uri>(L, 2) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<UnityEngine.Networking.DownloadHandler>(L, 4) && translator.Assignable<UnityEngine.Networking.UploadHandler>(L, 5))
				{
					System.Uri _uri = (System.Uri)translator.GetObject(L, 2, typeof(System.Uri));
					string _method = LuaAPI.lua_tostring(L, 3);
					UnityEngine.Networking.DownloadHandler _downloadHandler = (UnityEngine.Networking.DownloadHandler)translator.GetObject(L, 4, typeof(UnityEngine.Networking.DownloadHandler));
					UnityEngine.Networking.UploadHandler _uploadHandler = (UnityEngine.Networking.UploadHandler)translator.GetObject(L, 5, typeof(UnityEngine.Networking.UploadHandler));
					
					UnityEngine.Networking.UnityWebRequest gen_ret = new UnityEngine.Networking.UnityWebRequest(_uri, _method, _downloadHandler, _uploadHandler);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearCookieCache_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 0) 
                {
                    
                    UnityEngine.Networking.UnityWebRequest.ClearCookieCache(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Uri>(L, 1)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    
                    UnityEngine.Networking.UnityWebRequest.ClearCookieCache( _uri );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.ClearCookieCache!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendWebRequest(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.Networking.UnityWebRequestAsyncOperation gen_ret = gen_to_be_invoked.SendWebRequest(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Abort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Abort(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRequestHeader(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.GetRequestHeader( _name );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRequestHeader(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    string _value = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.SetRequestHeader( _name, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResponseHeader(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.GetResponseHeader( _name );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResponseHeaders(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Generic.Dictionary<string, string> gen_ret = gen_to_be_invoked.GetResponseHeaders(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Get_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Get( _uri );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Uri>(L, 1)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Get( _uri );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.Get!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Delete_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Delete( _uri );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Uri>(L, 1)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Delete( _uri );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.Delete!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Head_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Head( _uri );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Uri>(L, 1)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Head( _uri );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.Head!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Put_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    byte[] _bodyData = LuaAPI.lua_tobytes(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Put( _uri, _bodyData );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Uri>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    byte[] _bodyData = LuaAPI.lua_tobytes(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Put( _uri, _bodyData );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    string _bodyData = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Put( _uri, _bodyData );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Uri>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    string _bodyData = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Put( _uri, _bodyData );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.Put!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Post_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    string _postData = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _postData );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Uri>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    string _postData = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _postData );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.WWWForm>(L, 2)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.WWWForm _formData = (UnityEngine.WWWForm)translator.GetObject(L, 2, typeof(UnityEngine.WWWForm));
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _formData );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Uri>(L, 1)&& translator.Assignable<UnityEngine.WWWForm>(L, 2)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    UnityEngine.WWWForm _formData = (UnityEngine.WWWForm)translator.GetObject(L, 2, typeof(UnityEngine.WWWForm));
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _formData );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>>(L, 2)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> _multipartFormSections = (System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>));
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _multipartFormSections );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Uri>(L, 1)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>>(L, 2)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> _multipartFormSections = (System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>));
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _multipartFormSections );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.Dictionary<string, string>>(L, 2)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.Dictionary<string, string> _formFields = (System.Collections.Generic.Dictionary<string, string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, string>));
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _formFields );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Uri>(L, 1)&& translator.Assignable<System.Collections.Generic.Dictionary<string, string>>(L, 2)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    System.Collections.Generic.Dictionary<string, string> _formFields = (System.Collections.Generic.Dictionary<string, string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, string>));
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _formFields );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> _multipartFormSections = (System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>));
                    byte[] _boundary = LuaAPI.lua_tobytes(L, 3);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _multipartFormSections, _boundary );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Uri>(L, 1)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.Uri _uri = (System.Uri)translator.GetObject(L, 1, typeof(System.Uri));
                    System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> _multipartFormSections = (System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>));
                    byte[] _boundary = LuaAPI.lua_tobytes(L, 3);
                    
                        UnityEngine.Networking.UnityWebRequest gen_ret = UnityEngine.Networking.UnityWebRequest.Post( _uri, _multipartFormSections, _boundary );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.Post!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EscapeURL_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _s = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = UnityEngine.Networking.UnityWebRequest.EscapeURL( _s );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 2)) 
                {
                    string _s = LuaAPI.lua_tostring(L, 1);
                    System.Text.Encoding _e = (System.Text.Encoding)translator.GetObject(L, 2, typeof(System.Text.Encoding));
                    
                        string gen_ret = UnityEngine.Networking.UnityWebRequest.EscapeURL( _s, _e );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.EscapeURL!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnEscapeURL_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _s = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = UnityEngine.Networking.UnityWebRequest.UnEscapeURL( _s );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 2)) 
                {
                    string _s = LuaAPI.lua_tostring(L, 1);
                    System.Text.Encoding _e = (System.Text.Encoding)translator.GetObject(L, 2, typeof(System.Text.Encoding));
                    
                        string gen_ret = UnityEngine.Networking.UnityWebRequest.UnEscapeURL( _s, _e );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.UnEscapeURL!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SerializeFormSections_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> _multipartFormSections = (System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>)translator.GetObject(L, 1, typeof(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>));
                    byte[] _boundary = LuaAPI.lua_tobytes(L, 2);
                    
                        byte[] gen_ret = UnityEngine.Networking.UnityWebRequest.SerializeFormSections( _multipartFormSections, _boundary );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GenerateBoundary_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        byte[] gen_ret = UnityEngine.Networking.UnityWebRequest.GenerateBoundary(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SerializeSimpleForm_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Collections.Generic.Dictionary<string, string> _formFields = (System.Collections.Generic.Dictionary<string, string>)translator.GetObject(L, 1, typeof(System.Collections.Generic.Dictionary<string, string>));
                    
                        byte[] gen_ret = UnityEngine.Networking.UnityWebRequest.SerializeSimpleForm( _formFields );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_disposeCertificateHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.disposeCertificateHandlerOnDispose);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_disposeDownloadHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.disposeDownloadHandlerOnDispose);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_disposeUploadHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.disposeUploadHandlerOnDispose);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_method(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.method);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_error(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.error);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_useHttpContinue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.useHttpContinue);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_url(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.url);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uri(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uri);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_responseCode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushint64(L, gen_to_be_invoked.responseCode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uploadProgress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.uploadProgress);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isModifiable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isModifiable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isDone(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isDone);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isNetworkError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isNetworkError);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isHttpError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isHttpError);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_downloadProgress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.downloadProgress);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uploadedBytes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushuint64(L, gen_to_be_invoked.uploadedBytes);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_downloadedBytes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushuint64(L, gen_to_be_invoked.downloadedBytes);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_redirectLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.redirectLimit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_chunkedTransfer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.chunkedTransfer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uploadHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uploadHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_downloadHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.downloadHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_certificateHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.certificateHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_timeout(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.timeout);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_disposeCertificateHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.disposeCertificateHandlerOnDispose = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_disposeDownloadHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.disposeDownloadHandlerOnDispose = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_disposeUploadHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.disposeUploadHandlerOnDispose = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_method(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.method = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_useHttpContinue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.useHttpContinue = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_url(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.url = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uri(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uri = (System.Uri)translator.GetObject(L, 2, typeof(System.Uri));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_redirectLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.redirectLimit = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_chunkedTransfer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.chunkedTransfer = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uploadHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uploadHandler = (UnityEngine.Networking.UploadHandler)translator.GetObject(L, 2, typeof(UnityEngine.Networking.UploadHandler));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_downloadHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.downloadHandler = (UnityEngine.Networking.DownloadHandler)translator.GetObject(L, 2, typeof(UnityEngine.Networking.DownloadHandler));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_certificateHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.certificateHandler = (UnityEngine.Networking.CertificateHandler)translator.GetObject(L, 2, typeof(UnityEngine.Networking.CertificateHandler));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_timeout(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.timeout = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
