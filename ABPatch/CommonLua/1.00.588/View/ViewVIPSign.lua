ViewVIPSign = {}

function ViewVIPSign:new(o,co_vip)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.VIPPackName = "Common"
	o.GCoVIP = co_vip
    o.GLoaderVIPNum1 = o.GCoVIP:GetChild("VIPLevel1").asLoader
    o.GLoaderVIPNum2 = o.GCoVIP:GetChild("VIPLevel2").asLoader
    o.GLoaderVIPNum3 = o.GCoVIP:GetChild("VIPLevel3").asLoader

    return o
end

function ViewVIPSign:setVIPLevel(vip_level,show_vip)
    if self.GCoVIP == nil then
        return
    end
	if(show_vip == nil)
	then
		show_vip = false
	end
	self.GCoVIP.visible = show_vip
    if (vip_level > 0)
	then
		self.GCoVIP.visible = true
        if (vip_level < 10)
		then
			self.GLoaderVIPNum1.visible = false
            self.GLoaderVIPNum2.visible = false
            self.GLoaderVIPNum3.visible = true
            self.GLoaderVIPNum3.icon = ViewHelper:FormatePackageImagePath(self.VIPPackName, "VIPNum" .. vip_level)
		else
			self.GLoaderVIPNum1.visible = true
            self.GLoaderVIPNum2.visible = true
            self.GLoaderVIPNum3.visible = false
            local vipnum1 = math.floor(vip_level / 10)
            self.GLoaderVIPNum1.icon = ViewHelper:FormatePackageImagePath(self.VIPPackName, "VIPNum" .. vipnum1)
            local vipnum2 = vip_level % 10
            self.GLoaderVIPNum2.icon = ViewHelper:FormatePackageImagePath(self.VIPPackName, "VIPNum" .. vipnum2)
		end
	elseif(vip_level == 0)
	then
		self.GLoaderVIPNum1.visible = false
        self.GLoaderVIPNum2.visible = false
        self.GLoaderVIPNum3.visible = true
        self.GLoaderVIPNum3.icon = ViewHelper:FormatePackageImagePath(self.VIPPackName, "VIPNum" .. vip_level)
	end
end


