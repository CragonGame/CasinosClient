-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemRank = {}

---------------------------------------
function ItemRank:new(o, com, view_ranking)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Com = com
    o.ViewRanking = view_ranking
    o.Com.onClick:Add(
            function()
                o:onClickItem()
            end
    )
    o.GTextPlayerNickName = o.Com:GetChild("NickName").asTextField
    o.GTextPlayerGold = o.Com:GetChild("Gold").asTextField
    local co_ranksign = o.Com:GetChild("CoRankSign").asCom
    o.GLoaderRankSign = co_ranksign:GetChild("LoaderRankSign").asLoader
    o.GTextRankIndex = co_ranksign:GetChild("RankLevel").asTextField
    local co_headicon = o.Com:GetChild("CoHeadIcon").asCom
    o.ViewHeadIcon = ViewHeadIcon:new(nil, co_headicon)
    return o
end

---------------------------------------
function ItemRank:SetRankInfo(view_mgr, player_guid, nick_name, icon_name, account_id, value, vip_level, index, show_value_shortway, value_format)
    self.ViewMgr = view_mgr
    self.ShowValueShortWay = show_value_shortway
    self.PlayerGuid = player_guid
    local show_ranksign = false
    local rank_signname = ""
    if (index == 0) then
        show_ranksign = true
        rank_signname = self.ViewRanking.ChampionSignName
    elseif (index == 1) then
        show_ranksign = true
        rank_signname = self.ViewRanking.SecondPlaceSignName
    elseif (index == 2) then
        show_ranksign = true
        rank_signname = self.ViewRanking.ThirdPlaceSignName
    end
    self.GLoaderRankSign.visible = show_ranksign
    if (show_ranksign) then
        self.GTextRankIndex.visible = false
    else
        self.GTextRankIndex.visible = true
    end

    if (show_ranksign) then
        self.GLoaderRankSign.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath("Common", rank_signname)
        self.GTextRankIndex.text = ""
    else
        self.GTextRankIndex.text = tostring(index + 1)
    end
    self.ViewHeadIcon:SetPlayerInfo(icon_name, account_id, vip_level)

    self.GTextPlayerNickName.text = nick_name
    local g_t = UiChipShowHelper:GetGoldShowStr(value, self.ViewMgr.LanMgr.LanBase, self.ShowValueShortWay)
    if value_format ~= nil then
        g_t = string.format("%s%s", g_t, value_format)
    end
    self.GTextPlayerGold.text = g_t
end

---------------------------------------
function ItemRank:onClickItem()
    local profile = self.ViewMgr:CreateView("PlayerProfile")
    profile:RequestRefreshByPlayerGuid(CS.Casinos._ePlayerProfileType.Ranking, self.PlayerGuid,
            function(player_info, headicon_texture)
                self:RefreshPlayerInfo(player_info, headicon_texture)
            end
    )
end

---------------------------------------
function ItemRank:RefreshPlayerInfo(player_info, headicon_texture)
    self.GTextPlayerNickName.text = player_info.PlayerInfoCommon.NickName
    self.GTextPlayerGold.text = UiChipShowHelper:GetGoldShowStr(player_info.PlayerInfoMore.Gold, self.ViewMgr.LanMgr.LanBase, self.ShowValueShortWay)
    self.ViewHeadIcon.GLoaderPlayerIcon.texture:Reload(headicon_texture)
end