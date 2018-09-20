-- 已废弃，原先用于手机验证码。

ViewSecurityCode = {}

function ViewSecurityCode:new(o, gco_ui, phone_num, phone_num_changed, code_changed,view_mgr)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	if(self.Instance==nil)
	then
		self.Instance = o
	end
	self.ViewMgr = view_mgr
	self.SendCodeCD = 60
	self.GComSecurityCode = gco_ui
    self:init(nil, nil, nil, nil, phone_num, phone_num_changed, code_changed)
    return self.Instance
end

function ViewSecurityCode:UiSecurityCode(btn_send_code, gtext_sendcode_tips, input_code, phone_num, code_changed)
	self:init(btn_send_code, gtext_sendcode_tips, input_code, nil, phone_num, nil, code_changed)
end

function ViewSecurityCode:updateCom(gco_ui, phone_num, phone_num_changed, code_changed)
	self.GComSecurityCode = gco_ui
    self:init(nil, nil, nil, nil, phone_num, phone_num_changed, code_changed)
end

function ViewSecurityCode:updateCom(btn_send_code, gtext_sendcode_tips, input_code, phone_num, code_changed)
	self:init(btn_send_code, gtext_sendcode_tips, input_code, nil, phone_num, nil, code_changed)
end

function ViewSecurityCode:update(tm)
	if (self.CanSendCode == false)
	then
		self.SendCodeCountdown = self.SendCodeCountdown - tm
        if (self.SendCodeCountdown <= 0)
		then
			self.SendCodeCountdown = self.SendCodeCD
            self.CanSendCode = true
            self:setTips("发送验证码")
		else
			self:setTips(tostring(self.SendCodeCountdown))
		end
	end
end

function ViewSecurityCode:getPhoneNum()
	if(self.GInputPhoneNum == nil)
	then
		return self.PhoneNum
	else
		return self.GInputPhoneNum.text
	end
end

function ViewSecurityCode:getSecurityCode()
	return self.GInputCode.text
end

function ViewSecurityCode:setTips(tips)
	self.GTextSendCodeTips.text = tips
end

function ViewSecurityCode:onClickBtnSendCode()
	local ev = self.ViewMgr.getEv("EvUiSendSecurityCode")
	if(ev == nil)
	then
		ev = EvUiSendSecurityCode:new(nil)
	end
	ev.phone_num = self:getPhoneNum()
	self.ViewMgr.sendEv(ev)
    self:setTips(tostring(self.SendCodeCountdown))
    self.CanSendCode = false
    self.GBtnSendCode.enabled = false
end

function ViewSecurityCode:phoneNumChanged()
	local is_valid_phonenum = CS.Casinos.CasinoHelper.IsValidPhoneNum(self:getPhoneNum())
    self.GBtnSendCode.enabled = is_valid_phonenum
end

function ViewSecurityCode:init(btn_send_code, gtext_sendcode_tips, input_code, input_phonenum, phone_num, phone_num_changed, code_changed)
	self.CanSendCode = true
    self.SendCodeCountdown = self.SendCodeCD
    self.PhoneNumChanged = phone_num_changed
    self.CodeChanged = code_changed
    self.PhoneNum = phone_num

    if (self.GComSecurityCode ~= nil)
	then
		local send_code = self.GComSecurityCode:GetChild("BtnSendCode")
        if (send_code ~= nil)
		then
			self.GBtnSendCode = send_code.asButton
            self.GTextSendCodeTips = self.GComSecurityCode:GetChild("SendCodeTips").asTextField
		end
		if(self.PhoneNum == nil or string.len(self.PhoneNum) <= 0)
		then
			local phone_numex = self.GComSecurityCode:GetChild("CoInputPhoneNum")
            if (phone_numex ~= nil)
			then
				 local co_phone_num = phone_numex.asCom
                 self.GInputPhoneNum = co_phone_num:GetChild("InputText").asTextInput
			end
		end
        local code = self.GComSecurityCode:GetChild("CoInputCode")
        if (code ~= nil)
	    then
		    local co_code = code.asCom
            self.GInputCode = co_codeGetChild("InputText").asTextInput
		end
	else 
		self.GBtnSendCode = btn_send_code
        self.GTextSendCodeTips = gtext_sendcode_tips
        self.GInputCode = input_code
        self.GInputPhoneNum = input_phonenum
	end
    if (self.GBtnSendCode ~= nil)
	then
		self.GBtnSendCode.onClick:Add(
			function()
				self:onClickBtnSendCode()
			end
		)
        self.GBtnSendCode.enabled = false
	end
    if (self.GInputPhoneNum ~= nil)
	then
		self.GInputPhoneNum.maxLength = 11
        self.GInputPhoneNum.onChanged:Add(
			function()
				self:phoneNumChanged()
			end
		)
        if (self.PhoneNumChanged ~= nil)
		then
			self.GInputPhoneNum.onChanged:Add(
				function()
					self:PhoneNumChanged()
				end
			)
		end
	end
    if (self.GInputCode ~= nil)
	then
		self.GInputCode.maxLength = 11
        if (self.CodeChanged ~= nil)
		then
			self.GInputCode.onChanged:Add(
				function()
					self:CodeChanged()
				end
			)
		end
	end
end
