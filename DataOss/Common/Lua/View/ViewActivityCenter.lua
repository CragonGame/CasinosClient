-- Copyright(c) Cragon. All rights reserved.
-- 如果活动Item中有按钮之类功能，消息在ViewActiveCenter中响应。如分享按钮

---------------------------------------
ViewActivityCenter = ViewBase:new()

---------------------------------------
function ViewActivityCenter:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    o.Tween = nil
    return o
end

---------------------------------------
function ViewActivityCenter:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("Activity"))
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerActivity = self.ViewMgr.ControllerMgr:GetController("Activity")
    self.GListActivityTitle = self.ComUi:GetChild("ListActivityTitle").asList
    self.GLoaderCurrentActContent = self.ComUi:GetChild("LoaderActContent").asLoader
    self.GTextCurrentActContent = self.ComUi:GetChild("TextActContent").asTextField
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    self.BtnShare = self.ComUi:GetChild("BtnShare").asCom
    self.BtnShare.onClick:Add(
            function()
                self:onClickShare()
            end)
    self:setActivityTitleList()
end

---------------------------------------
function ViewActivityCenter:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewActivityCenter:SetCurrentSelectItem(item)
    if (self.CurrentSelectItem == item) then
        return
    else
        if (self.CurrentSelectItem ~= nil) then
            self.CurrentSelectItem:BeSelectedOrNot(false)
        end
        self.CurrentSelectItem = item
        local show_share = false
        if self.CurrentSelectItem.ItemActivity.IsShare then
            show_share = true
        end

        self.CurrentSelectItem:BeSelectedOrNot(true)
        local content_text = self.CurrentSelectItem.ItemActivity.ContenText
        local content_image = self.CurrentSelectItem.ItemActivity.ContentImage
        local show_content = false
        if (content_text ~= nil) then
            show_content = true
            self.GTextCurrentActContent.text = content_text
        end
        ViewHelper:SetGObjectVisible(show_content, self.GTextCurrentActContent)
        ViewHelper:SetGObjectVisible(false, self.GLoaderCurrentActContent)
        ViewHelper:SetGObjectVisible(false, self.BtnShare)
        if (content_image ~= nil) then
            local t = {}
            table.insert(t, CS.Casinos.CasinosContext.Instance.Config.ConfigUrl)
            table.insert(t, "/Activity/")
            table.insert(t, content_image)
            table.insert(t, ".png")
            local t_str = table.concat(t)
            CS.Casinos.HeadIconMgr.Instant:asyncLoadIcon(content_image, t_str, content_image, nil,
                    function(ex, tick)
                        if (ex ~= nil and self.GLoaderCurrentActContent ~= nil and self.GLoaderCurrentActContent.displayObject ~= nil and self.GLoaderCurrentActContent.displayObject.gameObject ~= nil) then
                            ViewHelper:SetGObjectVisible(show_share, self.BtnShare)
                            ViewHelper:SetGObjectVisible(false, self.GTextCurrentActContent)
                            ViewHelper:SetGObjectVisible(true, self.GLoaderCurrentActContent)
                            local texture = CS.Casinos.LuaHelper.UnityObjectCastToTexture(ex, true)
                            self.GLoaderCurrentActContent.texture = CS.FairyGUI.NTexture(texture)
                        end
                    end
            )
        end
    end
end

---------------------------------------
function ViewActivityCenter:setActivityTitleList()
    local list_act = self.ControllerActivity.ListActivity
    if (#list_act == 0) then
        return
    else
        for i = 1, #list_act do
            local com = self.GListActivityTitle:AddItemFromPool().asCom
            local item = list_act[i]
            local item_activitytile = ItemActivityTitle:new(nil, com, item, self)
            if (i == 1) then
                self:SetCurrentSelectItem(item_activitytile)
            end
        end
    end
end

---------------------------------------
function ViewActivityCenter:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewActivityCenter:onClickShare()
    self.ViewMgr:CreateView("ShareType")
end

---------------------------------------
ViewActivityCenterFactory = class(ViewFactory)

---------------------------------------
function ViewActivityCenterFactory:CreateView()
    local view = ViewActivityCenter:new(nil)
    return view
end