using System;
using JFramework.Common.Interface;

namespace JFramework.Common
{
    public abstract class JFrameBaseSocket : IJFrameworkSocket
    {
        public event Action<IJFrameworkSocket> onOpen;
        public event Action<IJFrameworkSocket, string> onError;
        public event Action<IJFrameworkSocket, byte[]> onBinary;
        public event Action<IJFrameworkSocket, string> onMessage;
        public event Action<IJFrameworkSocket, SocketStatusCodes, string> onClosed;

        public abstract bool IsOpen { get; }
        public abstract void Open();

        public abstract void Close();
        public abstract void Init(string url);
        public abstract void Send(byte[] data);
    }
}
