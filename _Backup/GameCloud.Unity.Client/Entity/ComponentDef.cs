// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public class ComponentDef
    {
        //---------------------------------------------------------------------
        // key=prop_name
        private Dictionary<string, PropDef> mMapPropDef = new Dictionary<string, PropDef>();
        // key=prop_name
        private Dictionary<string, IProp> mMapProp = new Dictionary<string, IProp>();
        // 脏属性集
        private Dictionary<string, IProp> mMapPropDirty = new Dictionary<string, IProp>();

        //---------------------------------------------------------------------
        public virtual void defAllProp(Dictionary<string, string> map_param)
        {
        }

        //---------------------------------------------------------------------
        public bool hasPropDef(string prop_name)
        {
            return mMapPropDef.ContainsKey(prop_name);
        }

        //---------------------------------------------------------------------
        public PropDef getPropDef(string prop_name)
        {
            PropDef prop_def = null;
            mMapPropDef.TryGetValue(prop_name, out prop_def);
            return prop_def;
        }

        //---------------------------------------------------------------------
        public bool hasProp(string prop_name)
        {
            return mMapProp.ContainsKey(prop_name);
        }

        //---------------------------------------------------------------------
        public IProp getProp(string prop_name)
        {
            IProp prop = null;
            mMapProp.TryGetValue(prop_name, out prop);
            return prop;
        }

        //---------------------------------------------------------------------
        public Prop<T> defProp<T>(Dictionary<string, string> map_param, string key, T default_value, bool collect_dirty = true)
        {
            PropDef prop_def = new PropDef(key, typeof(T), collect_dirty);
            mMapPropDef[prop_def.getKey()] = prop_def;
            Prop<T> prop = new Prop<T>(this, prop_def, default_value);
            mMapProp[prop_def.getKey()] = prop;

            if (map_param == null) return prop;

            string json = null;
            if (map_param.TryGetValue(prop_def.getKey(), out json))
            {
                if (!string.IsNullOrEmpty(json))
                {
                    prop.set(EbTool.jsonDeserialize<T>(json));
                }
            }

            return prop;
        }

        //---------------------------------------------------------------------
        public Dictionary<string, IProp> getMapProp()
        {
            return mMapProp;
        }

        //---------------------------------------------------------------------
        public Dictionary<string, string> getMapProp4SaveDb(byte current_nodetype)
        {
            if (mMapProp.Count == 0) return null;

            Dictionary<string, string> map_ret = new Dictionary<string, string>();

            FieldInfo[] list_fieldinfo = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var fi in list_fieldinfo)
            {
                if (!fi.FieldType.BaseType.Equals(typeof(IProp))) continue;

                Attribute[] attrs = System.Attribute.GetCustomAttributes(fi);
                foreach (Attribute attr in attrs)
                {
                    if (!(attr is PropAttrDistribution)) continue;

                    PropAttrDistribution a = (PropAttrDistribution)attr;
                    if (a.NodePrimary == current_nodetype && a.Save2Db)
                    {
                        IProp p = (IProp)fi.GetValue(this);
                        map_ret[p.getKey()] = EbTool.jsonSerialize(p.getValue());
                    }
                    break;
                }
            }

            return map_ret;
        }

        //---------------------------------------------------------------------
        public Dictionary<string, string> getMapProp4NetSync(byte current_nodetype, byte to_nodetype)
        {
            if (mMapProp.Count == 0) return null;

            Dictionary<string, string> map_ret = new Dictionary<string, string>();

            FieldInfo[] list_fieldinfo = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var fi in list_fieldinfo)
            {
                if (!fi.FieldType.BaseType.Equals(typeof(IProp))) continue;

                Attribute[] attrs = System.Attribute.GetCustomAttributes(fi);
                foreach (Attribute attr in attrs)
                {
                    if (!(attr is PropAttrDistribution)) continue;

                    PropAttrDistribution a = (PropAttrDistribution)attr;
                    if (a.NodePrimary == current_nodetype && a.NodeDistribution != null)
                    {
                        foreach (var i in a.NodeDistribution)
                        {
                            if (i == to_nodetype)
                            {
                                IProp p = (IProp)fi.GetValue(this);
                                map_ret[p.getKey()] = EbTool.jsonSerialize(p.getValue());
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            return map_ret;
        }

        //---------------------------------------------------------------------
        public Dictionary<string, string> getMapProp4All()
        {
            Dictionary<string, string> map_ret = new Dictionary<string, string>();
            foreach (var prop in mMapProp)
            {
                map_ret[prop.Key] = EbTool.jsonSerialize(prop.Value.getValue());
            }
            return map_ret;
        }

        //---------------------------------------------------------------------
        public Dictionary<string, string> getMapPropDirtyThenClear()
        {
            if (mMapPropDirty.Count == 0) return null;

            Dictionary<string, string> map_prop_json = new Dictionary<string, string>();
            foreach (var i in mMapPropDirty)
            {
                map_prop_json[i.Key] = EbTool.jsonSerialize(i.Value.getValue());
            }

            mMapPropDirty.Clear();

            return map_prop_json;
        }

        //---------------------------------------------------------------------
        public void applyMapPropDirty(Dictionary<string, string> map_prop_dirty)
        {
            if (map_prop_dirty == null) return;

            foreach (var j in map_prop_dirty)
            {
                if (!mMapProp.ContainsKey(j.Key)) continue;
                IProp prop = mMapProp[j.Key];
                prop.fromJsonString(j.Value);
            }
        }

        //---------------------------------------------------------------------
        public void clearMapPropDirty()
        {
            mMapPropDirty.Clear();
        }

        //---------------------------------------------------------------------
        internal void _tryAddDirtyProp(IProp prop)
        {
            if (!mMapPropDirty.ContainsKey(prop.getKey()))
            {
                mMapPropDirty.Add(prop.getKey(), prop);
            }
        }
    }
}
