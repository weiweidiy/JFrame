//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NUnit.Framework;

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
            var executor = Substitute.For<BaseExecutor>(new float[3] { 1, 0, 0 });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            frame.DeltaTime.Returns(1);

            //action
            executor.ReadyToExecute(null, null, target);
            executor.Update(frame); 

            //expect
            executor.Received(1).Hit(null, null, target);
        }

        /// <summary>
        /// 延迟触发
        /// </summary>
        [Test]
        public void TestBaseExecutorAndDelayHitCalled()
        {
            //arrange
            var executor = Substitute.For<BaseExecutor>(new float[3] { 1, 2, 0 }); //延迟2秒
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            frame.DeltaTime.Returns(1);

            //action
            executor.ReadyToExecute(null, null, target);
            executor.Update(frame);
            executor.Update(frame);
            executor.Update(frame);
            //expect
            executor.Received(1).Hit(null, null, target);
        }

        /// <summary>
        /// 普通伤害
        /// </summary>
        [Test]
        public void TestExecutorDamage()
        {
            //arrange
            var executor = new ExecutorDamage(new float[4] { 1, 0, 0, 1 });
            var caster = Substitute.For<IBattleUnit>();
            caster.Atk.Returns(1);
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);

            //action
            executor.Hit(caster, null, target);

            //expect
            Assert.AreEqual(9, target.HP);
        }

        /// <summary>
        /// 普通回血
        /// </summary>
        [Test]
        public void TestExecutorHeal()
        {
            //arrange
            var executor = new ExecutorHeal(new float[4] { 1, 0, 0, 1 });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);

            //action
            target.OnDamage(null, null, new IntValue() { Value = 3 });
            executor.Hit(null, null, target);

            //expect
            Assert.AreEqual(8, target.HP);
        }

        /// <summary>
        /// 目标生命百分比伤害
        /// </summary>
        [Test]
        public void TestExecutorHpDamage()
        {
            //arrange
            var executor = new ExecutorHpDamage(new float[4] { 1, 0, 0, 0.1f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 100, uid = "1" }, null, null);

            //action
            executor.Hit(null, null, target);

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
            var executor = new ExecutorMaxHpUp(new float[4] { 1, 0, 0, 0.1f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 100, uid = "1" }, null, null);

            var caster = Substitute.For<IBattleUnit>();
            caster.MaxHP.Returns(100);
            var action = Substitute.For<IBattleAction>();
            action.Owner.Returns(caster);
            executor.OnAttach(action);

            //action
            executor.Hit(caster, action, target);

            //expect
            Assert.AreEqual(110, target.MaxHP);
            Assert.AreEqual(110, target.HP);
            
        }
    }


}
