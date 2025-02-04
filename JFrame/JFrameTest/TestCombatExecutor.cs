//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace JFrameTest
{
    public class TestCombatExecutor
    {
        CombatUnit targetUnit;
        CombatAttributeLong hpAttr;
        CombatAttributeManger attributeManager;
        CombatExtraData extraData;

        [SetUp]
        public void SetUp()
        {
            
            targetUnit = Substitute.For<CombatUnit>();
            hpAttr = new CombatAttributeLong(PVPAttribute.HP.ToString(), 100, 100);
            attributeManager = new CombatAttributeManger();
            attributeManager.Add(hpAttr);
            targetUnit.GetAttributeManager().Returns(attributeManager);
            extraData = new CombatExtraData();
            extraData.Targets = new List<ICombatUnit>();
            extraData.Targets.Add(targetUnit);
        }


        [TearDown]
        public void Clear()
        {

        }

        [Test]
        public void TestExecutorDamageAndMinusHp()
        {
            //arrange
            var executor = new ExecutorCombatDamage(null);
            executor.Initialize(null, new float[] {1f, 2f });
            extraData.Value = 10;
            var frame = new BattleFrame();

            //act
            executor.Execute(extraData);
            executor.Update(frame);

            //expect
            Assert.AreEqual(80, hpAttr.CurValue);
        }

        [Test]
        public void TestExecutorContinuousDamageAndMinusHp()
        {
            //arrange
            var executor = new ExecutorCombatContinuousDamage(null);
            executor.Initialize(null, new float[] {1f, 1f , 0.25f, 3 });
            extraData.Value = 10;
            var frame = new BattleFrame();
            //act
            executor.Execute(extraData);
            executor.Update(frame);
            executor.Update(frame);
            executor.Update(frame);
            executor.Update(frame);

            //expect
            Assert.AreEqual(70, hpAttr.CurValue);
        }
    }


}
