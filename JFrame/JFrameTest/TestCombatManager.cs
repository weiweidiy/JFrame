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
            Assert.AreEqual(1, combatManager.GetUnitCount(1));
            Assert.AreEqual(1, combatManager.GetUnitCount(2));
        }
    }


}
