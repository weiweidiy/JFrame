using System;
using System.Collections.Generic;
using System.Text;

namespace JFrame.Core.Interface
{
    public interface IDecrypter
    {
        byte[] Decrypt(byte[] bytes);
    }
}
