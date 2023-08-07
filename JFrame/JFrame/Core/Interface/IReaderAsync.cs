using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JFrame.Core.Interface
{
    public interface IReaderAsync
    {
        Task<byte[]> ReadAsync(string location);
    }
}
