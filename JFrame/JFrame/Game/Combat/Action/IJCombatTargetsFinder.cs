using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatTargetsFinder
    {
        List<IJCombatUnit> GetTargets(IJCombatQuery query);
    }
}
