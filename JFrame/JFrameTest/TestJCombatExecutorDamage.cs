using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using JFramework.Game;

namespace JFrameTest
{
    [TestFixture]
    public class TestJCombatExecutorDamage
    {
        [Test]
        public void TestExecute()
        {
            // Arrange
            var finder = Substitute.For<IJCombatTargetsFinder>();
            var formula = Substitute.For<IJCombatFormula>();
            var executor = new JCombatExecutorDamage(null,null,finder, formula, new float[] { });
            var mockAction = Substitute.For<IJCombatAction>();
            var mockQuery = Substitute.For<IJCombatQuery>();
            mockAction.GetCaster().Returns("casterUid");
            var caster = Substitute.For<IJCombatCasterTargetableUnit>();
            mockQuery.GetUnit("casterUid").Returns(caster);
            executor.SetOwner(mockAction);
            executor.SetQuery(mockQuery);
            var triggerArgs = Substitute.For<IJCombatTriggerArgs>();
            var target = Substitute.For<IJCombatCasterTargetableUnit>();
            var targets = new List<IJCombatCasterTargetableUnit>
            {
                target
            };
            var finderResp = Substitute.For<IJCombatExecutorExecuteArgs>();
            finderResp.TargetUnits = targets;
            finder.GetTargetsData().Returns(finderResp);

            // Act
            executor.Execute(finderResp);

            // Assert
            Assert.IsTrue(executor is JCombatExecutorDamage);
            float hitValue = 0f;
            formula.Received(1).CalcHitValue(target, ref hitValue);
            caster.Received(1).NotifyBeforeHitting(Arg.Any<IJCombatDamageData>());
            caster.Received(1).NotifyAfterHitted(Arg.Any<IJCombatDamageData>());
            target.Received(1).OnHurt(Arg.Any<IJCombatDamageData>());
            target.Received(1).NotifyBeforeHurt(Arg.Any<IJCombatDamageData>());
            target.Received(1).NotifyAfterHurt(Arg.Any<IJCombatDamageData>());
        }


    }
}