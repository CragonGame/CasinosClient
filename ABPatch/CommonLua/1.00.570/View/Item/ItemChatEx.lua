ItemChatEx = {}

function ItemChatEx:new(o)
	o = o or {}
	setmetatable(o, self)
	self.__index = self

	return o
end

function ItemChatEx:setObj(com)
	local co_vip = com:GetChild("CoVIP")
	if (co_vip ~= nil)
	then
		self.ViewVIPSign = ViewVIPSign:new(nil,co_vip.asCom)
	end
	self.Com = com
	self.ControllerLeft = self.Com:GetController("ControllerLeft")
	self.ControllerShowBg = self.Com:GetController("ControllerShowBg")
	self.GGroupLeft = self.Com:GetChild("GroupLeft").asGroup
	self.GRichTextLeft = self.Com:GetChildInGroup(self.GGroupLeft, "ChatContent").asRichTextField
	local bg1 = self.Com:GetChildInGroup(self.GGroupLeft, "ChatBg1")
	if (bg1 ~= nil)
	then
		self.GImageBg1 = bg1.asImage
	end
	local bg2 = self.Com:GetChildInGroup(self.GGroupLeft, "ChatBg2")
	if (bg2 ~= nil)
	then
		self.GImageBg2 = bg2.asImage
	end
	self.GGroupRight = self.Com:GetChild("GroupRight").asGroup
	self.GRichTextRight = self.Com:GetChildInGroup(self.GGroupRight, "ChatContent").asRichTextField
	local group_center = self.Com:GetChild("GroupCenter")
	if (group_center ~= nil)
	then
		self.GGroupCenter = group_center.asGroup
		self.GRichTextCenter = self.Com:GetChildInGroup(self.GGroupCenter, "ChatContent").asRichTextField
	end
end

function ItemChatEx:setChat(sender_nameex, chat_contentex, sender_viplevel,sender_tm,sender_name_color_code, content_color_code,show_left,
							init_width, item_type,name_content_same_line,show_bg,show_nickname,is_tmshow,show_bg1,content_size)
	if(name_content_same_line == nil)
	then
		name_content_same_line = true
	end
	if(show_bg == nil)
	then
		show_bg = false
	end
	if(show_nickname == nil)
	then
		show_nickname = true
	end
	if(is_tmshow == nil)
	then
		is_tmshow = false
	end
	if(show_bg1 == nil)
	then
		show_bg1 = true
	end
	if(content_size == nil)
	then
		content_size = 0
	end
	self.ItemType = item_type
	local group_parent = self.GGroupLeft
	if (is_tmshow)
	then
		self.ControllerLeft.selectedIndex = 2
		local d_tm = CS.System.DateTime.Parse(sender_tm)
		self.GRichTextCenter.text = CS.Casinos.LuaHelper.DataTimeToString(d_tm,"yyyy.MM.dd HH:mm")
		return
	else
		if(show_left == false)
		then
			group_parent = self.GGroupRight
		end
		if(show_left == true)
		then
			self.ControllerLeft.selectedIndex = 0
		else
			self.ControllerLeft.selectedIndex = 1
		end
		if (self.GImageBg1 ~= nil)
		then
			ViewHelper:setGObjectVisible(show_bg1, self.GImageBg1)
			local show_bg2 = true
			if(show_bg1 == true)
			then
				show_bg2 = false
			end
			ViewHelper:setGObjectVisible(show_bg2, self.GImageBg2)
		end
	end

	if (self.ControllerShowBg ~= nil)
	then
		if(show_bg)
		then
			self.ControllerShowBg.selectedIndex = 0
		else
			self.ControllerShowBg.selectedIndex = 1
		end
	end
	local sender_color_code = "5DC1FF"
	local tm_color_code = "999999"
	if(sender_name_color_code ~= nil and string.len(sender_color_code) > 0)
	then
		sender_color_code = sender_name_color_code
	end
	if (self.ViewVIPSign ~=nil)
	then
		self.ViewVIPSign:setVIPLevel(sender_viplevel)
		init_width = init_width - self.ViewVIPSign.GCoVIP.width
	else
		if (sender_viplevel > 0)
		then
			sender_nameex = sender_nameex .. "(VIP)"
		end
	end
	local sender_name = nil
	local sender_tm_ex = nil
	if (sender_tm ~= CS.System.DateTime.MinValue)
	then
		sender_name = "[color=#" .. sender_color_code .."]" ..sender_nameex .."[/color]"
		sender_tm_ex = "[color=#" .. tm_color_code .."][size=26]" .. CS.Casinos.UiHelper:getLocalTmToString(sender_tm) .. "[/size][/color]"
	else
		sender_name = "[color=#" .. sender_color_code .."]" ..sender_nameex .. ":[/color]"
	end
	if(content_color_code == nil or string.len(content_color_code) == 0)
	then
		content_color_code = "FFFFFF"
	end
	local chat_content = CS.ChatParser.inst:Parse("[color=#" .. content_color_code .. "]" .. chat_contentex .. "[/color]")
	if((sender_tm_ex ~= nil and string.len(sender_tm_ex) > 0) or name_content_same_line == false)
	then
		if (show_nickname)
		then
			chat_content = CS.Casinos.CasinosContext.Instance:AppendStrWithSB(sender_name, "    ", sender_tm_ex, "\n", " " .. chat_content)
		end
	else
		if (show_nickname)
		then
			chat_content = CS.Casinos.CasinosContext.Instance:AppendStrWithSB(sender_name, " " .. chat_content)
		end
	end
	if (self.ItemType == CS.Casinos._eChatItemType.Marquee)
	then
		self.GRichTextLeft.autoSize = CS.FairyGUI.AutoSizeType.Both
	end
	if (content_size > 0)
	then
		chat_content = "[size=" .. content_size .. "]" ..chat_content .. "[/size]"
	end
	self.GRichTextLeft.width = init_width
	self.GRichTextLeft.text = chat_content
	self.GRichTextLeft.width = self.GRichTextLeft.textWidth
	self.GRichTextRight.width = init_width
	self.GRichTextRight.text = chat_content
	self.GRichTextRight.width = self.GRichTextRight.textWidth
	if (self.ViewVIPSign ~= nil)
	then
		local text_x = self.ViewVIPSign.GCoVIP.x
		if (sender_viplevel > 0)
		then
			text_x = self.ViewVIPSign.GCoVIP.x + self.ViewVIPSign.GCoVIP.width
		end
		self.GRichTextLeft.x = text_x;
	end
end


function ItemChatEx:moveItem(move_speed, pos, moveend_callback)
	self.MoveEndCallback = moveend_callback
	self.Com.position = pos
	local to_x = self.GRichTextLeft.width
	if (self.ViewVIPSign ~= nil)
	then
		to_x = to_x + self.ViewVIPSign.GCoVIP.width
	end
	local move_tm = (pos.x + to_x) / move_speed
	self.TweenerMove = self.Com:TweenMoveX(-to_x, move_tm):SetEase(CS.DG.Tweening.Ease.Linear):OnComplete(
			function()
				if self.MoveEndCallback ~= nil then
					self.MoveEndCallback(self, self.ItemType)
					self.MoveEndCallback = nil
				end
			end
	)
end

function ItemChatEx:reset()
	local pos = CS.UnityEngine.Vector3()
	pos.x = 10000
	pos.y = 10000
	pos.z = 10000
	self.Com.position = pos
	ViewHelper:setGObjectVisible(false, self.Com)
	self.GRichTextLeft.text = nil
	self.GRichTextLeft.color = CS.UnityEngine.Color.white
	if (self.ViewVIPSign ~= nil)
	then
		self.ViewVIPSign:setVIPLevel(0)
	end
	self.MoveEndCallback = nil
end

function ItemChatEx:reset1()
	self.ViewVIPSign = nil
	self.Com = nil
	self.ControllerLeft = nil
	self.ControllerShowBg = nil
	self.GGroupLeft = nil
	self.GRichTextLeft = nil
	self.GImageBg1 = nil
	self.GImageBg2 = nil
	self.GGroupRight = nil
	self.GRichTextRight = nil
	self.GGroupCenter = nil
	self.GRichTextCenter = nil
	self.MoveEndCallback = nil
end