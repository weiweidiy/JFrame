//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace JFrameTest
{
    public class TestCombatTriggers
    {
        [Test]
        public void TestTriggerRange()
        {
            //arrange
            var component = new TriggerRangeNearest();

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
            combatManager.Initialize(null, null, 90);
            combatManager.GetOppoTeamId(Arg.Any<ICombatUnit>()).Returns(1);
            combatManager.GetUnits(Arg.Any<ICombatUnit>(), Arg.Any<int>(), Arg.Any<float>()).Returns(new System.Collections.Generic.List<ICombatUnit>() { unit1, unit2 });
            //component.Owner.Returns(acition);
            context.CombatManager.Returns(combatManager);

            component.OnAttach(acition);
            component.Initialize(context, new float[] { 4, 1 });//攻擊距離

            //act   
            component.Update(null);

            //expect
            Assert.AreEqual(true, component.IsOn());
        }
    }


}
