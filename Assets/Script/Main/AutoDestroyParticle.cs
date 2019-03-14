// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using UnityEngine;

    public class AutoDestroyParticle : MonoBehaviour
    {
        //-------------------------------------------------------------------------
        public ParticleSystem mParticleSystem;
        bool CheckDestroy;
        Action DestroyCallBack;

        //-------------------------------------------------------------------------
        public void play(Action destroy_callback = null)
        {
            DestroyCallBack = destroy_callback;
            CheckDestroy = true;
            mParticleSystem.Play();
        }

        //-------------------------------------------------------------------------
        void Update()
        {
            if (CheckDestroy && !mParticleSystem.isPlaying)
            {
                CheckDestroy = false;
                if (DestroyCallBack != null) DestroyCallBack.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
