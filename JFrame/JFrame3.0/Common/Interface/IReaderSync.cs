using System;
using System.Collections.Generic;
using System.Text;

namespace HiplayEngine.Common.Interface
{
    public interface IReaderSync
    {
        byte[] Read(string location);
    }
}
