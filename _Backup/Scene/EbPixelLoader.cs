// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public delegate void onPixel(bool is_block, int x, int y);

    public interface IEbPixelLoader
    {
        //---------------------------------------------------------------------
        int width { get; }
        int height { get; }

        //---------------------------------------------------------------------
        bool load(string file_name);

        //---------------------------------------------------------------------
        bool load(byte[] data);

        //---------------------------------------------------------------------
        bool load(Stream stream);

        //---------------------------------------------------------------------
        void foreachPixel(onPixel fun);
    }
}
