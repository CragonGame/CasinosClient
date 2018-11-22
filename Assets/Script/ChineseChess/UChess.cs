using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// 棋子类型
public enum EChessType
{
    Ju,
    Ma,
    Pao,
    Xiang,
    Shi,
    Shuai,
    Bing,
    Count
}

[Serializable]
public struct ChessData
{
    // 棋子名称
    public string name;
    // 棋子所属阵营(红或蓝)
    public ECampType campType;
    // 棋子坐标（以本方阵营为准，从右到左X轴1到9，从下到上Y轴1到10）
    public Point point;
    // 棋子类型
    public EChessType chessType;
    // 预制体对象索引
    public GameObject prefab;
}

// 棋子基类
public abstract class UChess
{
    public string name;// 棋子名称
    public ECampType campType;// 棋子所属阵营(红或蓝)
    public EChessType chessType;
    public Point point;// 棋子坐标（以本方阵营为准，从右到左X轴1到9，从下到上Y轴1到10）
    protected GameObject prefab;// 预制体对象索引
    public GameObject gameObject;// 实例化对象

    UChessBoard _chessboard;
    public UChessBoard chessboard
    {
        get
        {
            return _chessboard;
        }
    }

    // 是否显示棋子
    bool show
    {
        get { return chessboard.show; }
    }

    public virtual void InitData(ChessData data, UChessBoard board)
    {
        this.name = data.name;
        this.campType = data.campType;
        this.point = data.point;
        this.prefab = data.prefab;
        this.chessType = data.chessType;
        _chessboard = board;
    }

    // 返回可以走动的位置
    public abstract List<Point> GetAvailablePoints();

    // 辅助函数，修正可移动点中我方棋子占据的点，并且移除当前位置的点
    protected List<Point> ModifyChessPoint(List<Point> allAvailablePoints)
    {
        List<Point> R = new List<Point>();
        foreach (var pt in allAvailablePoints)
        {
            if (pt == point)
                continue;
            UChess ptChess = chessboard[chessboard.ToChessboardPoint(pt, campType)];
            if (ptChess != null && ptChess.campType == campType)
                continue;
            R.Add(pt);
        }
        return R;
    }

    // 返回可吃掉别人棋子的点
    public List<Point> GetEatPoints()
    {
        List<Point> R = new List<Point>();
        List<Point> AvailablePoints = GetAvailablePoints();
        for (int i = 0; i < AvailablePoints.Count; i++)
        {
            UChess chess = chessboard[chessboard.ToChessboardPoint(AvailablePoints[i], campType)];
            if (chess != null && chess.campType != campType)
                R.Add(AvailablePoints[i]);
        }
        return R;
    }

    // 返回棋盘坐标 （以红方为准，从左到右 0-8，从下到上0-9）
    public Point ToChessboardPoint()
    {
        return chessboard.ToChessboardPoint(point, campType);
    }

    // 返回棋子的三维世界坐标
    public Vector3 GetWorldPosstion()
    {
        return chessboard.PosToWorld(ToChessboardPoint());
    }

    // 还原显示位置
    public void ResetPosition()
    {
        if (show)
        {
            gameObject.transform.position = GetWorldPosstion();
        }
    }

    public void onAdded()
    {
        if (show)
        {
            //创建棋子
            gameObject = GameObject.Instantiate<GameObject>(prefab);
            //坐标
            gameObject.transform.position = GetWorldPosstion();
            if (campType == ECampType.Red)
            {
                Vector3 Eurler = gameObject.transform.localEulerAngles;
                Eurler.y = 180;
                gameObject.transform.localEulerAngles = Eurler;
            }
        }
    }
    public void onRemoved()
    {
        if (show)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void onMoved(Point lastPos, Point newPos)
    {
        if (show)
        {
            gameObject.transform.position = GetWorldPosstion();
        }
    }

    protected bool IsValidPoint(Point pt)
    {
        return pt.x >= 1 && pt.x <= 9 && pt.y >= 1 && pt.y <= 10;
    }
}

// 车
public class UChess_Ju : UChess
{
    // 横竖线的所有可移动点
    public override List<Point> GetAvailablePoints()
    {
        List<Point> R = new List<Point>();

        Point PT = point;
        // X轴负向
        while (PT.x > 1)
        {
            PT.x--;
            Point cbPoint = chessboard.ToChessboardPoint(PT, campType);
            UChess chess = chessboard[cbPoint];
            R.Add(PT);
            if (chess != null)
            {
                break;
            }
        }

        // X轴正向
        PT = point;
        while (PT.x < 9)
        {
            PT.x++;
            Point cbPoint = chessboard.ToChessboardPoint(PT, campType);
            UChess chess = chessboard[cbPoint];
            R.Add(PT);
            if (chess != null)
            {
                break;
            }
        }

        // Y轴负向
        PT = point;
        while (PT.y > 1)
        {
            PT.y--;
            Point cbPoint = chessboard.ToChessboardPoint(PT, campType);
            UChess chess = chessboard[cbPoint];
            R.Add(PT);
            if (chess != null)
            {
                break;
            }
        }

        // X轴正向
        PT = point;
        while (PT.y < 10)
        {
            PT.y++;
            Point cbPoint = chessboard.ToChessboardPoint(PT, campType);
            UChess chess = chessboard[cbPoint];
            R.Add(PT);
            if (chess != null)
            {
                break;
            }
        }
        return ModifyChessPoint(R);
    }
}

// 马
public class UChess_Ma : UChess
{
    public override List<Point> GetAvailablePoints()
    {
        List<Point> R = new List<Point>();

        UChess leftchess = null;
        UChess rightchess = null;
        UChess upchess = null;
        UChess downchess = null;
        // 日字型
        Point pt_left = new Point(point.x - 1, point.y);
        Point pt_right = new Point(point.x + 1, point.y);
        Point pt_up = new Point(point.x, point.y + 1);
        Point pt_down = new Point(point.x, point.y - 1);

        if (IsValidPoint(pt_left))
        {
            leftchess = chessboard[chessboard.ToChessboardPoint(pt_left, campType)];
        }
        if (IsValidPoint(pt_right))
        {
            rightchess = chessboard[chessboard.ToChessboardPoint(pt_right, campType)];
        }
        if (IsValidPoint(pt_up))
        {
            upchess = chessboard[chessboard.ToChessboardPoint(pt_up, campType)];
        }
        if (IsValidPoint(pt_down))
        {
            downchess = chessboard[chessboard.ToChessboardPoint(pt_down, campType)];
        }

        if (leftchess == null)
        {
            Point left1 = new Point(point.x - 2, point.y - 1);
            Point left2 = new Point(point.x - 2, point.y + 1);
            if (IsValidPoint(left1))
            {
                R.Add(left1);
            }
            if (IsValidPoint(left2))
            {
                R.Add(left2);
            }
        }

        if (rightchess == null)
        {
            Point right1 = new Point(point.x + 2, point.y - 1);
            Point right2 = new Point(point.x + 2, point.y + 1);
            if (IsValidPoint(right1))
            {
                R.Add(right1);
            }
            if (IsValidPoint(right2))
            {
                R.Add(right2);
            }
        }

        if (upchess == null)
        {
            Point up1 = new Point(point.x - 1, point.y + 2);
            Point up2 = new Point(point.x + 1, point.y + 2);
            if (IsValidPoint(up1))
            {
                R.Add(up1);
            }
            if (IsValidPoint(up2))
            {
                R.Add(up2);
            }
        }

        if (downchess == null)
        {
            Point down1 = new Point(point.x - 1, point.y - 2);
            Point down2 = new Point(point.x + 1, point.y - 2);
            if (IsValidPoint(down1))
            {
                R.Add(down1);
            }
            if (IsValidPoint(down2))
            {
                R.Add(down2);
            }
        }

        return ModifyChessPoint(R);
    }
}

// 炮
public class UChess_Pao : UChess
{
    public override List<Point> GetAvailablePoints()
    {
        List<Point> R = new List<Point>();

        Point PT = point;
        // X轴负向
        while (PT.x > 1)
        {
            PT.x--;
            Point cbPoint = chessboard.ToChessboardPoint(PT, campType);
            UChess chess = chessboard[cbPoint];
            if (chess != null)
            {
                // 射击点
                while (PT.x > 1)
                {
                    PT.x--;
                    UChess chess2 = chessboard[chessboard.ToChessboardPoint(PT, campType)];
                    if (chess2 != null)
                    {
                        R.Add(PT);
                        break;
                    }
                }

                break;
            }
            R.Add(PT);
        }

        // X轴正向
        PT = point;
        while (PT.x < 9)
        {
            PT.x++;
            Point cbPoint = chessboard.ToChessboardPoint(PT, campType);
            UChess chess = chessboard[cbPoint];
            if (chess != null)
            {
                // 射击点
                while (PT.x < 9)
                {
                    PT.x++;
                    UChess chess2 = chessboard[chessboard.ToChessboardPoint(PT, campType)];
                    if (chess2 != null)
                    {
                        R.Add(PT);
                        break;
                    }
                }
                break;
            }
            R.Add(PT);
        }

        // Y轴负向
        PT = point;
        while (PT.y > 1)
        {
            PT.y--;
            Point cbPoint = chessboard.ToChessboardPoint(PT, campType);
            UChess chess = chessboard[cbPoint];
            if (chess != null)
            {
                // 射击点
                while (PT.y > 1)
                {
                    PT.y--;
                    UChess chess2 = chessboard[chessboard.ToChessboardPoint(PT, campType)];
                    if (chess2 != null)
                    {
                        R.Add(PT);
                        break;
                    }
                }
                break;
            }
            R.Add(PT);
        }

        // Y轴正向
        PT = point;
        while (PT.y < 10)
        {
            PT.y++;
            Point cbPoint = chessboard.ToChessboardPoint(PT, campType);
            UChess chess = chessboard[cbPoint];
            if (chess != null)
            {
                // 射击点
                while (PT.y < 10)
                {
                    PT.y++;
                    UChess chess2 = chessboard[chessboard.ToChessboardPoint(PT, campType)];
                    if (chess2 != null)
                    {
                        R.Add(PT);
                        break;
                    }
                }
                break;
            }
            R.Add(PT);
        }

        return ModifyChessPoint(R);
    }
}

// 象
public class UChess_Xiang : UChess
{
    bool ValidPoint(Point pt)
    {
        return IsValidPoint(pt) && pt.y <= 5;
    }

    public override List<Point> GetAvailablePoints()
    {
        List<Point> R = new List<Point>();

        Point pt1 = new Point(point.x - 1, point.y - 1);
        Point pt2 = new Point(point.x + 1, point.y - 1);
        Point pt3 = new Point(point.x - 1, point.y + 1);
        Point pt4 = new Point(point.x + 1, point.y + 1);
        UChess chess1 = null;
        UChess chess2 = null;
        UChess chess3 = null;
        UChess chess4 = null;

        if (ValidPoint(pt1))
        {
            chess1 = chessboard[chessboard.ToChessboardPoint(pt1, campType)];
        }
        if (IsValidPoint(pt2))
        {
            chess2 = chessboard[chessboard.ToChessboardPoint(pt2, campType)];
        }
        if (IsValidPoint(pt3))
        {
            chess3 = chessboard[chessboard.ToChessboardPoint(pt3, campType)];
        }
        if (IsValidPoint(pt4))
        {
            chess4 = chessboard[chessboard.ToChessboardPoint(pt4, campType)];
        }

        Point ptTarget1 = new Point(point.x - 2, point.y - 2);
        Point ptTarget2 = new Point(point.x + 2, point.y - 2);
        Point ptTarget3 = new Point(point.x - 2, point.y + 2);
        Point ptTarget4 = new Point(point.x + 2, point.y + 2);

        if (chess1 == null && ValidPoint(ptTarget1))
            R.Add(ptTarget1);
        if (chess2 == null && ValidPoint(ptTarget2))
            R.Add(ptTarget2);
        if (chess3 == null && ValidPoint(ptTarget3))
            R.Add(ptTarget3);
        if (chess4 == null && ValidPoint(ptTarget4))
            R.Add(ptTarget4);
        return ModifyChessPoint(R);
    }
}

// 士
public class UChess_Shi : UChess
{
    bool ValidPoint(Point pt)
    {
        return IsValidPoint(pt) && pt.y <= 3 && pt.x >= 4 && pt.x <= 6;
    }

    public override List<Point> GetAvailablePoints()
    {
        Point pt1 = new Point(point.x - 1, point.y - 1);
        Point pt2 = new Point(point.x + 1, point.y - 1);
        Point pt3 = new Point(point.x - 1, point.y + 1);
        Point pt4 = new Point(point.x + 1, point.y + 1);
        List<Point> R = new List<Point>();
        if (ValidPoint(pt1)) R.Add(pt1);
        if (ValidPoint(pt2)) R.Add(pt2);
        if (ValidPoint(pt3)) R.Add(pt3);
        if (ValidPoint(pt4)) R.Add(pt4);
        return ModifyChessPoint(R);
    }
}

// 兵
public class UChess_Bing : UChess
{
    bool ValidPoint(Point pt)
    {
        if (!IsValidPoint(pt))
            return false;

        if (point.y <= 5)
            return point.x == pt.x;
        else
            return Mathf.Abs(point.x - pt.x) <= 1;
    }

    public override List<Point> GetAvailablePoints()
    {
        Point pt1 = new Point(point.x - 1, point.y);
        Point pt2 = new Point(point.x + 1, point.y);
        Point pt3 = new Point(point.x, point.y + 1);
        List<Point> R = new List<Point>();
        if (ValidPoint(pt1))
            R.Add(pt1);
        if (ValidPoint(pt2))
            R.Add(pt2);
        if (ValidPoint(pt3))
            R.Add(pt3);
        return ModifyChessPoint(R);
    }
}

// 帅
public class UChess_Shuai : UChess
{
    bool ValidPoint(Point pt)
    {
        return pt.x >= 4 && pt.x <= 6 && pt.y >= 1 && pt.y <= 3;
    }

    public override List<Point> GetAvailablePoints()
    {
        Point pt1 = new Point(point.x - 1, point.y);
        Point pt2 = new Point(point.x + 1, point.y);
        Point pt3 = new Point(point.x, point.y + 1);
        Point pt4 = new Point(point.x, point.y - 1);

        List<Point> R = new List<Point>();
        if (ValidPoint(pt1))
            R.Add(pt1);
        if (ValidPoint(pt2))
            R.Add(pt2);
        if (ValidPoint(pt3))
            R.Add(pt3);
        if (ValidPoint(pt4))
            R.Add(pt4);

        // 查看是否可以喝酒
        Point PT = point;
        // X轴负向
        while (PT.y < 10)
        {
            PT.y++;
            Point cbPoint = chessboard.ToChessboardPoint(PT, campType);
            UChess chess = chessboard[cbPoint];
            if (chess != null)
            {
                if (chess.chessType == EChessType.Shuai)
                {
                    R.Add(PT);
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        return ModifyChessPoint(R);
    }
}