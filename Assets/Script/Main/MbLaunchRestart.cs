// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;

    public class MbLaunchRestart : MonoBehaviour
    {
        //---------------------------------------------------------------------
        bool RunOnce { get; set; }
        bool CanRestart { get; set; }

        //---------------------------------------------------------------------
        void Start()
        {
            RunOnce = false;
            CanRestart = false;
        }

        //---------------------------------------------------------------------
        void Update()
        {
            if (RunOnce && !CanRestart)
            {
                CanRestart = true;

                Destroy(gameObject.GetComponent<MbLaunchRestart>());

                var mb_launch = gameObject.GetComponent<MbLaunch>();
                mb_launch.Init();
            }

            if (!RunOnce)
            {
                RunOnce = true;

                //Destroy(gameObject.GetComponent<MbLaunchRestart>());
                var mb_launch = gameObject.GetComponent<MbLaunch>();
                mb_launch.Close();
                //mb_launch.Restart();
            }
        }
    }
}
