// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    // 场景中的地图网格中的一格
    public class EbGridRegion
    {
        //---------------------------------------------------------------------
        public EntityEventPublisher Publisher { get; private set; }
        public HashSet<Entity> SetEntity { get; private set; }

        //---------------------------------------------------------------------
        public EbGridRegion(EntityMgr entity_mgr)
        {
            Publisher = new EntityEventPublisher(entity_mgr);
            SetEntity = new HashSet<Entity>();
        }

        //---------------------------------------------------------------------
        public void entityEnterRegion(Entity entity)
        {
            SetEntity.Add(entity);

            // 发送有新Entity进入Region的消息
            var ev = Publisher.genEvent<EvSceneEntityEnterRegion>();
            ev.et = entity;
            ev.send(entity);

            Publisher.addHandler(entity);
        }

        //---------------------------------------------------------------------
        public void entityMove(Entity entity)
        {
            // 发送有新Entity在Region移动的消息
            var ev = Publisher.genEvent<EvSceneEntityMoveOnRegion>();
            ev.et = entity;
            ev.send(entity);
        }

        //---------------------------------------------------------------------
        public void entityLeaveRegion(Entity entity)
        {
            SetEntity.Remove(entity);

            Publisher.removeHandler(entity);

            // 发送有Entity离开Region的消息
            var ev = Publisher.genEvent<EvSceneEntityLeaveRegion>();
            ev.et = entity;
            ev.send(entity);
        }
    }
}
