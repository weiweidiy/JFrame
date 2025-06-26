using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JFrame.Game.Tests
{
    [TestFixture]
    public class JCombatTests
    {
        // 模拟依赖项
        private IJCombatDataSource _dataSource;
        private IJCombatFrameRecorder _frameRecorder;
        private IJCombatJudger _combatJudger;
        private IJCombatEventRecorder _eventRecorder;
        private IJCombatResult _combatResult;

        // 测试用的具体实现类
        private class TestCombat : JCombat
        {
            public int UpdateCallCount { get; private set; }
            public bool StartCalled { get; private set; }
            public bool StopCalled { get; private set; }

            public TestCombat(
                IJCombatDataSource dataSource,
                IJCombatFrameRecorder frameRecorder,
                IJCombatJudger combatJudger,
                IJCombatEventRecorder eventRecorder,
                IJCombatResult combatResult)
                : base(dataSource, frameRecorder, combatJudger, eventRecorder, combatResult)
            {
            }

            protected override IJCombatResult CreateReport()
            {
                return Substitute.For<IJCombatResult>();
            }

            protected override void Update(IJCombatFrameRecorder frameRecorder)
            {
                UpdateCallCount++;
            }

            protected override void Start()
            {
                StartCalled = true;
                base.Start();
            }

            protected override void Stop()
            {
                StopCalled = true;
                base.Stop();
            }
        }

        [SetUp]
        public void Setup()
        {
            // 初始化模拟对象
            _dataSource = Substitute.For<IJCombatDataSource>();
            _frameRecorder = Substitute.For<IJCombatFrameRecorder>();
            _combatJudger = Substitute.For<IJCombatJudger>();
            _eventRecorder = Substitute.For<IJCombatEventRecorder>();
            _combatResult = Substitute.For<IJCombatResult>();

            // 设置帧记录器默认行为
            _frameRecorder.GetCurFrame().Returns(0);
            _frameRecorder.NextFrame().Returns(1, 2, 3);
        }

        [Test]
        public void Constructor_WithNullDependencies_ShouldThrowException()
        {
            // 测试各种null依赖情况
            Assert.Throws<ArgumentNullException>(() => new TestCombat(null, _frameRecorder, _combatJudger, _eventRecorder, _combatResult));
            Assert.Throws<ArgumentNullException>(() => new TestCombat(_dataSource, null, _combatJudger, _eventRecorder, _combatResult));
            Assert.Throws<ArgumentNullException>(() => new TestCombat(_dataSource, _frameRecorder, null, _eventRecorder, _combatResult));
            Assert.Throws<ArgumentNullException>(() => new TestCombat(_dataSource, _frameRecorder, _combatJudger, null, _combatResult));
            Assert.Throws<ArgumentNullException>(() => new TestCombat(_dataSource, _frameRecorder, _combatJudger, _eventRecorder, null));
        }

        [Test]
        public async Task GetResult_WhenCombatNotOver_ShouldCallUpdateMultipleTimes()
        {
            // 安排
            _combatJudger.IsCombatOver().Returns(false, false, false, true); // 前3帧未结束，第4帧结束
            var combat = new TestCombat(_dataSource, _frameRecorder, _combatJudger, _eventRecorder, _combatResult);

            // 执行
            await combat.GetResult();

            // 断言
            Assert.AreEqual(3, combat.UpdateCallCount, "Update方法应该被调用3次");
            _frameRecorder.Received(3).NextFrame();
        }

        [Test]
        public async Task GetResult_WhenCombatEndsImmediately_ShouldNotCallUpdate()
        {
            // 安排
            _combatJudger.IsCombatOver().Returns(true); // 立即结束
            var combat = new TestCombat(_dataSource, _frameRecorder, _combatJudger, _eventRecorder, _combatResult);

            // 执行
            await combat.GetResult();

            // 断言
            Assert.AreEqual(0, combat.UpdateCallCount, "Update方法不应该被调用");
            _frameRecorder.DidNotReceive().NextFrame();
        }

        [Test]
        public async Task GetResult_ShouldCallStartAndStopMethods()
        {
            // 安排
            _combatJudger.IsCombatOver().Returns(true);
            var combat = new TestCombat(_dataSource, _frameRecorder, _combatJudger, _eventRecorder, _combatResult);

            // 执行
            await combat.GetResult();

            // 断言
            Assert.IsTrue(combat.StartCalled, "Start方法应该被调用");
            Assert.IsTrue(combat.StopCalled, "Stop方法应该被调用");
        }

        [Test]
        public async Task GetResult_ShouldSetCombatResultCorrectly()
        {
            // 安排
            var winner = Substitute.For<IJCombatTeam>();
            var events = new List<IJCombatEvent> { Substitute.For<IJCombatEvent>() };

            _combatJudger.IsCombatOver().Returns(true);
            _combatJudger.GetWinner().Returns(winner);
            _eventRecorder.GetAllCombatEvents().Returns(events);

            var combat = new TestCombat(_dataSource, _frameRecorder, _combatJudger, _eventRecorder, _combatResult);

            // 执行
            await combat.GetResult();

            // 断言
            _combatResult.Received(1).SetCombatEvents(events);
            _combatResult.Received(1).SetCombatWinner(winner);
        }

        [Test]
        public async Task GetResult_ShouldResetFrameCounter()
        {
            // 安排
            _combatJudger.IsCombatOver().Returns(true);
            var combat = new TestCombat(_dataSource, _frameRecorder, _combatJudger, _eventRecorder, _combatResult);

            // 执行
            await combat.GetResult();

            // 断言
            _frameRecorder.Received(1).ResetFrame();
        }

        [Test]
        public void GetTeam_ShouldDelegateToDataSource()
        {
            // 安排
            var teamId = 1;
            var expectedTeam = Substitute.For<IJCombatTeam>();
            _dataSource.GetTeam(teamId).Returns(expectedTeam);

            var combat = new TestCombat(_dataSource, _frameRecorder, _combatJudger, _eventRecorder, _combatResult);

            // 执行
            var result = combat.GetTeam(teamId);

            // 断言
            Assert.AreEqual(expectedTeam, result);
            _dataSource.Received(1).GetTeam(teamId);
        }

        [Test]
        public async Task GetResult_ShouldRunInBackgroundThread()
        {
            // 安排
            _combatJudger.IsCombatOver().Returns(true);
            var combat = new TestCombat(_dataSource, _frameRecorder, _combatJudger, _eventRecorder, _combatResult);
            var mainThreadId = Environment.CurrentManagedThreadId;
            int? taskThreadId = null;

            // 执行
            await combat.GetResult().ContinueWith(t =>
            {
                taskThreadId = Environment.CurrentManagedThreadId;
            });

            // 断言
            Assert.AreNotEqual(mainThreadId, taskThreadId, "GetResult应该在后台线程执行");
        }
    }
}