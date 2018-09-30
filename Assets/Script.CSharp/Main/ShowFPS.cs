using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    //-------------------------------------------------------------------------
    float FPS { get; set; } = 0;
    float MS { get; set; } = 0;

    private Rect rect;
    private int count = 0;
    private float deltaTime = 0f;

    //-------------------------------------------------------------------------
    private void Start()
    {
        rect = new Rect(Screen.width / 2, 0, 200, 50);
    }

    //-------------------------------------------------------------------------
    void Update()
    {
        count++;
        deltaTime += Time.unscaledDeltaTime;
        if (deltaTime >= 1f)
        {
            FPS = count / deltaTime;
            MS = deltaTime * 1000 / count;
            count = 0;
            deltaTime = 0f;
        }
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        string info = string.Format("{0:0.0}fps，{1:0.0}ms", FPS, MS);
        GUI.Label(rect, info);
    }
}