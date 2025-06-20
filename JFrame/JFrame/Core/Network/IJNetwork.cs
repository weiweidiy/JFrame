using System.Net.WebSockets;
using System;
using System.Threading.Tasks;
using JFramework.Common.Interface;

namespace JFramework.Common.Interface
{
    public enum SocketStatusCodes
    {

    }
}

namespace JFramework
{
    public interface IJNetwork
    {
        event Action onOpen;
        event Action<SocketStatusCodes, string> onClose;
        event Action<string> onMessage;
        //event Action<byte[]> onBinary;
        event Action<string> onError;

        Task Connect(string url);

        void Disconnect();

        Task<TResponse> SendMessage<TResponse>(IUnique pMsg, TimeSpan? timeout = null) where TResponse : class, IUnique;

        bool IsConnecting();
    }
}