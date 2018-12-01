-- Copyright (c) Cragon. All rights reserved.
require('EventView')
require('ViewBase')
-- 层管理由原先C#版移植过来
-- 每个View的创建和销毁
-- 多语言的替换。命名规则

---------------------------------------
TableUiLayer = {
    None = 0,
    Background = 1,
    SceneActor = 51,
    DesktopChat = 151,
    PlayerOperateUi = 251,
    ShootingText = 300,
    NomalUiMain = 351,
    NomalUi = 451,
    MessgeBox = 551,
    Loading = 651,
    Waiting = 751,
    QuitGame = 851,
    GM = 951,
}

---------------------------------------
ViewMgr = {
    STANDARD_WIDTH = 1066,
    STANDARD_HEIGHT = 640,
    LayerDistance = 1,
    Instance = nil,
    Context = Context,
    EventSys = EventSys:new(nil),
    ControllerMgr = nil,
    CasinosContext = CS.Casinos.CasinosContext.Instance,
    LuaMgr = CS.Casinos.CasinosContext.Instance.LuaMgr,
    TableViewFactory = {},
    TableViewSingle = {},
    TableViewMultiple = {},
    TableMaxDepth = {},
    TableUpdateView = {},
}

---------------------------------------
function ViewMgr:OnCreate()
    --print("ViewMgr:OnCreate")
    CS.FairyGUI.GRoot.inst:SetContentScaleFactor(self.STANDARD_WIDTH, self.STANDARD_HEIGHT, CS.FairyGUI.UIContentScaler.ScreenMatchMode.MatchWidthOrHeight)
    CS.FairyGUI.UIConfig.defaultFont = "FontXi"
    if (CS.UnityEngine.PlayerPrefs.HasKey("ScreenAutoRotation")) then
        local auto_rotation = CS.UnityEngine.PlayerPrefs.GetString("ScreenAutoRotation")
        if (auto_rotation == "true") then
            CS.UnityEngine.Screen.orientation = CS.UnityEngine.ScreenOrientation.AutoRotation
            CS.UnityEngine.Screen.autorotateToLandscapeRight = true
            CS.UnityEngine.Screen.autorotateToLandscapeLeft = true
            CS.UnityEngine.Screen.autorotateToPortraitUpsideDown = false
            CS.UnityEngine.Screen.autorotateToPortrait = false
        end
    end
end

---------------------------------------
function ViewMgr:OnDestroy()
    self.CasinosContext = nil
    self.LuaMgr = nil
end

---------------------------------------
function ViewMgr:RegView(view_key, view_factory)
    if (view_factory ~= nil) then
        self.TableViewFactory[view_key] = view_factory
    end
end

---------------------------------------
function ViewMgr:CreateView(view_key)
    local view_factory = self.TableViewFactory[view_key]
    if (view_factory == nil) then
        return nil
    end
    local view = self.TableViewSingle[view_key]
    if (view_factory.IsSingle and view ~= nil) then
        return view
    end
    self:_checkDestroyUi(view_factory.UILayer)
    local go = CS.UnityEngine.GameObject()
    go.name = view_factory.ComponentName
    local layer = CS.UnityEngine.LayerMask.NameToLayer(CS.FairyGUI.StageCamera.LayerName)
    go.layer = layer
    local ui_panel = CS.Casinos.LuaHelper.addFairyGUIPanel(go)
    ui_panel.packageName = view_factory.PackageName
    ui_panel.componentName = view_factory.ComponentName
    ui_panel.fitScreen = view_factory.FitScreen
    ui_panel:ApplyModifiedProperties(true, true)
    view = view_factory:CreateView()
    view.ViewMgr = self.Instance
    view.GoUi = go
    view.ComUi = ui_panel.ui
    view.Panel = ui_panel
    view.UILayer = view_factory.UILayer
    view.ViewKey = view_key
    view:OnCreate()
    if (view_factory.IsSingle) then
        self.TableViewSingle[view_key] = view
    else
        local table_multiple = self.TableViewMultiple[view_key]
        if (table_multiple == nil) then
            table_multiple = {}
            self.TableViewMultiple[view_key] = table_multiple
        end
        table_multiple[view] = view
    end
    local depth_layer = self.TableMaxDepth[view_factory.UILayer]
    if (depth_layer == nil) then
        depth_layer = TableUiLayer[view_factory.UILayer]
        view.InitDepth = depth_layer
    else
        view.InitDepth = depth_layer
        depth_layer = depth_layer + self.LayerDistance
    end
    ui_panel:SetSortingOrder(depth_layer, true)
    self.TableMaxDepth[view_factory.UILayer] = depth_layer

    self.LanMgr:parseComponent(ui_panel.ui)-- 多语言自动替换

    if (view_factory.UILayer == "MessgeBox" or view_factory.UILayer == "NomalUiMain" or view_factory.UILayer == "NomalUi" or view_factory.UILayer == "QuitGame") then
        self.CasinosContext:Play("CreateDialog", CS.Casinos._eSoundLayer.LayerReplace)
    end
    return view
end

---------------------------------------
function ViewMgr:DestroyView(view)
    if (view ~= nil) then
        local view_key = view.ViewKey
        local view_ex = self.TableViewSingle[view_key]
        if (view_ex ~= nil) then
            self.TableViewSingle[view_key] = nil
            view:OnDestroy()
        else
            local table_multiple = self.TableViewMultiple[view_key]
            if (table_multiple ~= nil and #table_multiple > 0) then
                LuaHelper:TableRemoveV(table_multiple, view)
                view:OnDestroy()
            end
        end
        self.TableMaxDepth[view.UILayer] = view.InitDepth
        CS.UnityEngine.GameObject.Destroy(view.GoUi)
        if (view.UILayer == "MessgeBox" or view.UILayer == "NomalUiMain" or view.UILayer == "NomalUi" or view.UILayer == "QuitGame") then
            self.CasinosContext:Play("DestroyDialog", CS.Casinos._eSoundLayer.LayerReplace)
        end
        view = nil
    end
end

---------------------------------------
function ViewMgr:DestroyAllView()
    local table_need_destroyui = {}
    for k, v in pairs(self.TableViewSingle) do
        table_need_destroyui[k] = v
    end
    for k, v in pairs(self.TableViewMultiple) do
        for k1, v1 in pairs(v) do
            table_need_destroyui[k1] = v1
        end
    end
    for k, v in pairs(table_need_destroyui) do
        self.TableMaxDepth[v.UILayer] = v.InitDepth
        v:OnDestroy()
        self.LuaMgr:DestroyGameObject(v.GoUi)
        v = nil
    end
    self.TableViewSingle = {}
    self.TableViewMultiple = {}
end

---------------------------------------
function ViewMgr:GetView(view_key)
    local view = self.TableViewSingle[view_key]
    return view
end

---------------------------------------
function ViewMgr:BindEvListener(ev_name, ev_listener)
    if (self.EventSys ~= nil) then
        self.EventSys:BindEvListener(ev_name, ev_listener)
    end
end

---------------------------------------
function ViewMgr:UnbindEvListener(ev_listener)
    if (self.EventSys ~= nil) then
        self.EventSys:UnbindEvListener(ev_listener)
    end
end

---------------------------------------
function ViewMgr:GetEv(ev_name)
    local ev = nil
    if (self.EventSys ~= nil) then
        ev = self.EventSys:GetEv(ev_name)
    end
    return ev
end

---------------------------------------
function ViewMgr:SendEv(ev)
    if (self.EventSys ~= nil) then
        self.EventSys:SendEv(ev)
    end
end

---------------------------------------
function ViewMgr:GetUiPackagePath(package_name)
    local s = CS.Casinos.CasinosContext.Instance.PathMgr.DirAbUi .. string.lower(package_name) .. ".ab"
    return s
end

---------------------------------------
function ViewMgr:PackData(data)
    local p_datas = self.RPC:PackData(data)
    return p_datas
end

---------------------------------------
function ViewMgr:UnpackData(data)
    local p_datas = self.RPC:UnpackData(data)
    return p_datas
end

---------------------------------------
function ViewMgr:_checkDestroyUi(t_layer)
    if (t_layer == "None" or t_layer == "MessgeBox" or t_layer == "SceneActor" or t_layer == "PlayerOperateUi" or t_layer == "ShootingText") then
        return
    end
    local layer_v = TableUiLayer[t_layer]
    local map_need_destroyui = {}
    for i, v in pairs(self.TableViewSingle) do
        local v_layer_v = TableUiLayer[v.UILayer]
        if ((t_layer == "MessgeBox" and t_layer == v.UILayer) or (t_layer == "NomalUi" and t_layer == v.UILayer)
                or (layer_v > v_layer_v)) then
        else
            map_need_destroyui[i] = v
        end
    end
    for i, v in pairs(map_need_destroyui) do
        local view = self.TableViewSingle[i]
        if (view ~= nil) then
            self.TableViewSingle[i] = nil
            local layer = v.UILayer
            self.TableMaxDepth[layer] = v.InitDepth
            v:OnDestroy()
            --v.ComUi:Dispose()
            self.LuaMgr:DestroyGameObject(v.GoUi)
        end
    end
    map_need_destroyui = {}
    local map_need_destroyuis = {}
    for i, v in pairs(self.TableViewMultiple) do
        local can_destroy = false
        for i_i, v_v in pairs(v) do
            local v_v_layer_v = TableUiLayer[v_v.UILayer]
            if (t_layer == v_v.UILayer or (layer_v > v_v_layer_v)) then
            else
                can_destroy = true
            end
        end

        if (can_destroy) then
            map_need_destroyuis[i] = v
        end
    end
    for i, v in pairs(map_need_destroyuis) do
        for i_i, v_v in pairs(v) do
            local views = self.TableViewMultiple[i]
            if (views ~= nil) then
                self.TableViewMultiple[i] = nil
                local layer = v_v.UILayer
                self.TableMaxDepth[layer] = v_v.InitDepth
                v_v:OnDestroy()
                --v_v.ComUi:Dispose()
                self.LuaMgr:DestroyGameObject(v.GoUi)
            end
        end
    end
    map_need_destroyuis = {}
end