-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewMTTProcess = class(ViewBase)

---------------------------------------
function ViewMTTProcess:ctor()
end

---------------------------------------
function ViewMTTProcess:OnCreate()
    self.ControllerProcess = self.ComUi:GetController("ControllerProcess")
end

---------------------------------------
function ViewMTTProcess:OnDestroy()
end

---------------------------------------
function ViewMTTProcess:OnHandleEv(ev)
end

---------------------------------------
function ViewMTTProcess:setProcess(process_type)
    self.ControllerProcess.selectedIndex = process_type
    local com_process = self.ComUi:GetChild("ComProcess" .. process_type).asCom
    if process_type == 3 or process_type == 4 then
        local c_t = com_process:GetChild("ComTransition")
        local c_t_t = c_t:GetTransition("t0")
        c_t_t:Play()
    end
    local transition = com_process:GetTransition("t0")
    transition:Play(
            function()
                self:_onClickClose()
            end)
end

---------------------------------------
function ViewMTTProcess:_onClickClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewMTTProcessFactory = class(ViewFactory)

---------------------------------------
function ViewMTTProcessFactory:CreateView()
    local view = ViewMTTProcess:new()
    return view
end