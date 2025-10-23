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
        public class NormalFormula : JCombatFormulaBase
        {
            public NormalFormula(float[] args) : base(args)
            {
            }

            public override void CalcHitValue(IJAttributeableUnit target, ref float value, IJCombatExecutorExecuteArgs executeArgs)
            {
                //伤害= 释放者攻击力 * 释放者Power - 目标防御力 * 目标Def
                var caster = query.GetUnit(GetOwner().GetCaster());
                var atk = caster.GetAttribute("Atk") as GameAttributeInt;
                var targetDef = target.GetAttribute("Def") as GameAttributeInt;
                value = atk.CurValue - targetDef.CurValue;
            }

            protected override int GetValidArgsCount()
            {
                return 0; // 不需要参数
            }
        }

        public class FakeAttrFacotry : IJCombatUnitAttrFactory
        {
            public List<IUnique> Create()
            {
                var result = new List<IUnique>();

                var hp = new GameAttributeInt("Hp", 100, 100);
                var maxhp = new GameAttributeInt("MaxHp", 200, 200);
                var atk = new GameAttributeInt("Atk", 70, 70);
                var def = new GameAttributeInt("Def", 10, 10);
                var speed = new GameAttributeInt("Speed", 50, 50);
                result.Add(hp);
                result.Add(speed);
                result.Add(atk);
                result.Add(def);
                result.Add(maxhp);

                return result;
            }
        }


        public class FakeAttrFacotry2 : IJCombatUnitAttrFactory
        {
            public List<IUnique> Create()
            {
                var result = new List<IUnique>();

                var hp = new GameAttributeInt("Hp", 200, 200);
                var maxhp = new GameAttributeInt("MaxHp", 200, 200);
                var atk = new GameAttributeInt("Atk", 60, 60);
                var def = new GameAttributeInt("Def", 20, 20);
                var speed = new GameAttributeInt("Speed", 40, 60);
                result.Add(hp);
                result.Add(speed);
                result.Add(atk);
                result.Add(def);
                result.Add(maxhp);

                return result;
            }
        }

        public class FakeAttrNameQuery : IJCombatTurnBasedAttrNameQuery
        {
            public string GetActionPointName()
            {
                return "Speed";
            }

            public string GetAttrName(int nameId)
            {
                throw new NotImplementedException();
            }

            public string GetHpAttrName()
            {
                return "Hp";
            }

            public string GetMaxHpAttrName()
            {
                return "MaxHp";
            }
        }

        public class FakeEventRecorder : JCombatTurnBasedEventRecorder
        {
            public FakeEventRecorder(IJCombatFrameRecorder frameRecorder, Func<JCombatTurnBasedEvent, string> keySelector) : base(frameRecorder, keySelector)
            {
            }
        }

        public class FakeContext : IJCombatContext
        {
            public IJCombatTurnBasedEventRecorder EventRecorder { get;  set; }

            public ILogger Logger { get; set; }

            public FakeContext(IJCombatTurnBasedEventRecorder eventRecorder)
            {
                this.EventRecorder = eventRecorder;
            }
        }

        public class FakeJCombatResult : IJCombatTurnBasedReportBuilder
        {
            public JCombatTurnBasedReportData<T> GetCombatReportData<T>() where T :class, IJCombatUnitData
            {
                return null;
            }

            public void SetCombatEvents(List<JCombatTurnBasedEvent> events)
            {
                //throw new NotImplementedException();
            }

            public void SetCombatWinner(IJCombatTeam team)
            {
                //throw new NotImplementedException();
            }

            public void SetForamtionData(List<IJCombatTeam> teams)
            {
                //throw new NotImplementedException();
            }
        }

        public class FakeJCombatAction : JCombatActionBase
        {
            public FakeJCombatAction(string uid, List<IJCombatExecutor> executors, IJCombatTargetsFinder finder, IJCombatContext context) : base(uid, null, finder, executors, context.EventRecorder)
            {
            }
        }


        JCombatTurnBased turnbasedCombat;
        JCombatTurnBasedFrameRecorder frameRecorder;
        JCombatSeatBasedQuery jcombatQuery;
        JCombatTeam team1;
        JCombatTeam team2;
        FakeEventRecorder eventRecorder;
        JCombatTurnBasedActionSelector actionSelector;

        Func<IUnique, string> funcAttr = (attr) => attr.Uid;

        JCombatTurnBasedRunner combatRunner;

        IJCombatContext context ;


        [SetUp]
        public void Setup()
        {
            Func<IJCombatUnit, string> funcUnit = (unit) => unit.Uid;
            
            Func<IJCombatTeam, string> funcTeam = (team) => team.Uid;
            Func<string, int> funcSeat = (unitUid) => { 
            
                switch(unitUid)
                {
                    case "unit1":
                    case "unit2":
                        return 1;
                    case "unit3":
                        return 2;
                    default:
                        throw new Exception("没有定义座位 " + unitUid);
                }
            };
            Func<JCombatTurnBasedEvent, string> funcEvent = (e) => e.Uid;
            var attrNameQuery = new FakeAttrNameQuery();



            frameRecorder = new JCombatTurnBasedFrameRecorder(19); //从0开始，共20回合
            var attrFactory = new FakeAttrFacotry();
            var attrFactory2 = new FakeAttrFacotry2();



            jcombatQuery = new JCombatSeatBasedQuery(funcSeat, funcTeam, funcUnit, frameRecorder, attrNameQuery);
            eventRecorder = new FakeEventRecorder(frameRecorder, funcEvent);
            context = new FakeContext(eventRecorder);

            //执行器
            var finder1 = new JCombatFindOppoDefault(null);
            var formula1 = new NormalFormula(null);
            var executor1 = new JCombatExecutorDamage(context,null,finder1, formula1, null);
            var lstExecutor1 = new List<IJCombatExecutor>();
            lstExecutor1.Add(executor1);

            

            //队伍1
            var unit1 = new JCombatTurnBasedUnit("unit1", attrFactory.Create(), funcAttr, attrNameQuery, new List<IJCombatAction>() { new FakeJCombatAction( "action1",  lstExecutor1, finder1, context) }, context);
            var lst1 = new List<IJCombatUnit>();
            lst1.Add(unit1);
            team1 = new JCombatTeam("team1", lst1, funcUnit);


   

            //队伍2
            var unit2 = new JCombatTurnBasedUnit("unit2", attrFactory2.Create(), funcAttr, attrNameQuery,  new List<IJCombatAction>() { new FakeJCombatAction( "action2", null, null ,context) }, context);
            var lst2 = new List<IJCombatUnit>();
            lst2.Add(unit2);
            team2 = new JCombatTeam("team2", lst2, funcUnit);

            var lstTeams = new List<IJCombatTeam>();
            lstTeams.Add(team1);
            lstTeams.Add(team2);


            jcombatQuery.SetTeams(lstTeams);

            actionSelector = new JCombatTurnBasedActionSelector(jcombatQuery.GetUnits().OfType<IJCombatTurnBasedUnit>().ToList(), funcUnit);

            var runables = new List<IRunable>();
            runables.Add(team1);
            runables.Add(team2);

            turnbasedCombat = new JCombatTurnBased(actionSelector, frameRecorder, jcombatQuery, runables);


            combatRunner = new JCombatTurnBasedRunner(turnbasedCombat, jcombatQuery, eventRecorder, new FakeJCombatResult());
            //combatRunner.SetRunable(turnbasedCombat);
        }

        [Test]
        public async Task TestDefaultFinder()
        {
            //arrange


            //act
            await combatRunner.Run();

            var result = combatRunner.GetReport();

            //expect
            Assert.AreEqual(3, frameRecorder.GetCurFrame());
            var hpAttr1 = jcombatQuery.GetUnit("unit1").GetAttribute("Hp") as GameAttributeInt;
            var hpAttr2 = jcombatQuery.GetUnit("unit2").GetAttribute("Hp") as GameAttributeInt;
            Assert.AreEqual(100, hpAttr1.CurValue);
            Assert.AreEqual(0, hpAttr2.CurValue);
            Assert.AreEqual(team1, jcombatQuery.GetWinner());
            Assert.AreEqual(7, eventRecorder.Count());


        }

        [Test]
        public async Task Test1V2Result()
        {
            //arrange
            var attrFactory2 = new FakeAttrFacotry2();
            var unit3 = new JCombatTurnBasedUnit("unit3", attrFactory2.Create(), funcAttr, new FakeAttrNameQuery(),  new List<IJCombatAction>() { new FakeJCombatAction( "action3", null, null, context) }, context);
            team2.Add(unit3);
            actionSelector.AddUnits(new List<IJCombatTurnBasedUnit> { unit3 });

            //act

            await combatRunner.Run();
            var result = combatRunner.GetReport();

            //expect
            Assert.AreEqual(7, frameRecorder.GetCurFrame());
            var hpAttr1 = jcombatQuery.GetUnit("unit1").GetAttribute("Hp") as GameAttributeInt;
            var hpAttr2 = jcombatQuery.GetUnit("unit2").GetAttribute("Hp") as GameAttributeInt;
            var hpAttr3 = jcombatQuery.GetUnit("unit3").GetAttribute("Hp") as GameAttributeInt;
            Assert.AreEqual(100, hpAttr1.CurValue);
            Assert.AreEqual(0, hpAttr2.CurValue);
            Assert.AreEqual(0, hpAttr3.CurValue);
            Assert.AreEqual(team1, jcombatQuery.GetWinner());
            Assert.AreEqual(18, eventRecorder.Count());

        }

        [Test]
        public async Task TestListeners()
        {
            //arrange
            var attrFactory2 = new FakeAttrFacotry2();
            var unit3 = new JCombatTurnBasedUnit("unit3", attrFactory2.Create(), funcAttr, new FakeAttrNameQuery(), new List<IJCombatAction>() { new FakeJCombatAction("action3", null, null, context) }, context);
            team2.Add(unit3);
            actionSelector.AddUnits(new List<IJCombatTurnBasedUnit> { unit3 });

            //act
            await combatRunner.Run();
            var result = combatRunner.GetReport();

            //expect
            Assert.AreEqual(7, frameRecorder.GetCurFrame());
            var hpAttr1 = jcombatQuery.GetUnit("unit1").GetAttribute("Hp") as GameAttributeInt;
            var hpAttr2 = jcombatQuery.GetUnit("unit2").GetAttribute("Hp") as GameAttributeInt;
            var hpAttr3 = jcombatQuery.GetUnit("unit3").GetAttribute("Hp") as GameAttributeInt;
            Assert.AreEqual(100, hpAttr1.CurValue);
            Assert.AreEqual(0, hpAttr2.CurValue);
            Assert.AreEqual(0, hpAttr3.CurValue);
            Assert.AreEqual(team1, jcombatQuery.GetWinner());
            Assert.AreEqual(18, eventRecorder.Count());

            //listner.Received(8).OnTurnStart(Arg.Any<int>());
            //listner.Received(7).OnTurnEnd(Arg.Any<int>());

        }
    }
}

