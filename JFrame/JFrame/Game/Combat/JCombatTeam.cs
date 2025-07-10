using JFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JFramework.Game
{
    public class JCombatTeam : RunableDictionaryContainer<IJCombatUnit>, IJCombatTeam
    {
        public string Uid { get; protected set; }

        public JCombatTeam(string uid, List<IJCombatUnit> units,  Func<IJCombatOperatable, string> keySelector) : base(keySelector)
        {
            AddRange(units);
            this.Uid = uid;
        }


        public List<IJCombatOperatable> GetAllUnits()
        {
            return GetAll().OfType<IJCombatOperatable>().ToList();
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
            var units = GetAll();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    unit.Start(extraData);
                }
            }
        }

        protected override void OnStop()
        {
            base.OnStop();

            var units = GetAll();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    unit.Stop();
                }
            }
        }
    }
}
