//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NUnit.Framework;
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
            attacker.Add(new BattlePoint(1), new BattleUnitInfo() { actionsId = new List<int>() { 1 }, atk = 10, id = 1, hp = 20 });
            attacker.Add(new BattlePoint(2), new BattleUnitInfo() { actionsId = new List<int>() { 1 }, atk = 8, id = 2, hp = 16 });
            defence.Add(new BattlePoint(3), new BattleUnitInfo() { actionsId = new List<int>() { 1 }, atk = 12, id = 2, hp = 18 });
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

            //action
            battle.Initialize(attacker, defence);

            //expect
            Assert.AreEqual(2, battle.GetUnitCount(PVPBattleManager.Team.Attacker));
            Assert.AreEqual(1, battle.GetUnitCount(PVPBattleManager.Team.Defence));
        }

        [Test]
        public void TestFindTarget()
        {
            //arrange
            BattleTrigger trigger = new CDTrigger(0f);
            BattleTargetFinder finder = Substitute.For<NormalTargetFinder>(Substitute.For<BattlePoint>(1), null);
            BattleFrame frame = Substitute.For<BattleFrame>();
            var normalAction = Substitute.For<NormalAttack>(trigger, finder);

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
            BattleTrigger trigger = new CDTrigger(1f);
            BattleTargetFinder finder = Substitute.For<NormalTargetFinder>(Substitute.For<BattlePoint>(1), null);
            BattleFrame frame = new BattleFrame();
            var normalAction = Substitute.For<NormalAttack>(trigger, finder);

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
            BattleTrigger trigger = new CDTrigger(1f,1f);
            BattleTargetFinder finder = Substitute.For<NormalTargetFinder>(Substitute.For<BattlePoint>(1), null);
            BattleFrame frame = new BattleFrame();
            var normalAction = Substitute.For<NormalAttack>(trigger, finder);

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

        //[Test]
        //public void BattleGetResultAndUpdateCall()
        //{
        //    //arrange

        //    //action
        //    battle.Initialize(attacker, defence);
        //    var report = battle.GetResult();

        //    //expect

        //}
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
