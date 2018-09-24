-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
LanMgr = {}

---------------------------------------
function LanMgr:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil)
    then
        self.Instance = o
        self.CasinosContext = CS.Casinos.CasinosContext.Instance
        self.TbDataMgr = TbDataMgr:new(nil)
        self.LanBase = nil
        self.CurrentLan = "ChineseSimplified"
        self.SystemLanguage = "ChineseSimplified"
        self.LanKey = "LanKey"
        self.LanTitleSign = "Lan"
        self:_checkAndCreateLanBase()
    end

    return self.Instance
end

---------------------------------------
function LanMgr.refreshLan()
    local lan_mgr = LanMgr:new(nil)
    lan_mgr:_checkAndCreateLanBase()
end

---------------------------------------
function LanMgr:parseLanKeyValue()
    if (self.CasinosContext.UseLan == true)
    then
        self.LanBase:parseLanKeyValue(self.TbDataMgr)
    end
end

---------------------------------------
function LanMgr:getLanValue(lan_key)
    local value = ""
    if (self.CasinosContext.UseLan == true)
    then
        local temp = self.LanBase:getValue(lan_key)
        if (temp ~= nil)
        then
            value = temp
        end
    end

    return value
end

---------------------------------------
function LanMgr:getLanPackageName()
    local value = ""
    if (self.CasinosContext.UseLan == true)
    then
        value = self.LanBase:getLanPackageName()
    end
    return value
end

---------------------------------------
function LanMgr:setLan(lan)
    if (self.CasinosContext.UseLan == false)
    then
        return
    end

    if (self.CurrentLan == lan)
    then
        return
    end

    if (self.LanBase ~= nil)
    then
        self.LanBase = nil
    end
    CS.UnityEngine.PlayerPrefs.SetString(self.LanKey, lan)
    self.CurrentLan = lan
    self:_createLanBase()
    self:parseLanKeyValue()
end

---------------------------------------
function LanMgr:parseComponent(component)
    if (self.CasinosContext.UseLan == false)
    then
        return
    end
    local children = component:GetChildren()
    local pack_name = self.LanBase:getLanPackageName()
    for i = 0, children.Length - 1 do
        local child = children[i]
        if (CS.Casinos.LuaHelper.objIsBtn(child) == true)
        then
            local name = child.name
            local l = string.find(name, self.LanTitleSign)
            if (l == nil)
            then
                self:parseComponent(child.asCom)
            else
                self:_parseGObject(name, child, pack_name)
            end
        elseif (CS.Casinos.LuaHelper.objIsComponent(child) == true)
        then
            self:parseComponent(child.asCom)
        else
            local name = child.name
            local l = string.find(name, self.LanTitleSign)
            if (l == nil)
            then
            else
                self:_parseGObject(name, child, pack_name)
            end
        end
    end
end

---------------------------------------
function LanMgr:_checkAndCreateLanBase()
    if (self.CasinosContext.UseLan == true)
    then
        if (self.CasinosContext.UseDefaultLan == false)
        then
            self.CurrentLan = self.SystemLanguage

            if (CS.UnityEngine.PlayerPrefs.HasKey(self.LanKey) == true)
            then
                self.CurrentLan = CS.UnityEngine.PlayerPrefs.GetString(self.LanKey)
            end
        else
            self.CurrentLan = self.CasinosContext.DefaultLan
        end

        self:_createLanBase()
    else
        self.LanBase = nil
    end
end

---------------------------------------
function LanMgr:_parseGObject(name, i, pack_name)
    local strs = CS.Casinos.LuaHelper.spliteStr(name, "_")
    local l = #strs
    if (l == 3)
    then
        local obj_type = strs[2]
        local obj_key = strs[3]
        local value = obj_key
        if (obj_type == "Text")
        then
            value = self.LanBase:getValue(obj_key)
            i.text = value
        elseif (obj_type == "Image")
        then
            local resource_url = CS.FairyGUI.UIPackage.GetItemURL(pack_name, value)
            if resource_url == nil then
                local p_name = pack_name
                if CS.Casinos.CasinosContext.Instance.UnityAndroid then
                    p_name = pack_name .. "Android"
                elseif CS.Casinos.CasinosContext.Instance.UnityIOS then
                    p_name = pack_name .. "IOS"
                end
                resource_url = CS.FairyGUI.UIPackage.GetItemURL(p_name, value)
            end
            --i.asLoader.url = resource_url
            if resource_url ~= nil then
                i.url = resource_url
            end
        elseif (obj_type == "Btn")
        then
            value = self.LanBase:getValue(obj_key)
            --i.asButton.title = value
            i.title = value
        end
    end
end

---------------------------------------
function LanMgr:_createLanBase()
    if (self.CurrentLan == "English")
    then
        self.LanBase = LanEn:new(nil)
    elseif (self.CurrentLan == "ChineseSimplified" or self.CurrentLan == "Chinese")
    then
        self.LanBase = LanZh:new(nil)
    else
        self.LanBase = LanEn:new(nil)
    end
end