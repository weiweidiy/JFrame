using System;
using System.Collections.Generic;
using System.Linq;

namespace JFrame
{

    public class BattleTeam
    {
        public event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, List<IBattleUnit>> onActionTriggerOn;
        public event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit> onActionDone;

        Dictionary<BattlePoint, IBattleUnit> units = new Dictionary<BattlePoint, IBattleUnit>();

        PVPBattleManager.Team team;

        public BattleTeam(PVPBattleManager.Team team,  Dictionary<BattlePoint, IBattleUnit> units)
        {
            this.team = team;
            this.units = units;

            foreach(var key in units.Keys)
            {
                var unit = units[key];
                unit.onActionTriggerOn += Unit_onActionTriggerOn;
                unit.onActionDone += Unit_onActionDone;
            }
        }


        #region 响应事件
        private void Unit_onActionTriggerOn(IBattleUnit arg1, IBattleAction arg2, List<IBattleUnit> arg3)
        {
            onActionTriggerOn?.Invoke(team, arg1, arg2, arg3);
        }

        private void Unit_onActionDone(IBattleUnit arg1, IBattleAction arg2, IBattleUnit arg3)
        {
            onActionDone?.Invoke(team,arg1, arg2,arg3);
        }
        #endregion
        /// <summary>
        /// 获取指定位置战斗单位
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IBattleUnit GetUnit(BattlePoint point)
        {
            if(units.ContainsKey(point)) 
                return units[point];

            throw new Exception("没有找到对应的点位 " + point.Point);
        }

        /// <summary>
        /// 获取指定位置单位
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IBattleUnit GetUnit(int point)
        {
            foreach(var btPoint in units.Keys)
            {
                if(btPoint.Point == point) 
                    return units[btPoint];
            }

            throw new Exception("没有找到对应的点位 " + point);
        }

        /// <summary>
        /// 获取所有战斗对象
        /// </summary>
        /// <returns></returns>
        public List<IBattleUnit> GetUnits()
        {
            return units.Values.ToList();
        }

        public void Update(BattleFrame frame)
        {
            foreach (var unit in units.Values)
            {
                (unit as BattleUnit).Update(frame);
            }
        }

        public int GetUnitCount()
        {
            return units.Count;
        }

        public bool IsAllDead()
        {
            foreach(var unit in units.Values)
            {
                if (unit.IsAlive())
                    return false;
            }

            return true;
        }
    }
}