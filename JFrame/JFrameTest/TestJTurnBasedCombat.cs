using JFramework.Game;
using JFramework;
using JFramework.Game;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NUnit.Framework.Constraints.Tolerance;

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
                var speed = new GameAttributeInt("Speed", 50, 50);
                result.Add(hp);
                result.Add(speed);

                return result;
            }
        }

        public class FakeAttrFacotry2 : IJCombatUnitAttrFactory
        {
            public List<IUnique> Create()
            {
                var result = new List<IUnique>();

                var hp = new GameAttributeInt("Hp", 200, 200);
                var speed = new GameAttributeInt("Speed", 40, 60);
                result.Add(hp);
                result.Add(speed);

                return result;
            }
        }

        public class FakeAttrNameQuery : IJCombatTurnBasedAttrNameQuery
        {
            public string GetActionPointName()
            {
                return "Speed";
            }

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

        public class FakeJCombatAction : JCombatActionBase
        {
            public FakeJCombatAction(IJCombatQuery query, string uid, List<IJCombatExecutor> executors) : base(query, uid, null, executors)
            {
            }
        }

        [Test]
        public async Task TestDefaultFinder()
        {
            //arrange
            Func<IJCombatUnit, string> funcUnit = (unit) => unit.Uid;
            Func<IUnique, string> funcAttr = (attr) => attr.Uid;
            Func<IJCombatTeam, string> funcTeam = (team) => team.Uid;
            Func<string, int> funcSeat = (unitUid) => 1;

            var actionSelector = new JCombatTurnBasedActionSelector(funcUnit);
            var frameRecorder = new JCombatTurnBasedFrameRecorder(19); //从0开始，共20回合
            var attrFactory = new FakeAttrFacotry();
            var attrFactory2 = new FakeAttrFacotry2();

            var jcombatQuery = new JCombatSeatBasedQuery(funcSeat, /*lstTeams,*/ funcTeam, funcUnit, frameRecorder);

            //执行器
            var finder1 = new JCombatDefaultFinder(jcombatQuery);
            var executor1 = new JCombatExecutorDamage(jcombatQuery, finder1);
            var lstExecutor1 = new List<IJCombatExecutor>();
            lstExecutor1.Add(executor1);

            //队伍1
            var unit1 = new JCombatTurnBasedUnit("unit1", attrFactory.Create(), funcAttr, new FakeAttrNameQuery(), jcombatQuery, new List<IJCombatAction>() { new FakeJCombatAction(jcombatQuery, "action1", lstExecutor1) });
            var lst1 = new List<IJCombatUnit>();
            lst1.Add(unit1);
            var team1 = new JCombatTeam("team1", lst1, funcUnit);


            //var finder1 = new JCombatDefaultFinder();
            //var executor1 = new JCombatExecutorDamage(finder1);
            //var lstExecutor1 = new List<IJCombatExecutor>();
            //lstExecutor1.Add(executor1);

            //队伍2
            var unit2 = new JCombatTurnBasedUnit("unit2", attrFactory2.Create(), funcAttr, new FakeAttrNameQuery(), jcombatQuery, new List<IJCombatAction>() { new FakeJCombatAction(jcombatQuery,"action2", null) });
            var lst2 = new List<IJCombatUnit>();
            lst2.Add(unit2);
            var team2 = new JCombatTeam("team2", lst2, funcUnit);

            var lstTeams = new List<IJCombatTeam>();
            lstTeams.Add(team1);
            lstTeams.Add(team2);


            jcombatQuery.AddRange(lstTeams);

             var turnbasedCombat = new JTurnBasedCombat(actionSelector, frameRecorder, jcombatQuery, new JCombatRunner(jcombatQuery, new FakeEventRecorder(), new FakeJCombatResult()));

            //act
            var result = await turnbasedCombat.GetResult();

            //expect
            Assert.AreEqual(9, frameRecorder.GetCurFrame());
            var hpAttr1 = jcombatQuery.GetUnit("unit1").GetAttribute("Hp") as GameAttributeInt;
            var hpAttr2 = jcombatQuery.GetUnit("unit2").GetAttribute("Hp") as GameAttributeInt;
            Assert.AreEqual(100, hpAttr1.CurValue);
            Assert.AreEqual(0, hpAttr2.CurValue);
            Assert.AreEqual(team1, jcombatQuery.GetWinner());
        }


        [Test]
        public async Task TestInitialize()
        {
            //arrange
            Func<IJCombatUnit, string> funcUnit = (unit) => unit.Uid;
            Func<IUnique, string> funcAttr = (attr) => attr.Uid;
            Func<IJCombatTeam, string> funcTeam = (team) => team.Uid;

            var actionSelector = new JCombatTurnBasedActionSelector(funcUnit);
            var frameRecorder = new JCombatTurnBasedFrameRecorder(19);
            var attrFactory = new FakeAttrFacotry();
            var attrFactory2 = new FakeAttrFacotry2();

            var jcombatQuery = new JCombatQuery(/*lstTeams, */funcTeam, frameRecorder);

            //执行器
            var finder1 = new JCombatDefaultFinder(jcombatQuery);
            var executor1 = new JCombatExecutorDamage(jcombatQuery, finder1);
            var lstExecutor1 = new List<IJCombatExecutor>();
            lstExecutor1.Add(executor1);

            //队伍1
            var unit1 = new JCombatTurnBasedUnit("unit1", attrFactory.Create(), funcAttr, new FakeAttrNameQuery(), jcombatQuery, new List<IJCombatAction>() { new FakeJCombatAction(jcombatQuery, "action1", lstExecutor1) });
            var lst1 = new List<IJCombatUnit>();
            lst1.Add(unit1);
            var team1 = new JCombatTeam("team1", lst1, funcUnit);


            //var finder1 = new JCombatDefaultFinder();
            //var executor1 = new JCombatExecutorDamage(finder1);
            //var lstExecutor1 = new List<IJCombatExecutor>();
            //lstExecutor1.Add(executor1);

            //队伍2
            var unit2 = new JCombatTurnBasedUnit("unit2", attrFactory2.Create(), funcAttr, new FakeAttrNameQuery(), jcombatQuery, new List<IJCombatAction>() { new FakeJCombatAction(jcombatQuery, "action2", null) });
            var lst2 = new List<IJCombatUnit>();
            lst2.Add(unit2);
            var team2 = new JCombatTeam("team2", lst2, funcUnit);

            var lstTeams = new List<IJCombatTeam>();
            lstTeams.Add(team1);
            lstTeams.Add(team2);

            jcombatQuery.AddRange(lstTeams);


            var turnbasedCombat = new JTurnBasedCombat(actionSelector, frameRecorder, jcombatQuery, new JCombatRunner(jcombatQuery, new FakeEventRecorder(), new FakeJCombatResult()));

            //act
            var result = await turnbasedCombat.GetResult();

            //expect
            Assert.AreEqual(19, frameRecorder.GetCurFrame());
            var hpAttr1 = jcombatQuery.GetUnit("unit1").GetAttribute("Hp") as GameAttributeInt;
            var hpAttr2 = jcombatQuery.GetUnit("unit2").GetAttribute("Hp") as GameAttributeInt;
            Assert.AreEqual(100, hpAttr1.CurValue);
            Assert.AreEqual(200, hpAttr2.CurValue);
        }

    }
}

