-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemUiShopGoods = {}

---------------------------------------
function ItemUiShopGoods:new(o, com, describe, icon_url, btn_title, function_btnOnClick)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    local text_describe = com:GetChild("TextItemDescribe").asRichTextField
    text_describe.text = describe
    local loader_icon = com:GetChild("LoaderItem").asLoader
    loader_icon.url = icon_url
    local btn_buy = com:GetChild("BtnBuy").asButton
    btn_buy.onClick:Clear()
    btn_buy.onClick:Add(function_btnOnClick)
    local text_btn_title = btn_buy:GetChild("TextTitle").asTextField
    text_btn_title.text = btn_title
    return o
end