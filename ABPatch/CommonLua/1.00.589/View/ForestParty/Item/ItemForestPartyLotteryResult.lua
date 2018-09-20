ItemForestPartyLotteryResult = {}

function ItemForestPartyLotteryResult:new(o,com,lamp)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.ComLotteryResult = com
    self.LoaderAnimal = self.ComLotteryResult:GetChild("LoaderAnimal").asLoader
    self.GTextMultiple = self.ComLotteryResult:GetChild("TextMultiple").asTextField
    self:setInfo(lamp)
	return o
end

function ItemForestPartyLotteryResult:setInfo(lamp)
	local animal_type = lamp.AnimalType
    local color_type = lamp.BlockColor
	local hang = CS.Casinos.BlockColor.__CastFrom('color_type')
    local lie = CS.Casinos.AnimalType.__CastFrom('animal_type')
    local bet_index = hang * 4 + lie
    local bet_multiple = ControllerForestParty.MapBetMultiple[bet_index]
    self.LoaderAnimal.url = CS.FairyGUI.UIPackage.GetItemURL("ForestPartyResult", animal_type.ToString())
    self.GTextMultiple.text = tostring(bet_multiple)
end
