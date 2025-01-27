using System.Collections.Generic;

namespace JFrame
{
    public interface ICombatFinder
    {
        List<ICombatUnit> FindTargets(CombatExtraData extraData);
    }


}