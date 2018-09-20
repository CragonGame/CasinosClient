using System;
using System.Collections.Generic;
using System.Text;
using GameCloud.Unity.Common;

public interface IEcEngineListener
{
    //---------------------------------------------------------------------
    void init(EntityMgr entity_mgr, Entity et_node);

    //---------------------------------------------------------------------
    void release();
}

public struct EcEngineSettings
{
    public string ProjectName;
    public string RootEntityType;
}

public sealed class EcEngine
{
    //---------------------------------------------------------------------
    static EcEngine mInstance;
    IEcEngineListener mListener;
    EntityMgr mEntityMgr;

    //---------------------------------------------------------------------
    public static EcEngine Instance { get { return mInstance; } }
    public EntityMgr EntityMgr { get { return mEntityMgr; } }
    public EcEngineSettings Settings { get; private set; }
    
    //---------------------------------------------------------------------
    public EcEngine(ref EcEngineSettings settings, IEcEngineListener listener)
    {
        mInstance = this;
        mListener = listener;
        Settings = settings;

        mEntityMgr = new EntityMgr(1, "Client");

        mEntityMgr.setRpcSessionFactory(new RpcSessionFactoryTcpClient());

        //mEntityMgr.regComponent<ClientAutoPatcher<DefAutoPatcher>>();
        //mEntityMgr.regComponent<ClientNode<DefNode>>();

        //mEntityMgr.regEntityDef<EtAutoPatcher>();
        //mEntityMgr.regEntityDef<EtNode>();

        //EtNode = mEntityMgr.createEntity<EtNode>(null, null);
        //var co_node = EtNode.getComponent<ClientNode<DefNode>>();

        //// 通知业务层初始化
        //if (mListener != null)
        //{
        //    mListener.init(mEntityMgr, EtNode);
        //}
    }

    //---------------------------------------------------------------------
    public void update(float elapsed_tm)
    {
        try
        {
            mEntityMgr.update(elapsed_tm);
        }
        catch (Exception ec)
        {
            EbLog.Error("EcEngine.update() Exception: " + ec);
        }
    }

    //---------------------------------------------------------------------
    public void close()
    {
        // 通知业务层销毁
        if (mListener != null)
        {
            mListener.release();
        }

        if (mEntityMgr != null)
        {
            mEntityMgr.destroy();
            mEntityMgr = null;
        }
    }
}
