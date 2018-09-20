// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;

    public abstract class IProp : IDisposable
    {
        //-----------------------------------------------------------------------------
        public delegate void _funcPropChanged(IProp prop, object param);
        protected PropDef mPropDef = null;

        //-----------------------------------------------------------------------------
        public _funcPropChanged OnChanged { get; set; }

        //-----------------------------------------------------------------------------
        public IProp(PropDef prop_def)
        {
            mPropDef = prop_def;
        }

        //-----------------------------------------------------------------------------
        ~IProp()
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
            return mPropDef.getKey();
        }

        //-----------------------------------------------------------------------------
        public Type getValueType()
        {
            return mPropDef.getValueType();
        }

        //-----------------------------------------------------------------------------
        public PropDef getPropertyDef()
        {
            return mPropDef;
        }

        //-----------------------------------------------------------------------------
        public abstract object getValue();

        //-----------------------------------------------------------------------------
        public abstract void setValue(object value);

        //-----------------------------------------------------------------------------
        public abstract IProp clone();

        //-----------------------------------------------------------------------------
        public abstract void copyValueFrom(IProp other_prop);

        //-----------------------------------------------------------------------------
        public abstract void fromJsonString(string json_str);

        //-----------------------------------------------------------------------------
        public abstract string toJsonString();

        //-----------------------------------------------------------------------------
        public static void setProp<T>(Dictionary<string, IProp> map_prop, string key, T value, bool collect_dirty = true)
        {
            IProp p = null;
            map_prop.TryGetValue(key, out p);
            if (p == null)
            {
                PropDef prop_def = new PropDef(key, typeof(T), collect_dirty);
                Prop<T> prop = new Prop<T>(null, prop_def, value);
                map_prop[prop.getKey()] = prop;
            }
            else p.setValue(value);
        }
    };

    public class Prop<T> : IProp
    {
        //-----------------------------------------------------------------------------
        protected T mValue;
        private ComponentDef mComponentDef;

        //-----------------------------------------------------------------------------
        public Prop(ComponentDef component_def, PropDef prop_def, T default_value)
            : base(prop_def)
        {
            mComponentDef = component_def;
            mValue = default_value;
        }

        //-----------------------------------------------------------------------------
        public T get()
        {
            return mValue;
        }

        //-----------------------------------------------------------------------------
        public override object getValue()
        {
            return mValue;
        }

        //-----------------------------------------------------------------------------
        public override void setValue(object value)
        {
            mValue = (T)value;

            if (mComponentDef != null && mPropDef.CollectDirty)
            {
                mComponentDef._tryAddDirtyProp(this);
            }

            if (OnChanged != null)
            {
                OnChanged(this, null);
            }
        }

        //-----------------------------------------------------------------------------
        public void set(T value)
        {
            mValue = value;

            if (mComponentDef != null && mPropDef.CollectDirty)
            {
                mComponentDef._tryAddDirtyProp(this);
            }

            if (OnChanged != null)
            {
                OnChanged(this, null);
            }
        }

        //-----------------------------------------------------------------------------
        public void set(T value, object param)
        {
            mValue = value;

            if (mComponentDef != null && mPropDef.CollectDirty)
            {
                mComponentDef._tryAddDirtyProp(this);
            }

            if (OnChanged != null)
            {
                OnChanged(this, param);
            }
        }

        //-----------------------------------------------------------------------------
        public override IProp clone()
        {
            IProp prop = new Prop<T>(mComponentDef, getPropertyDef(), mValue);
            return prop;
        }

        //-----------------------------------------------------------------------------
        public override void copyValueFrom(IProp other_prop)
        {
            T value = (T)other_prop.getValue();
            set(value);
        }

        //-----------------------------------------------------------------------------
        public override void fromJsonString(string json_str)
        {
            mValue = EbTool.jsonDeserialize<T>(json_str);

            if (mComponentDef != null && mPropDef.CollectDirty)
            {
                mComponentDef._tryAddDirtyProp(this);
            }

            if (OnChanged != null)
            {
                OnChanged(this, null);
            }
        }

        //-----------------------------------------------------------------------------
        public override string toJsonString()
        {
            return EbTool.jsonSerialize(mValue);
        }
    }
}
