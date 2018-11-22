using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// AI控制器（随机走法）
public class UControllerRandomAI : UController {

    //List<UChess> GetMyChessList()
    //{
    //    List<UChess> R = new List<UChess>();
    //    for (int i = 0; i < 9; i++)
    //    {
    //        for (int j = 0; j < 10; j++)
    //        {
    //            Point pt = new Point(i, j);
    //            if (UChessboard.Instance[pt] != null && UChessboard.Instance[pt].campType == MyCamp)
    //            {
    //                R.Add(UChessboard.Instance[pt]);
    //            }
    //        }
    //    }
    //    return R;
    //}

    //public override void Update()
    //{
    //    //随机一个棋子，随机一步
    //    List<UChess> MyChessList = GetMyChessList();
    //    int random_index = Random.Range(0, MyChessList.Count);
    //    //优先吃棋子
    //    for (int i = 0; i < MyChessList.Count; i++)
    //    {
    //        int index = (random_index + i) % MyChessList.Count;
    //        List<Point> EatedPos = MyChessList[index].GetEatPoints();
    //        if (EatedPos.Count > 0)
    //        {
    //            Command Cmd = new Command();
    //            Cmd.From = MyChessList[index].ToChessboardPoint();
    //            Cmd.To = UChessboard.Instance.ToChessboardPoint(EatedPos[Random.Range(0, EatedPos.Count)], MyCamp);
    //            Cmd.Camp = MyCamp;
    //            UChessboard.Instance.DoCommand(Cmd);
    //            return;
    //        }
    //    }
    //    //走棋子
    //    for (int i = 0; i < MyChessList.Count; i++)
    //    {
    //        int index = (random_index + i) % MyChessList.Count;
    //        List<Point> AvailablePoints = MyChessList[index].GetAvailablePoints();
    //        if (AvailablePoints.Count > 0)
    //        {
    //            Command Cmd = new Command();
    //            Cmd.From = MyChessList[index].ToChessboardPoint();
    //            Cmd.To = UChessboard.Instance.ToChessboardPoint(AvailablePoints[Random.Range(0, AvailablePoints.Count)], MyCamp);
    //            Cmd.Camp = MyCamp;
    //            UChessboard.Instance.DoCommand(Cmd);
    //            return;
    //        }
    //    }
    //}
}