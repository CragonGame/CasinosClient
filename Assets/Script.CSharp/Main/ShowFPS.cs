using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    //-------------------------------------------------------------------------
    float FPS { get; set; } = 0;
    float MS { get; set; } = 0;

    private GUIStyle FontStyle1;
    private Rect Rect;
    private int count = 0;
    private float deltaTime = 0f;

    //-------------------------------------------------------------------------
    private void Start()
    {
        Rect = new Rect(Screen.width / 2, 0, 200, 50);

        FontStyle1 = new GUIStyle();
        FontStyle1.fontSize = 22;
        FontStyle1.fontStyle = FontStyle.Normal;
        FontStyle1.alignment = TextAnchor.MiddleLeft;
        FontStyle1.normal.textColor = Color.white;
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
        GUI.Label(Rect, info, FontStyle1);
    }
}