ViewLogin = ViewBase:new()

function ViewLogin:new(o)
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

function ViewLogin:onCreate()
	self.ViewMgr:bindEvListener("EvRegisterDestroy",self)
	self:_switchController("LoginState", "LoginMain")

    local controller_wechat = self.ComUi:GetController("ControllerWeChat")
    if (controller_wechat ~= nil)
    then
		if(CS.Casinos.CasinosContext.Instance.ShowWeiChat == true)
		then
			controller_wechat.selectedIndex = 0
		else
			controller_wechat.selectedIndex = 1
		end
    end
    
	local btn_otherlogin = self.ComUi:GetChild("BtnAccount")
	if(btn_otherlogin ~= nil)
	then		
		btn_otherlogin.asButton.onClick:Add(
			function()
				self:_switchLoginState()
			end
		)
	end    

    local btn_weixin = self.ComUi:GetChild("BtnWeChat")
    if (btn_weixin ~= nil)
    then        
        btn_weixin.asButton.onClick:Add(
			function()
				self:_onClickWeiXin()
			end
		)
    end
	
	local com_login = self.ComUi:GetChild("ComLogin").asCom
	local com_shadelogin = com_login:GetChild("ComBack")
	if(com_shadelogin ~= nil)
	then
		com_shadelogin.asCom.onClick:Add(
			function()
				self:_switch2LoginMain()
			end
		)
	end  

	self.BtnLogin = com_login:GetChild("BtnLogin")
	if(self.BtnLogin ~= nil)
	then		
		self.BtnLogin.asButton.onClick:Add(
			function()
				self:_onClickBtnLogin()
			end
		)
	end
	
    local obj_input_acclogin = com_login:GetChild("ComInputAcc")
	if(obj_input_acclogin ~= nil)
	then		
		self.GTextInputAccLogin = obj_input_acclogin:GetChild("InputText").asTextInput
		self.GTextInputAccLogin.maxLength = 32
		self.GTextInputAccLogin.onChanged:Set(
			function()
				self:_checkloginInput()
			end
		)
	end
    
	local obj_input_pwdlogin = com_login:GetChild("ComInputPwd")
	if(obj_input_pwdlogin ~= nil)
	then
		self.GTextInputPwdLogin = obj_input_pwdlogin:GetChild("InputText").asTextInput    
		self.GTextInputPwdLogin.displayAsPassword = true
        self.GTextInputPwdLogin.maxLength = 16
		self.GTextInputPwdLogin.onChanged:Set(
			function()
				self:_checkloginInput()
			end
		)    
	end    
    self.GBtnRemeberPwd = com_login:GetChild("BtnRemeberPwd").asButton	
	local btn_fogetpwd = com_login:GetChild("BtnResetPwd")
	if(btn_fogetpwd ~= nil)
	then
		btn_fogetpwd.asButton.onClick:Add(self._onClickBtnForgetPwd)	
	end

    local btn_show_register = self.ComUi:GetChild("BtnRegister")
	if(btn_show_register ~= nil)
	then
		btn_show_register.asButton.onClick:Add(
			function()
				self:_onClickBtnShowRegister()
			end
		)
	end        

	local btn_guestaccess = self.ComUi:GetChild("BtnGuest")
	if(btn_guestaccess ~= nil)
	then		
		btn_guestaccess.asButton.onClick:Add(
			function()
				self:_onClickBtnGuestAccess()
			end
		)
	end

	local co_commonbg = self.ComUi:GetChild("ComLoadingBg").asCom
    co_commonbg.width = self.ComUi.width
    co_commonbg.height = self.ComUi.height
    CS.Casinos.UiHelperCasinos.SetLoadingBgParticle(co_commonbg)
    
    local text_version = self.ComUi:GetChild("Version")
    if (text_version ~= nil)
    then
        self.GTextVersion = text_version.asTextField
    end

	local server_state = self.ComUi:GetChild("SeverStateInfo")
    if (server_state ~= null)
    then
        self.GTextServerState = server_state.asTextField
    end
end

function ViewLogin:onDestroy()		
   local view_register = self.ViewMgr.getView("Register") 
   if(view_register ~= nil)
   then
	   self.ViewMgr.destroyView(view_register)
   end
end

function ViewLogin:onUpdate(tm)		
end

function ViewLogin:onHandleEv(ev)	
	if(ev ~= nil)
	then
		if(ev.EventName == "EvRegisterDestroy")
		then		
			self:_switchController("LoginState", "LoginMain");	
		end
	end
end

function ViewLogin:_switchController(controller_name,page_name)
    local controller = self.ComUi:GetController(controller_name)
    controller:SetSelectedPage(page_name)
end

function ViewLogin:SetVersionAndServerStateInfo(bundle_version, data_version,server_is_invalid,serverstate_info)
    if (self.GTextVersion ~= nil)    
	then
        self.GTextVersion.text = string.format("应用版本：%s 数据版本：%s", bundle_version, data_version)
    end

	if(self.GTextServerState ~= nil)
	then
		 self.GTextServerState.visible = server_is_invalid
         self.GTextServerState.text = serverstate_info
	end
end

function ViewLogin:SetAccPwd(acc, pwd)
	local view = ViewLogin:new(nil)
    view.GTextInputAccLogin.text = acc
    view.GTextInputPwdLogin.text = pwd
    view:_checkloginInput()
end

function ViewLogin:Switch2DlgLogin(acc, pwd)
	local view = ViewLogin:new(nil)
    view:_switchController("LoginState", "Login")
    view:SetAccPwd(acc, pwd)
end

-- 点击登录按钮
function ViewLogin:_onClickBtnLogin()	
	local is_valid_acc = CS.Casinos.CasinoHelper.IsValidStr(self.GTextInputAccLogin.text)
    if (is_valid_acc == false)   
	then
        CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg("用户名仅可以使用数字，大小写英文以及@.-_")
        return
    end

    local is_valid_pwd = CS.Casinos.CasinoHelper.IsValidStr(self.GTextInputPwdLogin.text)
    if (is_valid_pwd == false)
    then
        CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg("密码仅可以使用数字，大小写英文以及@.-_")
        return
    end

	local ev = self.ViewMgr:getEv("EvUiLogin")
	if(ev == nil)
	then
		ev  = EvUiLogin:new(nil)
	end
	ev.login_type = 0
	ev.acc = self.GTextInputAccLogin.text
	ev.pwd = self.GTextInputPwdLogin.text
	ev.remeber_pwd = self.GBtnRemeberPwd.selected
	self.ViewMgr:sendEv(ev)

	local remeber_pwd_key = CS.Casinos.IUiLogin.RemeberPwdTitle .. self.GTextInputAccLogin.text
    CS.UnityEngine.PlayerPrefs.SetString(remeber_pwd_key, tostring(self.GBtnRemeberPwd.selected))
end

-- 游客Access
function ViewLogin:_onClickBtnGuestAccess()
	if (CS.Casinos.CasinosContext.Instance.ServerIsInvalid)
    then
       CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg(CS.Casinos.CasinosContext.Instance.ServerStateInfo)
       return
    end
	
	local ev = self.ViewMgr:getEv("EvUiLogin")
	if(ev == nil)
	then
		ev  = EvUiLogin:new(nil)
	end
	ev.login_type = 1
	ev.acc = ""
	ev.pwd = ""
	ev.remeber_pwd = true
	self.ViewMgr:sendEv(ev)
end

function ViewLogin:_onClickWeiXin()
	if (CS.Casinos.CasinosContext.Instance.ServerIsInvalid)
    then
       CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg(CS.Casinos.CasinosContext.Instance.ServerStateInfo)
       return
    end
	
	local ev = self.ViewMgr:getEv("EvUiLogin")
	if(ev == nil)
	then
		ev  = EvUiLogin:new(nil)
	end
	ev.login_type = 2
	ev.acc = ""
	ev.pwd = ""
	ev.remeber_pwd = true
	self.ViewMgr:sendEv(ev)
end
        
function ViewLogin:_onClickBtnForgetPwd()
    -- CasinosContext.Instance.UiMgr.createUi<IUiResetPwd>()
end

-- 切换到注册对话框
function ViewLogin:_onClickBtnShowRegister()
	 if (CS.Casinos.CasinosContext.Instance.ServerIsInvalid)
     then
        CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg(CS.Casinos.CasinosContext.Instance.ServerStateInfo)
        return
     end
	 
     local view_register = self.ViewMgr.createView("Register")    
     view_register:isRegister(true)
     self:_switchController("LoginState", "Login")
end

--[[ 返回登录对话框
function ViewLogin:_onClickBtnReturn()
	local view = ViewLogin:new(nil)
    view:_switchController("LoginState", "Login")
end--]]

function ViewLogin:_switchLoginState()
	if (CS.Casinos.CasinosContext.Instance.ServerIsInvalid)
    then
       CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg(CS.Casinos.CasinosContext.Instance.ServerStateInfo)
       return
    end
	
    self:_switchController("LoginState", "Login")
	local tran = self.ComUi:GetTransition("AniLogin")
    tran:Play()
end

--[[-- 点击注册按钮
function ViewLogin:_onClickBtnRegister()
	if (CS.Casinos.CasinosContext.Instance.ServerIsInvalid)
    then
       CS.Casinos.UiHelperCasinos.UiShowPermanentPosMsg(CS.Casinos.CasinosContext.Instance.ServerStateInfo)
       return
    end

	local view = ViewLogin:new(nil)
	local ev = view.ViewMgr:getEv("EvUiLoginClickBtnRegister")
	if(ev == nil)
	then
		ev  = EvUiLoginClickBtnRegister:new(nil)
	end
	ev.AccountName = view.GTextInputAccRegister.text
	ev.Password = view.GTextInputPwdRegister.text
	ev.SuperPassword = view.GTextInputSuperPwdRegister.text
	ev.remeber_pwd = true
	ev.Email = ""
    ev.Identity = ""
    ev.Phone = ""
    ev.Name = ""
    ev.Device = nil
	view.ViewMgr:sendEv(ev)
end
        
function ViewLogin:_onClickBtnAgreement()
	local view = ViewLogin:new(nil)
    view:_switchController("LoginState", "Agreement")
end--]]

function ViewLogin:_checkloginInput()    	
	if(self.GTextInputAccLogin == nil or self.GTextInputPwdLogin == nil)
	then
		return
	end
	
    if ((self.GTextInputAccLogin ~= nil and string.len(self.GTextInputAccLogin.text) > 0)
		and (self.GTextInputPwdLogin ~= nil and string.len(self.GTextInputPwdLogin.text) > 0))
    then
        self.BtnLogin.alpha = 1
        self.BtnLogin.enabled = true    
    else    
        self.BtnLogin.alpha = 0.5
        self.BtnLogin.enabled = false
    end

	local remeber_pwd = false
    if ((self.GTextInputAccLogin ~= nil and string.len(self.GTextInputAccLogin.text) > 0))
    then
        local remeber_pwd_key = CS.Casinos.IUiLogin.RemeberPwdTitle .. self.GTextInputAccLogin.text
        if (CS.UnityEngine.PlayerPrefs.HasKey(remeber_pwd_key))
        then
			remeber_pwd = true
        end
    end
    self.GBtnRemeberPwd.selected = remeber_pwd
end

--[[function ViewLogin:_checkRegisterInput() 
	local view = ViewLogin:new(nil)
	if(view.GTextInputAccRegister == nil or view.GTextInputPwdRegister == nil
		or view.GTextInputSuperPwdRegister == nil)
	then
		return
	end

    if ((view.GTextInputAccRegister ~= nil and string.len(view.GTextInputAccRegister.text) > 0)
		and (view.GTextInputPwdRegister ~= nil and string.len(view.GTextInputPwdRegister.text) > 0)
        and (view.GTextInputSuperPwdRegister ~= nil and string.len(view.GTextInputSuperPwdRegister.text) > 0))
    then
        view.BtnRegister.alpha = 1
        view.BtnRegister.enabled = true    
    else    
        view.BtnRegister.alpha = 0.5
        view.BtnRegister.enabled = false
    end
end--]]

function ViewLogin:_switch2LoginMain()	
	local tran = self.ComUi:GetTransition("AniLogin")
    tran:PlayReverse(
		function()
			self:_switchController("LoginState", "LoginMain")
		end
	)
end


ViewLoginFactory = ViewFactory:new()

function ViewLoginFactory:new(o,ui_package_name,ui_component_name,
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

function ViewLoginFactory:createView()	
	local view = ViewLogin:new(nil)	
	return view
end