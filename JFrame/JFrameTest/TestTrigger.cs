//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NUnit.Framework;
using static JFrame.PVPBattleManager;
using System.Collections.Generic;
using System;

namespace JFrameTest
{
    public class TestTrigger
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
        /// 测试死亡触发器
        /// </summary>
        [Test]
        public void TestDeathTrigger()
        {
            //arrange
            var trigger = new DeathTrigger(simBattle, 0);
            var unit1 = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            var action = Substitute.For<IBattleAction>();
            action.Owner.Returns(unit1);
            bool triggerOn = false;
            int count = 0;
            trigger.onTrigger += () => { triggerOn = true; count++; };

            //action
            trigger.OnAttach(action);
            unit1.OnDamage(null, null, new IntValue() { Value = 10 });
            unit1.OnDamage(null, null, new IntValue() { Value = 10 });

            //expect
            Assert.AreEqual(true, triggerOn);
            Assert.AreEqual(1, count);
        }

        /// <summary>
        /// cd 触发器
        /// </summary>
        [Test]
        public void TestCDTrigger()
        {
            //arrange
            var trigger = new CDTrigger(simBattle, 3);
            bool triggerOn = false;
            int count = 0;
            trigger.onTrigger += () => { triggerOn = true; count++; };
            frame.DeltaTime.Returns(3);

            //action
            trigger.Update(frame);
            trigger.Update(frame);

            //expect
            Assert.AreEqual(true, triggerOn);
            Assert.AreEqual(2, count);
        }

        /// <summary>
        /// 战斗开始触发器
        /// </summary>
        [Test]
        public void TestBattleStartTrigger()
        {
            //arrange
            var trigger = new BattleStartTrigger(simBattle, 3, 1);
            bool triggerOn = false;
            int count = 0;
            trigger.onTrigger += () => { triggerOn = true; count++; };
            frame.DeltaTime.Returns(2);

            //action
            trigger.Update(frame);
            trigger.Update(frame);

            //expect
            Assert.AreEqual(true, triggerOn);
            Assert.AreEqual(1, count);
        }
    }

    
}
