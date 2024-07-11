using System;

namespace JFrame
{

    public abstract class AdPlatform : IAdPlatform
    {
        public abstract void Initialize(Action completed);
    }
}
