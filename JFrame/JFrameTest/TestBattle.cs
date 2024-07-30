//using JFrame.UI;
//using NUnit.Framework;
using JFrame;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JFrameTest
{
    public class TestBattle
    {
        // GetTextLength测试null字符串
        PVPBattleManager battle;
        Dictionary<BattlePoint, BattleUnitInfo> attacker = new Dictionary<BattlePoint, BattleUnitInfo>();
        Dictionary<BattlePoint, BattleUnitInfo> defence = new Dictionary<BattlePoint, BattleUnitInfo>();

        Dictionary<PVPBattleManager.Team, BattleTeam> teams = new Dictionary<PVPBattleManager.Team, BattleTeam>();

        ActionDataSource actionDataSource;
        BufferDataSource actionBufferDataSource;
        IBattleReporter reporter;
        IPVPBattleManager simBattle;
        BattleFrame battleFrame;


        [SetUp]
        public void SetUp()
        {
            //var cfg = new DataSource();
            battle =  new PVPBattleManager(); //Substitute.For<PVPBattleManager>();// 
            attacker.Add(new BattlePoint(1, PVPBattleManager.Team.Attacker), new BattleUnitInfo() {uid = Guid.NewGuid().ToString(), actionsId = new List<int>() { 1 }, atk = 10, id = 1, hp = 20 });
            attacker.Add(new BattlePoint(2, PVPBattleManager.Team.Attacker), new BattleUnitInfo() { uid = Guid.NewGuid().ToString(), actionsId = new List<int>() { 1 }, atk = 8, id = 2, hp = 16 });
            defence.Add(new BattlePoint(3, PVPBattleManager.Team.Defence), new BattleUnitInfo() { uid = Guid.NewGuid().ToString(), actionsId = new List<int>() { 1 }, atk = 12, id = 3, hp = 190 });


            //attacker = Substitute.For<Dictionary<BattlePoint, BattleUnitInfo>>();
            //defence = Substitute.For<Dictionary<BattlePoint, BattleUnitInfo>>();
            
            actionDataSource = Substitute.For<ActionDataSource>(battle);
            actionBufferDataSource = Substitute.For<BufferDataSource>();         
            battleFrame = Substitute.For<BattleFrame>();
            reporter = Substitute.For<BattleReporter>(battleFrame, teams);
            simBattle = Substitute.For<PVPBattleManager>();
        }

        [TearDown]
        public void Clear()
        {
            attacker.Clear();
            defence.Clear();
        }


        /// <summary>
        /// 战斗管理器初始化成功, createTeam调用
        /// </summary>
        [Test]
        public void TestBattleManagerInitialize()
        {
            //arrange
            actionDataSource.GetConditionTriggerType(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1); //返回 cdtrigger
            actionDataSource.GetFinderType(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1); //返回 orderTargetFinder
            actionDataSource.GetExcutorTypes(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new List<int>() {1}); //返回 battleDamage

            //action
            simBattle.Initialize(attacker, defence, actionDataSource, actionBufferDataSource,reporter);

            //expect
            simBattle.Received(1).CreateTeam(PVPBattleManager.Team.Attacker, attacker);
            simBattle.Received(1).CreateTeam(PVPBattleManager.Team.Defence, defence);

        }

        /// <summary>
        /// 战斗管理器更新
        /// </summary>
        [Test]
        public void TestBattleManagerUpdateAndTeamUpdateCalled()
        {
            //arrange
            actionDataSource.GetConditionTriggerType(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1); //返回 cdtrigger
            actionDataSource.GetFinderType(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1); //返回 orderTargetFinder
            actionDataSource.GetExcutorTypes(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new List<int>() { 1 }); //返回 battleDamage
            //var team = battle.CreateTeam(Arg.Any<PVPBattleManager.Team>(), Arg.Any<Dictionary<BattlePoint, BattleUnitInfo>>()).Returns(Substitute.For<IBattleTeam>());

            //action
            //simBattle.Initialize(attacker, defence, actionDataSource, actionBufferDataSource, reporter);
            var atkTeam = Substitute.For<BattleTeam>(PVPBattleManager.Team.Attacker, null);
            atkTeam.AddUnit(new BattlePoint(1, PVPBattleManager.Team.Attacker), Substitute.For<IBattleUnit>());
            simBattle.AddTeam(PVPBattleManager.Team.Attacker, atkTeam);
            simBattle.Update();

            //expect
            atkTeam.Received(1).Update(battleFrame);
        }

        //[Test]
        //public void TestFindTarget()
        //{
        //    //arrange
        //    var cfg = new ActionDataSource(battle);
        //    var timer = new JFrameTimerUtils();
        //    battle.Initialize(attacker, defence, cfg, null, reporter);
        //    BaseBattleTrigger trigger = new CDTimeTrigger(battle, 0, 0f);
        //    IBattleTargetFinder finder = Substitute.For<OrderOppoFinder>(Substitute.For<BattlePoint>(1, PVPBattleManager.Team.Attacker), battle, cfg.GetFinderArg("1",1, 1));
        //    IBattleExecutor excutor = Substitute.For<ExecutorDamage>(cfg.GetExcutorArg("1",1, 1, 1));
        //    BattleFrame frame = Substitute.For<BattleFrame>();
        //    var actionId = 1;
        //    var normalAction = Substitute.For<BaseAction>("uid",actionId, trigger, finder, new List<IBattleExecutor>() { excutor });

        //    //action
        //    normalAction.Update(frame);

        //    //expect
        //    //trigger.Received(1).Update(frame);
        //    finder.Received(1).FindTargets();
        //}

        //[Test]
        //public void TestUnderTriggerCd()
        //{
        //    //arrange
        //    var cfg = new ActionDataSource(battle);
        //    var timer = new JFrameTimerUtils();
        //    battle.Initialize(attacker, defence, cfg, null, reporter);
        //    BaseBattleTrigger trigger = new CDTimeTrigger(battle, 1f, 0);
        //    IBattleTargetFinder finder = Substitute.For<OrderOppoFinder>(Substitute.For<BattlePoint>(1, PVPBattleManager.Team.Attacker), battle, cfg.GetFinderArg("1", 1, 1));
        //    IBattleExecutor excutor = Substitute.For<ExecutorDamage>(cfg.GetExcutorArg("1", 1, 1, 1));
        //    BattleFrame frame = new BattleFrame();
        //    var actionId = 1;
        //    var normalAction = Substitute.For<BaseAction>("uid", actionId, trigger, finder, new List<IBattleExecutor>() { excutor });

        //    //action
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    //第二个周期
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);

        //    //expect
        //    //trigger.Received(1).Update(frame);
        //    finder.Received(2).FindTargets();
        //    Assert.AreEqual(true, trigger.GetEnable()); //开关只要没关就一直update
        //}

        //[Test]
        //public void TestUnderTriggerDelay()
        //{
        //    //arrange
        //    var cfg = new ActionDataSource(battle);
        //    var timer = new JFrameTimerUtils();
        //    battle.Initialize(attacker, defence, cfg, null, reporter);
        //    BaseBattleTrigger trigger = new CDTimeTrigger(battle, 1f, 1f);
        //    IBattleTargetFinder finder = Substitute.For<OrderOppoFinder>(Substitute.For<BattlePoint>(1, PVPBattleManager.Team.Attacker), battle, cfg.GetFinderArg("1",1, 1));
        //    IBattleExecutor excutor = Substitute.For<ExecutorDamage>(cfg.GetExcutorArg("1", 1, 1, 1));
        //    BattleFrame frame = new BattleFrame();
        //    var actionId = 1;
        //    var normalAction = Substitute.For<BaseAction>("uid", actionId, trigger, finder, new List<IBattleExecutor>() { excutor });

        //    //action
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    //第一个周期
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);
        //    frame.NextFrame();
        //    normalAction.Update(frame);


        //    //expect
        //    //trigger.Received(1).Update(frame);
        //    finder.Received(1).FindTargets();
        //}


        [Test]
        public void TestAddReport()
        {
            //arrange
            var reporter = new BattleReporter(Substitute.For<BattleFrame>(), battle.GetTeams());
            var casterUID = Guid.NewGuid().ToString();
            var targetUID = Guid.NewGuid().ToString();
            //action
            var reportUid = reporter.AddReportData(casterUID, ReportType.Action, targetUID, new object[] { 1 });
            var reportData = reporter.GetReportData(reportUid);
            reporter.AddReportData(casterUID, ReportType.Damage, targetUID, new object[] { 1 });

            //expect
            Assert.AreEqual(casterUID, reportData.CasterUID);
            Assert.AreEqual(2, reporter.GetAllReportData().Count);
        }


        //[Test]
        //public void TestActionCast()
        //{
        //    //arrange
        //    var cfg = Substitute.For<ActionDataSource>(battle);
        //    cfg.GetConditionTriggerArg(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(0);
        //    cfg.GetConditionTriggerType(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1);
        //    //cfg.GetDuration(Arg.Any<int>()).Returns(0);
        //    cfg.GetFinderType(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1);
        //    cfg.GetExcutorTypes(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new List<int>() { 1 });
        //    cfg.GetExcutorArg(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new float[] { 1, 0.1f, 0, 1 });
        //    //action
        //    battle.Initialize(attacker, defence, cfg, null, null);
        //    battle.Update();

        //    //timer.Call();
        //    //timer.Call();
        //    var reporter = battle.GetReporter();
        //    var lstData = reporter.GetAllReportData();

        //    //expect
        //    Assert.AreEqual(3, lstData.Count);
        //    var unit = battle.GetUnit(PVPBattleManager.Team.Defence, 3);
        //    Assert.AreEqual(190, unit.HP);
        //}

        //[Test]
        //public void TestGetResult()
        //{
        //    //arrange
        //    var cfg = new ActionDataSource(battle);
        //    var timer = new JFrameTimerUtils();
        //    //action
        //    battle.Initialize(attacker, defence, cfg, null, null);
        //    var lstData = battle.GetResult();
        //    //var reporter = battle.GetReporter();
        //    //var lstData = reporter.GetAllReportData();

        //    //expect
        //    Assert.AreEqual(22, lstData.report.Count);
        //    var unit3 = battle.GetUnit(PVPBattleManager.Team.Defence, 3);
        //    Assert.AreEqual(138, unit3.HP);

        //    var unit1 = battle.GetUnit(PVPBattleManager.Team.Attacker, 1);
        //    Assert.AreEqual(0, unit1.HP);

        //    var unit2 = battle.GetUnit(PVPBattleManager.Team.Attacker, 2);
        //    Assert.AreEqual(0, unit2.HP);
        //}

        //[Test]
        //public void TestReportParser()
        //{
        //    //arrange
        //    var cfg = new ActionDataSource(battle);
        //    var timer = new JFrameTimerUtils();
        //    battle.Initialize(attacker, defence, cfg, null, null);
        //    var lstData = battle.GetResult();
        //    var parser = new PVPBattleReportParser(lstData.report);

        //    //action
        //    Assert.AreEqual(22, parser.Count());

        //    var result = parser.GetData(3);
        //    Assert.AreEqual(3, result.Count);
        //    Assert.AreEqual(19, parser.Count());
        //}

        [Test]
        public void TestAddBuffAndCallOnAttach()
        {
            //arrange
            var buffDataSource = Substitute.For<BufferDataSource>();
            var buffFactory = Substitute.For<BufferFactory>(buffDataSource);
            var buffer = Substitute.For<JFrame.Buffer>("1", 1, 1, new float[] { });
            buffFactory.Create(Arg.Any<int>(), Arg.Any<int>()).Returns(buffer);
            var buffManager = new BaseBufferManager(buffDataSource, buffFactory);
            var unit = new BattleUnit(new BattleUnitInfo(), Substitute.For<ActionManager>(), buffManager);

            //action
            buffManager.AddBuffer(unit, 1, 1);

            //expect
            buffer.Received().OnAttach(unit);
        }

        [Test]
        public void TestRemoveBuffAndCallOnDettach()
        {
            //arrange
            string bufferUID = "1";
            var buffDataSource = Substitute.For<BufferDataSource>();
            var buffFactory = Substitute.For<BufferFactory>(buffDataSource);
            var buffer = Substitute.For<JFrame.Buffer>(bufferUID, 1, 1, new float[] { });
            buffer.UID.Returns(bufferUID);
            buffFactory.Create(Arg.Any<int>(), Arg.Any<int>()).Returns(buffer);
            var buffManager = new BaseBufferManager(buffDataSource, buffFactory);
            var unit = new BattleUnit(new BattleUnitInfo(), Substitute.For<ActionManager>(), buffManager);

            //action
            buffManager.AddBuffer(unit, 1, 1);
            buffManager.RemoveBuffer(bufferUID);

            //expect
            buffer.Received(1).OnDettach();
        }

        [Test]
        public void TesUpdateDurationBuffAndCallOnDettach()
        {
            //arrange
            string bufferUID = "1";
            var buffDataSource = Substitute.For<BufferDataSource>();
            var buffFactory = Substitute.For<BufferFactory>(buffDataSource);
            //var buffer = new DurationBuffer("1", 1, 1, new float[] {1});
            var buffer = Substitute.For<JFrame.DurationBuffer>(bufferUID, 1, 1, new float[] { 1f });
            buffer.UID.Returns(bufferUID);
            buffer.IsValid().Returns(false);
            buffer.GetDuration().Returns(1f);
            buffFactory.Create(Arg.Any<int>(), Arg.Any<int>()).Returns(buffer);
            var buffManager = new BaseBufferManager(buffDataSource, buffFactory);
            var unit = new BattleUnit(new BattleUnitInfo(), Substitute.For<ActionManager>(), buffManager);
            var frame = new BattleFrame();

            //action
            buffManager.AddBuffer(unit, 1, 1);
            buffManager.Update(frame);

            //expect
            buffer.Received(1).Update(frame);
            buffer.Received(1).OnDettach();
        }

        [Test]
        public void TesAddAtkUpBuffAndAtkChanged()
        {
            //arrange
            string bufferUID = "1";
            var buffDataSource = Substitute.For<BufferDataSource>();
            var buffFactory = Substitute.For<BufferFactory>(buffDataSource);
            //var buffer = new DurationBuffer("1", 1, 1, new float[] {1});
            var buffer = new BufferAttackUp(bufferUID, 1, 1, new float[] { 1f, 10f });// Substitute.For<BufferAttackUp>(bufferUID, 1, 1, new float[] { 1f });            
            buffFactory.Create(Arg.Any<int>(), Arg.Any<int>()).Returns(buffer);
            var buffManager = new BaseBufferManager(buffDataSource, buffFactory);
            var unit = new BattleUnit(new BattleUnitInfo() { atk = 5 }, Substitute.For<ActionManager>(), buffManager);
            var frame = new BattleFrame();

            //action
            buffManager.AddBuffer(unit, 1, 1);

            //expect
            Assert.AreEqual(15, unit.Atk);
        }

        [Test]
        public void TestExecuteAddBufferAndBufferAddedToUnit()
        {
            //arrange
            var addBufferExecutor = Substitute.For<ExecutorTargetAddBuffer>(new float[6] { 1, 0, 0, 1, 1 ,1});


            var buffer = Substitute.For<IBuffer>();

            List<IBuffer> buffers = new List<IBuffer>();

            var buffManager = Substitute.For<BaseBufferManager>(null, null);
            buffManager.When(x => x.AddBuffer(Arg.Any<IBattleUnit>(), Arg.Any<int>(), Arg.Any<int>()))
                .Do(x => buffers.Add(buffer));

            var target = Substitute.For<IBattleUnit>();

            target.When(x => x.AddBuffer(Arg.Any<int>(), Arg.Any<int>()))
                .Do(x => { buffManager.AddBuffer(target, 1, 1); Console.WriteLine("11111"); });

            var targets = new List<IBattleUnit>() { target };

            addBufferExecutor.When(x => x.Hit(null, null, targets))
                .Do(x => target.AddBuffer(1, 1));

            //action
            addBufferExecutor.Hit(null, null, targets);


            //expect
            Assert.AreEqual(1, buffers.Count);
        }

        //[Test]
        //public void TestExecuteAddBufferAndReportAddedBuffer()
        //{
        //    //arrange
        //    var cfg = Substitute.For<ActionDataSource>(battle);
        //    cfg.GetConditionTriggerArg(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1);
        //    cfg.GetConditionTriggerType(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1);
        //    cfg.GetFinderType(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(1);
        //    cfg.GetExcutorTypes(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new List<int>() { 1, 2 });
        //    cfg.GetExcutorArg(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new float[6] { 1, 0, 0, 999, 1,1 });

        //    var buffData = Substitute.For<BufferDataSource>();
        //    buffData.GetArgs(Arg.Any<int>()).Returns(new float[] { 10f, 0.1f });

        //    var timer = new JFrameTimerUtils();
        //    //action
        //    battle.Initialize(attacker, defence, cfg, buffData, null);
        //    var lstData = battle.GetResult();
        //    //var reporter = battle.GetReporter();
        //    //var lstData = reporter.GetAllReportData();

        //    //expect
        //    Assert.AreEqual(4, lstData.report.Count);
        //    var unit3 = battle.GetUnit(PVPBattleManager.Team.Defence, 3);
        //    Assert.AreEqual(0, unit3.HP);

        //    var unit1 = battle.GetUnit(PVPBattleManager.Team.Attacker, 1);
        //    Assert.AreEqual(20, unit1.HP);

        //    var unit2 = battle.GetUnit(PVPBattleManager.Team.Attacker, 2);
        //    Assert.AreEqual(16, unit2.HP);
        //}

        

        
    }

    
}
