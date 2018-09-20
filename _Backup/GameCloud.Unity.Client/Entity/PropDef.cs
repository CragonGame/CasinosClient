// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PropDef : IDisposable
    {
        //-----------------------------------------------------------------------------
        public bool CollectDirty { get; private set; }
        private string mPropKey;
        private Type mValueType;

        //-----------------------------------------------------------------------------
        public PropDef(string key, Type value_type, bool collect_dirty = true)
        {
            mPropKey = key;
            mValueType = value_type;
            CollectDirty = collect_dirty;
        }

        //-----------------------------------------------------------------------------
        ~PropDef()
        {
            this.Dispose(false);
        }

        //-----------------------------------------------------------------------------
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        //-----------------------------------------------------------------------------
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        //-----------------------------------------------------------------------------
        public string getKey()
        {
            return mPropKey;
        }

        //-----------------------------------------------------------------------------
        public Type getValueType()
        {
            return mValueType;
        }
    }
}
