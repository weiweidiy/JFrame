//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System;
using static System.Collections.Specialized.BitVector32;

namespace JFrameTest
{
    public class TestCombatAction
    {
        CombatUnitAction action;
        CombatContext context;
        CombatUnit my;
        BattleFrame frame;
        [SetUp]
        public void Setup()
        {
            frame = new BattleFrame();
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
            combatManager.GetOppoTeamId(Arg.Any<ICombatUnit>()).Returns(1);
            combatManager.GetUnits(Arg.Any<ICombatUnit>(), Arg.Any<int>(), Arg.Any<float>()).Returns(new System.Collections.Generic.List<ICombatUnit>() { unit1, unit2 });
            //component.Owner.Returns(acition);
            context.CombatManager.Returns(combatManager);
        }

        [Test]
        public void TestAction()
        {
            //arrage
            action = new CombatUnitAction();
            action.OnAttach(my);
            var trigger = new TriggerTime();
            trigger.OnAttach(action);
            trigger.Initialize(context, new float[] { 0.3f });
            var sm = new ActionSM();
            sm.Initialize(action);
            action.Initialize(1, ActionType.Normal, ActionMode.Active, new List<BaseTrigger>() { trigger},new TriggerTime(), new List<BaseExecutor>(), new List<ICombatTrigger>(), sm);
            bool isOn = false;
            CombatExtraData data = null;
            int count = 0;
            action.onTriggerOn += (extraData) => { isOn = true; data = extraData; count++; };
            action.SwitchToDisable();
            action.SwitchToTrigging();

            //act
            action.Update(frame);
            action.Update(frame);
            action.Update(frame);

            //expect
            Assert.AreEqual(true, isOn);
            Assert.AreEqual(my, data.SourceUnit);
            Assert.AreEqual(2, count);
        }
    }


}
