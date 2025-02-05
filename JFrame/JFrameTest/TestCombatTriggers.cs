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
        CombatUnitAction action;
        CombatContext context;

        CombatUnit my;

        [SetUp]
        public void Setup()
        {
            context = Substitute.For<CombatContext>();
            var combatManager = Substitute.For<CombatManager>();
            my = Substitute.For<CombatUnit>();
            my.GetPosition().Returns(new CombatVector() { x = -1 });
            action = Substitute.For<CombatUnitAction>();
            action.Owner.Returns(my);
            var unit1 = Substitute.For<CombatUnit>();
            var unit2 = Substitute.For<CombatUnit>();
            unit1.GetPosition().Returns(new CombatVector() { x = 1 });
            unit2.GetPosition().Returns(new CombatVector() { x = 2 });
            combatManager.Initialize(new List<CombatUnitInfo>(), new List<CombatUnitInfo>(), 90);
            combatManager.GetOppoTeamId(Arg.Any<CombatUnit>()).Returns(1);
            combatManager.GetUnits(Arg.Any<CombatUnit   >(), Arg.Any<int>(), Arg.Any<float>()).Returns(new System.Collections.Generic.List<CombatUnit>() { unit1, unit2 });
            //component.Owner.Returns(acition);
            context.CombatManager.Returns(combatManager);
        }

        [Test]
        public void TestTriggerRange()
        {
            //arrange
            var component = new TriggerRangeNearest();
            component.ExtraData = new CombatExtraData() { SourceUnit = my,  Action = action };
            component.OnAttach(action);
            component.Initialize(context, new float[] { 4, 1 });//攻擊距離

            //act   
            component.Update(null);

            //expect
            Assert.AreEqual(true, component.IsOn());
        }

        [Test]  
        public void TestTriggerTime()
        {
            //arrange
            var component = new TriggerTime();
            component.ExtraData = new CombatExtraData() { SourceUnit = my, Action = action };
            component.OnAttach(action);
            component.Initialize(context, new float[] { 0.5f});//攻擊距離
            var frame = new BattleFrame();
            //act   
            component.Update(frame);
            component.Update(frame);

            //expect
            Assert.AreEqual(true, component.IsOn());
            Assert.AreEqual(action, component.ExtraData.Action);
        }
    }


}
