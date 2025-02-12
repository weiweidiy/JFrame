using System.Collections.Generic;

namespace JFrame
{
    public interface ICombatFinder
    {
        List<CombatUnit> FindTargets(CombatExtraData extraData);
    }
}