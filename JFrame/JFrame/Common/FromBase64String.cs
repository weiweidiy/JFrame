using System;
using System.Collections.Generic;
using System.Text;
using JFrame.Common.Interface;

namespace JFrame.Common
{
    public class FromBase64String : IProcesser
    {
        Encoding _encoding;
        public FromBase64String(Encoding encoding = null)
        {
            _encoding = encoding ?? Encoding.UTF8;
        }

        public byte[] Process(byte[] bytes)
        {
            string base64String = _encoding.GetString(bytes);
            return Convert.FromBase64String(base64String);
        }
    }
}
