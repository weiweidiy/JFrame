using JFramework;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTeam : DictionaryContainer<IJCombatOperatable>, IJCombatTeam
    {
        public JCombatTeam(string uid, List<IJCombatOperatable> units,  Func<IJCombatOperatable, string> keySelector) : base(keySelector)
        {
            AddRange(units);
            this.Uid = uid;
        }

        public string Uid { get; protected set; }

        public List<IJCombatOperatable> GetAllUnit()
        {
            return GetAll();
        }

        public IJCombatOperatable GetUnit(string uid)
        {
            return Get(uid);
        }

        public bool IsAllDead()
        {
            var allUnits = GetAllUnit();

            foreach (var unit in allUnits)
            {
                if (!unit.IsDead())
                    return false;
            }

            return true;
        }
    }
}
