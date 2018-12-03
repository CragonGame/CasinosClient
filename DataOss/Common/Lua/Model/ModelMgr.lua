-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ModelMgr = {
    MapAssetBundle = {}, -- key=AssetBundle Name, value=AssetBundle
}

---------------------------------------
function ModelMgr:Setup()
end

---------------------------------------
-- 是否加载过指定名字的AssetBundle
function ModelMgr:QueryAssetBundle(ab_name)
    local ab = self.MapAssetBundle[ab_name];
    return ab
end

---------------------------------------
-- 确认加载过指定名字的AssetBundle
function ModelMgr:AddAssetBundle(ab_name, ab)
    --table.insert(self.MapAssetBundle, ab_name, ab)
    self.MapAssetBundle[ab_name] = ab
end