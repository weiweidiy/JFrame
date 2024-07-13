using System.Collections.Generic;

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


        BattleReport pvpReport = new BattleReport();

        BattleResult battleResult = new BattleResult();

        BattleFrame frame = new BattleFrame();

        Dictionary<Team, BattleTeam> teams = new Dictionary<Team, BattleTeam>();

        /// <summary>
        /// 配置表
        /// </summary>
        ActionConfig cfgAction = new ActionConfig();

        #region 响应方法：战斗规则
        /// <summary>
        /// 有动作准备好了
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void BattleUnit_onActionReady(IBattleUnit unit, IBattleAction action, List<IBattleUnit> targets)
        {
            action.Cast(targets);
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
            return teams[team].GetUnitCount();
        }

        /// <summary>
        /// 获取战斗播报对象
        /// </summary>
        /// <returns></returns>
        public BattleReport GetResult()
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
            return pvpReport;
        }
        #endregion

        #region 帮助方法
        /// <summary>
        /// 初始化战斗
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defence"></param>
        public void Initialize(Dictionary<BattlePoint, BattleUnitInfo> attacker, Dictionary<BattlePoint, BattleUnitInfo> defence)
        {
            teams.Clear();
            var attackTeam = CreateTeam(Team.Attacker, attacker);
            teams.Add(Team.Attacker, attackTeam);
            var defenceTeam = CreateTeam(Team.Defence, defence);
            teams.Add(Team.Defence, defenceTeam);
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
                battleUnit.onActionReady += BattleUnit_onActionReady;
                dicUnits.Add(slot, battleUnit);
            }

            var battleTeam = new BattleTeam(dicUnits);
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
            foreach(var id in ids)
            {
                //debug 全是普攻,周期触发
                BattleTrigger trigger = new CDTrigger(cfgAction.GetDuration(id));
                NormalTargetFinder finder = new NormalTargetFinder(battlePoint, this);
                var action = new NormalAttack(trigger, finder);
                actions.Add(action);
            }
            return actions;
        }
        #endregion

    }
}