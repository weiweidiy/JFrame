using JFramework.Common.Interface;
using JFramework.Common;
using JFramework;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFrameTest
{
    public class TestJNetworkInterfaces
    {

        internal class TestableNetwork : JNetwork
        {
            ISerializer serializer;
            public TestableNetwork(IJSocket socket, JTaskCompletionSourceManager<IUnique> taskManager, ISerializer serializer)
                : base(socket, taskManager) { this.serializer = serializer; }

            public override ISerializer GetSerializer() => serializer;

            // 暴露需要测试的protected方法
            public new void Socket_OnBinary(IJSocket s, byte[] data) => base.Socket_OnBinary(s, data);
        }

        /// <summary>
        /// 纯接口测试
        /// </summary>
        [Test]
        public void IJSocket_Should_Raise_Events_In_Correct_Order()
        {
            var socket = Substitute.For<IJSocket>();
            var log = new List<string>();

            socket.onOpen += _ => log.Add("open");
            socket.onClosed += (s, c, d) => log.Add("closed");

            socket.onOpen += Raise.Event<Action<IJSocket>>(socket);
            socket.onClosed += Raise.Event<Action<IJSocket, SocketStatusCodes, string>>(
                socket, SocketStatusCodes.NormalClosure, "test");

            Assert.AreEqual(new[] { "open", "closed" }, log);
        }


        [Test]
        public async Task JNetwork_Should_Handle_Socket_Events_Correctly()
        {
            // Arrange
            var socket = Substitute.For<IJSocket>();
            var serializer = Substitute.For<ISerializer>();
            var taskManager = new JTaskCompletionSourceManager<IUnique>();
            var network = new TestableNetwork(socket, taskManager, serializer);

            // 关键点1：设置socket状态
            socket.IsOpen.Returns(true);

            // 关键点2：初始化事件监听
            var connectTcs = new TaskCompletionSource<bool>();
            network.InitSocket("ws://test", connectTcs); // 必须调用初始化

            var openEventRaised = false;
            network.onOpen += () => openEventRaised = true;

            // Act
            // 正确触发方式：通过socket.Open()触发
            socket.When(x => x.Open()).Do(_ => {
                socket.onOpen += Raise.Event<Action<IJSocket>>(socket);
            });

            await network.Connect("ws://test");

            // Assert
            Assert.IsTrue(openEventRaised, "onOpen事件应该被触发");
            Assert.IsTrue(network.IsConnecting(), "连接状态应该为true");
            Assert.IsTrue(connectTcs.Task.IsCompleted, "连接任务应该完成");
        }

        [Test]
        public async Task Connect_Should_Complete_When_Socket_Opens()
        {
            // Arrange
            var socket = Substitute.For<IJSocket>();
            var serializer = Substitute.For<ISerializer>();
            var taskManager = new JTaskCompletionSourceManager<IUnique>();
            var network = new TestableNetwork(socket, taskManager, serializer);

            // 模拟Socket行为
            socket.When(s => s.Open())
                .Do(_ => socket.onOpen += Raise.Event<Action<IJSocket>>(socket));

            // Act & Assert
            Assert.DoesNotThrowAsync(() => network.Connect("ws://test"));
        }

        [Test]
        public async Task SendMessage_Should_Complete_When_Response_Received()
        {
            // Arrange
            var socket = Substitute.For<IJSocket>();
            socket.IsOpen.Returns(true);

            var serializer = Substitute.For<ISerializer>();
            var taskManager = new JTaskCompletionSourceManager<IUnique>();
            var network = new TestableNetwork(socket, taskManager, serializer);

            var testMsg = Substitute.For<IUnique>();
            testMsg.Uid.Returns("test-123");

            // 设置序列化行为
            serializer.ToJson(testMsg).Returns("{}");
            var responseObj = Substitute.For<IUnique>();
            responseObj.Uid.Returns("test-123");
            serializer.ToObject<IUnique>("{\"Uid\":\"test-123\"}").Returns(responseObj);

            // 使用TaskCompletionSource控制流程
            var responseSignal = new TaskCompletionSource<bool>();

            // 模拟socket响应
            socket.When(x => x.Send(Arg.Any<byte[]>()))
                .Do(_ =>
                {
                    // 在后台线程触发响应
                    Task.Run(() =>
                    {
                        var responseData = Encoding.UTF8.GetBytes("{\"Uid\":\"test-123\"}");
                        network.Socket_OnBinary(socket, responseData);
                        responseSignal.SetResult(true);
                    });
                });

            // Act
            var sendTask = network.SendMessage<IUnique>(testMsg);
            await Task.WhenAny(sendTask, responseSignal.Task); // 等待响应完成

            // Assert
            Assert.IsTrue(sendTask.IsCompleted);
            Assert.AreEqual("test-123", sendTask.Result.Uid);
            socket.Received(1).Send(Arg.Any<byte[]>());

        }
    }
}
