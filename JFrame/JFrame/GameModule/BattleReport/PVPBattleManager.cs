using System.Collections.Generic;
using System.Net.Http.Headers;
using static JFrame.PVPBattleManager;
using static System.Collections.Specialized.BitVector32;

namespace JFrame
{
    /// <summary>
    /// pvp战斗管理
    /// </summary>
    public class PVPBattleManager : IPVPBattleManager
    {
        public enum Team
        {
            Attacker,
            Defence
        }

        /// <summary>
        /// 战斗日志
        /// </summary>
        IBattleReporter pvpReporter;

        /// <summary>
        /// 最终的战报对象
        /// </summary>
        PVPBattleReport report = new PVPBattleReport();

        /// <summary>
        /// 战斗结果
        /// </summary>
        BattleResult battleResult;

        /// <summary>
        /// 帧
        /// </summary>
        BattleFrame frame = new BattleFrame();

        /// <summary>
        /// 战斗队伍
        /// </summary>
        Dictionary<Team, BattleTeam> teams = new Dictionary<Team, BattleTeam>();

        /// <summary>
        /// 动作配置表
        /// </summary>
        ActionDataSource dataSource = null;

        /// <summary>
        /// buff数据源
        /// </summary>
        BufferDataSource bufferDataSource = null;

        /// <summary>
        /// 公式管理
        /// </summary>
        FormulaManager formulaManager;


        #region 响应方法：战斗规则
        ///// <summary>
        ///// 有动作准备好了
        ///// </summary>
        ///// <param name="arg1"></param>
        ///// <param name="arg2"></param>
        ///// <param name="arg3"></param>
        ///// <exception cref="System.NotImplementedException"></exception>
        //private void BattleTeam_onActionTriggerOn(Team team, IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets)
        //{
        //    //var uids = new List<string>();
        //    //foreach (var target in targets)
        //    //{
        //    //    uids.Add(target.UID);
        //    //}
        //    //var reportUID = pvpReporter.AddReportActionData(caster.UID, action.Id, uids);
        //    //action.Cast(caster, targets, pvpReporter);
        //}

        ///// <summary>
        ///// 动作生效了, 单个目标调用1次，AOE在1次action中会调用多次
        ///// </summary>
        ///// <param name="team"></param>
        ///// <param name="caster"></param>
        ///// <param name="action"></param>
        ///// <param name="target"></param>
        //private void BattleTeam_onActionDone(Team team, IBattleUnit caster, IBattleAction action, IBattleUnit target)
        //{
        //    //Console.WriteLine("Frame " + frame.CurFrame + "~ 队伍：" + team.ToString() + "~ 单位 ： " + caster.Name + " ~对目标 " + target.Name + " ~执行动作：" + action.Name);
        //}



        #endregion

        #region Get方法

        /// <summary>
        /// 战斗最大时长
        /// </summary>
        /// <returns></returns>
        public float GetBattleTimeLimit()
        {
            return frame.AllTime;
        }
        /// <summary>
        /// 获取指定位置单位
        /// </summary>
        /// <param name="team"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public IBattleUnit GetUnit(Team team, int point)
        {
            var battleTeam = GetTeam(team);
            return battleTeam.GetUnit(point);
        }

        /// <summary>
        /// 获取所有战斗对象
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public virtual List<IBattleUnit> GetUnits(Team team)
        {
            var battleTeam = GetTeam(team);
            return battleTeam.GetUnits();
        }

        /// <summary>
        /// 获取指定队伍对象
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public BattleTeam GetTeam(Team team)
        {
            return teams[team];
        }

        /// <summary>
        /// 获取队伍单位数量
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public int GetUnitCount(Team team)
        {
            return GetTeam(team).GetUnitCount();
        }

        /// <summary>
        /// 获取敌对队伍
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public Team GetOppoTeam(Team team)
        {
            switch (team)
            {
                case Team.Attacker:
                    return Team.Defence;
                case Team.Defence:
                    return Team.Attacker;
                default:
                    throw new System.Exception("没有实现队伍类型 " + team);

            }
        }

        /// <summary>
        /// 获取战斗播报对象
        /// </summary>
        /// <returns></returns>
        public PVPBattleReport GetResult()
        {
            //如果战斗没有决出胜负，则继续战斗
            while (!battleResult.IsOver() && !frame.IsMaxFrame())
            {
                foreach (var team in teams.Values)
                {
                    team.Update(frame);
                }

                frame.NextFrame();
            }

            report.report = pvpReporter.GetAllReportData();
            report.winner = battleResult.GetWinner().Team == Team.Attacker ? 1 : 0 ; //1:挑战成功 0：挑战失败
            //Debug.Log("战斗结束 " + frame.FrameCount);
            return report;
        }

        /// <summary>
        /// 获取战报
        /// </summary>
        /// <returns></returns>
        public IBattleReporter GetReporter()
        {
            return pvpReporter;
        }

        /// <summary>
        /// 获取所有队伍
        /// </summary>
        /// <returns></returns>
        public Dictionary<Team, BattleTeam> GetTeams()
        {
            return teams;
        }
        #endregion

        #region 帮助方法

        /// <summary>
        /// 更新一帧
        /// </summary>
        public void Update()
        {
            foreach (var team in teams.Values)
            {
                team.Update(frame);
            }

            frame.NextFrame();
        }

        /// <summary>
        /// 初始化战斗
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defence"></param>
        public void Initialize(Dictionary<BattlePoint, BattleUnitInfo> attacker, Dictionary<BattlePoint, BattleUnitInfo> defence, ActionDataSource dataSource, BufferDataSource bufferDataSource, IBattleReporter reporter, FormulaManager formulaManager)
        {
            this.formulaManager = formulaManager;
            this.dataSource = dataSource;
            this.bufferDataSource = bufferDataSource;
            teams.Clear();
            var attackTeam = CreateTeam(Team.Attacker, attacker);
            teams.Add(Team.Attacker, attackTeam);
            var defenceTeam = CreateTeam(Team.Defence, defence);
            teams.Add(Team.Defence, defenceTeam);

            attackTeam.Initialize();
            defenceTeam.Initialize();

            battleResult = new BattleResult(attackTeam, defenceTeam);
            pvpReporter = reporter == null?  new BattleReporter(frame, teams) : reporter ;
            report.attacker = attacker;
            report.defence = defence;
        }

        public void Release()
        {
            battleResult = null;
            pvpReporter = null;
        }


        /// <summary>
        /// 添加一个队伍
        /// </summary>
        /// <param name="team"></param>
        /// <param name="teamObj"></param>
        public void AddTeam(Team team, BattleTeam teamObj)
        {
            teams.Add(team, teamObj);
        }

        /// <summary>
        /// 初始化队伍数据
        /// </summary>
        /// <param name="units"></param>
        public virtual BattleTeam CreateTeam(Team team, Dictionary<BattlePoint, BattleUnitInfo> units)
        {
            var dicUnits = new Dictionary<BattlePoint, IBattleUnit>();
            foreach (var slot in units.Keys)
            {
                var info = units[slot];
                var battleUnit = new BattleUnit(info, CreateActionManager(info, info.actionsId, slot), CreateBufferManager(slot));
                //battleUnit.onActionReady += BattleUnit_onActionReady;
                dicUnits.Add(slot, battleUnit);
            }

            var battleTeam = new BattleTeam(team, dicUnits);
            //battleTeam.onActionTriggerOn += BattleTeam_onActionTriggerOn;
            //battleTeam.onActionDone += BattleTeam_onActionDone;
            return battleTeam;
        }

        /// <summary>
        /// 创建 buff管理器
        /// </summary>
        /// <returns></returns>
        BaseBufferManager CreateBufferManager(BattlePoint battlePoint)
        {
            return new BaseBufferManager(bufferDataSource, new BufferFactory(bufferDataSource, this, battlePoint, formulaManager));
        }


        ActionManager CreateActionManager(BattleUnitInfo unitInfo , List<int> actionIds, BattlePoint battlePoint)
        {
            string unitUID = unitInfo.uid;
            int unitId = unitInfo.id;

            var manager = new ActionManager();
            var factory = new ActionFactory(unitInfo, dataSource, battlePoint, this, formulaManager);

            //var actions = new List<IBattleAction>();
            foreach (var actionId in actionIds)
            {
                // to do: 根据actionID,创建不同的类实例
                var action = factory.Create(actionId);
                manager.Add(action);
                //actions.Add(action);
            }
            return manager;
        }

        /// <summary>
        /// 获取自身队伍
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public virtual Team GetFriendTeam(IBattleUnit unit)
        {
            var atkTeam =  GetTeam(Team.Attacker);
            BattlePoint point = null;
            point = atkTeam.GetPoint(unit);
            if (point != null) return point.Team;

            var defTeam = GetTeam(Team.Defence);
            point = defTeam.GetPoint(unit);
            if (point != null) return point.Team;

            throw new System.Exception("没有找到指定单位的队伍 " + unit.Name);
        }

        ///// <summary>
        ///// 根据id创建技能实例
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //List<IBattleAction> CreateActions(string unitUID, int unitId, List<int> actionIds, BattlePoint battlePoint)
        //{
        //    var factory = new ActionFactory(unitUID, unitId, dataSource, battlePoint,this);

        //    var actions = new List<IBattleAction>();
        //    foreach (var actionId in actionIds)
        //    {
        //        // to do: 根据actionID,创建不同的类实例
        //        var action = factory.Create(actionId);
        //        actions.Add(action);
        //    }
        //    return actions;
        //}



        #endregion

    }
}