BgAttachMode = {
    Top = 1,
    Center = 2,
    Bottom = 3,
}

ViewHelper = {
    Instance = nil,
    ListShootingTextColor = {
        [0] = "0000FF",
        [1] = "FFFF00",
        [2] = "00FFFF",
        [3] = "00FF00",
        [4] = "FF00FF",
        [5] = "FFFFFF",
    },
    ViewMgr = nil
}

function ViewHelper:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil)
    then
        self.Instance = o
        self.ViewMgr = ViewMgr:new(nil)
    end

    return self.Instance
end

function ViewHelper.UiShowInfoSuccess(table, info)
    return table:UiShowInfoSuccess(info)
end

function ViewHelper:UiShowInfoSuccess(info)
    local ui_floatmsg = self.ViewMgr:getView("FloatMsg")
    if (ui_floatmsg == nil)
    then
        ui_floatmsg = self.ViewMgr:createView("FloatMsg")
    end
    ui_floatmsg:showInfo(info, CS.UnityEngine.Color.white)
    return ui_floatmsg
end

function ViewHelper.UiShowInfoFailed(table, info)
    return table:UiShowInfoFailed(info)
end

function ViewHelper:UiShowInfoFailed(info)
    local ui_floatmsg = self.ViewMgr:getView("FloatMsg")
    if (ui_floatmsg == nil)
    then
        ui_floatmsg = self.ViewMgr:createView("FloatMsg")
    end

    ui_floatmsg:showInfo(info, CS.UnityEngine.Color.red)

    return ui_floatmsg
end

function ViewHelper.UiShowPermanentPosMsg(table, info)
    return table:UiShowPermanentPosMsg(info)
end

function ViewHelper:UiShowPermanentPosMsg(info)
    local ui_msg = self.ViewMgr:getView("PermanentPosMsg")
    if (ui_msg == nil)
    then
        ui_msg = self.ViewMgr:createView("PermanentPosMsg")
    end
    ui_msg:showInfo(info)

    return ui_msg
end

function ViewHelper:UiShowMsgBox(info, ok, cancel)
    local ui = self.ViewMgr:createView("MsgBox")
    ui:showMsgBox3(info, ok, cancel)

    return ui
end

function ViewHelper:UiShowMsgBox(info, ok)
    local ui = self.ViewMgr:createView("MsgBox")
    ui:showMsgBox4(info, ok)

    return ui
end

function ViewHelper.UiBeginWaiting(table, tips, auto_destroytm)
    return table:UiBeginWaiting(tips, auto_destroytm)
end

function ViewHelper:UiBeginWaiting(tips, auto_destroytm)
    if (auto_destroytm == nil)
    then
        auto_destroytm = 5
    end
    local ui_waiting = self.ViewMgr:getView("Waiting")
    if (ui_waiting == nil)
    then
        ui_waiting = self.ViewMgr:createView("Waiting")
    end
    ui_waiting:setTips(tips, auto_destroytm)

    return ui_waiting
end

function ViewHelper.UiEndWaiting(table)
    return table:UiEndWaiting()
end

function ViewHelper:UiEndWaiting()
    local ui_waiting = self.ViewMgr:getView("Waiting")
    self.ViewMgr:destroyView(ui_waiting)
end

function ViewHelper:UiShowLoading(tips, progress)
    local ui_loading = self.ViewMgr:getView("Loading")
    if (ui_loading == nil)
    then
        ui_loading = self.ViewMgr:createView("Loading")
    end

    ui_loading:setTip(tips)
    ui_loading:setLoadingProgress(progress)

    return ui_loading
end

function ViewHelper:UiEndLoading()
    local ui_loading = self.ViewMgr:getView("Loading")
    self.ViewMgr:destroyView(ui_loading)
end

function ViewHelper:FormatePackageImagePath(package_name, image_name)
    local l = string.format("ui://%s/%s", package_name, image_name)
    return l
end

function ViewHelper:setGObjectVisible(is_visible, obj)
    if (obj.visible ~= is_visible)
    then
        obj.visible = is_visible
    end
end

function ViewHelper:GetRandomShootingTextColor()
    local index = math.random(0, 5)
    return self.ListShootingTextColor[index]
end

function ViewHelper:subStrToTargetLength(str, target_length)
    if (string.len(str) > target_length)
    then
        str = string.sub(str, 0, target_length)
    end

    return str
end

function ViewHelper:makeUiBgFiteScreen(design_width, design_height, logic_width, logic_height, image_width, image_height, obj, anchor_mode,t_anchor_point)
    local w = logic_width / design_width
    local h = logic_height / design_height
    if (w >= h)
    then
        obj.width = logic_width
        obj.height = logic_width * image_height / image_width
        obj.x = 0
        local y = 0
        if anchor_mode == BgAttachMode.Top then
            y = 0
            if t_anchor_point ~= nil then
                for i, v in pairs(t_anchor_point) do
                    local p = v.xy
                    local p_y = p.y
                    local p_p = CS.UnityEngine.Vector2()
                    p_p.y = p_y - p_y/2 * w
                    p_p.x = p.x
                    v.xy = p_p
                end
            end
        elseif anchor_mode == BgAttachMode.Center then
            y = (logic_height - obj.height) / 2
            if t_anchor_point ~= nil then
                for i, v in pairs(t_anchor_point) do
                    local p = v.xy
                    local p_y = p.y
                    local p_p = CS.UnityEngine.Vector2()
                    p_p.y = p_y + p_y / 2 * (w-h) / 2
                    p_p.x = p.x
                    v.xy = p_p
                end
            end
        elseif anchor_mode == BgAttachMode.Bottom then
            y = logic_height - obj.height
            if t_anchor_point ~= nil then
                for i, v in pairs(t_anchor_point) do
                    local p = v.xy
                    local p_y = p.y
                    local p_p = CS.UnityEngine.Vector2()
                    p_p.y = p_y + p_y/2 * w
                    p_p.x = p.x
                    v.xy = p_p
                end
            end
        end
        obj.y = y
    else
        obj.height = logic_height
        obj.width = logic_height * image_width / image_height
        obj.x = (logic_width - obj.width) / 2
        obj.y = 0
        if t_anchor_point ~= nil then
            for i, v in pairs(t_anchor_point) do
                local p = v.xy
                local p_y = p.y
                local p_p = CS.UnityEngine.Vector2()
                p_p.y = p_y + p_y / 2 * (h-w) / 2
                p_p.x = p.x
                v.xy = p_p
            end
        end
    end
end

function ViewHelper:getABParticleResourceTitlePath()
    return CS.Casinos.CasinosContext.Instance.ABResourcePathTitle .. "Particle/"
end

function ViewHelper:getABItemResourceTitlePath()
    return CS.Casinos.CasinosContext.Instance.ABResourcePathTitle .. "Item/"
end

function ViewHelper:getABAudioResourceTitlePath()
    return CS.Casinos.CasinosContext.Instance.ABResourcePathTitle .. "Audio/"
end

function ViewHelper:PopUi(comui, title)
    local proportion = 0.85
    local tween_time = 0.3
    comui:SetPivot(0.5, 0.5)
    local scale = comui.scale
    comui:SetScale(scale.x * proportion, scale.y * proportion)
    local com_bg = comui:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade:SetPivot(0.5, 0.5)
    com_shade:SetScale(scale.x * (1 / proportion), scale.y * (1 / proportion))
    comui:TweenScale(scale, tween_time):SetEase(CS.DG.Tweening.Ease.OutBack)
    if (title ~= nil)
    then
        local text_title = comui:GetChild("TextTitle").asTextField
        self:SetUiTitle(text_title, title)
    end
end

function ViewHelper:SetUiTitle(text_title, title)
    local temp = {}
    temp[1] = "[color=#FFFFFF,#B8D1F6]"
    temp[2] = title
    temp[3] = "[/color]"
    text_title.text = table.concat(temp)
end
