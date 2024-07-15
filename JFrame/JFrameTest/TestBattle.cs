//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace JFrameTest
{
    public class TestBattle
    {
        // GetTextLength测试null字符串
        PVPBattleManager battle;
        Dictionary<BattlePoint, BattleUnitInfo> attacker = new Dictionary<BattlePoint, BattleUnitInfo>();
        Dictionary<BattlePoint, BattleUnitInfo> defence = new Dictionary<BattlePoint, BattleUnitInfo>();

        [SetUp]
        public void SetUp()
        {
            battle = new PVPBattleManager();
            attacker.Add(new BattlePoint(1, PVPBattleManager.Team.Attacker), new BattleUnitInfo() { actionsId = new List<int>() { 1 }, atk = 10, id = 1, hp = 20 });
            attacker.Add(new BattlePoint(2, PVPBattleManager.Team.Attacker), new BattleUnitInfo() { actionsId = new List<int>() { 1 }, atk = 8, id = 2, hp = 16 });
            defence.Add(new BattlePoint(3, PVPBattleManager.Team.Defence), new BattleUnitInfo() { actionsId = new List<int>() { 1 }, atk = 12, id = 3, hp = 190 });
        }

        [TearDown]
        public void Clear()
        {
            attacker.Clear();
            defence.Clear();
        }


        [Test]
        public void InitializeBattleSuccess()
        {
            //arrange
            var cfg = new ActionConfig();
            //action
            battle.Initialize(attacker, defence, cfg);

            //expect
            Assert.AreEqual(2, battle.GetUnitCount(PVPBattleManager.Team.Attacker));
            Assert.AreEqual(1, battle.GetUnitCount(PVPBattleManager.Team.Defence));
        }

        [Test]
        public void TestFindTarget()
        {
            //arrange
            var cfg = new ActionConfig();
            battle.Initialize(attacker, defence, cfg);
            BattleTrigger trigger = new CDTrigger(0f);
            IBattleTargetFinder finder = Substitute.For<NormalTargetFinder>(Substitute.For<BattlePoint>(1, PVPBattleManager.Team.Attacker), battle);
            BattleFrame frame = Substitute.For<BattleFrame>();
            var actionId = 1;
            var normalAction = Substitute.For<NormalAttack>(actionId, trigger, finder);

            //action
            normalAction.Update(frame);

            //expect
            //trigger.Received(1).Update(frame);
            finder.Received(1).FindTargets();
        }

        [Test]
        public void TestUnderTriggerCd()
        {
            //arrange
            var cfg = new ActionConfig();
            battle.Initialize(attacker, defence, cfg);
            BattleTrigger trigger = new CDTrigger(1f);
            IBattleTargetFinder finder = Substitute.For<NormalTargetFinder>(Substitute.For<BattlePoint>(1, PVPBattleManager.Team.Attacker), battle);
            BattleFrame frame = new BattleFrame();
            var actionId = 1;
            var normalAction = Substitute.For<NormalAttack>(actionId,trigger, finder);

            //action
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            //第二个周期
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);

            //expect
            //trigger.Received(1).Update(frame);
            finder.Received(2).FindTargets();
            Assert.AreEqual(true, trigger.GetState()); //开关只要没关就一直update
        }

        [Test]
        public void TestUnderTriggerDelay()
        {
            //arrange
            var cfg = new ActionConfig();
            battle.Initialize(attacker, defence, cfg);
            BattleTrigger trigger = new CDTrigger(1f,1f);
            IBattleTargetFinder finder = Substitute.For<NormalTargetFinder>(Substitute.For<BattlePoint>(1, PVPBattleManager.Team.Attacker), battle);
            BattleFrame frame = new BattleFrame();
            var actionId = 1;
            var normalAction = Substitute.For<NormalAttack>(actionId, trigger, finder);

            //action
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            //第一个周期
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);
            frame.NextFrame();
            normalAction.Update(frame);


            //expect
            //trigger.Received(1).Update(frame);
            finder.Received(1).FindTargets();
        }


        [Test]
        public void TestAddReport()
        {
            //arrange
            var reporter = new BattleReporter();
            var hostUID = Guid.NewGuid().ToString();
            var targetUID = "333";
            //action
            var reportUid = reporter.AddReportMainData(1,0f, hostUID,1,new List<string>() { targetUID });
            var reportData = reporter.GetReportData(reportUid);
            reporter.AddReportResultData(reportUid, targetUID, 1, 4, -1);

            //expect
            Assert.AreEqual(hostUID, reportData.CasterUID);
            Assert.AreEqual(4, reportData.TargetsAttribute[targetUID]);
        }

        [Test]
        public void TestUpdateReport()
        {
            //arrange
            var reporter = new BattleReporter();
            var hostUID = Guid.NewGuid().ToString();
            var targetUID = "333";
            //action
            var reportUid = reporter.AddReportMainData(1, 0f, hostUID, 1, new List<string>() { targetUID });
            var reportData = reporter.GetReportData(reportUid);
            var values = new Dictionary<string, int>
            {
                { targetUID, 5 }
            };
            reporter.UpdateReportResultData(reportUid, values, null, null);

            //expect
            Assert.AreEqual(hostUID, reportData.CasterUID);
            Assert.AreEqual(5, reportData.TargetsValue[targetUID]);
        }

        [Test]
        public void TestAddResultReport()
        {
            //arrange
            var reporter = new BattleReporter();
            var hostUID = Guid.NewGuid().ToString();
            var targetUID = "333";
            //action
            var reportUid = reporter.AddReportMainData(1, 0f, hostUID, 1, new List<string>() { targetUID });
            var reportData = reporter.GetReportData(reportUid);
            var values = 5;
            reporter.AddReportResultData(reportUid, targetUID, values, 5, -1);

            //expect
            Assert.AreEqual(hostUID, reportData.CasterUID);
            Assert.AreEqual(5, reportData.TargetsValue[targetUID]);
        }

        [Test]
        public void TestActionCast()
        {
            //arrange
            var cfg = Substitute.For<ActionConfig>();
            cfg.GetTriggerArg(Arg.Any<int>()).Returns(0);
            cfg.GetTriggerType(Arg.Any<int>()).Returns(1);
            cfg.GetDuration(Arg.Any<int>()).Returns(0);
            cfg.GetFinderType(Arg.Any<int>()).Returns(1);
            //action
            battle.Initialize(attacker, defence, cfg);
            battle.Update();
            var reporter = battle.GetReporter();
            var lstData = reporter.GetAllReportData();

            //expect
            Assert.AreEqual(3, lstData.Count);
            var unit = battle.GetUnit(PVPBattleManager.Team.Defence, 3);
            Assert.AreEqual(172, unit.HP);
        }

        [Test]
        public void TestGetResult()
        {
            //arrange
            var cfg = new ActionConfig();
            //action
            battle.Initialize(attacker, defence, cfg);
            var lstData = battle.GetResult();
            //var reporter = battle.GetReporter();
            //var lstData = reporter.GetAllReportData();

            //expect
            Assert.AreEqual(12, lstData.Count);
            var unit3 = battle.GetUnit(PVPBattleManager.Team.Defence, 3);
            Assert.AreEqual(118, unit3.HP);

            var unit1 = battle.GetUnit(PVPBattleManager.Team.Attacker, 1);
            Assert.AreEqual(0, unit1.HP);

            var unit2 = battle.GetUnit(PVPBattleManager.Team.Attacker, 2);
            Assert.AreEqual(0, unit2.HP);
        }
    }

    //public class TestBattleAction
    //{


    //    [SetUp]
    //    public void SetUp()
    //    {

    //    }

    //    [TearDown]
    //    public void Clear()
    //    {

    //    }


    //    [Test]
    //    public void TestActionUpdateAndTriggerUpdateSuccess() //CD触发器在CD内只会触发一次
    //    {
    //        //arrange
    //        //var normalAction = new NormalAttack(new CDTrigger);

    //        //action


    //        //expect

    //    }
    //}

    //    public interface UnityGameObject
    //    {
    //        void AddChild(UnityGameObject go);
    //    }

    //    public class UnityUIManager : UIManager<UnityGameObject>
    //    {
    //        public UnityUIManager(IInstantiator<UnityGameObject> instantiator, IViewBinder<UnityGameObject> viewBinder) : base(instantiator, viewBinder) { }
    //        protected override void SetRelationship(UnityGameObject parent, IUIView child)
    //        {
    //            //parent.AddChild(child);
    //        }
    //    }

    //    public class TestUIManager
    //    {


    //        /// <summary>
    //        /// 打开ui，并进行正确的调用
    //        /// </summary>
    //        [Test]
    //        public void TestOpenUI()
    //        {

    //            //Arrange
    //            string prefabName = "login";
    //            var parent = Substitute.For<UnityGameObject>();
    //            var view = Substitute.For<IUIView>();
    //            var go = Substitute.For<UnityGameObject>();
    //            //模拟实例化器
    //            var instantiator = Substitute.For<IInstantiator<UnityGameObject>>();
    //            instantiator.Instantiate(prefabName, parent).Returns(go);
    //            //模拟绑定器
    //            var viewBinder = Substitute.For<IViewBinder<UnityGameObject>>();
    //            viewBinder.BindView<IUIView>(go).Returns(view);
    //            //模拟ui管理器
    //            var uiManager = Substitute.For<UIManager<UnityGameObject>>(instantiator, viewBinder);

    //            //Act
    //            uiManager.Open<IUIView>(prefabName, parent);

    //            //Assert
    //            instantiator.Received().Instantiate(prefabName, parent);
    //            viewBinder.Received().BindView<IUIView>(go);
    //        }

    //        /// <summary>
    //        /// 测试正确打开，并正确的父节点和子节点
    //        /// </summary>
    //        [Test]
    //        public void TestOpenUIAndCorrectRecorgenize()
    //        {
    //            //Arrange
    //            string prefabName = "login";
    //            var view = Substitute.For<IUIView>();
    //            var go = Substitute.For<UnityGameObject>();
    //            var parent = Substitute.For<UnityGameObject>();   


    //            //模拟实例化器
    //            var instantiator = Substitute.For<IInstantiator<UnityGameObject>>();
    //            instantiator.Instantiate(prefabName, parent).Returns(go);
    //            //模拟绑定器
    //            var viewBinder = Substitute.For<IViewBinder<UnityGameObject>>();
    //            viewBinder.BindView<IUIView>(go).Returns(view);
    //            //模拟ui管理器
    //            var uiManager = Substitute.For<UIManager<UnityGameObject>>(instantiator, viewBinder);


    //            //Act
    //            var ui = uiManager.Open<IUIView>(prefabName, parent);

    //            //Assert
    //            //view.Received().Parent = parent;
    //            //parent.Received().AddChild(view);
    //        }
    //    }
}
