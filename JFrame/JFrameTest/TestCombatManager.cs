//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;

namespace JFrameTest
{
    public class TestCombatManager
    {
        CombatManager combatManager;
 

        [SetUp]
        public void SetUp()
        {
            combatManager = new CombatManager();    
        }


        [TearDown]
        public void Clear()
        {
            
        }

        [Test]
        public void TestCombatManagerInitTeam()
        {
            //arrange
            var unitInfos = Substitute.For<List<CombatUnitInfo>>();

            //act
            combatManager.Initialize(unitInfos, null, 90);

            //expect
            Assert.AreEqual(1, combatManager.GetTeams().Count);
        }

        [Test]
        public void TestCombatManagerInitTeamAndUnit()
        {
            //arrange
            var team1Info = new List<CombatUnitInfo>();
            team1Info.Add(new CombatUnitInfo());
            var team2Info = new List<CombatUnitInfo>();
            team2Info.Add(new CombatUnitInfo());

            //act
            combatManager.Initialize(team1Info, team2Info, 90);

            //expect
            Assert.AreEqual(1, combatManager.GetUnitCount(0));
            Assert.AreEqual(1, combatManager.GetUnitCount(1));
        }

        [Test]
        public void TestCombatManagerGetUnitInRange()
        {
            //arrange 
            var lstCombat = new List<ICombatUnit>();
            var unit1 = Substitute.For<CombatUnit>();
            unit1.GetPosition().Returns(new CombatVector() { x = 1 });
            var unit2 = Substitute.For<CombatUnit>();
            unit2.GetPosition().Returns(new CombatVector() { x = 2 });
            var myUnit = Substitute.For<CombatUnit>();
            myUnit.GetPosition().Returns(new CombatVector() { x = 0 });
            lstCombat.Add(unit1);
            lstCombat.Add(unit2);
            var team1 = Substitute.For<CommonCombatTeam>();
            team1.GetUnits().Returns(lstCombat);
            combatManager.Initialize(null, null, 90);
            combatManager.AddTeam(1, team1);

            //act
            var units = combatManager.GetUnits(myUnit, 1, 2f);

            //expect
            Assert.AreEqual(2,units.Count);
        }
    }


}
