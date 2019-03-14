// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;

    public class MbShowFPS : MonoBehaviour
    {
        //---------------------------------------------------------------------
        float FPS { get; set; }
        float MS { get; set; }

        private GUIStyle FontStyle1;
        private Rect Rect;
        private int Count = 0;
        private float DeltaTime = 0f;

        //---------------------------------------------------------------------
        private void Start()
        {
            Rect = new Rect(Screen.width - 250, 10, 200, 40);

            FontStyle1 = new GUIStyle();
            FontStyle1.fontSize = 16;
            FontStyle1.fontStyle = FontStyle.Normal;
            FontStyle1.alignment = TextAnchor.UpperRight;
            FontStyle1.normal.textColor = Color.white;
        }

        //---------------------------------------------------------------------
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

        //---------------------------------------------------------------------
        void OnGUI()
        {
            string info = string.Format("{0:0.0}fps，{1:0.0}ms", FPS, MS);
            GUI.Label(Rect, info, FontStyle1);
        }
    }
}