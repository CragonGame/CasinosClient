// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    //using XLua;

    //[LuaCallCSharp]
    public static class UiHelperCasinos
    {
        //---------------------------------------------------------------------
        public static string FormatePackageImagePath(string package_name, string image_name)
        {
            return "ui://" + package_name + "/" + image_name;
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
        public static string FormatPlayerActorId(long actor_id)
        {
            return actor_id.ToString("00-000-00");
        }
    }
}