ViewDesktopPlayerInfoTexas = ViewBase:new()

function ViewDesktopPlayerInfoTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Player = nil
    o.ViewDesktop = nil
    o.DesktopSeatCount = nil
    o.PlayerSeatWidgetControllerEx = nil
    o.UiHeadIcon = nil
    o.UiHandCardFirst = nil
    o.UiHandCardSecond = nil
    o.UiPlayerGiftAndVIP = nil
    --o.GGraphWinStarParent = nil
    o.GComPlayerCenter = nil
    o.GComChatParent = nil
    o.GTextActionOrName = nil
    o.GTextDesktopAmount = nil
    o.GImagePlayerShadow = nil
    o.GImagePlayerActionProgress = nil
    o.GImagePlayerWinner = nil
    o.GImageFriendMark = nil
    o.ListPlayerCard = nil
    o.FirstCard = nil
    o.SecondCard = nil
    o.ThinkingActionColorBegin = nil
    o.ThinkingActionColorMiddle = nil
    o.ThinkingActionColorEnd = nil
    o.ItemChatDesktop = nil
    o.UiSingleEmotion = nil
    o.ActionWaitingTime = 0
    o.CurrentActionTime = 0
    o.GameEndResetPlayerInfoTime = 255
    o.GameEndShowExpTime = 255
    o.Exp = 0
    o.Point = 0
    o.IsGameEnd = false
    o.VibrateOnce = true
    o.IsRelease = false

    return o
end

function ViewDesktopPlayerInfoTexas:onCreate()
    self.ViewMgr:bindEvListener("EvEntityRecvChatFromDesktop", self)
    self.ViewMgr:bindEvListener("EvCurrentWinner", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopShowdownNotify", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopGameEndNotifyTexas", self)
    self.ViewMgr:bindEvListener("EvEntitySelfAutoActionChange", self)
    self.ViewMgr:bindEvListener("EvCommonCardShowEnd", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopPreFlopNotify", self)
    self.ComUi.onClick:Add(
            function()
                self:_onClickSelf()
            end
    )
    self.ViewDesktop = self.ViewMgr:getView("DesktopTexas")
    self.GTextActionOrName = self.ComUi:GetChild("ActionOrNameLable").asTextField
    self.GTextDesktopAmount = self.ComUi:GetChild("DesktopAmountLable").asTextField
    local com_headicon = self.ComUi:GetChild("ComHeadIcon").asCom
    self.UiHeadIcon = ViewHeadIcon:new(nil, com_headicon)
    self.GImagePlayerShadow = self.ComUi:GetChild("ImagePlayerShadow").asImage
    self.GImagePlayerActionProgress = self.ComUi:GetChild("PlayerActionProgress").asImage
    self.GImagePlayerWinner = self.ComUi:GetChild("PlayerWinner").asImage
    self.GImageFriendMark = self.ComUi:GetChild("FriendMark").asImage
    local com_cardfirst = self.ComUi:GetChild("ComCardOne").asCom
    self.UiHandCardFirst = ViewHandCardTexas:new(nil, com_cardfirst, "", 1)
    local com_cardsecond = self.ComUi:GetChild("ComCardTwo").asCom
    self.UiHandCardSecond = ViewHandCardTexas:new(nil, com_cardsecond, "", 2)
    self.GComChatParent = self.ComUi:GetChild("ComChatParent").asCom
    self.ImageWinName = self.ComUi:GetChild("ImageWinName").asImage
    self.TransitionWinName = self.ComUi:GetTransition("TransitionWinName")
    --self.GGraphWinStarParent = self.ComUi:GetChild("GraphWinStarParent").asGraph
    --local particle1 = p_helper:GetParticel("winnerstar.ab")
    --local p_1 = CS.UnityEngine.Object.Instantiate(particle1:LoadAsset("WinnerStar"))
    --self.GGraphWinStarParent:SetNativeObject(CS.FairyGUI.GoWrapper(p_1))
    --self.GGraphWinStarParent.visible = false
    self.GMovieAllIn = self.ComUi:GetChild("MovieAllIn").asMovieClip

    self.GComPlayerCenter = self.ComUi:GetChild("ComPlayerCenter").asCom
    self.PlayerSeatWidgetControllerEx = PlayerSeatWidgetControllerTexas:new(nil, self, self.GComPlayerCenter)
    self.PlayerShowTips = PlayerShowTips:new(nil, self)
    self.ImageAutoAction = self.ComUi:GetChild("ImageAutoAction")
    self.ComAction = self.ComUi:GetChild("ComAction").asCom
    self.ControllerAction = self.ComAction:GetController("ControllerAction")

    self.ListPlayerCard = CS.Casinos.LuaHelper.GetNewCardList()
    self.ThinkingActionColorBegin = CS.UnityEngine.Color.green
    self.ThinkingActionColorMiddle = CS.UnityEngine.Color.yellow
    self.ThinkingActionColorEnd = CS.UnityEngine.Color.red
    self.GameEndResetPlayerInfoTime = 255
    self.GameEndShowExpTime = 255
    self.Exp = 0
    self.Point = 0
    self.VibrateOnce = true
    self.IsRelease = false
end

function ViewDesktopPlayerInfoTexas:onDestroy()
    self.ViewMgr:unbindEvListener(self)
    if (self.ItemChatDesktop ~= nil)
    then
        self.ViewDesktop.UiDesktopChatParent:destroyChat(self.ItemChatDesktop)
    end
end

function ViewDesktopPlayerInfoTexas:release()
    self.IsRelease = true
    self.IsGameEnd = false
    self.FirstCard = nil
    self.SecondCard = nil
    self.Player = nil
    self.ListPlayerCard:Clear()
    self.GameEndResetPlayerInfoTime = 255
    self.GameEndShowExpTime = 255
    self.Exp = 0
    self.Point = 0
    self.UiHeadIcon:setPlayerInfo("", "", 0)
    self.UiHandCardFirst:hideCard()
    self.UiHandCardSecond:hideCard()
    self.UiPlayerGiftAndVIP:release()
    self.PlayerShowTips:reset()
    if (self.ItemChatDesktop ~= nil)
    then
        self.ItemChatDesktop:reset()
    end
    if (self.UiSingleEmotion ~= nil)
    then
        self.UiSingleEmotion:resetEmotion()
    end
    self.ComUi:SetXY(10000,10000)
    ViewHelper:setGObjectVisible(false, self.ImageAutoAction)
    ViewHelper:setGObjectVisible(false, self.ComUi)
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:release()
    end
end

function ViewDesktopPlayerInfoTexas:onHandleEv(ev)
    if (self.IsRelease)
    then
        return
    end

    if (ev ~= nil)
    then
        if (ev.EventName == "EvEntityRecvChatFromDesktop")
        then
            local chat_info = ev.chat_info

            if (chat_info.sender_etguid == self.Player.Guid)
            then
                local chat_is_remotion = false
                local map_exp = self.ViewMgr.TbDataMgr:GetMapData("Expression")
                for k, v in pairs(map_exp) do
                    local tb_expression = v
                    if (tb_expression.ExpressionName == chat_info.chat_content)
                    then
                        chat_is_remotion = true
                        break
                    end
                end

                if (chat_is_remotion)
                then
                    if (self.UiSingleEmotion == nil)
                    then
                        local co_chat = CS.FairyGUI.UIPackage.CreateObject("Common", "ComChatEmotion").asCom
                        self.ComUi:AddChild(co_chat)
                        local pos = self.GComChatParent.xy
                        local pos_x = pos.x
                        pos_x = pos_x - co_chat.width / 2
                        pos.x = pos_x
                        local pos_y = pos.y
                        pos_y = pos_y - co_chat.height / 2
                        pos.y = pos_y
                        co_chat.xy = pos
                        self.UiSingleEmotion = CS.Casinos.UiSingleEmotion(co_chat)
                    end
                    self.UiSingleEmotion:setEmotion(chat_info.chat_content, true)
                else
                    if (self.ItemChatDesktop ~= nil)
                    then
                        self.ViewDesktop.UiDesktopChatParent:destroyChat(self.ItemChatDesktop)
                    end

                    local co_chatname = "CoChatLeft"
                    if (self.UiPlayerGiftAndVIP.ShowLeftGift)
                    then
                        co_chatname = "CoChatRight"
                    end

                    if (chat_info.sender_etguid == self.ViewDesktop.ControllerDesktop.Guid)
                    then
                        co_chatname = "CoChatLeft"
                    end

                    self.ItemChatDesktop = self.ViewDesktop.UiDesktopChatParent:addChat(co_chatname, self.ComUi, self.GComChatParent.xy)
                    self.ItemChatDesktop:setChatTextAndSortingOrder(chat_info, self.ComUi.sortingOrder)
                end
            end
        elseif (ev.EventName == "EvCurrentWinner")
        then
            if (ev.player ~= self.Player)
            then
                --ViewHelper:setGObjectVisible(true, self.GImagePlayerShadow)
                self:showShade(true)
                self:_hideHighLight()
            end
        elseif (ev.EventName == "EvEntityDesktopShowdownNotify")
        then
            self.PlayerSeatWidgetControllerEx:hideHighLight()
        elseif (ev.EventName == "EvEntityDesktopGameEndNotifyTexas")
        then
            self.PlayerSeatWidgetControllerEx:hideHighLight()
        elseif (ev.EventName == "EvEntitySelfAutoActionChange")
        then
            if self.Player.IsMe then
                ViewHelper:setGObjectVisible(ev.is_autoaction, self.ImageAutoAction)
            end
        elseif (ev.EventName == "EvCommonCardShowEnd")
        then
            if self.IsShowDown then
                self:_hideHighLight()
                self.UiHandCardFirst:showCard(0.2)
                self.UiHandCardSecond:showCard(0.2)
                self.UiPlayerGiftAndVIP:playerIsShowDown()
                self.PlayerSeatWidgetControllerEx:hideBetInfoAndCards()
                self.IsShowDown = false
            end
        end
    end
end

function ViewDesktopPlayerInfoTexas:onUpdate(elapsed_tm)
    if (CS.Casinos.CasinosContext.Instance.Pause or self.IsRelease)
    then
        return
    end

    if (self.IsGameEnd)
    then
        if self.GameEndResetPlayerInfoTime > 0 then
            self.GameEndResetPlayerInfoTime = self.GameEndResetPlayerInfoTime - elapsed_tm
            if (self.GameEndResetPlayerInfoTime <= 0)
            then
                self:_resetPlayerInfo()
                if (self.PlayerSeatWidgetControllerEx ~= nil)
                then
                    self.PlayerSeatWidgetControllerEx:reset()
                end
                self.GameEndResetPlayerInfoTime = 255
            end
        end
        if self.GameEndShowExpTime > 0 then
            self.GameEndShowExpTime = self.GameEndShowExpTime - elapsed_tm
            if (self.GameEndShowExpTime <= 0)
            then
                self.PlayerShowTips:showExpAndPoint(self.Exp, self.Point)
                self.GameEndShowExpTime = 255
                self.IsGameEnd = false
                self.Exp = 0
                self.Point = 0
            end
        end
    end

    if (CS.System.String.IsNullOrEmpty(self.Player.DesktopTexas.PlayerTurn.player_guid) == false and
            self.Player.DesktopTexas.PlayerTurn.player_guid == self.Player.Guid and self.IsGameEnd == false)
    then
        self.CurrentActionTime = self.CurrentActionTime + elapsed_tm

        local rest_time = self.ActionWaitingTime - self.CurrentActionTime
        if (rest_time <= 0)
        then
            rest_time = 0
        end

        self:_setThinkingTimeSprite(rest_time / self.ActionWaitingTime, true)
    end

    if (self.ItemChatDesktop ~= nil)
    then
        self.ItemChatDesktop:update(elapsed_tm)
    end
end

function ViewDesktopPlayerInfoTexas:gameEnd()
    self.IsShowDown = false
    self:_hideHighLight()

    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:gameEnd()
    end

    self.GameEndResetPlayerInfoTime = self.Player.DesktopTexas.ReBeginTm - 3.7
    self.GameEndShowExpTime = self.Player.DesktopTexas.ReBeginTm - 1.2
    self.IsGameEnd = true
    self:_resetThinkTimePro()
    if self.Player.PlayerDataDesktop.PlayerActionType ~= PlayerActionTypeTexas.Fold and self.Player.PlayerDataDesktop.DesktopPlayerState ~= TexasDesktopPlayerState.WaitWhile then
        self:_setNameOrAction(CS.Casinos.UiHelper.addEllipsisToStr(self.Player.PlayerDataDesktop.NickName,15,4), CS.UnityEngine.Color.white)
    end
    ViewHelper:setGObjectVisible(false, self.GMovieAllIn)
end

function ViewDesktopPlayerInfoTexas:addExpAndPoint(exp, point)
    self.Exp = exp
    self.Point = point
end

function ViewDesktopPlayerInfoTexas:playerChangeSeat()
    if (self.ViewDesktop ~= nil)
    then
        local change_seat = self.ViewDesktop:getUiSeatInfo(self.Player.UiSeatIndex)
        if (change_seat ~= nil)
        then
            local pos_e = change_seat.GComSeatPlayerParent.xy
            local pos_ex = pos_e.x
            pos_ex = pos_ex - self.GImagePlayerShadow.width / 2
            pos_e.x = pos_ex
            local pos_ey = pos_e.y
            pos_ey = pos_ey - self.GImagePlayerShadow.height / 2
            pos_e.y = pos_ey

            CS.Casinos.UiDoTweenHelper.TweenMove(self.ComUi, self.ComUi.xy, pos_e, 0.4, true)

            if (self.PlayerSeatWidgetControllerEx ~= nil)
            then
                self.PlayerSeatWidgetControllerEx:resetSeatIndex()
            end

            self.UiPlayerGiftAndVIP:checkGiftAndVIPPos(self.DesktopSeatCount, self.Player.UiSeatIndex, self.Player.PlayerDataDesktop.VIPLevel)
        end
    end
end

function ViewDesktopPlayerInfoTexas:playerOb(seat_index)
    self.ViewDesktop:playerOB(seat_index)
    if (self.Player ~= nil and self.Player.IsMe)
    then
        self.ViewDesktop:showCommonCardType(self.IsGameEnd)
    end
end

function ViewDesktopPlayerInfoTexas:playerWait4Next()
    self:_resetThinkTimePro()
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        --ViewHelper:setGObjectVisible(false, self.GGraphWinStarParent)
        ViewHelper:setGObjectVisible(false, self.GMovieAllIn)
    end
end

function ViewDesktopPlayerInfoTexas:playerWaitWhile()
    self:_setNameOrAction(self.ViewMgr.LanMgr:getLanValue("StepOut"), CS.UnityEngine.Color.white)
    self.UiPlayerGiftAndVIP:reset()
    --ViewHelper:setGObjectVisible(true, self.GImagePlayerShadow)
    self:showShade(true)
    --ViewHelper:setGObjectVisible(true, self.GImagePlayerActionProgress)
    ViewHelper:setGObjectVisible(false, self.GMovieAllIn)
end

function ViewDesktopPlayerInfoTexas:playerReturn()
    self:setNickName()
    self:_setAmontChip(self.Player.PlayerDataDesktop.Stack)
end

function ViewDesktopPlayerInfoTexas:playerInGame()
    if (self.Player.PlayerDataDesktop.PlayerActionType ~= PlayerActionTypeTexas.Fold)
    then
        --ViewHelper:setGObjectVisible(false, self.GImagePlayerShadow)

        self:showShade(false)
        ViewHelper:setGObjectVisible(false, self.GImagePlayerActionProgress)
    end
end

function ViewDesktopPlayerInfoTexas:playerTurn(is_player_turn, turn_tm)
    self:_resetThinkTimePro()

    if (is_player_turn == false)
    then
        ViewHelper:setGObjectVisible(false, self.GImagePlayerActionProgress)
    else
        self.VibrateOnce = true
        self.CurrentActionTime = self.ActionWaitingTime - turn_tm
        ViewHelper:setGObjectVisible(true, self.GImagePlayerActionProgress)
        --ViewHelper:setGObjectVisible(false, self.GImagePlayerShadow)
        self:showShade(false)
        ViewHelper:setGObjectVisible(false, self.GImagePlayerWinner)
        --ViewHelper:setGObjectVisible(false, self.GGraphWinStarParent)
        ViewHelper:setGObjectVisible(false, self.GMovieAllIn)
        self:setNickName()
    end
end

function ViewDesktopPlayerInfoTexas:playerStackChange()
    self:_setAmontChip(self.Player.PlayerDataDesktop.Stack)
end

function ViewDesktopPlayerInfoTexas:allIn()
    --ViewHelper:setGObjectVisible(true, self.GGraphAllInParent)
end

function ViewDesktopPlayerInfoTexas:reset()
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:resetSeatIndex()
    end
end

function ViewDesktopPlayerInfoTexas:setPlayerInfo(player, parent, pos, desk_seatcount, action_waiting_time, is_gameend)
    ViewHelper:setGObjectVisible(true, self.ComUi)
    self.IsRelease = false
    local pos_e = pos
    local pos_ex = pos_e.x
    pos_ex = pos_ex - self.GImagePlayerShadow.width / 2
    pos_e.x = pos_ex
    local pos_ey = pos_e.y
    pos_ey = pos_ey - self.GImagePlayerShadow.height / 2
    pos_e.y = pos_ey
    self.ComUi.xy = pos_e
    self.Player = player
    local com_giftandvip_left = self.ComUi:GetChild("ComGiftAndVIPLeft").asCom
    local com_giftandvip_right = self.ComUi:GetChild("ComGiftAndVIPRight").asCom
    self.UiPlayerGiftAndVIP = ViewPlayerGiftAndVIP:new(nil, self.Player.Guid, com_giftandvip_left, com_giftandvip_right, self.ComUi, self.ViewMgr)
    self.DesktopSeatCount = desk_seatcount
    self.ActionWaitingTime = action_waiting_time
    self.IsGameEnd = is_gameend
    self.UiHandCardFirst:hideHighLight()
    self.UiHandCardSecond:hideHighLight()
    ViewHelper:setGObjectVisible(false, self.GImageFriendMark)
    if (self.ViewDesktop ~= nil)
    then
        self.ViewDesktop:playerSeatInDesk(self.Player.UiSeatIndex)
    end
    self.PlayerSeatWidgetControllerEx:init()

    local show_shadow = true
    local player_state = self.Player.PlayerDataDesktop.DesktopPlayerState
    if (player_state == TexasDesktopPlayerState.InGame and self.Player.PlayerDataDesktop.PlayerActionType ~= PlayerActionTypeTexas.Fold)
    then
        show_shadow = false
    end
    --ViewHelper:setGObjectVisible(show_shadow, self.GImagePlayerShadow)
    self:showShade(show_shadow)
    if player_state ~= TexasDesktopPlayerState.InGame then
        if player_state == TexasDesktopPlayerState.WaitWhile then
            self:_setNameOrAction(self.ViewMgr.LanMgr:getLanValue("StepOut"), CS.UnityEngine.Color.white)
        else
            self:_setNameOrAction(CS.Casinos.UiHelper.addEllipsisToStr(self.Player.PlayerDataDesktop.NickName,15,4), CS.UnityEngine.Color.white)
        end
    else
        local current_showindex = self:getActionIndex(self.Player.PlayerDataDesktop.PlayerActionType)
        if current_showindex >= 0 then
            ViewHelper:setGObjectVisible(false, self.GTextActionOrName)
            ViewHelper:setGObjectVisible(true, self.ComAction)
            self.ControllerAction.selectedIndex = current_showindex
        else
            self:setNickName()
        end
    end
    self:_setAmontChip(self.Player.PlayerDataDesktop.Stack)
    ViewHelper:setGObjectVisible(false, self.GImagePlayerWinner)
    ViewHelper:setGObjectVisible(false, self.GImagePlayerActionProgress)
    --ViewHelper:setGObjectVisible(false, self.GGraphWinStarParent)
    ViewHelper:setGObjectVisible(false, self.GMovieAllIn)
    self.UiHandCardFirst:hideCard()
    self.UiHandCardSecond:hideCard()
    local vip_level = self.Player.PlayerDataDesktop.VIPLevel
    self.UiHeadIcon:setPlayerInfo(self.Player.PlayerDataDesktop.IconName, self.Player.PlayerDataDesktop.AccountId, vip_level)
    self.UiPlayerGiftAndVIP:checkGiftAndVIPPos(self.DesktopSeatCount, self.Player.UiSeatIndex, vip_level)

    local current_itemdata = self.Player.PlayerDataDesktop.CurrentGiftItemData
    if (current_itemdata ~= nil and CS.System.String.IsNullOrEmpty(current_itemdata.item_objid) == false)
    then
        local tb_item = self.ViewMgr.TbDataMgr:GetData("Item", current_itemdata.item_tbid)
        if (tb_item.UnitType == "GiftTmp")
        then
            self:setGift(current_itemdata, self.Player.Guid, false)
        end
    end
end

function ViewDesktopPlayerInfoTexas:setCard(show_card, card1, card2, is_init)
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        ViewHelper:setGObjectVisible(false, self.GImagePlayerActionProgress)
        self.PlayerSeatWidgetControllerEx:showCardAndBetInfo(card1, card2, show_card, is_init)
    end
end

function ViewDesktopPlayerInfoTexas:setPlayerStateAndAction(player_state, action_type,is_init)
    local action_name = CS.Casinos.UiHelper.addEllipsisToStr(self.Player.PlayerDataDesktop.NickName,15,4)
    local show_shadow = false
    local sound = "chip"
    local play_sound = true
    local state = player_state
    if (state ~= TexasDesktopPlayerState.InGame)
    then
        if is_init then
            show_shadow = true
        else
            if state ~= TexasDesktopPlayerState.Wait4Next then
                show_shadow = true
            else
                if action_type == PlayerActionTypeTexas.Fold then
                    show_shadow = true
                end
            end
        end
        play_sound = false
        self:setNickName()
    else
        local action = action_type--PlayerActionTypeTexas.__CastFrom()
        if (action == PlayerActionTypeTexas.None)
        then
            play_sound = false
        elseif (action == PlayerActionTypeTexas.Bet)
        then
            action_name = self.ViewMgr.LanMgr:getLanValue("BetOn")
            play_sound = false
        elseif (action == PlayerActionTypeTexas.Fold)
        then
            show_shadow = true
            sound = "desk_player_fold"
            action_name = self.ViewMgr.LanMgr:getLanValue("Fold")
        elseif (action == PlayerActionTypeTexas.Check)
        then
            play_sound = false
            sound = "dongdong"
            if self.Player.DesktopTexas.IsSnapshot == false then
                CS.Casinos.CasinosContext.Instance:play(sound, CS.Casinos._eSoundLayer.LayerNormal)
            end
            action_name = self.ViewMgr.LanMgr:getLanValue("Check")
        elseif (action == PlayerActionTypeTexas.Call)
        then
            sound = "desk_post_bet"
            action_name = self.ViewMgr.LanMgr:getLanValue("Call")
        elseif (action == PlayerActionTypeTexas.Raise)
        then
            action_name = self.ViewMgr.LanMgr:getLanValue("AddBet")
            play_sound = false
        elseif (action == PlayerActionTypeTexas.ReRaise)
        then
            action_name = self.ViewMgr.LanMgr:getLanValue("AgainAddBet")
            play_sound = false
        elseif (action == PlayerActionTypeTexas.AllIn)
        then
            action_name = self.ViewMgr.LanMgr:getLanValue("All-in")
            ViewHelper:setGObjectVisible(true, self.GMovieAllIn)
            play_sound = false
        end

        local current_showindex = self:getActionIndex(action)
        if current_showindex >= 0 then
            ViewHelper:setGObjectVisible(false, self.GTextActionOrName)
            ViewHelper:setGObjectVisible(true, self.ComAction)
            self.ControllerAction.selectedIndex = current_showindex
        else
            self:setNickName()
        end
    end
    --ViewHelper:setGObjectVisible(show_shadow, self.GImagePlayerShadow)
    self:showShade(show_shadow)

    if (play_sound and self.Player.DesktopTexas.IsSnapshot == false)
    then
        CS.Casinos.CasinosContext.Instance:play(sound, CS.Casinos._eSoundLayer.LayerNormal)
    end

    self:_resetThinkTimePro()
end
--
--function ViewDesktopPlayerInfoTexas:showWinnerCard()
--    if (self.ViewDesktop ~= nil)
--    then
--        self.ViewDesktop:resetComminityShow()
--    end
--
--    self:_hideHighLight()
--
--    if (self.FirstCard == nil)
--    then
--        return
--    end
--
--    self.UiPlayerGiftAndVIP:playerIsWin()
--end

function ViewDesktopPlayerInfoTexas:setGift(current_item, target_etguid, is_sendgift)
    local item = nil
    local from_pos = CS.UnityEngine.Vector2.zero
    if (current_item ~= nil)
    then
        local item_data = ItemData1:new(nil)
        item_data.item_objid = current_item.item_objid
        item_data.item_tbid = current_item.item_tbid
        item_data.count = current_item.count
        item_data.map_unit_data = current_item.map_unit_data
        item = Item:new(nil, self.ViewMgr.TbDataMgr, item_data)
        if (item.TbDataItem.UnitType ~= "GiftTmp")
        then
            return
        end

        local unit_tmp = item.UnitLink
        local sender_etguid = unit_tmp.GiveEtGuid
        if (CS.System.String.IsNullOrEmpty(sender_etguid))
        then
            return
        end

        local from_playerinfo = self:getPlayerInfo(sender_etguid)
        if (from_playerinfo ~= nil)
        then
            from_pos = self.ViewDesktop.ComUi:TransformPoint(from_playerinfo.ComUi.xy, self.ComUi)
        end
    end

    self.UiPlayerGiftAndVIP:setGift(item, from_pos, is_sendgift)
end

function ViewDesktopPlayerInfoTexas:sendMagicExp(exp)
    local tb_magicexp = self.ViewMgr.TbDataMgr:GetData("UnitMagicExpression", exp.item_tbid)
    if (tb_magicexp == nil)
    then
        return
    end

    local item_data = ItemData1:new(nil)
    item_data.item_objid = exp.item_objid
    item_data.item_tbid = exp.item_tbid
    item_data.count = exp.count
    item_data.map_unit_data = exp.map_unit_data
    local item = Item:new(nil, self.ViewMgr.TbDataMgr, item_data)
    local unit_link = item.UnitLink
    local from_seat = self.ViewDesktop.Desktop:getSeatByGuid(unit_link.GiveEtGuid)
    if (from_seat ~= nil)
    then
        local from_player = from_seat.player_texas
        local from_pos = from_player.UiDesktopPlayerInfo.PlayerSeatWidgetControllerEx.SeatWidget.GComChipStart.xy
        from_pos = from_player.UiDesktopPlayerInfo.ComUi:TransformPoint(from_pos, self.ComUi)
        local to_pos = self.PlayerSeatWidgetControllerEx.SeatWidget.GComChipStart.xy
        local ui_pool = self.ViewMgr:getView("Pool")
        local item_magicsender = ui_pool:getMagicExpSender()
        self.ComUi:AddChild(item_magicsender.GCoMagicExpSender)
        item_magicsender:sendMagicExp(from_pos, to_pos, exp.item_tbid)
    end
end

function ViewDesktopPlayerInfoTexas:sendWinnerGolds(winner_golds, map_win_pot)
    self.PlayerSeatWidgetControllerEx:sendWinnerChips(winner_golds, map_win_pot)
end

function ViewDesktopPlayerInfoTexas:setIsBtn(is_btn)
    self.PlayerSeatWidgetControllerEx:setIsBtn(is_btn)
end

function ViewDesktopPlayerInfoTexas:setSmallBlind()
    --self:_setNameOrAction(self.ViewMgr.LanMgr:getLanValue("小盲注", "SmallBlind"), CS.UnityEngine.Color.yellow)
end

function ViewDesktopPlayerInfoTexas:setBigBlind()
    --self:_setNameOrAction(self.ViewMgr.LanMgr:getLanValue("大盲注", "BigBlind"), CS.UnityEngine.Color.yellow)
end

function ViewDesktopPlayerInfoTexas:deskIdle()
    if (self.Player.PlayerDataDesktop.DesktopPlayerState == TexasDesktopPlayerState.WaitWhile)
    then
        return
    end

    self.IsShowDown = false
    self:_resetPlayerInfo()
    self.UiPlayerGiftAndVIP:reset()

    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:deskIdle()
    end
end

function ViewDesktopPlayerInfoTexas:dealCardDone()
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:dealCard()
    end
end

function ViewDesktopPlayerInfoTexas:preflopBegin()
    self.IsGameEnd = false
    self.IsShowDown = false
    self.FirstCard = nil
    self.SecondCard = nil
    self.Exp = 0
    self.Point = 0
    self.ListPlayerCard:Clear()

    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:preflopBegin()
    end

    self:_resetPlayerInfo()
    self.UiPlayerGiftAndVIP:reset()
    self.PlayerShowTips:reset()
end

function ViewDesktopPlayerInfoTexas:roundEnd(t_playerchips_inpot)
    if (t_playerchips_inpot ~= nil and #t_playerchips_inpot > 0)
    then
        self.PlayerSeatWidgetControllerEx:goldsInMainPot(t_playerchips_inpot)
    end
end

function ViewDesktopPlayerInfoTexas:flop()
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:flop()
    end
    self:setNickName()
end

function ViewDesktopPlayerInfoTexas:turn()
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:turn()
    end
    self:setNickName()
end

function ViewDesktopPlayerInfoTexas:river()
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:river()
    end
    self:setNickName()
end

function ViewDesktopPlayerInfoTexas:setShowCardState(showcard_state)
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:setShowCardState(showcard_state)
    end
end

function ViewDesktopPlayerInfoTexas:setShowCardData(first_card, second_card)
    local f_c = nil
    if first_card ~= nil then
        f_c = first_card:GetCardData()
    end
    if self.Player.IsMe then
        if f_c ~= nil and self.PlayerSeatWidgetControllerEx ~= nil then
            self.PlayerSeatWidgetControllerEx:showcard1()
        end
    else
        self.UiHandCardFirst:setCardData(f_c)
        self.UiHandCardFirst:showCard(0)
    end

    local s_c = nil
    if second_card ~= nil then
        s_c = second_card:GetCardData()
    end
    if self.Player.IsMe then
        if s_c ~= nil and self.PlayerSeatWidgetControllerEx ~= nil then
            self.PlayerSeatWidgetControllerEx:showcard2()
        end
    else
        self.UiHandCardSecond:setCardData(s_c)
        self.UiHandCardSecond:showCard(0)
    end
end

function ViewDesktopPlayerInfoTexas:showDown(card_first, card_second, carddata_left)
    if (card_first == nil)
    then
        return
    end
    self.FirstCard = card_first
    self.SecondCard = card_second
    self.UiHandCardFirst:setCardData(self.FirstCard:GetCardData())
    self.UiHandCardSecond:setCardData(self.SecondCard:GetCardData())
    self.ListPlayerCard:Clear()
    for i, v in pairs(self.Player.DesktopTexas.CommunityCards) do
        self.ListPlayerCard:Add(v)
    end
    self.ListPlayerCard:Add(self.FirstCard)
    self.ListPlayerCard:Add(self.SecondCard)
    local left_cards = #carddata_left
    if left_cards > 0 then
        self.IsShowDown = true
    else
        self:_hideHighLight()
        self.UiHandCardFirst:showCard(0.2)
        self.UiHandCardSecond:showCard(0.2)
        self.UiPlayerGiftAndVIP:playerIsShowDown()
        self.PlayerSeatWidgetControllerEx:hideBetInfoAndCards()
    end
end

function ViewDesktopPlayerInfoTexas:showWinStar(showWinStar)
    --ViewHelper:setGObjectVisible(false, self.GImagePlayerShadow)
    self:showShade(false)
    local ev = self.ViewMgr:getEv("EvCurrentWinner")
    if (ev == nil)
    then
        ev = EvCurrentWinner:new(nil)
    end
    ev.player = self.Player
    self.ViewMgr:sendEv(ev)

    self.PlayerShowTips:showWinGold(showWinStar)
    if (self.ViewDesktop ~= nil)
    then
        self.ViewDesktop:resetComminityShow()
    end

    local best_hand = nil

    if (self.ListPlayerCard ~= nil and self.ListPlayerCard.Count > 0)
    then
        best_hand = CS.Casinos.CardTypeHelperTexas.GetBestHand(self.ListPlayerCard)
        for i = 0, self.ListPlayerCard.Count - 1 do
            local c = self.ListPlayerCard[i]
        end
    end

    local type_name = ""
    local show_winner_tips = true
    if (best_hand == nil)
    then
        type_name = self.ViewMgr.LanMgr:getLanValue("Winner")
        show_winner_tips = false
    else
        if (best_hand.RankType == CS.Casinos.HandRankTypeTexas.None)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("Winner")
            show_winner_tips = false
        end
    end

    self:_setAmontChip(self.Player.PlayerDataDesktop.Stack)

    if (show_winner_tips)
    then
        if (self.FirstCard ~= nil)
        then
            local t_ranktype_cards = CS.Casinos.LuaHelper.ListToLuatable(best_hand.RankTypeCards)
            local t_ranktype_cards_all = CS.Casinos.LuaHelper.ListToLuatable(best_hand.Cards)
            local hand_type = best_hand.RankType
            local show_cardtype_tips = true
            if (hand_type == CS.Casinos.HandRankTypeTexas.HighCard)
            then
                show_cardtype_tips = false
            end

            local show_firstcard_highlight = false
            local show_secondcard_highlight = false
            if (show_cardtype_tips)
            then
                for i, v in pairs(t_ranktype_cards) do
                    local card = v
                    if (self.FirstCard.Type == card.type and self.FirstCard.Suit == card.suit)
                    then
                        show_firstcard_highlight = true
                    elseif (self.SecondCard.Type == card.type and self.SecondCard.Suit == card.suit)
                    then
                        show_secondcard_highlight = true
                    end
                end
            end

            local first_not_dark = false
            local s_not_dark = false
            for i, v in pairs(t_ranktype_cards_all) do
                local card = v
                if (self.FirstCard.Type == card.type and self.FirstCard.Suit == card.suit)
                then
                    first_not_dark = true
                elseif (self.SecondCard.Type == card.type and self.SecondCard.Suit == card.suit)
                then
                    s_not_dark = true
                end
            end

            self.UiHandCardFirst:showHighLight(show_firstcard_highlight,first_not_dark)
            self.UiHandCardSecond:showHighLight(show_secondcard_highlight,s_not_dark)
            self.ViewDesktop:showCardTips(best_hand.RankType, best_hand.RankTypeCards,best_hand.Cards, type_name, true, true)
        end
    end

    --ViewHelper:setGObjectVisible(true, self.GGraphWinStarParent)
    if self.Player.IsMe then
        self.ViewDesktop:showMeWin()
    end
end

function ViewDesktopPlayerInfoTexas:showWinnerHandType(win_golds)
    local best_hand = nil

    if (self.ListPlayerCard ~= nil and self.ListPlayerCard.Count > 0)
    then
        best_hand = CS.Casinos.CardTypeHelperTexas.GetBestHand(self.ListPlayerCard)
        for i = 0, self.ListPlayerCard.Count - 1 do
            local c = self.ListPlayerCard[i]
        end
    end

    local type_name = ""
    local show_winner_tips = true
    local add_win = true
    if (best_hand == nil)
    then
        type_name = self.ViewMgr.LanMgr:getLanValue("Winner")
        show_winner_tips = false
        add_win = false
    else
        if (best_hand.RankType == CS.Casinos.HandRankTypeTexas.None)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("Winner")
            show_winner_tips = false
            add_win = false
        elseif (best_hand.RankType == CS.Casinos.HandRankTypeTexas.HighCard)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("HighCard")
        elseif (best_hand.RankType == CS.Casinos.HandRankTypeTexas.Pair)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("Pair")
        elseif (best_hand.RankType == CS.Casinos.HandRankTypeTexas.TwoPairs)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("TwoPairs")
        elseif (best_hand.RankType == CS.Casinos.HandRankTypeTexas.ThreeOfAKind)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("ThreeOfAKind")
        elseif (best_hand.RankType == CS.Casinos.HandRankTypeTexas.Straight)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("Straight")
        elseif (best_hand.RankType == CS.Casinos.HandRankTypeTexas.Flush)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("Flush")
        elseif (best_hand.RankType == CS.Casinos.HandRankTypeTexas.FullHouse)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("FullHouse")
        elseif (best_hand.RankType == CS.Casinos.HandRankTypeTexas.FourOfAKind)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("FourOfAKind")
        elseif (best_hand.RankType == CS.Casinos.HandRankTypeTexas.StraightFlush)
        then
            type_name = self.ViewMgr.LanMgr:getLanValue("StraightFlush")
        end
    end

    if add_win then
        type_name = type_name .. self.ViewMgr.LanMgr:getLanValue("WinGame")
    end

    if self.Player.DesktopTexas.IsSnapshot == false then
        self.TransitionWinName:Play()
    end
    self:_setNameOrAction(type_name, CS.UnityEngine.Color.yellow)
    ViewHelper:setGObjectVisible(true, self.GImagePlayerWinner)
    ViewHelper:setGObjectVisible(false, self.GMovieAllIn)
end

function ViewDesktopPlayerInfoTexas:showHandCardHighLight(best_hand, card_type_str)
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:showHandCardHighLight(best_hand, card_type_str)
    end
end

function ViewDesktopPlayerInfoTexas:raiseChips()
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:raiseChips()
    end
end

function ViewDesktopPlayerInfoTexas:playerFold()
    if (self.PlayerSeatWidgetControllerEx ~= nil)
    then
        self.PlayerSeatWidgetControllerEx:playerFold()
    end
end

function ViewDesktopPlayerInfoTexas:getPlayerInfo(player_guid)
    local player = self.Player.DesktopTexas.MapPlayerTexas[player_guid]
    local to_playerinfo = nil
    if (player ~= nil)
    then
        to_playerinfo = player.UiDesktopPlayerInfo
    end

    return to_playerinfo
end

function ViewDesktopPlayerInfoTexas:_resetThinkTimePro()
    self.CurrentActionTime = 0.0
    if (self.Player.PlayerDataDesktop.DesktopPlayerState ~= TexasDesktopPlayerState.WaitWhile)
    then
        self.GImagePlayerActionProgress.fillAmount = 1
        ViewHelper:setGObjectVisible(false, self.GImagePlayerActionProgress)
    end
end

function ViewDesktopPlayerInfoTexas:_resetPlayerInfo()
    self.UiHandCardFirst:resetCard(false)
    self.UiHandCardSecond:resetCard(false)
    ViewHelper:setGObjectVisible(false, self.ImageWinName)
    self.TransitionWinName:Stop(true, true)
    self.PlayerShowTips:reset()
    --ViewHelper:setGObjectVisible(false, self.GImagePlayerShadow)

    if self.Player.PlayerDataDesktop.DesktopPlayerState ~= TexasDesktopPlayerState.WaitWhile and self.Player.PlayerDataDesktop.PlayerActionType ~= PlayerActionTypeTexas.Fold
            and self.Player.PlayerDataDesktop.DesktopPlayerState ~= TexasDesktopPlayerState.HoldSeat
    then
        self:showShade(false)
    end

    ViewHelper:setGObjectVisible(false, self.GImagePlayerWinner)
    --ViewHelper:setGObjectVisible(false, self.GGraphWinStarParent)
    ViewHelper:setGObjectVisible(false, self.GMovieAllIn)
    if self.Player.PlayerDataDesktop.PlayerActionType ~= PlayerActionTypeTexas.Fold  and self.Player.PlayerDataDesktop.DesktopPlayerState ~= TexasDesktopPlayerState.WaitWhile then
        self:_setNameOrAction(CS.Casinos.UiHelper.addEllipsisToStr(self.Player.PlayerDataDesktop.NickName,15,4), CS.UnityEngine.Color.white)
    end
    if (self.Player.PlayerDataDesktop.DesktopPlayerState ~= TexasDesktopPlayerState.WaitWhile)
    then
        self:_resetThinkTimePro()
        self:_setAmontChip(self.Player.PlayerDataDesktop.Stack)
    end
end

function ViewDesktopPlayerInfoTexas:setNickName()
    if self.Player.PlayerDataDesktop.PlayerActionType ~= PlayerActionTypeTexas.Fold and self.Player.PlayerDataDesktop.PlayerActionType ~= PlayerActionTypeTexas.AllIn and self.Player.PlayerDataDesktop.DesktopPlayerState ~= TexasDesktopPlayerState.WaitWhile then
        self:_setNameOrAction(CS.Casinos.UiHelper.addEllipsisToStr(self.Player.PlayerDataDesktop.NickName,15,4), CS.UnityEngine.Color.white)
    end
end

function ViewDesktopPlayerInfoTexas:_setNameOrAction(name_or_action, color)
    self.GTextActionOrName.text = name_or_action
    self.GTextActionOrName.color = color
    ViewHelper:setGObjectVisible(true, self.GTextActionOrName)
    ViewHelper:setGObjectVisible(false, self.ComAction)
end

function ViewDesktopPlayerInfoTexas:setLeaveWhileTime(rest_time)
    if (rest_time <= 0)
    then
        self:_setNameOrAction(CS.Casinos.UiHelper.addEllipsisToStr(self.Player.PlayerDataDesktop.NickName,15,4), CS.UnityEngine.Color.white)
        --ViewHelper:setGObjectVisible(false, self.GImagePlayerShadow)
        self:showShade(false)
        ViewHelper:setGObjectVisible(false, self.GImagePlayerActionProgress)
    else
        self:_setNameOrAction(self.ViewMgr.LanMgr:getLanValue("StepOut"), CS.UnityEngine.Color.white)
        --ViewHelper:setGObjectVisible(true, self.GImagePlayerShadow)
        self:showShade(true)
        ViewHelper:setGObjectVisible(true, self.GImagePlayerActionProgress)
        self:_setThinkingTimeSprite(rest_time / self.Player.DesktopTexas.WaitWhileTm, false)
    end
end

function ViewDesktopPlayerInfoTexas:showShade(show_shade)
    ViewHelper:setGObjectVisible(show_shade, self.GImagePlayerShadow)
    local lable_a = 1
    if show_shade then
        lable_a = 0.5
    end
    self.GTextActionOrName.alpha = lable_a
end

function ViewDesktopPlayerInfoTexas:_onClickSelf()
    if (self.Player.Guid == self.ViewDesktop.ControllerDesktop.Guid)
    then
        self.ViewMgr:createView("ChatExPression")
    else
        local ui_profile = self.ViewMgr:createView("PlayerProfile")
        ui_profile:setPlayerGuid(CS.Casinos._ePlayerProfileType.Desktop, self.Player.Guid,
                function(player_info, head_icon)
                    self:_playerInfo(player_info, head_icon)
                end
        )
    end
end

function ViewDesktopPlayerInfoTexas:_playerInfo(player_info, head_icon)
    if self.UiHeadIcon ~= nil and self.UiHeadIcon.GLoaderPlayerIcon ~= nil then
        self.UiHeadIcon.GLoaderPlayerIcon.texture = CS.FairyGUI.NTexture(head_icon)
    end
end

function ViewDesktopPlayerInfoTexas:_hideHighLight()
    self.UiHandCardFirst:hideHighLight()
    self.UiHandCardSecond:hideHighLight()
end

function ViewDesktopPlayerInfoTexas:_setAmontChip(chips)
    local chips_show = UiChipShowHelper:getGoldShowStr(chips, self.ViewMgr.LanMgr.LanBase, true, 1)
    self.GTextDesktopAmount.text = chips_show
end

function ViewDesktopPlayerInfoTexas:_setThinkingTimeSprite(fill_amount, need_vibrate)
    ViewHelper:setGObjectVisible(true, self.GImagePlayerActionProgress)
    local persent = fill_amount
    local color = CS.UnityEngine.Color()

    if (persent <= 0.5 and persent >= 0.48 and (self.Player.Guid == self.ViewDesktop.ControllerDesktop.Guid) and self.VibrateOnce and need_vibrate)
    then
        --#if UNITY_ANDROID || UNITY_IOS
        CS.UnityEngine.Handheld.Vibrate()
        --#endif
        self.VibrateOnce = false
        CS.Casinos.CasinosContext.Instance:play("half_time", CS.Casinos._eSoundLayer.LayerNormal)
    end

    if (persent <= 0.5)
    then
        color = CS.UnityEngine.Color.Lerp(self.ThinkingActionColorMiddle, self.ThinkingActionColorEnd, 1 - persent * 2)
    else
        color = CS.UnityEngine.Color.Lerp(self.ThinkingActionColorBegin, self.ThinkingActionColorMiddle, (1 - persent) * 2)
    end

    self.GImagePlayerActionProgress.fillAmount = persent
    self.GImagePlayerActionProgress.color = color
end

function ViewDesktopPlayerInfoTexas:getActionIndex(action)
    local current_showindex = -1
    if (action == PlayerActionTypeTexas.None)
    then
    elseif (action == PlayerActionTypeTexas.Bet)
    then
        current_showindex = 3
    elseif (action == PlayerActionTypeTexas.Fold)
    then
        current_showindex = 0
    elseif (action == PlayerActionTypeTexas.Check)
    then
        current_showindex = 1
    elseif (action == PlayerActionTypeTexas.Call)
    then
        current_showindex = 2
    elseif (action == PlayerActionTypeTexas.Raise)
    then
        current_showindex = 3
    elseif (action == PlayerActionTypeTexas.ReRaise)
    then
        current_showindex = 3
    elseif (action == PlayerActionTypeTexas.AllIn)
    then
        current_showindex = 4
    end

    return current_showindex
end

ViewDesktopPlayerInfoTexasFactory = ViewFactory:new()

function ViewDesktopPlayerInfoTexasFactory:new(o, ui_package_name, ui_component_name,
                                               ui_layer, is_single, fit_screen)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

function ViewDesktopPlayerInfoTexasFactory:createView()
    local view = ViewDesktopPlayerInfoTexas:new(nil)
    return view
end