-- Copyright(c) Cragon. All rights reserved.
-- 相册&拍照对话框

---------------------------------------
ViewTakePhoto = ViewBase:new()

---------------------------------------
function ViewTakePhoto:new(o)
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
    self.PhotoFinalPath = "/photos"
    self.GetPicDefaultName = "GetPicDefaultName"
    self.mPhotoSize = 640
    return o
end

---------------------------------------
function ViewTakePhoto:onCreate()
    ViewHelper:PopUi(self.ComUi)
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self.ViewMgr:destroyView(self)
            end
    )
    self.GBtnTakePhoto = self.ComUi:GetChild("Lan_Btn_Photograph").asButton
    self.GBtnTakePhoto.onClick:Add(
            function()
                self:onClickTakePhoto()
            end
    )
    self.GBtnTakePic = self.ComUi:GetChild("Lan_Btn_Photo").asButton
    self.GBtnTakePic.onClick:Add(
            function()
                self:onClickTakePic()
            end
    )
end

---------------------------------------
function ViewTakePhoto:onClickTakePhoto()
    ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("GetImage"))
    CS.NativeTakePhoto.takeNewPhoto(self.mPhotoSize, self.mPhotoSize, self.GetPicDefaultName .. ".jpg"
    , CS.Casinos.CasinosContext.Instance.PathMgr:getPersistentDataPath() .. self.PhotoFinalPath)
    self.ViewMgr:destroyView(self)
end

---------------------------------------
function ViewTakePhoto:onClickTakePic()
    ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("GetImage"))
    CS.NativeTakePhoto.takeExistPhoto(self.mPhotoSize, self.mPhotoSize, self.GetPicDefaultName .. ".jpg"
    , CS.Casinos.CasinosContext.Instance.PathMgr:getPersistentDataPath() .. self.PhotoFinalPath)
    self.ViewMgr:destroyView(self)
end

---------------------------------------
ViewTakePhotoFactory = ViewFactory:new()

---------------------------------------
function ViewTakePhotoFactory:new(o, ui_package_name, ui_component_name,
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

---------------------------------------
function ViewTakePhotoFactory:createView()
    local view = ViewTakePhoto:new(nil)
    return view
end