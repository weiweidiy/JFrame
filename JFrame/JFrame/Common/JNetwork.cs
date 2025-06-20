using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
//using System.Reactive.Linq;
//using System.Reactive.Subjects;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JFramework.Common.Interface;

namespace JFramework.Common
{

    public abstract class JNetwork : IJNetwork
    {
        /// <summary>
        /// 接口事件
        /// </summary>
        public event Action onOpen;
        public event Action<SocketStatusCodes, string> onClose;
        public event Action<string> onMessage;
        public event Action<string> onError;

        /// <summary>
        /// socket对象
        /// </summary>
        IJSocket socket;

        /// <summary>
        /// 任务管理器
        /// </summary>
        TaskManager<IUnique> taskManager = null;


        #region 公开接口
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
            InitSocket(url, tcs);
            //链接
            GetSocket().Open();

            //等待链接成功：等待tcs.setresult(true)
            await tcs.Task;
        }

        public void Disconnect()
        {
            var socket = GetSocket();
            try
            {
                if (socket == null || !socket.IsOpen)
                    return;


                socket.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsConnecting()
        {
            var socket = GetSocket();

            if (socket == null)
                return false;

            return socket.IsOpen;
        }

        /// <summary>
        /// 发送消息， RPC风格调用， 如果像protobuf这种无法实现接口的，可以自己定义个适配器，实现iunique接口即可
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="pMsg"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TResponse> SendMessage<TResponse>(IUnique pMsg, TimeSpan? timeout = null) where TResponse : class, IUnique 
        {
            var socket = GetSocket();

            if (socket == null || !socket.IsOpen)
                throw new Exception("链接已断开，无法发送消息 socket");

            //创建任务
            var tcs = AddTask(pMsg.Uid);
            if (tcs == null) 
                throw new Exception("Duplicate UID detected.");

            try
            {
                //转json->byte[]
                var byteMsg = Serlialize(pMsg);

                //数据处理（比如加密，编码等）
                byteMsg = GetDataOutProcesser() != null ? GetDataOutProcesser().GetResult(byteMsg) : byteMsg;

                //发送
                socket.Send(byteMsg);

                //等待任务完成或者超时
                var result = await WaitingTask(pMsg.Uid, timeout); //可能超时

                return result as TResponse; // 等待直到 OnWebSocketMessage 调用 TrySetResult
            }
            catch (Exception ex)
            {
                SetTaskException(pMsg.Uid, ex);
                throw ex;
            }
            finally
            {
                var result = RemoveTask(pMsg.Uid);
            }

        }



        #endregion

        #region 响应事件
        private void Scoket_OnError(IJSocket s, string message, TaskCompletionSource<bool> tcs)
        {
            tcs.SetException(new Exception(message));

            onError?.Invoke(message);
        }



        private void Socket_OnBinary(IJSocket s, byte[] data)
        {
            //数据加工           
            data = GetDataComingProcesser() != null? GetDataComingProcesser().GetResult(data) : data;

            //处理接收的数据=>反序列化等
            string msg;
            try
            {
                msg = Encoding.UTF8.GetString(data);
            }
            catch (DecoderFallbackException)
            {
                throw new Exception("Invalid UTF-8 data received.");
            }


            var obj = Deserialize(data);

            try
            {
                //如果没有tcs，那可能是一个推送消息
                var tcs = GetTask(obj.Uid);
                if (tcs != null)
                {
                    tcs.TrySetResult(obj); // 完成等待的任务
                }
            }
            catch (Exception ex)
            {
                // 处理解析错误
                Console.WriteLine($"Error parsing message: {ex.Message}");
            }

            onMessage?.Invoke(msg);

        }


        private void Socket_OnClose(IJSocket s, SocketStatusCodes code, string message)
        {
            onClose?.Invoke(code, message);
        }

        private void Socket_OnOpen(IJSocket webSocket, TaskCompletionSource<bool> tcs)
        {
            tcs.SetResult(true);

            //在完成异步之后，再进行事件通知
            onOpen?.Invoke();
        }
        #endregion

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="pMsg"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        byte[] Serlialize(IUnique pMsg)
        {
            if (GetSerializer() == null)
                throw new Exception("Serialize 为空，无法序列化");

            var json = GetSerializer().ToJson(pMsg);
            var byteMsg = Encoding.UTF8.GetBytes(json);
            return byteMsg;
        }

        //to do:
        IUnique Deserialize(byte[] data)
        {
            string msg;
            try
            {
                msg = Encoding.UTF8.GetString(data);
            }
            catch (DecoderFallbackException)
            {
                throw new Exception("Invalid UTF-8 data received.");
            }


            return GetSerializer().ToObject<JNetMessage>(msg);
        }

        /// <summary>
        /// 获取序列化工具，子类实现
        /// </summary>
        /// <returns></returns>
        public abstract ISerializer GetSerializer();

        /// <summary>
        /// 数据出去前的处理工具
        /// </summary>
        /// <returns></returns>
        public virtual JDataProcesserManager GetDataOutProcesser() => null;

        /// <summary>
        /// 数据进来时候的处理工具
        /// </summary>
        /// <returns></returns>
        public virtual JDataProcesserManager GetDataComingProcesser() => null;

        #region task管理相关
        /// <summary>
        /// 添加一个任务到缓存
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tcs"></param>
        /// <returns></returns>
        protected TaskCompletionSource<IUnique> AddTask(string uid)
        {
            return taskManager.AddTask(uid);
        }

        /// <summary>
        /// 移除一个任务
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        protected bool RemoveTask(string uid)
        {
            return taskManager.RemoveTask(uid);
        }

        /// <summary>
        /// 获取缓存中的任务
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        protected TaskCompletionSource<IUnique> GetTask(string uid)
        {
            TaskCompletionSource<IUnique> result = taskManager.GetTask(uid);
            return result;
        }

        /// <summary>
        /// 等待任务
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected Task<IUnique> WaitingTask(string uid, TimeSpan? timeout = null)
        {
            return taskManager.WaitingTask(uid, timeout);
        }

        /// <summary>
        /// 设置任务异常
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="exception"></param>
        protected void SetTaskException(string uid, Exception exception)
        {
            taskManager.SetException(uid, exception);
        }

        /// <summary>
        /// 设置任务结果
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="result"></param>
        protected void SetTaskResult(string uid, IUnique result)
        {
            taskManager.SetResult(uid, result);
        }
        #endregion


        public JNetwork(IJSocket socket, TaskManager<IUnique> taskManager)
        {
            this.socket = socket;
            this.taskManager = taskManager;
        }


        /// <summary>
        /// 创建socket
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tcs"></param>
        /// <returns></returns>
        void InitSocket(string url, TaskCompletionSource<bool> tcs)
        {
            var socket = GetSocket();
            socket.Init(url);

            //监听事件
            socket.onOpen += (s) => { Socket_OnOpen(s, tcs); };
            socket.onClosed += (s, code, message) => { Socket_OnClose(s, code, message); };
            socket.onBinary += (s, data) => { Socket_OnBinary(s, data); };
            //socket.onMessage += (s, message) => { Socket_OnMessage(s, message); };
            socket.onError += (s, message) => { Scoket_OnError(s, message, tcs); };
        }

        IJSocket GetSocket() => socket;

    }
}
