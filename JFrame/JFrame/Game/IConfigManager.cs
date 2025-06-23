using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFramework.Game
{


    public interface IConfigTable<T> : IEnumerable<T> where T : IUnique
    {
        void Initialize(byte[] data);
    }

    public interface IConfigLoader
    {
        Task<byte[]> LoadBytesAsync(string location);
    }


}