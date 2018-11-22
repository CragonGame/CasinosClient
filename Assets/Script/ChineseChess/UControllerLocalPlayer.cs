using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 本地玩家控制器
public class ULocalPlayerController : UControllerPlayer
{
    //-------------------------------------------------------------------------
    enum EOpStep
    {
        None,   // 无法操作
        Select, // 选择棋子
        Push    // 放下棋子
    }

    EOpStep MyOperateStep;// 操作阶段
    UChess selectedChess;// 选择的棋子
    ECampType ViewType;

    //-------------------------------------------------------------------------
    public override void Update()
    {
        // H键切换视图
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (ViewType == ECampType.Red) ViewType = ECampType.Black;
            else ViewType = ECampType.Red;
            Gamer.Chessboard.SetPlayerView(ViewType);
        }

        switch (MyOperateStep)
        {
            case EOpStep.Select:
                {
                    // 选择棋子
                    if (Input.GetMouseButtonDown(0))
                    {
                        RaycastHit HitInfo;
                        if (Physics.Raycast(Gamer.Chessboard.GetViewCamera().ScreenPointToRay(Input.mousePosition), out HitInfo))
                        {
                            UChess HitChess = Gamer.Chessboard[Gamer.Chessboard.WorldToPos(HitInfo.point)];
                            if (HitChess != null && HitChess.campType == MyCamp)
                            {
                                selectedChess = HitChess;
                                MyOperateStep = EOpStep.Push;
                                ShowMoveTargets();
                            }
                        }
                    }
                }
                break;
            case EOpStep.Push:
                {
                    RaycastHit HitInfo;
                    if (Physics.Raycast(Gamer.Chessboard.GetViewCamera().ScreenPointToRay(Input.mousePosition), out HitInfo))
                    {
                        if (selectedChess.gameObject != null)
                        {
                            selectedChess.gameObject.transform.position = HitInfo.point;
                        }

                        if (Input.GetMouseButtonDown(0))
                        {
                            Point NewPoint = Gamer.Chessboard.WorldToPos(HitInfo.point);
                            if (selectedChess.GetAvailablePoints().Contains(Gamer.Chessboard.ToChessPoint(NewPoint, selectedChess.campType)))
                            {
                                Command Cmd = new Command();
                                Cmd.Camp = MyCamp;
                                Cmd.From = selectedChess.ToChessboardPoint();
                                Cmd.To = NewPoint;
                                Gamer.Chessboard.DoCommand(Cmd);
                            }
                            else
                            {
                                selectedChess.ResetPosition();
                            }

                            ClearMoveTargets();

                            MyOperateStep = EOpStep.Select;
                        }
                    }
                }
                break;

            default:
                break;
        }
    }

    //-------------------------------------------------------------------------
    public override void TunStart()
    {
        MyOperateStep = EOpStep.Select;
    }

    //-------------------------------------------------------------------------
    public override void TunEnd()
    {
        MyOperateStep = EOpStep.None;
    }

    //-------------------------------------------------------------------------
    List<GameObject> Marks = new List<GameObject>();
    void ShowMoveTargets()
    {
        List<Point> Targets = selectedChess.GetAvailablePoints();
        for (int i = 0; i < Targets.Count; i++)
        {
            Marks.Add(GameObject.Instantiate(Gamer.Chessboard.MarkPrefab,
                Gamer.Chessboard.PosToWorld(Gamer.Chessboard.ToChessboardPoint(Targets[i], selectedChess.campType)),
                Gamer.Chessboard.MarkPrefab.transform.rotation) as GameObject);
        }
    }

    //-------------------------------------------------------------------------
    void ClearMoveTargets()
    {
        for (int i = 0; i < Marks.Count; i++)
        {
            GameObject.Destroy(Marks[i]);
        }
        Marks.Clear();
    }

    //-------------------------------------------------------------------------
    public override void DebugDraw()
    {
        //GUILayout.Label(string.Format("MyOperateStep {0}", MyOperateStep));
    }
}