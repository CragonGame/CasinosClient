// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;

    //-------------------------------------------------------------------------
    //[Serializable]
    //[ProtoContract]
    //public class ComponentData
    //{
    //    [ProtoMember(1)]
    //    public string component_name;// 组件类工厂名
    //    [ProtoMember(2)]
    //    public Dictionary<string, string> def_propset;// 组件定义类属性集
    //}

    //-------------------------------------------------------------------------
    [Serializable]
    public class EntityData
    {
        public string entity_type;// ==entity_def_name
        public string entity_guid;
        //public List<ComponentData> list_component;
        public Dictionary<string, Dictionary<string, string>> map_component;
        [NonSerialized]
        public Dictionary<string, object> cache_data;
        public List<EntityData> entity_children;
    }
}