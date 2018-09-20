// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class EntityPool
    {
        //---------------------------------------------------------------------
        class Pool
        {
            public ushort max_entity_count;
            public Queue<Entity> que_entity;
        }

        //---------------------------------------------------------------------
        Dictionary<string, Pool> mMapEntityPool = new Dictionary<string, Pool>();

        //---------------------------------------------------------------------
        public void configPool<TEntityDef>(ushort max_entity_count) where TEntityDef : EntityDef
        {
            Pool pool = new Pool();
            pool.max_entity_count = max_entity_count;
            pool.que_entity = new Queue<Entity>(max_entity_count);
            string type_name = typeof(TEntityDef).Name;
            mMapEntityPool[type_name] = pool;
        }

        //---------------------------------------------------------------------
        public Entity genEntity<TEntityDef>(EntityData entity_data) where TEntityDef : EntityDef
        {
            string type_name = typeof(TEntityDef).Name;
            Pool pool = null;
            mMapEntityPool.TryGetValue(type_name, out pool);
            if (pool == null) return null;

            if (pool.que_entity.Count > 0)
            {
                Entity et = pool.que_entity.Dequeue();
                et._initAllComponent();
                return et;
            }
            else
            {
                return EntityMgr.Instance.genEntity<TEntityDef>(entity_data);
            }
        }

        //---------------------------------------------------------------------
        public Entity genEntity<TEntityDef, TUserData>(EntityData entity_data, TUserData user_data) where TEntityDef : EntityDef
        {
            string type_name = typeof(TEntityDef).Name;
            Pool pool = null;
            mMapEntityPool.TryGetValue(type_name, out pool);
            if (pool == null) return null;

            if (pool.que_entity.Count > 0)
            {
                Entity et = pool.que_entity.Dequeue();
                et._initAllComponent();
                return et;
            }
            else
            {
                return EntityMgr.Instance.genEntity<TEntityDef, TUserData>(entity_data, user_data);
            }
        }

        //---------------------------------------------------------------------
        public Entity genEntity<TEntityDef>(Dictionary<string, object> cache_data) where TEntityDef : EntityDef
        {
            string type_name = typeof(TEntityDef).Name;
            Pool pool = null;
            mMapEntityPool.TryGetValue(type_name, out pool);
            if (pool == null) return null;

            if (pool.que_entity.Count > 0)
            {
                Entity et = pool.que_entity.Dequeue();
                et._initAllComponent();
                return et;
            }
            else
            {
                return EntityMgr.Instance.genEntity<TEntityDef>(cache_data);
            }
        }

        //---------------------------------------------------------------------
        public Entity genEntity<TEntityDef, TUserData>(Dictionary<string, object> cache_data, TUserData user_data) where TEntityDef : EntityDef
        {
            string type_name = typeof(TEntityDef).Name;
            Pool pool = null;
            mMapEntityPool.TryGetValue(type_name, out pool);
            if (pool == null) return null;

            if (pool.que_entity.Count > 0)
            {
                Entity et = pool.que_entity.Dequeue();
                et._initAllComponent();
                return et;
            }
            else
            {
                return EntityMgr.Instance.genEntity<TEntityDef, TUserData>(cache_data, user_data);
            }
        }

        //---------------------------------------------------------------------
        public void freeEntity<TEntityDef>(Entity et) where TEntityDef : EntityDef
        {
            string type_name = typeof(TEntityDef).Name;
            Pool pool = null;
            mMapEntityPool.TryGetValue(type_name, out pool);
            if (pool == null)
            {
                et.close();
                return;
            }

            if (pool.que_entity.Count > pool.max_entity_count)
            {
                et.close();
            }
            else
            {
                et._releaseAllComponent();
                pool.que_entity.Enqueue(et);
            }
        }
    }
}
