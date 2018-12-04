-- Copyright(c) Cragon. All rights reserved.
-- 系统公告对话框

---------------------------------------
ViewNotice = class(ViewBase)

---------------------------------------
function ViewNotice:ctor()
    self.PackName = "Notice"
    self.SendNoticeTbKey = "IMMarqueeCostItemId"
    self.HornTbId = 14000
end

---------------------------------------
function ViewNotice:OnCreate()
    self.ViewMgr:BindEvListener("EvEntityReceiceMarquee", self)
    self.ControllerMarquee = self.ControllerMgr:GetController("Marquee")
    self.ControllerBag = self.ControllerMgr:GetController("Bag")
    self.ViewPool = self.ViewMgr:GetView("Pool")
    self.GCoShade = self.ComUi:GetChild("CoShade").asCom
    self.GCoShade.onClick:Add(
            function()
                self:onClickReturn()
            end
    )
    self.SendNoticeTbId = TbDataHelper:GetCommonValue("IMMarqueeCostItemId")
    local co_return = self.ComUi:GetChild("CoReturn")
    if (co_return ~= nil) then
        self.GCoReturn = co_return.asCom
        self.GCoReturn.onClick:Add(
                function()
                    self.onClickReturn()
                end
        )
    end
    local co_input = self.ComUi:GetChild("CoInput").asCom
    self.GTextInput = co_input:GetChild("InputText").asTextInput
    self.GTextInput.promptText = self.ViewMgr.LanMgr:getLanValue("EnterContentTips")
    self.GTextInput.maxLength = 64
    self.GBtnSend = self.ComUi:GetChild("BtnSend").asButton
    self.GBtnSend.onClick:Add(
            function()
                self:onClickSend()
            end
    )
    self.ControllerSend = self.ComUi:GetController("ControllerSend")
    self.GTextItemCount = self.ComUi:GetChild("ItemCount").asTextField
    if (self.SendNoticeTbId ~= nil and string.len(self.SendNoticeTbId) > 0) then
        local sendnotice_itemtbid = tonumber(self.SendNoticeTbId)
        if (sendnotice_itemtbid ~= 0) then
            self.GTextItemCount.text = "1"
        end
    end
    self.TransitiOnCreate = self.ComUi:GetTransition("TransitiOnCreate")
    if (self.TransitiOnCreate ~= nil) then
        self.TransitiOnCreate:Play()
    else
        local pos = self.ComUi.position
        pos.x = -self.ComUi.width
        self.ComUi.position = pos
        self.ComUi:TweenMoveX(0, 0.5)
    end
    self.GListNotice = self.ComUi:GetChild("ListNotice").asList
    self.GListNotice:SetVirtual()
    self.GListNotice.itemRenderer = function(a, b)
        self:RenderListItem(a, b)
    end
    self.GListNotice.numItems = #self.ControllerMarquee.ListIMMarquee
end

---------------------------------------
function ViewNotice:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewNotice:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvEntityReceiceMarquee") then
            self.GListNotice.numItems = #self.ControllerMarquee.ListIMMarquee
            self.GListNotice.scrollPane:ScrollTop()
        end
    end
end

---------------------------------------
function ViewNotice:RenderListItem(index, obj)
    local com = CS.Casinos.LuaHelper.GObjectCastToGCom(obj)
    local item = self.ViewPool:getItemChatEx(com)
    if (#self.ControllerMarquee.ListIMMarquee > 0) then
        local list_index = #self.ControllerMarquee.ListIMMarquee - 1 - index
        if (list_index < 0) then
            list_index = 0
        end
        local im_marquee = self.ControllerMarquee.ListIMMarquee[list_index + 1]
        local color_title = CS.UnityEngine.Color.green
        local color_content = CS.UnityEngine.Color.yellow
        if (im_marquee.SenderType == IMMarqueeSenderType.System) then
            color_title = CS.UnityEngine.Color.red
        end
        item:setChat(im_marquee.NickName, im_marquee.Msg, im_marquee.VIPLevel, CS.System.DateTime.MinValue,
                nil, nil, true, self.GListNotice.width - 50, CS.Casinos._eChatItemType.NormalChat, nil, true, true, false, true)
    end
end

---------------------------------------
function ViewNotice:onClickReturn()
    self.ViewMgr:DestroyView(self)
    --[[if (self.TransitiOnCreate ~= nil) then
        self.ComUi:TweenMoveX(-self.ComUi.width, 0.5):OnStart(
            function()
                self.GCoShade.visible = false
            end
        ):OnComplete(
            function()
                self.ViewMgr:DestroyView(self)
            end
        )
    else
        self.ViewMgr:DestroyView(self)
    end]]
end

---------------------------------------
function ViewNotice:onClickSend()
    if (self.SendNoticeTbId ~= nil and string.len(self.SendNoticeTbId) > 0) then
        local changename_itemtbid = tonumber(self.SendNoticeTbId)
        if (changename_itemtbid ~= 0) then
            local horn_count = self.ControllerBag:getAlreadyHaveItemCount(changename_itemtbid)
            if (horn_count <= 0) then
                local tips = self.ViewMgr.LanMgr:getLanValue("NoHornPleaseBuy")
                ViewHelper:UiShowMsgBox(tips, function()
                    local viewShop = self.ViewMgr:CreateView("Shop")
                    viewShop:showItem()
                end
                )
                return
            end
        end
    end
    if (self.GTextInput.text == nil or string.len(self.GTextInput.text) <= 0) then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("SendNoEmpty"))
        return
    end
    local ev = self.ViewMgr.GetEv("EvRequestSendMarquee")
    if (ev == nil) then
        ev = EvRequestSendMarquee:new(nil)
    end
    ev.msg = self.GTextInput.text
    self:SendEv(ev)
    self.GTextInput.text = ""
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewNoticeFactory = class(ViewFactory)

---------------------------------------
function ViewNoticeFactory:CreateView()
    local view = ViewNotice:new()
    return view
end
