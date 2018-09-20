// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using DG.Tweening;
    using DG.Tweening.Core;
    using FairyGUI;
    using XLua;

    [LuaCallCSharp]
    public static class LuaHelper
    {
        //---------------------------------------------------------------------
        static Texture2D T2D;

        //---------------------------------------------------------------------
        public static string readAllText(string path)
        {
            string all_text = string.Empty;
            if (File.Exists(path))
            {
                all_text = File.ReadAllText(path);
            }
            return all_text;
        }

        //---------------------------------------------------------------------
        public static void writeFile(byte[] bytes, string path)
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }
            }
        }

        //---------------------------------------------------------------------
        public static GComponent GObjectCastToGCom(GObject obj)
        {
            return (GComponent)obj;
        }

        //---------------------------------------------------------------------
        public static Vector2 GetVector2(float x, float y)
        {
            Vector2 v;
            v.x = x;
            v.y = y;
            return v;
        }

        //---------------------------------------------------------------------
        public static Vector3 GetVector3(float x, float y, float z)
        {
            Vector3 v;
            v.x = x;
            v.y = y;
            v.z = z;
            return v;
        }

        //---------------------------------------------------------------------
        public static DisplayObject getDisObj(GObject obj)
        {
            return obj.displayObject;
        }

        //---------------------------------------------------------------------
        public static GameObject getGameObj(GObject obj)
        {
            return obj.displayObject.gameObject;
        }

        //---------------------------------------------------------------------
        public static Texture UnityObjectCastToTexture(UnityEngine.Object obj, bool need_movemipmap = false)
        {
            T2D = null;
            var t = (Texture2D)obj;
            if (need_movemipmap)
            {
                var p = t.GetPixels();
                T2D = new Texture2D(t.width, t.height, TextureFormat.ARGB32, false);
                T2D.SetPixels(p);
                T2D.Apply();
            }
            else
            {
                T2D = t;
            }

            return T2D;
        }

        //---------------------------------------------------------------------
        public static void deleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        //---------------------------------------------------------------------
        public static LuaTable spliteStr(string str, string splite_s)
        {
            var splite_str = str.Split(new string[] { splite_s }, StringSplitOptions.RemoveEmptyEntries);
            var t = CasinosContext.Instance.CasinosLua.LuaEnv.NewTable();
            int index = 1;
            foreach (var i in splite_str)
            {
                t.Set(index, i);
                index++;
            }

            return t;
        }

        //---------------------------------------------------------------------
        public static string insertToStr(string target, int index, string insert_obj)
        {
            return target.Insert(index, insert_obj);
        }

        //---------------------------------------------------------------------
        public static string formatNumToStr(float num, string format_info)
        {
            return num.ToString(format_info);
        }

        //---------------------------------------------------------------------
        public static int parseSysLanToInt()
        {
            return (int)Application.systemLanguage;
        }

        //---------------------------------------------------------------------
        public static bool objIsComponent(GObject obj)
        {
            return obj is GComponent;
        }

        //---------------------------------------------------------------------
        public static bool objIsBtn(GObject obj)
        {
            return obj is GButton;
        }

        //---------------------------------------------------------------------
        public static UIPanel addFairyGUIPanel(GameObject obj)
        {
            return obj.AddComponent<UIPanel>();
        }

        //---------------------------------------------------------------------
        public static string getDeviceUniqueIdentifier()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }

        //---------------------------------------------------------------------
        public static string getDeviceName()
        {
            return SystemInfo.deviceName;
        }

        //---------------------------------------------------------------------
        public static string getDeviceModel()
        {
            return SystemInfo.deviceModel;
        }

        //---------------------------------------------------------------------
        public static string getDeviceOperatingSystem()
        {
            return SystemInfo.operatingSystem;
        }

        //---------------------------------------------------------------------
        public static string getDevicedeviceType()
        {
            return SystemInfo.deviceType.ToString();
        }

        //---------------------------------------------------------------------
        public static GLoaderEx GLoaderCastToGLoaderEx(GLoader loader)
        {
            return (GLoaderEx)loader;
        }

        //---------------------------------------------------------------------
        public static GComponent EventDispatcherCastToGComponent(EventDispatcher ev)
        {
            return (GComponent)ev;
        }

        //---------------------------------------------------------------------
        public static Tweener DoTweenTo(DOGetter<int> g, DOSetter<int> s, int endValue, float duration)
        {
            var t = DOTween.To(g, s, endValue, duration);
            return t;
        }

        //---------------------------------------------------------------------
        public static LuaTable getIconName(bool is_small, string icon_name, string icon_resource_name1)
        {
            string icon_resource_name = icon_resource_name1;
            string icon = HeadIconMgr.getIconName(is_small, icon_name, ref icon_resource_name);
            var t = CasinosContext.Instance.CasinosLua.LuaEnv.NewTable();
            t.Set(0, icon_resource_name);
            t.Set(1, icon);
            return t;
        }

        //---------------------------------------------------------------------
        public static string bytes2StringByDefault(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }

        //---------------------------------------------------------------------
        public static string bytes2StringByUTF8(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        //---------------------------------------------------------------------
        public static string bytes2StringByUnicode(byte[] bytes)
        {
            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        //---------------------------------------------------------------------
        public static string bytes2StringByASCII(byte[] bytes)
        {
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        //---------------------------------------------------------------------
        public static byte[] string2BytesByDefault(string str)
        {
            var b = System.Text.Encoding.Default.GetBytes(str);
            return b;
        }

        //---------------------------------------------------------------------
        public static byte[] string2BytesByUTF8(string str)
        {
            var b = System.Text.Encoding.UTF8.GetBytes(str);
            return b;
        }

        //---------------------------------------------------------------------
        public static byte[] string2BytesByUnicode(string str)
        {
            var b = System.Text.Encoding.Unicode.GetBytes(str);
            return b;
        }

        //---------------------------------------------------------------------
        public static byte[] string2BytesByASCII(string str)
        {
            var b = System.Text.Encoding.ASCII.GetBytes(str);
            return b;
        }

        //---------------------------------------------------------------------
        public static short getBytesLenShort(byte[] bytes)
        {
            return (short)bytes.Length;
        }

        //---------------------------------------------------------------------
        public static ushort getBytesLenUShort(byte[] bytes)
        {
            return (ushort)bytes.Length;
        }

        //---------------------------------------------------------------------
        public static ushort parseShortToUShort(short num)
        {
            return (ushort)num;
        }

        //---------------------------------------------------------------------
        public static uint parseShortToUInt(short num)
        {
            return (uint)num;
        }

        //---------------------------------------------------------------------
        public static ulong parseShortToULong(short num)
        {
            return (ulong)num;
        }

        //---------------------------------------------------------------------
        public static int parseShortToInt(short num)
        {
            return num;
        }

        //---------------------------------------------------------------------
        public static long parseShortToLong(short num)
        {
            return num;
        }

        //---------------------------------------------------------------------
        public static short parseUShortToShort(ushort num)
        {
            return (short)num;
        }

        //---------------------------------------------------------------------
        public static uint parseUShortToUInt(ushort num)
        {
            return num;
        }

        //---------------------------------------------------------------------
        public static ulong parseUShortToULong(ushort num)
        {
            return num;
        }

        //---------------------------------------------------------------------
        public static int parseUShortToInt(ushort num)
        {
            return num;
        }

        //---------------------------------------------------------------------
        public static long parseUShortToLong(ushort num)
        {
            return num;
        }

        //---------------------------------------------------------------------
        public static long parseIntToLong(int num)
        {
            return num;
        }

        //---------------------------------------------------------------------
        public static ulong parseIntToULong(int num)
        {
            return (ulong)num;
        }

        //---------------------------------------------------------------------
        public static byte[] readBytesWithLength(byte[] bytes, int offset, int length)
        {
            byte[] b = new byte[length];
            Array.Copy(bytes, offset, b, 0, length);

            return b;
        }

        //---------------------------------------------------------------------
        public static UniWebView AddUniWebViewToObj(GameObject obj)
        {
            return obj.AddComponent<UniWebView>();
        }

        //---------------------------------------------------------------------
        public static LuaTable DictionaryToLuatable(IDictionary obj)
        {
            LuaTable lua_table = null;
            if (obj != null)
            {
                lua_table = CasinosContext.Instance.CasinosLua.LuaEnv.NewTable();
                foreach (var key in obj.Keys)
                {
                    lua_table.Set(key, obj[key]);
                }
            }

            return lua_table;
        }

        //---------------------------------------------------------------------
        public static LuaTable ListToLuatable(IList obj)
        {
            LuaTable lua_table = null;
            if (obj != null)
            {
                lua_table = CasinosContext.Instance.CasinosLua.LuaEnv.NewTable();
                for (int i = 0; i < obj.Count; i++)
                {
                    lua_table.Set(i + 1, obj[i]);
                }
            }

            return lua_table;
        }

        //---------------------------------------------------------------------
        public static TimeSpan TimeDifferenceNow(DateTime time)
        {
            var last_baozi_date = DateTime.Now - time.ToLocalTime();
            return last_baozi_date;
        }

        //---------------------------------------------------------------------
        public static string GetNowFormat(string format, bool to_local = true)
        {
            var now = DateTime.Now;
            if (to_local)
            {
                now = now.ToLocalTime();
            }
            return now.ToString(format);
        }

        //---------------------------------------------------------------------
        public static string DataTimeToString(DateTime time, string format)
        {
            return time.ToString(format);
        }

        //---------------------------------------------------------------------
        public static Dictionary<string, string> GetNewStringStringMap()
        {
            return new Dictionary<string, string>();
        }

        //---------------------------------------------------------------------
        public static Dictionary<string, Dictionary<string, string>> GetNewStringMapStringMap()
        {
            return new Dictionary<string, Dictionary<string, string>>();
        }

        //---------------------------------------------------------------------
        public static Dictionary<byte, object> GetNewByteObjMap()
        {
            return new Dictionary<byte, object>();
        }

        public static object GetHashTableValue(Hashtable t, object t_key)
        {
            return t[t_key];
        }

        //---------------------------------------------------------------------
        public static List<Card> GetNewCardList()
        {
            return new List<Card>();
        }

        //---------------------------------------------------------------------
        public static string ParseHandRankTypeTexasHToStr(HandRankTypeTexasH rank)
        {
            return rank.ToString();
        }

        //---------------------------------------------------------------------
        public static string ParseHandRankTypeTexasToStr(HandRankTypeTexas rank)
        {
            return rank.ToString();
        }

        //---------------------------------------------------------------------
        public static LoginGuestInfo JsonDeserializeLoginGuestInfo(string str_json)
        {
            return EbTool.jsonDeserialize<LoginGuestInfo>(str_json);
        }

        //---------------------------------------------------------------------
        public static LoginAccountInfo JsonDeserializeLoginAccountInfo(string str_json)
        {
            return EbTool.jsonDeserialize<LoginAccountInfo>(str_json);
        }

        //---------------------------------------------------------------------
        public static DateTime JsonDeserializeDateTime(string str_json)
        {
            return EbTool.jsonDeserialize<DateTime>(str_json);
        }

        //---------------------------------------------------------------------
        public static AutoDestroyParticle GetComponentAutoDestroyParticle(GameObject gameobject)
        {
            return gameobject.GetComponent<AutoDestroyParticle>();
        }

        //---------------------------------------------------------------------
        //public static byte[] ProtobufSerializeLong(long value)
        //{
        //    return EbTool.protobufSerialize(CasinosContext.Instance.MemoryStream, value);
        //}

        ////---------------------------------------------------------------------
        //public static byte[] ProtobufSerializePlayerAutoActionTypeTexas(PlayerAutoActionTypeTexas value)
        //{
        //    return EbTool.protobufSerialize(CasinosContext.Instance.MemoryStream, value);
        //}

        ////---------------------------------------------------------------------
        //public static byte[] ProtobufSerializeHandRankTypeTexasH(HandRankTypeTexasH card_type)
        //{
        //    return EbTool.protobufSerialize(CasinosContext.Instance.MemoryStream, card_type);
        //}

        ////---------------------------------------------------------------------
        //public static HandRankTypeTexasH ProtobufDeserializeHandRankTypeTexasH(byte[] data)
        //{
        //    return EbTool.protobufDeserialize<HandRankTypeTexasH>(CasinosContext.Instance.MemoryStream, data);
        //}

        //---------------------------------------------------------------------
        public static int EnumCastToInt(Enum e)
        {
            return Convert.ToInt32(e);
        }

        //---------------------------------------------------------------------
        public static string FormatPlayerActorId(long actor_id)
        {
            return actor_id.ToString("00-000-00");
        }

        //---------------------------------------------------------------------
        public static string FormatTmFromSecondToMinute(float tm, bool showhours)
        {
            var h = -1;
            var m = 0;
            var s = 0;
            if (showhours)
            {
                h = (int)tm / 3600;
                var temp = (int)tm % 3600;
                m = temp / 60;
                s = temp % 60;
            }
            else
            {
                m = (int)tm / 60;
                s = (int)tm % 60;
            }
            string m_str = m.ToString("00") + ":";
            string s_str = s.ToString("00");
            string h_str = h == -1 ? string.Empty : h.ToString("00") + ":";
            return h_str + m_str + s_str;
        }

        //---------------------------------------------------------------------
        public static ParticleSystem.Particle[] NewParticleArry(int length)
        {
            return new ParticleSystem.Particle[length];
        }
    }
}