--
-- lua-MessagePack : <http://fperrad.github.io/lua-MessagePack/>
--

local assert = assert
local error = error
local pairs = pairs
local pcall = pcall
local setmetatable = setmetatable
local tostring = tostring
local type = type
local char = require'string'.char
local format = require'string'.format
local math_type = require'math'.type
local tointeger = require'math'.tointeger
local tconcat = require'table'.concat
local pack = require'string'.pack
local unpack = require'string'.unpack

local _ENV = nil
local m = {}

--[[ debug only
local function hexadump (s)
    return (s:gsub('.', function (c) return format('%02X ', c:byte()) end))
end
m.hexadump = hexadump
--]]

local function argerror (caller, narg, extramsg)
    error("bad argument #" .. tostring(narg) .. " to "
          .. caller .. " (" .. extramsg .. ")")
end

local function typeerror (caller, narg, arg, tname)
    argerror(caller, narg, tname .. " expected, got " .. type(arg))
end

local function checktype (caller, narg, arg, tname)
    if type(arg) ~= tname then
        typeerror(caller, narg, arg, tname)
    end
end

local packers = setmetatable({}, {
    __index = function (t, k)
        if k == 1 then return end   -- allows ipairs
        error("pack '" .. k .. "' is unimplemented")
    end
})
m.packers = packers

packers['nil'] = function (buffer)
    buffer[#buffer+1] = char(0xC0)                      -- nil
end

packers['boolean'] = function (buffer, bool)
    if bool then
        buffer[#buffer+1] = char(0xC3)                  -- true
    else
        buffer[#buffer+1] = char(0xC2)                  -- false
    end
end

packers['string_compat'] = function (buffer, str)
    local n = #str
    if n <= 0x1F then
        buffer[#buffer+1] = char(0xA0 + n)              -- fixstr
    elseif n <= 0xFFFF then
        buffer[#buffer+1] = pack('>B I2', 0xDA, n)      -- str16
    elseif n <= 0xFFFFFFFF.0 then
        buffer[#buffer+1] = pack('>B I4', 0xDB, n)      -- str32
    else
        error"overflow in pack 'string_compat'"
    end
    buffer[#buffer+1] = str
end

packers['_string'] = function (buffer, str)
    local n = #str
    if n <= 0x1F then
        buffer[#buffer+1] = char(0xA0 + n)              -- fixstr
    elseif n <= 0xFF then
        buffer[#buffer+1] = char(0xD9, n)               -- str8
    elseif n <= 0xFFFF then
        buffer[#buffer+1] = pack('>B I2', 0xDA, n)      -- str16
    elseif n <= 0xFFFFFFFF.0 then
        buffer[#buffer+1] = pack('>B I4', 0xDB, n)      -- str32
    else
        error"overflow in pack 'string'"
    end
    buffer[#buffer+1] = str
end

packers['binary'] = function (buffer, str)
    local n = #str
    if n <= 0xFF then
        buffer[#buffer+1] = char(0xC4, n)               -- bin8
    elseif n <= 0xFFFF then
        buffer[#buffer+1] = pack('>B I2', 0xC5, n)      -- bin16
    elseif n <= 0xFFFFFFFF.0 then
        buffer[#buffer+1] = pack('>B I4', 0xC6, n)      -- bin32
    else
        error"overflow in pack 'binary'"
    end
    buffer[#buffer+1] = str
end

local set_string = function (str)
    if str == 'string_compat' then
        packers['string'] = packers['string_compat']
    elseif str == 'string' then
        packers['string'] = packers['_string']
    elseif str == 'binary' then
        packers['string'] = packers['binary']
    else
        argerror('set_string', 1, "invalid option '" .. str .."'")
    end
end
m.set_string = set_string

packers['map'] = function (buffer, tbl, n)
    if n <= 0x0F then
        buffer[#buffer+1] = char(0x80 + n)              -- fixmap
    elseif n <= 0xFFFF then
        buffer[#buffer+1] = pack('>B I2', 0xDE, n)      -- map16
    elseif n <= 0xFFFFFFFF.0 then
        buffer[#buffer+1] = pack('>B I4', 0xDF, n)      -- map32
    else
        error"overflow in pack 'map'"
    end
    for k, v in pairs(tbl) do
        packers[type(k)](buffer, k)
        packers[type(v)](buffer, v)
    end
end

packers['array'] = function (buffer, tbl, n)
    if n <= 0x0F then
        buffer[#buffer+1] = char(0x90 + n)              -- fixarray
    elseif n <= 0xFFFF then
        buffer[#buffer+1] = pack('>B I2', 0xDC, n)      -- array16
    elseif n <= 0xFFFFFFFF.0 then
        buffer[#buffer+1] = pack('>B I4', 0xDD, n)      -- array32
    else
        error"overflow in pack 'array'"
    end
    for i = 1, n do
        local v = tbl[i]
        packers[type(v)](buffer, v)
    end
end

local set_array = function (array)
    if array == 'without_hole' then
        packers['_table'] = function (buffer, tbl)
            local is_map, n, max = false, 0, 0
            for k in pairs(tbl) do
                if type(k) == 'number' and k > 0 then
                    if k > max then
                        max = k
                    end
                else
                    is_map = true
                end
                n = n + 1
            end
            if max ~= n then    -- there are holes
                is_map = true
            end
            if is_map then
                packers['map'](buffer, tbl, n)
            else
                packers['array'](buffer, tbl, n)
            end
        end
    elseif array == 'with_hole' then
        packers['_table'] = function (buffer, tbl)
            local is_map, n, max = false, 0, 0
            for k in pairs(tbl) do
                if type(k) == 'number' and k > 0 then
                    if k > max then
                        max = k
                    end
                else
                    is_map = true
                end
                n = n + 1
            end
            if is_map then
                packers['map'](buffer, tbl, n)
            else
                packers['array'](buffer, tbl, max)
            end
        end
    elseif array == 'always_as_map' then
        packers['_table'] = function(buffer, tbl)
            local n = 0
            for k in pairs(tbl) do
                n = n + 1
            end
            packers['map'](buffer, tbl, n)
        end
    else
        argerror('set_array', 1, "invalid option '" .. array .."'")
    end
end
m.set_array = set_array

packers['table'] = function (buffer, tbl)
    packers['_table'](buffer, tbl)
end

packers['unsigned'] = function (buffer, n)
    if n >= 0 then
        if n <= 0x7F then
            buffer[#buffer+1] = char(n)                 -- fixnum_pos
        elseif n <= 0xFF then
            buffer[#buffer+1] = char(0xCC, n)           -- uint8
        elseif n <= 0xFFFF then
            buffer[#buffer+1] = pack('>B I2', 0xCD, n)  -- uint16
        elseif n <= 0xFFFFFFFF.0 then
            buffer[#buffer+1] = pack('>B I4', 0xCE, n)  -- uint32
        else
            buffer[#buffer+1] = pack('>B I8', 0xCF, n)  -- uint64
        end
    else
        if n >= -0x20 then
            buffer[#buffer+1] = char(0x100 + n)         -- fixnum_neg
        elseif n >= -0x80 then
            buffer[#buffer+1] = pack('>B i1', 0xD0, n)  -- int8
        elseif n >= -0x8000 then
            buffer[#buffer+1] = pack('>B i2', 0xD1, n)  -- int16
        elseif n >= -0x80000000 then
            buffer[#buffer+1] = pack('>B i4', 0xD2, n)  -- int32
        else
            buffer[#buffer+1] = pack('>B i8', 0xD3, n)  -- int64
        end
    end
end

packers['signed'] = function (buffer, n)
    if n >= 0 then
        if n <= 0x7F then
            buffer[#buffer+1] = char(n)                 -- fixnum_pos
        elseif n <= 0x7FFF then
            buffer[#buffer+1] = pack('>B i2', 0xD1, n)  -- int16
        elseif n <= 0x7FFFFFFF then
            buffer[#buffer+1] = pack('>B i4', 0xD2, n)  -- int32
        else
            buffer[#buffer+1] = pack('>B i8', 0xD3, n)  -- int64
        end
    else
        if n >= -0x20 then
            buffer[#buffer+1] = char(0xE0 + 0x20 + n)   -- fixnum_neg
        elseif n >= -0x80 then
            buffer[#buffer+1] = pack('>B i1', 0xD0, n)  -- int8
        elseif n >= -0x8000 then
            buffer[#buffer+1] = pack('>B i2', 0xD1, n)  -- int16
        elseif n >= -0x80000000 then
            buffer[#buffer+1] = pack('>B i4', 0xD2, n)  -- int32
        else
            buffer[#buffer+1] = pack('>B i8', 0xD3, n)  -- int64
        end
    end
end

local set_integer = function (integer)
    if integer == 'unsigned' then
        packers['integer'] = packers['unsigned']
    elseif integer == 'signed' then
        packers['integer'] = packers['signed']
    else
        argerror('set_integer', 1, "invalid option '" .. integer .."'")
    end
end
m.set_integer = set_integer

packers['float'] = function (buffer, n)
    buffer[#buffer+1] = pack('>B f', 0xCA, n)
end

packers['double'] = function (buffer, n)
    buffer[#buffer+1] = pack('>B d', 0xCB, n)
end

local set_number = function (number)
    if number == 'float' then
        packers['number'] = function (buffer, n)
            if math_type(n) == 'integer' then
                packers['integer'](buffer, n)
            else
                packers['float'](buffer, n)
            end
        end
    elseif number == 'double' then
        packers['number'] = function (buffer, n)
            if math_type(n) == 'integer' then
                packers['integer'](buffer, n)
            else
                packers['double'](buffer, n)
            end
        end
    else
        argerror('set_number', 1, "invalid option '" .. number .."'")
    end
end
m.set_number = set_number

for k = 0, 4 do
    local n = tointeger(2^k)
    local fixext = 0xD4 + k
    packers['fixext' .. tostring(n)] = function (buffer, tag, data)
        assert(#data == n, "bad length for fixext" .. tostring(n))
        buffer[#buffer+1] = pack('>B i1', fixext, tag)
        buffer[#buffer+1] = data
    end
end

packers['ext'] = function (buffer, tag, data)
    local n = #data
    if n <= 0xFF then
        buffer[#buffer+1] = pack('>B B i1', 0xC7, n, tag)       -- ext8
    elseif n <= 0xFFFF then
        buffer[#buffer+1] = pack('>B I2 i1', 0xC8, n, tag)      -- ext16
    elseif n <= 0xFFFFFFFF.0 then
        buffer[#buffer+1] = pack('>B I4 i1', 0xC9, n, tag)      -- ext32
    else
        error"overflow in pack 'ext'"
    end
    buffer[#buffer+1] = data
end

function m.pack (data)
    local buffer = {}
    packers[type(data)](buffer, data)
    return tconcat(buffer)
end


local unpackers         -- forward declaration

local function unpack_cursor (c)
    local s, i, j = c.s, c.i, c.j
    if i > j then
        c:underflow(i)
        s, i, j = c.s, c.i, c.j
    end
    local val = s:sub(i, i):byte()
    c.i = i+1
    return unpackers[val](c, val)
end
m.unpack_cursor = unpack_cursor

local function unpack_str (c, n)
    local s, i, j = c.s, c.i, c.j
    local e = i+n-1
    if e > j or n < 0 then
        c:underflow(e)
        s, i, j = c.s, c.i, c.j
        e = i+n-1
    end
    c.i = i+n
    return s:sub(i, e)
end

local function unpack_array (c, n)
    local t = {}
    for i = 1, n do
        t[i] = unpack_cursor(c)
    end
    return t
end

local function unpack_map (c, n)
    local t = {}
    for i = 1, n do
        local k = unpack_cursor(c)
        local val = unpack_cursor(c)
        if k == nil or k ~= k then
            k = m.sentinel
        end
        if k ~= nil then
            t[k] = val
        end
    end
    return t
end

local function unpack_float (c)
    local s, i, j = c.s, c.i, c.j
    if i+3 > j then
        c:underflow(i+3)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+4
    return unpack('>f', s, i)
end

local function unpack_double (c)
    local s, i, j = c.s, c.i, c.j
    if i+7 > j then
        c:underflow(i+7)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+8
    return unpack('>d', s, i)
end

local function unpack_uint8 (c)
    local s, i, j = c.s, c.i, c.j
    if i > j then
        c:underflow(i)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+1
    return unpack('>I1', s, i)
end

local function unpack_uint16 (c)
    local s, i, j = c.s, c.i, c.j
    if i+1 > j then
        c:underflow(i+1)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+2
    return unpack('>I2', s, i)
end

local function unpack_uint32 (c)
    local s, i, j = c.s, c.i, c.j
    if i+3 > j then
        c:underflow(i+3)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+4
    return unpack('>I4', s, i)
end

local function unpack_uint64 (c)
    local s, i, j = c.s, c.i, c.j
    if i+7 > j then
        c:underflow(i+7)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+8
    return unpack('>I8', s, i)
end

local function unpack_int8 (c)
    local s, i, j = c.s, c.i, c.j
    if i > j then
        c:underflow(i)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+1
    return unpack('>i1', s, i)
end

local function unpack_int16 (c)
    local s, i, j = c.s, c.i, c.j
    if i+1 > j then
        c:underflow(i+1)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+2
    return unpack('>i2', s, i)
end

local function unpack_int32 (c)
    local s, i, j = c.s, c.i, c.j
    if i+3 > j then
        c:underflow(i+3)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+4
    return unpack('>i4', s, i)
end

local function unpack_int64 (c)
    local s, i, j = c.s, c.i, c.j
    if i+7 > j then
        c:underflow(i+7)
        s, i, j = c.s, c.i, c.j
    end
    c.i = i+8
    return unpack('>i8', s, i)
end

function m.build_ext (tag, data)
    return nil
end

local function unpack_ext (c, n, tag)
    local s, i, j = c.s, c.i, c.j
    local e = i+n-1
    if e > j or n < 0 then
        c:underflow(e)
        s, i, j = c.s, c.i, c.j
        e = i+n-1
    end
    c.i = i+n
    return m.build_ext(tag, s:sub(i, e))
end

unpackers = setmetatable({
    [0xC0] = function () return nil end,
    [0xC2] = function () return false end,
    [0xC3] = function () return true end,
    [0xC4] = function (c) return unpack_str(c, unpack_uint8(c)) end,    -- bin8
    [0xC5] = function (c) return unpack_str(c, unpack_uint16(c)) end,   -- bin16
    [0xC6] = function (c) return unpack_str(c, unpack_uint32(c)) end,   -- bin32
    [0xC7] = function (c) return unpack_ext(c, unpack_uint8(c), unpack_int8(c)) end,
    [0xC8] = function (c) return unpack_ext(c, unpack_uint16(c), unpack_int8(c)) end,
    [0xC9] = function (c) return unpack_ext(c, unpack_uint32(c), unpack_int8(c)) end,
    [0xCA] = unpack_float,
    [0xCB] = unpack_double,
    [0xCC] = unpack_uint8,
    [0xCD] = unpack_uint16,
    [0xCE] = unpack_uint32,
    [0xCF] = unpack_uint64,
    [0xD0] = unpack_int8,
    [0xD1] = unpack_int16,
    [0xD2] = unpack_int32,
    [0xD3] = unpack_int64,
    [0xD4] = function (c) return unpack_ext(c, 1, unpack_int8(c)) end,
    [0xD5] = function (c) return unpack_ext(c, 2, unpack_int8(c)) end,
    [0xD6] = function (c) return unpack_ext(c, 4, unpack_int8(c)) end,
    [0xD7] = function (c) return unpack_ext(c, 8, unpack_int8(c)) end,
    [0xD8] = function (c) return unpack_ext(c, 16, unpack_int8(c)) end,
    [0xD9] = function (c) return unpack_str(c, unpack_uint8(c)) end,
    [0xDA] = function (c) return unpack_str(c, unpack_uint16(c)) end,
    [0xDB] = function (c) return unpack_str(c, unpack_uint32(c)) end,
    [0xDC] = function (c) return unpack_array(c, unpack_uint16(c)) end,
    [0xDD] = function (c) return unpack_array(c, unpack_uint32(c)) end,
    [0xDE] = function (c) return unpack_map(c, unpack_uint16(c)) end,
    [0xDF] = function (c) return unpack_map(c, unpack_uint32(c)) end,
}, {
    __index = function (t, k)
        if k < 0xC0 then
            if k < 0x80 then
                return function (c, val) return val end
            elseif k < 0x90 then
                return function (c, val) return unpack_map(c, val & 0xF) end
            elseif k < 0xA0 then
                return function (c, val) return unpack_array(c, val & 0xF) end
            else
                return function (c, val) return unpack_str(c, val & 0x1F) end
            end
        elseif k > 0xDF then
            return function (c, val) return val - 0x100 end
        else
            return function () error("unpack '" .. format('%#x', k) .. "' is unimplemented") end
        end
    end
})

local function cursor_string (str)
    return {
        s = str,
        i = 1,
        j = #str,
        underflow = function ()
                        error "missing bytes"
                    end,
    }
end

local function cursor_loader (ld)
    return {
        s = '',
        i = 1,
        j = 0,
        underflow = function (self, e)
                        self.s = self.s:sub(self.i)
                        e = e - self.i + 1
                        self.i = 1
                        self.j = 0
                        while e > self.j do
                            local chunk = ld()
                            if not chunk then
                                error "missing bytes"
                            end
                            self.s = self.s .. chunk
                            self.j = #self.s
                        end
                    end,
    }
end

function m.unpack (s)
    checktype('unpack', 1, s, 'string')
    local cursor = cursor_string(s)
    local data = unpack_cursor(cursor)
    if cursor.i <= cursor.j then
        error "extra bytes"
    end
    return data
end

function m.unpacker (src)
    if type(src) == 'string' then
        local cursor = cursor_string(src)
        return function ()
            if cursor.i <= cursor.j then
                return cursor.i, unpack_cursor(cursor)
            end
        end
    elseif type(src) == 'function' then
        local cursor = cursor_loader(src)
        return function ()
            if cursor.i > cursor.j then
                pcall(cursor.underflow, cursor, cursor.i)
            end
            if cursor.i <= cursor.j then
                return true, unpack_cursor(cursor)
            end
        end
    else
        argerror('unpacker', 1, "string or function expected, got " .. type(src))
    end
end

set_string'string_compat'
set_integer'unsigned'
if #pack('n', 0.0) == 4 then
    m.small_lua = true
    unpackers[0xCB] = nil       -- double
    unpackers[0xCF] = nil       -- uint64
    unpackers[0xD3] = nil       -- int64
    set_number'float'
else
    m.full64bits = true
    set_number'double'
    if #pack('n', 0.0) > 8 then
        m.long_double = true
    end
end
set_array'without_hole'

m._VERSION = '0.5.1'
m._DESCRIPTION = "lua-MessagePack : a pure Lua 5.3 implementation"
m._COPYRIGHT = "Copyright (c) 2012-2018 Francois Perrad"
return m
--
-- This library is licensed under the terms of the MIT/X11 license,
-- like Lua itself.
--
