// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Hjg.Pngcs;

    // png读取库
    // https://code.google.com/p/pngcs/
    public class EbPixelReaderPng : IEbPixelLoader
    {
        //---------------------------------------------------------------------
        PngReader mPngReader;
        const int mTolerance = 128;
        public int width
        {
            get { return mPngReader.ImgInfo.Cols; }
        }
        public int height
        {
            get { return mPngReader.ImgInfo.Rows; }
        }

        //---------------------------------------------------------------------
        public bool load(string file_name)
        {
            string path_png = file_name.ToLower();
            if (!path_png.EndsWith(".png"))
            {
                path_png += ".png";
            }

            mPngReader = FileHelper.CreatePngReader(path_png);

            return (mPngReader != null);
        }

        //---------------------------------------------------------------------
        public bool load(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            mPngReader = new PngReader(ms);

            return (mPngReader != null);
        }

        //---------------------------------------------------------------------
        public bool load(Stream stream)
        {
            mPngReader = new PngReader(stream);

            return (mPngReader != null);
        }

        //---------------------------------------------------------------------
        public void foreachPixel(onPixel fun)
        {
            for (int y = 0; y < height; ++y)
            {
                // ImageLine的Scanline出来的行数据，表示RGBA
                ImageLine pngLine = mPngReader.ReadRowInt(y);
                int[] pngLineArray = pngLine.Scanline;

                for (int x = 0; x < width; ++x)
                {
                    fun(_isBlackColor(pngLineArray, x), x, y);
                }
            }
        }

        //---------------------------------------------------------------------
        bool _isBlackColor(int[] array, int index)
        {
            int offset = index * 3;// Png24位, 如果是Png32位就是 int offset = index * 4;
            return _isInTolerate(array[offset], array[offset + 1], array[offset + 2]);
        }

        //---------------------------------------------------------------------
        // png图片黑白边缘的地方黑的不够彻底，留一点容忍度
        bool _isInTolerate(int r, int g, int b)
        {
            return (r < mTolerance && g < mTolerance && b < mTolerance);
        }
    }
}
