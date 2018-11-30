-- Copyright(c) Cragon. All rights reserved.
-- 刚进游戏主动弹出的活动对话框。弹多个时，由ControllerActive控制依次弹出。

---------------------------------------
ViewActivityPopup = class(ViewBase)

---------------------------------------
function ViewActivityPopup:ctor()
    self.Context = Context
    self.Tween = nil
end

---------------------------------------
function ViewActivityPopup:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi)
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.GLoaderContent = self.ComUi:GetChild("LoaderContent").asLoader
    self.GTextContent = self.ComUi:GetChild("TextContent").asTextField
    local btn_close = self.ComUi:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
end

---------------------------------------
function ViewActivityPopup:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
    local ev = self.ViewMgr:GetEv("EvUiCloseActivityPopupBox")
    if (ev == nil) then
        ev = EvUiCloseActivityPopupBox:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewActivityPopup:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewActivityPopup:SetActivityInfo(item)
    if (item.ContenText ~= nil) then
        self.GTextContent.text = item.ContenText
    end

    if (item.ContentImage ~= nil) then
        local content_image = item.ContentImage
        local t = {}
        table.insert(t, self.Context.Cfg.OssRootUrl)
        table.insert(t, "/Activity/")
        table.insert(t, content_image)
        table.insert(t, ".png")
        local t_str = table.concat(t)

        CS.Casinos.HeadIconMgr.Instant:asyncLoadIcon(content_image, t_str, content_image, nil,
                function(ex, tick)
                    if (ex ~= nil and self.GLoaderContent ~= nil and self.GLoaderContent.displayObject ~= nil and self.GLoaderContent.displayObject.gameObject ~= nil) then
                        local texture = CS.Casinos.LuaHelper.UnityObjectCastToTexture(ex, true)
                        self.GLoaderContent.texture = CS.FairyGUI.NTexture(texture)
                    end
                end
        )
    end
end

---------------------------------------
ViewActivityPopupFactory = class(ViewFactory)

---------------------------------------
function ViewActivityPopupFactory:CreateView()
    local view = ViewActivityPopup:new()
    return view
end