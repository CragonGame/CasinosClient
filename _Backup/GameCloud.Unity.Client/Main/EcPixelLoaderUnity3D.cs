//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using GameCloud.Unity.Common;

//namespace Ec
//{
//    public class EcPixelLoaderUnity3D : IEbPixelLoader
//    {
//        //-------------------------------------------------------------------------
//        Texture2D mTexture2D;

//        //-------------------------------------------------------------------------
//        public Int32 width
//        {
//            get { return (mTexture2D == null) ? 0 : mTexture2D.width; }
//        }

//        public Int32 height
//        {
//            get { return (mTexture2D == null) ? 0 : mTexture2D.height; }
//        }

//        //-------------------------------------------------------------------------
//        public bool load(string png_file_path)
//        {
//            mTexture2D = Resources.Load(png_file_path) as Texture2D;

//            return (mTexture2D == null) ? false : true;
//        }

//        //-------------------------------------------------------------------------
//        public void foreachPixel(onPixel fun)
//        {
//            if (mTexture2D == null) return;

//            for (int y = 0; y < height; ++y)
//            {
//                for (int x = 0; x < width; ++x)
//                {
//                    Color color = mTexture2D.GetPixel(x, y);
//                    fun(_isBlackColor(color), x, y);
//                }
//            }
//        }

//        //-------------------------------------------------------------------------
//        bool _isBlackColor(Color color)
//        {
//            return _isInTolerate(color.r, color.g, color.b);
//        }

//        //-------------------------------------------------------------------------
//        //png图片黑白边缘的地方黑的不够彻底，留一点容忍度
//        bool _isInTolerate(float r, float g, float b)
//        {
//            return (r < 126 / 255f && g < 126 / 255f && b < 126 / 255f);
//        }
//    }
//}
