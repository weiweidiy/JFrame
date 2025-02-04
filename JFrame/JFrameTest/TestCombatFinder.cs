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
            var component = Substitute.For<BaseFinder>();

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
            combatManager.Initialize(new List<CombatUnitInfo>(), new List<CombatUnitInfo>(), 90);
            combatManager.GetOppoTeamId(Arg.Any<ICombatUnit>()).Returns(1);
            combatManager.GetUnits(Arg.Any<ICombatUnit>(), Arg.Any<int>(), Arg.Any<float>()).Returns(new System.Collections.Generic.List<ICombatUnit>() { unit1, unit2 });
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
