// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    // 编辑器相关属性描述，没有该属性描述时默认不显示在编辑器
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class PropAttrEditor : Attribute
    {
        //---------------------------------------------------------------------
        public enum Visible { Show = 0, Hide }

        //---------------------------------------------------------------------
        public string EditorName { get; private set; }
        public Visible IsVisible { get; private set; }

        //---------------------------------------------------------------------
        public PropAttrEditor(string enditor_name, Visible v = PropAttrEditor.Visible.Show)
        {
            EditorName = enditor_name;
            IsVisible = v;
        }
    }

    // 属性分布描述
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class PropAttrDistribution : Attribute
    {
        //---------------------------------------------------------------------
        public byte NodePrimary { get; private set; }
        public bool Save2Db { get; private set; }
        public byte[] NodeDistribution { get; private set; }

        //---------------------------------------------------------------------
        public PropAttrDistribution(byte node_primary, bool save2db, params byte[] node_distribution)
        {
            NodePrimary = node_primary;
            Save2Db = save2db;
            NodeDistribution = node_distribution;
        }
    }
}
