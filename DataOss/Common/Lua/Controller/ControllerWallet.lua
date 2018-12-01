-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerWallet = class(ControllerBase)

---------------------------------------
function ControllerWallet:ctor(controller_data, controller_name)
end

---------------------------------------
function ControllerWallet:OnCreate()
    self.RPC = self.ControllerMgr.RPC
    self.MC = CommonMethodType
end

---------------------------------------
function ControllerWallet:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerWallet:OnHandleEv(ev)
end

---------------------------------------
ControllerWalletFactory = class(ControllerFactory)

function ControllerWalletFactory:GetName()
    return 'Wallet'
end

function ControllerWalletFactory:CreateController(controller_data)
    local ctrl_name = self:GetName()
    local ctrl = ControllerWallet:new(controller_data, ctrl_name)
    return ctrl
end