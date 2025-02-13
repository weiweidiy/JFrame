//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System;

namespace JFrameTest
{
    public class TestCombatExecutor
    {
        CombatUnit unit1;
        CombatAttributeDouble hpAttr;
        CombatAttributeDouble maxHpAttr;
        CombatAttributeDouble atkAttr;
        CombatAttributeManger attributeManager;
        CombatExtraData extraData;
        CombatBaseFormula fakeFormula;

        [SetUp]
        public void SetUp()
        {
            
            unit1 = Substitute.For<CombatUnit>();
            hpAttr = new CombatAttributeDouble(CombatAttribute.CurHp.ToString(), 90, double.MaxValue);
            maxHpAttr = new CombatAttributeDouble(CombatAttribute.MaxHP.ToString(), 100, double.MaxValue);
            atkAttr = new CombatAttributeDouble(CombatAttribute.ATK.ToString(), 10, double.MaxValue);
            attributeManager = new CombatAttributeManger();
            attributeManager.Add(hpAttr);
            attributeManager.Add(maxHpAttr);
            //attributeManager.Add(atkAttr);
            unit1.GetAttributeManager().Returns(attributeManager);
            extraData = new CombatExtraData();
            extraData.Targets = new List<CombatUnit>();
            extraData.Targets.Add(unit1);
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
            var frame = new ComabtFrame();

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
            var frame = new ComabtFrame();
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
            var frame = new ComabtFrame();
            
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
            extraData.Action = new CombatAction();
            extraData.Action.Initialize(1, "action", ActionType.All, ActionMode.Active, null, null, null, null, null);
            //hpAttr = new CombatAttributeDouble(PVPAttribute.HP.ToString(), 90, double.MaxValue);
            unit1.GetAttribute(CombatAttribute.MaxHP).Returns(hpAttr);
            var executor = new ExecutorCombatChangeAttribute(null,null);
            executor.Initialize(null, new float[] {0.1f, 102, 0.1f });

            //act
            executor.OnStart();
            executor.Execute(extraData);
            executor.Update(null);

            //expect
            unit1.Received(1).AddExtraValue(CombatAttribute.MaxHP, "action", Arg.Any<double>());
        }

        [Test]
        public void TestExecutorAttrRemove()
        {
            //arrange
            extraData.Action = new CombatAction();
            extraData.Action.Initialize(1, "action", ActionType.All, ActionMode.Active, null, null, null, null, null);

            //hpAttr = new CombatAttributeDouble(PVPAttribute.HP.ToString(), 90, double.MaxValue);
            unit1.GetAttribute(CombatAttribute.MaxHP).Returns(hpAttr);
            var executor = new ExecutorCombatChangeAttribute(null, null);
            executor.Initialize(null, new float[] { 0.1f, 102, 0.1f });

            //act
            executor.OnStart();
            executor.Execute(extraData);
            executor.Update(null);
            executor.OnStop();
            //expect
            unit1.Received(1).AddExtraValue(CombatAttribute.MaxHP, "action", Arg.Any<double>());
            unit1.Received(1).RemoveExtraValue(CombatAttribute.MaxHP, "action");
            Assert.AreEqual(90, hpAttr.CurValue);
        }
    }


}
