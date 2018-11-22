using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
    //-------------------------------------------------------------------------
    public GameObject spriteGameOver;
    public GameObject btnUndo;
    //public UILabel labelWinner;
    //public UILabel labelTime;
    //public UILabel labelTun;
    public UChessBoard Chessboard;

    //-------------------------------------------------------------------------
    void Start()
    {
        //UIEventListener.Get(spriteGameOver).onClick = OnClickGameOver;
        //UIEventListener.Get(btnUndo).onClick = OnClickUndo;
        spriteGameOver.SetActive(false);
        Chessboard.onGameOver = OnGameOver;
    }

    //-------------------------------------------------------------------------
    void OnGameOver(ECampType Winner)
    {
        if (UGamerPlayer.LocalPlayer != null)
        {
            spriteGameOver.SetActive(true);
            if (Winner == UGamerPlayer.LocalPlayer.Camp)
            {
                //labelWinner.text = "你赢了";
            }
            else
            {
                //labelWinner.text = "你输了";
            }
        }
    }

    //-------------------------------------------------------------------------
    void OnClickGameOver(GameObject go)
    {
        Chessboard.Restart();
        spriteGameOver.SetActive(false);
    }

    //-------------------------------------------------------------------------
    void OnClickUndo(GameObject go)
    {
        Chessboard.Undo();
        Chessboard.Undo();
    }

    //-------------------------------------------------------------------------
    void Update()
    {
        //labelTime.text =string.Format("{0:D2}:{1:D2}", (int)(Chessboard.game_start_timer/60), (int)(Chessboard.game_start_timer%60));
        //labelTun.text = Chessboard.NowPlayer== ECampType.Red?"[ff0000]红方":"[000000]黑方";
    }
}