using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatTargetsFinder : IJCombatActionComponent
    {
        List<IJCombatOperatable> GetTargets(/*IJCombatQuery query*/);
    }
}
