-- 加载界面

ViewLoading = ViewBase:new()

function ViewLoading:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	o.ViewMgr = nil
	o.GoUi = nil
	o.ComUi = nil
	o.Panel = nil
	o.UILayer = nil
	o.InitDepth = nil
	o.ViewKey = nil

    return o
end

function ViewLoading:onCreate()	
	local pro = self.ComUi:GetChild("Progress")
	if(pro ~= nil)
	then
		self.GProgressBar = pro.asProgress
		self.GProgressBar.max = 100
		self.GProgressBar.value = 0
        self.GProgressBar.visible = false
	end

	local text = self.ComUi:GetChild("Tips")
	if(text ~= nil)
	then
		self.GTextFieldTips = text.asTextField
	end

	self.OnFinished = nil
	self.IsAuto = false
	self.ListRandomTips = {}

	--CS.FairyGUI.Timers.inst:Add(0, 0, self._updateTips)

	--local bg = self.ComUi:GetChild("Bg")
	--if(bg ~= nil)
	--then
	--	CS.Casinos.UiHelperCasinos.makeUiBgFiteScreen(bg, self.ComUi.width, self.ComUi.height, bg.width, bg.height)
	--end
	
	
	local com_bg = self.ComUi:GetChild("ComBg").asCom
	local image_bg = com_bg:GetChild("ImageMote").asImage
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	if(self.CasinosContext.NeedHideClientUi == false)
	then
		image_bg.visible = false
		local p_helper = ParticleHelper:new(nil)
		local abTextAtlas = p_helper:GetSpine("Spine/LoadingMarry/mary_loading.atlas.ab")
		local atlas = abTextAtlas:LoadAsset("Mary_Loading.atlas")
		local abtexture = p_helper:GetSpine("Spine/LoadingMarry/mary_loading.ab")
		local texture = abtexture:LoadAsset("Mary_Loading")
		local abjson = p_helper:GetSpine("Spine/LoadingMarry/mary_loadingjson.ab")
		local json = abjson:LoadAsset("Mary_LoadingJson")
		
		self.PlayerAnim = CS.Casinos.SpineHelper.LoadResourcesPrefab(atlas,texture,json,"Spine/Skeleton",314)
		local moteParent = self.ComUi:GetChild("MoteParent").asCom
		self.PlayerAnim.transform.position = moteParent.displayObject.gameObject.transform.position
		self.PlayerAnim.transform.localScale = CS.UnityEngine.Vector3(1.25,1.25,1)
		self.PlayerAnim.transform.gameObject.layer = moteParent.displayObject.gameObject.layer
		self.PlayerAnim:Initialize(false)
		self.PlayerAnim.loop = true
		self.MoteRender = self.PlayerAnim.transform.gameObject:GetComponent("MeshRenderer")
		self.PlayerAnim.AnimationName = "animation"
		self.MoteRender.sortingOrder = 315
	else
		image_bg.visible = true
	end
end

function ViewLoading:onDestroy()	
	if(self.CasinosContext.NeedHideClientUi == false)
	then
		CS.UnityEngine.GameObject.Destroy(self.PlayerAnim.transform.gameObject)
	end
    --if (self.IsAuto  == true)
    --then
		--CS.FairyGUI.Timers.inst:Remove(self._playProgress)
    --end

    --CS.FairyGUI.Timers.inst:Remove(self._updateTips)
end

function ViewLoading:onUpdate(tm)		
end

function ViewLoading:onHandleEv(ev)	
end
        
function ViewLoading.fireAutoLoadingProgress()
	local loading = ViewLoading:new(nil)	
	loading.GProgressBar.visible = true
	loading.IsAuto = true

	--CS.FairyGUI.Timers.inst:Add(0.01, 0, loading._playProgress)
end

function ViewLoading.fireManualLoadingProgress(progress, loading_info)        
	local loading = ViewLoading:new(nil)	
    loading.IsAuto = false
    if (progress ~= 0)
    then
        loading.GProgressBar.visible = true
    end

    --setTip(loading_info)
    local cur = loading.GProgressBar.value
    cur = cur + progress
    if (loading.GProgressBar ~= nil)
    then
        loading.GProgressBar.value = cur
        if (loading.GProgressBar.value < loading.GProgressBar.max)
        then
        else        
            if (loading.OnFinished ~= nil)
            then
                --CS.FairyGUI.Timers.inst:Remove(loading._updateTips)
                loading.OnFinished()
            end
        end
    end
end

function ViewLoading.setLoadingProgress(progress)
	local loading = ViewLoading:new(nil)	
    loading.GProgressBar.visible = true   
    if (loading.GProgressBar ~= nil)
    then
        loading.GProgressBar.value = progress
        if (loading.GProgressBar.value < loading.GProgressBar.max)
        then
        else        
            if (loading.OnFinished ~= nil)
            then
                --CS.FairyGUI.Timers.inst:Remove(loading._updateTips)
                loading.OnFinished()
            end
        end
    end
end

function ViewLoading:setTip(tip)
	self.ViewMgr.MianC.LuaHelper:DeleteAllTableEle(self.ListRandomTips)
    self.ListRandomTips[tip] = tip

    local data_version = CS.Casinos.CasinosContext.Instance.Config.InitDataVersion
    local data_version_key = self.ViewMgr.MianC.GetLocalVersionInfoKey()
    if (CS.UnityEngine.PlayerPrefs.HasKey(data_version_key))
    then
        data_version = CS.UnityEngine.PlayerPrefs.GetString(data_version_key)
    end

    local gtext_version = self.ComUi:GetChild("Version")
    if (gtext_version ~= nil)
    then
        local version_text = gtext_version.asTextField
        version_text.text = string.format("Ӧ�ð汾��%s ���ݰ汾��%s", CS.UnityEngine.Application.version, data_version)
    end
end

function ViewLoading:setTips(list_tips)
    self.ViewMgr.MianC.LuaHelper:DeleteAllTableEle(loading.ListRandomTips)
	for k,v in pairs(list_tips) do
        self.ListRandomTips[k] = v
	end    
end

function ViewLoading:_updateTips(param)
    local tips = nil
	local count = self.ViewMgr.MianC.LuaHelper:GetTableCount(self.ListRandomTips)
    if (count > 0)
    then
        tips = self.ListRandomTips[CS.UnityEngine.Random.Range(0, count)]
    end

    if ((tips ~= nil and string.len(tips) <= 0))
    then
		if(self.GTextFieldTips ~= nil)
		then
            self.GTextFieldTips.text = tips
		end
    end
end

function ViewLoading:_playProgress(param)
    if (self.GProgressBar ~= nil)
	then
        self.GProgressBar.visible = true
        if (self.GProgressBar.value <= self.GProgressBar.max)
        then
			local value = self.GProgressBar.value
			value = value + 2
            self.GProgressBar.value = value
        else        
            --CS.FairyGUI.Timers.inst.Remove(loading._playProgress)

            if (self.OnFinished ~= nil)
            then
                self.OnFinished()
            end
        end
    end
end

ViewLoadingFactory = ViewFactory:new()

function ViewLoadingFactory:new(o,ui_package_name,ui_component_name,
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

function ViewLoadingFactory:createView()	
	local view = ViewLoading:new(nil)	
	return view
end