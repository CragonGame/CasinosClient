// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class IComponentFactory
    {
        //-------------------------------------------------------------------------
        protected EntityMgr mEntityMgr = null;

        //-------------------------------------------------------------------------
        public IComponentFactory(EntityMgr entity_mgr)
        {
            mEntityMgr = entity_mgr;
        }

        //-------------------------------------------------------------------------
        public abstract string getName();

        //-------------------------------------------------------------------------
        public abstract IComponent createComponent(Entity container, Dictionary<string, string> map_param);
    }

    public class ComponentFactory<T> : IComponentFactory where T : IComponent, new()
    {
        //-------------------------------------------------------------------------
        public ComponentFactory(EntityMgr entity_mgr)
            : base(entity_mgr)
        {
        }

        //-------------------------------------------------------------------------
        public override string getName()
        {
            return mEntityMgr.getComponentName<T>();
        }

        //-------------------------------------------------------------------------
        public override IComponent createComponent(Entity container, Dictionary<string, string> map_param)
        {
            T component = new T();
            component.Entity = container;
            component.EntityMgr = mEntityMgr;
            component.EnableUpdate = true;
            component._genDef(map_param);
            return component;
        }
    }
}
