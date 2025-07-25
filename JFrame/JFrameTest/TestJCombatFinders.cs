using JFramework.Game;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JFramework.Game.Tests
{
    public class TestJCombatFinders
    {
        IJCombatAction mockAction;
        IJCombatTeam mockTeam;
        IJCombatSeatBasedQuery mockQuery;

        IJCombatCasterTargetableUnit mockUnit;
        IJCombatCasterTargetableUnit mockUnit2;
        IJCombatCasterTargetableUnit mockUnit3;
        IJCombatCasterTargetableUnit mockUnit4;
        IJCombatCasterTargetableUnit mockUnit5;
        [SetUp]
        public void SetUp()
        {
            mockAction = Substitute.For<IJCombatAction>();
            mockAction.GetCaster().Returns("caster1");

            mockTeam = Substitute.For<IJCombatTeam>();
            mockQuery = Substitute.For<IJCombatSeatBasedQuery, IJCombatQuery>();
            (mockQuery as IJCombatQuery).GetOppoTeams("caster1").Returns(new List<IJCombatTeam> { mockTeam });
            mockQuery.GetSeat("caster1").Returns(1);

            mockUnit = Substitute.For<IJCombatCasterTargetableUnit>();
            mockUnit.Uid.Returns("caster1");
            mockUnit.IsDead().Returns(false);

            mockUnit2 = Substitute.For<IJCombatCasterTargetableUnit>();
            mockUnit2.Uid.Returns("caster2");
            mockUnit2.IsDead().Returns(false);

            mockUnit3 = Substitute.For<IJCombatCasterTargetableUnit>();
            mockUnit3.Uid.Returns("caster3");
            mockUnit3.IsDead().Returns(false);

            mockUnit4 = Substitute.For<IJCombatCasterTargetableUnit>();
            mockUnit4.Uid.Returns("caster4");
            mockUnit4.IsDead().Returns(true);

            mockUnit5 = Substitute.For<IJCombatCasterTargetableUnit>();
            mockUnit5.Uid.Returns("caster5");
            mockUnit5.IsDead().Returns(false);


            mockQuery.GetUnit(mockTeam, 0).Returns(mockUnit4);
            mockQuery.GetUnit(mockTeam, 1).Returns(mockUnit4);
            mockQuery.GetUnit(mockTeam, 2).Returns(mockUnit4);
            mockQuery.GetUnit(mockTeam, 3).Returns(mockUnit4);
            mockQuery.GetUnit(mockTeam, 6).Returns(mockUnit4);
            
            mockQuery.GetUnit(mockTeam, 8).Returns(mockUnit4);

            mockQuery.GetUnit(mockTeam, 4).Returns(mockUnit2); //主目标
            mockQuery.GetUnit(mockTeam, 5).Returns(mockUnit3);
            mockQuery.GetUnit(mockTeam, 7).Returns(mockUnit5);

            mockQuery.GetSeat("caster2").Returns(4); // 主目标所在座位
            //finder.SetQuery(mockQuery as IJCombatQuery);
        }

        [Test]
        public void TestJCombatDefaultFinder()
        {
            var finder = new JCombatDefaultFinder();
            finder.SetOwner(mockAction);
            finder.SetQuery(mockQuery as IJCombatQuery);

            //act
            var result = finder.GetTargets();

            //expect
            Assert.IsNotNull(result);
            Assert.AreEqual(mockUnit2, result[0]);
        }


        [Test]
        public void TestRowFinder()
        {
            //arrange
            var finder = new JCombatRowFinder();
            finder.SetOwner(mockAction);
            finder.SetQuery(mockQuery as IJCombatQuery);
            
            //act
            var result = finder.GetTargets();

            //expect    
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(mockUnit2, result[0]);
            Assert.AreEqual(mockUnit3, result[1]);
        }

        [Test]
        public void TestColFinder()
        {
            //arrange
            var finder = new JCombatColFinder();
            finder.SetOwner(mockAction);
            finder.SetQuery(mockQuery as IJCombatQuery);

            //act
            var result = finder.GetTargets();

            //expect    
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(mockUnit2, result[0]);
            Assert.AreEqual(mockUnit5, result[1]);
        }

        [Test]
        public void TestCrossFinder()
        {
            //arrange
            var finder = new JCombatCrossFinder();
            finder.SetOwner(mockAction);
            finder.SetQuery(mockQuery as IJCombatQuery);

            //act
            var result = finder.GetTargets();

            //expect    
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(mockUnit2, result[0]);
            Assert.AreEqual(mockUnit3, result[1]);
            Assert.AreEqual(mockUnit5, result[2]);
        }

        public void TestJCombatRandomFinder()
        {
            //arrange
            var finder = new JCombatRandomFinder(2);
            finder.SetOwner(mockAction);
            finder.SetQuery(mockQuery as IJCombatQuery);
            //act
            var result = finder.GetTargets();
            //expect    
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(mockUnit2) || result.Contains(mockUnit3) || result.Contains(mockUnit5));
        }
    }
}
