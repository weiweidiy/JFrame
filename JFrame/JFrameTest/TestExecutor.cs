//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace JFrameTest
{
    public class TestExecutor
    {
        BattleFrame frame = Substitute.For<BattleFrame>();
        IPVPBattleManager simBattle = Substitute.For<PVPBattleManager>();

        [SetUp]
        public void SetUp()
        {

        }


        [TearDown]
        public void Clear()
        {

        }

        /// <summary>
        /// 触发器基类测，启动后，指定时间后触发命中
        /// </summary>
        [Test]
        public void TestBaseExecutorAndHitCalled()
        {
            //arrange
            var executor = Substitute.For<BaseExecutor>(new FormulaManager(), new float[3] { 1, 0, 0 });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            frame.DeltaTime.Returns(1);

            //action
            executor.ReadyToExecute(null, null, targets);
            executor.Update(frame); 

            //expect
            executor.Received(1).Hit(null, null, targets);
        }

        /// <summary>
        /// 延迟触发
        /// </summary>
        [Test]
        public void TestBaseExecutorAndDelayHitCalled()
        {
            //arrange
            var executor = Substitute.For<BaseExecutor>(new FormulaManager(), new float[3] { 1, 2, 0 }); //延迟2秒
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            frame.DeltaTime.Returns(1);

            //action
            executor.ReadyToExecute(null, null, targets);
            executor.Update(frame);
            executor.Update(frame);
            executor.Update(frame);
            //expect
            executor.Received(1).Hit(null, null, targets);
        }

        /// <summary>
        /// 普通伤害
        /// </summary>
        [Test]
        public void TestExecutorDamage()
        {
            //arrange
            var executor = new ExecutorDamage(new FormulaManager(), new float[4] { 1, 0, 0, 1 });
            var caster = Substitute.For<IBattleUnit>();
            caster.Atk.Returns(1);
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var action = Substitute.For<IBattleAction>();
            action.Type.Returns(ActionType.Normal);


            //action
            executor.OnAttach(action);
            executor.Hit(caster, action, targets);

            //expect
            Assert.AreEqual(9, target.HP);
        }

        /// <summary>
        /// 普通伤害多单位
        /// </summary>
        [Test]
        public void TestExecutorDamageMultTargets()
        {
            //arrange
            var executor = new ExecutorDamage(new FormulaManager(), new float[4] { 1, 0, 0, 1 });
            var caster = Substitute.For<IBattleUnit>();
            caster.Atk.Returns(1);
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            var target2 = new BattleUnit(new BattleUnitInfo() { atk = 2, hp = 20, uid = "2" }, null, null);
            var targets = new List<IBattleUnit>() { target , target2 };
            var action = Substitute.For<IBattleAction>();
            action.Type.Returns(ActionType.Normal);

            //action
            executor.OnAttach(action);
            executor.Hit(caster, action, targets);

            //expect
            Assert.AreEqual(9, target.HP);
            Assert.AreEqual(19, target2.HP);
        }

        /// <summary>
        /// 普通回血
        /// </summary>
        [Test]
        public void TestExecutorHeal()
        {
            //arrange
            var executor = new ExecutorHeal(new FormulaManager(), new float[4] { 1, 0, 0, 0.2f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var caster = Substitute.For<IBattleUnit>();
            caster.MaxHP.Returns(20);

            //action
            target.OnDamage(null, null, new ExecuteInfo() { Value = 5 });
            executor.Hit(caster, null, targets);

            //expect
            Assert.AreEqual(9, target.HP);
        }

        /// <summary>
        /// 目标生命百分比伤害
        /// </summary>
        [Test]
        public void TestExecutorHpDamage()
        {
            //arrange
            var executor = new ExecutorHpDamage(new FormulaManager(), new float[4] { 1, 0, 0, 0.1f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 100, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var action = Substitute.For<IBattleAction>();
            action.Type.Returns(ActionType.Normal);

            //action
            executor.OnAttach(action);
            executor.Hit(Substitute.For<IBattleUnit>(), action, targets);

            //expect
            Assert.AreEqual(90, target.HP);
        }

        /// <summary>
        /// 目标生命百分比伤害
        /// </summary>
        [Test]
        public void TestExecutorMaxHpUp()
        {
            //arrange
            var executor = new ExecutorMaxHpUp(new FormulaManager(), new float[4] { 1, 0, 0, 0.1f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 100, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var caster = Substitute.For<IBattleUnit>();
            caster.MaxHP.Returns(100);
            var action = Substitute.For<IBattleAction>();
            action.Owner.Returns(caster);
            executor.OnAttach(action);

            //action
            executor.Hit(caster, action, targets);

            //expect
            Assert.AreEqual(110, target.MaxHP);
            Assert.AreEqual(110, target.HP);
            
        }

        /// <summary>
        /// 给目标添加buff
        /// </summary>
        [Test]
        public void TestExecutorTargetAddBuffer()
        {
            //arrange
            var executor = new ExecutorTargetAddBuffer(new FormulaManager(), new float[6] { 1, 0, 0, 999,1,1 });
            var bufferManager = Substitute.For<IBufferManager>();
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 100, uid = "1" }, null, bufferManager);
            var targets = new List<IBattleUnit>() { target };
            var caster = Substitute.For<IBattleUnit>();
            var action = Substitute.For<IBattleAction>();
            action.Owner.Returns(caster);

            //action
            executor.OnAttach(action);
            executor.Hit(caster, null, targets);

            //expect
            bufferManager.Received(1).AddBuffer(Arg.Any<IBattleUnit>(), target, 999, 1);
        }

        /// <summary>
        /// 给本体添加buffer 
        /// </summary>
        [Test]
        public void TestExecutorSelfAddBuffer()
        {
            //arrange
            var executor = new ExecutorSelfAddBuffer(new FormulaManager(), new float[6] { 1, 0, 0, 999, 1, 1 });
            var bufferManager = Substitute.For<IBufferManager>();
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 100, uid = "1" }, null, bufferManager);

            //action
            executor.Hit(target, null, null);

            //expect
            bufferManager.Received(1).AddBuffer(Arg.Any<IBattleUnit>(),  target, 999, 1);
        }

        /// <summary>
        /// 递增伤害
        /// </summary>
        [Test]
        public void TestExecutorIncrementalDamage()
        {
            //arrange
            var executor = new ExecutorIncrementalDamage(new FormulaManager(), new float[6] { 1, 0, 0, 1 , 3, 0.5f});
            var caster = Substitute.For<IBattleUnit>();
            caster.Atk.Returns(1);
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var action = Substitute.For<IBattleAction>();
            action.Owner.Returns(caster);


            //action
            executor.OnAttach(action);
            executor.Hit(caster, null, targets);

            //expect
            Assert.AreEqual(9, target.HP);
        }

        /// <summary>
        /// 复活
        /// </summary>
        [Test]
        public void TestExecutorReborn()
        {
            //arrange
            var executor = new ExecutorReborn(new FormulaManager(), new float[4] { 1, 0, 0, 0.5f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            //action
            target.OnDamage(null, null, new ExecuteInfo() { Value = 10 });
            executor.Hit(null, null, targets);

            //expect
            Assert.AreEqual(5, target.HP);
        }


        /// <summary>
        /// 吸血
        /// </summary>
        [Test]
        public void TestExecutorSuckHp()
        {
            //arrange
            var executor = new ExecutorSuckHp(new FormulaManager(), new float[5] { 1, 0, 0, 1f,1f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 30, hp = 100, uid = "1" }, null, null);
            var caster = new BattleUnit(new BattleUnitInfo() { atk = 20, hp = 100, uid = "2" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var action = Substitute.For<IBattleAction>();
            action.Type.Returns(ActionType.Normal);

            //action
            caster.OnDamage(target, null, new ExecuteInfo() { Value = 50 });
            executor.OnAttach(action);
            executor.Hit(caster, action, targets);

            //expect
            Assert.AreEqual(70, caster.HP);
        }
    }


}
