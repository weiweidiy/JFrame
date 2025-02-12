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
        CombatAttributeDouble hpAttr;
        CombatAttributeDouble maxHpAttr;
        CombatAttributeManger attributeManager;
        CombatExtraData extraData;
        CombatBaseFormula fakeFormula;

        [SetUp]
        public void SetUp()
        {
            
            targetUnit = Substitute.For<CombatUnit>();
            hpAttr = new CombatAttributeDouble(PVPAttribute.HP.ToString(), 90, double.MaxValue);
            maxHpAttr = new CombatAttributeDouble(PVPAttribute.MaxHP.ToString(), 100, double.MaxValue);
            attributeManager = new CombatAttributeManger();
            attributeManager.Add(hpAttr);
            attributeManager.Add(maxHpAttr);
            targetUnit.GetAttributeManager().Returns(attributeManager);
            extraData = new CombatExtraData();
            extraData.Targets = new List<CombatUnit>();
            extraData.Targets.Add(targetUnit);
            fakeFormula = Substitute.For<CombatBaseFormula>();
        }


        [TearDown]
        public void Clear()
        {

        }

        [Test]
        public void TestExecutorDamageAndMinusHp()
        {
            //arrange
            fakeFormula.GetBaseValue(Arg.Any<CombatExtraData>()).Returns(10);
            var executor = new ExecutorCombatDamage(null, fakeFormula);
            executor.Initialize(null, new float[] {1f, 2f });
            //extraData.Value = 10;
            var frame = new BattleFrame();

            //act
            executor.OnStart();
            executor.Execute(extraData);
            executor.Update(frame);

            //expect
            Assert.AreEqual(70, hpAttr.CurValue);
        }

        [Test]
        public void TestExecutorContinuousDamageAndMinusHp()
        {
            //arrange
            fakeFormula.GetBaseValue(Arg.Any<CombatExtraData>()).Returns(10);
            var executor = new ExecutorCombatContinuousDamage(null, fakeFormula);
            executor.Initialize(null, new float[] { 1f,1f , 0.25f, 3 });
            //extraData.Value = 10;
            var frame = new BattleFrame();
            //act
            executor.OnStart();
            executor.Execute(extraData);
            executor.Update(frame);
            executor.Update(frame);
            executor.Update(frame);
            executor.Update(frame);

            //expect
            Assert.AreEqual(60, hpAttr.CurValue);
        }

        [Test]
        public void TestExecutorHealAndPlusHp()
        {
            //arrange
            fakeFormula.GetBaseValue(Arg.Any<CombatExtraData>()).Returns(10);
            var executor = new ExecutorCombatHeal(null, fakeFormula);
            executor.Initialize(null, new float[] { 1f, 2f });
            //extraData.Value = 10;
            var frame = new BattleFrame();
            //act
            executor.OnStart();
            executor.Execute(extraData);
            executor.Update(frame);
            executor.Update(frame);
            executor.Update(frame);
            executor.Update(frame);

            //expect
            Assert.AreEqual(100, hpAttr.CurValue);
        }

        [Test]
        public void TestExecutorAttr()
        {
            //arrange
            var executor = new ExecutorAttribute

            //act

            //expect

        }
    }


}
