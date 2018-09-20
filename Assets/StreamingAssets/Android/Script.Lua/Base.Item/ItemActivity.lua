-- Copyright(c) Cragon. All rights reserved.

ItemActivity = {}

function ItemActivity:new(o,type,title,content_text,content_image,is_share)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.Type = type
	o.Title = title
	o.ContenText = content_text
	o.ContentImage = content_image
	o.IsShare = is_share
	return o 
end