ViewForestParty = ViewBase:new()

function ViewForestParty:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	self.BetBgMusic = "ForestPartyBetStateBg"
    self.RotateMusic = "ForestPartyRotateBg"
    if(self.Instance==nil)
	then
		self.ViewMgr = nil
		self.GoUi = nil
		self.ComUi = nil
		self.Panel = nil
		self.UILayer = nil
		self.InitDepth = nil
		self.ViewKey = nil
		self.Instance = o
	end

    return self.Instance
end

function ViewForestParty:onCreate()
	self.BtnBetPanel = self.ComUi:GetChild("BtnBetPanel").asButton
    self.BtnBetPanel.onClick:Add(
		function()
			self:onClickBtnBetPanel()
		end
	)
    self.ImageBetPanel = self.ComUi:GetChild("ImageBetPanel").asImage
    self.ComBlocks = self.ComUi:GetChild("ComBlocks").asCom
    self.ComArrow = self.ComUi:GetChild("ComArrow").asCom
    self.ComAnimals = self.ComUi:GetChild("ComAnimals").asCom
    self.ComSongDeng = self.ComUi:GetChild("ComSongDeng").asCom
    self.ComDaSanYuan = self.ComUi:GetChild("ComDaSanYuan").asCom
    self.ComZongHengSiHai = self.ComUi:GetChild("ComZongHengSiHai").asCom
    self.ComMoreLottery = self.ComUi:GetChild("ComMoreLottery").asCom
    self.ComLotteryMultiple = self.ComUi:GetChild("ComLotteryMultiple").asCom
    self.GroupBlockDark = self.ComBlocks:GetChild("GroupBlockDark").asGroup
    self.GTextSongDeng = self.ComSongDeng:GetChild("TextSongDeng").asTextField
    self.GListMoreLottery = self.ComMoreLottery:GetChild("ListMoreLottery").asList
    self.ControllerZongHengSiHai = self.ComZongHengSiHai:GetController("CotrollerZongHengSiHai")
    self.TransitionWait = self.ComUi:GetTransition("TransitionWait")
    self.TransitionCountDown = self.ComUi:GetTransition("TransitionCountDown")
    self.TransitionRGY = self.ComUi:GetTransition("TransitionRGY")
    self.TransitionFanBei2 = self.ComUi:GetTransition("TransitionFanBei2")
    self.TransitionFanBei3 = self.ComUi:GetTransition("TransitionFanBei3")
    self.TransitionLightBlocks = self.ComBlocks:GetTransition("TransitionLight")
    self.TransitionFlashingSongDeng = self.ComSongDeng:GetTransition("TransitionFlashing")
    self.TransitionFlashingDaSanYuan = self.ComDaSanYuan:GetTransition("TransitionFlashing")
    self.TransitionFlashingZongHengSiHai = self.ComZongHengSiHai:GetTransition("TransitionFlashing")
    self.TransitionCountDown:SetHook("3",
		function()
			CS.Casinos.CasinosContext.Instance:Play("CountDown3", CS.Casinos._eSoundLayer.LayerNormal)
		end
	)
    self.TransitionCountDown:SetHook("2",
		function()
			CS.Casinos.CasinosContext.Instance:Play("CountDown2", CS.Casinos._eSoundLayer.LayerNormal)
		end
	) 
    self.TransitionCountDown:SetHook("1",
		function()
			CS.Casinos.CasinosContext.Instance:Play("CountDown1", CS.Casinos._eSoundLayer.LayerNormal)
		end
	)
    self.TransitionCountDown:SetHook("Go", 
		function()
			CS.Casinos.CasinosContext.Instance:Play("CountDownGo", CS.Casinos._eSoundLayer.LayerNormal)
		end
	)
    self.MoveClipShanDian = self.ComUi:GetChild("MoveClipShanDian").asMovieClip
    self.MoveClipBaoZhuang = self.ComUi:GetChild("MoveClipBaoZhuang").asMovieClip
    local transition_red = self.ComUi:GetTransition("TransitionRed")
    local transition_green = self.ComUi:GetTransition("TransitionGreen")
    local transition_yellow = self.ComUi:GetTransition("TransitionYellow")
    local btn_menu = self.ComUi:GetChild("BtnMenu").asButton
    btn_menu.onClick:Add(
		function()
			self:onClickBtnMenu()
		end
	)
    local com_common = self.ComUi:GetChild("ComCommon").asCom
    local com_caijin = self.ComUi:GetChild("ComCaiJin").asCom
    self.GTextCaijin = com_caijin:GetChild("TextCaiJin").asTextField
    self.ArrayTransitionYezi = {}
    self.ArrayGroupYeZi = {}
    self.ArrayTransitionYezi[0] = transition_red
    self.ArrayTransitionYezi[1] = transition_green
    self.ArrayTransitionYezi[2] = transition_yellow
    self.ArrayGroupYeZi[0] = self.ComUi:GetChild("YeZiRed").asGroup
    self.ArrayGroupYeZi[1] = self.ComUi:GetChild("YeZiGreen").asGroup
    self.ArrayGroupYeZi[2] = self.ComUi:GetChild("YeZiYellow").asGroup
    self.ForestPartyCommon =  ForestPartyCommon:new(nil,com_common)
    self.QueResultLamps = {}
    self.ArrayComLamps = {}
    self.ArrayComAnimals = {}
    self.ArrayAnimalsPos = {}
    self.ArrayAnimalsRot = {}
	for i = 0 23 do
		local com_lamp = self.ComBlocks:GetChild("Block" .. tostring(i)).asCom
        local com_animal = self.ComAnimals:GetChild("Animal" .. tostring(i)).asCom
        self.ArrayComLamps[i] = com_lamp
        self.ArrayComAnimals[i] = com_animal
        self.ArrayAnimalsPos[i] = com_animal.position
        self.ArrayAnimalsRot[i] = com_animal.rotation
	end
    self.ChangTime = 0.5f
end

function ViewForestParty:onUpdate(tm)
	self.ForestPartyCommon:Update(tm)
    if (self.UpdateSongDeng)
	then
		if (self.ComSongDeng.visible == false)
		then
			self.ComSongDeng.visible = true
		end
        self.ChangTime = self.ChangTime - tm
        if (self.ChangTime <= 0)
		then
			local deng_amount = CS.UnityEngine.Random.Range(0, 12)
            self.GTextSongDeng.text = tostring(deng_amount)
            self.ChangTime = 0.5
		end
	end
    if (self.UpdateCaiJin)
	then
		self.ChangTime = self.ChangTime - tm
        if (self.ChangTime <= 0)
		then
			local caijin = CS.UnityEngine.Random.Range(10000, 100000)
            self.GTextCaijin.text = tostring(caijin)
            self.ChangTime = 0.5
		end
	end
end

function ViewForestParty:onHandleEv(ev)
	if(ev ~= nil)
	then
		self.ForestPartyCommon:HandleEvent(ev)
		if(ev.EventName == "EvEntityForestPartyBetState")
		then
			self:reset()
            local game_result = ControllerForestParty.GameResult
            local lamp = game_result.ListLamps[game_result.ListLamps.Count - 1]
            rotateArrowAndComAnimals(lamp, 0, 0, 0, false)
            self:bet()
		else if(ev.EventName == "EvEntityForestPartyGameEnd")
		then
			self:gameEnd()
		else if(ev.EventName == "EvEntityRecvChatFromForestParty")
		then
            local chat_info = ev.chat_info
            local use_tanmu = true
            if (CS.UnityEngine.PlayerPrefs.HasKey(ViewChat.PlayTanMuKey))
			then
				local return_value = nil
				return_value,use_tanmu = CS.System.bool.TryParse(CS.UnityEngine.PlayerPrefs.GetString(ViewChat.PlayTanMuKey),use_tanmu)
			end
            if (use_tanmu)
			then
				local ui_shootingtext = self.ViewMgr.getView("ShootingText")
                if (ui_shootingtext == nil)
				then
					ui_shootingtext = self.ViewMgr.createView("ShootingText")
                    ui_shootingtext:init(true, false)
				end
                ui_shootingtext:setShootingText(chat_info.sender_name, chat_info.chat_content, chat_info.sender_viplevel)
			end
		end
	end
end

function ViewForestParty:InitForestParty()
	self:reset()
    self.BtnBetPanel.enabled = true
    self.ImageBetPanel.grayed = false
    local game_state = ControllerForestParty.ForestPartyState
    self:changeAllAnimalsState(true)
    if (game_state == CS.Casinos._eForestPartyState.Bet)
	then
		self:lightBlocks()
        local left_time = ControllerForestParty.BetLeftTime
        self:ForestPartyCommon:StartCoutDown()
        self:changeAllAnimalsState(false)
        CS.Casinos.CasinosContext.Instance:Play(self.BetBgMusic, CS.Casinos._eSoundLayer.Background)
	else if(game_state == CS.Casinos._eForestPartyState.GameEnd)
	then
		self:gameEnd()
	end
end

function ViewForestParty:bet()
	CS.Casinos.CasinosContext.Instance:Play(self.BetBgMusic, CS.Casinos._eSoundLayer.Background)
    self.BtnBetPanel.enabled = true
    self.ImageBetPanel.grayed = false
    self.ForestPartyCommon:StartCoutDown()
    self:changeAllAnimalsState(false)
    self:onClickBtnBetPanel()
end

function ViewForestParty:gameEnd()
	CS.Casinos.CasinosContext.Instance:Play(self.RotateMusic, CS.Casinos._eSoundLayer.Background)
    self.BtnBetPanel.enabled = false
    self.ImageBetPanel.grayed = true
    self:changeAllAnimalsState(true)
    self.ForestPartyCommon:StopCoutDown()
    self.TransitionCountDown:Play(
		function()
			self:showLotteryResult()
		end
	)
    self:lightBlocks()
end

function ViewForestParty:showLotteryResult()
	local result = ControllerForestParty.GameResult
    local lottery_type = result.ResultLotteryType
    self.QueResultLamps == {}
	for i = 0 result.ListLamps.Count - 1 do
		table.insert(self.QueResultLamps,result.ListLamps[i])
	end
	local first_lamp = table.remove(self.QueResultLamps,1)
	if(lottery_type == CS.Casinos.LotteryType.Normal)
	then
		self:rotateArrowAndComAnimals(first_lamp, 4, 3, 6)
	else if(lottery_type == CS.Casinos.LotteryType.FanBei)
	then
		self.TweenShake = CS.FairyGUI.StageCamera.main.DOShakePosition(1, 0.2, 50).OnComplete(
			function()
				self:rotateArrowAndComAnimals(first_lamp, 4, 3, 6)
                self.MoveClipShanDian.visible = true
                self.TransitionWait:Play(4, 0, 
					function()
						self.MoveClipShanDian.visible = false
					end
				)
                if (result.Beishu == 2)
				then
					self.TransitionFanBei2:Play()
				else if(result.Beishu == 3)
				then
					self.TransitionFanBei3:Play()
				end
			end
		)
	else if(lottery_type == CS.Casinos.LotteryType.CaiJin)
	then
		self.TransitionRGY:Play(4, 0, nil)
		for i = 0 3 do
			local com_caijin_animal = self.ArrayComAnimals[result.CaijinAnimalIndex[i]]
            local controller_golden = com_caijin_animal:GetController("ControllerGolden")
            controller_golden:SetSelectedIndex(1)
		end
        self.TweenShake = CS.FairyGUI.StageCamera.main.DOShakePosition(1, 0.2, 50).OnComplete(
			function()
				self:rotateArrowAndComAnimals(first_lamp, 4, 3, 6)
                self.UpdateCaiJin = true
			end
		)
	else if(lottery_type == CS.Casinos.LotteryType.SongDeng)
	then
		self.TransitionRGY:Play(10, 0, nil)
        self.TweenShake = CS.FairyGUI.StageCamera.main.DOShakePosition(1, 0.2, 50).OnComplete(
			function()
				self.ComSongDeng.visible = true
                local transition_big = self.ComSongDeng:GetTransition("TransitionBig")
                transition_big:Play(
					function()
						self.TransitionFlashingSongDeng:Play()
						self:rotateArrowAndComAnimals(first_lamp, 4, 3, 6)
						self.UpdateSongDeng = true
					end
				)
			end
		)
	else if(lottery_type == CS.Casinos.LotteryType.DaSanYuan)
	then
		self.TransitionRGY:Play(10, 0, nil)
        self.TweenShake = CS.FairyGUI.StageCamera.main.DOShakePosition(1, 0.2, 50).OnComplete(
			function()
				self.ComDaSanYuan.visible = true
                self.TransitionFlashingDaSanYuan:Play(6, 0, nil)
                self:rotateArrowAndComAnimals(first_lamp, 4, 3, 6)
			end
		)
	else if(lottery_type == CS.Casinos.LotteryType.ZongHengSiHai)
	then
		self.TransitionRGY:Play(10, 0, nil)
        self.ComZongHengSiHai.visible = true
        self.TransitionFlashingZongHengSiHai:Play(5, 0, nil)
        self.TweenShake = CS.FairyGUI.StageCamera.main.DOShakePosition(1, 0.2, 50).OnComplete(
			function()
				self.ControllerZongHengSiHai:SetSelectedIndex(1)
                self:rotateArrowAndComAnimals(first_lamp, 4, 3, 6)
			end
		)
	end
end

function ViewForestParty:lightBlocks()
	local list_blocks = ControllerForestParty.ListBlocks
	for i = 0 list_blocks.Count - 1 do
		local com_lamp = self.ComBlocks:GetChild("Block" .. tostring(i)).asCom
        local loader = com_lamp:GetChild("Loader").asLoader
        loader.url = CS.FairyGUI.UIPackage.GetItemURL("ForestParty", "Block" .. list_blocks[i].ToString())
	end
    
    self.GroupBlockDark.visible = true
    self.TransitionLightBlocks:Play(
		function()
			self.GroupBlockDark.visible = false
		end
	)
end

function ViewForestParty:rotateArrowAndComAnimals(lamp,cycles_arrow,cycles_animals,duration,oncomplete_callback)
	local block_index = lamp.BlockIndex
    local animal_index = lamp.AnimalIndex
    local curRot_arrow = self.ComArrow.rotation % 360
    local tgtRot_arrow = block_index * 15 - 360
	local lerp_arrow = nil
	if(tgtRot_arrow > curRot_arrow)
	then
		lerp_arrow = 360 + curRot_arrow - tgtRot_arrow
	else
	then
		lerp_arrow = curRot_arrow - tgtRot_arrow
	end
    self.TweenRotArrow = self.ComArrow:TweenRotate(self.ComArrow.rotation - (lerp_arrow + 360 * cycles_arrow), duration)
        .SetEase(CS.DG.TweeningEase.InOutQuad)
    local rot_animals = self.ComAnimals.rotation % 360
    local tgtRot_animal = block_index * 15
    local cutRot_animal = rot_animals + animal_index * 15
	local lerp_animals = nil
	if(tgtRot_animal >= cutRot_animal)
	then
		lerp_animals = tgtRot_animal - cutRot_animal
	else
	then
		lerp_animals = 360 + tgtRot_animal - cutRot_animal
	end
    self.TweenRotComAnimals = self.ComAnimals:TweenRotate(self.ComAnimals.rotation + (lerp_animals + 360 * cycles_animals), duration)
        .SetEase(CS.DG.TweeningEaseEase.InOutQuad)
    if (oncomplete_callback ~= nil)
	then
		self.TweenRotComAnimals.OnComplete(
			function()
				self:blockFlashing(lamp)
			end
		)
	end
end

function ViewForestParty:blockFlashing(lamp)
	local block_index = lamp.BlockIndex
    local transition_falshing = self.ArrayComLamps[block_index]:GetTransition("TransitionFlashing")
    transition_falshing:Play(
		function()
			self:showNextLamp(lamp)
		end
	)
    local game_result = ControllerForestParty.GameResult
    if (game_result.ResultLotteryType == CS.Casinos.LotteryType.CaiJin)
	then
		self.UpdateCaiJin = false
        self.GTextCaijin.text = game_result.Caijin.ToString()
	else if(game_result.ResultLotteryType == CS.Casinos.LotteryType.SongDeng)
	then
		self.UpdateSongDeng = false
        self.GTextSongDeng.text = tostring(table.getn(self.QueResultLamps) + 1)
	else if(game_result.ResultLotteryType == CS.Casinos.LotteryType.DaSanYuan)
	then
		local animal_type = lamp.AnimalType
        local animal_movelip = self.ComDaSanYuan:GetChild("Movelip" .. animal_type.ToString()).asMovieClip
        animal_movelip.visible = true
        self.TransitionFlashingDaSanYuan:Stop()
	else if(game_result.ResultLotteryType == CS.Casinos.LotteryType.ZongHengSiHai)
	then
		self.TransitionFlashingZongHengSiHai:Stop(true, false)
        self.TransitionRGY:Stop()
        local color_type = lamp.BlockColor
        self.ComZongHengSiHai:GetChild("Lamp" .. color_type.ToString()).visible = true
		self.ArrayGroupYeZi[CS.Casinos.BlockColor.__CastFrom(color_type)].visible = true
        local group_light = self.ComZongHengSiHai:GetChild("GroupLight" .. color_type.ToString())
        group_light.visible = true
        local transition_light = self.ComZongHengSiHai:GetTransition("TransitionLight" .. color_type.ToString())
        transition_light:Play()
        self.ControllerZongHengSiHai:SetSelectedIndex(0)
	end
end

function ViewForestParty:showNextLamp(lamp)
	local animal_type = lamp.AnimalType
    local animal_index = lamp.AnimalIndex
    local color_type = lamp.BlockColor
    local game_result = ControllerForestParty.GameResult
    CS.Casinos.CasinosContext.Instance:Play("ForestPartyLottery" .. game_result.ResultLotteryType.ToString(), CS.Casinos._eSoundLayer.Background)
    if (game_result.ResultLotteryType == CS.Casinos.LotteryType.Normal ||
        game_result.ResultLotteryType == CS.Casinos.LotteryType.CaiJin ||
        game_result.ResultLotteryType == CS.Casinos.LotteryType.FanBei)
	then
		CS.Casinos.CasinosContext.Instance:Play("ForestParty" .. lamp.BlockColor.ToString() .. lamp.AnimalType.ToString(), CS.Casinos._eSoundLayer.LayerNormal)
        if (game_result.ResultLotteryType == CS.Casinos.LotteryType.Normal)
		then
			self.ArrayTransitionYezi[CS.Casinos.BlockColor.__CastFrom(color_type)]:Play(6, 0, nil)
		end
        self:TransitionRGY:Stop()
        self:showLotteryMultiple(lamp)
        local rot_Animals = (self.ComAnimals.rotation + animal_index * 15) % 360
        if (rot_Animals < 180)
		then
			rot_Animals = -rot_Animals
		else 
		then
			rot_Animals = 360 - rot_Animals
		end
        local com_animal = self.ArrayComAnimals[animal_index]
        local transition_fly = com_animal:GetTransition("TransitionFly")
        local controller_animal = com_animal:GetController("ControllerAnimal")
        local pos_end = self.ComUi:TransformPoint(self.ComArrow.position, self.ComAnimals)
        self.TweenPosAnimal = com_animal:TweenMove(pos_end, 0.5)
        self.TweenRotAnimal = com_animal:TweenRotate(com_animal.rotation + rot_Animals, 0.5f)
        self.TweenPosAnimal:SetAutoKill(false)
        self.TweenRotAnimal:SetAutoKill(false)
        transition_fly:Play(
			function()
				controller_animal:SetSelectedIndex(2)
			end
		)
        controller_animal:SetSelectedIndex(1)
        self.TransitionWait:Play(12, 0,
			function()
				self.TweenPosAnimal:PlayBackwards()
                self.TweenRotAnimal:PlayBackwards()
                transition_fly:PlayReverse(
					function()
						self:addLotteryRecord()
						if (ControllerForestParty.BankerData.Gold <= 0)
						then
							self.MoveClipBaoZhuang.visible = true
							self.MoveClipBaoZhuang:SetPlaySettings(0, -1, 1, -1)
							self.MoveClipBaoZhuang.playing = true
							self.MoveClipBaoZhuang.onPlayEnd:Add(
								function()
									self.MoveClipBaoZhuang.visible = false
									self.ViewMgr.createView("ForestPartyResult")
								end
						else
						then
							self.ViewMgr.createView("ForestPartyResult")
						end
					end
				)
                controller_animal:SetSelectedIndex(1)
                self:hideLotterMultiple()
			end
		)
        self.ForestPartyCommon:PlayClearGoldTransition()
	else
	then
		self.ComMoreLottery.visible = true
        self:addMoreLotteryResult(lamp)
		if(table.getn(self.QueResultLamps) > 0)
		then
			local next_lamp = table.remove(self.QueResultLamps,1)
            self:rotateArrowAndComAnimals(next_lamp, 1, 1, 3)
		else
		then
			self.TransitionWait:Play(2, 0, 
				function()
					if (game_result.ResultLotteryType == CS.Casinos.LotteryType.SongDeng)
					then
						self.ComSongDeng.visible = false
					else if(game_result.ResultLotteryType == CS.Casinos.LotteryType.DaSanYuan)
					then
						local animal_movelip = self.ComDaSanYuan:GetChild("Movelip" .. animal_type.ToString()).asMovieClip
						animal_movelip.visible = false
						self.ComDaSanYuan.visible = false
					else if(game_result.ResultLotteryType == CS.Casinos.LotteryType.ZongHengSiHai)
					then
						self.ArrayGroupYeZi[CS.Casinos.BlockColor.__CastFrom(lamp.BlockColor)].visible = false
						local image_color = self.ComZongHengSiHai:GetChild("Lamp" + color_type.ToString())
						local transition_light = self.ComZongHengSiHai:GetTransition("TransitionLight" .. color_type.ToString())
						local group_light = self.ComZongHengSiHai:GetChild("GroupLight" .. color_type.ToString()).asGroup
						transition_light:Stop()
						group_light.visible = false
						image_color.visible = false
					end
					self.TransitionRGY:Stop()
					self.ComMoreLottery.visible = false
					self.GListMoreLottery:RemoveChildren()
					self:addLotteryRecord()
					if (ControllerForestParty.BankerData.Gold <= 0)
					then
						self.MoveClipBaoZhuang.visible = true
						self.MoveClipBaoZhuang:SetPlaySettings(0, -1, 1, -1)
						self.MoveClipBaoZhuang.playing = true
						self.MoveClipBaoZhuang.onPlayEnd:Add(
							function()
								self.MoveClipBaoZhuang.visible = false
								self.ViewMgr.createView("ForestPartyResult")
							end
						)
					else
					then
						self.ViewMgr.createView("ForestPartyResult")
					end
				end
			)
            self.ForestPartyCommon:PlayClearGoldTransition()
		end
	end
end

function ViewForestParty:addMoreLotteryResult(lamp)
	local animal_type = lamp.AnimalType
    local color = lamp.BlockColor
    local com_morelottery = self.GListMoreLottery:AddItemFromPool().asCom
    local loader_icon = com_morelottery:GetChild("LoaderIcon").asLoader
    loader_icon.url = CS.FairyGUI.UIPackage.GetItemURL("ForestParty", "HistoryRecord" .. animal_type.ToString() .. color.ToString())
    local transition_rotate = com_morelottery:GetTransition("TransitionRotate")
    transition_rotate:Play()
end

function ViewForestParty:showLotteryMultiple(lamp)
	local hang = (int)lamp.BlockColor
    local lie = (int)lamp.AnimalType
    local bet_index = hang * 4 + lie
    local lottery_multiple = UiMgr.CoPlayer.CoPlayerForestParty.MapBetMultiple[bet_index]
    local text_multiple = ComLotteryMultiple.GetChild("TextMultiple").asTextField
    text_multiple.text = lottery_multiple.ToString()
    ComLotteryMultiple.visible = true
end

function ViewForestParty:hideLotterMultiple()
	self.ComLotteryMultiple.visible = false
end

function ViewForestParty:changeAllAnimalsState(isStatic)
	for key value in pairs(self.ArrayComAnimals) do
		local controller_animal = self.ArrayComAnimals[i]:GetController("ControllerAnimal")
        if(isStatic)
		then
			controller_animal:SetSelectedIndex(1)
		else
		then
			controller_animal:SetSelectedIndex(0)
		end
	end
end

function ViewForestParty:addLotteryRecord()
	local game_result = ControllerForestParty.GameResult
    local record = CS.Casinos.LampRecord()
    record.LotteryType = game_result.ResultLotteryType
    if (record.LotteryType == CS.Casinos.LotteryType.Normal)
	then
		record.AnimalType = game_result.ListLamps[0].AnimalType
        record.BlockColor = game_result.ListLamps[0].BlockColor
	end
    self.ForestPartyCommon:addLotteryRecord(record, true)
end

function ViewForestParty:reset()
	for i = 0 23 do
		self.ArrayComAnimals[i].position = self.ArrayAnimalsPos[i]
        self.ArrayComAnimals[i].rotation = self.ArrayAnimalsRot[i]
        local controller_animal = self.ArrayComAnimals[i]:GetController("ControllerAnimal")
        local controller_golden = self.ArrayComAnimals[i]:GetController("ControllerGolden")
        local transition_fly = self.ArrayComAnimals[i]:GetTransition("TransitionFly")
        local transition_block_flashing = self.ArrayComLamps[i]:GetTransition("TransitionFlashing")
        local image_static = self.ArrayComAnimals[i]:GetChild("ImageStatic").asImage
        local image_static_golden = self.ArrayComAnimals[i]:GetChild("ImageStaticGolden").asImage
        image_static:SetSize(64, 85)
        image_static_golden:SetSize(64, 85)
        controller_animal:SetSelectedIndex(0)
        controller_golden:SetSelectedIndex(0)
        transition_fly:Stop(true, false)
        transition_block_flashing:Stop(true, false)
	end
    changeAllAnimalsState(false)
    self.TweenPosAnimal:Kill()
    self.TweenRotAnimal:Kill()
    self.TweenRotArrow:Kill()
    self.TweenRotComAnimals:Kill()
    self.TweenRotComAnimals:Kill()
    self.TransitionWait:Stop(true, false)
    self.TransitionCountDown:Stop(true, false)
    self.TransitionLightBlocks:Stop(true, false)
    self.TransitionRGY:Stop(true, false)
    self.TransitionFanBei2:Stop(true, false)
    self.TransitionFanBei3:Stop(true, false)
    self.TransitionFlashingSongDeng:Stop(true, false)
    self.TransitionFlashingDaSanYuan:Stop(true, false)
    self.TransitionFlashingZongHengSiHai:Stop(true, false)
    self.ArrayTransitionYezi[0]:Stop(true, false)
    self.ArrayTransitionYezi[1]:Stop(true, false)
    self.ArrayTransitionYezi[2]:Stop(true, false)
    self.ArrayGroupYeZi[0].visible = false
    self.ArrayGroupYeZi[1].visible = false
    self.ArrayGroupYeZi[2].visible = false
    self.MoveClipShanDian.visible = false
    self.MoveClipBaoZhuang.visible = false
    self.MoveClipBaoZhuang.playing = false
    self.ComAnimals.rotation = 0
    self.ComArrow.rotation = 0
    self.ComSongDeng.visible = false
    self.ForestPartyCommon:StopClearGoldTransition()
    local transition_big = self.ComSongDeng:GetTransition("TransitionBig")
    transition_big:Stop(true, false)
    self.ComDaSanYuan.visible = false
    self.ComZongHengSiHai.visible = false
    self.UpdateCaiJin = false
    self.UpdateSongDeng = false
    self.GListMoreLottery:RemoveChildren()
    self.ComMoreLottery.visible = false
    self.ComLotteryMultiple.visible = false
    local movelip_lion = self.ComDaSanYuan:GetChild("MovelipLion").asMovieClip
    movelip_lion.visible = false
    local movelip_panda = self.ComDaSanYuan:GetChild("MovelipPanda").asMovieClip
    movelip_panda.visible = false
    local moveliplion_monkey = self.ComDaSanYuan:GetChild("MovelipMonkey").asMovieClip
    moveliplion_monkey.visible = false
    local movelip_rabbit = self.ComDaSanYuan:GetChild("MovelipRabbit").asMovieClip
    movelip_rabbit.visible = false
    self.ControllerZongHengSiHai:SetSelectedIndex(0)
    local image_red = self.ComZongHengSiHai:GetChild("LampRed").asImage
    image_red.visible = false
    local image_green = self.ComZongHengSiHai:GetChild("LampGreen").asImage
    image_green.visible = false
    local image_yellow = self.ComZongHengSiHai:GetChild("LampYellow").asImage
    image_yellow.visible = false
    if (self.TweenShake ~= nil)
	then
		self.TweenShake:Kill()
	end
    local view_result = self.ViewMgr.getView("ForestPartyResult")
    if (view_result ~= nil)
	then
		self.ViewMgr.destroyView(view_result)
	end
end

function ViewForestParty:onClickBtnMenu()
	self.ViewMgr.createView("ForestPartyMenu")
end

function ViewForestParty:onClickBtnBetPanel()
	self.ViewMgr.createView("ForestPartyBet")
end