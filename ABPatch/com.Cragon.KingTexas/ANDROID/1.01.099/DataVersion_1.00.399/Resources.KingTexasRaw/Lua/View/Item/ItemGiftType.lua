ItemGiftType = {}

function ItemGiftType:new(o,GiftType,KingGiftShop)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	o.mComSelf = GiftType
    o.mComSelf.onClick:Add(
		function()
			o:onClick()
		end
	)
    o.mKingGiftShop = KingGiftShop
    o.mGLoaderGiftType = o.mComSelf:GetChild("LoaderGiftIcon").asLoader       
    o.mGTextName = o.mComSelf:GetChild("TextName").asTextField
    o.mControlSelect = o.mComSelf:GetController("ControlSelect")
    o.mControllerBg = o.mComSelf:GetController("ConttollerBg")
	return o
end

function ItemGiftType:setGiftType(gift_typeid,from_etguid,to_etguid)
	self.mTbGiftTypeId = gift_typeid
    local tb_data = self:getTbDataItemType()
    if (tb_data.ParentTbId == 100)
	then
		self.mControllerBg.selectedIndex = 0
	elseif(tb_data.ParentTbId == 200)
	then
		self.mControllerBg.selectedIndex = 1
	end

    self.mGLoaderGiftType.icon = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath
        ("Resources.KingTexas/Item/" .. string.lower(tb_data.TypeIcon) .. ".ab")
    self.mGTextName.text = tb_data.TypeName
end

function ItemGiftType:getTbDataItemType()
	return CS.Casinos.CasinosContext.Instance.TbDataMgrLua:GetData("ItemType",self.mTbGiftTypeId)
end

function ItemGiftType:onClick()
	self.mKingGiftShop:closeCurrentGiftType()
    self.mControlSelect.selectedIndex = 1
    self.mKingGiftShop:setCurrentGiftType(self)
end
