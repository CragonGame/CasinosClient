ItemLobbyDeskPlayInfo = {}

function ItemLobbyDeskPlayInfo:new(o,item,playerInfo,lan_mgr)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    o.Item = CS.Casinos.LuaHelper.GObjectCastToGCom(item)
    o.Item.visible = true
    o.GImageBg = o.Item:GetChild("ImageBg").asImage
    o.GComHeadIcon = o.Item:GetChild("ComHeadIcon").asCom
    o.GTextChipAmount = o.Item:GetChild("TextChipAmount").asTextField
    o.GTextPlayerName = o.Item:GetChild("TextName").asTextField
    o.ViewHeadIcon = ViewHeadIcon:new(nil,o.GComHeadIcon)
    o.ViewHeadIcon:setPlayerInfo(playerInfo.icon, playerInfo.account_id, 0)
    o.GTextPlayerName.text = playerInfo.nick_name
    o.GTextChipAmount.text = UiChipShowHelper:getGoldShowStr(playerInfo.chip,lan_mgr.LanBase)
    return o
end

function ItemLobbyDeskPlayInfo:HideHeadIcon()
    self.GComHeadIcon:TweenScale(CS.Casinos.LuaHelper.GetVector2(0,0), 0.25)
    self.GTextChipAmount.visible = false
    self.GTextPlayerName.visible = false
end

function ItemLobbyDeskPlayInfo:ShowHeadIconUseTween()
    self.GComHeadIcon:TweenScale(CS.Casinos.LuaHelper.GetVector2(1,1), 0.25)
    self.GTextChipAmount.visible = true
    self.GTextPlayerName.visible = true
end

function ItemLobbyDeskPlayInfo:reset()
    self.Item.visible = false
end

