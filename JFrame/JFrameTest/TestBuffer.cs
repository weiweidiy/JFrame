//using JFrame.UI;
//using NUnit.Framework;
using NUnit.Framework;
using JFrame;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace JFrameTest
{
    public class TestBuffer
    {
        //[Test]
        //public void TestBufferFactory()
        //{
        //    //arrange
        //    BufferAttackDown a = new BufferAttackDown(Guid.NewGuid().ToString(), 999, 1, new float[2] { 5f, 1 });
        //    var atype = a.GetType();

        //    var type = Type.GetType("JFrame.BufferAttackDown");
        //    object[] args = new object[4] { Guid.NewGuid().ToString(), 999, 1, new float[2] { 5f, 1} };

        //    //action
        //    var buff = (JFrame.Buffer)Activator.CreateInstance(type, args);

        //    //expect
        //    Assert.NotNull(type);
        //    Assert.NotNull(buff);
        //}

        IBattleTrigger trigger;
        IBattleTargetFinder finder;
        List<IBattleExecutor> executors;

        [SetUp]
        public void SetUp()
        {
            trigger = Substitute.For<IBattleTrigger>();
            finder = Substitute.For<IBattleTargetFinder>();
            executors = Substitute.For<List<IBattleExecutor>>();
        }

        //[Test]
        //public void TestBufferAttackDown()
        //{
        //    //arrange
        //    var buffer = new DeBufferAttackDown(null,false,"1", 1, 1, new float[2] { 10, 0.2f }, trigger, finder, executors);
        //    var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 10, uid = "1" }, null, null);

        //    //action
        //    buffer.OnAttach(target);

        //    //expect
        //    Assert.AreEqual(80, target.Atk);
        //}

        //[Test]
        //public void TestDettachBufferAttackDown()
        //{
        //    //arrange
        //    var buffer = new DeBufferAttackDown(null, false, "1", 1, 1, new float[2] { 10, 20f }, trigger, finder, executors);
        //    var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 10, uid = "1" }, null, null);

        //    //action
        //    buffer.OnAttach(target);
        //    buffer.OnDettach();

        //    //expect
        //    Assert.AreEqual(100, target.Atk);
        //}

        //[Test]
        //public void TestBufferAttackDownFold()
        //{
        //    //arrange
        //    var buffer = new DeBufferAttackDown(null, false, "1", 2, 2, new float[2] { 10, 0.2f }, trigger, finder, executors);
        //    var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 10, uid = "1" }, null, null);

        //    //action
        //    buffer.OnAttach(target);

        //    //expect
        //    Assert.AreEqual(60, target.Atk);
        //}

        [Test]
        public void TestBufferAttackSpeedUp()
        {
            //arrange
            var value = 5;
            var buffer = new BufferAttackSpeedUp(null,true,1, "1", 101, 1, new float[2] { 10, 1f }, trigger, finder, executors);
            var target = Substitute.For<IBattleUnit>();          
            var action = Substitute.For<IBattleAction>();
            var cdTrigger = new CDTimeTrigger(null, new float[] { 10 });
            action.Type.Returns(ActionType.Normal);
            action.GetCDTrigger().Returns(cdTrigger);
            target.GetActions().Returns(new IBattleAction[] { action });


            //action
            buffer.OnAttach(target);

            //expect
            Assert.AreEqual(value ,  cdTrigger.GetArgs()[0]);
        }

        [Test]
        public void TestDeBufferAttackSpeedDown()
        {
            //arrange
            var value = 15;
            var buffer = new DebufferAttackSpeedDown(null,false, 1, "1", 101, 1, new float[2] { 10, 0.5f }, trigger, finder, executors);
            var target = Substitute.For<IBattleUnit>();
            var action = Substitute.For<IBattleAction>();
            var cdTrigger = new CDTimeTrigger(null, new float[] { 10 });
            action.Type.Returns(ActionType.Normal);
            action.GetCDTrigger().Returns(cdTrigger);
            target.GetActions().Returns(new IBattleAction[] { action });


            //action
            buffer.OnAttach(target);

            //expect
            Assert.AreEqual(value, cdTrigger.GetArgs()[0]);
        }

        //[Test]
        //public void TestBufferDebuffAntiUpgrade()
        //{
        //    //arrange
        //    var buffer = new BufferDebuffAntiUpgrade(null, false, "1", 1, 1, new float[2] { 10, 0.2f }, trigger, finder, executors);
        //    var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 10, uid = "1" }, null, null);

        //    //action
        //    buffer.OnAttach(target);

        //    //expect
        //    Assert.AreEqual(0.2f, target.ControlResistance);
        //}

        //[Test]
        //public void TestBufferShield()
        //{
        //    //arrange
        //    var buffer = new BufferShield(null,true, "1", 1, 1, new float[2] { 10, 2 }, trigger, finder, executors);
        //    var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 100, maxHp = 100, uid = "1" }, null, null);

        //    //action
        //    buffer.OnAttach(target);
        //    target.OnDamage(null, null,new ExecuteInfo() { Value = 10}); 
        //    target.OnDamage(null, null, new ExecuteInfo() { Value = 10 });
        //    target.OnDamage(null, null, new ExecuteInfo() { Value = 10 });

        //    //expect
        //    Assert.AreEqual(90, target.HP);
        //}

        //[Test]
        //public void TestBufferStunning()
        //{
        //    //arrange
        //    var buffer = new DeBufferStunning(null, false, "1", 1, 1, new float[1] { 10 }, trigger, finder, executors);
        //    var actionManager = Substitute.For<ActionManager>();
        //    var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 100, uid = "1" }, actionManager, null);
        //    var action = Substitute.For<IBattleAction>();
        //    action.IsExecuting().Returns(true);
        //    var actions = new List<IBattleAction>() { action };
        //    actionManager.GetActionsByType(Arg.Any<ActionType>()).Returns(actions);

        //    //action
        //    buffer.OnAttach(target);


        //    //expect
        //    action.Received(1).Interrupt();
        //}

        //[Test]
        //public void TestBufferSkillDmgUp()
        //{
        //    //arrange
        //    var buffer = new BufferSkillDmgUp(null,true,"1", 1, 1, new float[2] { 10, 2 }, trigger, finder, executors);
        //    var target = Substitute.For< IBattleUnit>();
        //    var info = new ExecuteInfo() { Value = 10 };
        //    var action = Substitute.For<IBattleAction>();
        //    action.Type.Returns(ActionType.Skill);

        //    //action
        //    buffer.OnAttach(target);
        //    target.onHittingTarget += Raise.Event<Action<IBattleUnit, IBattleAction, IBattleUnit, ExecuteInfo>>(null, action, null, info);

        //    //expect
        //    Assert.AreEqual(30, info.Value);
        //}

        //[Test]
        //public void TestBufferCriUp()
        //{
        //    //arrange
        //    var buffer = new BufferCriUp(null, true,"1", 1, 1, new float[2] { 10, 0.1f }, trigger, finder, executors);
        //    var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 100, uid = "1", cri = 0.5f }, null, null);

        //    //action
        //    buffer.OnAttach(target);


        //    //expect
        //    Assert.AreEqual(0.6f, target.Critical);
        //}

        //[Test]
        //public void TestBufferLightningFlag()
        //{
        //    //arrange
        //    var value = 5f;
            
        //    var target = Substitute.For<IBattleUnit>();
        //    var action = Substitute.For<IBattleAction>();
        //    var cdTrigger = new CDTimeTrigger(null, new float[] { 10 });
        //    action.Type.Returns(ActionType.Normal);
        //    action.GetCDTrigger().Returns(cdTrigger);
        //    target.GetActions().Returns(new IBattleAction[] { action });
        //    var buffer = new BufferLightningFlag(null,true, "1", 101, 1, new float[2] { 10, 1f }, trigger, finder, executors);

        //    //action
        //    buffer.OnAttach(target);
        //    target.onActionCast += Raise.Event<Action<IBattleUnit, IBattleAction, List<IBattleUnit>, float>>(null, null, null, 1f);
        //    target.onActionCast += Raise.Event<Action<IBattleUnit, IBattleAction, List<IBattleUnit>, float>>(null, null, null, 1f);


        //    //expect
        //    Assert.AreEqual(value, cdTrigger.GetArgs()[0]);
        //}
    }


}

//action1.onCanCast += Raise.Event<Action<IBattleAction>>(action1);