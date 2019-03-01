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
    public class cnsharesdkunity3dShareContentWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(cn.sharesdk.unity3d.ShareContent);
			Utils.BeginObjectRegister(type, L, translator, 0, 83, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetHidePlatforms", _m_SetHidePlatforms);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTitle", _m_SetTitle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetText", _m_SetText);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUrl", _m_SetUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetImagePath", _m_SetImagePath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetImageUrl", _m_SetImageUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetImageArray", _m_SetImageArray);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetShareType", _m_SetShareType);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTitleUrl", _m_SetTitleUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetComment", _m_SetComment);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSite", _m_SetSite);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSiteUrl", _m_SetSiteUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAddress", _m_SetAddress);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFilePath", _m_SetFilePath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMusicUrl", _m_SetMusicUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLatitude", _m_SetLatitude);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLongitude", _m_SetLongitude);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSource", _m_SetSource);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAuthor", _m_SetAuthor);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSafetyLevel", _m_SetSafetyLevel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetContentType", _m_SetContentType);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetHidden", _m_SetHidden);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIsPublic", _m_SetIsPublic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIsFriend", _m_SetIsFriend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIsFamily", _m_SetIsFamily);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFriendsOnly", _m_SetFriendsOnly);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetGroupID", _m_SetGroupID);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAudioPath", _m_SetAudioPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetVideoPath", _m_SetVideoPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetNotebook", _m_SetNotebook);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTags", _m_SetTags);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPrivateStatus", _m_SetPrivateStatus);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetObjectID", _m_SetObjectID);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAlbumID", _m_SetAlbumID);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEmotionPath", _m_SetEmotionPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetExtInfoPath", _m_SetExtInfoPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSourceFileExtension", _m_SetSourceFileExtension);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSourceFilePath", _m_SetSourceFilePath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetThumbImageUrl", _m_SetThumbImageUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUrlDescription", _m_SetUrlDescription);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetBoard", _m_SetBoard);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMenuX", _m_SetMenuX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMenuY", _m_SetMenuY);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetVisibility", _m_SetVisibility);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetBlogName", _m_SetBlogName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRecipients", _m_SetRecipients);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCCRecipients", _m_SetCCRecipients);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetBCCRecipients", _m_SetBCCRecipients);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAttachmentPath", _m_SetAttachmentPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDesc", _m_SetDesc);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIsPrivateFromSource", _m_SetIsPrivateFromSource);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetResolveFinalUrl", _m_SetResolveFinalUrl);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFolderId", _m_SetFolderId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTweetID", _m_SetTweetID);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetToUserID", _m_SetToUserID);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPermission", _m_SetPermission);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEnableShare", _m_SetEnableShare);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetImageWidth", _m_SetImageWidth);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetImageHeight", _m_SetImageHeight);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAppButtonTitle", _m_SetAppButtonTitle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAndroidExecParam", _m_SetAndroidExecParam);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAndroidMarkParam", _m_SetAndroidMarkParam);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIphoneExecParam", _m_SetIphoneExecParam);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIphoneMarkParam", _m_SetIphoneMarkParam);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIpadExecParam", _m_SetIpadExecParam);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIpadMarkParam", _m_SetIpadMarkParam);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTemplateArgs", _m_SetTemplateArgs);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTemplateId", _m_SetTemplateId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFacebookHashtag", _m_SetFacebookHashtag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFacebookQuote", _m_SetFacebookQuote);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMessengerGif", _m_SetMessengerGif);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEnableClientShare", _m_SetEnableClientShare);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEnableSinaWeiboAPIShare", _m_SetEnableSinaWeiboAPIShare);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEnableAdvancedInterfaceShare", _m_SetEnableAdvancedInterfaceShare);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSinaShareEnableShareToStory", _m_SetSinaShareEnableShareToStory);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMiniProgramUserName", _m_SetMiniProgramUserName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMiniProgramPath", _m_SetMiniProgramPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMiniProgramWithShareTicket", _m_SetMiniProgramWithShareTicket);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMiniProgramType", _m_SetMiniProgramType);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetMiniProgramHdThumbImage", _m_SetMiniProgramHdThumbImage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetShareContentCustomize", _m_SetShareContentCustomize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetShareParamsStr", _m_GetShareParamsStr);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetShareParams", _m_GetShareParams);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					cn.sharesdk.unity3d.ShareContent gen_ret = new cn.sharesdk.unity3d.ShareContent();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to cn.sharesdk.unity3d.ShareContent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetHidePlatforms(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string[] _hidePlatformList = (string[])translator.GetObject(L, 2, typeof(string[]));
                    
                    gen_to_be_invoked.SetHidePlatforms( _hidePlatformList );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTitle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _title = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetTitle( _title );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetText(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetText( _text );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetUrl( _url );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetImagePath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _imagePath = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetImagePath( _imagePath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetImageUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _imageUrl = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetImageUrl( _imageUrl );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetImageArray(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string[] _imageArray = (string[])translator.GetObject(L, 2, typeof(string[]));
                    
                    gen_to_be_invoked.SetImageArray( _imageArray );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetShareType(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _shareType = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetShareType( _shareType );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTitleUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _titleUrl = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetTitleUrl( _titleUrl );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetComment(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _comment = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetComment( _comment );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSite(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _site = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetSite( _site );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSiteUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _siteUrl = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetSiteUrl( _siteUrl );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAddress(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _address = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetAddress( _address );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFilePath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _filePath = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetFilePath( _filePath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMusicUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _musicUrl = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetMusicUrl( _musicUrl );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLatitude(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _latitude = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetLatitude( _latitude );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLongitude(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _longitude = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetLongitude( _longitude );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSource(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _source = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetSource( _source );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAuthor(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _author = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetAuthor( _author );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSafetyLevel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _safetyLevel = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetSafetyLevel( _safetyLevel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetContentType(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _contentType = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetContentType( _contentType );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetHidden(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _hidden = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetHidden( _hidden );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIsPublic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _isPublic = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetIsPublic( _isPublic );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIsFriend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _isFriend = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetIsFriend( _isFriend );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIsFamily(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _isFamily = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetIsFamily( _isFamily );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFriendsOnly(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _friendsOnly = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetFriendsOnly( _friendsOnly );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGroupID(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _groupID = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetGroupID( _groupID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAudioPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _audioPath = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetAudioPath( _audioPath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetVideoPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _videoPath = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetVideoPath( _videoPath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetNotebook(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _notebook = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetNotebook( _notebook );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTags(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _tags = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetTags( _tags );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPrivateStatus(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _status = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetPrivateStatus( _status );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetObjectID(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _objectId = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetObjectID( _objectId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAlbumID(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _albumId = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetAlbumID( _albumId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEmotionPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _emotionPath = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetEmotionPath( _emotionPath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetExtInfoPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _extInfoPath = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetExtInfoPath( _extInfoPath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSourceFileExtension(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _sourceFileExtension = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetSourceFileExtension( _sourceFileExtension );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSourceFilePath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _sourceFilePath = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetSourceFilePath( _sourceFilePath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetThumbImageUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _thumbImageUrl = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetThumbImageUrl( _thumbImageUrl );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUrlDescription(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _urlDescription = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetUrlDescription( _urlDescription );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetBoard(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _SetBoard = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetBoard( _SetBoard );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMenuX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _menuX = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetMenuX( _menuX );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMenuY(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _menuY = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetMenuY( _menuY );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetVisibility(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _visibility = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetVisibility( _visibility );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetBlogName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _blogName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetBlogName( _blogName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRecipients(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _recipients = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetRecipients( _recipients );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCCRecipients(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _ccRecipients = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetCCRecipients( _ccRecipients );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetBCCRecipients(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _bccRecipients = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetBCCRecipients( _bccRecipients );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAttachmentPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _attachmentPath = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetAttachmentPath( _attachmentPath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDesc(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _desc = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetDesc( _desc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIsPrivateFromSource(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _isPrivateFromSource = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetIsPrivateFromSource( _isPrivateFromSource );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetResolveFinalUrl(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _resolveFinalUrl = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetResolveFinalUrl( _resolveFinalUrl );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFolderId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _folderId = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetFolderId( _folderId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTweetID(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _tweetID = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetTweetID( _tweetID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetToUserID(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _toUserID = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetToUserID( _toUserID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPermission(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _permission = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetPermission( _permission );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEnableShare(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enableShare = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetEnableShare( _enableShare );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetImageWidth(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _imageWidth = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetImageWidth( _imageWidth );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetImageHeight(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _imageHeight = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetImageHeight( _imageHeight );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAppButtonTitle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _appButtonTitle = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetAppButtonTitle( _appButtonTitle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAndroidExecParam(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Hashtable _androidExecParam = (System.Collections.Hashtable)translator.GetObject(L, 2, typeof(System.Collections.Hashtable));
                    
                    gen_to_be_invoked.SetAndroidExecParam( _androidExecParam );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAndroidMarkParam(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _androidMarkParam = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetAndroidMarkParam( _androidMarkParam );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIphoneExecParam(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Hashtable _iphoneExecParam = (System.Collections.Hashtable)translator.GetObject(L, 2, typeof(System.Collections.Hashtable));
                    
                    gen_to_be_invoked.SetIphoneExecParam( _iphoneExecParam );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIphoneMarkParam(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _iphoneMarkParam = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetIphoneMarkParam( _iphoneMarkParam );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIpadExecParam(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Hashtable _ipadExecParam = (System.Collections.Hashtable)translator.GetObject(L, 2, typeof(System.Collections.Hashtable));
                    
                    gen_to_be_invoked.SetIpadExecParam( _ipadExecParam );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIpadMarkParam(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _ipadMarkParam = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetIpadMarkParam( _ipadMarkParam );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTemplateArgs(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Hashtable _templateArgs = (System.Collections.Hashtable)translator.GetObject(L, 2, typeof(System.Collections.Hashtable));
                    
                    gen_to_be_invoked.SetTemplateArgs( _templateArgs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTemplateId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _templateId = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetTemplateId( _templateId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFacebookHashtag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _hashtag = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetFacebookHashtag( _hashtag );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFacebookQuote(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _quote = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetFacebookQuote( _quote );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMessengerGif(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _gif = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetMessengerGif( _gif );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEnableClientShare(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enalble = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetEnableClientShare( _enalble );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEnableSinaWeiboAPIShare(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enalble = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetEnableSinaWeiboAPIShare( _enalble );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEnableAdvancedInterfaceShare(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enalble = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetEnableAdvancedInterfaceShare( _enalble );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSinaShareEnableShareToStory(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enalble = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetSinaShareEnableShareToStory( _enalble );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMiniProgramUserName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _userName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetMiniProgramUserName( _userName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMiniProgramPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetMiniProgramPath( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMiniProgramWithShareTicket(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enalble = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetMiniProgramWithShareTicket( _enalble );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMiniProgramType(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _type = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetMiniProgramType( _type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetMiniProgramHdThumbImage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _hdThumbImage = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetMiniProgramHdThumbImage( _hdThumbImage );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetShareContentCustomize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    cn.sharesdk.unity3d.PlatformType _platform;translator.Get(L, 2, out _platform);
                    cn.sharesdk.unity3d.ShareContent _content = (cn.sharesdk.unity3d.ShareContent)translator.GetObject(L, 3, typeof(cn.sharesdk.unity3d.ShareContent));
                    
                    gen_to_be_invoked.SetShareContentCustomize( _platform, _content );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetShareParamsStr(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.GetShareParamsStr(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetShareParams(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                cn.sharesdk.unity3d.ShareContent gen_to_be_invoked = (cn.sharesdk.unity3d.ShareContent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Hashtable gen_ret = gen_to_be_invoked.GetShareParams(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
