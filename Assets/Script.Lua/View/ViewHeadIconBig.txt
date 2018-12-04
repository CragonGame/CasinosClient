-- Copyright(c) Cragon. All rights reserved.
-- 全屏头像

---------------------------------------
ViewHeadIconBig = class(ViewBase)

---------------------------------------
function ViewHeadIconBig:ctor()
end

---------------------------------------
function ViewHeadIconBig:OnCreate()
    self.GLoaderPlayerIcon = self.ComUi:GetChild("LoaderIcon").asLoader
    self.ComUi.onClick:Add(
            function()
                self.ViewMgr:DestroyView(self)
            end
    )
end

---------------------------------------
function ViewHeadIconBig:SetIcon(icon)
    if icon ~= nil then
        self.GLoaderPlayerIcon.visible = true
        self.GLoaderPlayerIcon.icon = icon
    end
end

---------------------------------------
function ViewHeadIconBig:SetIcon(texture)
    if texture ~= nil then
        self.GLoaderPlayerIcon.visible = true
        self.GLoaderPlayerIcon.texture = CS.FairyGUI.NTexture(texture)
    end
end

---------------------------------------
ViewHeadIconBigFactory = class(ViewFactory)

---------------------------------------
function ViewHeadIconBigFactory:CreateView()
    local view = ViewHeadIconBig:new()
    return view
end