ViewDailyReward = ViewBase:new()

function ViewDailyReward:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	o.REWARD_SOUND = nil
	o.ViewMgr = nil
	o.GoUi = nil
	o.ComUi = nil
	o.Panel = nil
	o.UILayer = nil
	o.InitDepth = nil
	o.ViewKey = nil

    return o
end

function ViewDailyReward:onCreate()
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("DailyRewardSign"))
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.MapDailyReward = {}
    self.GBtnGetReward = self.ComUi:GetChild("Lan_Btn_GetDailyReward").asButton
    self.GBtnGetReward.onClick:Add(
		function()
			self:onClickBtnGetReward()
		end
	)
	local btn_beVip = self.ComUi:GetChild("Lan_Btn_BecomeVIP").asButton
    btn_beVip.onClick:Add(
		function()
			self:onClickBtnBeVip()
		end
	)
	local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
	local btn_close = com_bg:GetChild("BtnClose").asButton
	btn_close.onClick:Add(
		function()
			self:close()
		end
	)
	local com_shade = com_bg:GetChild("ComShade").asCom
	com_shade.onClick:Add(
		function()
			self:close()
		end
	)
	self.PosParticle = self.ComUi:GetChild("ComParticlePos").position
end

function ViewDailyReward:onDestroy()

end

function ViewDailyReward:onUpdate(tm)
	if self.ParticleSystem ~= nil and self.ParticleSystem.isPlaying == false then
		self.ViewMgr:destroyView(self)
	end
end

function ViewDailyReward:setRewardInfo(list_dailyreward,tb_id,operate_action)
	self.mOperateAction = operate_action
    local list_reward = self.ComUi:GetChild("ListItemReward").asList
	self.CurrentDay = tb_id
    if (self.CurrentDay == 0)
	then
		self.CurrentDay = 7
	end
	for key,value in pairs(list_dailyreward)
	do
		local item_reward = list_reward:AddItemFromPool().asCom
        local day = value.Id
        if (day == 0)
		then
			day = 7
		end
        self.ViewMgr.LanMgr:parseComponent(item_reward)
        local reward_info = self:getTitle(day)
        self:createItemDailyReward(day,item_reward,value,self.CurrentDay,reward_info)
	end
end
	
function ViewDailyReward:createItemDailyReward(day,co_dailyreward,tb_reward,current_day,reward_info,show_icon)
	show_icon = false 
	self.MapDailyReward[day] = ItemDailyReward:new(nil,co_dailyreward,tb_reward,self.CurrentDay,reward_info,show_icon,self.ViewMgr)
end

function ViewDailyReward:onClickBtnBeVip()
	self.ViewMgr:destroyView(self)
	local shop = self.ViewMgr:createView("Shop")
	shop:showGold()
end

function ViewDailyReward:onClickBtnGetReward()
	local daily_reward = nil
	if(self.MapDailyReward[self.CurrentDay] ~= nil)
	then
		daily_reward = self.MapDailyReward[self.CurrentDay]
		daily_reward:setAlreadyGet()
	end
    self.AutoDestroyUi = true
    local particleblue_parent = self.ComUi:GetChild("FallGoldParent").asGraph
	local p_helper = ParticleHelper:new(nil)
    --local particle_path = self.CasinosContext.PathMgr:combinePersistentDataPath(
    --ViewHelper:getABParticleResourceTitlePath() .. "fallgoldex.ab")
	local particle2 = p_helper:GetParticel("fallgoldex.ab")
    --self.AssetBundleFallGold = CS.UnityEngine.AssetBundle.LoadFromFile(particle_path)
	local fall_gold = CS.UnityEngine.Object.Instantiate(particle2:LoadAsset("FallGoldEx"))
	--local fall_gold = CS.UnityEngine.Object.Instantiate(self.AssetBundleFallGold:LoadAsset("FallGoldEx"))
    particleblue_parent:SetNativeObject(CS.FairyGUI.GoWrapper(fall_gold))
	self.ParticleSystem = fall_gold:GetComponent("ParticleSystem")
	self.ParticleSystem:Play()
	--local auto_destroy = CS.Casinos.LuaHelper.GetComponentAutoDestroyParticle(fall_gold)
    --auto_destroy:play(
	--	function()
	--		self.ViewMgr:destroyView(self)
     --       self.AssetBundleFallGold:Unload(true)
	--	end
	--)
    if (self.mOperateAction ~= nil)
	then
		CS.Casinos.CasinosContext.Instance:Play(self.REWARD_SOUND, CS.Casinos._eSoundLayer.LayerNormal)
        self.mOperateAction(true)
	end
end

function ViewDailyReward:getTitle(day)
	local reward_info = tRewardInfo:new(nil)
    local title = self.ViewMgr.LanMgr:getLanValue(string.format("Day%s",day))
    local icon_url = ""
	if(day == 1)
	then
        icon_url = "ui://DailyReward/Chips" .. day
	elseif(day == 2)
	then
        icon_url = "ui://DailyReward/Chips" .. day
	elseif(day == 3)
	then
        icon_url = "ui://DailyReward/Chips" .. day
	elseif(day == 4)
	then
        icon_url = "ui://DailyReward/Chips" .. day
	elseif(day == 5)
	then
        icon_url = "ui://DailyReward/Chips" .. day
	elseif(day == 6)
	then
        icon_url = "ui://DailyReward/Chips" .. day
	elseif(day == 7)
	then
        icon_url = "ui://DailyReward/Chips" .. day
	end
	if (self.CasinosContext.UseLan == true)
	then
		title = self.ViewMgr.LanMgr:getLanValue("Day" .. day)
	end
    reward_info.title = title
    reward_info.icon_url = icon_url
    return reward_info
end

function ViewDailyReward:close()
	self.ViewMgr:destroyView(self)
end
			

ViewDailyRewardFactory = ViewFactory:new()

function ViewDailyRewardFactory:new(o,ui_package_name,ui_component_name,
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

function ViewDailyRewardFactory:createView()	
	local view = ViewDailyReward:new(nil)	
	return view
end


tRewardInfo = {}
function tRewardInfo:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.icon_url = nil
	o.title = nil
	return o
end
