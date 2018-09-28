//// Copyright (c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using UnityEngine;
//    using FairyGUI;
//    using XLua;
//    //using DG.Tweening;

//    [LuaCallCSharp]
//    public static class UiDoTweenHelper
//    {
//        //---------------------------------------------------------------------
//        public static Tweener TweenMove(GObject target, Vector2 startValue, Vector2 endValue, float duration, bool is_snapping)
//        {
//            return DOTween.To(() => startValue, x => target.xy = x, endValue, duration)
//                .SetOptions(is_snapping)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenMoveX(GObject target, float startValue, float endValue, float duration, bool is_snapping)
//        {
//            return DOTween.To(() => startValue, x => target.x = x, endValue, duration)
//                .SetOptions(is_snapping)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenMoveY(GObject target, float startValue, float endValue, float duration, bool is_snapping)
//        {
//            return DOTween.To(() => startValue, x => target.y = x, endValue, duration)
//                .SetOptions(is_snapping)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenScale(GObject target, Vector2 startValue, Vector2 endValue, float duration)
//        {
//            return DOTween.To(() => startValue, x => target.scale = x, endValue, duration)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenScaleX(GObject target, float startValue, float endValue, float duration)
//        {
//            return DOTween.To(() => startValue, x => target.scaleX = x, endValue, duration)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenScaleY(GObject target, float startValue, float endValue, float duration)
//        {
//            return DOTween.To(() => startValue, x => target.scaleY = x, endValue, duration)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenResize(GObject target, Vector2 startValue, Vector2 endValue, float duration, bool is_snapping)
//        {
//            return DOTween.To(() => startValue, x => target.size = x, endValue, duration)
//                .SetOptions(is_snapping)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenFade(GObject target, float startValue, float endValue, float duration)
//        {
//            return DOTween.To(() => startValue, x => target.alpha = x, endValue, duration)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenRotate(GObject target, float startValue, float endValue, float duration)
//        {
//            return DOTween.To(() => startValue, x => target.rotation = x, endValue, duration)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenRotateX(GObject target, float startValue, float endValue, float duration)
//        {
//            return DOTween.To(() => startValue, x => target.rotationX = x, endValue, duration)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }

//        //---------------------------------------------------------------------
//        public static Tweener TweenRotateY(GObject target, float startValue, float endValue, float duration)
//        {
//            return DOTween.To(() => startValue, x => target.rotationY = x, endValue, duration)
//                .SetUpdate(true)
//                .SetTarget(target);
//        }
//    }
//}