--[[ParticleHelper = {
}

function ParticleHelper:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
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
        local particle_path = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(
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
        local spine_path = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(
                CS.Casinos.CasinosContext.Instance.ABResourcePathTitle .. path)
        spine = CS.UnityEngine.AssetBundle.LoadFromFile(spine_path)
        self.TableSpine[path] = spine
    end

    return spine
end]]