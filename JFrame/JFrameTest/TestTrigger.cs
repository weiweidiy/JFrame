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
            var trigger = new DeathTrigger(simBattle, new float[] { 0 });
            var unit1 = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10, uid = "1" }, null, null);
            var action = Substitute.For<IBattleAction>();
            action.Owner.Returns(unit1);
            //bool triggerOn = false;
            //int count = 0;
            //trigger.onTrigger += () => { triggerOn = true; count++; };

            //action
            trigger.OnAttach(action);
            unit1.OnDamage(null, null, new ExecuteInfo() { Value = 10 });
            unit1.OnDamage(null, null, new ExecuteInfo() { Value = 10 });
            trigger.Update(frame);

            //expect
            Assert.AreEqual(true, trigger.IsOn());
            //Assert.AreEqual(1, count);
        }

        /// <summary>
        /// cd 触发器
        /// </summary>
        [Test]
        public void TestCDTrigger()
        {
            //arrange
            var trigger = new CDTimeTrigger(simBattle, new float[] { 3 });
            bool triggerOn = false;
            int count = 0;
            //trigger.onTrigger += () => { triggerOn = true; count++; };
            frame.DeltaTime.Returns(3);

            //action
            trigger.Update(frame);
            trigger.Restart();
            trigger.Update(frame);

            //expect
            Assert.AreEqual(true, trigger.IsOn());
            //Assert.AreEqual(2, count); //因为只会触
        }

        /// <summary>
        /// 战斗开始触发器
        /// </summary>
        [Test]
        public void TestBattleStartTrigger()
        {
            //arrange
            var trigger = new BattleStartTrigger(simBattle, new float[] { 3 }, 1);
            //bool triggerOn = false;
            //int count = 0;
            //trigger.onTrigger += () => { triggerOn = true; count++; };
            frame.DeltaTime.Returns(2);

            //action
            trigger.Update(frame);
            trigger.Update(frame);

            //expect
            Assert.AreEqual(true, trigger.IsOn());
            //Assert.AreEqual(1, count);
        }

        [Test]
        public void TestFriendHurtTrigger()
        {
            //arrange
            var trigger = new FriendsHurtTrigger(simBattle, new float[] { 1 });
            var friend = Substitute.For<IBattleUnit>();
            friend.IsHpFull().Returns(false);
            friend.IsAlive().Returns(true);
            var friend2 = Substitute.For<IBattleUnit>();
            friend2.IsHpFull().Returns(true);
            friend2.IsAlive().Returns(true);
            simBattle.GetUnits(Arg.Any<Team>()).Returns(new List<IBattleUnit>() { friend , friend2 });
            simBattle.GetFriendTeam(Arg.Any<IBattleUnit>()).Returns(Team.Attacker);
            frame.DeltaTime.Returns(2);
            var action = Substitute.For<IBattleAction>();
            action.Owner.Returns(friend2);

            //action
            trigger.OnAttach(action);
            trigger.Update(frame);

            //expect
            Assert.AreEqual(true, trigger.IsOn());
        }
    }

    
}
