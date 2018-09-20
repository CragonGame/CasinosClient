ViewTexasClassic = ViewDesktopTypeBase:new()

function ViewTexasClassic:new(o,view_desktop)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    o.ViewDesktop = view_desktop
    o.DesktopType = "Classic"
    o.GBtnMenu = o.ViewDesktop.ComUi:GetChild("BtnMenu").asButton
    o.GBtnMenu.onClick:Add(
            function()
                o:_onClickMenu()
            end
    )
    local com_desktop = view_desktop.ComUi
    local loader_bg = com_desktop:GetChild("LoaderBg").asLoader
    --local bg_path = CS.Casinos.CasinosContext.Instance:AppendStrWithSB(CS.Casinos.CasinosContext.Instance.ABResourcePathTitle,
    --        "DesktopImage/classicbg.ab")
    loader_bg.icon = CS.FairyGUI.UIPackage.GetItemURL("Desktop","ClassicBg") --CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(bg_path)
    ViewHelper:makeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH,ViewMgr.STANDARD_HEIGHT, o.ViewDesktop.ComUi.width, o.ViewDesktop.ComUi.height, loader_bg.width, loader_bg.height,loader_bg,BgAttachMode.Center)
    local loader_desk = com_desktop:GetChild("LoaderDesk").asLoader
    --local desk_path = CS.Casinos.CasinosContext.Instance:AppendStrWithSB(CS.Casinos.CasinosContext.Instance.ABResourcePathTitle,
    --        "DesktopImage/classicdesk.ab")
    loader_desk.icon = CS.FairyGUI.UIPackage.GetItemURL("Desktop","ClassicDesk")--CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(desk_path)
    local btn_shop = com_desktop:GetChild("BtnShop").asButton
    ViewHelper:setGObjectVisible(true,btn_shop)
    btn_shop.onClick:Add(
            function()
                o:_onClickShop()
            end
    )
    local com_lottryticket = com_desktop:GetChild("ComLotteryTicket")
    ViewHelper:setGObjectVisible(true,com_lottryticket)

    return o
end

function ViewTexasClassic:onHandleEv(ev)

end

function ViewTexasClassic:setSnapShot(snapshot,is_init)
    self.Ante = snapshot.normal_texas.Ante
    if is_init == true then
        local hide_chair = false
        for k, v in pairs(self.ViewDesktop.MapAllUiChairInfo) do
            local chair_info = v
            if (self.ViewDesktop.Desktop.SeatNum == TexasDesktopSeatNum.Five)
            then
                local a = (k + 3) % 2
                if (a == 0)
                then
                    chair_info.GComChair.visible = false
                    chair_info.GComSitOrInvite.visible = false
                    hide_chair = true
                else
                    hide_chair = false
                end
            end

            if (hide_chair == false)
            then
                chair_info.GComSitOrInvite.onClick:Add(
                        function(context)
                            self.ViewDesktop:_onClickChair(context)
                        end
                )
                self.ViewDesktop.MapAllValidPlayerSeat[k] = v
                self.ViewDesktop.MapValidNoPlayerSeat[k] = chair_info
            end
        end
        self:_checkSeat()
    end
end

function ViewTexasClassic:preflopBegin()

end

function ViewTexasClassic:_checkSeat()
    for k, v in pairs(self.ViewDesktop.MapAllValidPlayerSeat)
    do
        local seat = self.ViewDesktop.MapValidNoPlayerSeat[k]
        if (seat == nil)
        then
            if (v ~= nil)
            then
                CS.Casinos.UiHelper.setGObjectVisible(false, v.GComChair, v.GImagePlayerInvite, v.GImagePlayerSit)
            end
        end
        for k, v in pairs(self.ViewDesktop.MapValidNoPlayerSeat)
        do
            local chair_info = v
            if (chair_info ~= nil)
            then
                if (self.ViewDesktop:_meIsSeat() == false)
                then
                    ViewHelper:setGObjectVisible(false, chair_info.GImagePlayerInvite)
                    ViewHelper:setGObjectVisible(true, chair_info.GComChair)
                    ViewHelper:setGObjectVisible(true, chair_info.GImagePlayerSit)
                else
                    ViewHelper:setGObjectVisible(true, chair_info.GComChair)
                    ViewHelper:setGObjectVisible(true, chair_info.GImagePlayerInvite)
                    ViewHelper:setGObjectVisible(false, chair_info.GImagePlayerSit)
                end
            end
        end
    end
end

function ViewTexasClassic:_onClickMenu()
    local desk_menu = self.ViewDesktop.ViewMgr:createView("DesktopMenuTexas")
    desk_menu:setPlayerState(self.ViewDesktop.Desktop.MeP.PlayerDataDesktop.DesktopPlayerState == TexasDesktopPlayerState.Ob,
            self.ViewDesktop.Desktop.MeP.PlayerDataDesktop.DesktopPlayerState == TexasDesktopPlayerState.WaitWhile,
            self.ViewDesktop.CanGetOnLineReward or self.ViewDesktop.CanGetTimingReward)
end

function ViewTexasClassic:_onClickShop()
    local ev = self.ViewDesktop.ViewMgr:getEv("EvUiClickShop")
    if (ev == nil)
    then
        ev = EvUiClickShop:new(nil)
    end
    self.ViewDesktop.ViewMgr:sendEv(ev)
end

ViewTexasClassicTypeFactory = {}

function ViewTexasClassicTypeFactory:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self

    return o
end

function ViewTexasClassicTypeFactory:GetName()
    return "TexasClassic"
end

function ViewTexasClassicTypeFactory:CreateViewDesktopType(view_desktop)
    local l = ViewTexasClassic:new(nil, view_desktop)
    return l
end