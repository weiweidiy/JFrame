using System;
using System.Collections.Generic;
using System.Text;

namespace JFrame.Core.Interface
{
    public interface ICompress
    {
        byte[] Compress(byte[] bytes);
    }
}
