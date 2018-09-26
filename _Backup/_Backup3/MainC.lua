-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ChannelConfigLuaName = 'Config.lua'

---------------------------------------
MainC = {
	Instance = nil,
	WWWLoader = nil,
	LuaHelper = nil,
	CommonLuaLoader = nil,
	ProjectDataLoader = nil,
	FileLoader = nil,
	SessionMgr = nil,
	ProjectListener = nil,
	WWWLoadLuaHelper = nil,
	WWWLoadWWWLoader = nil,
	WWWLoadCommonLua = nil,
	WWWLoadProjectDataLua = nil,
	WWWChannelConfigLua = nil,
	WWWLoadFileLoader = nil,
	LoadLuaHelperDone = false,
	LoadWWWLoaderDone = false
	-- WWW追加随机数，调用C#封装的函数,LuaHelper需要下载，下载LuaHelper的地址也需要追加随机数
}

---------------------------------------
local local_dataversionkey = 'LocalVersionInfo'
local local_commonlua_versionkey = 'LocalCommonLuaVersion'
local local_autoupdate_key = "AutoUpdate"
local compare_dataversion = false
local can_compare_dataversion = false
local is_reload = false
local re_loadcommonlua = false
local loadconfig_callback = nil

---------------------------------------
function MainC:new(o)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
    if(self.Instance==nil)
	then
		self.Instance = o
	end
    return self.Instance
end

---------------------------------------
function MainCInit()    
	MainC = MainC:new(nil)
	local casino_context = CS.Casinos.CasinosContext.Instance
	casino_context.MainCLua = MainC	
	MainC:LoadConfig(false,nil)
end

---------------------------------------
function MainC:LoadConfig(reload,callback_allload)
	compare_dataversion = false
	can_compare_dataversion = false
	is_reload = reload
	re_loadcommonlua = false
	loadconfig_callback = callback_allload
	local casino_context = CS.Casinos.CasinosContext.Instance
	casino_context.LoadDataDone = false
	local id = CS.UnityEngine.Application.identifier
    local l = #id
    local index1,index2 = string.find(id,'%.',5)    
    local channel_name = string.sub(id,index1+1)    		
	if(string.len(channel_name) == 0)
	then
		channel_name = casino_context.Config.ChannelNameBase
	end
		
	local channel_config_url = casino_context.Config.ConfigUrl
	channel_config_url = table.concat({channel_config_url,"/"})
	channel_config_url = table.concat({channel_config_url,casino_context:getPlatformName(false)})
	channel_config_url = table.concat({channel_config_url,"/Bundle/"})
	local local_bundle_version = CS.UnityEngine.Application.version	
	channel_config_url = table.concat({channel_config_url,local_bundle_version})
	channel_config_url = table.concat({channel_config_url,"/"})
	channel_config_url = table.concat({channel_config_url,channel_name})
	channel_config_url = table.concat({channel_config_url,"/"})
	channel_config_url = table.concat({channel_config_url,ChannelConfigLuaName})
	channel_config_url = CS.Casinos.CasinoHelper.FormalUrlWithRandomVersion(channel_config_url)		
	MainC.WWWChannelConfigLua = CS.UnityEngine.WWW(channel_config_url)		
end

---------------------------------------
function MainCUpdate(tm)
	if(MainC.WWWChannelConfigLua ~= nil)
	then
		if (MainC.WWWChannelConfigLua.isDone == true)
		then
			if ((MainC.WWWChannelConfigLua.error == nil or (MainC.WWWChannelConfigLua.error ~= nil and string.len(MainC.WWWChannelConfigLua.error) <= 0)))
			then
				MainC:unloadLua("Config")
				local casinos_lua = CS.Casinos.CasinosContext.Instance.CasinosLua
				casinos_lua:addLuaFile('Config',MainC.WWWChannelConfigLua.bytes)
				CS.Casinos.CasinosContext.Instance.CasinosLua:doString("Config")
				MainC:_loadConfigDown()
				MainC.WWWChannelConfigLua = nil
			else
				CS.Casinos.UiHelperCasinos.UiShowPreLoading("WWWChannelConfigLua Error"..MainC.WWWChannelConfigLua.error.."  url = "..MainC.WWWChannelConfigLua.url,0)
			end
		end
	end

	if(MainC.WWWLoadLuaHelper ~= nil)
	then
		if(MainC.WWWLoadLuaHelper.isDone == true)
		then
			if ((MainC.WWWLoadLuaHelper.error == nil or (MainC.WWWLoadLuaHelper.error ~= nil and string.len(MainC.WWWLoadLuaHelper.error) <= 0)))
			then
				MainC:unloadLua("LuaHelper")
				local casinos_lua = CS.Casinos.CasinosContext.Instance.CasinosLua
				casinos_lua:addLuaFile('LuaHelper',MainC.WWWLoadLuaHelper.bytes)
				MainC.WWWLoadLuaHelper = nil
				MainC.LoadLuaHelperDone = true
				if(MainC.LoadWWWLoaderDone == true)
				then
					MainC:_loadHelperDown()
				end
			else
				CS.Casinos.UiHelperCasinos.UiShowPreLoading("WWWLoadLuaHelper Error "..MainC.WWWLoadLuaHelper.error.."  url = "..MainC.WWWLoadLuaHelper.url,0)
				return
			end
		end
	end
	if(MainC.WWWLoadWWWLoader ~= nil)
	then
		if(MainC.WWWLoadWWWLoader.isDone == true)
		then
			if ((MainC.WWWLoadWWWLoader.error == nil or (MainC.WWWLoadWWWLoader.error ~= nil and string.len(MainC.WWWLoadWWWLoader.error) <= 0)))
			then
				MainC:unloadLua("WWWLoader")
				local casinos_lua = CS.Casinos.CasinosContext.Instance.CasinosLua
				casinos_lua:addLuaFile('WWWLoader',MainC.WWWLoadWWWLoader.bytes)
				MainC.WWWLoadWWWLoader = nil
				MainC.LoadWWWLoaderDone = true
				if(MainC.LoadLuaHelperDone == true)
				then
					MainC:_loadHelperDown()
				end
			else
				CS.Casinos.UiHelperCasinos.UiShowPreLoading("WWWLoadWWWLoader Error "..MainC.WWWLoadWWWLoader.error.."  url = "..MainC.WWWLoadWWWLoader.url,0)
				return
			end
		end
	end

	if(MainC.WWWLoadFileLoader ~= nil)
	then
		if(MainC.WWWLoadFileLoader.isDone == true)
		then
			if ((MainC.WWWLoadFileLoader.error == nil or (MainC.WWWLoadFileLoader.error ~= nil and string.len(MainC.WWWLoadFileLoader.error) <= 0)))
			then
				MainC:unloadLua("FileLoader")
				local casinos_lua = CS.Casinos.CasinosContext.Instance.CasinosLua
				casinos_lua:addLuaFile('FileLoader',MainC.WWWLoadFileLoader.bytes)				
		        MainC.WWWLoadFileLoader = nil
				MainC:_loadFileLoaderDown()
			else				
				CS.Casinos.UiHelperCasinos.UiShowPreLoading("WWWLoadFileLoader Error "..MainC.WWWLoadFileLoader.error.."  url = "..MainC.WWWLoadFileLoader.url,0)	
				return
			end
		end
	end

	if(MainC.WWWLoadCommonLua ~= nil)
	then
		if (MainC.WWWLoadCommonLua.isDone == true)
		then
			if ((MainC.WWWLoadCommonLua.error == nil or (MainC.WWWLoadCommonLua.error ~= nil and string.len(MainC.WWWLoadCommonLua.error) <= 0)))
			then
				MainC:unloadLua("CommonLuaLoader")
				local casinos_lua = CS.Casinos.CasinosContext.Instance.CasinosLua
				casinos_lua:addLuaFile('CommonLuaLoader',MainC.WWWLoadCommonLua.bytes)
				CS.Casinos.CasinosContext.Instance.CasinosLua:doString("CommonLuaLoader")
				MainC.CommonLuaLoader = CommonLuaLoader:new(nil)
				MainC.CommonLuaLoader:onCreate(CommonLuaVersion,CommonLuaFileListTxtName,CommonLuaRootURL,
						local_commonlua_versionkey,MainC._loadCommonLuaDown)
				MainC.WWWLoadCommonLua = nil
			else
				CS.Casinos.UiHelperCasinos.UiShowPreLoading("WWWLoadCommonLua Error "..MainC.WWWLoadCommonLua.error.."  url = "..MainC.WWWLoadCommonLua.url,0)
			end
		end
	end

	if(CS.UnityEngine.PlayerPrefs.HasKey(local_dataversionkey) == true and compare_dataversion == false and can_compare_dataversion == true)
	then
		compare_dataversion = true
		local data_version_issame = MainC:_compareDataVersion()
		if(data_version_issame ~= true)
		then	
			if (CS.UnityEngine.Application.internetReachability == CS.UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
		then
				if loadconfig_callback ~= nil then
					loadconfig_callback(true)
				end
			MainC:_confirmUpdateData()
		else
			if(MainC:_checkIfNeedConfirm())
			then
			--更新数据 显示Ui,点击确定 更新
			local tips = "游戏运行需要更新美术资源，\n点击确定开始更新，请耐心等待！"
			local lan = CS.Casinos.CasinosContext.Instance.CurrentLan
			if(lan == "English")
			then
				tips = "The game needs to be updated with art resources. \nClick OK and start updating. Please be patient!"
			else 
				if(lan == "Chinese" or lan == "ChineseSimplified")
				then
					tips = "游戏运行需要更新美术资源，\n点击确定开始更新，请耐心等待！"
				end
			end	
			CS.Casinos.UiHelperCasinos.UiShowPreMsgBox(tips,
			function()
				if loadconfig_callback ~= nil then
					loadconfig_callback(true)
				end
				MainC:_saveAutoUpdateTm()
				MainC:_confirmUpdateData()
			end,MainC._cancelUpdate)				
			else
				if loadconfig_callback ~= nil then
					loadconfig_callback(true)
				end
				MainC:_confirmUpdateData()
			end
			end
		else
			if is_reload and re_loadcommonlua == false then
				if loadconfig_callback ~= nil then
					loadconfig_callback(false)
				end
				CS.Casinos.CasinosContext.Instance:SetLoadDataDone()
			else
				if re_loadcommonlua then
					if loadconfig_callback ~= nil then
						loadconfig_callback(true)
					end
				end
				--不需要更新数据，直接解包
				MainC._loadProjectDataDown()
			end
		end	
	end
	
	if(MainC.WWWLoadProjectDataLua ~= nil)
	then
		if (MainC.WWWLoadProjectDataLua.isDone == true)
		then		
		    if ((MainC.WWWLoadProjectDataLua.error == nil or (MainC.WWWLoadProjectDataLua.error ~= nil and string.len(MainC.WWWLoadProjectDataLua.error) <= 0)))        
			then
				MainC:unloadLua("ProjectDataLoader")
				local casinos_lua = CS.Casinos.CasinosContext.Instance.CasinosLua
				casinos_lua:addLuaFile('ProjectDataLoader',MainC.WWWLoadProjectDataLua.bytes)
				CS.Casinos.CasinosContext.Instance.CasinosLua:doString('ProjectDataLoader')
				MainC.ProjectDataLoader = ProjectDataLoader:new(nil)
				MainC.ProjectDataLoader:onCreate(DataVersion,DataFileListTextName,ProjectDataRootURL,
					local_dataversionkey,MainC._loadProjectDataDown)
		        MainC.WWWLoadProjectDataLua = nil
			else
				CS.Casinos.UiHelperCasinos.UiShowPreLoading("WWWLoadProjectDataLua Error "..MainC.WWWLoadProjectDataLua.error.."  url = "..MainC.WWWLoadProjectDataLua.url,0)	
			end
		end
	end	
	
	if(MainC.FileLoader ~= nil)
	then
		MainC.FileLoader:onUpdate(tm)
	end

	if(MainC.WWWLoader ~= nil)
	then
		MainC.WWWLoader:onUpdate(tm)
	end

	if(MainC.CommonLuaLoader ~= nil)
	then
		MainC.CommonLuaLoader:onUpdate(tm)
	end
	
	if(MainC.ProjectDataLoader ~= nil)
	then
		MainC.ProjectDataLoader:onUpdate(tm)
	end		

	if(MainC.ProjectListener ~=nil)
	then
		MainC.ProjectListener:onUpdate(tm)
	end
end

---------------------------------------
function MainCRelease()   
	if(MainC.CommonLuaLoader ~= nil)
	then
		MainC.CommonLuaLoader:onRelease()
	end

	if(MainC.ProjectDataLoader ~= nil)
	then
		MainC.ProjectDataLoader:onRelease()
	end

	if(MainC.ProjectListener ~=nil)
	then
		MainC.ProjectListener:onDestroy()
	end
end

---------------------------------------
function CreateController(table_data)
	MainC.ProjectListener:CreateController(table_data)
end

---------------------------------------
function MainC.GetLocalVersionInfoKey()
	return local_dataversionkey
end

---------------------------------------
function MainC:_loadHelperDown()	
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("LuaHelper")
	MainC.LuaHelper = LuaHelper:new(nil)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("WWWLoader")
	MainC.WWWLoader = WWWLoader:new(nil)
	MainC.WWWLoader:onCreate()

	local local_bundle_version = CS.UnityEngine.Application.version
	if(string.len(BundleUpdateURL) > 0)
	then
		if (CS.UnityEngine.Application.internetReachability == CS.UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
			then
				MainC:_confirmUpdateBundle()
			else
			if(MainC:_checkIfNeedConfirm())
				then
		-- 更新Bundle 显示Ui,点击确定 更新
		local tips = "游戏版本更新，\n点击确定开始更新，请耐心等待！"
		local lan = CS.Casinos.CasinosContext.Instance.CurrentLan
		if(lan == "English")
		then
			tips = "Game version update, \n click OK to start updating, please be patient!"
		else 
			if(lan == "Chinese" or lan == "ChineseSimplified")
			then
				tips = "游戏版本更新，\n点击确定开始更新，请耐心等待！"
			end
		end
		CS.Casinos.UiHelperCasinos.UiShowPreMsgBox(tips,
		function()
					MainC:_saveAutoUpdateTm()
					MainC:_confirmUpdateBundle()
				end,MainC._cancelUpdate)
		else
			MainC:_confirmUpdateBundle()
		end
		end
	else
		local commonlua_version_issame = MainC:_compareCommonLuaVersion()
		if(commonlua_version_issame == false)
		then
		if (CS.UnityEngine.Application.internetReachability == CS.UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
			then
				MainC:_confirmLoadCommonLua()
			else
			if(MainC:_checkIfNeedConfirm())
				then
			--更新CommonLua,显示Ui,点击确定 更新
			local tips = "游戏运行需要更新数据，\n点击确定开始更新，请耐心等待！"
			local lan = CS.Casinos.CasinosContext.Instance.CurrentLan
			if(lan == "English")
			then
				tips = "Game running requires updating data. \n click OK to start updating. Please be patient!"
			else 
				if(lan == "Chinese" or lan == "ChineseSimplified")
				then
					tips = "游戏运行需要更新数据，\n点击确定开始更新，请耐心等待！"
				end
			end
			CS.Casinos.UiHelperCasinos.UiShowPreMsgBox(tips,
				function()
						MainC:_saveAutoUpdateTm()
						MainC:_confirmLoadCommonLua()
					end,MainC._cancelUpdate)
				else
					MainC:_confirmLoadCommonLua()
				end
			end
		else
			MainC:_checkIfNeedCopyAssets()
		end
	end
end

---------------------------------------
function MainC:_loadConfigDown()
	local casinos_context = CS.Casinos.CasinosContext.Instance
	local lua_helper_path = CommonLuaRootURL..CommonLuaHelper
	if(casinos_context.UseHttps == true)
	then
		lua_helper_path = "https://" .. lua_helper_path
	else
		lua_helper_path = "http://" .. lua_helper_path
	end	
	lua_helper_path = CS.Casinos.CasinoHelper.FormalUrlWithRandomVersion(lua_helper_path)
	MainC.WWWLoadLuaHelper = CS.UnityEngine.WWW(lua_helper_path)

	local www_loader_path = CommonLuaRootURL..CommonWWWLoader
	if(casinos_context.UseHttps == true)
	then
		www_loader_path = "https://" .. www_loader_path
	else
		www_loader_path = "http://" .. www_loader_path
	end	
	www_loader_path = CS.Casinos.CasinoHelper.FormalUrlWithRandomVersion(www_loader_path)
	MainC.WWWLoadWWWLoader = CS.UnityEngine.WWW(www_loader_path)

    casinos_context:regLuaFilePath("Resources.KingTexasRaw/Lua/", "KingTexasListener")
	
	local config_production = CS.Casinos.ConfigSection()
    config_production.UCenterDomain = CS.Casinos.UiHelper.formateAndoridIOSUrl(UCenterDomain)
    config_production.AutopatcherUrl = CS.Casinos.UiHelper.formateAndoridIOSUrl(AutopatcherUrl)
    config_production.PlayerIconDomain = CS.Casinos.UiHelper.formateAndoridIOSUrl(PlayerIconDomain)
    config_production.BotIconDomain = CS.Casinos.UiHelper.formateAndoridIOSUrl(BotIconDomain)
    config_production.GatewayIp = GatewayIp
    config_production.GatewayPort = GatewayPort
    config_production.Tips = casinos_context.UserConfig.ConfigProduction.Tips
    casinos_context.UserConfig.ConfigProduction = config_production
    local show_fps = ClientShowFPS			
	local show_fps_obj = casinos_context.Config.gameObject:GetComponent("ShowFPS")            
    show_fps_obj.enabled = show_fps
	if(FPSLimit == -1)
	then
		CS.UnityEngine.QualitySettings.vSyncCount = 0
	elseif(FPSLimit == 30)
	then
		CS.UnityEngine.QualitySettings.vSyncCount = 2
	elseif(FPSLimit == 60)
	then
		CS.UnityEngine.QualitySettings.vSyncCount = 1
	else
		CS.UnityEngine.QualitySettings.vSyncCount = 0
		CS.UnityEngine.Application.targetFrameRate = 60
	end
	
    local show_weichat = ClientShowWeiChat
    casinos_context.ShowWeiChat = show_weichat	
	casinos_context.NeedHideClientUi = NeedHideClientUi	
	casinos_context.ClientShowFirstRecharge = ClientShowFirstRecharge	
	casinos_context.DesktopHSysBankShowDBValue = DesktopHSysBankShowDBValue		
	casinos_context.ShootingTextShowVIPLimit = ShootingTextShowVIPLimit
	casinos_context.DesktopHCanChatVIPLimit = DesktopHCanChatVIPLimit	
	casinos_context.DesktopCanChatVIPLimit = DesktopCanChatVIPLimit
	casinos_context.CanReportLog = CanReportLog
	casinos_context.CanReportLogDeviceId = CanReportLogDeviceId
	casinos_context.CanReportLogPlayerId = CanReportLogPlayerId
	casinos_context.ShowGoldTree = ShowGoldTree
	if(casinos_context.UnityAndroid == true)
	then
		casinos_context.UseWeiChatPay = UseWeiChatPay
		casinos_context.UseALiPay = UseALiPay
	end
	--casinos_context.UseIAP = UseIAP
	casinos_context.UseLan = UseLan
	casinos_context.UseDefaultLan = UseDefaultLan
	casinos_context.DefaultLan = DefaultLan	
	casinos_context.CSharpLastMethodId = CSharpLastMethodId
	casinos_context.BuglyAppId = BuglyAppId
	casinos_context.PinggPPAppId = PinggPPAppId
	casinos_context.WeChatAppId = WeChatAppId 
	casinos_context.WeChatState = WeChatState 
	casinos_context.DataEyeId = DataEyeId
	casinos_context.PushAppId = PushAppId
	casinos_context.PushAppKey = PushAppKey
	casinos_context.PushAppSecret = PushAppSecret
	casinos_context.ShareSDKAppKey = ShareSDKAppKey
	casinos_context.ShareSDKAppSecret = ShareSDKAppSecret

    local list_dbcommon = DBListCommon
    local db_names_common = CS.Casinos.LuaHelper.spliteStr(list_dbcommon,"|")
	for k,v in pairs(db_names_common) do
		casinos_context.UserConfig:addDBName2(v)	
	end
    
    local list_dbclient = DBListClient
    local db_names_client = CS.Casinos.LuaHelper.spliteStr(list_dbclient,"|")
	for k,v in pairs(db_names_client) do
		casinos_context.UserConfig:addDBName2(v)
	end            
	
	casinos_context:loadConfigDone()
end

---------------------------------------
function MainC:_loadFileLoaderDown()
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("FileLoader")
	MainC.FileLoader = FileLoader:new(nil)
	MainC.FileLoader:onCreate()
	MainC.FileLoader:StartDownload(BundleUpdateURL,function(pro)
		CS.Casinos.UiHelperCasinos.UiShowPreLoading("下载新安装包中...",pro * 100)
	end,function(bytes)
		local path_mgr = CS.Casinos.CasinosContext.Instance.PathMgr
		local local_file_path =	path_mgr:combinePersistentDataPath("KingTexas.apk",true)	
		CS.Casinos.LuaHelper.writeFile(bytes,local_file_path)
		CS.NativeFun.installAPK(local_file_path)
		CS.UnityEngine.Application.Quit()
	end)
end

---------------------------------------
function MainC._loadCommonLuaDown()	
	MainC:_checkIfNeedCopyAssets()
end

---------------------------------------
function MainC._loadProjectDataDown()

	MainC:unloadLua("KingTexasListener")
	local casinos_context = CS.Casinos.CasinosContext.Instance
	casinos_context.CasinosLua:doString("KingTexasListener")
	MainC.ProjectListener = KingTexasListener:new(nil)
	MainC.ProjectListener:onCreate()	
	casinos_context.ProjectListener = MainC.ProjectListener
	
	casinos_context:setLoadDataDone()
end

---------------------------------------
function MainC:_checkIfNeedCopyAssets()
	can_compare_dataversion = true
	if(CS.UnityEngine.PlayerPrefs.HasKey(local_dataversionkey) ~= true)
	then
		CS.Casinos.CasinosContext.Instance:copyStreamingAssetsToPersistentDataPath()
	end
end

---------------------------------------
function MainC:_compareCommonLuaVersion()	
	local player_prefs = CS.UnityEngine.PlayerPrefs
	local version_issame = false
	if(player_prefs.HasKey(local_commonlua_versionkey) == true)
	then
		local local_commonlua_version = player_prefs.GetString(local_commonlua_versionkey)		
		if(local_commonlua_version == CommonLuaVersion)
		then
			version_issame = true
		else
			version_issame = false
		end
	end	
	return version_issame
end

---------------------------------------
function MainC:_compareDataVersion()	
	local player_prefs = CS.UnityEngine.PlayerPrefs
	local data_version_issame = false
	if(player_prefs.HasKey(local_dataversionkey) == true)
	then
		local local_dataversion = player_prefs.GetString(local_dataversionkey)
		if(local_dataversion == DataVersion)
		then			
			data_version_issame = true
		else			
			data_version_issame = false
		end	
	end
	
	return data_version_issame
end

---------------------------------------
function MainC:_confirmUpdateBundle()	
	local casinos_context = CS.Casinos.CasinosContext.Instance
	local channel_config_url = CommonLuaRootURL.."Helper/FileLoader.lua"
	if(casinos_context.UseHttps == true)
	then
		channel_config_url = "https://" .. channel_config_url
	else
		channel_config_url = "http://" .. channel_config_url
	end	
	channel_config_url = CS.Casinos.CasinoHelper.FormalUrlWithRandomVersion(channel_config_url)
	
	MainC.WWWLoadFileLoader = CS.UnityEngine.WWW(channel_config_url)			
end

---------------------------------------
function MainC:_confirmLoadCommonLua()
	if is_reload then
		re_loadcommonlua = true
	end
	MainC:_formatCommonLuaRootURL()

	local commonlua_loader_url = CommonLuaRootURL..CommonLuaLoaderPath
	commonlua_loader_url = CS.Casinos.CasinoHelper.FormalUrlWithRandomVersion(commonlua_loader_url)
	MainC.WWWLoadCommonLua = CS.UnityEngine.WWW(commonlua_loader_url)
end

---------------------------------------
function MainC:_confirmUpdateData()	
	MainC:_formatCommonLuaRootURL()
	if(CS.Casinos.CasinosContext.Instance.UseHttps == true)
	then				
		ProjectDataRootURL = "https://" .. ProjectDataRootURL
	else
		ProjectDataRootURL = "http://" .. ProjectDataRootURL
	end			

	local data_loader_url = CommonLuaRootURL..ProjectDataLoaderPath
	data_loader_url = CS.Casinos.CasinoHelper.FormalUrlWithRandomVersion(data_loader_url)
	MainC.WWWLoadProjectDataLua = CS.UnityEngine.WWW(data_loader_url)			
end

---------------------------------------
function MainC._cancelUpdate()	
	CS.UnityEngine.Application.Quit()
end

---------------------------------------
function MainC:_checkIfNeedConfirm()
	local need_confirm = true
	local player_prefs = CS.UnityEngine.PlayerPrefs
	if(player_prefs.HasKey(local_autoupdate_key) == true)
	then
		local local_data = player_prefs.GetString(local_autoupdate_key)
		local tm = CS.System.DateTime.Parse(local_data)
		local nowtm = CS.System.DateTime.Now
		local t = nowtm - tm
		if(t.Seconds <= 600)
		then
			need_confirm = false
		end
	end
	
	return need_confirm
end

---------------------------------------
function MainC:_saveAutoUpdateTm()
	CS.UnityEngine.PlayerPrefs.SetString(local_autoupdate_key,CS.System.DateTime.Now:ToString())
end

---------------------------------------
function MainC:_formatCommonLuaRootURL()
	local i, j = string.find(CommonLuaRootURL, "http")
	if(i == nil)
	then
		if(CS.Casinos.CasinosContext.Instance.UseHttps == true)
		then
			CommonLuaRootURL = "https://" .. CommonLuaRootURL
		else
			CommonLuaRootURL = "http://" .. CommonLuaRootURL
		end
	end	
end

---------------------------------------
function MainC:isReload()
	return is_reload
end

---------------------------------------
function MainC:doString(name)
	MainC:unloadLua(name)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString(name)
end

---------------------------------------
function MainC:unloadLua(name)
	package.preload[name] = nil
	package.loaded[name] = nil
end