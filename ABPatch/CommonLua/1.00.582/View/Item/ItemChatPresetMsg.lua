ItemChatPresetMsg = {}

function ItemChatPresetMsg:new(o,g_co,msg,view_mgr)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.GTextContent = g_co:GetChild("Msg").asTextField
    o.GTextContent.text = msg
    g_co.onClick:Add(
		function()
			local i_uichat = view_mgr:getView("Chat")
			i_uichat:setChatMsg(o.GTextContent.text)
		end
	)
	return o
end