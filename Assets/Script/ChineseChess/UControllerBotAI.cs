using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AI控制器 神经网络算法
/// </summary>
public class UControllerBotAI : UController
{
    //-------------------------------------------------------------------------
    public UNeuronNet_Controller Net;
    public double Fitness;// 这个AI的适应性分数(开始都是10分)

    //-------------------------------------------------------------------------
    public UControllerBotAI()
    {
        Fitness = UGameEngine.start_fitness_score;

        Net = new UNeuronNet_Controller();
        UNeuronNet.ConfigData Config = new UNeuronNet.ConfigData();
        Config.NumInputs = 32 * 3;// 棋子数*3
        Config.NumHiddenLayer = 2;// 1层隐藏层
        Config.NumNeuronPerHiddenLayer = 32;// 每层神经元
        Config.NumOutputs = 1;// 1个输出
        Net.Init(Config);
    }

    //-------------------------------------------------------------------------
    List<UChess> GetAllChessList()
    {
        List<UChess> R = new List<UChess>();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Point pt = new Point(i, j);
                if (Gamer.Chessboard[pt] != null)
                {
                    R.Add(Gamer.Chessboard[pt]);
                }
            }
        }
        return R;
    }

    //-------------------------------------------------------------------------
    public override void Update()
    {
        Command Cmd = Net.Update(GetAllChessList().ToArray(), MyCamp);
        if (Cmd != null)
        {
            Gamer.Chessboard.DoCommand(Cmd);

            // 增加适应性分数
            if (Cmd.EatedHistory != null)
            {
                double changed = 0;
                switch (Cmd.EatedHistory.chessType)
                {
                    case EChessType.Bing:
                        changed += 20;
                        break;
                    case EChessType.Ju:
                        changed += 50;
                        break;
                    case EChessType.Ma:
                        changed += 30;
                        break;
                    case EChessType.Pao:
                        changed += 30;
                        break;
                    case EChessType.Shi:
                        changed += 30;
                        break;
                    case EChessType.Xiang:
                        changed += 30;
                        break;
                    case EChessType.Shuai:
                        changed += 100;
                        break;
                    default:
                        changed -= 1;
                        break;
                }

                Fitness += changed;

                Fitness = Mathf.Max((float)Fitness, 0);
            }
        }
    }
}