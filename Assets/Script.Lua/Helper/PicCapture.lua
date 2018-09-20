-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
PicCapture = {}

---------------------------------------
function PicCapture:new(o, view_mgr, listner)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil)
    then
        self.Instance = o
        self.ViewMgr = view_mgr
        self.Listner = listner
        self.CanShowTips = false
        self.CheckPicTm = 0
    end

    return self.Instance
end

---------------------------------------
function PicCapture:OnUpdate(tm)
    if self.CanShowTips then
        local c_t = self.CheckPicTm
        c_t = c_t + tm

        if c_t >= 0.5 then
            local f, e = io.open(self.CurrentPicName)
            if f ~= nil then
                f:close()
                if self.PicDownCallBack ~= nil then
                    self.PicDownCallBack()
                    self.PicDownCallBack = nil
                end
                self.CanShowTips = false
            end
            c_t = 0
        end
        self.CheckPicTm = c_t
    end
end

---------------------------------------
function PicCapture:CapturePic(pic_name, pic_down_callback)
    self.CanShowTips = true
    self.PicDownCallBack = pic_down_callback
    CS.UnityEngine.ScreenCapture.CaptureScreenshot(pic_name)
    self.CurrentPicName = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(pic_name)
end