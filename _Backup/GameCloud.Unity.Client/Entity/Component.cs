// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class IComponent
    {
        //---------------------------------------------------------------------
        public Entity Entity { internal set; get; }
        public EntityMgr EntityMgr { internal set; get; }
        public EntityEventPublisher Publisher { get { return Entity.Publisher; } }
        public RpcSession DefaultRpcSession { get { return EntityMgr.Instance.DefaultRpcSession; } }
        public bool EnableUpdate { set; get; }
        public bool EnableSave2Db { set; get; }
        public bool EnableNetSync { get; set; }
        internal bool _Init { set; get; }

        //---------------------------------------------------------------------
        public IComponent()
        {
            EnableUpdate = true;
            EnableSave2Db = true;
            EnableNetSync = true;
        }

        //---------------------------------------------------------------------
        public abstract void awake();

        //---------------------------------------------------------------------
        public abstract void init();

        //---------------------------------------------------------------------
        public abstract void release();

        //---------------------------------------------------------------------
        public abstract void update(float elapsed_tm);

        //---------------------------------------------------------------------
        public abstract void handleEvent(object sender, EntityEvent e);

        //---------------------------------------------------------------------
        public abstract void onChildInit(Entity child);

        //---------------------------------------------------------------------
        public abstract ComponentDef getDef();

        //---------------------------------------------------------------------
        internal abstract void _genDef(Dictionary<string, string> map_param);
    }

    public class Component<TDef> : IComponent where TDef : ComponentDef, new()
    {
        //---------------------------------------------------------------------
        private TDef mDef = null;

        //---------------------------------------------------------------------
        public TDef Def { private set { mDef = value; } get { return mDef; } }

        //---------------------------------------------------------------------
        public override void awake()
        {
        }

        //---------------------------------------------------------------------
        public override void init()
        {
        }

        //---------------------------------------------------------------------
        public override void release()
        {
        }

        //---------------------------------------------------------------------
        public override void update(float elapsed_tm)
        {
        }

        //---------------------------------------------------------------------
        public override void handleEvent(object sender, EntityEvent e)
        {
        }

        //---------------------------------------------------------------------
        public override void onChildInit(Entity child)
        {
        }

        //---------------------------------------------------------------------
        public override ComponentDef getDef()
        {
            return mDef;
        }

        //---------------------------------------------------------------------
        internal override void _genDef(Dictionary<string, string> map_param)
        {
            mDef = new TDef();
            mDef.defAllProp(map_param);
        }
    }
}
