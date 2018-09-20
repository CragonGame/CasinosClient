ViewShare = ViewBase:new()

function ViewShare:new(o)
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

    return o
end

function ViewShare:onCreate()
    self.ComUi.onClick:Add(
            function()
                self:onClickClose()
            end)

    local co_headicon = self.ComUi:GetChild("ComHead").asCom
    self.LoaderIcon = co_headicon:GetChild("LoaderHead").asLoader
    local loader = CS.Casinos.LuaHelper.GLoaderCastToGLoaderEx(self.LoaderIcon)
    loader.LoaderDoneCallBack = function(bo)
        self:loadIconDone(bo)
    end
    self.NickName = self.ComUi:GetChild("NickName").asTextField
    self.LoaderQRCode = self.ComUi:GetChild("LoaderQRCode").asLoader
end

function ViewShare:onUpdate(tm)
end

function ViewShare:setPlayerInfo(nick_name,account_id,share_type)
    self.ShareType = share_type
    self.NickName.text = string.format("%s:%s",self.ViewMgr.LanMgr:getLanValue("NickName"),nick_name)
    local icon_resource_name = ""
    local temp_table = CS.Casinos.LuaHelper.getIconName(true, account_id, icon_resource_name)
    local icon = temp_table[1]
    if (icon ~= nil and string.len(icon) > 0)
    then
        self.LoaderIcon.icon = CS.Casinos.CasinosContext.Instance.UserConfig.Current.PlayerIconDomain .. icon
    end
    self.LoaderQRCode.texture = CS.FairyGUI.NTexture(Native.Instance.PlayerQRCodeTexture)
end

function ViewShare:loadIconDone(is_success)
    local pic_name = "Share.png"
    local pic_path = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(pic_name)
    local p_t = CS.cn.sharesdk.unity3d.PlatformType.WeChat
    if self.ShareType == ShareType.WeChat then
        p_t = CS.cn.sharesdk.unity3d.PlatformType.WeChat
    elseif(self.ShareType == ShareType.WeChatMoments) then
        p_t = CS.cn.sharesdk.unity3d.PlatformType.WeChatMoments
    end
    PicCapture.Instance:CapturePic(pic_name, function()
        Native.Instance:ShareContent(p_t, self.ViewMgr.LanMgr:getLanValue("PlayGameNow"), pic_path, self.ViewMgr.LanMgr:getLanValue("CragonPoker"),
                Native.Instance.ShareUrl, CS.cn.sharesdk.unity3d.ContentType.Image)--Webpage
    end)
end

function ViewShare:onClickClose()
    self.ViewMgr:destroyView(self)
end

ViewShareFactory = ViewFactory:new()

function ViewShareFactory:new(o, ui_package_name, ui_component_name,
                              ui_layer, is_single, fit_screen)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

function ViewShareFactory:createView()
    local view = ViewShare:new(nil)
    return view
end