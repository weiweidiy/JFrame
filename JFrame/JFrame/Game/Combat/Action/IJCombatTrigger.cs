using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatTrigger
    {
        bool IsTriggerOn(IJCombatQuery query, out List<IJCombatUnit> targets);
    }
}
