// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;

    public class MbLaunchRestart : MonoBehaviour
    {
        //---------------------------------------------------------------------
        bool RunOnce { get; set; }
        //---------------------------------------------------------------------
        void Start()
        {
            RunOnce = false;
        }

        //---------------------------------------------------------------------
        void Update()
        {
            if (!RunOnce)
            {
                RunOnce = true;

                Destroy(gameObject.GetComponent<MbLaunchRestart>());

                var mb_launch = gameObject.GetComponent<MbLaunch>();
                mb_launch.Restart();
            }
        }
    }
}
