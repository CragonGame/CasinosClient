using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    //-------------------------------------------------------------------------
    float mShowTime = 1f;
    float mDeltaTime = 0f;
    int mFrameUpdate = 0;
    float mFPS = 0;
    float mMilliSecond = 0;

    //-------------------------------------------------------------------------
    void Update()
    {
        mFrameUpdate++;
        mDeltaTime += Time.deltaTime;
        if (mDeltaTime >= mShowTime)
        {
            mFPS = mFrameUpdate / mDeltaTime;
            mMilliSecond = mDeltaTime * 1000 / mFrameUpdate;
            mFrameUpdate = 0;
            mDeltaTime = 0f;
        }
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2, 0, 1000, 1000),
            string.Format(" 当前每帧渲染间隔：{0:0.0} ms ({1:0.} 帧每秒)", mMilliSecond, mFPS));
    }
}