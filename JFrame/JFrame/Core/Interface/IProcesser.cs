﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JFrame.Core.Interface
{
    /// <summary>
    /// 数据加工器
    /// </summary>
    public interface IProcesser
    {
        byte[] Process(byte[] bytes);
    }
}