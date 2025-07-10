using JFramework;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTeam : RunableDictionaryContainer<IJCombatOperatable>, IJCombatTeam
    {
        public JCombatTeam(string uid, List<IJCombatOperatable> units,  Func<IJCombatOperatable, string> keySelector) : base(keySelector)
        {
            AddRange(units);
            this.Uid = uid;
        }

        public string Uid { get; protected set; }


        public List<IJCombatOperatable> GetAllUnits()
        {
            return GetAll();
        }

        public IJCombatOperatable GetUnit(string uid)
        {
            return Get(uid);
        }

        public bool IsAllDead()
        {
            var allUnits = GetAllUnits();

            foreach (var unit in allUnits)
            {
                if (!unit.IsDead())
                    return false;
            }

            return true;
        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);
            var units = GetAllUnits();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    unit.OnStart();
                }
            }
        }

        protected override void OnStop()
        {
            base.OnStop();

            var units = GetAllUnits();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    unit.OnStop();
                }
            }
        }
    }
}
