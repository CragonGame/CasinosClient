-- Copyright(c) Cragon. All rights reserved.
-- 登录界面

---------------------------------------
ViewLogin = class(ViewBase)

---------------------------------------
function ViewLogin:ctor()
    self.AgreeAgreement = true
    self.TimerUpdate = nil
end

---------------------------------------
function ViewLogin:OnCreate()
    self.ViewMgr:BindEvListener("EvUiChooseCountry", self)

    self:_switchController("LoginState", "LoginMain")
    ViewHelper:UiEndWaiting()

    local controller_wechat = self.ComUi:GetController("ControllerWeChat")
    if (controller_wechat ~= nil) then
        if (self.Context.Cfg.ClientShowWechat == true) then
            controller_wechat.selectedIndex = 0
        else
            controller_wechat.selectedIndex = 1
        end
    end

    local btn_otherlogin = self.ComUi:GetChild("BtnOtherLogin")
    if (btn_otherlogin ~= nil) then
        btn_otherlogin.asButton.onClick:Add(
                function()
                    self:_switchLoginState()
                end
        )
    end

    local btn_otherloginex = self.ComUi:GetChild("BtnOtherLoginEx")
    if (btn_otherloginex ~= nil) then
        btn_otherloginex.asButton.onClick:Add(
                function()
                    self:_switchLoginState()
                end
        )
    end

    local btn_weixin = self.ComUi:GetChild("BtnWeiXin")
    if (btn_weixin ~= nil) then
        btn_weixin.asButton.onClick:Add(
                function()
                    self:_onClickWeiXin()
                end
        )
    end

    local com_shadelogin = self.ComUi:GetChild("CoShadeLogin")
    if (com_shadelogin ~= nil) then
        com_shadelogin.asCom.onClick:Add(
                function()
                    self:_switch2LoginMain()
                end
        )
    end

    self.BtnLogin = self.ComUi:GetChild("Lan_Btn_Login1")
    if (self.BtnLogin ~= nil) then
        self.BtnLogin.asButton.onClick:Add(
                function()
                    self:_onClickBtnLogin()
                end
        )
    end

    local btn_show_register = self.ComUi:GetChild("Lan_Btn_Register")
    if (btn_show_register ~= nil) then
        btn_show_register.asButton.onClick:Add(
                function()
                    self:_onClickBtnShowRegister()
                end
        )
    end

    local btn_guestaccess = self.ComUi:GetChild("BtnGuest")
    if (btn_guestaccess ~= nil) then
        btn_guestaccess.asButton.onClick:Add(
                function()
                    self:_onClickBtnGuestAccess()
                end
        )
    end

    local btn_guestaccessex = self.ComUi:GetChild("BtnGuestEx").asButton
    btn_guestaccessex.onClick:Add(
            function()
                self:_onClickBtnGuestAccess()
            end
    )

    local btn_fogetpwd = self.ComUi:GetChild("Lan_Btn_ResetPwd")
    if (btn_fogetpwd ~= nil) then
        btn_fogetpwd.asButton.onClick:Add(
                function()
                    self:_onClickBtnForgetPwd()
                end
        )
    end

    self.UiChooseCountryCode = UiChooseCountryCode:new(self)
    self.UiRegister = UiRegister:new(self)
    self.UiResetPwd = UiResetPwd:new(self)

    local com_input_acclogin = self.ComUi:GetChild("ComCountryCordLogin").asCom
    local com_text_code = com_input_acclogin:GetChild("ComTextCountryCode").asCom
    com_text_code.onClick:Add(
            function()
                self.UiChooseCountryCode:show()
            end)
    self.TextCountryCode = com_text_code:GetChild("TextCountryCord").asTextField
    self.GTextInputAccLogin = com_input_acclogin:GetChild("InputAccLogin").asTextInput
    self.GTextInputAccLogin.promptText = string.format("[color=#999999]%s[/color]", self.ViewMgr.LanMgr:getLanValue("EnterPhone"))
    self.GTextInputAccLogin.onChanged:Set(
            function()
                self:_checkloginInput()
            end
    )

    local com_input_pwdlogin = self.ComUi:GetChild("InputPwdLogin").asCom
    self.GTextInputPwdLogin = com_input_pwdlogin:GetChild("InputPwdRegister").asTextInput
    self.GTextInputPwdLogin.promptText = string.format("[color=#999999]%s[/color]", self.ViewMgr.LanMgr:getLanValue("EnterPwdTips1"))
    self.GTextInputPwdLogin.onChanged:Set(
            function()
                self:_checkloginInput()
            end
    )

    local group_agreement = self.ComUi:GetChild("Agreement")
    if (group_agreement ~= nil) then
        local btn_returnagreement = self.ComUi:GetChildInGroup(group_agreement.asGroup, "Lan_Btn_Return")
        if (btn_returnagreement ~= nil) then
            btn_returnagreement.asButton.onClick:Add(
                    function()
                        self:_onClickBtnReturn()
                    end
            )
        end
    end

    local text_version = self.ComUi:GetChild("Version")
    if (text_version ~= nil) then
        self.GTextVersion = text_version.asTextField
    end

    local server_state = self.ComUi:GetChild("SeverStateInfo")
    if (server_state ~= nil) then
        self.GTextServerState = server_state.asTextField
    end

    local btn_agree = self.ComUi:GetChild("BtnAgree").asButton
    btn_agree.onClick:Add(
            function()
                self:_onClickBtnAgree()
            end
    )
    local com_link = self.ComUi:GetChild("ComLink").asCom
    com_link.onClick:Add(
            function()
                self:_onClickComLink()
            end
    )

    local com_bg = self.ComUi:GetChild("ComBg")
    local image_bg = com_bg:GetChild("ImageMote").asImage
    if (self.Context.Cfg.NeedHideClientUi == false) then
        image_bg.visible = false

        local loadingmarry_anim = nil
        if (self.CasinosContext.PathMgr.DirLaunchAbType == CS.Casinos.DirType.Raw) then
            local ab_path_prefix = self.CasinosContext.PathMgr.DirAbRoot .. 'Spine/'
            loadingmarry_anim = self.CasinosContext.SpineMgr:CreateSpineObjFromAb(ab_path_prefix, 'LoadingMarry', 'Mary_Loading.atlas', 'Mary_Loading', 'Mary_LoadingJson', 'Spine/Skeleton')
        else
            local res_prefix = 'Resources.KingTexasLaunch/LoadingMarry/'
            loadingmarry_anim = self.CasinosContext.SpineMgr:CreateSpineObjFromRes(res_prefix, 'Mary_Loading.atlas', 'Mary_Loading', 'Mary_LoadingJson', 'Spine/Skeleton')
        end

        loadingmarry_anim.transform.localScale = CS.Casinos.LuaHelper.GetVector3(70, 70, 1000)
        loadingmarry_anim:Initialize(false)
        loadingmarry_anim.loop = true
        loadingmarry_anim.AnimationName = "animation"
        loadingmarry_anim.transform.gameObject.name = "LoadingMote"
        local loadingmarry_render = loadingmarry_anim.transform.gameObject:GetComponent("MeshRenderer")
        loadingmarry_render.sortingOrder = 4

        local loadingmarry_holder = self.ComUi:GetChild("HolderMote").asGraph
        loadingmarry_holder:SetNativeObject(CS.FairyGUI.GoWrapper(loadingmarry_anim.transform.gameObject))
    else
        image_bg.visible = true
    end

    local denglong_anim = nil
    if (self.CasinosContext.PathMgr.DirLaunchAbType == CS.Casinos.DirType.Raw) then
        local ab_path_prefix = self.CasinosContext.PathMgr.DirAbRoot .. 'Spine/'
        denglong_anim = self.CasinosContext.SpineMgr:CreateSpineObjFromAb(ab_path_prefix, 'DengLong', 'denglong.atlas', 'denglong', 'denglongJson', 'Spine/Skeleton')
    else
        local res_prefix = 'Resources.KingTexasLaunch/DengLong/'
        denglong_anim = self.CasinosContext.SpineMgr:CreateSpineObjFromRes(res_prefix, 'denglong.atlas', 'denglong', 'denglongJson', 'Spine/Skeleton')
    end

    local denglong_parent = self.ComUi:GetChild("DengLongParent").asCom
    denglong_anim.transform.parent = denglong_parent.displayObject.gameObject.transform
    denglong_anim.transform.localPosition = CS.Casinos.LuaHelper.GetVector3(-10, -90, -318)
    denglong_anim.transform.localScale = CS.Casinos.LuaHelper.GetVector3(90, 90, 90)
    denglong_anim.transform.gameObject.layer = denglong_parent.displayObject.gameObject.layer
    denglong_anim:Initialize(false)
    denglong_anim.loop = true
    denglong_anim.transform.gameObject.name = "DengLong"
    denglong_anim.AnimationName = "animation"
    local denglong_render = denglong_anim.transform.gameObject:GetComponent("MeshRenderer")
    denglong_render.sortingOrder = 4

    local bg = com_bg:GetChild("bg")
    ViewHelper:MakeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH, ViewMgr.STANDARD_HEIGHT, self.ComUi.width, self.ComUi.height, bg.width, bg.height, bg, BgAttachMode.Center, { self.HolderMote })

    self.ComboChooseUCenter = self.ComUi:GetChild("ComboChooseUCenter").asComboBox
    self.ComboChooseGateWay = self.ComUi:GetChild("ComboChooseGateWay").asComboBox
    local show_combo = false
    --if IsDev then
    --    show_combo = true
    --    local items = {}
    --    items[1] = "http://ucenter.cragon.cn"
    --    items[2] = "http://ucenterdev.cragon.cn"
    --    self.ComboChooseUCenter.items = items
    --    self.ComboChooseUCenter.onChanged:Add(
    --            function()
    --                self:onClickUCenter()
    --            end
    --    )
    --    self.ComboChooseUCenter.text = items[1]
    --
    --    local items1 = {}
    --    items1[1] = "king-gateway.cragon.cn"
    --    items1[2] = "king-gateway-dev.cragon.cn"
    --    items1[3] = "223.104.212.140"
    --    self.ComboChooseGateWay.items = items1
    --    self.ComboChooseGateWay.onChanged:Add(
    --            function()
    --                self:onClickGateway()
    --            end
    --    )
    --    self.ComboChooseGateWay.text = items1[1]
    --end

    ViewHelper:SetGObjectVisible(show_combo, self.ComboChooseUCenter)
    ViewHelper:SetGObjectVisible(show_combo, self.ComboChooseGateWay)

    self.Tips = self.ComUi:GetChild("Tips")

    -- 显示版本信息
    local version_bundle = self.CasinosContext.Config.VersionBundle
    local version_data = self.CasinosContext.Config.VersionDataPersistent
    self:SetVersionAndServerStateInfo(version_bundle, version_data, self.Context.Cfg.ServerState, self.Context.Cfg.ServerStateInfo)

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(100, self, self._timerUpdate)
end

---------------------------------------
function ViewLogin:OnDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
end

---------------------------------------
function ViewLogin:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvUiChooseCountry") then
            if self.UiRegister ~= nil then
                self.UiRegister:setCurrentCountryCode(ev.CountryKey, ev.CountryCode, ev.KeyAndCodeFormat)
            end
            if self.UiResetPwd ~= nil then
                self.UiResetPwd:setCurrentCountryCode(ev.CountryKey, ev.CountryCode, ev.KeyAndCodeFormat)
            end
            self.TextCountryCode.text = ev.KeyAndCodeFormat
        end
    end
end

---------------------------------------
function ViewLogin:_timerUpdate(tm)
    if self.UiRegister ~= nil then
        self.UiRegister:Update(tm)
    end
    if self.UiResetPwd ~= nil then
        self.UiResetPwd:Update(tm)
    end
end

---------------------------------------
function ViewLogin:onClickUCenter()
    local ev = self.ViewMgr:GetEv("EvUiChooseUCenter")
    if (ev == nil) then
        ev = EvUiChooseUCenter:new(nil)
    end
    ev.ucenter = self.ComboChooseUCenter.text
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewLogin:onClickGateway()
    local ev = self.ViewMgr:GetEv("EvUiChooseGateWay")
    if (ev == nil) then
        ev = EvUiChooseGateWay:new(nil)
    end
    ev.gateway = self.ComboChooseGateWay.text
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewLogin:_switchController(controller_name, page_name)
    local controller = self.ComUi:GetController(controller_name)
    controller:SetSelectedPage(page_name)
end

---------------------------------------
function ViewLogin:SetVersionAndServerStateInfo(bundle_version, data_version, server_is_invalid, serverstate_info)
    if (self.GTextVersion ~= nil) then
        local version_tips = self.ViewMgr.LanMgr:getLanValue("AppVersion") .. "：%s " .. self.ViewMgr.LanMgr:getLanValue("DataVersion") .. "：%s"
        local version_info = string.format(version_tips, bundle_version, data_version) .. self.Context.Cfg.Env
        print(version_info)
        self.GTextVersion.text = version_info
    end

    if (self.GTextServerState ~= nil) then
        self.GTextServerState.visible = server_is_invalid
        self.GTextServerState.text = serverstate_info
    end
end

---------------------------------------
function ViewLogin:SetAccPwd(acc, pwd)
    self.TextCountryCode.text = self.UiChooseCountryCode.KeyAndCodeFormat
    self.GTextInputAccLogin.text = acc
    self.GTextInputPwdLogin.text = pwd
    self:_checkloginInput()
end

---------------------------------------
function ViewLogin:Switch2LoginMain()
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    self:_switchController("LoginState", "LoginMain")
end

---------------------------------------
function ViewLogin:Switch2LoginPhone(acc, pwd)
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    self:_switchController("LoginState", "Login")
    self:SetAccPwd(acc, pwd)
end

---------------------------------------
function ViewLogin:Switch2Logining()
    self:_switchController("LoginState", "Logining")
    self.Tips.text = self.ViewMgr.LanMgr:getLanValue("Logining")
end

---------------------------------------
function ViewLogin:Switch2RegisterCode()
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    self:_switchController("LoginState", "RegisterCode")
end

---------------------------------------
function ViewLogin:Switch2ResetPwd()
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    self:_switchController("LoginState", "ResetPwd")
end

---------------------------------------
function ViewLogin:Switch2ResetPwdCode()
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    self:_switchController("LoginState", "ResetPwdCode")
end

---------------------------------------
-- 点击登录按钮
function ViewLogin:_onClickBtnLogin()
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    local ev = self.ViewMgr:GetEv("EvUiLogin")
    if (ev == nil) then
        ev = EvUiLogin:new(nil)
    end
    ev.login_type = 0
    ev.acc = self.UiChooseCountryCode.CountryCode .. self.GTextInputAccLogin.text
    ev.pwd = self.GTextInputPwdLogin.text
    ev.remeber_pwd = true
    ev.phone = self.GTextInputAccLogin.text
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
-- 游客Access
function ViewLogin:_onClickBtnGuestAccess()
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    if (self.Context.Cfg.ServerState > 0) then
        ViewHelper:UiShowInfoSuccess(self.Context.Cfg.ServerStateInfo)
        return
    end

    local ev = self.ViewMgr:GetEv("EvUiLogin")
    if (ev == nil) then
        ev = EvUiLogin:new(nil)
    end
    ev.login_type = 1
    ev.acc = ""
    ev.pwd = ""
    ev.remeber_pwd = true
    ev.phone = ""

    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewLogin:_onClickWeiXin()
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    if (self.Context.Cfg.ServerState > 0) then
        ViewHelper:UiShowInfoSuccess(self.Context.Cfg.ServerStateInfo)
        return
    end

    local ev = self.ViewMgr:GetEv("EvUiLogin")
    if (ev == nil) then
        ev = EvUiLogin:new(nil)
    end
    ev.login_type = 2
    ev.acc = ""
    ev.pwd = ""
    ev.remeber_pwd = true
    ev.phone = ""

    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewLogin:_onClickBtnForgetPwd()
    self:Switch2ResetPwd()
end

---------------------------------------
-- 切换到注册对话框
function ViewLogin:_onClickBtnShowRegister()
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    self:_switchController("LoginState", "Register")
end

---------------------------------------
-- 返回登录对话框
function ViewLogin:_onClickBtnReturn()
    self:_switchController("LoginState", "Login")
end

---------------------------------------
function ViewLogin:_switchLoginState()
    if (self:_hasAgreeAgreement() == false) then
        return
    end
    if (self.Context.Cfg.ServerState > 0) then
        ViewHelper:UiShowInfoSuccess(self.Context.Cfg.ServerStateInfo)
        return
    end

    self:_switchController("LoginState", "Login")
end

---------------------------------------
function ViewLogin:_onClickBtnAgreement()
    self:_switchController("LoginState", "Agreement")
end

---------------------------------------
function ViewLogin:_checkloginInput()
    if (self.GTextInputAccLogin == nil or self.GTextInputPwdLogin == nil) then
        return
    end

    if ((self.GTextInputAccLogin ~= nil and string.len(self.GTextInputAccLogin.text) > 0)
            and (self.GTextInputPwdLogin ~= nil and string.len(self.GTextInputPwdLogin.text) > 0)) then
        self.BtnLogin.alpha = 1
        self.BtnLogin.enabled = true
    else
        self.BtnLogin.alpha = 0.5
        self.BtnLogin.enabled = false
    end
end

---------------------------------------
function ViewLogin:_onClickBtnAgree()
    if (self.AgreeAgreement == true) then
        self.AgreeAgreement = false
    else
        self.AgreeAgreement = true
    end
end

---------------------------------------
function ViewLogin:_onClickComLink()
    self.ViewMgr:CreateView("About")
end

---------------------------------------
function ViewLogin:_switch2LoginMain()
    self:_switchController("LoginState", "LoginMain")
end

---------------------------------------
function ViewLogin:_hasAgreeAgreement()
    if (self.AgreeAgreement == true) then
        return true
    else
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("AgreePrivacyStatement"))
        return false
    end
end

---------------------------------------
ViewLoginFactory = class(ViewFactory)

---------------------------------------
function ViewLoginFactory:CreateView()
    local view = ViewLogin:new()
    return view
end