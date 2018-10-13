// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IDeck
    {
        //---------------------------------------------------------------------
        void Shuffle();

        //---------------------------------------------------------------------
        Card GetNextCard();
    }
}
