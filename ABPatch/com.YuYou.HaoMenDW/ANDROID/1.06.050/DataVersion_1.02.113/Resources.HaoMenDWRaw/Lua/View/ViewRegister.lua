ViewRegister = ViewBase:new()

function ViewRegister:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	
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

function ViewRegister:onCreate()
	self.IsRegister = true
    local co_back = self.ComUi:GetChild("ComBack").asCom
    co_back.onClick:Add(
		function()
			self:_onClose()
		end
	)
    self.ControllerRegister = self.ComUi:GetController("ControllerRegister")

    self.GBtnRegister = self.ComUi:GetChild("BtnReg").asButton
    self.GBtnRegister.onClick:Add(
		function()
			 self:_register(true)
		end
	)
    self.GBtnBindWeiChat = self.ComUi:GetChild("BtnBindWeiChat").asButton
    self.GBtnBindWeiChat.onClick:Add(
		function()
			self:_register(false)
		end
	)

    local co_acc = self.ComUi:GetChild("CoInputAcc").asCom
    self.GTextAcc = co_acc:GetChild("InputText").asTextInput
    self.GTextAcc.maxLength = 32
    local co_pwd = self.ComUi:GetChild("CoInputPwd").asCom
    self.GTextPwd = co_pwd:GetChild("InputText").asTextInput
    self.GTextPwd.displayAsPassword = true
    self.GTextPwd.maxLength = 16
    local co_superpwd = self.ComUi:GetChild("CoInputPwdConfirm").asCom
    self.GTextPwdConfirm = co_superpwd:GetChild("InputText").asTextInput
    self.GTextPwdConfirm.displayAsPassword = true
    self.GTextPwdConfirm.maxLength = 16

    if (CS.Casinos.CasinosContext.Instance.UseBindPhoneAndWeiChat)
    then
        local co_securitycode = self.ComUi:GetChild("ComSecurityCode")
        if (co_securitycode ~= nil)
        then
            self.UiSecurityCode = UiSecurityCode:new(co_securitycode.asCom, "", 
			function()
				self:_checkGetPwdInput()
			end
			,
			function()
				self:_checkGetPwdInput()
			end
			)
        end
    end

    self.GTextAcc.onChanged:Set(
		function()
			self:_checkGetPwdInput()
		end
	)
    self.GTextPwd.onChanged:Set(
		function()
			self:_checkGetPwdInput()
		end
	)
    self.GTextPwdConfirm.onChanged:Set(
		function()
			self:_checkGetPwdInput()
		end
	)
end

function ViewRegister:onDestroy()	
   local view = ViewRegister:new(nil)	
   local ev = view.ViewMgr:getEv("EvRegisterDestroy")
   if(ev == nil)
   then
   	  ev  = EvRegisterDestroy:new(nil)
   end   
   view.ViewMgr:sendEv(ev)
end

function ViewRegister:onUpdate(tm)		
	if (self.UiSecurityCode ~= nil)
    then
       self.UiSecurityCode:update(elapsed_tm)
    end
end

function ViewRegister:onHandleEv(ev)	
end

function ViewRegister:isRegister(is_register)        
  self.IsRegister = is_register
  if(self.isRegister)
  then
	self.ControllerRegister.selectedIndex = 0
  else
	self.ControllerRegister.selectedIndex = 1
  end
  
  self:_checkGetPwdInput()
end
        
function ViewRegister:_onClose()   
   local view_register = self.ViewMgr.getView("Register") 
   self.ViewMgr.destroyView(view_register)     
end
 
function ViewRegister:_register(is_register)
   local is_valid_acc = CS.Casinos.CasinoHelper.IsValidStr(self.GTextAcc.text)
   if (is_valid_acc == false)
   then
       CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg("用户名仅可以使用数字，大小写英文以及@.-_")
       return
   end

   local is_valid_pwd = CS.Casinos.CasinoHelper.IsValidStr(self.GTextPwd.text)
   if (is_valid_pwd == false)
   then
       CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg("密码仅可以使用数字，大小写英文以及@.-_")
       return
   end

   local is_valid_pwd_confirm = CS.Casinos.CasinoHelper.IsValidStr(self.GTextPwdConfirm.text)
   if (is_valid_pwd_confirm == false)
   then
       CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg("密码仅可以使用数字，大小写英文以及@.-_")
       return
   end

   if (self.GTextPwdConfirm.text ~= self.GTextPwd.text)
   then
       CS.Casinos.UiHelperCasinos.UiShowInfoFailed("两次输入密码不一致!")
       return
   end
   
   local phone_num = nil
   local security_code = nil
   if (self.UiSecurityCode ~= nil)
   then
       phone_num = self.UiSecurityCode:getPhoneNum()
       security_code = self.UiSecurityCode:getSecurityCode()
   end
   local ev = self.ViewMgr:getEv("EvUiLoginClickBtnRegister")
   if(ev == nil)
   then
   	ev  = EvUiLoginClickBtnRegister:new(nil)
   end
   ev.AccountName = self.GTextAcc.text
   ev.Password = self.GTextPwd.text
   ev.SuperPassword = self.GTextPwdConfirm.text
   ev.remeber_pwd = true
   ev.Email = ""
   ev.Identity = ""
   ev.Phone = ""
   ev.Name = ""
   ev.Device = nil
   ev.PhoneNum = phone_num
   ev.SecurityCode = security_code
   ev.IsRegister = is_register
   self.ViewMgr:sendEv(ev)
end
        
function ViewRegister:_checkGetPwdInput()
   local btn_enabled = false   
   if (self.UiSecurityCode ~= nil)
   then
	   local phone_num = self.UiSecurityCode:getPhoneNum()
	   local security_code = self.UiSecurityCode:getSecurityCode()
       if ((self.GTextAcc ~= nil and string.len(self.GTextAcc.text) > 0)
			and (self.GTextPwd ~= nil and string.len(self.GTextPwd.text) > 0)
            and (self.GTextPwdConfirm ~= nil and string.len(self.GTextPwdConfirm.text) > 0)
			and (phone_num ~= nil and string.len(phone_num) > 0)
            and (security_code ~= nil and string.len(security_code) > 0))
       then
           btn_enabled = true
       end
   else   
       if ((self.GTextAcc ~= nil and string.len(self.GTextAcc.text) > 0)
			and (self.GTextPwd ~= nil and string.len(self.GTextPwd.text) > 0)
            and (self.GTextPwdConfirm ~= nil and string.len(self.GTextPwdConfirm.text) > 0))
       then
           btn_enabled = true
       end
   end

   if (self.IsRegister == true)
   then
	   if(btn_enabled == true)
	   then
			self.GBtnRegister.alpha = 1
	   else
			self.GBtnRegister.alpha = 0.5
	   end       
       self.GBtnRegister.enabled = btn_enabled   
   else   
	   if(btn_enabled == true)
	   then
			self.GBtnBindWeiChat.alpha = 1
	   else
			self.GBtnBindWeiChat.alpha = 0.5
	   end              
       self.GBtnBindWeiChat.enabled = btn_enabled
   end
end


ViewRegisterFactory = ViewFactory:new()

function ViewRegisterFactory:new(o,ui_package_name,ui_component_name,
	ui_layer,is_single,fit_screen)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self
	self.PackageName = ui_package_name
	self.ComponentName = ui_component_name
	self.UILayer = ui_layer
	self.IsSingle = is_single
	self.FitScreen = fit_screen
    return o
end

function ViewRegisterFactory:createView()	
	local view = ViewRegister:new(nil)	
	return view
end