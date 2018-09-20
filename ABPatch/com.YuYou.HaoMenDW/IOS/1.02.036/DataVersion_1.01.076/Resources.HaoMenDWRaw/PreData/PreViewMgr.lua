PreTableUiLayer = {
	None=0,-- 手动管理
	Background = 1,-- 背景层，最下面的层         
    SceneActor = 51,
    ShootingText = 151,
    PlayerOperateUi = 251,-- 操作界面
    NomalUiMain = 351,-- 常规大界面        
    NomalUi = 451,-- 常规界面      
    MessgeBox = 551,-- MessageBox界面层 之间不互斥
    Loading = 651,-- 加载界面层
    Waiting = 751,-- 等待界面层       
    QuitGame = 851,
    GM = 951,
}

PreViewMgr = { 
	STANDARD_WIDTH = 960,
    STANDARD_HEIGHT = 640,
    LayerDistance = 1,
	Instance = nil,	
	EventSys = nil,
	TableViewFactory = {},
	TableViewSingle = {},
	TableViewMultiple = {},
	TableMaxDepth = {},
	TableUpdateView = {},
}

function PreViewMgr:new(o)
	 o = o or {}  
    setmetatable(o,self)  
    self.__index = self  
	if(self.Instance==nil)
	then
		self.Instance = o
	end
    return self.Instance
end

function PreViewMgr:onCreate()
	print("PreViewMgr:onCreate")
	CS.FairyGUI.GRoot.inst:SetContentScaleFactor(PreViewMgr.STANDARD_WIDTH, PreViewMgr.STANDARD_HEIGHT,
                CS.FairyGUI.UIContentScaler.ScreenMatchMode.MatchWidthOrHeight)
    CS.FairyGUI.UIConfig.defaultFont = "Microsoft YaHei"	
	-- PreViewMgr.EventSys = EventSys:new(nil)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("PreViewBase")
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("PreViewFactory")
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("PreViewLoading")
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("PreViewMsgBox")
	local view_loading_fac = PreViewLoadingFactory:new(nil,"PreLoading","PreLoading","Loading",true,CS.FairyGUI.FitScreen.FitSize)
	PreViewMgr.regView("PreLoading",view_loading_fac)
	local view_msgbox_fac = PreViewMsgBoxFactory:new(nil,"PreMsgBox","PreMsgBox","Waiting",true,CS.FairyGUI.FitScreen.FitSize)
	PreViewMgr.regView("PreMsgBox",view_msgbox_fac)	
end

function PreViewMgr:onDestroy()
	
end

function PreViewMgr:onUpdate(tm)	
	for k,v in pairs(PreViewMgr.TableViewSingle) do
		if(v~=nil)
		then
			v:onUpdate(tm)
		end
	end
end

function PreViewMgr.regView(view_key,view_factory)
	if(view_factory~=nil)
	then
		PreViewMgr.TableViewFactory[view_key]=view_factory
	end	
end

function PreViewMgr.createView(view_key)	
	local view_factory = PreViewMgr.TableViewFactory[view_key]
	if(view_factory==nil)
	then		
		return nil
	end

	local view = PreViewMgr.TableViewSingle[view_key]
	if(view_factory.IsSingle and view~=nil)
	then
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
    view.PreViewMgr = PreViewMgr
    view.GoUi = go
    view.ComUi = ui_panel.ui	
    view.Panel = ui_panel
    view.UILayer = view_factory.UILayer
	view.ViewKey = view_key
    view:onCreate()

	if(view_factory.IsSingle)
	then
		PreViewMgr.TableViewSingle[view_key] = view
	else	
		local table_multiple = PreViewMgr.TableViewMultiple[view_key]
		if(table_multiple==nil)
		then
			table_multiple = {}
			PreViewMgr.TableViewMultiple[view_key] =table_multiple
		end
		table_multiple[view] = view
	end

	local depth_layer = PreViewMgr.TableMaxDepth[view_factory.UILayer]
	if(depth_layer == nil)
	then
		depth_layer = PreTableUiLayer[view_factory.UILayer]
        view.InitDepth = depth_layer
	else				
		view.InitDepth = depth_layer        
		depth_layer = depth_layer + PreViewMgr.LayerDistance		
	end

    ui_panel:SetSortingOrder(depth_layer, true)
    PreViewMgr.TableMaxDepth[view_factory.UILayer] = depth_layer

	return view
end

function PreViewMgr.destroyView(view)
	if(view~=nil)
	then
		local view_key = view.ViewKey
		local view_ex = PreViewMgr.TableViewSingle[view_key]
		if(view_ex~=nil)
		then			
			view:onDestroy()
			PreViewMgr.TableViewSingle[view_key] = nil
		else			
			local table_multiple = PreViewMgr.TableViewMultiple[view_key]
            if (table_multiple ~= null)
            then				
                view:onDestroy()
                table_multiple[view]=nil				
            end
		end
		PreViewMgr.TableMaxDepth[view.UILayer] = view.InitDepth;
		CS.UnityEngine.GameObject.Destroy(view.GoUi)		
	end
end

function PreViewMgr.getView(view_key)
	local view = PreViewMgr.TableViewSingle[view_key]
	return view
end

function PreViewMgr.destroyAllView()
	 for k,v in pairs(PreViewMgr.TableViewSingle) do          
		PreViewMgr.TableMaxDepth[v.UILayer] = v.InitDepth
		v:onDestroy()
		CS.UnityEngine.GameObject.Destroy(v.GoUi)
	 end  

	 for k,v in pairs(PreViewMgr.TableViewMultiple) do  
		for k1,v1 in pairs(v) do			
			PreViewMgr.TableMaxDepth[v.UILayer] = v.InitDepth
			v:onDestroy()
			CS.UnityEngine.GameObject.Destroy(v.GoUi)
		end        
	 end       
end

function PreViewMgr:bindEvListener(ev_name,ev_listener)
	if(PreViewMgr.EventSys ~= nil)
	then
		PreViewMgr.EventSys:bindEvListener(ev_name,ev_listener)
	end
end

function PreViewMgr:unbindEvListener(ev_listener)
	if(PreViewMgr.EventSys ~= nil)
	then
		PreViewMgr.EventSys:unbindEvListener(ev_listener)
	end
end

function PreViewMgr:getEv(ev_name)
	local ev = nil
	if(PreViewMgr.EventSys ~= nil)
	then
		ev = PreViewMgr.EventSys:getEv(ev_name)
	end
	return ev
end

function PreViewMgr:sendEv(ev)
	if(PreViewMgr.EventSys ~= nil)
	then
		PreViewMgr.EventSys:sendEv(ev)
	end
end

function PreViewMgr:GetTableCount(t)
	local count = 0
    for k, v in pairs(t) do
        count = count + 1
    end

    return count
end

function PreViewMgr:GetAndRemoveTableFirstEle(t)
	local key = nil	
	local value = nil
	
	for k, v in pairs(t) do		
        key = k
		value = v
		break
    end	
	
	t[key] = nil	
	
	return key,value
end