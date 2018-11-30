-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
local _class = {}

function class(super)
    local class_type = {}
    class_type.ctor = false
    class_type.super = super
    class_type.new = function(...)
        local obj = {}
        do
            local create
            create = function(c, ...)
                if c.super then
                    create(c.super, ...)
                end
                if c.ctor then
                    c.ctor(obj, ...)
                end
            end

            create(class_type, ...)
        end
        setmetatable(obj, { __index = _class[class_type] })
        return obj
    end
    local vtbl = {}
    _class[class_type] = vtbl

    setmetatable(class_type, { __newindex = function(t, k, v)
        vtbl[k] = v
    end
    })

    if super then
        setmetatable(vtbl, { __index = function(t, k)
            local ret = _class[super][k]
            vtbl[k] = ret
            return ret
        end
        })
    end

    return class_type
end

---------------------------------------
--vb = class()-- 定义一个基类 base_type
--
--function vb:ctor(x, y)
--    -- 定义 base_type 的构造函数
--    self.x = x
--    self.y = y
--    print("viewbase.ctor() x=%s y=%s", self.x, self.y)
--end
--
--function vb:print_x()
--    -- 定义一个成员函数 base_type:print_x
--    print("viewbase.print_x() x=%s y=%s", self.x, self.y)
--end
--
--function vb:hello()
--    -- 定义另一个成员函数 base_type:hello
--    print("viewbase.hello() x=%s y=%s", self.x, self.y)
--end
--
--vshop = class(vb)    -- 定义一个类 test 继承于 base_type
--
--function vshop:ctor(m, n, o)
--    -- 定义 test 的构造函数
--    self.m = m
--    self.n = n
--    self.o = o
--    print("viewshop.ctor() x=%s y=%s m=%s n=%s o=%s", self.x, self.y, self.m, self.n, self.o)
--end
--
--function vshop:hello()
--    -- 重载 base_type:hello 为 test:hello
--    print("viewshop.hello() x=%s y=%s m=%s n=%s o=%s", self.x, self.y, self.m, self.n, self.o)
--end
--
--a = vshop.new(1, 2, 3)-- 输出两行，base_type ctor 和 test ctor 。这个对象被正确的构造了。
--a:print_x()-- 输出 1 ，这个是基类 base_type 中的成员函数。
--a:hello()-- 输出 hello test ，这个函数被重载了。
--
--b = vshop.new(5, 6)-- 输出两行，base_type ctor 和 test ctor 。这个对象被正确的构造了。
--b:print_x()-- 输出 1 ，这个是基类 base_type 中的成员函数。
--b:hello()-- 输出 hello test ，这个函数被重载了。