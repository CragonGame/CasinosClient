-- Copyright(c) Cragon. All rights reserved.
-- 普通桌屏蔽聊天界面中的一个头像

ItemPlayerChatLock = { SystemName = "发牌员" }

function ItemPlayerChatLock:new(o, com, view_mgr, self_guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Com = com
    local com_headicon = o.Com:GetChild("CoHeadIcon").asCom
    o.ViewHeadIcon = ViewHeadIcon:new(nil, com_headicon)
    o.GTextName = o.Com:GetChild("TextName").asTextField
    o.Com.onClick:Add(
            function()
                o:onClickBtnLock()
            end
    )
    o.GLoadLock = o.Com:GetChild("LoadLock").asLoader
    o.GImageDealer = o.Com:GetChild("Dealer").asImage
    o.GImageDealer.visible = false
    o.ViewMgr = view_mgr
    o.SelfGuid = self_guid

    return o
end

function ItemPlayerChatLock:setPlayerChatInfo(et_guid, icon, name, account_id, vip_level, is_lock, is_system)
    self.PlayerEtguid = et_guid
    self.IsLock = is_lock
    self.IsSystem = is_system
    if ((self.PlayerEtguid ~= nil and string.len(self.PlayerEtguid) > 0) and (self.PlayerEtguid ~= self.SelfGuid))
    then
        self.GTextName.text = name
        self:setLockIcon()
        self.ViewHeadIcon:setPlayerInfo(icon, account_id, vip_level)
    else
        if (self.IsSystem)
        then
            self.GImageDealer.visible = true
            self.GTextName.text = self.ViewMgr.LanMgr:getLanValue("TheDealer")
            self:setLockIcon()
        else
            self.ViewHeadIcon:hideIcon()
        end
    end
end

function ItemPlayerChatLock:setChatLock(is_lock)
    self.IsLock = is_lock
    if (self.IsSystem or (self.PlayerEtguid ~= nil and string.len(self.PlayerEtguid) > 0))
    then
        local lock_name = "TextureUnLock"
        if (self.IsLock)
        then
            lock_name = "TextureLocked"
        end
        self.GLoadLock.url = CS.FairyGUI.UIPackage.GetItemURL("LockChat", lock_name)
    end
end

function ItemPlayerChatLock:setLockIcon()
    local lock_name = "TextureUnLock"
    if (self.IsLock)
    then
        lock_name = "TextureLocked"
    end
    self.GLoadLock.url = CS.FairyGUI.UIPackage.GetItemURL("LockChat", lock_name)
end

function ItemPlayerChatLock:onClickBtnLock()
    if (self.IsLock)
    then
        self.IsLock = false
    else
        self.IsLock = true
    end
    if (self.IsSystem)
    then
        local ev = self.ViewMgr:getEv("EvUiRequestLockSystemChat")
        if (ev == nil)
        then
            ev = EvUiRequestLockSystemChat:new(nil)
        end
        ev.requestLock = self.IsLock
        self.ViewMgr:sendEv(ev)
    else
        local ev = self.ViewMgr:getEv("EvUiRequestLockPlayerChat")
        if (ev == nil)
        then
            ev = EvUiRequestLockPlayerChat:new(nil)
        end
        ev.player_guid = self.PlayerEtguid
        ev.requestLock = self.IsLock
        self.ViewMgr:sendEv(ev)
    end
    self:setLockIcon()
end