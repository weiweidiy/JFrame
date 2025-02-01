//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NUnit.Framework;

namespace JFrameTest
{

    public class TestActionSM
    {
        ActionSM sm;

        [SetUp]
        public void SetUp()
        {
            sm = new ActionSM();
            sm.Initialize(Substitute.For<CombatAction>());
        }


        [TearDown]
        public void Clear()
        {
            sm = null;
        }

        [Test]
        public void TestSwitchToDisable()
        {
            //arrange
            

            //action         
            sm.SwitchToDisable();

            //expect
            Assert.IsTrue(sm.GetCurState().Name == "ActionDisableState");
        }

        [Test]
        public void TestSwitchToStandby()
        {
            //arrange


            //action         
            sm.SwitchToDisable();
            sm.SwitchToStandby();

            //expect
            Assert.IsTrue(sm.GetCurState().Name == "ActionStandbyState");
        }

        [Test]
        public void TestSwitchToExecuting()
        {
            //arrange


            //action         
            sm.SwitchToDisable();
            sm.SwitchToStandby();
            sm.SwitchToExecuting();

            //expect
            Assert.IsTrue(sm.GetCurState().Name == "ActionExecutingState");
        }

        [Test]
        public void TestSwitchToCding()
        {
            //arrange


            //action         
            sm.SwitchToDisable();
            sm.SwitchToStandby();
            sm.SwitchToExecuting();
            sm.SwitchToCding();

            //expect
            Assert.IsTrue(sm.GetCurState().Name == "ActionCdingState");
        }
    }


}
