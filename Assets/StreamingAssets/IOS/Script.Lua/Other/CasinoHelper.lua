-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
CasinoHelper = {}

---------------------------------------
function CasinoHelper:new(o,m_p,lan_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil)
    then
        self.Instance = o
        self.MP =m_p
        self.LanMgr = lan_mgr
    end

    return self.Instance
end

---------------------------------------
function CasinoHelper:TranslateFriendStateEx(friend_info)
    local friend_state = _eFriendStateClient.Offline
    local online_state = friend_info.PlayerInfoMore.OnlineState
    if (online_state == PlayerOnlineState.Offline)
    then
        friend_state = _eFriendStateClient.Offline
    elseif (online_state == PlayerOnlineState.Online)
    then
        local player_playstate = friend_info.PlayerPlayState
        if (player_playstate ~= nil and CS.System.String.IsNullOrEmpty(player_playstate.DesktopGuid) == false)
        then
            local module = player_playstate.CasinosModule
            if (module == CasinosModule.Fishing)
            then
                friend_state = _eFriendStateClient.Fishing
            elseif (module == CasinosModule.GFlower)
            then
                if (player_playstate.DesktopType == DesktopTypeEx.Desktop)
                then
                    friend_state = _eFriendStateClient.GFlowerDesktopNormal
                elseif (player_playstate.DesktopType == DesktopTypeEx.DesktopH)
                then
                    friend_state = _eFriendStateClient.GFlowerDesktopH
                end
            elseif (module == CasinosModule.Texas)
            then
                if (player_playstate.DesktopType == DesktopTypeEx.Desktop)
                then
                    if (CS.System.String.IsNullOrEmpty(player_playstate.UserData2) == false)
                    then
                        local desktop_filter1 = self.MP.unpack(player_playstate.UserData2)
                        local desktop_filter = DesktopFilter:new(nil)
                        desktop_filter:setData(desktop_filter1)
                        local desktop_f1 = self.MP.unpack(desktop_filter.FilterData)
                        local desktop_f = DesktopFilterTexas:new(nil)
                        desktop_f:setData(desktop_f1)
                        if (desktop_f.is_private)
                        then
                            friend_state = _eFriendStateClient.TexasDesktopClassicPrivate
                        else
                            friend_state = _eFriendStateClient.TexasDesktopClassic
                        end
                    end
                elseif (player_playstate.DesktopType == DesktopTypeEx.DesktopH)
                then
                    friend_state = _eFriendStateClient.TexasDesktopH
                elseif(player_playstate.DesktopType == DesktopTypeEx.DesktopMatch)
                then
                    friend_state = _eFriendStateClient.DesktopMatch
                end
            elseif (module == CasinosModule.ZhongFB)
            then
                friend_state = _eFriendStateClient.ZhongFBDesktopH
            elseif (module == CasinosModule.NiuNiu)
            then
                friend_state = _eFriendStateClient.NiuNiuDesktopH
            end
        else
            friend_state = _eFriendStateClient.NotInDesktop
        end
    end

    return friend_state
end

---------------------------------------
function CasinoHelper:TranslateFriendState(friend_state)
    local state = ""
    local lan_mgr = self.LanMgr
    if (friend_state == _eFriendStateClient.Offline)
    then
        state = lan_mgr:getLanValue("OffLine")
    elseif (friend_state == _eFriendStateClient.Fishing)
    then
        state = lan_mgr:getLanValue("Fishing")
    elseif (friend_state == _eFriendStateClient.GFlowerDesktopH)
    then
        state = lan_mgr:getLanValue("ZhaJinHuaDesktopH")
    elseif (friend_state == _eFriendStateClient.GFlowerDesktopPrivate)
    then
        state = lan_mgr:getLanValue("ZhaJinHuaUnknow")
    elseif (friend_state == _eFriendStateClient.GFlowerDesktopNormal)
    then
        state = lan_mgr:getLanValue("ZhaJinHuaClassic")
    elseif (friend_state == _eFriendStateClient.TexasDesktopClassicPrivate)
    then
        state = lan_mgr:getLanValue("TexasPokerUnknow")
    elseif (friend_state == _eFriendStateClient.TexasDesktopClassic)
    then
        state = lan_mgr:getLanValue("TexasPokerClassic")
    elseif (friend_state == _eFriendStateClient.TexasDesktopMustBet)
    then
        state = lan_mgr:getLanValue("TexasPokerMustBet")
    elseif (friend_state == _eFriendStateClient.TexasDesktopMustBetPrivate)
    then
        state = lan_mgr:getLanValue("TexasPokerUnknow")
    elseif (friend_state == _eFriendStateClient.TexasDesktopH)
    then
        state = lan_mgr:getLanValue("TexasPokerDesktopH")
    elseif (friend_state == _eFriendStateClient.NiuNiuDesktopH)
    then
        state = lan_mgr:getLanValue("NiuNiuDesktopH")
    elseif (friend_state == _eFriendStateClient.ZhongFBDesktopH)
    then
        state = lan_mgr:getLanValue("ZhongFaBaiDesktopH")
    elseif (friend_state == _eFriendStateClient.NotInDesktop)
    then
        state = lan_mgr:getLanValue("InLobby")
    elseif (friend_state == _eFriendStateClient.DesktopMatch)
    then
        state = lan_mgr:getLanValue("DesktopMatch")
    end

    return state
end