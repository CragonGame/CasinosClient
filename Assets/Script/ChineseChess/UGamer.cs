using UnityEngine;
using System.Collections;

// 下棋手基类，UBot，UPlayer继承此类
public class UGamer
{
    //-------------------------------------------------------------------------
    public UChessBoard Chessboard;// 对弈的棋盘
    public string name;// 名称    
    public ECampType Camp;// 阵营
    private UController _controller;// 控制器
    public UController Controller
    {
        get { return _controller; }
    }

    //-------------------------------------------------------------------------
    public void Attach(UController controller)
    {
        if (Controller != null)
        {
            Controller.OnDetach(this);
        }
        _controller = controller;
        if (Controller != null)
        {
            Controller.OnAttach(this);
        }
    }
}