using System;

public static class EbTool
{
    //-------------------------------------------------------------------------
    //public static byte[] protobufSerialize<T>(MemoryStream s, T obj)
    //{
    //    s.Seek(0, SeekOrigin.Begin);
    //    s.SetLength(0);

    //    byte[] data = null;

    //    try
    //    {
    //        ProtoBuf.Serializer.Serialize<T>(s, obj);
    //        data = s.ToArray();
    //    }
    //    catch (Exception ex)
    //    {
    //        EbLog.Error(ex.ToString());
    //    }

    //    return data;
    //}

    ////-------------------------------------------------------------------------
    //public static T protobufDeserialize<T>(MemoryStream s, byte[] data)
    //{
    //    s.Seek(0, SeekOrigin.Begin);
    //    if (s.Length > data.Length) s.SetLength(0);
    //    s.Write(data, 0, data.Length);
    //    s.Seek(0, SeekOrigin.Begin);

    //    T obj = default(T);

    //    try
    //    {
    //        obj = ProtoBuf.Serializer.Deserialize<T>(s);
    //    }
    //    catch (Exception ex)
    //    {
    //        EbLog.Error(ex.ToString());
    //    }

    //    return obj;
    //}

    //-------------------------------------------------------------------------
    public static string jsonSerialize(object obj)
    {
        return UnityEngine.JsonUtility.ToJson(obj);
        //return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }

    //-------------------------------------------------------------------------
    public static T jsonDeserialize<T>(string str_json)
    {
        return UnityEngine.JsonUtility.FromJson<T>(str_json);
        //return (T)Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str_json);
    }

    //-------------------------------------------------------------------------
    // 根据年月日计算星期几
    public static string caculateWeekDay(int y, int m, int d)
    {
        if (m == 1) m = 13;
        if (m == 2) m = 14;
        int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;
        string weekstr = "";
        switch (week)
        {
            case 1: weekstr = "星期一"; break;
            case 2: weekstr = "星期二"; break;
            case 3: weekstr = "星期三"; break;
            case 4: weekstr = "星期四"; break;
            case 5: weekstr = "星期五"; break;
            case 6: weekstr = "星期六"; break;
            case 0: weekstr = "星期日"; break;
        }

        return weekstr;
    }

    //-------------------------------------------------------------------------
    // 18位身份证验证
    public static string checkCidInfo(string cid)
    {
        string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
        double iSum = 0;

        System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|x)$");
        System.Text.RegularExpressions.Match mc = rg.Match(cid);
        if (!mc.Success)
        {
            return "";
        }
        cid = cid.ToLower();
        cid = cid.Replace("x", "a");
        if (aCity[int.Parse(cid.Substring(0, 2))] == null)
        {
            return "非法地区";
        }
        try
        {
            DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
        }
        catch
        {
            return "非法生日";
        }
        for (int i = 17; i >= 0; i--)
        {
            iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);

        }
        if (iSum % 11 != 1) return ("非法证号");

        return (aCity[int.Parse(cid.Substring(0, 2))] + "," + cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2) + "," + (int.Parse(cid.Substring(16, 1)) % 2 == 1 ? "男" : "女"));
    }
}