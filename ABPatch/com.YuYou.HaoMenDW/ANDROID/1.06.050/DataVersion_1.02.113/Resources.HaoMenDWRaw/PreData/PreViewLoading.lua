PreViewLoading = PreViewBase:new()

function PreViewLoading:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.ViewMgr = nil
	self.GoUi = nil
	self.ComUi = nil
	self.Panel = nil
	self.UILayer = nil
	self.InitDepth = nil
	self.ViewKey = nil
	if(self.Instance==nil)
	then
		self.Instance = o
	end

    return self.Instance
end

function PreViewLoading:onCreate()
	local pro = self.ComUi:GetChild("Progress")
	if(pro ~= nil)
	then
		self.GProgressBar = pro.asProgress
		self.GProgressBar.max = 100
		self.GProgressBar.value = 0
	end

	local text = self.ComUi:GetChild("Tips")
	if(text ~= nil)
	then
		self.GTextFieldTips = text.asTextField
	end

	self.OnFinished = nil
	self.IsAuto = false
	self.ListRandomTips = {}
	CS.FairyGUI.Timers.inst:Add(0, 0, self._updateTips)

    local co_commonbg = self.ComUi:GetChild("ComLoadingBg")
    if (co_commonbg ~= nil)
    then
        local co_commonbgex = co_commonbg.asCom
        co_commonbgex.width = self.ComUi.width
        co_commonbgex.height = self.ComUi.height
        CS.Casinos.UiHelperCasinos.SetLoadingBgParticle(co_commonbgex)
    end
end

function PreViewLoading:onDestroy()	
	if (self.IsAuto  == true)
    then
		CS.FairyGUI.Timers.inst:Remove(self._playProgress)
    end

    CS.FairyGUI.Timers.inst:Remove(self._updateTips)
end

function PreViewLoading:onUpdate(tm)		
end

function PreViewLoading:onHandleEv(ev)	
end
        
function PreViewLoading.fireAutoLoadingProgress()       
	local loading = PreViewLoading:new(nil)	
    loading.GProgressBar.visible = true
    loading.IsAuto = true
    
    CS.FairyGUI.Timers.inst:Add(0.01, 0, loading._playProgress)      
end

function PreViewLoading.fireManualLoadingProgress(progress, loading_info)        
	local loading = PreViewLoading:new(nil)	
    loading.IsAuto = false
    if (progress ~= 0)
    then
        loading.GProgressBar.visible = true
    end

    setTip(loading_info)
    local cur = loading.GProgressBar.value
    cur = cur + progress
    if (loading.GProgressBar ~= nil)
    then
        loading.GProgressBar.value = cur
        if (loading.GProgressBar.value < loading.GProgressBar.max)
        then
        else        
            if (OnFinished ~= nil)
            then
                CS.FairyGUI.Timers.inst:Remove(loading._updateTips)
                OnFinished()
            end
        end
    end
end

function PreViewLoading.setLoadingProgress(progress)
	local loading = PreViewLoading:new(nil)
    loading.GProgressBar.visible = true   
    if (loading.GProgressBar ~= nil)
    then
        loading.GProgressBar.value = progress
        if (loading.GProgressBar.value < loading.GProgressBar.max)
        then
        else        
            if (OnFinished ~= nil)
            then
                CS.FairyGUI.Timers.inst:Remove(loading._updateTips)
                OnFinished()
            end
        end
    end
end

function PreViewLoading.setTip(tip)	
	local loading = PreViewLoading:new(nil)	
	loading.ListRandomTips = {}
	loading.ListRandomTips[tip] = tip    
	
    local data_version = CS.Casinos.CasinosContext.Instance.Config.InitDataVersion
    local data_version_key = CS.Casinos.CasinosContext.LocalDataVersionKey
    if (CS.UnityEngine.PlayerPrefs.HasKey(data_version_key))
    then
        data_version = CS.UnityEngine.PlayerPrefs.GetString(data_version_key)
    end
		
    local gtext_version = loading.ComUi:GetChild("Version")
    if (gtext_version ~= nil)
    then
        local version_text = gtext_version.asTextField
        version_text.text = string.format("应用版本：%s 数据版本：%s", CS.UnityEngine.Application.version, data_version)
    end
end

function PreViewLoading.setTips(list_tips)
	local loading = PreViewLoading:new(nil)	
    loading.ListRandomTips = {}
	for k,v in pairs(list_tips) do
		loading.ListRandomTips[k] = v
	end    
end

function PreViewLoading._updateTips(param)	
	local loading = PreViewLoading:new(nil)	
    local tips_key,tips_value = nil
	local count = loading.PreViewMgr:GetTableCount(loading.ListRandomTips)	

    if (count > 0)
    then		
        tips_key,tips_value = loading.PreViewMgr:GetAndRemoveTableFirstEle(loading.ListRandomTips)		
    end

    if ((tips_key ~= nil and string.len(tips_key) > 0))
    then
		if(loading.GTextFieldTips ~= nil)
		then
			loading.GTextFieldTips.text = tips_key
		end
    end
end

function PreViewLoading._playProgress(param)
	local loading = PreViewLoading:new(nil)	
    if (loading.GProgressBar ~= nil)    
	then
        loading.GProgressBar.visible = true
        if (loading.GProgressBar.value <= loading.GProgressBar.max)
        then
			local value = loading.GProgressBar.value
			value = value+2
             loading.GProgressBar.value = value
        else        
            CS.FairyGUI.Timers.inst.Remove(loading._playProgress)

            if (OnFinished ~= nil)
            then
                OnFinished()
            end
        end
    end
end

PreViewLoadingFactory = PreViewFactory:new()

function PreViewLoadingFactory:new(o,ui_package_name,ui_component_name,
	ui_layer,is_single,fit_screen)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self
	self.PackageName = ui_package_name
	self.ComponentName = ui_component_name
	self.UILayer = ui_layer
	self.IsSingle = is_single
	self.FitScreen = fit_screen
    return o
end

function PreViewLoadingFactory:createView()	
	local view = PreViewLoading:new(nil)	
	return view
end