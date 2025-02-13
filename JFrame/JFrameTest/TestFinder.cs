//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace JFrameTest
{
    public class TestFinder
    {
        ComabtFrame frame = Substitute.For<ComabtFrame>();
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
        /// 搜索敌对最大攻击力的单位
        /// </summary>
        [Test]
        public void TestOrderOppoTopAtkFinder()
        {
            //arrange
            var battlePoint = Substitute.For<BattlePoint>(1, PVPBattleManager.Team.Attacker);
            var unit1 = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 1 , maxHp = 1}, null, null);
            var unit2 = new BattleUnit(new BattleUnitInfo() { atk = 2, hp = 1 , maxHp = 1 }, null, null);
            simBattle.GetUnits(Arg.Any<PVPBattleManager.Team>()).Returns(new List<IBattleUnit>() { unit1, unit2 });
            var finder = new OrderOppoTopAtkFinder(battlePoint, simBattle, 1);

            //action
            var result = finder.FindTargets(null);

            //expect
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, result[0].Atk);

        }

        /// <summary>
        /// 测试寻找友方受伤队员
        /// </summary>
        [Test]
        public void TestOrderFriendsHurtFinder()
        {
            //arrange
            var battlePoint = Substitute.For<BattlePoint>(1, PVPBattleManager.Team.Attacker);
            var unit1 = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 10,maxHp = 10, uid = "1" }, null, null);
            var unit2 = new BattleUnit(new BattleUnitInfo() { atk = 2, hp = 10,maxHp = 10, uid = "2" }, null, null);
            unit1.OnDamage(null, null, new ExecuteInfo() { Value = 1 });
            unit2.OnDamage(null, null, new ExecuteInfo() { Value = 2 });
            simBattle.GetUnits(Arg.Any<PVPBattleManager.Team>()).Returns(new List<IBattleUnit>() { unit1, unit2 });
            var finder = new FriendsLowestHpFinder(battlePoint, simBattle, 2);

            //action
            var result = finder.FindTargets(null);

            //expect
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("2", result[0].Uid);

        }

        /// <summary>
        /// 本体，不管死活
        /// </summary>
        [Test]
        public void TestSelfFinder()
        {
            //arrange
            var battlePoint = Substitute.For<BattlePoint>(1, PVPBattleManager.Team.Attacker);
            var unit1 = new BattleUnit(new BattleUnitInfo() { atk = 1, hp = 1 }, null, null);
            //var unit2 = new BattleUnit(new BattleUnitInfo() { atk = 2, hp = 1 }, null, null);
            //simBattle.GetUnits(Arg.Any<PVPBattleManager.Team>()).Returns(new List<IBattleUnit>() { unit1, unit2 });
            var finder = new SelfFinder(battlePoint, simBattle, 1);
            var action = Substitute.For<IAttachOwner>();
            action.Owner.Returns(unit1);

            //action
            finder.OnAttach(action as IAttachOwner);
            var result = finder.FindTargets(null);

            //expect
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(unit1, result[0]);

        }
    }
}
