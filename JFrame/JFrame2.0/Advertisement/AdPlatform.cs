using System;

namespace JFrame.Advertisement
{

    public abstract class AdPlatform : IAdPlatform
    {
        public abstract void Initialize(Action completed);
    }
}
