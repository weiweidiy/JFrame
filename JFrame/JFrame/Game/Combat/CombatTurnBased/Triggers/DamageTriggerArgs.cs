using System.Collections.Generic;

namespace JFramework.Game
{
    public class DamageTriggerArgs : IJCombatTriggerArgs
    {
        public IJCombatDamageData DamageData { get; set; }

        public List<IJCombatCasterTargetableUnit> GetTargets()
        {
            var targets = new List<IJCombatCasterTargetableUnit>();
            return targets;
        }
    }


}
