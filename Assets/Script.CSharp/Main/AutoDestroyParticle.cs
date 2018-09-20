// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class AutoDestroyParticle : MonoBehaviour
    {
        //-------------------------------------------------------------------------
        public ParticleSystem mParticleSystem;
        bool mCanCheckDestroy;
        Action DestroyCallBack;

        //-------------------------------------------------------------------------
        public void play(Action destroy_callback = null)
        {
            DestroyCallBack = destroy_callback;
            mCanCheckDestroy = true;
            mParticleSystem.Play();
        }

        //-------------------------------------------------------------------------
        void Update()
        {
            if (mCanCheckDestroy && !mParticleSystem.isPlaying)
            {
                if (DestroyCallBack != null)
                {
                    DestroyCallBack();
                }
                GameObject.Destroy(gameObject);
            }
        }
    }
}
