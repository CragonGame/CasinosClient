-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
PreTableUiLayer = {
    None = 0,
    Background = 1,
    SceneActor = 51,
    ShootingText = 151,
    PlayerOperateUi = 251,
    NomalUiMain = 351,
    NomalUi = 451,
    MessgeBox = 551,
    Loading = 651,
    Waiting = 751,
    QuitGame = 851,
    GM = 951,
}

---------------------------------------
PreViewMgr = {
    STANDARD_WIDTH = 1066,
    STANDARD_HEIGHT = 640,
    LayerDistance = 1,
    Instance = nil,
    EventSys = nil,
    TableViewFactory = {},
    TableViewSingle = {},
    TableViewMultiple = {},
    TableMaxDepth = {},
    CasinosContext = CS.Casinos.CasinosContext.Instance,
    CasinosLua = CS.Casinos.CasinosContext.Instance.CasinosLua
}

---------------------------------------
function PreViewMgr:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.TableViewFactory = {}
    if (self.Instance == nil) then
        self.Instance = o
    end
    return self.Instance
end

---------------------------------------
function PreViewMgr:Init()
    CS.FairyGUI.GRoot.inst:SetContentScaleFactor(PreViewMgr.STANDARD_WIDTH, PreViewMgr.STANDARD_HEIGHT, CS.FairyGUI.UIContentScaler.ScreenMatchMode.MatchWidthOrHeight)
    CS.FairyGUI.UIConfig.defaultFont = "FontXi"

    local view_loading_fac = PreViewLoadingFactory:new(nil, "PreLoading", "PreLoading", "Loading", true, CS.FairyGUI.FitScreen.FitSize)
    self:regView("PreLoading", view_loading_fac)
    local view_msgbox_fac = PreViewMsgBoxFactory:new(nil, "PreMsgBox", "PreMsgBox", "Waiting", true, CS.FairyGUI.FitScreen.FitSize)
    self:regView("PreMsgBox", view_msgbox_fac)
end

---------------------------------------
function PreViewMgr:Release()
    self:destroyAllView()
    self.CasinosContext = nil
    self.CasinosLua = nil
end

---------------------------------------
function PreViewMgr:regView(view_key, view_factory)
    if (view_factory ~= nil) then
        self.TableViewFactory[view_key] = view_factory
    end
end

---------------------------------------
function PreViewMgr:createView(view_key)
    local view_factory = self.TableViewFactory[view_key]
    if (view_factory == nil) then
        return nil
    end

    local view = self.TableViewSingle[view_key]
    if (view_factory.IsSingle and view ~= nil) then
        return view
    end

    local go = CS.UnityEngine.GameObject()
    go.name = view_factory.ComponentName
    local layer = CS.UnityEngine.LayerMask.NameToLayer(CS.FairyGUI.StageCamera.LayerName)
    go.layer = layer
    local ui_panel = CS.Casinos.LuaHelper.addFairyGUIPanel(go)
    ui_panel.packageName = view_factory.PackageName
    ui_panel.componentName = view_factory.ComponentName
    ui_panel.fitScreen = view_factory.FitScreen
    ui_panel:ApplyModifiedProperties(false, true)
    view = view_factory:createView()
    view.PreViewMgr = self
    view.GoUi = go
    view.ComUi = ui_panel.ui
    view.Panel = ui_panel
    view.UILayer = view_factory.UILayer
    view.ViewKey = view_key
    view:onCreate()

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
        depth_layer = PreTableUiLayer[view_factory.UILayer]
        view.InitDepth = depth_layer
    else
        view.InitDepth = depth_layer
        depth_layer = depth_layer + self.LayerDistance
    end

    ui_panel:SetSortingOrder(depth_layer, true)
    self.TableMaxDepth[view_factory.UILayer] = depth_layer

    return view
end

---------------------------------------
function PreViewMgr:destroyView(view)
    if (view ~= nil) then
        local view_key = view.ViewKey
        local view_ex = self.TableViewSingle[view_key]
        if (view_ex ~= nil) then
            view:onDestroy()
            self.TableViewSingle[view_key] = nil
        else
            local table_multiple = self.TableViewMultiple[view_key]
            if (table_multiple ~= null) then
                view:onDestroy()
                table_multiple[view] = nil
            end
        end
        self.TableMaxDepth[view.UILayer] = view.InitDepth
        self.CasinosLua:DestroyGameObject(view.GoUi)
        view = nil
    end
end

---------------------------------------
function PreViewMgr:getView(view_key)
    local view = self.TableViewSingle[view_key]
    return view
end

---------------------------------------
function PreViewMgr:destroyAllView()
    for k, v in pairs(self.TableViewSingle) do
        self.TableMaxDepth[v.UILayer] = v.InitDepth
        v:onDestroy()
        self.CasinosLua:DestroyGameObject(v.GoUi)
        --CS.UnityEngine.GameObject.Destroy(v.GoUi)
    end

    for k, v in pairs(PreViewMgr.TableViewMultiple) do
        for k1, v1 in pairs(v) do
            self.TableMaxDepth[v.UILayer] = v.InitDepth
            v:onDestroy()
            self.CasinosLua:DestroyGameObject(v.GoUi)
        end
    end
end

---------------------------------------
function PreViewMgr:bindEvListener(ev_name, ev_listener)
    if (self.EventSys ~= nil) then
        self.EventSys:bindEvListener(ev_name, ev_listener)
    end
end

---------------------------------------
function PreViewMgr:unbindEvListener(ev_listener)
    if (self.EventSys ~= nil) then
        self.EventSys:unbindEvListener(ev_listener)
    end
end

---------------------------------------
function PreViewMgr:getEv(ev_name)
    local ev = nil
    if (self.EventSys ~= nil) then
        ev = self.EventSys:getEv(ev_name)
    end
    return ev
end

---------------------------------------
function PreViewMgr:sendEv(ev)
    if (self.EventSys ~= nil) then
        self.EventSys:sendEv(ev)
    end
end

---------------------------------------
function PreViewMgr:GetTableCount(t)
    local count = 0
    for k, v in pairs(t) do
        count = count + 1
    end
    return count
end

---------------------------------------
function PreViewMgr:GetAndRemoveTableFirstEle(t)
    local key = nil
    local value = nil
    for k, v in pairs(t) do
        key = k
        value = v
        break
    end
    t[key] = nil
    return key, value
end