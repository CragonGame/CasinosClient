// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public sealed class Entity
    {
        //---------------------------------------------------------------------
        Entity mParent;
        Dictionary<string, Dictionary<string, Entity>> mMapChild;// key1=entity_type, key2=entity_guid
        Dictionary<string, object> mMapCacheData;
        Dictionary<string, IComponent> mMapComponent = new Dictionary<string, IComponent>();// 用于查找
        List<IComponent> mListComponent = new List<IComponent>();// 用于遍历，具有顺序性
        EntityEventPublisher mPublisher;

        //---------------------------------------------------------------------
        public EntityMgr EntityMgr { get; private set; }
        public string Type { get; private set; }
        public string Guid { get; private set; }
        public List<IComponent> ListComponent { get { return mListComponent; } }
        public Entity Parent { get { return mParent; } }
        public bool SignDestroy { internal set; get; }
        public EntityEventPublisher Publisher { get { return mPublisher; } }

        //---------------------------------------------------------------------
        public Entity(EntityMgr entity_mgr)
        {
            EntityMgr = entity_mgr;
        }

        //---------------------------------------------------------------------
        public EntityData genEntityData4SaveDb(bool recursive = false)
        {
            EntityData entity_data = new EntityData();
            entity_data.entity_type = Type;
            entity_data.entity_guid = Guid;
            entity_data.cache_data = null;
            entity_data.map_component = new Dictionary<string, Dictionary<string, string>>();

            foreach (var i in mMapComponent)
            {
                if (!i.Value.EnableSave2Db) continue;

                Dictionary<string, string> def_propset = i.Value.getDef().getMapProp4SaveDb(EntityMgr.NodeType);
                entity_data.map_component[i.Key] = def_propset;
            }

            if (recursive && mMapChild != null)
            {
                entity_data.entity_children = new List<EntityData>();
                foreach (var i in mMapChild)
                {
                    foreach (var j in i.Value)
                    {
                        entity_data.entity_children.Add(j.Value.genEntityData4SaveDb(recursive));
                    }
                }
            }

            return entity_data;
        }

        //---------------------------------------------------------------------
        public EntityData genEntityData4NetSync(byte to_node, bool recursive = false)
        {
            EntityData entity_data = new EntityData();
            entity_data.entity_type = Type;
            entity_data.entity_guid = Guid;
            entity_data.cache_data = null;
            entity_data.map_component = new Dictionary<string, Dictionary<string, string>>();

            foreach (var i in mMapComponent)
            {
                if (!i.Value.EnableNetSync) continue;

                Dictionary<string, string> def_propset = i.Value.getDef().getMapProp4NetSync(EntityMgr.NodeType, to_node);
                entity_data.map_component[i.Key] = def_propset;
            }

            if (recursive && mMapChild != null)
            {
                entity_data.entity_children = new List<EntityData>();
                foreach (var i in mMapChild)
                {
                    foreach (var j in i.Value)
                    {
                        entity_data.entity_children.Add(j.Value.genEntityData4NetSync(to_node, recursive));
                    }
                }
            }

            return entity_data;
        }

        //---------------------------------------------------------------------
        public EntityData genEntityData4All(bool recursive = false)
        {
            EntityData entity_data = new EntityData();
            entity_data.entity_type = Type;
            entity_data.entity_guid = Guid;
            entity_data.cache_data = null;
            entity_data.map_component = new Dictionary<string, Dictionary<string, string>>();

            foreach (var i in mMapComponent)
            {
                Dictionary<string, string> def_propset = i.Value.getDef().getMapProp4All();
                entity_data.map_component[i.Key] = def_propset;
            }

            if (recursive && mMapChild != null)
            {
                entity_data.entity_children = new List<EntityData>();
                foreach (var i in mMapChild)
                {
                    foreach (var j in i.Value)
                    {
                        entity_data.entity_children.Add(j.Value.genEntityData4All(recursive));
                    }
                }
            }

            return entity_data;
        }

        //---------------------------------------------------------------------
        public TDef getComponentDef<TDef>() where TDef : ComponentDef, new()
        {
            string type_name = typeof(TDef).Name;
            IComponent co = null;
            if (mMapComponent.TryGetValue(type_name, out co))
            {
                return (TDef)((Component<TDef>)co).Def;
            }
            else
            {
                return default(TDef);
            }
        }

        //---------------------------------------------------------------------
        public T getComponent<T>() where T : IComponent
        {
            string type_name = EntityMgr.getComponentName<T>();

            IComponent co = null;
            if (mMapComponent.TryGetValue(type_name, out co))
            {
                return (T)co;
            }
            else
            {
                return default(T);
            }
        }

        //---------------------------------------------------------------------
        public IComponent getComponent(string type)
        {
            IComponent co = null;
            mMapComponent.TryGetValue(type, out co);
            return co;
        }

        //---------------------------------------------------------------------
        public void setUserData<T>(T data)
        {
            if (mMapCacheData == null)
            {
                mMapCacheData = new Dictionary<string, object>();
            }

            mMapCacheData[data.GetType().Name] = data;
        }

        //---------------------------------------------------------------------
        public T getUserData<T>()
        {
            string key = typeof(T).Name;
            if (mMapCacheData == null || !mMapCacheData.ContainsKey(key)) return default(T);
            else return (T)mMapCacheData[key];
        }

        //---------------------------------------------------------------------
        public void setCacheData(string key, object v)
        {
            if (mMapCacheData == null)
            {
                mMapCacheData = new Dictionary<string, object>();
            }

            mMapCacheData[key] = v;
        }

        //---------------------------------------------------------------------
        public object getCacheData(string key)
        {
            if (mMapCacheData == null || !mMapCacheData.ContainsKey(key)) return null;
            else return mMapCacheData[key];
        }

        //---------------------------------------------------------------------
        public bool hasCacheData(string key)
        {
            return mMapCacheData.ContainsKey(key);
        }

        //---------------------------------------------------------------------
        public void setParent(Entity parent)
        {
            mParent = parent;
            mParent._addChild(this);
        }

        //---------------------------------------------------------------------
        public void removeChild(Entity child)
        {
            if (mMapChild == null) return;

            Dictionary<string, Entity> m = null;
            if (mMapChild.TryGetValue(child.Type, out m))
            {
                m.Remove(child.Guid);
            }
        }

        //---------------------------------------------------------------------
        public Dictionary<string, Entity> getChildrenByType(string et_type)
        {
            if (mMapChild == null) return null;

            Dictionary<string, Entity> m = null;
            mMapChild.TryGetValue(et_type, out m);
            return m;
        }

        //---------------------------------------------------------------------
        public Entity getChild(string et_type, string et_guid)
        {
            if (mMapChild == null) return null;

            Entity et = null;
            Dictionary<string, Entity> m = null;
            if (mMapChild.TryGetValue(et_type, out m))
            {
                m.TryGetValue(et_guid, out et);
            }
            return et;
        }

        //---------------------------------------------------------------------
        public Dictionary<string, Dictionary<string, Entity>> getChildren()
        {
            return mMapChild;
        }

        //---------------------------------------------------------------------
        public void update(float elapsed_tm)
        {
            _update(elapsed_tm);
        }

        //---------------------------------------------------------------------
        public void close()
        {
            _destroy();
        }

        //---------------------------------------------------------------------
        public void _reset(EntityData entity_data)
        {
            SignDestroy = false;
            Type = entity_data.entity_type;
            Guid = entity_data.entity_guid;
            mMapCacheData = entity_data.cache_data;

            mPublisher = new EntityEventPublisher(EntityMgr);
            mPublisher.addHandler(this);

            EntityDef entity_def = EntityMgr.getEntityDef(entity_data.entity_type);
            if (entity_def == null) return;

            foreach (var i in entity_def.ListComponentDef)
            {
                IComponentFactory component_factory = EntityMgr.getComponentFactory(i);
                if (component_factory == null)
                {
                    EbLog.Error("Entity.addComponent() failed! can't find component_factory, component=" + i);
                    continue;
                }

                Dictionary<string, string> def_propset = null;
                if (entity_data.map_component != null)
                {
                    entity_data.map_component.TryGetValue(i, out def_propset);
                }

                var component = component_factory.createComponent(this, def_propset);
                mMapComponent[i] = component;
                mListComponent.Add(component);
                component.awake();
            }
        }

        //---------------------------------------------------------------------
        public void _initAllComponent()
        {
            foreach (var i in mListComponent)
            {
                i.init();
            }
        }

        //---------------------------------------------------------------------
        public void _releaseAllComponent()
        {
            mListComponent.Reverse();
            foreach (var i in mListComponent)
            {
                i.release();
                i.Entity = null;
                i.EntityMgr = null;
            }
            mListComponent.Reverse();
        }

        //---------------------------------------------------------------------
        // 直接销毁该Entity
        internal void _destroy()
        {
            if (SignDestroy) return;
            SignDestroy = true;

            // 先销毁所有子Entity
            if (mMapChild != null)
            {
                Dictionary<string, Dictionary<string, Entity>> map_children =
                    new Dictionary<string, Dictionary<string, Entity>>(mMapChild);
                foreach (var i in map_children)
                {
                    List<string> list_entity = new List<string>(i.Value.Keys);
                    foreach (var j in list_entity)
                    {
                        EntityMgr.destroyEntity(j);
                    }
                }
                map_children.Clear();
            }

            // 销毁Entity上挂接的所有组件
            mListComponent.Reverse();
            foreach (var i in mListComponent)
            {
                if (!EbTool.isNull(i))
                {
                    i.release();
                    i.Entity = null;
                    i.EntityMgr = null;
                }
            }
            mListComponent.Clear();
            mMapComponent.Clear();

            if (mPublisher != null)
            {
                mPublisher.removeHandler(this);
            }

            if (mMapCacheData != null)
            {
                mMapCacheData.Clear();
            }

            if (mMapChild != null)
            {
                mMapChild.Clear();
                mMapChild = null;
            }

            // 从父Entity中移除
            if (Parent != null)
            {
                Parent.removeChild(this);
            }

            Type = "";
            Guid = "";
            mParent = null;
        }

        //---------------------------------------------------------------------
        internal void _update(float elapsed_tm)
        {
            if (SignDestroy) return;

            // 循环更新Update容器中所有元素
            foreach (var i in mListComponent)
            {
                i.update(elapsed_tm);

                if (SignDestroy) break;
            }
        }

        //---------------------------------------------------------------------
        internal void _onChildInit(Entity child)
        {
            if (SignDestroy) return;

            foreach (var i in mListComponent)
            {
                i.onChildInit(child);

                if (SignDestroy) break;
            }
        }

        //---------------------------------------------------------------------
        internal void _handleEvent(object sender, EntityEvent e)
        {
            if (SignDestroy) return;

            foreach (var i in mListComponent)
            {
                i.handleEvent(sender, e);
                if (SignDestroy) break;
            }
        }

        //---------------------------------------------------------------------
        internal void _addChild(Entity child)
        {
            if (mMapChild == null)
            {
                mMapChild = new Dictionary<string, Dictionary<string, Entity>>();
            }

            string et_type = child.Type;
            string et_guid = child.Guid;
            if (mMapChild.ContainsKey(et_type))
            {
                Dictionary<string, Entity> map_child = mMapChild[et_type];
                map_child[et_guid] = child;
            }
            else
            {
                Dictionary<string, Entity> map_child = new Dictionary<string, Entity>();
                map_child[et_guid] = child;
                mMapChild[et_type] = map_child;
            }
        }
    }
}
