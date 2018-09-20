// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using UnityEngine;
    using FairyGUI;

    public static class UiHelper
    {
        //---------------------------------------------------------------------
        static GameObject mChatPool;
        static GameObject mActorParent;
        static GameObject mPickGroundPool;
        static GameObject mActorNameAndHpPool;
        static GameObject mActorInfoParent;
        static GameObject mTextEffextParent;
        static GameObject mSkillParent;

        //static bool mIsScaleEffectEnd = false;
        public static float mDefaultHeight = 640;
        public static float mDefaultWidth = 960;

        //---------------------------------------------------------------------
        public static void setGObjectVisible(bool is_visible, GObject obj)
        {
            if (obj.visible != is_visible)
            {
                obj.visible = is_visible;
            }
        }

        //---------------------------------------------------------------------
        public static void setGObjectVisible(bool is_visible, params GObject[] objs)
        {
            foreach (var i in objs)
            {
                if (i.visible != is_visible)
                {
                    i.visible = is_visible;
                }
            }
        }

        //---------------------------------------------------------------------
        public static string uiColorText(Color color, string text)
        {
            return "[" + _colorvalude2string(color.r) + _colorvalude2string(color.g)
                + _colorvalude2string(color.b) + "]" + text + "[-]";
        }

        //---------------------------------------------------------------------
        static string _colorvalude2string(float f)
        {
            int value = (int)(f * 255);
            if (value < 0) value = 0;
            if (value > 255) value = 255;
            return value.ToString("x2");
        }

        //---------------------------------------------------------------------
        public static T getComponent<T>(GameObject obj) where T : MonoBehaviour
        {
            T[] ui_components = obj.GetComponentsInChildren<T>();

            if (ui_components.Length > 1 || ui_components.Length <= 0)
                throw new Exception("" + getGameObjectName(obj) + "\" must has only one " + typeof(T).Name + " component.");

            foreach (var comp in ui_components)
            {
                return comp;
            }

            return null;
        }

        //---------------------------------------------------------------------
        static string getGameObjectName(GameObject obj)
        {
            string name = "";
            while (obj != null)
            {
                name = obj.name + "->" + name;
                Transform parent_trans = obj.transform.parent;
                if (parent_trans != null)
                {
                    obj = obj.transform.parent.gameObject;
                }
                else
                {
                    obj = null;
                }
            }
            return "[" + name + "]";
        }

        //---------------------------------------------------------------------
        public static void resetActiveState(params GameObject[] objs)
        {
            foreach (var it in objs)
            {
                it.SetActive(false);
                it.SetActive(true);
            }
        }

        //---------------------------------------------------------------------
        public static void setActiveState(bool is_active, params GameObject[] objs)
        {
            foreach (var it in objs)
            {
                if (it.activeSelf != is_active)
                {
                    it.SetActive(is_active);
                }
            }
        }

        //---------------------------------------------------------------------
        public static void switchActiveState(params GameObject[] objs)
        {
            foreach (var it in objs)
            {
                it.SetActive(!it.activeSelf);
            }
        }

        //---------------------------------------------------------------------
        public static void switchUi(params GameObject[] uis)
        {
            if (uis.Length <= 0) return;

            int cur_index = -1;

            for (int i = 0; i < uis.Length; i++)
            {
                if (uis[i].activeSelf)
                {
                    cur_index = i;
                    break;
                }
            }

            cur_index = (cur_index + 1) % uis.Length;

            for (int i = 0; i < uis.Length; i++)
            {
                uis[i].SetActive(i == cur_index);
            }
        }

        //---------------------------------------------------------------------
        public static void switchInclusiveUi(GameObject cur, params GameObject[] uis)
        {
            if (uis.Length <= 0) return;

            foreach (var it in uis)
            {
                it.SetActive(it == cur);
            }
        }

        //---------------------------------------------------------------------
        public static string checkTime(float time, ref bool time_out)
        {
            string t = "";
            time_out = false;
            if (time <= 0)
            {
                time_out = true;
                t = "00:00";
            }
            else
            {
                string min = (Convert.ToInt32(time) / 60).ToString();
                string sec = (Convert.ToInt32(time) % 60).ToString();
                t = (Convert.ToInt32(min) < 10 ? "0" + min : min) + ":" + (Convert.ToInt32(sec) < 10 ? "0" + sec : sec);
            }
            return t;
        }

        //---------------------------------------------------------------------
        public static void resetObj(GameObject obj)
        {
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            obj.transform.localScale = Vector3.one;
        }

        //---------------------------------------------------------------------
        public static bool checkTextEmpty(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }
            return false;
        }

        //---------------------------------------------------------------------
        public static string checkTextLength(string text, int max_length)
        {
            if (max_length < 2)
            {
                return text;
            }

            byte[] bytes = System.Text.Encoding.Default.GetBytes(text);
            byte[] new_bytes = new byte[max_length - 2];

            if (bytes.Length > max_length)
            {
                for (int i = 0; i < max_length - 2; i++)
                {
                    new_bytes[i] = bytes[i];
                }
                text = System.Text.Encoding.Default.GetString(new_bytes) + "...";
            }

            return text;
        }

        //---------------------------------------------------------------------
        public static Camera GetUiCamera()
        {
            return GameObject.Find("UI Camera").GetComponent<Camera>();
        }

        //---------------------------------------------------------------------
        public static int ConvertFloatToInt(float num)
        {
            if ((int)(num + 0.5) > num)
            {
                return (int)(num + 1);
            }
            else
            {
                return (int)num;
            }
        }

        //---------------------------------------------------------------------
        public static string getOnLineTimeDays(DateTime online_time)
        {
            string result = "";
            DateTime local_onlinetm = getLocalTm(online_time);
            TimeSpan span = DateTime.Now - local_onlinetm;

            if (span.TotalDays > 60)
            {
                result = online_time.ToShortDateString();
            }
            else if (span.TotalDays > 30)
            {
                result = "1月前";
            }
            else if (span.TotalDays > 14)
            {
                result = "2周前";
            }
            else if (span.TotalDays > 7)
            {
                result = "1周前";
            }
            else if (span.TotalDays > 1)
            {
                result = string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else
            {
                if (span.TotalHours < 1)
                {
                    result = "刚刚";
                }
                else
                {
                    result = "今天";
                }
            }

            return result;
        }

        //---------------------------------------------------------------------
        public static bool timeIsSameMinute(DateTime time1, DateTime time2)
        {
            TimeSpan span = time1 - time2;

            return span.TotalMinutes <= 1 && span.TotalMinutes >= -1;
        }

        //---------------------------------------------------------------------
        public static DateTime getLocalTm(DateTime tm)
        {
            return tm.ToLocalTime();
        }

        //---------------------------------------------------------------------
        public static string getLocalTmToString(DateTime tm)
        {
            return tm.ToLocalTime().ToString("yyyy.MM.dd HH:mm");
        }

        //---------------------------------------------------------------------
        public static string formateAndoridIOSUrl(string str)
        {
            if (CasinosContext.Instance.UseHttps)
            {
                str = "https://" + str;
            }
            else
            {
                str = "http://" + str;
            }

            return str;
        }

        //---------------------------------------------------------------------
        public static string addEllipsisToStr(string str, int max_length, int show_length)
        {
            byte[] last_text = System.Text.Encoding.Default.GetBytes(str);
            if (last_text.Length > max_length)
            {
                str = str.Substring(0, show_length) + "...";
            }

            return str;
        }

        //---------------------------------------------------------------------
        public static void createGoldFallParticle()
        {
            var p = GameObject.Instantiate(Resources.Load<ParticleSystem>("Particle/FallGold"));
            Vector3 pos;
            pos.x = 0;
            pos.y = Screen.height / 2;
            pos.z = 0;
            p.transform.parent = UiHelper.GetUiCamera().transform;
            p.transform.localScale = Vector3.one;
            p.transform.localPosition = pos;
            var auto_destroy = p.GetComponent<AutoDestroyParticle>();
            auto_destroy.play();
        }
    }
}