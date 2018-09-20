// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public sealed class EntityMgr
    {
        //---------------------------------------------------------------------
        static EntityMgr mEntityMgr;
        Dictionary<string, IComponentFactory> mMapComponentFactory = new Dictionary<string, IComponentFactory>();
        Dictionary<string, EntityDef> mMapEntityDef = new Dictionary<string, EntityDef>();// key=entity_type
        //EntityEventPublisher mEntityEventPublisherDefault;
        EbFileStream mEntityFileStream = new EbFileStreamDefault();
        RpcSessionFactory mRpcSessionFactory = null;
        RpcSession mDefaultRpcSession = null;
        Dictionary<string, RpcSession> mMapRpcSession = new Dictionary<string, RpcSession>();// key=RpcSession Name
        Dictionary<string, Dictionary<string, Entity>> mMapAllEntity4Search1
            = new Dictionary<string, Dictionary<string, Entity>>();// key1=entity_type, key2=entity_guid
        Dictionary<string, Entity> mMapAllEntity4Search3 = new Dictionary<string, Entity>();// key=entity_guid
        Dictionary<string, Entity> mMapAllEntity4Update = new Dictionary<string, Entity>();// key=entity_guid
        Queue<Entity> mQueCreateEntity = new Queue<Entity>();// 每帧创建的Entity队列
        Queue<string> mQueSignDestroyEntity = new Queue<string>();// 每帧标识为销毁状态的Entity队列

        //---------------------------------------------------------------------
        public delegate void OnEntityCreate(Entity entity);
        public delegate void OnEntityDestroy(string entity_type, string entity_guid);
        static public EntityMgr Instance { get { return mEntityMgr; } }
        public OnEntityCreate OnEtCreate { get; set; }
        public OnEntityDestroy OnEtDestroy { get; set; }
        public byte NodeType { get; private set; }
        public string NodeTypeAsString { get; private set; }
        public bool SignDestroy { internal set; get; }
        public RpcSession DefaultRpcSession { get { return mDefaultRpcSession; } }

        //---------------------------------------------------------------------
        public EntityMgr(byte node_type, string nodetype_string)
        {
            mEntityMgr = this;
            NodeType = node_type;
            NodeTypeAsString = nodetype_string;

            //mEntityEventPublisherDefault = new EntityEventPublisher(this);
        }

        //---------------------------------------------------------------------
        public void destroy()
        {
            if (SignDestroy) return;
            SignDestroy = true;

            List<Entity> list_top_entity = new List<Entity>();
            foreach (var i in mMapAllEntity4Search3)
            {
                if (i.Value.Parent == null)
                {
                    list_top_entity.Add(i.Value);
                }
            }
            foreach (var i in list_top_entity)
            {
                destroyEntity(i);
            }

            mMapAllEntity4Search1.Clear();
            mMapAllEntity4Search3.Clear();
            mMapAllEntity4Update.Clear();
            mQueCreateEntity.Clear();
            mQueSignDestroyEntity.Clear();
        }

        //---------------------------------------------------------------------
        public void update(float elapsed_tm)
        {
            if (SignDestroy) return;

            // 每帧更新RpcSession
            foreach (var i in mMapRpcSession)
            {
                i.Value.update(elapsed_tm);
            }

            // 每帧新创建的Entity添加到mMapAllEntity4Update中
            while (mQueCreateEntity.Count > 0)
            {
                Entity entity_create = mQueCreateEntity.Dequeue();
                mMapAllEntity4Update[entity_create.Guid] = entity_create;
            }

            // 每帧更新
            foreach (var i in mMapAllEntity4Update)
            {
                if (!EbTool.isNull(i.Value))
                {
                    i.Value._update(elapsed_tm);
                }
            }

            // 每帧新销毁的Entity从mMapAllEntity4Update中移除
            while (mQueSignDestroyEntity.Count > 0)
            {
                string entity_guid = mQueSignDestroyEntity.Dequeue();
                mMapAllEntity4Update.Remove(entity_guid);
            }
        }

        //---------------------------------------------------------------------
        public void regComponent<T>() where T : IComponent, new()
        {
            IComponentFactory factory = new ComponentFactory<T>(this);
            mMapComponentFactory[factory.getName()] = factory;
        }

        //---------------------------------------------------------------------
        public void regEntityDef<TEntityDef>() where TEntityDef : EntityDef, new()
        {
            string name = typeof(TEntityDef).Name;

            var entity_def = new TEntityDef();
            entity_def.declareAllComponent(NodeType);

            mMapEntityDef[name] = entity_def;
        }

        //---------------------------------------------------------------------
        public void setRpcSessionFactory(RpcSessionFactory factory)
        {
            mRpcSessionFactory = factory;

            mDefaultRpcSession = mRpcSessionFactory.createRpcSession();
            mMapRpcSession["Default"] = mDefaultRpcSession;
        }

        //---------------------------------------------------------------------
        public RpcSession createRpcSession(string name, bool as_default = false)
        {
            if (mMapRpcSession.ContainsKey(name))
            {
                EbLog.Error("EntityMgr.createRpcSession() Failed! Exist Name=" + name);
                return null;
            }

            var rpc_session = mRpcSessionFactory.createRpcSession();
            if (as_default) mDefaultRpcSession = rpc_session;
            mMapRpcSession[name] = rpc_session;
            return rpc_session;
        }

        //---------------------------------------------------------------------
        public Dictionary<string, IComponentFactory> getMapComponentFactory()
        {
            return mMapComponentFactory;
        }

        //---------------------------------------------------------------------
        public IComponentFactory getComponentFactory(string name)
        {
            if (mMapComponentFactory.ContainsKey(name))
            {
                return mMapComponentFactory[name];
            }
            else
            {
                return null;
            }
        }

        //---------------------------------------------------------------------
        public EntityDef getEntityDef(string entity_type)
        {
            EntityDef entity_def = null;
            mMapEntityDef.TryGetValue(entity_type, out entity_def);
            if (entity_def == null)
            {
                EbLog.Error("EntityMgr.getEntityDef() Error! 不存在entity_type=" + entity_type);
            }
            return entity_def;
        }

        //---------------------------------------------------------------------
        public string getComponentName<T>() where T : IComponent
        {
            Type t = typeof(T);
            return _getComponentName(t);
        }

        //---------------------------------------------------------------------
        public string getComponentName(IComponent component)
        {
            Type t = component.GetType();
            return _getComponentName(t);
        }

        //---------------------------------------------------------------------
        public void setFileStream(EbFileStream file_stream)
        {
            mEntityFileStream = file_stream;
        }

        //---------------------------------------------------------------------
        public Entity createEntity<TEntityDef>(Dictionary<string, object> cache_data,
            Entity parent = null) where TEntityDef : EntityDef
        {
            string entity_guid = Guid.NewGuid().ToString();
            return createEntityById<TEntityDef>(entity_guid, cache_data, parent);
        }

        //---------------------------------------------------------------------
        public Entity createEntityById<TEntityDef>(string entity_guid, Dictionary<string, object> cache_data,
            Entity parent = null) where TEntityDef : EntityDef
        {
            EntityData entity_data = new EntityData();
            entity_data.entity_type = typeof(TEntityDef).Name;
            entity_data.entity_guid = entity_guid;
            entity_data.map_component = null;
            entity_data.cache_data = cache_data;

            return _createEntityImpl(entity_data, parent);
        }

        //---------------------------------------------------------------------
        public Entity createEntityByData<TEntityDef>(EntityData entity_data,
            Entity parent = null, bool recursive = false) where TEntityDef : EntityDef
        {
            return _createEntityImpl(entity_data, parent, recursive);
        }

        //---------------------------------------------------------------------
        public Entity genEntity<TEntityDef>(EntityData entity_data) where TEntityDef : EntityDef
        {
            Entity entity = new Entity(this);
            entity._reset(entity_data);
            entity._initAllComponent();
            return entity;
        }

        //---------------------------------------------------------------------
        public Entity genEntity<TEntityDef, TUserData>(EntityData entity_data, TUserData user_data) where TEntityDef : EntityDef
        {
            Entity entity = new Entity(this);
            entity._reset(entity_data);
            entity.setUserData<TUserData>(user_data);
            entity._initAllComponent();
            return entity;
        }

        //---------------------------------------------------------------------
        public Entity genEntity<TEntityDef>(Dictionary<string, object> cache_data) where TEntityDef : EntityDef
        {
            EntityData entity_data = new EntityData();
            entity_data.entity_type = typeof(TEntityDef).Name;
            entity_data.entity_guid = Guid.NewGuid().ToString();
            entity_data.cache_data = cache_data;
            entity_data.map_component = null;
            entity_data.entity_children = null;

            return genEntity<TEntityDef>(entity_data);
        }

        //---------------------------------------------------------------------
        public Entity genEntity<TEntityDef, TUserData>(Dictionary<string, object> cache_data, TUserData user_data) where TEntityDef : EntityDef
        {
            EntityData entity_data = new EntityData();
            entity_data.entity_type = typeof(TEntityDef).Name;
            entity_data.entity_guid = Guid.NewGuid().ToString();
            entity_data.cache_data = cache_data;
            entity_data.map_component = null;
            entity_data.entity_children = null;

            return genEntity<TEntityDef, TUserData>(entity_data, user_data);
        }

        //---------------------------------------------------------------------
        public void destroyEntity(Entity entity)
        {
            if (EbTool.isNull(entity)) return;

            string entity_type = entity.Type;
            string entity_guid = entity.Guid;

            // 销毁自身
            entity._destroy();

            // 从查找map中移除
            if (mMapAllEntity4Search1.ContainsKey(entity_type))
            {
                Dictionary<string, Entity> m = mMapAllEntity4Search1[entity_type];
                m.Remove(entity_guid);
            }

            mMapAllEntity4Search3.Remove(entity_guid);

            mQueSignDestroyEntity.Enqueue(entity_guid);

            // 广播Entity销毁消息
            if (OnEtDestroy != null)
            {
                OnEtDestroy(entity_type, entity_guid);
            }
        }

        //---------------------------------------------------------------------
        public void destroyEntity(string entity_guid)
        {
            Entity et = null;
            mMapAllEntity4Search3.TryGetValue(entity_guid, out et);
            destroyEntity(et);
        }

        //---------------------------------------------------------------------
        public Entity findEntity(string entity_guid)
        {
            if (SignDestroy || string.IsNullOrEmpty(entity_guid)) return null;

            Entity et = null;
            mMapAllEntity4Search3.TryGetValue(entity_guid, out et);
            return et;
        }

        //---------------------------------------------------------------------
        public Entity findFirstEntityByType<TEntityDef>() where TEntityDef : EntityDef
        {
            if (SignDestroy) return null;

            string entity_type = typeof(TEntityDef).Name;
            Entity et = null;
            Dictionary<string, Entity> m = null;
            if (mMapAllEntity4Search1.TryGetValue(entity_type, out m))
            {
                var enumerator = m.GetEnumerator();
                if (enumerator.MoveNext()) et = enumerator.Current.Value;
            }

            return et;
        }

        //---------------------------------------------------------------------
        // 返回的集合中可能包含已标记为销毁的entity，需要手动剔除
        public Dictionary<string, Entity> findEntityByType<TEntityDef>() where TEntityDef : EntityDef
        {
            if (SignDestroy) return null;

            string entity_type = typeof(TEntityDef).Name;
            Dictionary<string, Entity> m = null;
            if (mMapAllEntity4Search1.ContainsKey(entity_type))
            {
                m = new Dictionary<string, Entity>(mMapAllEntity4Search1[entity_type]);
            }

            return m;
        }

        //---------------------------------------------------------------------
        public EntityEventPublisher genEventPublisher()
        {
            return new EntityEventPublisher(this);
        }

        ////---------------------------------------------------------------------
        //public EntityEventPublisher getDefaultEventPublisher()
        //{
        //    return mEntityEventPublisherDefault;
        //}

        //---------------------------------------------------------------------
        public RpcSession getDefaultRpcSession()
        {
            return null;
        }

        //---------------------------------------------------------------------
        public RpcSession getRpcSession(string rpc_session_name)
        {
            return null;
        }

        //---------------------------------------------------------------------
        public string _getComponentName(Type t)
        {
            if (t == null) return "";
            Type[] list_t_arg = t.GetGenericArguments();
            Type t_arg = list_t_arg[0];
            return t_arg.Name;
        }

        //---------------------------------------------------------------------
        internal EbFileStream _getFileStream()
        {
            return mEntityFileStream;
        }

        //---------------------------------------------------------------------
        internal Entity _createEntityImpl(EntityData entity_data,
            Entity parent = null, bool recursive = false)
        {
            Entity entity = new Entity(this);
            entity._reset(entity_data);

            string entity_type = entity.Type;
            string entity_guid = entity.Guid;

            if (mMapAllEntity4Search1.ContainsKey(entity_type))
            {
                Dictionary<string, Entity> m = mMapAllEntity4Search1[entity_type];
                m[entity_guid] = entity;
            }
            else
            {
                Dictionary<string, Entity> m = new Dictionary<string, Entity>();
                m[entity_guid] = entity;
                mMapAllEntity4Search1[entity_type] = m;
            }

            mMapAllEntity4Search3[entity_guid] = entity;
            mQueCreateEntity.Enqueue(entity);

            // 调用Entity._setup()，让逻辑层有机会做事情
            if (parent != null)
            {
                entity.setParent(parent);
                parent._onChildInit(entity);
            }

            entity._initAllComponent();

            // 广播Entity创建消息
            if (OnEtCreate != null)
            {
                OnEtCreate(entity);
            }

            // 递归创建子Entity
            if (recursive && entity_data.entity_children != null)
            {
                foreach (var i in entity_data.entity_children)
                {
                    _createEntityImpl(i, entity, recursive);
                }
            }

            return entity;
        }
    }
}
