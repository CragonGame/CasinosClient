-- Copyright(c) Cragon. All rights reserved.
-- 相册&拍照对话框

---------------------------------------
ViewTakePhoto = class(ViewBase)

---------------------------------------
function ViewTakePhoto:ctor()
    self.Tween = nil
    self.PhotoFinalPath = "/photos"
    self.GetPicDefaultName = "GetPicDefaultName"
    self.mPhotoSize = 640
end

---------------------------------------
function ViewTakePhoto:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi)
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self.ViewMgr:DestroyView(self)
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
function ViewTakePhoto:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewTakePhoto:onClickTakePhoto()
    ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("GetImage"))
    CS.NativeTakePhoto.takeNewPhoto(self.mPhotoSize, self.mPhotoSize, self.GetPicDefaultName .. ".jpg"
    , CS.Casinos.CasinosContext.Instance.PathMgr:GetPersistentDataPath() .. self.PhotoFinalPath)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewTakePhoto:onClickTakePic()
    ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("GetImage"))
    CS.NativeTakePhoto.takeExistPhoto(self.mPhotoSize, self.mPhotoSize, self.GetPicDefaultName .. ".jpg"
    , CS.Casinos.CasinosContext.Instance.PathMgr:GetPersistentDataPath() .. self.PhotoFinalPath)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewTakePhotoFactory = class(ViewFactory)

---------------------------------------
function ViewTakePhotoFactory:CreateView()
    local view = ViewTakePhoto:new()
    return view
end