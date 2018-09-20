ViewDesktopHTexas = {}

function ViewDesktopHTexas:new(o,ui_desktoph)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	                        
		 o.ViewDesktopH = ui_desktoph
            o.GTextGameTips = o.ViewDesktopH.GCotips:GetChild("GameTips").asTextField
            o.MapBetOperate = {}
            o.ListBetOperate = {}
			o.DesktopHState = nil
        o.Sound  = ""
    self.PackageName = "DesktopHTexas"
    self.BgMusic = "DesktopHTexasBg"
    self.TurnCardSound = "desk_new_card"
    self.SeatCount = 6
            o:_setBetOperate()
    return o
end

function ViewDesktopHTexas:updateGameState(game_state, total_tm, max_canbet,map_userdata,is_screenshot)        
            self.DesktopHState = game_state

			if(game_state == _eDesktopHState.Idle)
			then
				self:idleState(is_screenshot)
			elseif(game_state == _eDesktopHState.Ready)
			then
				self:readyState(is_screenshot, map_userdata)
			elseif(game_state == _eDesktopHState.Bet)
			then
				self:betState(is_screenshot)
			elseif(game_state == _eDesktopHState.GameEnd)
			then
				self:gameEnd(is_screenshot)
			elseif(game_state == _eDesktopHState.Rest)
			then
				self:rest(is_screenshot)
			end           
end

function ViewDesktopHTexas:idleState(is_screenshot)
            if (is_screenshot)
            then	
                self.ViewDesktopH:dealCardAtPos(5, 0.4)
            end
end

function ViewDesktopHTexas:readyState(is_screenshot,map_userdata)        
            if (is_screenshot == false)
            then
                self.ViewDesktopH:dealCard(5, 0.4, map_userdata, false)            
            else
                self.ViewDesktopH:dealCardAtPos(5, 0.4)
            end
end

function ViewDesktopHTexas:betState(is_screenshot)
            if (is_screenshot)
            then
                self.ViewDesktopH:dealCardAtPos(5, 0.4)
            end
end

function ViewDesktopHTexas:gameEnd(is_screenshot)
            if (is_screenshot)
            then
                self.ViewDesktopH:dealCardAtPos(5, 0.4)
            end
            self.ViewDesktopH:showCard()
end

function ViewDesktopHTexas:rest(is_screenshot)
end

function ViewDesktopHTexas:getGoldOperateList()
            return self.ListBetOperate
end

function ViewDesktopHTexas:updateBetOperate()        
            self:_setBetOperate()
end

function ViewDesktopHTexas:getGoldOperateMap()        
            return self.MapBetOperate
end

function ViewDesktopHTexas:getCardTypeAndSoundPath(cardtype_name, is_win)
			local audio_path = "DeskTopH" .. cardtype_name
            if (is_win)
            then
                cardtype_name =cardtype_name .. "Win"            
            else            
                cardtype_name =cardtype_name .. "Loose"
            end
            local pack_name = self.PackageName
            if (CS.Casinos.CasinosContext.Instance.UseLan)
            then
                pack_name = self.ViewDesktopH.ViewMgr.LanMgr:getLanPackageName()
            end
            local  path = CS.FairyGUI.UIPackage.GetItemURL(pack_name, cardtype_name)
            local cardtype_info = _tCardTypeInfo:new(nil,path,audio_path)

            return cardtype_info
end

function ViewDesktopHTexas:getBeBankPlayerMinGolds()
	local tb_mgr = TbDataMgr:new(nil)
	local tb_desktopinfo = tb_mgr:GetData("DesktopHInfoTexas",1)
    local golds_info =_tBeBankGoldsInfo:new(nil,tb_desktopinfo.MinTakeGolds,tb_desktopinfo.MinLeaveGolds)
    return golds_info
end

function ViewDesktopHTexas:getSeatDownMinGolds()
	local tb_mgr = TbDataMgr:new(nil)
	local tb_desktopinfo = tb_mgr:GetData("DesktopHInfoTexas",2)
    return tb_desktopinfo.MinTakeGolds
end

function ViewDesktopHTexas:getCardType(list_card)        
            local rank_type = CS.Casinos.CardTypeHelperTexas.GetHandRankHTexas(list_card)
            local l =self:getCardTypeDes(rank_type)
            return l
end

function ViewDesktopHTexas:getCardTypeDes(card_type)
            local rank_t = self.ViewDesktopH.ViewMgr.LanMgr:getLanValue(card_type)
            return rank_t
end

function ViewDesktopHTexas:getAllCardType()        
            local map_allcardtype = {}
            for i = 0,9 do
                map_allcardtype[i] = self:getCardTypeDes(CS.Casinos.HandRankTypeTexasH.__CastFrom(i):ToString())
			end

            return map_allcardtype
end

function ViewDesktopHTexas:getCardResName(card_data)        
            local card_res_name = card_data.suit .. "_" .. card_data.type
            return card_res_name
end

function ViewDesktopHTexas:getBgMusic()        
            return self.BgMusic
end

function ViewDesktopHTexas:getTurnCardSound()
            return self.TurnCardSound
end

function ViewDesktopHTexas:getSeatCount()        
            return self.SeatCount
end

function ViewDesktopHTexas:getBetPotSign(betpot_index)
            return "Pot" .. betpot_index
end

function ViewDesktopHTexas:getShaiZhong()        
            return nil
end

function ViewDesktopHTexas:setRewardPotPlayerInfo(total_info)
            local co_betpot_playerinfo = CS.FairyGUI.UIPackage.CreateObject(self.ViewDesktopH:getDesktopBasePackageName(),
             self.ViewDesktopH.UiDesktopHComDesktopHRewardPotPlayerInfoTitle .. self.ViewDesktopH.FactoryName).asCom

            return co_betpot_playerinfo
end

function ViewDesktopHTexas:betPotIsWin(bet_pot)
end

function ViewDesktopHTexas:betPotIsReset(bet_pot)
end

function ViewDesktopHTexas:setCountDown(current_tm)
            if (self.DesktopHState == _eDesktopHState.Ready)
            then
                self.ViewDesktopH.GCotips.visible = true
                self.GTextGameTips.text = string.format("%s  %s",self.ViewDesktopH.ViewMgr.LanMgr:getLanValue("ReadyTime"), current_tm)

                local sound = ""
				if(current_tm == 3 or current_tm == 2 or current_tm == 1)
				then
					sound = "desktophcountdowntip"
				elseif(current_tm == 0)
				then
					sound = "desktophcountdownend"
				end
                if (CS.System.String.IsNullOrEmpty(sound) == false and sound ~= self.Sound)
                then
                    self.Sound = sound
                    CS.Casinos.CasinosContext.Instance:play(sound, CS.Casinos._eSoundLayer.LayerNormal)
                end
            elseif (self.DesktopHState == _eDesktopHState.Bet)
            then
                self.ViewDesktopH.GCotips.visible = true
                self.GTextGameTips.text =  string.format("%s  %s", self.ViewDesktopH.ViewMgr.LanMgr:getLanValue("BetRestTime"), current_tm)
            elseif (self.DesktopHState == _eDesktopHState.Rest)
            then
                self.ViewDesktopH.GCotips.visible = true
                self.GTextGameTips.text = string.format("%s  %s", self.ViewDesktopH.ViewMgr.LanMgr:getLanValue("BreathSpace"), current_tm)
            elseif (self.DesktopHState == _eDesktopHState.GameEnd)
            then
                self.ViewDesktopH.GCotips.visible = true
                self.GTextGameTips.text = string.format("%s  %s", self.ViewDesktopH.ViewMgr.LanMgr:getLanValue("SettlementTime"), current_tm)
            end
end

function ViewDesktopHTexas:setPlayerCurrentBetInfo(current_bet)
end

function ViewDesktopHTexas:getStandPlayerChatName()        
            return "CoChatRightTop"
end

function ViewDesktopHTexas:getSysBankPlayerInitGold()
	local tb_mgr = TbDataMgr:new(nil)
	local tb_sysbank = tb_mgr:GetData("CfigTexasDesktopHSysBank",1)    
    return tb_sysbank.Golds * 2
end

function ViewDesktopHTexas:getTongPeiWhenAllShowEndShowGameResultTm()
            return 4.2
end

function ViewDesktopHTexas:getTongPeiImageName()
            return "Lan_Image_TongPei"
end

function ViewDesktopHTexas:_setBetOperate()
	local tb_mgr = TbDataMgr:new(nil)
	local map_operate = tb_mgr:GetMapData("DesktopHBetOperateTexas")            
    --[[self.ListBetOperate = map_operate.OrderByDescending(x => ((TbDataDesktopHBetOperateTexas)x.Value).OperateGolds).
        Select(x => ((TbDataDesktopHBetOperateTexas)x.Value).OperateGolds).ToList()--]]
	for k,v in pairs(map_operate) do                
		self.MapBetOperate[k] = v.OperateGolds
	end
end


		
ViewDesktopHTexasFactory = ViewDesktopHBaseFactory:new(nil)

function ViewDesktopHTexasFactory:new(o)
	o = o or {}
    setmetatable(o,self)
    self.__index = self	
	
    return o
end
        
function ViewDesktopHTexasFactory:GetName()
            return "Texas"
end

function ViewDesktopHTexasFactory:CreateViewDesktopHBase(ui_desktoph)        
            local desktoph_base = ViewDesktopHTexas:new(nil,ui_desktoph)
            return desktoph_base
end