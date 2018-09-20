// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public abstract class EntityDef
    {
        //---------------------------------------------------------------------
        List<string> mListComponentDef = new List<string>();

        //---------------------------------------------------------------------
        public List<string> ListComponentDef { get { return mListComponentDef; } }

        //---------------------------------------------------------------------
        public abstract void declareAllComponent(byte node_type);

        //---------------------------------------------------------------------
        public void declareComponent<T>() where T : ComponentDef
        {
            string name = typeof(T).Name;
            mListComponentDef.Add(name);
        }
    }
}
