using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    //-------------------------------------------------------------------------
    float FPS { get; set; } = 0;
    float MS { get; set; } = 0;

    private GUIStyle FontStyle1;
    private Rect Rect;
    private int Count = 0;
    private float DeltaTime = 0f;

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
        Count++;
        DeltaTime += Time.deltaTime;
        if (DeltaTime >= 1f)
        {
            FPS = Count / DeltaTime;
            MS = DeltaTime * 1000 / Count;
            Count = 0;
            DeltaTime = 0f;
        }
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        string info = string.Format("{0:0.0}fps，{1:0.0}ms", FPS, MS);
        GUI.Label(Rect, info, FontStyle1);
    }
}