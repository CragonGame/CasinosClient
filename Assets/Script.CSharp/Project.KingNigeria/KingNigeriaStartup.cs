// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;

    public class KingNigeriaStartup : MonoBehaviour
    {
        //---------------------------------------------------------------------
        void Awake()
        {
        }

        //---------------------------------------------------------------------
        void Update()
        {
            var context = CasinosContext.Instance;
            if (context != null)
            {
                context.Update(Time.deltaTime);
            }
        }

        //---------------------------------------------------------------------
        void OnDestroy()
        {
            CasinosContext.Instance.Close();
        }

        //-------------------------------------------------------------------------
        void OnApplicationPause(bool pause)
        {
        }

        //-------------------------------------------------------------------------
        void OnApplicationFocus(bool focusStatus)
        {
        }
    }
}