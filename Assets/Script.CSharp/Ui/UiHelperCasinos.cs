// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using FairyGUI;
    using XLua;

    [LuaCallCSharp]
    public static class UiHelperCasinos
    {
        //---------------------------------------------------------------------
        static List<string> ListShootingTextColor = new List<string>()
        {
            "0000FF", "FFFF00", "00FFFF", "00FF00", "FF00FF","FFFFFF"
        };

        //---------------------------------------------------------------------
        //public static LuaTable UiShowPreMsgBox(string info, Action ok, Action cancel)
        //{
        //    LuaTable view = CasinosContext.Instance.createPreView("PreMsgBox");
        //    var f = view.Get<Action<string, Action, Action>>("showMsgBox");
        //    f(info, ok, cancel);
        //    return view;
        //}

        ////---------------------------------------------------------------------
        //public static LuaTable UiShowPreMsgBoxEx(string info, Action ok)
        //{
        //    LuaTable view = CasinosContext.Instance.createPreView("PreMsgBox");
        //    var f = view.Get<Action<string, Action>>("showMsgBoxEx");
        //    f(info, ok);
        //    return view;
        //}

        ////---------------------------------------------------------------------
        //public static LuaTable UiShowPreLoading(string tips, float progress)
        //{
        //    LuaTable view_loding = CasinosContext.Instance.createPreView("PreLoading");
        //    var f_settips = view_loding.Get<Action<string>>("setTip");
        //    f_settips(tips);
        //    var f_setpro = view_loding.Get<Action<float>>("setLoadingProgress");
        //    f_setpro(progress);

        //    return view_loding;
        //}

        ////---------------------------------------------------------------------
        //public static void UiEndPreLoading()
        //{
        //    LuaTable view_loding = CasinosContext.Instance.getPreView("PreLoading");
        //    CasinosContext.Instance.destroyPreView(view_loding);
        //}

        //---------------------------------------------------------------------
        public static string FormatePackageImagePath(string package_name, string image_name)
        {
            return "ui://" + package_name + "/" + image_name;
        }

        //---------------------------------------------------------------------
        public static string GetMaJiangCardResName(CardData card_data)
        {
            string card_res_name = "";
            MaJiangSuit majiang_suit = (MaJiangSuit)card_data.suit;
            if (majiang_suit != MaJiangSuit.Wan && majiang_suit != MaJiangSuit.Tong
                && majiang_suit != MaJiangSuit.Tiao)
            {
                card_res_name = majiang_suit.ToString();
            }
            else
            {
                MaJiangType majiang_type = (MaJiangType)card_data.type;
                card_res_name = majiang_type.ToString() + majiang_suit.ToString();
            }

            return card_res_name;
        }

        //---------------------------------------------------------------------
        public static string GetRandomShootingTextColor()
        {
            var index = UnityEngine.Random.Range(0, ListShootingTextColor.Count);
            return ListShootingTextColor[index];
        }

        //---------------------------------------------------------------------
        public static void setParticle(GComponent g, string particle_resouce_path)
        {
            var particle_parent = g.GetChild("ParticleParent").asGraph;
            GameObject particle = (GameObject)GameObject.Instantiate(Resources.Load(particle_resouce_path));
            particle_parent.SetNativeObject(new GoWrapper(particle));
        }

        //---------------------------------------------------------------------
        public static void SetCommonBgParticle(GComponent co_commonbg)
        {
            var particleblue_parent = co_commonbg.GetChild("ParticleBlue").asGraph;
            SetParticle(particleblue_parent, "Particle/StarBlue");
            //var particleyellow_parent = co_commonbg.GetChild("ParticleYellow").asGraph;
            //GameObject yellow = (GameObject)GameObject.Instantiate(Resources.Load("Particle/StarYellow"));
            //particleyellow_parent.SetNativeObject(new GoWrapper(yellow));
            var particleyellowright_parent = co_commonbg.GetChild("ParticleYellowRight").asGraph;
            SetParticle(particleyellowright_parent, "Particle/StarYellowRight");
        }

        //---------------------------------------------------------------------
        public static void SetLoadingBgParticle(GComponent co_commonbg)
        {
            var particleblue_parent = co_commonbg.GetChild("ParticleTop").asGraph;
            SetParticle(particleblue_parent, "Particle/Star");
            var particleyellow_parent = co_commonbg.GetChild("ParticleLeft").asGraph;
            SetParticle(particleyellow_parent, "Particle/Star");
            var particleyellowright_parent = co_commonbg.GetChild("ParticleRight").asGraph;
            SetParticle(particleyellowright_parent, "Particle/Star");
        }

        //---------------------------------------------------------------------
        public static void SetParticle(GGraph graph, string particle_name)
        {
            GameObject particle = (GameObject)GameObject.Instantiate(Resources.Load(particle_name));
            graph.SetNativeObject(new GoWrapper(particle));
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

        //---------------------------------------------------------------------
        public static string getABCardResourceTitlePath()
        {
            return CasinosContext.Instance.ABResourcePathTitle + "Cards/";
        }

        //---------------------------------------------------------------------
        public static string getABAudioResourceTitlePath()
        {
            return CasinosContext.Instance.ABResourcePathTitle + "Audio/";
        }

        //---------------------------------------------------------------------
        public static string getABItemResourceTitlePath()
        {
            return CasinosContext.Instance.ABResourcePathTitle + "Item/";
        }

        //---------------------------------------------------------------------
        public static string getABParticleResourceTitlePath()
        {
            return CasinosContext.Instance.ABResourcePathTitle + "Particle/";
        }

        //---------------------------------------------------------------------
        public static string subStrToTargetLength(string str, int target_length)
        {
            if (str.Length > target_length)
            {
                str = str.Substring(0, target_length);
            }

            return str;
        }
    }
}