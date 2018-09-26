-- Copyright(c) Cragon. All rights reserved.

ViewDesktopHBase = {}

function ViewDesktopHBase:new(o,ui_desktoph)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    return o
end        
        
function ViewDesktopHBase:updateGameState(game_state, total_tm,max_canbet, map_userdata, is_screenshot)
end
        
function ViewDesktopHBase:idleState(is_screenshot)
end

function ViewDesktopHBase:readyState(is_screenshot,map_userdata)
end
     
function ViewDesktopHBase:betState(is_screenshot)
end

function ViewDesktopHBase:gameEnd(is_screenshot)
end

function ViewDesktopHBase:rest(is_screenshot)
end

function ViewDesktopHBase:getGoldOperateList()
end

function ViewDesktopHBase:updateBetOperate()
end

function ViewDesktopHBase:getGoldOperateMap()
end

function ViewDesktopHBase:getCardTypeAndSoundPath(cardtype_name, is_win)
end

function ViewDesktopHBase:getBeBankPlayerMinGolds()
end

function ViewDesktopHBase:getSeatDownMinGolds()
end

function ViewDesktopHBase:getCardType(list_card)
end

function ViewDesktopHBase:getCardTypeDes(card_type)
end

function ViewDesktopHBase:getAllCardType()
end

function ViewDesktopHBase:getCardResName(card_data)
end

function ViewDesktopHBase:getBgMusic()
end

function ViewDesktopHBase:getTurnCardSound()
end

function ViewDesktopHBase:getSeatCount()
end

function ViewDesktopHBase:getBetPotSign(betpot_index)
end

function ViewDesktopHBase:getShaiZhong()
end

function ViewDesktopHBase:setRewardPotPlayerInfo(total_info)
end

function ViewDesktopHBase:betPotIsWin(bet_pot)
end

function ViewDesktopHBase:betPotIsReset(bet_pot)
end

function ViewDesktopHBase:setCountDown(current_tm)
end

function ViewDesktopHBase:setPlayerCurrentBetInfo(current_bet)
end

function ViewDesktopHBase:getStandPlayerChatName()
end

function ViewDesktopHBase:getSysBankPlayerInitGold()
end

function ViewDesktopHBase:getTongPeiWhenAllShowEndShowGameResultTm()
end

function ViewDesktopHBase:getTongPeiImageName()
end

ViewDesktopHBaseFactory = {}

function ViewDesktopHBaseFactory:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	
    return o
end     
   
function ViewDesktopHBaseFactory:GetName()
end
        
function ViewDesktopHBaseFactory:CreateViewDesktopHBase(ui_desktoph)
end

_tCardTypeInfo = {}

function _tCardTypeInfo:new(o,cardtype_path,cardtype_soundpath)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	self.CardTypePath = cardtype_path
	self.CardTypeSoundPath = cardtype_soundpath
	
    return o
end     

_tBeBankGoldsInfo = {}

function _tBeBankGoldsInfo:new(o,mintake_golds,min_leavegold)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	self.MinTakeGolds = mintake_golds
	self.MinLeaveGolds = min_leavegold
	
    return o
end     