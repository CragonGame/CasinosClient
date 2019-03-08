using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cs
{
    public class ViewDesktopTexas : View
    {
        //---------------------------------------------------------------------
        DesktopTexasFlow Flow { get; set; }

        //---------------------------------------------------------------------
        public override void Create()
        {
            Flow = new DesktopTexasFlow();
            Flow.Create();
        }

        //---------------------------------------------------------------------
        public override void Destory()
        {
            if (Flow != null)
            {
                Flow.Destory();
                Flow = null;
            }
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(Event ev)
        {
        }
    }
}
