// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using GameCloud.Unity.Common;

    public class CasinosInput
    {
        //---------------------------------------------------------------------
        public bool ActiveInput { get; set; }
        public bool MouseDown { get; private set; }
        MbCasinosFingerGestures FingerGestures { get; set; }

        //---------------------------------------------------------------------
        public CasinosInput()
        {
            MouseDown = false;
            ActiveInput = false;

            FingerGestures = GameObject.FindObjectOfType<MbCasinosFingerGestures>();

            if (FingerGestures == null)
            {
                return;
            }

            FingerGestures.onFingerDown += (Vector2 fingerPos) =>
            {
                if (ActiveInput)
                {
                    MouseDown = true;
                    Casinos.CasinosContext.Instance.Listener.OnFingerDownDelegate(fingerPos);
                }
            };

            FingerGestures.onFingerUp += (Vector2 fingerPos) =>
            {
                if (ActiveInput)
                {
                    MouseDown = false;
                    Casinos.CasinosContext.Instance.Listener.OnFingerUpDelegate(fingerPos);
                }
            };

            FingerGestures.onFingerDragMove += (Vector2 fingerPos) =>
            {
                if (ActiveInput)
                {
                    Casinos.CasinosContext.Instance.Listener.OnFingerDragMoveDelegate(fingerPos);
                }
            };

            FingerGestures.onFingerLongPress += (Vector2 fingerPos) =>
            {
                if (ActiveInput)
                {
                    Casinos.CasinosContext.Instance.Listener.OnFingerLongPressDelegate(fingerPos);
                }
            };

            FingerGestures.onFingerFingerSwipe += (FingerGestures.SwipeDirection direction,
                Vector2 finger_start_position, Vector2 finger_end_position) =>
            {
                if (ActiveInput)
                {
                    Casinos.CasinosContext.Instance.Listener.OnFingerSwipeDelegate(
                        direction, finger_start_position, finger_end_position);
                }
            };
        }
    }
}
