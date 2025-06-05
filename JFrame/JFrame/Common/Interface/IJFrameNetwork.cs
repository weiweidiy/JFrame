using System.Net.WebSockets;
using System;
using System.Threading.Tasks;

namespace JFramework.Common.Interface
{
    public enum SocketStatusCodes
    {

    }

    public interface IJFrameNetwork
    {
        event Action onOpen;
        event Action<SocketStatusCodes, string> onClose;
        event Action<string> onMessage;
        //event Action<byte[]> onBinary;
        event Action<string> onError;

        Task Connect(string url);

        void Disconnect();

        Task<TResponse> SendMessage<TResponse>(JFrameworkNetMessage pMsg, TimeSpan? timeout = null) where TResponse : JFrameworkNetMessage;

        bool IsConnecting();
    }


}
