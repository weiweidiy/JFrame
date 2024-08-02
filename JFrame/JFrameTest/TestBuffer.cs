//using JFrame.UI;
//using NUnit.Framework;
using NUnit.Framework;
using JFrame;
using NSubstitute;
using System;

namespace JFrameTest
{
    public class TestBuffer
    {
        //[Test]
        //public void TestBufferFactory()
        //{
        //    //arrange
        //    BufferAttackDown a = new BufferAttackDown(Guid.NewGuid().ToString(), 999, 1, new float[2] { 5f, 1 });
        //    var atype = a.GetType();

        //    var type = Type.GetType("JFrame.BufferAttackDown");
        //    object[] args = new object[4] { Guid.NewGuid().ToString(), 999, 1, new float[2] { 5f, 1} };

        //    //action
        //    var buff = (JFrame.Buffer)Activator.CreateInstance(type, args);

        //    //expect
        //    Assert.NotNull(type);
        //    Assert.NotNull(buff);
        //}

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

        [Test]
        public void TestBufferAttackSpeedUp()
        {
            //arrange
            var value = 5;
            var buffer = new BufferAttackSpeedUp("1", 101, 1, new float[2] { 10, 1f });
            var target = Substitute.For<IBattleUnit>();          
            var action = Substitute.For<IBattleAction>();
            var cdTrigger = new CDTimeTrigger(null, new float[] { 10 });
            action.Type.Returns(1);
            action.GetCDTrigger().Returns(cdTrigger);
            target.GetActions().Returns(new IBattleAction[] { action });


            //action
            buffer.OnAttach(target);

            //expect
            Assert.AreEqual(value ,  cdTrigger.GetArgs()[0]);
        }

        [Test]
        public void TestBufferDebuffAntiUpgrade()
        {
            //arrange
            var buffer = new BufferDebuffAntiUpgrade("1", 1, 1, new float[2] { 10, 0.2f });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 10, uid = "1" }, null, null);

            //action
            buffer.OnAttach(target);

            //expect
            Assert.AreEqual(0.2f, target.DebuffAnti);
        }

        [Test]
        public void TestBufferShield()
        {
            //arrange
            var buffer = new BufferShield("1", 1, 1, new float[2] { 10, 2 });
            var target = new BattleUnit(new BattleUnitInfo() { atk = 100, hp = 100, uid = "1" }, null, null);

            //action
            buffer.OnAttach(target);
            target.OnDamage(null, null,new ExecuteInfo() { Value = 10}); 
            target.OnDamage(null, null, new ExecuteInfo() { Value = 10 });
            target.OnDamage(null, null, new ExecuteInfo() { Value = 10 });

            //expect
            Assert.AreEqual(90, target.HP);
        }
    }


}
