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


        public void Initialize(Dictionary<int, BattleUnitInfo> attacker, Dictionary<int, BattleUnitInfo> defence)
        {
            teams.Clear();
            InitTeam(Team.Attacker, attacker);
            InitTeam(Team.Defence, defence);
        }

        /// <summary>
        /// 初始化队伍数据
        /// </summary>
        /// <param name="units"></param>
        void InitTeam(Team team, Dictionary<int, BattleUnitInfo> units)
        {
            var dicUnits = new Dictionary<int, BattleUnit>();
            foreach (var slot in units.Keys)
            {
                var info = units[slot];
                var battleUnit = new BattleUnit(info);
                dicUnits.Add(slot, battleUnit);
            }

            var battleTeam = new BattleTeam(dicUnits);
            teams.Add(team, battleTeam);
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
    }
}