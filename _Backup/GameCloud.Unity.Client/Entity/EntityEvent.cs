// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EntityEvent
    {
        //---------------------------------------------------------------------
        public EntityEvent() : base() { }

        //---------------------------------------------------------------------
        internal EntityEventPublisher Publisher { get; set; }

        //---------------------------------------------------------------------
        public void send(object sender)
        {
            Publisher._publish(sender, this);
        }
    }

    public class EntityEventPublisher
    {
        //---------------------------------------------------------------------
        Dictionary<string, Entity> mMapEntity = new Dictionary<string, Entity>();
        EntityMgr mEntityMgr;

        //---------------------------------------------------------------------
        public EntityEventPublisher(EntityMgr entity_mgr)
        {
            mEntityMgr = entity_mgr;
        }

        //---------------------------------------------------------------------
        public void addHandler(Entity entity)
        {
            mMapEntity[entity.Guid] = entity;
        }

        //---------------------------------------------------------------------
        public void removeHandler(string et_guid)
        {
            if (!string.IsNullOrEmpty(et_guid))
            {
                mMapEntity.Remove(et_guid);
            }
        }

        //---------------------------------------------------------------------
        public void removeHandler(Entity entity)
        {
            if (entity != null && !string.IsNullOrEmpty(entity.Guid))
            {
                mMapEntity.Remove(entity.Guid);
            }
        }

        //---------------------------------------------------------------------
        public T genEvent<T>() where T : EntityEvent, new()
        {
            T ev = new T();
            ev.Publisher = this;
            return ev;
        }

        //---------------------------------------------------------------------
        internal void _publish(object sender, EntityEvent e)
        {
            var map_entity = new Dictionary<string, Entity>(mMapEntity);
            foreach (var i in map_entity)
            {
                if (mMapEntity.ContainsKey(i.Key)) i.Value._handleEvent(sender, e);
            }

            e.Publisher = null;
        }
    }
}
