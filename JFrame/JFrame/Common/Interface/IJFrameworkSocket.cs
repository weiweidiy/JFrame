using System;

namespace JFramework.Common.Interface
{
    public interface IJFrameworkSocket
    {
        event Action<IJFrameworkSocket> onOpen;
        event Action<IJFrameworkSocket, SocketStatusCodes, string> onClosed;
        event Action<IJFrameworkSocket, string> onError;
        event Action<IJFrameworkSocket, byte[]> onBinary;
        //event Action<IJFrameworkSocket, string> onMessage;
        bool IsOpen { get; }
        void Init(string url);
        void Open();
        void Close();

        void Send(byte[] data);
    }
}
