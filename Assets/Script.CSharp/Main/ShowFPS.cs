using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    //-------------------------------------------------------------------------
    float ShowTime { get; set; } = 1f;
    float DeltaTime { get; set; } = 0f;
    int FrameUpdate { get; set; } = 0;
    float FPS { get; set; } = 0;
    float MilliSecond { get; set; } = 0;

    //-------------------------------------------------------------------------
    void Update()
    {
        FrameUpdate++;
        DeltaTime += Time.deltaTime;
        if (DeltaTime >= ShowTime)
        {
            FPS = FrameUpdate / DeltaTime;
            MilliSecond = DeltaTime * 1000 / FrameUpdate;
            FrameUpdate = 0;
            DeltaTime = 0f;
        }
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2, 0, 1000, 1000),
            string.Format(" 当前每帧渲染间隔：{0:0.0}ms，({1:0.}帧每秒)", MilliSecond, FPS));
    }
}