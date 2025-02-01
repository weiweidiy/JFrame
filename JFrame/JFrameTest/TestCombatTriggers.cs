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
        public void TestTriggerNone()
        {
            //arrange
            var finders = new List<ICombatFinder>();
            var finder = Substitute.For<ICombatFinder>();
            finders.Add(finder);
            finder.FindTargets(Arg.Any<CombatExtraData>()).Returns(new List<ICombatUnit>() { Substitute.For<ICombatUnit>()});
            var trigger = new TriggerNone(finders);
            var triggerOn = false;
            trigger.onTriggerOn += (extraData) => { triggerOn = true; };

            //act   
            trigger.Update(null);

            //expect
            Assert.AreEqual(true, triggerOn);
        }
    }


}
