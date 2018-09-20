ItemRank = {}

function ItemRank:new(o,com)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	o.Com = com
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
    o.ViewHeadIcon = ViewHeadIcon:new(nil,co_headicon)
	return o
end

function ItemRank:setRankInfo(view_mgr,player_guid,nick_name,icon_name,account_id,value,vip_level,index,show_value_shortway)
	self.ViewMgr = view_mgr
	self.ShowValueShortWay = show_value_shortway
    self.PlayerEtguid = player_guid
    local show_ranksign = false
    local rank_signname = ""
	if(index == 0)
	then
		show_ranksign = true
        rank_signname = ViewRanking.ChampionSignName
	elseif(index == 1)
	then
		show_ranksign = true
        rank_signname = ViewRanking.SecondPlaceSignName
	elseif(index == 2)
	then
		show_ranksign = true
        rank_signname = ViewRanking.ThirdPlaceSignName
	end
    self.GLoaderRankSign.visible = show_ranksign
	if(show_ranksign)
	then
		self.GTextRankIndex.visible = false
	else
		self.GTextRankIndex.visible = true
	end

    if (show_ranksign)
	then
		self.GLoaderRankSign.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath("Common", rank_signname)
        self.GTextRankIndex.text = ""
	else
		self.GTextRankIndex.text = tostring(index + 1)
	end
    self.ViewHeadIcon:setPlayerInfo(icon_name, account_id, vip_level)

    self.GTextPlayerNickName.text = nick_name
    self.GTextPlayerGold.text = UiChipShowHelper:getGoldShowStr(value,self.ViewMgr.LanMgr.LanBase, self.ShowValueShortWay)
end

function ItemRank:onClickItem()
	local profile = self.ViewMgr:createView("PlayerProfile")
    profile:setPlayerGuid(CS.Casinos._ePlayerProfileType.Ranking, self.PlayerEtguid,
		function(a,b)
			self:playerInfo(a,b)
		end
	)
end

function ItemRank:playerInfo(player_info,head_icon)
	self.GTextPlayerNickName.text = player_info.PlayerInfoCommon.NickName
    self.GTextPlayerGold.text = UiChipShowHelper:getGoldShowStr(player_info.PlayerInfoMore.Gold,self.ViewMgr.LanMgr.LanBase, self.ShowValueShortWay)
	self.ViewHeadIcon.GLoaderPlayerIcon.texture.nativeTexture = head_icon
end


