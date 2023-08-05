using System;
using System.Collections.Generic;
using System.Text;

namespace HiplayEngine.Common.Interface
{
    public interface IDecrypter
    {
        byte[] Decrypt(byte[] bytes);
    }
}
