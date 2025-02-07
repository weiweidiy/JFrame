//using JFrame.UI;
//using NUnit.Framework;
using NUnit.Framework;
using NSubstitute;
using JFrame;
using System.Collections.Generic;

namespace JFrameTest
{
    public class TestCombatFinder
    {
        [SetUp]
        public void SetUp()
        {
            //combatManager = new CombatManager();
        }


        [TearDown]
        public void Clear()
        {

        }

        [Test]
        public void TestCombatActionComponentGetNearestOppoUnit()
        {
            //arrange
            var component = Substitute.For<CombatBaseFinder>();

            var context = Substitute.For<CombatContext>();
            var combatManager = Substitute.For<CombatManager>();
            var my = Substitute.For<CombatUnit>();
            my.GetPosition().Returns(new CombatVector() { x = -1 });
            var acition = Substitute.For<CombatUnitAction>();
            acition.Owner.Returns(my);
            var unit1 = Substitute.For<CombatUnit>();
            var unit2 = Substitute.For<CombatUnit>();
            unit1.GetPosition().Returns(new CombatVector() { x = 1 });
            unit2.GetPosition().Returns(new CombatVector() { x = 2 });
            var dicTeam1 = new KeyValuePair<CombatTeamType, List<CombatUnitInfo>>(CombatTeamType.Single, new List<CombatUnitInfo>());
            var dicTeam2 = new KeyValuePair<CombatTeamType, List<CombatUnitInfo>>(CombatTeamType.Single, new List<CombatUnitInfo>());
            combatManager.Initialize(dicTeam1, dicTeam2, 90);
            combatManager.GetOppoTeamId(Arg.Any<CombatUnit>()).Returns(1);
            combatManager.GetUnits(Arg.Any<CombatUnit>(), Arg.Any<int>(), Arg.Any<float>(), Arg.Any<bool>(), Arg.Any<bool>()).Returns(new System.Collections.Generic.List<CombatUnit>() { unit1, unit2 });
            component.Owner.Returns(acition);
            context.CombatManager.Returns(combatManager);
            component.Initialize(context, new float[] { 4 });//攻擊距離

            //act
            var lst = component.GetNearestOppoUnitInRange(new CombatExtraData(), 1);

            //expect
            Assert.AreEqual(unit1, lst[0]);
        }
    }


}
