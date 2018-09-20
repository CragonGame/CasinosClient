// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using GameCloud.Unity.Common;

    public class MbCasinosFingerGestures : MonoBehaviour
    {
        //---------------------------------------------------------------------
        public delegate void FingerDownDelegate(Vector2 finger_position);
        public FingerDownDelegate onFingerDown;
        public delegate void FingerUpDelegate(Vector2 finger_position);
        public FingerUpDelegate onFingerUp;
        public delegate void FingerDragMoveDelegate(Vector2 finger_position);
        public FingerDragMoveDelegate onFingerDragMove;
        public delegate void FingerLongPressDelegate(Vector2 finger_position);
        public FingerLongPressDelegate onFingerLongPress;
        public delegate void FingerSwipeDelegate(FingerGestures.SwipeDirection direction,
            Vector2 finger_start_position, Vector2 finger_end_position);
        public FingerSwipeDelegate onFingerFingerSwipe;

        //---------------------------------------------------------------------
        void OnFingerDown(FingerDownEvent e)
        {
            if (onFingerDown != null)
            {
                onFingerDown(e.Position);
            }
        }

        //---------------------------------------------------------------------
        void OnFingerUp(FingerUpEvent e)
        {
            if (onFingerUp != null)
            {
                onFingerUp(e.Position);
            }
        }

        //---------------------------------------------------------------------
        void OnLongPress(LongPressGesture gesture)
        {
            if (onFingerLongPress != null)
            {
                onFingerLongPress(gesture.Position);
            }
        }

        //---------------------------------------------------------------------
        void OnDrag(DragGesture gesture)
        {
            if (onFingerDragMove != null)
            {
                onFingerDragMove(gesture.Position);
            }
        }

        //---------------------------------------------------------------------
        void OnSwipe(SwipeGesture gesture)
        {
            if (onFingerFingerSwipe != null)
            {
                onFingerFingerSwipe(gesture.Direction, gesture.StartPosition, gesture.StartPosition + gesture.Move);
            }
        }
    }
}
