using System;
using System.Collections.Generic;
using System.Text;

namespace JFrame.Common.Interface
{
    public interface IReaderSync
    {
        byte[] Read(string location);
    }
}
