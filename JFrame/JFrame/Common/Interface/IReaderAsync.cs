using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JFrame.Common.Interface
{
    public interface IReaderAsync
    {
        Task<byte[]> ReadAsync(string location);
    }
}
