PreViewLoading = PreViewBase:new()

function PreViewLoading:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    self.ViewMgr = nil
    self.GoUi = nil
    self.ComUi = nil
    self.Panel = nil
    self.UILayer = nil
    self.InitDepth = nil
    self.ViewKey = nil
    self.ShowSPine = true

    if(self.Instance==nil)
    then
        self.Instance = o
    end

    return self.Instance
end

function PreViewLoading:onCreate()
    local pro = self.ComUi:GetChild("Progress")
    if(pro ~= nil)
    then
        self.GProgressBar = pro.asProgress
        self.GProgressBar.max = 100
        self.GProgressBar.value = 0
    end

    local text = self.ComUi:GetChild("Tips")
    if(text ~= nil)
    then
        self.GTextFieldTips = text.asTextField
    end

    self.OnFinished = nil
    self.IsAuto = false
    self.ListRandomTips = {}
    CS.FairyGUI.Timers.inst:Add(0, 0, self._updateTips)

    local com_bg = self.ComUi:GetChild("ComBg")
    com_bg = self.ComUi:GetChild("ComBg")
    local image_mote = com_bg:GetChild("ImageMote").asImage
    local p_helper = ParticleHelper:new(nil)
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    if(self.ShowSPine)
    then
        image_mote.visible = false
        local abTextAtlas = p_helper:GetPreSpine("Spine/LoadingMarry/mary_loading.atlas.ab")
        local atlas = abTextAtlas:LoadAsset("Mary_Loading.atlas")
        local abtexture = p_helper:GetPreSpine("Spine/LoadingMarry/mary_loading.ab")
        local texture = abtexture:LoadAsset("Mary_Loading")
        local abjson = p_helper:GetPreSpine("Spine/LoadingMarry/mary_loadingjson.ab")
        local json = abjson:LoadAsset("Mary_LoadingJson")

        self.PlayerAnim = CS.Casinos.SpineHelper.LoadResourcesPrefab(atlas,texture,json,"Spine/Skeleton",314)
        --local moteParent = self.ComUi:GetChild("MoteParent").asCom
        self.HolderMote = self.ComUi:GetChild("HolderMote").asGraph
        --self.PlayerAnim.transform.position = moteParent.displayObject.gameObject.transform.position
        self.PlayerAnim.transform.localScale = CS.UnityEngine.Vector3(70, 70, 1000)
        --self.PlayerAnim.transform.gameObject.layer = moteParent.displayObject.gameObject.layer
        self.PlayerAnim:Initialize(false)
        self.PlayerAnim.loop = true
        self.MoteRender = self.PlayerAnim.transform.gameObject:GetComponent("MeshRenderer")
        self.PlayerAnim.AnimationName = "animation"
        self.MoteRender.sortingOrder = 4
        self.PlayerAnim.transform.gameObject.name = "LoadingMote"
        self.HolderMote:SetNativeObject(CS.FairyGUI.GoWrapper(self.PlayerAnim.transform.gameObject))
    else
        image_mote.visible = true
    end
    local bg = com_bg:GetChild("Bg")
    self:makeUiBgFiteScreen(1066,640, self.ComUi.width, self.ComUi.height, bg.width, bg.height,bg,2,{self.HolderMote})

    local abTextAtlas1 = p_helper:GetPreSpine("Spine/DengLong/denglong.atlas.ab")
    local atlas1 = abTextAtlas1:LoadAsset("denglong.atlas")
    local abtexture1 = p_helper:GetPreSpine("Spine/DengLong/denglong.ab")
    local texture1 = abtexture1:LoadAsset("denglong")
    local abjson1 = p_helper:GetPreSpine("Spine/DengLong/denglongjson.ab")
    local json1 = abjson1:LoadAsset("denglongJson")

    self.DengLongAnim = CS.Casinos.SpineHelper.LoadResourcesPrefab(atlas1,texture1,json1,"Spine/Skeleton",314)
    local denglongParent = self.ComUi:GetChild("DengLongParent").asCom
    self.DengLongAnim.transform.position = denglongParent.displayObject.gameObject.transform.position
    self.DengLongAnim.transform.localScale = CS.UnityEngine.Vector3(1.1,1.1,1)
    self.DengLongAnim.transform.gameObject.layer = denglongParent.displayObject.gameObject.layer
    self.DengLongAnim:Initialize(false)
    self.DengLongAnim.loop = true
    self.DengLongRender = self.DengLongAnim.transform.gameObject:GetComponent("MeshRenderer")
    self.DengLongAnim.AnimationName = "animation"
    self.DengLongRender.sortingOrder = 4
    self.DengLongAnim.transform.gameObject.name = "DengLong"
end

function PreViewLoading:onDestroy()
    if(self.ShowSPine)
    then
        CS.UnityEngine.GameObject.Destroy(self.PlayerAnim.transform.gameObject)
    end
    CS.UnityEngine.GameObject.Destroy(self.DengLongAnim.transform.gameObject)
    if (self.IsAuto  == true)
    then
        CS.FairyGUI.Timers.inst:Remove(self._playProgress)
    end

    CS.FairyGUI.Timers.inst:Remove(self._updateTips)
end

function PreViewLoading:onUpdate(tm)
end

function PreViewLoading:onHandleEv(ev)
end

function PreViewLoading.fireAutoLoadingProgress()
    local loading = PreViewLoading:new(nil)
    loading.GProgressBar.visible = true
    loading.IsAuto = true

    CS.FairyGUI.Timers.inst:Add(0.01, 0, loading._playProgress)
end

function PreViewLoading.fireManualLoadingProgress(progress, loading_info)
    local loading = PreViewLoading:new(nil)
    loading.IsAuto = false
    if (progress ~= 0)
    then
        loading.GProgressBar.visible = true
    end

    setTip(loading_info)
    local cur = loading.GProgressBar.value
    cur = cur + progress
    if (loading.GProgressBar ~= nil)
    then
        loading.GProgressBar.value = cur
        if (loading.GProgressBar.value < loading.GProgressBar.max)
        then
        else
            if (OnFinished ~= nil)
            then
                CS.FairyGUI.Timers.inst:Remove(loading._updateTips)
                OnFinished()
            end
        end
    end
end

function PreViewLoading.setLoadingProgress(progress)
    local loading = PreViewLoading:new(nil)
    loading.GProgressBar.visible = true
    if (loading.GProgressBar ~= nil)
    then
        loading.GProgressBar.value = progress
        if (loading.GProgressBar.value < loading.GProgressBar.max)
        then
        else
            if (OnFinished ~= nil)
            then
                CS.FairyGUI.Timers.inst:Remove(loading._updateTips)
                OnFinished()
            end
        end
    end
end

function PreViewLoading.setTip(tip)
    local loading = PreViewLoading:new(nil)
    loading.ListRandomTips = {}
    loading.ListRandomTips[tip] = tip

    local data_version = CS.Casinos.CasinosContext.Instance.Config.InitDataVersion
    local data_version_key = CS.Casinos.CasinosContext.LocalDataVersionKey
    if (CS.UnityEngine.PlayerPrefs.HasKey(data_version_key))
    then
        data_version = CS.UnityEngine.PlayerPrefs.GetString(data_version_key)
    end

    local gtext_version = loading.ComUi:GetChild("Version")
    if (gtext_version ~= nil)
    then
        local version_text = gtext_version.asTextField
        local app_version = "应用版本"
        local data_versionex = "数据版本"
        local lan = CS.Casinos.CasinosContext.Instance.CurrentLan
        if(lan == "English")
        then
            app_version = "AppVersion"
            data_versionex = "DataVersion"
        else
            if(lan == "Chinese" or lan == "ChineseSimplified")
            then
                app_version = "应用版本"
                data_versionex = "数据版本"
            end
        end
        version_text.text = string.format("%s：%s %s：%s",app_version, CS.UnityEngine.Application.version,data_versionex, data_version)
    end
end

function PreViewLoading.setTips(list_tips)
    local loading = PreViewLoading:new(nil)
    loading.ListRandomTips = {}
    for k,v in pairs(list_tips) do
        loading.ListRandomTips[k] = v
    end
end

function PreViewLoading._updateTips(param)
    local loading = PreViewLoading:new(nil)
    local tips_key,tips_value = nil
    local count = loading.PreViewMgr:GetTableCount(loading.ListRandomTips)

    if (count > 0)
    then
        tips_key,tips_value = loading.PreViewMgr:GetAndRemoveTableFirstEle(loading.ListRandomTips)
    end

    if ((tips_key ~= nil and string.len(tips_key) > 0))
    then
        if(loading.GTextFieldTips ~= nil)
        then
            loading.GTextFieldTips.text = tips_key
        end
    end
end

function PreViewLoading._playProgress(param)
    local loading = PreViewLoading:new(nil)
    if (loading.GProgressBar ~= nil)
    then
        loading.GProgressBar.visible = true
        if (loading.GProgressBar.value <= loading.GProgressBar.max)
        then
            local value = loading.GProgressBar.value
            value = value+2
            loading.GProgressBar.value = value
        else
            CS.FairyGUI.Timers.inst.Remove(loading._playProgress)

            if (OnFinished ~= nil)
            then
                OnFinished()
            end
        end
    end
end

function PreViewLoading:makeUiBgFiteScreen(design_width, design_height, logic_width, logic_height, image_width, image_height, obj, anchor_mode,t_anchor_point)
    local w = logic_width / design_width
    local h = logic_height / design_height
    if (w >= h)
    then
        obj.width = logic_width
        obj.height = logic_width * image_height / image_width
        obj.x = 0
        local y = 0
        if anchor_mode == 1 then
            y = 0
            if t_anchor_point ~= nil then
                for i, v in pairs(t_anchor_point) do
                    local p = v.xy
                    local p_y = p.y
                    local p_p = CS.UnityEngine.Vector2()
                    p_p.y = p_y - p_y/2 * w
                    p_p.x = p.x
                    v.xy = p_p
                end
            end
        elseif anchor_mode == 2 then
            y = (logic_height - obj.height) / 2
            if t_anchor_point ~= nil then
                for i, v in pairs(t_anchor_point) do
                    local p = v.xy
                    local p_y = p.y
                    local p_p = CS.UnityEngine.Vector2()
                    p_p.y = p_y + p_y / 2 * (w-h) / 2
                    p_p.x = p.x
                    v.xy = p_p
                end
            end
        elseif anchor_mode == 3 then
            y = logic_height - obj.height
            if t_anchor_point ~= nil then
                for i, v in pairs(t_anchor_point) do
                    local p = v.xy
                    local p_y = p.y
                    local p_p = CS.UnityEngine.Vector2()
                    p_p.y = p_y + p_y/2 * w
                    p_p.x = p.x
                    v.xy = p_p
                end
            end
        end
        obj.y = y
    else
        obj.height = logic_height
        obj.width = logic_height * image_width / image_height
        obj.x = (logic_width - obj.width) / 2
        obj.y = 0
        if t_anchor_point ~= nil then
            for i, v in pairs(t_anchor_point) do
                local p = v.xy
                local p_y = p.y
                local p_p = CS.UnityEngine.Vector2()
                p_p.y = p_y + p_y / 2 * (h-w) / 2
                p_p.x = p.x
                v.xy = p_p
            end
        end
    end
end




PreViewLoadingFactory = PreViewFactory:new()

function PreViewLoadingFactory:new(o,ui_package_name,ui_component_name,
                                   ui_layer,is_single,fit_screen)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

function PreViewLoadingFactory:createView()
    local view = PreViewLoading:new(nil)
    return view
end






ParticleHelper = {
}

function ParticleHelper:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    if (self.Instance == nil)
    then
        self.Instance = o
        self.TableParticle = {}
        self.TableSpine = {}
    end

    return self.Instance
end

function ParticleHelper:GetParticel(path)
    local particle = self.TableParticle[path]
    if (particle == nil)
    then
        local particle_path = self.CasinosContext.PathMgr:combinePersistentDataPath(
                ViewHelper:getABParticleResourceTitlePath() .. path)
        particle = CS.UnityEngine.AssetBundle.LoadFromFile(particle_path)
        self.TableParticle[path] = particle
    end

    return particle
end

function ParticleHelper:GetSpine(path)
    local spine = self.TableSpine[path]
    if (spine == nil)
    then
        local spine_path = self.CasinosContext.PathMgr:combinePersistentDataPath(
                self.CasinosContext.ABResourcePathTitle .. path)
        spine = CS.UnityEngine.AssetBundle.LoadFromFile(spine_path)
        self.TableSpine[path] = spine
    end

    return spine
end

function ParticleHelper:GetPreSpine(path)
    local spine = self.TableSpine[path]
    if (spine == nil)
    then
        local spine_path = self.CasinosContext.PathMgr:combinePersistentDataPath(
                self.CasinosContext.ResourcesRowPathRoot .. "PreData/Pre" .. path)
        spine = CS.UnityEngine.AssetBundle.LoadFromFile(spine_path)
        self.TableSpine[path] = spine
    end

    return spine
end