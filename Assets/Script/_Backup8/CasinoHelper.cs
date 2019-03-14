// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    //using XLua;
    using System.Text.RegularExpressions;

    //[LuaCallCSharp]
    public static class CasinoHelper
    {
        //---------------------------------------------------------------------
        public static string FormalUrlWithRandomVersion(string url)
        {
            CasinosContext.Instance.ClearSB();
            var sb = CasinosContext.Instance.SB;
            sb.Append(url);
            sb.Append("?v=");
            int rd = UnityEngine.Random.Range(1, 1001);
            sb.Append(rd);
            return sb.ToString();
        }

        //---------------------------------------------------------------------
        public static bool IsValidStr(string str)
        {
            bool is_valid = true;
            string pattern = @"^[A-Za-z0-9_@.-]+$";
            var mathes = Regex.Matches(str, pattern);
            if (mathes.Count <= 0)
            {
                return false;
            }

            foreach (Match match in mathes)
            {
                if (string.IsNullOrEmpty(match.Value))
                {
                    is_valid = false;
                    break;
                }
            }

            return is_valid;
        }

        //---------------------------------------------------------------------
        public static bool IsValidPhoneNum(string phone_num)
        {
            bool is_valid = true;
            string pattern = @"^1[0-9]{10}$";
            var mathes = Regex.Matches(phone_num, pattern);
            if (mathes.Count <= 0)
            {
                return false;
            }

            foreach (Match match in mathes)
            {
                if (string.IsNullOrEmpty(match.Value))
                {
                    is_valid = false;
                    break;
                }
            }

            return is_valid;
        }
    }
}