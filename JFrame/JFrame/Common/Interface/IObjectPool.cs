using System;

namespace JFrame.Common.Interface
{
    public interface IObjectPool
    {
        T Get<T>(Action<T> onGet = null);

        void Return<T>(T obj);
    }
}