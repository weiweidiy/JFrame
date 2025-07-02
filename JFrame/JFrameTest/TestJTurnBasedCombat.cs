using JFrame.Game;
using JFramework;
using JFramework.Game;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFrameTest
{
    public class TestJTurnBasedCombat
    {
        public class FakeAttrFacotry : IJCombatUnitAttrFactory
        {
            public List<IUnique> Create()
            {
                var result = new List<IUnique>();

                var hp = new GameAttributeInt("Hp", 100, 100);
                result.Add(hp);

                return result;
            }
        }

        public class FakeAttrNameQuery : IJCombatAttrNameQuery
        {
            public string GetHpAttrName()
            {
                return "Hp";
            }
        }

        public class FakeEventRecorder : IJCombatEventRecorder
        {
            public List<IJCombatEvent> GetAllCombatEvents()
            {
                var result = new List<IJCombatEvent>();

                return result;
            }
        }

        public class FakeJCombatResult : IJCombatResult
        {
            public void SetCombatEvents(List<IJCombatEvent> events)
            {
                //throw new NotImplementedException();
            }

            public void SetCombatWinner(IJCombatTeam team)
            {
                //throw new NotImplementedException();
            }
        }

        [Test]
        public async Task TestInitialize()
        {
            //arrange
            Func<IJCombatUnit, string> funcUnit = (unit) => unit.Uid;
            Func<IUnique, string> funcAttr = (attr) => attr.Uid;
            Func<IJCombatTeam, string> funcTeam = (team) => team.Uid;

            var actionSelector = new JCombatSpeedBasedActionSelector(funcUnit);
            var frameRecorder = new JCombatFrameRecorder(20);
            var attrFactory = new FakeAttrFacotry();

            //队伍1
            var unit1 = new JCombatUnit(attrFactory.Create(), funcAttr, new FakeAttrNameQuery());
            var lst1 = new List<IJCombatUnit>();
            lst1.Add(unit1);
            var team1 = new JCombatTeam("team1", lst1, funcUnit);
            //队伍2
            var unit2 = new JCombatUnit(attrFactory.Create(), funcAttr, new FakeAttrNameQuery());
            var lst2 = new List<IJCombatUnit>();
            lst2.Add(unit2);
            var team2 = new JCombatTeam("team2", lst2, funcUnit);

            var lstTeams = new List<IJCombatTeam>();
            lstTeams.Add(team1); 
            lstTeams.Add(team2);


            var jcombatQuery = new JCombatQuery(lstTeams, funcTeam, frameRecorder);

            var turnbasedCombat = new JTurnBasedCombat(actionSelector, frameRecorder, jcombatQuery, new FakeEventRecorder(), new FakeJCombatResult());

            //act
            var result =  await turnbasedCombat.GetResult();

            //expect
            Assert.AreEqual(20, frameRecorder.GetCurFrame());

        }
    }
}
