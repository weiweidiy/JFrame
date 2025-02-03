using System;
using System.Collections.Generic;

namespace JFrame
{
    public class CommonCombatTeam : BaseContainer<ICombatUnit>, ICombatTeam<ICombatUnit, ICombatAction, ICombatBuffer> , ICombatUpdatable
    {
        public event Action<int, ICombatUnit, ICombatAction, List<ICombatUnit>, float> onActionCast;
        public event Action<int, ICombatUnit, ICombatAction, float> onActionStartCD;

        public event Action<int, CombatExtraData> onDamage;
        public event Action<int, ICombatUnit, ICombatAction, ICombatUnit, int> onHeal;
        public event Action<int, CombatExtraData> onDead;
        public event Action<int, ICombatUnit, ICombatAction, ICombatUnit, int> onReborn;
        public event Action<int, ICombatUnit, ICombatAction, ICombatUnit, int> onMaxHpUp;
        public event Action<int, ICombatUnit, ICombatAction, ICombatUnit, int> onDebuffAnti;

        public event Action<int, ICombatUnit, ICombatBuffer> onBufferAdded;
        public event Action<int, ICombatUnit, ICombatBuffer> onBufferRemoved;
        public event Action<int, ICombatUnit, ICombatBuffer> onBufferCast;
        public event Action<int, ICombatUnit, ICombatBuffer, int, float[]> onBufferUpdate;

        int team;
        public int TeamId => team;

        public void AddUnit(ICombatUnit unit)
        {
            Add(unit);
        }

        public void RemoveUnit(ICombatUnit unit)
        {
            Remove(unit.Uid);
        }

        public ICombatUnit GetUnit(string uid)
        {
            return Get(uid);
        }

        public int GetUnitCount()
        {
            return Count();
        }

        public List<ICombatUnit> GetUnits()
        {
            return GetAll();
        }

        public void Update(BattleFrame frame)
        {
            var units = GetUnits();
            if (units == null)
                return;

            foreach (var unit in units)
            {
                (unit as ICombatUpdatable).Update(frame);
            }
        }

        public virtual bool IsAllDead()
        {
            foreach (var unit in GetUnits())
            {
                if (unit.IsAlive())
                    return false;
            }

            return true;
        }


        public void Initialize(CombatContext context)
        {
            var units = GetUnits();
            if (units != null)
            {
                foreach (var unit in units)
                {

                    //(unit as CombatUnit).Initialize(context);
                    //unit.onActionTriggerOn += Unit_onActionTriggerOn;
                    unit.onActionCast += Unit_onActionCast;
                    unit.onActionStartCD += Unit_onActionStartCD;
                    //unit.onActionHitTarget += Unit_onActionDone;
                    unit.onDamaged += Unit_onDamage;
                    unit.onHealed += Unit_onHeal;
                    unit.onRebord += Unit_onRebord;
                    unit.onDebuffAnti += Unit_onDebuffAnti;
                    unit.onMaxHpUp += Unit_onMaxHpUp;
                    unit.onDead += Unit_onDead;
                    unit.onBufferAdded += Unit_onBufferAdded;
                    unit.onBufferRemoved += Unit_onBufferRemoved;
                    unit.onBufferCast += Unit_onBufferCast;
                    unit.onBufferUpdate += Unit_onBufferUpdate;
                }
            }
        }

        #region 响应事件


        private void Unit_onActionCast(ICombatUnit arg1, ICombatAction arg2, List<ICombatUnit> arg3, float duration)
        {
            onActionCast?.Invoke(team, arg1, arg2, arg3, duration);
        }
        private void Unit_onActionStartCD(ICombatUnit arg1, ICombatAction arg2, float arg3)
        {
            onActionStartCD?.Invoke(team, arg1, arg2, arg3);
        }

        private void Unit_onDamage(CombatExtraData extraData /*ICombatUnit arg1, ICombatAction arg2, ICombatUnit arg3, ExecuteInfo arg4*/)
        {
            onDamage?.Invoke(team, extraData);
        }

        private void Unit_onHeal(ICombatUnit arg1, ICombatAction arg2, ICombatUnit arg3, int arg4)
        {
            onHeal?.Invoke(team, arg1, arg2, arg3, arg4);
        }

        private void Unit_onMaxHpUp(ICombatUnit arg1, ICombatAction arg2, ICombatUnit arg3, int arg4)
        {
            onMaxHpUp?.Invoke(team, arg1, arg2, arg3, arg4);
        }

        private void Unit_onRebord(ICombatUnit arg1, ICombatAction arg2, ICombatUnit arg3, int arg4)
        {
            onReborn?.Invoke(team, arg1, arg2, arg3, arg4);
        }


        private void Unit_onDead(CombatExtraData extraData)
        {
            onDead?.Invoke(team, extraData);
        }

        private void Unit_onBufferAdded(ICombatUnit arg1, ICombatBuffer arg2)
        {
            onBufferAdded?.Invoke(team, arg1, arg2);
        }
        private void Unit_onBufferCast(ICombatUnit arg1, ICombatBuffer arg2)
        {
            onBufferCast?.Invoke(team, arg1, arg2);
        }
        private void Unit_onBufferRemoved(ICombatUnit arg1, ICombatBuffer arg2)
        {
            onBufferRemoved?.Invoke(team, arg1, arg2);
        }

        private void Unit_onBufferUpdate(ICombatUnit arg1, ICombatBuffer arg2, int arg3, float[] arg4)
        {
            onBufferUpdate?.Invoke(team, arg1, arg2, arg3, arg4);
        }
        private void Unit_onDebuffAnti(ICombatUnit arg1, ICombatAction arg2, ICombatUnit arg3, int arg4)
        {
            onDebuffAnti?.Invoke(team, arg1, arg2, arg3, arg4);
        }


        #endregion
    }
}