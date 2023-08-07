using System;
using System.Collections.Generic;
using System.Text;

namespace JFrame.Core.Interface
{
    public interface IWriterSync
    {
        void Write(string toPath, byte[] buffer, Encoding encoding = null);

        void Write(string toPath, string buffer , Encoding encoding = null);
    }
}
