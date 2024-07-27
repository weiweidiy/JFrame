using System;
using System.Collections.Generic;
using System.Linq;

namespace JFrame
{

    public class BattleTeam : IBattleTeam
    {
        public event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, List<IBattleUnit>> onActionTriggerOn;
        public event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, List<IBattleUnit>> onActionCast;
        public event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit> onActionDone;

        public event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit, int> onDamage;
        public event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit, int> onHeal;
        public event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit> onDead;
        public event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit, int> onReborn;

        public event Action<PVPBattleManager.Team, IBattleUnit, IBuffer> onBufferAdded;
        public event Action<PVPBattleManager.Team, IBattleUnit, IBuffer> onBufferRemoved;
        public event Action<PVPBattleManager.Team, IBattleUnit, IBuffer> onBufferCast;

        Dictionary<BattlePoint, IBattleUnit> units = new Dictionary<BattlePoint, IBattleUnit>();

        PVPBattleManager.Team team;
        public PVPBattleManager.Team Team { get => team; }

        public BattleTeam(PVPBattleManager.Team team, Dictionary<BattlePoint, IBattleUnit> units)
        {
            this.team = team;
            this.units = units;

            if(this.units != null)
            {
                foreach (var key in units.Keys)
                {
                    var unit = units[key];
                    unit.onActionTriggerOn += Unit_onActionTriggerOn;
                    unit.onActionCast += Unit_onActionCast;
                    unit.onActionHitTarget += Unit_onActionDone;
                    unit.onDamage += Unit_onDamage;
                    unit.onHeal += Unit_onHeal;
                    unit.onRebord += Unit_onRebord;
                    unit.onDead += Unit_onDead;
                    unit.onBufferAdded += Unit_onBufferAdded;
                    unit.onBufferRemoved += Unit_onBufferRemoved;
                    unit.onBufferCast += Unit_onBufferCast;
                }
            }

        }




        #region 响应事件
        private void Unit_onActionTriggerOn(IBattleUnit arg1, IBattleAction arg2, List<IBattleUnit> arg3)
        {
            onActionTriggerOn?.Invoke(team, arg1, arg2, arg3);
        }

        private void Unit_onActionCast(IBattleUnit arg1, IBattleAction arg2, List<IBattleUnit> arg3)
        {
            onActionCast?.Invoke(team, arg1, arg2, arg3);
        }


        private void Unit_onActionDone(IBattleUnit arg1, IBattleAction arg2, IBattleUnit arg3)
        {
            onActionDone?.Invoke(team, arg1, arg2, arg3);
        }

        private void Unit_onDamage(IBattleUnit arg1, IBattleAction arg2, IBattleUnit arg3, int arg4)
        {
            onDamage?.Invoke(team, arg1, arg2, arg3, arg4);
        }

        private void Unit_onHeal(IBattleUnit arg1, IBattleAction arg2, IBattleUnit arg3, int arg4)
        {
            onHeal?.Invoke(team,arg1, arg2, arg3, arg4);
        }

        private void Unit_onRebord(IBattleUnit arg1, IBattleAction arg2, IBattleUnit arg3, int arg4)
        {
            onReborn?.Invoke(team, arg1, arg2, arg3, arg4);
        }


        private void Unit_onDead(IBattleUnit arg1, IBattleAction arg2, IBattleUnit arg3)
        {
            onDead?.Invoke(team, arg1, arg2, arg3);
        }

        private void Unit_onBufferAdded(IBattleUnit arg1, IBuffer arg2)
        {
            onBufferAdded?.Invoke(team, arg1, arg2);
        }
        private void Unit_onBufferCast(IBattleUnit arg1, IBuffer arg2)
        {
            onBufferCast?.Invoke(team, arg1, arg2);
        }
        private void Unit_onBufferRemoved(IBattleUnit arg1, IBuffer arg2)
        {
            onBufferRemoved?.Invoke(team, arg1, arg2);
        }

        #endregion

        /// <summary>
        /// 添加一个单位
        /// </summary>
        /// <param name="point"></param>
        /// <param name="unit"></param>
        public void AddUnit(BattlePoint point, IBattleUnit unit)
        {
            if (units == null)
                units = new Dictionary<BattlePoint, IBattleUnit>();

            units.Add(point, unit);
        }


        /// <summary>
        /// 获取指定位置战斗单位
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IBattleUnit GetUnit(BattlePoint point)
        {
            if (units.ContainsKey(point))
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
            foreach (var btPoint in units.Keys)
            {
                if (btPoint.Point == point)
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
            var collection = GetUnits();
            if(collection == null)
                return;

            foreach (var unit in collection)
            {
                unit.Update(frame);
            }
        }

        public int GetUnitCount()
        {
            return units.Count;
        }

        public bool IsAllDead()
        {
            foreach (var unit in units.Values)
            {
                if (unit.IsAlive())
                    return false;
            }

            return true;
        }


    }
}