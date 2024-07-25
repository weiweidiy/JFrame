//using JFrame.UI;
//using NUnit.Framework;
using NUnit.Framework;
using JFrame;
using NSubstitute;

namespace JFrameTest
{
    public class TestBuffer
    {
        [Test]
        public void TestBufferAttackDown()
        {
            //arrange
            var buffer = new BufferAttackDown("1", 1, 1, new float[2] { 10, 0.2f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 10, uid = "1" }, null, null);

            //action
            buffer.OnAttach(target);

            //expect
            Assert.AreEqual(80, target.Atk);
        }

        [Test]
        public void TestDettachBufferAttackDown()
        {
            //arrange
            var buffer = new BufferAttackDown("1", 1, 1, new float[2] { 10, 20f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 10, uid = "1" }, null, null);

            //action
            buffer.OnAttach(target);
            buffer.OnDettach();

            //expect
            Assert.AreEqual(100, target.Atk);
        }

        [Test]
        public void TestBufferAttackDownFold()
        {
            //arrange
            var buffer = new BufferAttackDown("1", 1, 2, new float[2] { 10, 0.2f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 10, uid = "1" }, null, null);

            //action
            buffer.OnAttach(target);

            //expect
            Assert.AreEqual(60, target.Atk);
        }
    }


}
