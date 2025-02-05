//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NUnit.Framework;

namespace JFrameTest
{
    public class TestCombatUnit
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
        public void TestUnitMove()
        {
            //arrage
            var unit = new CombatUnit();
            unit.Initialize("uid", null, null, null,null);
            unit.SetPosition(new CombatVector() { x = 10, y = 0 });
            unit.SetSpeed(new CombatVector() { x = -1,y =0 });

            //act
            unit.StartMove();
            unit.UpdatePosition(new BattleFrame());
            unit.Update(new BattleFrame());

            //expect
            Assert.AreEqual(9, unit.GetPosition().x);
        }
    }


}
