-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
MailType = {
    Normal = 0,
    Event = 1,
}

MailSenderType = {
    System = 0, -- 系统
    Player = 1, -- 玩家
}

MailState = {
    New = 0, -- 新邮件
    Read = 1, -- 已读
    ReadAndRecvAttachments = 2, -- 已读且接受所有附件
    Delete = 3, -- 已删除
}

MailOperateType = {
    None = 0, -- 无操作
    Read = 1, -- 标记为已读
    RecvAttachment = 2, -- 领取附件
    Delete = 3, -- 删除邮件（包括领取附件）
}

---------------------------------------
MailAttachment = {}

function MailAttachment:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ListItem = nil
    o.Gold = 0
    o.Diamond = 0
    return o
end

function MailAttachment:setData(data)
    self.ListItem = data[1]
    self.Gold = data[2]
    self.Diamond = data[3]
end

function MailAttachment:getData4Pack()
    local temp = {}
    table.insert(temp, self.ListItem)
    table.insert(temp, self.Gold)
    table.insert(temp, self.Diamond)
    return temp
end

---------------------------------------
MailOperateRequestResult = {}

function MailOperateRequestResult:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Result = 0
    o.MailGuid = nil
    o.OperateType = 0
    o.Attachment = nil
    return o
end

function MailOperateRequestResult:setData(data)
    self.Result = data[1]
    self.MailGuid = data[2]
    self.OperateType = data[3]
    local a = data[4]
    if a ~= nil then
        local a_t = MailAttachment:new(nil)
        a_t:setData(a)
        self.Attachment = a_t
    end
end

function MailOperateRequestResult:getData4Pack()
    local temp = {}
    table.insert(temp, self.Result)
    table.insert(temp, self.MailGuid)
    table.insert(temp, self.OperateType)
    if self.Attachment ~= nil then
        table.insert(temp, self.Attachment:getData4Pack())
    end
    return temp
end

---------------------------------------
MailClient = {}

function MailClient:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.MailGuid = nil
    o.MailState = 0
    o.MailType = 0
    o.SenderType = 0
    o.SenderGuid = nil
    o.SenderNickname = nil
    o.SenderIcon = nil
    o.RecverGuid = nil
    o.Title = nil
    o.Content = nil
    o.CreateTime = nil
    o.Attachment = nil
    return o
end

function MailClient:setData(data)
    self.MailGuid = data[1]
    self.MailState = data[2]
    self.MailType = data[3]
    self.SenderType = data[4]
    self.SenderGuid = data[5]
    self.SenderNickname = data[6]
    self.SenderIcon = data[7]
    self.RecverGuid = data[8]
    self.Title = data[9]
    self.Content = data[10]
    self.CreateTime = data[11]
    local a = data[12]
    if a ~= nil then
        local a_t = MailAttachment:new(nil)
        a_t:setData(a)
        self.Attachment = a_t
    end
end

function MailClient:getData4Pack()
    local temp = {}
    table.insert(temp, self.MailGuid)
    table.insert(temp, self.MailState)
    table.insert(temp, self.MailType)
    table.insert(temp, self.SenderType)
    table.insert(temp, self.SenderGuid)
    table.insert(temp, self.SenderNickname)
    table.insert(temp, self.SenderIcon)
    table.insert(temp, self.RecverGuid)
    table.insert(temp, self.Title)
    table.insert(temp, self.Content)
    table.insert(temp, self.CreateTime)
    if self.Attachment ~= nil then
        table.insert(temp, self.Attachment:getData4Pack())
    end
    return temp
end