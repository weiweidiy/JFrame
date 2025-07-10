using NUnit.Framework;
using NSubstitute;
using System;

namespace JFramework.Tests
{
    [TestFixture]
    public class BaseRunableTests
    {
        // 测试用的派生类实现
        private class TestableRunable : BaseRunable
        {
            public bool OnStartTriggered { get; private set; }
            public bool OnStopTriggered { get; private set; }
            public bool OnUpdateTriggered { get; private set; }
            public RunableExtraData ReceivedUpdateData { get; private set; }

            protected override void OnStart(RunableExtraData extraData)
            {
                OnStartTriggered = true;
            }

            protected override void OnStop()
            {
                OnStopTriggered = true;
            }

            protected override void OnUpdate(RunableExtraData extraData)
            {
                OnUpdateTriggered = true;
                ReceivedUpdateData = extraData;
            }
        }

        private TestableRunable testInstance;
        private RunableExtraData sampleData;

        [SetUp]
        public void Initialize()
        {
            testInstance = new TestableRunable();
            sampleData = new RunableExtraData();
        }

        [Test]
        public void Start_SetsCorrectStateAndTriggersOnStart()
        {
            /* 测试Start方法是否：
               1. 正确设置IsRunning状态
               2. 正确存储ExtraData
               3. 调用OnStart方法 */
            testInstance.Start(sampleData);

            Assert.IsTrue(testInstance.IsRunning);
            Assert.AreEqual(sampleData, testInstance.ExtraData);
            Assert.IsTrue(testInstance.OnStartTriggered);
        }

        [Test]
        public void Start_WhenRunning_ThrowsException()
        {
            /* 测试重复调用Start时是否抛出异常 */
            testInstance.Start(sampleData);
            Assert.Throws<Exception>(() => testInstance.Start(sampleData));
        }

        [Test]
        public void Stop_ChangesStateAndTriggersOnStop()
        {
            /* 测试Stop方法是否：
               1. 正确修改IsRunning状态
               2. 调用OnStop方法
               3. 当未运行时不会执行操作 */
            testInstance.Start(sampleData);
            testInstance.Stop();

            Assert.IsFalse(testInstance.IsRunning);
            Assert.IsTrue(testInstance.OnStopTriggered);
        }

        [Test]
        public void Stop_WhenNotRunning_NoActionTaken()
        {
            /* 测试未运行时调用Stop是否无副作用 */
            var initialData = testInstance.ExtraData;
            testInstance.Stop();

            Assert.IsFalse(testInstance.IsRunning);
            Assert.IsFalse(testInstance.OnStopTriggered);
            Assert.AreEqual(initialData, testInstance.ExtraData);
        }

        [Test]
        public void Stop_InvokesCompleteEvent()
        {
            /* 测试Stop是否触发onComplete事件 */
            var eventHandler = Substitute.For<Action<IRunable>>();
            testInstance.onComplete += eventHandler;

            testInstance.Start(sampleData);
            testInstance.Stop();

            eventHandler.Received(1).Invoke(testInstance);
        }

        [Test]
        public void Update_TriggersOnUpdateWithCorrectData()
        {
            /* 测试Update方法是否：
               1. 调用OnUpdate
               2. 传递正确的数据 */
            var updateData = new RunableExtraData();
            testInstance.Update(updateData);

            Assert.IsTrue(testInstance.OnUpdateTriggered);
            Assert.AreEqual(updateData, testInstance.ReceivedUpdateData);
        }
    }
}