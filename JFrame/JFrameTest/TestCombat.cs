//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System;
using System.Runtime.Remoting.Contexts;
using static JFrame.PVPBattleManager;

namespace JFrameTest
{
    public class TestCombat
    {
        CombatUnit myUnit;
        CombatUnit unit1;
        CombatUnit unit2;

        CombatContext context;


        BattleFrame frame;

        List<CombatUnitInfo> team1;
        List<CombatUnitInfo> team2;
        CombatManager combatManager;

        [SetUp]
        public void Setup()
        {
            //创建combatManager
            combatManager = new CombatManager();
            team1 = new List<CombatUnitInfo>();
            team2 = new List<CombatUnitInfo>();
            team1.Add(CreateUnitInfo("1", 100, 10, 1f, new CombatVector(), new CombatVector(), CreateActions(4f)));
            team2.Add(CreateUnitInfo("2", 100, 1, 1f, new CombatVector() { x = 3 }, new CombatVector() { x = -1f }, CreateActions(2f)));
            team2.Add(CreateUnitInfo("3", 200, 2, 1f, new CombatVector() { x = 5 }, new CombatVector() { x = -1f }, CreateActions(3f)));
            combatManager.Initialize(team1, team2, 90);

            //获取我自己
            myUnit = combatManager.GetUnit("1");
            unit1 = combatManager.GetUnit("2");
            unit2 = combatManager.GetUnit("3");

            //创建上下文
            context = new CombatContext();
            context.CombatManager = combatManager;

            frame = context.CombatManager.Frame;
        }

        CombatUnitInfo CreateUnitInfo(string uid, int hp, int atk, float atkSpeed, CombatVector position, CombatVector moveSpeed , Dictionary<int, ActionInfo> actionsData = null)
        {
            var unitInfo = new CombatUnitInfo();
            unitInfo.uid = uid;
            unitInfo.hp = hp;
            unitInfo.atk = atk;
            unitInfo.maxHp = hp;
            unitInfo.atkSpeed = atkSpeed;
            unitInfo.position = position;
            unitInfo.moveSpeed = moveSpeed;
            unitInfo.actionsData = actionsData;
            return unitInfo;
        }

        Dictionary<int, ActionInfo> CreateActions(float range)
        {
            var actionsData = new Dictionary<int, ActionInfo>();
            var actionInfo = new ActionInfo();
            actionInfo.type = ActionType.Normal;
            actionInfo.mode = ActionMode.Active;
            actionInfo.uid = Guid.NewGuid().ToString();

            var dicComponentInfo = new Dictionary<ActionComponentType, List<ActionComponentInfo>>();
            //条件触发器
            var conditionTriggers = new List<ActionComponentInfo>();
            var conditionTrigger = new ActionComponentInfo() { id = 1, args = new float[] { range, 1 } }; //查找最近单位触发器 攻击距离， 查找个数
            conditionTriggers.Add(conditionTrigger);
            dicComponentInfo.Add(ActionComponentType.ConditionTrigger, conditionTriggers);
            //延迟触发器
            var delayTriggers = new List<ActionComponentInfo>();
            var delayTrigger = new ActionComponentInfo() { id = 3, args = new float[] { 0.1f } }; //时间触发器， 时长
            delayTriggers.Add(delayTrigger);
            dicComponentInfo.Add(ActionComponentType.DelayTrigger, delayTriggers);
            //查找器
            var finders = new List<ActionComponentInfo>();
            //var finder = new ActionComponentInfo() { id = 1, } // 执行器不需要
            dicComponentInfo.Add(ActionComponentType.Finder, finders);

            //执行器
            var executors = new List<ActionComponentInfo>();
            var executor = new ActionComponentInfo() { id = 1, args = new float[] { 0.1f, 1f } };//伤害触发器， 时长， 伤害倍率
            executors.Add(executor);
            dicComponentInfo.Add(ActionComponentType.Executor, executors);
            //cd触发器
            var cdTriggers = new List<ActionComponentInfo>();
            var cdTrigger = new ActionComponentInfo() { id = 3, args = new float[] { 0.1f } };//时间触发器， 时长
            cdTriggers.Add(cdTrigger);
            dicComponentInfo.Add(ActionComponentType.CdTrigger, cdTriggers);

            actionInfo.componentInfo = dicComponentInfo;
            actionsData.Add(1, actionInfo);//actionID

            return actionsData;
        }

       [Test]
        public void TestCombatResult()
        {
            //arrange


            //act
            //frame.NextFrame();
            //combatManager.Update(frame); //0.25f    
            //frame.NextFrame();
            //combatManager.Update(frame); //0.5f    //触发了，但是有延迟0.1f才会生效
            //frame.NextFrame();
            //combatManager.Update(frame);//0.75f      //生效了，持续0.1f
            //frame.NextFrame();
            //combatManager.Update(frame);
            //frame.NextFrame();
            //combatManager.Update(frame);
            //frame.NextFrame();
            //combatManager.Update(frame);
            //frame.NextFrame();


            var report = combatManager.GetResult();



            //expect
            var reporter = combatManager.Reporter;
            Assert.AreEqual(2f, unit1.GetPosition().x); //攻击距离1，所以不走了
            Assert.AreEqual(3f, unit2.GetPosition().x); //攻击距离1，所以不走了
            //Assert.AreEqual(true, triggerOn);
            //Assert.AreEqual(true, isOn);
            Assert.AreEqual(0, combatManager.GetUnit("2").GetAttributeCurValue(PVPAttribute.HP));
            Assert.AreEqual(0, combatManager.GetUnit("3").GetAttributeCurValue(PVPAttribute.HP));
            Assert.AreEqual(33, combatManager.GetUnit("1").GetAttributeCurValue(PVPAttribute.HP));
        }
    }


}
