//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace JFrameTest
{
    public class TestNormalExecutor : ExecutorNormal
    {
        public TestNormalExecutor(FormulaManager formulaManager, float[] args) : base(formulaManager, args)
        {
        }

        public override void Hit(IBattleUnit caster, IBattleAction action, List<IBattleUnit> target, object[] args = null)
        {
            
        }
    }
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
        //[Test]
        //public void TestBaseExecutorAndHitCalled()
        //{
        //    //arrange
        //    var executor = Substitute.For<TestNormalExecutor>(new FormulaManager(), new float[3] { 1, 0, 0 });
        //    var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
        //    var targets = new List<IBattleUnit>() { target };
        //    frame.DeltaTime.Returns(1);

        //    //action
        //    executor.ReadyToExecute(null, null, targets);
        //    executor.Update(frame); 

        //    //expect
        //    executor.Received(1).Hit(null, null, targets,null);
        //}

        ///// <summary>
        ///// 延迟触发
        ///// </summary>
        //[Test]
        //public void TestBaseExecutorAndDelayHitCalled()
        //{
        //    //arrange
        //    var executor = Substitute.For<NormalExecutor>(new FormulaManager(), new float[3] { 1, 2, 0 }); //延迟2秒
        //    var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
        //    var targets = new List<IBattleUnit>() { target };
        //    frame.DeltaTime.Returns(1);

        //    //action
        //    executor.ReadyToExecute(null, null, targets);
        //    executor.Update(frame);
        //    executor.Update(frame);
        //    executor.Update(frame);
        //    //expect
        //    executor.Received(1).Hit(null, null, targets);
        //}

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
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, maxHp = 10, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var action = Substitute.For<IBattleAction, IAttachOwner>();
            action.Type.Returns(ActionType.Normal);


            //action
            executor.OnAttach(action as IAttachOwner);
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
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, maxHp = 20, uid = "1" }, null, null);
            var target2 = new BattleUnit(new BattleUnitInfo() { atk = 2, hp = 20, maxHp = 20, uid = "2" }, null, null);
            var targets = new List<IBattleUnit>() { target , target2 };
            var action = Substitute.For<IBattleAction, IAttachOwner>();
            action.Type.Returns(ActionType.Normal);

            //action
            executor.OnAttach(action as IAttachOwner);
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
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, maxHp = 10, uid = "1" }, null, null);
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
            var executor = new ExecutorHpDamage(new FormulaManager(), new float[6] { 1, 0, 0, 0.1f , 3000, 0.1f});
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 100, maxHp = 100, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var action = Substitute.For<IBattleAction, IAttachOwner>();
            action.Type.Returns(ActionType.Normal);
            var caster = Substitute.For<IBattleUnit>();
            caster.MaxHP.Returns(200);

            //action
            executor.OnAttach(action as IAttachOwner);
            executor.Hit(caster, action, targets);

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
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 100, maxHp = 100, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var caster = Substitute.For<IBattleUnit>();
            caster.MaxHP.Returns(100);
            var action = Substitute.For<IBattleAction, IAttachOwner>();
            action.Owner.Returns(caster);
            executor.OnAttach(action as IAttachOwner);

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
            var action = Substitute.For<IBattleAction, IAttachOwner>();
            action.Owner.Returns(caster);

            //action
            executor.OnAttach(action as IAttachOwner);
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
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, maxHp = 10, uid = "1" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var action = Substitute.For<IBattleAction, IAttachOwner>();
            action.Owner.Returns(caster);


            //action
            executor.OnAttach(action as IAttachOwner);
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
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, maxHp = 10, uid = "1" }, null, null);
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
            var target = new BattleUnit(new BattleUnitInfo() { atk = 30, hp = 100, maxHp = 100, uid = "1" }, null, null);
            var caster = new BattleUnit(new BattleUnitInfo() { atk = 20, hp = 100, maxHp = 100, uid = "2" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var action = Substitute.For<IBattleAction, IAttachOwner>();
            action.Type.Returns(ActionType.Normal);

            //action
            caster.OnDamage(target, null, new ExecuteInfo() { Value = 50 });
            executor.OnAttach(action as IAttachOwner);
            executor.Hit(caster, action, targets);

            //expect
            Assert.AreEqual(70, caster.HP);
        }

        [Test]
        public void TestExecutorAttrChanged()
        {
            //arrange
            var executor = new ExecutorChangeAttr(new FormulaManager(), new float[5] { 1,0,0,1000, 1});
            var target = new BattleUnit(new BattleUnitInfo() { atk = 30, hp = 100, uid = "1" }, null, null);
            var owner = Substitute.For<IAttachOwner>();
            owner.GetFoldCount().Returns(1);

            //action
            executor.OnAttach(owner);
            executor.Hit(null,null, new List<IBattleUnit>() { target});

            //expect
            Assert.AreEqual(60, target.Atk);
        }

        [Test]
        public void TestExecutorAttrDettach()
        {
            //arrange
            var executor = new ExecutorChangeAttr(new FormulaManager(), new float[5] { 1, 0, 0, 1000, 1 });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 30, hp = 100, uid = "1" }, null, null);
            var owner = Substitute.For<IAttachOwner>();
            owner.GetFoldCount().Returns(1);

            //action
            executor.OnAttach(owner);
            executor.Hit(null, null, new List<IBattleUnit>() { target });
            executor.OnDetach();

            //expect
            Assert.AreEqual(30, target.Atk);
        }

        [Test]
        public void TestExecutorDamageUp()
        {
            //arrange
            var executor = new ExecutorDamageUp(new FormulaManager(), new float[4] { 1, 0, 0, 0.5f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 30, hp = 100, uid = "1" }, null, null);
            var owner = Substitute.For<IBattleAction, IAttachOwner>();
            owner.GetFoldCount().Returns(1);
            ExecuteInfo info = new ExecuteInfo() { Value = 100 };

            //action
            executor.OnAttach(owner);
            executor.Hit(null, null, new List<IBattleUnit>() { target }, new object[] { owner as IBattleAction, target,  info });


            //expect
            Assert.AreEqual(150, info.Value);
        }


        [Test]
        public void TestExecutorDamageCounter()
        {
            //arrange
            var executor = new ExecutorDamageCounter(new FormulaManager(), new float[4] { 1, 0, 0, 0.5f});
            var target = new BattleUnit(new BattleUnitInfo() { atk = 30, hp = 100, maxHp = 100, uid = "1" }, null, null);
            var caster = new BattleUnit(new BattleUnitInfo() { atk = 20, hp = 100, maxHp = 100, uid = "2" }, null, null);
            var targets = new List<IBattleUnit>() { target };
            var action = Substitute.For<IBattleAction, IAttachOwner>();
            action.Type.Returns(ActionType.Normal);

            //action
            var info = new ExecuteInfo() { Value = 50 , Source = target };
            caster.OnDamage(target, null, info);
            executor.OnAttach(action as IAttachOwner);
            executor.Hit(caster, action, targets, new object[] {null,null, info });

            //expect
            Assert.AreEqual(75, target.HP);
            Assert.AreEqual(50, caster.HP);
        }

        [Test]
        public void TestExecutorRandomClearDebuff()
        {
            //arrange
            var executor = new ExecutorRandomClearDebuff(new FormulaManager(), new float[4] { 1, 0, 0, 2 });
            var target = Substitute.For<IBattleUnit>();
            var buffer = Substitute.For<IBuffer>();
            buffer.IsBuff().Returns(false);
            target.GetBuffers().Returns(new IBuffer[]{ buffer });

            //action
            executor.Hit(null, null, new List<IBattleUnit>() { target });

            //expect
            target.Received(1).RemoveBuffer(Arg.Any<string>());
        }
    }


}
