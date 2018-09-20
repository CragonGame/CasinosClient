// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EvSceneEntityEnterRegion : EntityEvent
    {
        public EvSceneEntityEnterRegion() : base() { }
        public Entity et;
    }

    public class EvSceneEntityLeaveRegion : EntityEvent
    {
        public EvSceneEntityLeaveRegion() : base() { }
        public Entity et;
    }

    public class EvSceneEntityMoveOnRegion : EntityEvent
    {
        public EvSceneEntityMoveOnRegion() : base() { }
        public Entity et;
    }
}
