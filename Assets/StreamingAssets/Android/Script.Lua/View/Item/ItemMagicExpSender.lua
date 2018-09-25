-- 播放中的魔法表情

ItemMagicExpSender = {}

function ItemMagicExpSender:new(o,view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = view_mgr
    o.GCoMagicExpSender = CS.FairyGUI.UIPackage.CreateObject("PlayerProfile", "ComMagicExpSender").asCom
    o.GCoMagicExpSender.touchable = false
    o.GLoaderIcon = o.GCoMagicExpSender:GetChild("LoaderExp").asLoader
    o.GLoaderIcon.touchable = false
    return o
end

function ItemMagicExpSender:sendMagicExp(from_pos, to_pos,exp_tbid)
    local tb_magicexp = CS.Casinos.CasinosContext.Instance.TbDataMgrLua:GetData("UnitMagicExpression",exp_tbid)
    if (tb_magicexp == nil)
    then
        return
    end
    CS.Casinos.CasinosContext.Instance:Play(tb_magicexp.AudioName .. "m", CS.Casinos._eSoundLayer.LayerNormal)
    self.GLoaderIcon.visible = true
    self.GLoaderIcon.icon = ViewHelper:FormatePackageImagePath("PlayerProfile", tb_magicexp.ExpIcon)
    local pos_co_magic_exp = from_pos
    pos_co_magic_exp.x = pos_co_magic_exp.x - self.GCoMagicExpSender.width / 2
    pos_co_magic_exp.y = pos_co_magic_exp.y - self.GCoMagicExpSender.height / 2
    self.GCoMagicExpSender.xy = pos_co_magic_exp
    local movie_magic_exp = CS.FairyGUI.UIPackage.CreateObject("PlayerProfile", tb_magicexp.AniName).asMovieClip
    self.GCoMagicExpSender:AddChild(movie_magic_exp)
    movie_magic_exp:SetXY((self.GCoMagicExpSender.width - movie_magic_exp.width) / 2,(self.GCoMagicExpSender.height - movie_magic_exp.height) / 2)
    movie_magic_exp.visible = false
    if (tb_magicexp.MagicExpMoveType == _eMagicExpMoveType.Rotate)
    then
        local ani_rotate = self.GCoMagicExpSender:GetTransition("AniRotate")
        ani_rotate:Play()
    end
    to_pos.x = to_pos.x - self.GCoMagicExpSender.width / 2
    to_pos.y = to_pos.y - self.GCoMagicExpSender.height / 2
    self.GCoMagicExpSender:TweenMove(to_pos, 1):OnComplete(
            function()
                self.GLoaderIcon.visible = false
                movie_magic_exp.visible = true
                movie_magic_exp:SetPlaySettings(0, -1, 1, -1)
                movie_magic_exp.onPlayEnd:Add(
                        function()
                            self.GCoMagicExpSender:RemoveChild(movie_magic_exp)
                            CS.UnityEngine.GameObject.Destroy(movie_magic_exp.displayObject.gameObject)
                            local ui_pool = self.ViewMgr:getView("Pool")
                            ui_pool:magicExpEnque(self)
                        end
                )
            end
    )
end