using System;
using System.Collections.Generic;
using System.Text;

namespace JFrame.Advertisement
{
    /// <summary>
    /// 广告平台需要实现的接口
    /// </summary>
    public interface IAdPlatform
    {
        void Initialize(Action completed);
    }
}
