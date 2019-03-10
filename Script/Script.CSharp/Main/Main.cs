// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System.Collections;
    using System.Collections.Generic;

    public class Main
    {
        //-------------------------------------------------------------------------
        static Context Context { get; set; }

        //-------------------------------------------------------------------------
        public static void Create(string platform, bool is_editor, bool is_editor_debug)
        {
            Context = new Context();
            Context.Create(platform, is_editor, is_editor_debug);
        }

        //-------------------------------------------------------------------------
        public static void Destroy()
        {
            Context.Destroy();
        }

        //-------------------------------------------------------------------------
        public static void Update(float elapsed_tm)
        {
            Context.Update(elapsed_tm);
        }
    }
}
