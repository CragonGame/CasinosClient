// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    public interface IDeepCloneable<out T>
    {
        T DeepClone();
    }
}
