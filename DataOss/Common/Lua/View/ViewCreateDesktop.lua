-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewCreateDesktop = class(ViewBase)

---------------------------------------
function ViewCreateDesktop:ctor()
    self.Tween = nil
end

---------------------------------------
function ViewCreateDesktop:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("CreatPrivateDesk"))
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    local com = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickClose()
            end
    )
    local btn_create = self.ComUi:GetChild("Lan_Btn_Create").asButton
    btn_create.onClick:Add(
            function()
                self:onClickBtnCreate()
            end
    )
    self.mTextBet = self.ComUi:GetChild("TextBet").asTextField
    self.mTextStake = self.ComUi:GetChild("TextStake").asTextField
    --self.mTextSpeed = self.ComUi:GetChild("TextSpeed").asTextField
    self.ListAnte = self.ComUi:GetChild("ListAnte").asList
    self.TIndexAnte = {}
end

---------------------------------------
function ViewCreateDesktop:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewCreateDesktop:setCreateInfo(desk_topinfo_id, map_params)
    self.mDeskTopId = desk_topinfo_id
    self.mSeatNum = map_params[0]
    self.mIsNormalSpeed = map_params[1]
    local tb_deskinfo = self.CasinosContext.TbDataMgrLua:GetData("DesktopInfoTexas", desk_topinfo_id)
    local tips = self.ViewMgr.LanMgr:getLanValue("Bet")
    local bet_str = self.CasinosContext:AppendStrWithSB(tips, ": ", UiChipShowHelper:GetGoldShowStr(tb_deskinfo.SmallBlind, self.ViewMgr.LanMgr.LanBase)
    , "/", UiChipShowHelper:GetGoldShowStr(tb_deskinfo.BigBlind, self.ViewMgr.LanMgr.LanBase))
    self.mTextBet.text = bet_str
    local seat_numstr = ""
    if (self.mSeatNum == TexasDesktopSeatNum.Unlimited) then
        seat_numstr = self.ViewMgr.LanMgr:getLanValue("Unlimited")
    elseif (self.mSeatNum == TexasDesktopSeatNum.Nine) then
        seat_numstr = "9"
    elseif (self.mSeatNum == TexasDesktopSeatNum.Five) then
        seat_numstr = "5"
    end
    self.mTextStake.text = self.CasinosContext:AppendStrWithSB(self.ViewMgr.LanMgr:getLanValue("Seat"), ": ", seat_numstr)
    for i, v in pairs(tb_deskinfo.Ante) do
        local item = self.TIndexAnte[i]
        if item == nil then
            local com = CS.FairyGUI.UIPackage.CreateObject("CreateDeskTop", "BtnAnte").asCom
            self.ListAnte:AddChild(com)
            local i_a = ItemAnte:new(nil)
            i_a:setAnte(com, self.ViewMgr.LanMgr, tonumber(v))
            self.TIndexAnte[i] = i_a
        end
    end
    self.ListAnte.selectedIndex = 0
end

---------------------------------------
function ViewCreateDesktop:onClickClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewCreateDesktop:onClickBtnCreate()
    local i = self.TIndexAnte[self.ListAnte.selectedIndex + 1]
    ViewHelper:UiBeginWaiting()
    local create_infotexas = PrivateDesktopCreateInfoTexas:new(nil)
    create_infotexas.desktop_tableid = self.mDeskTopId
    create_infotexas.seat_num = self.mSeatNum
    create_infotexas.speed = self.mIsNormalSpeed
    create_infotexas.is_vip = false
    create_infotexas.ante = i.Ante
    print(self.ListAnte.selectedIndex, i.Ante)
    local create_info = PrivateDesktopCreateInfo:new(nil)
    create_info.FactoryName = "Texas"
    create_info.Data = self.ViewMgr:PackData(create_infotexas:getData4Pack())
    local ev = self.ViewMgr:GetEv("EvUiClickCreateDeskTop")
    if (ev == nil) then
        ev = EvUiClickCreateDeskTop:new(nil)
    end
    ev.create_info = create_info
    self.ViewMgr:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewCreateDesktopFactory = class(ViewFactory)

---------------------------------------
function ViewCreateDesktopFactory:CreateView()
    local view = ViewCreateDesktop:new()
    return view
end