ViewLogin = ViewBase:new()

function ViewLogin:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    self.ViewMgr = nil
    self.GoUi = nil
    self.ComUi = nil
    self.Panel = nil
    self.UILayer = nil
    self.InitDepth = nil
    self.ViewKey = nil
    self.AgreeAgreement = true

    return o
end

function ViewLogin:onCreate()
    self.ViewMgr:bindEvListener("EvUiChooseCountry", self)
    local c_icon = self.ComUi:GetController("ControllerShowIcon")
    c_icon.selectedIndex = 1
    self:_switchController("LoginState", "LoginMain")
    ViewHelper:UiEndWaiting()
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    local controller_wechat = self.ComUi:GetController("ControllerWeChat")
    if (controller_wechat ~= nil)
    then
        if (self.CasinosContext.ShowWeiChat == true)
        then
            controller_wechat.selectedIndex = 0
        else
            controller_wechat.selectedIndex = 1
        end
    end

    local btn_otherlogin = self.ComUi:GetChild("BtnOtherLogin")
    if (btn_otherlogin ~= nil)
    then
        btn_otherlogin.asButton.onClick:Add(
                function()
                    self:_switchLoginState()
                end
        )
    end

    local btn_otherloginex = self.ComUi:GetChild("BtnOtherLoginEx")
    if (btn_otherloginex ~= nil)
    then
        btn_otherloginex.asButton.onClick:Add(
                function()
                    self:_switchLoginState()
                end
        )
    end

    local btn_weixin = self.ComUi:GetChild("BtnWeiXin")
    if (btn_weixin ~= nil)
    then
        btn_weixin.asButton.onClick:Add(
                function()
                    self:_onClickWeiXin()
                end
        )
    end

    local com_shadelogin = self.ComUi:GetChild("CoShadeLogin")
    if (com_shadelogin ~= nil)
    then
        com_shadelogin.asCom.onClick:Add(
                function()
                    self:_switch2LoginMain()
                end
        )
    end

    self.BtnLogin = self.ComUi:GetChild("Lan_Btn_Login1")
    if (self.BtnLogin ~= nil)
    then
        self.BtnLogin.asButton.onClick:Add(
                function()
                    self:_onClickBtnLogin()
                end
        )
    end

    local btn_show_register = self.ComUi:GetChild("Lan_Btn_Register")
    if (btn_show_register ~= nil)
    then
        btn_show_register.asButton.onClick:Add(
                function()
                    self:_onClickBtnShowRegister()
                end
        )
    end

    local btn_guestaccess = self.ComUi:GetChild("BtnGuest")
    if (btn_guestaccess ~= nil)
    then
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
    if (btn_fogetpwd ~= nil)
    then
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
    self.GTextInputAccLogin.promptText = string.format("[color=#999999]%s[/color]",self.ViewMgr.LanMgr:getLanValue("EnterPhone"))
    self.GTextInputAccLogin.onChanged:Set(
            function()
                self:_checkloginInput()
            end
    )

    local com_input_pwdlogin = self.ComUi:GetChild("InputPwdLogin").asCom
    self.GTextInputPwdLogin = com_input_pwdlogin:GetChild("InputPwdRegister").asTextInput
    self.GTextInputPwdLogin.promptText = string.format("[color=#999999]%s[/color]",self.ViewMgr.LanMgr:getLanValue("EnterPwdTips1"))
    self.GTextInputPwdLogin.onChanged:Set(
            function()
                self:_checkloginInput()
            end
    )

    local group_agreement = self.ComUi:GetChild("Agreement")
    if (group_agreement ~= nil)
    then
        local btn_returnagreement = self.ComUi:GetChildInGroup(group_agreement.asGroup, "Lan_Btn_Return")
        if (btn_returnagreement ~= nil)
        then
            btn_returnagreement.asButton.onClick:Add(
                    function()
                        self:_onClickBtnReturn()
                    end
            )
        end
    end

    local text_version = self.ComUi:GetChild("Version")
    if (text_version ~= nil)
    then
        self.GTextVersion = text_version.asTextField
    end

    local server_state = self.ComUi:GetChild("SeverStateInfo")
    if (server_state ~= nil)
    then
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

    local p_helper = ParticleHelper:new(nil)
    local com_bg = self.ComUi:GetChild("ComBg")
    local image_bg = com_bg:GetChild("ImageMote").asImage
    if (self.CasinosContext.NeedHideClientUi == false)
    then
        image_bg.visible = false
        local abTextAtlas = p_helper:GetSpine("Spine/LoadingMarry/mary_loading.atlas.ab")
        local atlas = abTextAtlas:LoadAsset("Mary_Loading.atlas")
        local abtexture = p_helper:GetSpine("Spine/LoadingMarry/mary_loading.ab")
        local texture = abtexture:LoadAsset("Mary_Loading")
        local abjson = p_helper:GetSpine("Spine/LoadingMarry/mary_loadingjson.ab")
        local json = abjson:LoadAsset("Mary_LoadingJson")

        self.PlayerAnim = CS.Casinos.SpineHelper.LoadResourcesPrefab(atlas, texture, json, "Spine/Skeleton", 314)
        --local moteParent = self.ComUi:GetChild("MoteParent").asCom
        self.HolderMote = self.ComUi:GetChild("HolderMote").asGraph
        --self.PlayerAnim.transform.position = moteParent.displayObject.gameObject.transform.position
        self.PlayerAnim.transform.localScale = CS.Casinos.LuaHelper.GetVector3(70, 70, 1000)
        --self.PlayerAnim.transform.gameObject.layer = moteParent.displayObject.gameObject.layer
        self.PlayerAnim:Initialize(false)
        self.PlayerAnim.loop = true
        self.MoteRender = self.PlayerAnim.transform.gameObject:GetComponent("MeshRenderer")
        self.PlayerAnim.AnimationName = "animation"
        self.MoteRender.sortingOrder = 316
        self.PlayerAnim.transform.gameObject.name = "LoadingMote"
        self.HolderMote:SetNativeObject(CS.FairyGUI.GoWrapper(self.PlayerAnim.transform.gameObject))
    else
        image_bg.visible = true
    end

    local abTextAtlas1 = p_helper:GetSpine("Spine/DengLong/denglong.atlas.ab")
    local atlas1 = abTextAtlas1:LoadAsset("denglong.atlas")
    local abtexture1 = p_helper:GetSpine("Spine/DengLong/denglong.ab")
    local texture1 = abtexture1:LoadAsset("denglong")
    local abjson1 = p_helper:GetSpine("Spine/DengLong/denglongjson.ab")
    local json1 = abjson1:LoadAsset("denglongJson")

    self.DengLongAnim = CS.Casinos.SpineHelper.LoadResourcesPrefab(atlas1, texture1, json1, "Spine/Skeleton", 314)
    local denglongParent = self.ComUi:GetChild("DengLongParent").asCom
    self.DengLongAnim.transform.position = denglongParent.displayObject.gameObject.transform.position
    self.DengLongAnim.transform.localScale = CS.Casinos.LuaHelper.GetVector3(1.1, 1.1, 1.1)
    self.DengLongAnim.transform.gameObject.layer = denglongParent.displayObject.gameObject.layer
    self.DengLongAnim:Initialize(false)
    self.DengLongAnim.loop = true
    self.DengLongRender = self.DengLongAnim.transform.gameObject:GetComponent("MeshRenderer")
    self.DengLongAnim.AnimationName = "animation"
    self.DengLongRender.sortingOrder = 316
    self.DengLongAnim.transform.gameObject.name = "DengLong"
    local bg = com_bg:GetChild("bg")
    ViewHelper:makeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH,ViewMgr.STANDARD_HEIGHT, self.ComUi.width, self.ComUi.height, bg.width, bg.height,bg,BgAttachMode.Center,{self.HolderMote})

    self.ComboChooseUCenter = self.ComUi:GetChild("ComboChooseUCenter").asComboBox
    self.ComboChooseGateWay = self.ComUi:GetChild("ComboChooseGateWay").asComboBox
    local show_combo = false
    if CS.Casinos.CasinosContext.Instance.IsDev then
        show_combo = true
        local items = {}
        items[1] = "http://ucenter.cragon.cn"
        items[2] = "http://ucenterdev.cragon.cn"
        self.ComboChooseUCenter.items = items
        self.ComboChooseUCenter.onChanged:Add(
                function()
                    self:onClickUCenter()
                end
        )
        self.ComboChooseUCenter.text = items[1]

        local items1 = {}
        items1[1] = "king-gateway.cragon.cn"
        items1[2] = "king-gateway-dev.cragon.cn"
        items1[3] = "223.104.212.140"
        self.ComboChooseGateWay.items = items1
        self.ComboChooseGateWay.onChanged:Add(
                function()
                    self:onClickGateWay()
                end
        )
        self.ComboChooseGateWay.text = items1[1]
    end

    self.Tips = self.ComUi:GetChild("Tips")

    ViewHelper:setGObjectVisible(show_combo, self.ComboChooseUCenter)
    ViewHelper:setGObjectVisible(show_combo, self.ComboChooseGateWay)
end

function ViewLogin:onClickUCenter()
    local ev = self.ViewMgr:getEv("EvUiChooseUCenter")
    if (ev == nil)
    then
        ev = EvUiChooseUCenter:new(nil)
    end
    ev.ucenter = self.ComboChooseUCenter.text
    self.ViewMgr:sendEv(ev)
end

function ViewLogin:onClickGateWay()
    local ev = self.ViewMgr:getEv("EvUiChooseGateWay")
    if (ev == nil)
    then
        ev = EvUiChooseGateWay:new(nil)
    end
    ev.gateway = self.ComboChooseGateWay.text
    self.ViewMgr:sendEv(ev)
end

function ViewLogin:onDestroy()
    if (self.CasinosContext.NeedHideClientUi == false)
    then
        CS.UnityEngine.GameObject.Destroy(self.PlayerAnim.transform.gameObject)
    end
    CS.UnityEngine.GameObject.Destroy(self.DengLongAnim.transform.gameObject)
end

function ViewLogin:onUpdate(tm)
    if self.UiRegister ~= nil then
        self.UiRegister:OnUpdate(tm)
    end
    if self.UiResetPwd ~= nil then
        self.UiResetPwd:OnUpdate(tm)
    end
end

function ViewLogin:onHandleEv(ev)
    if (ev ~= nil)
    then
        if (ev.EventName == "EvUiChooseCountry")
        then
            if self.UiRegister~=nil then
                self.UiRegister:setCurrentCountryCode(ev.CountryKey,ev.CountryCode,ev.KeyAndCodeFormat)
            end
            if self.UiResetPwd~=nil then
                self.UiResetPwd:setCurrentCountryCode(ev.CountryKey,ev.CountryCode,ev.KeyAndCodeFormat)
            end
            self.TextCountryCode.text = ev.KeyAndCodeFormat
        end
    end
end

function ViewLogin:_switchController(controller_name, page_name)
    local controller = self.ComUi:GetController(controller_name)
    controller:SetSelectedPage(page_name)
end

function ViewLogin:SetVersionAndServerStateInfo(bundle_version, data_version, server_is_invalid, serverstate_info)
    if (self.GTextVersion ~= nil)
    then
        local version_tips = self.ViewMgr.LanMgr:getLanValue("AppVersion") .. "：%s " .. self.ViewMgr.LanMgr:getLanValue("DataVersion") .. "：%s"
        local en = " Pro"
        if string.find(GatewayIp,"dev") then
            en = " Dev"
        end
        self.GTextVersion.text = string.format(version_tips, bundle_version, data_version) .. en
    end

    if (self.GTextServerState ~= nil)
    then
        self.GTextServerState.visible = server_is_invalid
        self.GTextServerState.text = serverstate_info
    end
end

function ViewLogin:SetAccPwd(acc, pwd)
    self.TextCountryCode.text = self.UiChooseCountryCode.KeyAndCodeFormat
    self.GTextInputAccLogin.text = acc
    self.GTextInputPwdLogin.text = pwd
    self:_checkloginInput()
end

function ViewLogin:Switch2DlgLogin(acc, pwd)
    if (self:_hasAgreeAgreement() == false)
    then
        return
    end
    self:_switchController("LoginState", "Login")
    self:SetAccPwd(acc, pwd)
end

function ViewLogin:Switch2RegisterCode()
    if (self:_hasAgreeAgreement() == false)
    then
        return
    end
    self:_switchController("LoginState", "RegisterCode")
end

function ViewLogin:Switch2ResetPwd()
    if (self:_hasAgreeAgreement() == false)
    then
        return
    end
    self:_switchController("LoginState", "ResetPwd")
end

function ViewLogin:Switch2ResetPwdCode()
    if (self:_hasAgreeAgreement() == false)
    then
        return
    end
    self:_switchController("LoginState", "ResetPwdCode")
end

function ViewLogin:Switch2Logining()
    self:_switchController("LoginState", "Logining")
    self.Tips.text = self.ViewMgr.LanMgr:getLanValue("Logining")
end

-- 点击登录按钮
function ViewLogin:_onClickBtnLogin()
    if (self:_hasAgreeAgreement() == false)
    then
        return
    end
    local ev = self.ViewMgr:getEv("EvUiLogin")
    if (ev == nil)
    then
        ev = EvUiLogin:new(nil)
    end
    ev.login_type = 0
    ev.acc = self.UiChooseCountryCode.CountryCode .. self.GTextInputAccLogin.text
    ev.pwd = self.GTextInputPwdLogin.text
    ev.remeber_pwd = true
    ev.phone = self.GTextInputAccLogin.text
    self.ViewMgr:sendEv(ev)
end

-- 游客Access
function ViewLogin:_onClickBtnGuestAccess()
    if (self:_hasAgreeAgreement() == false)
    then
        return
    end
    if (self.CasinosContext.ServerIsInvalid)
    then
        ViewHelper:UiShowInfoSuccess(self.CasinosContext.ServerStateInfo)
        return
    end

    local ev = self.ViewMgr:getEv("EvUiLogin")
    if (ev == nil)
    then
        ev = EvUiLogin:new(nil)
    end
    ev.login_type = 1
    ev.acc = ""
    ev.pwd = ""
    ev.remeber_pwd = true
    ev.phone = ""

    self.ViewMgr:sendEv(ev)
end

function ViewLogin:_onClickWeiXin()
    if (self:_hasAgreeAgreement() == false)
    then
        return
    end
    if (self.CasinosContext.ServerIsInvalid)
    then
        ViewHelper:UiShowInfoSuccess(self.CasinosContext.ServerStateInfo)
        return
    end

    local ev = self.ViewMgr:getEv("EvUiLogin")
    if (ev == nil)
    then
        ev = EvUiLogin:new(nil)
    end
    ev.login_type = 2
    ev.acc = ""
    ev.pwd = ""
    ev.remeber_pwd = true
    ev.phone = ""

    self.ViewMgr:sendEv(ev)
end

function ViewLogin:_onClickBtnForgetPwd()
    self:Switch2ResetPwd()
end

-- 切换到注册对话框
function ViewLogin:_onClickBtnShowRegister()
    if (self:_hasAgreeAgreement() == false)
    then
        return
    end
    self:_switchController("LoginState", "Register")
end

-- 返回登录对话框
function ViewLogin:_onClickBtnReturn()
    self:_switchController("LoginState", "Login")
end

function ViewLogin:_switchLoginState()
    if (self:_hasAgreeAgreement() == false)
    then
        return
    end
    if (self.CasinosContext.ServerIsInvalid)
    then
        ViewHelper:UiShowInfoSuccess(self.CasinosContext.ServerStateInfo)
        return
    end

    self:_switchController("LoginState", "Login")
end

function ViewLogin:_onClickBtnAgreement()
    self:_switchController("LoginState", "Agreement")
end

function ViewLogin:_checkloginInput()
    if (self.GTextInputAccLogin == nil or self.GTextInputPwdLogin == nil)
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
end

function ViewLogin:_onClickBtnAgree()
    if (self.AgreeAgreement == true)
    then
        self.AgreeAgreement = false
    else
        self.AgreeAgreement = true
    end
end

function ViewLogin:_onClickComLink()
    self.ViewMgr:createView("About")
end

function ViewLogin:_switch2LoginMain()
    self:_switchController("LoginState", "LoginMain")
end

function ViewLogin:_hasAgreeAgreement()
    if (self.AgreeAgreement == true)
    then
        return true
    else
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("AgreePrivacyStatement"))
        return false
    end
end

ViewLoginFactory = ViewFactory:new()

function ViewLoginFactory:new(o, ui_package_name, ui_component_name,
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

function ViewLoginFactory:createView()
    local view = ViewLogin:new(nil)
    return view
end