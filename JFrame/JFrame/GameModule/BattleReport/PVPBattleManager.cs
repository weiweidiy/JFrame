using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace JFrame
{

    /// <summary>
    /// pvp战斗管理
    /// </summary>
    public class PVPBattleManager
    {
        public enum Team
        {
            Attacker,
            Defence
        }

        /// <summary>
        /// 战斗日志
        /// </summary>
        BattleReporter pvpReporter = new BattleReporter();

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
        /// 配置表
        /// </summary>
        ActionConfig cfgAction = null;

        #region 响应方法：战斗规则
        /// <summary>
        /// 有动作准备好了
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void BattleTeam_onActionTriggerOn(Team team, IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets)
        {
            var uids = new List<string>();
            foreach (var target in targets)
            {
                uids.Add(target.UID);
            }

            var reportUID = pvpReporter.AddReportMainData(frame.CurFrame, frame.GetDeltaTime(frame.CurFrame), caster.UID, action.Id, uids);
            action.Cast(caster, targets, pvpReporter, reportUID);
        }

        /// <summary>
        /// 动作生效了, 单个目标调用1次，AOE在1次action中会调用多次
        /// </summary>
        /// <param name="team"></param>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        private void BattleTeam_onActionDone(Team team, IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            //Console.WriteLine("Frame " + frame.CurFrame + "~ 队伍：" + team.ToString() + "~ 单位 ： " + caster.Name + " ~对目标 " + target.Name + " ~执行动作：" + action.Name);
        }



        #endregion

        #region Get方法

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
        public List<IBattleUnit> GetUnits(Team team)
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
        public List<BattleReportData> GetResult()
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

            //Debug.Log("战斗结束 " + frame.FrameCount);
            return pvpReporter.GetAllReportData();
        }

        /// <summary>
        /// 获取战报
        /// </summary>
        /// <returns></returns>
        public BattleReporter GetReporter()
        {
            return pvpReporter;
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
        public void Initialize(Dictionary<BattlePoint, BattleUnitInfo> attacker, Dictionary<BattlePoint, BattleUnitInfo> defence, ActionConfig cfgAction)
        {
            this.cfgAction = cfgAction;
            teams.Clear();
            var attackTeam = CreateTeam(Team.Attacker, attacker);
            teams.Add(Team.Attacker, attackTeam);
            var defenceTeam = CreateTeam(Team.Defence, defence);
            teams.Add(Team.Defence, defenceTeam);

            battleResult = new BattleResult(attackTeam, defenceTeam);
        }

        /// <summary>
        /// 初始化队伍数据
        /// </summary>
        /// <param name="units"></param>
        BattleTeam CreateTeam(Team team, Dictionary<BattlePoint, BattleUnitInfo> units)
        {
            var dicUnits = new Dictionary<BattlePoint, IBattleUnit>();
            foreach (var slot in units.Keys)
            {
                var info = units[slot];
                var battleUnit = new BattleUnit(info, CreateActions(info.actionsId, slot));
                //battleUnit.onActionReady += BattleUnit_onActionReady;
                dicUnits.Add(slot, battleUnit);
            }

            var battleTeam = new BattleTeam(team, dicUnits);
            battleTeam.onActionTriggerOn += BattleTeam_onActionTriggerOn;
            battleTeam.onActionDone += BattleTeam_onActionDone;
            return battleTeam;
        }

        /// <summary>
        /// 根据id创建技能实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<IBattleAction> CreateActions(List<int> ids, BattlePoint battlePoint)
        {
            var actions = new List<IBattleAction>();
            foreach (var id in ids)
            {
                //debug 全是普攻,周期触发
                BattleTrigger trigger = CreateTrigger(cfgAction.GetTriggerType(id), cfgAction.GetTriggerArg(id), 0f);
                IBattleTargetFinder finder = CreateTargetFinder(cfgAction.GetFinderType(id), battlePoint);
                var action = new NormalAttack(id, trigger, finder);
                actions.Add(action);
            }
            return actions;
        }

        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        BattleTrigger CreateTrigger(int triggerType, float arg, float delay = 0)
        {
            switch (triggerType)
            {
                case 1:
                    return new CDTrigger(arg, delay);
                default:
                    throw new Exception(triggerType + " 技能未实现的action triiger " + triggerType);
            }
        }

        /// <summary>
        /// 创建目标搜索器
        /// </summary>
        /// <param name="finderType"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        IBattleTargetFinder CreateTargetFinder(int finderType, BattlePoint point)
        {
            switch (finderType)
            {
                case 1:
                    return new NormalTargetFinder(point, this);
                default:
                    throw new Exception("没有实现目标finder type " + finderType);
            }
        }
        #endregion

    }
}