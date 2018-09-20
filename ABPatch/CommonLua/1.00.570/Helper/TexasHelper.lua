TexasHelper = {}

function TexasHelper:getTbDataDesktopInfoSuitable(data_mgr,chip_acc)
	local map_tbdatadesktopinfo = data_mgr:GetMapData("DesktopInfoTexas")
    local tbdata_desktopinfo = nil
	for key,value in pairs(map_tbdatadesktopinfo) do
		if (chip_acc >= value.BetMax)
		then
			tbdata_desktopinfo = value
            break
		end
	end

    if (tbdata_desktopinfo == nil)
	then
		if (chip_acc <= map_tbdatadesktopinfo[1].BetMin)
		then
			tbdata_desktopinfo = map_tbdatadesktopinfo[1]
		elseif (chip_acc >= map_tbdatadesktopinfo[#map_tbdatadesktopinfo].BetMax)
		then
			tbdata_desktopinfo = map_tbdatadesktopinfo[#map_tbdatadesktopinfo]
		end
	end

    return tbdata_desktopinfo
end

function TexasHelper:getTbDataMustBetDesktopInfoSuitable(data_mgr,chip_acc)
	local map_tbdatadesktopinfo = data_mgr:GetMapData("MustBetDesktopInfoTexas");

    local tbdata_desktopinfo = nil
    if (chip_acc >= map_tbdatadesktopinfo[#map_tbdatadesktopinfo].CanPlayMinChips)
	then
		tbdata_desktopinfo = map_tbdatadesktopinfo[#map_tbdatadesktopinfo]
        return tbdata_desktopinfo
	end

	for i = 1,#map_tbdatadesktopinfo do
		local tb_i = map_tbdatadesktopinfo[i]
        if (chip_acc >= tb_i.CanPlayMinChips and chip_acc < map_tbdatadesktopinfo[i + 1].CanPlayMinChips)
		then
			tbdata_desktopinfo = tb_i
            break
		end
	end

    if (tbdata_desktopinfo == nil)
	then
		if (chip_acc < map_tbdatadesktopinfo[1].CanPlayMinChips)
		then
			tbdata_desktopinfo = map_tbdatadesktopinfo[1]
		end
	end

    return tbdata_desktopinfo
end

function TexasHelper:enoughChip4DesktopBetMin(data_mgr,chip,tbdata_desktopinfo_id)
	local chip_enough = false
	local tbdata_desktopinfo = data_mgr:GetData("DesktopInfoTexas",tbdata_desktopinfo_id)
	if (tbdata_desktopinfo ~= nil)
	then
		chip_enough = chip >= tbdata_desktopinfo.BetMin
	end

    return chip_enough
end

function TexasHelper:calcAutoPushStack(data_mgr,chip_acc,tbdata_desktopinfo_id)
	local push_stack = 0
	local tbdata_desktopinfo = data_mgr:GetData("DesktopInfoTexas",tbdata_desktopinfo_id)
	if (tbdata_desktopinfo ~= nil)
	then
		if (chip_acc < tbdata_desktopinfo.BetMin)
		then
			push_stack = 0
		elseif(chip_acc >= tbdata_desktopinfo.BetMax)
		then
			push_stack = (tbdata_desktopinfo.BetMin + tbdata_desktopinfo.BetMax) / 2
		else
			push_stack = (tbdata_desktopinfo.BetMin + chip_acc) / 2
		end
	end

    return push_stack
end
