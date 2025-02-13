//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NUnit.Framework;
using NSubstitute;
using System;

namespace JFrameTest
{

    public class TestAction
    {
        [SetUp]
        public void SetUp()
        {

        }


        [TearDown]
        public void Clear()
        {

        }

        [Test]
        public void TestActionSm()
        {
            //arrange
            var sm = new OldActionSM();

            //action
            sm.Initialize(Substitute.For<BaseAction>("uid", 1, ActionType.Normal, 1f, null, null,null,null, sm));
            sm.SwitchToStandby();

            //expect
            Assert.AreEqual(nameof(ActionStandby), sm.GetCurState());
        }

        [Test]
        public void TestActionDefaultState()
        {
            //arrange
            var sm = new OldActionSM();
            var action = Substitute.For<BaseAction>("uid",1, ActionType.Normal, 1f, null,null,null,null, sm);

            //action
            action.OnAttach(null);

            //expect
            Assert.AreEqual(nameof(ActionStandby), sm.GetCurState());
        }

        [Test]
        public void TestActionManager()
        {
            // arrange
            ComabtFrame frame = Substitute.For<ComabtFrame>();
            var manager = new ActionManager();
            var action1 = Substitute.For<IBattleAction>();
            action1.Id.Returns(1);
            action1.Mode.Returns(ActionMode.Active);
            manager.Add(action1);

            //action
            action1.onCanCast += Raise.Event<Action<IBattleAction>>(action1);
            //manager.Update(frame);

            //expect
            Assert.AreEqual(true, manager.IsBusy);


        }

        /// <summary>
        /// 2个技能触发，只能有1个技能释放
        /// </summary>
        [Test]
        public void TestActionManagerAndOneActionCalled()
        {
            // arrange
            ComabtFrame frame = Substitute.For<ComabtFrame>();
            var manager = new ActionManager();
            var action1 = Substitute.For<IBattleAction>();
            action1.Id.Returns(1);
            action1.Mode.Returns(ActionMode.Active);
            var action2 = Substitute.For<IBattleAction>();
            action2.Id.Returns(2);
            action2.Mode.Returns(ActionMode.Active);
            manager.Add(action1);
            manager.Add(action2);

            //action
            action1.onCanCast += Raise.Event<Action<IBattleAction>>(action1);
            action2.onCanCast += Raise.Event<Action<IBattleAction>>(action2);

            //manager.Update(frame);

            //expect
            Assert.AreEqual(true, manager.IsBusy);
            action1.Received(1).Cast();
            action2.DidNotReceive().Cast();
        }

        /// <summary>
        /// 2个技能触发，只能有1个技能释放
        /// </summary>
        [Test]
        public void TestActionManagerAndTwoeActionCalled()
        {
            // arrange
            ComabtFrame frame = Substitute.For<ComabtFrame>();
            frame.DeltaTime.Returns(2f);
            var manager = new ActionManager();
            var action1 = Substitute.For<IBattleAction>();
            action1.Id.Returns(1);
            action1.Mode.Returns(ActionMode.Active);
            action1.Cast().Returns(1f);
            var action2 = Substitute.For<IBattleAction>();
            action2.Id.Returns(2);
            action2.Mode.Returns(ActionMode.Active);
            manager.Add(action1);
            manager.Add(action2);

            //action
            action1.onCanCast += Raise.Event<Action<IBattleAction>>(action1);
            manager.Update(frame);
            action2.onCanCast += Raise.Event<Action<IBattleAction>>(action2);

            //manager.Update(frame);

            //expect
            Assert.AreEqual(true, manager.IsBusy);
            action1.Received(1).Cast();
            action2.Received(1).Cast();
        }


        /// </summary>
        [Test]
        public void TestActionSMExecutingState()
        {
            //arrange
            var action = Substitute.For<IBattleAction>();

            //action
            action.Cast();

            //expect


        }
    }


}
