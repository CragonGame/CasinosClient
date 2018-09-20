UiChooseCountryCode = {}

function UiChooseCountryCode:new(view)
    local o = {}
    setmetatable(o, self)
    self.__index = self
    o.ViewLogin = view
    local com_choose = view.ComUi:GetChild("ComCountryCodeChoose").asCom
    o.ControllerChooseCountryCode = view.ComUi:GetController("ControllerChooseCountryCode")
    o.TextSearchCountry = com_choose:GetChild("TextSearchCountry").asTextInput
    o.TextSearchCountry.promptText = string.format("[color=#999999]%s[/color]",view.ViewMgr.LanMgr:getLanValue("SearchCountry"))
    o.TextSearchCountry.onChanged:Set(
            function()
                o:_checkInput()
            end
    )
    o.ListCountryCode = com_choose:GetChild("ListCountryCode").asList

    o.BtnCancel = com_choose:GetChild("Lan_Btn_Cancel").asButton
    o.BtnCancel.onClick:Add(
            function()
                o:_onClickBtnCancel()
            end
    )

    o:createListItem(PhoneCountryCode)

    local current_code = "CurrentCountryCode"
    local c_k = ""
    if (CS.UnityEngine.PlayerPrefs.HasKey(current_code))
    then
        local s = CS.UnityEngine.PlayerPrefs.GetString(current_code)
        c_k = s
    else
        if CS.Casinos.CasinosContext.Instance.IsEditor == false then
            c_k = CS.NativeFun.getCountryCode()
        --else
        --    c_k = "CN"
        end
    end

    if CS.System.String.IsNullOrEmpty(c_k) then
        c_k = "CN"
    end

    o:setCurrentCode(c_k,true)

    return o
end

function UiChooseCountryCode:setCurrentCode(key,set_self)
    self.CountryKey = key
    local v = PhoneCountryCode[key]
    if v == nil then
        v = PhoneCountryCode["CN"]
    end
    self.CountryCode = v["Code"]
    local t = {}
    table.insert(t, key)
    table.insert(t, " ")
    table.insert(t, "+")
    table.insert(t, v["Code"])
    self.KeyAndCodeFormat = table.concat(t)

    CS.UnityEngine.PlayerPrefs.SetString("CurrentCountryCode", key)
    if set_self == false then
        self:_onClickBtnCancel()
    end
end

function UiChooseCountryCode:show()
    self.ControllerChooseCountryCode.selectedIndex = 1
end

function UiChooseCountryCode:_onClickBtnCancel()
    self.ControllerChooseCountryCode.selectedIndex = 0
end

function UiChooseCountryCode:_checkInput()
    local country = self.TextSearchCountry.text

end

function UiChooseCountryCode:createListItem(list_item)
    self.ListCountryCode:RemoveChildrenToPool()
    local l = #list_item
    local index = 1
    for k, v in pairs(list_item) do
        local com = self.ListCountryCode:AddItemFromPool().asCom
        local item = ItemCountryCode:new(nil, com, self.ViewLogin.ViewMgr, self, index == l)
        item:setDetail(k, v)
        index = index + 1
    end
end