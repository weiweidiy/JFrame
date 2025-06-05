using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JFramework.Common.Interface;

namespace JFramework.Common
{
    public class JFrameNetwork<T> : IJFrameNetwork where T : IJFrameworkSocket, new()
    {
        public event Action onOpen;
        public event Action<SocketStatusCodes, string> onClose;
        public event Action<string> onMessage;
        public event Action<string> onError;


        JFrameProcesserManager msgEncode = null;
        JFrameProcesserManager msgDecode = null;

        Dictionary<string, TaskCompletionSource<string>> pendingResponses = new Dictionary<string, TaskCompletionSource<string>>();


        ISerializer serializer;

        T socket;

        ///// <summary>
        ///// 检查间隔
        ///// </summary>
        //const int CHECK_INTERVAL = 100;
        /// <summary>
        /// 检查最大次数
        /// </summary>
        const int CHECK_MAX_COUNT = 3000;

        const string CONNECT_UID = "connect";

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="serializer">序列化器：用作对象转json</param>
        public JFrameNetwork(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        /// <summary>
        /// 发起连接，RPC调用风格，直接等待响应
        /// </summary>
        /// <param name="socketName"></param>
        /// <param name="url"></param>
        /// <param name="msgEncode"></param>
        /// <param name="msgDecode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Connect(string url)
        {

            var tcs = new TaskCompletionSource<bool>();
            //创建并初始化
            socket = new T();
            socket.Init(url);


            //监听事件
            socket.onOpen += (s) => { Socket_OnOpen(s, tcs); };
            socket.onClosed += (s, code, message) => { Socket_OnClose(s, code, message); };
            socket.onBinary += (s, data) => { Socket_OnBinary(s, data); };
            //socket.onMessage += (s, message) => { Socket_OnMessage(s, message); };
            socket.onError += (s, message) => { Scoket_OnError(s, message, tcs); };

            //链接
            socket.Open();

            //等待链接成功：等待tcs.setresult(true)
            await tcs.Task;
        }

        protected string CreateUid(string forceUid = null)
        {
            if (forceUid == null)
                return Guid.NewGuid().ToString();
            else
                return forceUid;
        }

        public void Disconnect()
        {
            if (socket == null)
                throw new Exception("socket is null, disconnect fail");

            if (!socket.IsOpen)
                throw new Exception("socket is closed, disconnect fail");

            socket.Close();
        }

        public bool IsConnecting()
        {
            if (socket == null)
                return false;

            return socket.IsOpen;
        }

        public async Task<TResponse> SendMessage<TResponse>(JFrameworkNetMessage pMsg, TimeSpan? timeout = null) where TResponse : JFrameworkNetMessage
        {
            if (socket == null || !socket.IsOpen)    
                throw new Exception("链接已断开，无法发送消息 socket");
            
            
            var tcs = new TaskCompletionSource<string>();
            pendingResponses.Add(pMsg.Uid, tcs); // 存储 TaskCompletionSource

            //转json->byte[]
            var json = serializer.ToJson(pMsg);
            var byteMsg = Encoding.UTF8.GetBytes(json);

            //可能加密
            if (msgEncode != null)
                byteMsg = msgEncode.GetResult(byteMsg);

            //发送
            socket.Send(byteMsg);

            var cts = new CancellationTokenSource(timeout ?? TimeSpan.FromSeconds(10));
            cts.Token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);

            try
            {
                var resJson = await tcs.Task; // 等待直到 OnWebSocketMessage 调用 TrySetResult
                return serializer.ToObject<TResponse>(resJson);
            }
            finally
            {
                pendingResponses.Remove(pMsg.Uid); // 清理
                cts.Dispose();
            }

        }


        #region 响应事件
        private void Scoket_OnError(IJFrameworkSocket s, string message, TaskCompletionSource<bool> tcs)
        {
            tcs.SetException(new Exception(message));

            onError?.Invoke(message);
        }

        //private void Socket_OnMessage(IJFrameworkSocket s, string message)
        //{
        //    onMessage?.Invoke(message);
        //}

        private void Socket_OnBinary(IJFrameworkSocket s, byte[] data)
        {
            //数据加工
            if (msgDecode != null)
                data = msgDecode.GetResult(data);

            var msg = Encoding.UTF8.GetString(data);
            var obj = serializer.ToObject<JFrameworkNetMessage>(msg);

            try
            {
                //如果没有tcs，那可能是一个推送消息
                if (pendingResponses.TryGetValue(obj.Uid, out var tcs))
                {
                    tcs.TrySetResult(msg); // 完成等待的任务
                }
            }
            catch (Exception ex)
            {
                // 处理解析错误
                Console.WriteLine($"Error parsing message: {ex.Message}");
            }

            onMessage?.Invoke(msg);
        }

        private void Socket_OnClose(IJFrameworkSocket s, SocketStatusCodes code, string message)
        {
            onClose?.Invoke(code, message);
        }

        private void Socket_OnOpen(IJFrameworkSocket webSocket, TaskCompletionSource<bool> tcs)
        {
            tcs.SetResult(true);

            onOpen?.Invoke();
        }
        #endregion
    }
}
