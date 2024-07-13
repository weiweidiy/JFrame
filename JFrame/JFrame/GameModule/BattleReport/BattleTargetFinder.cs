using System.Collections.Generic;

namespace JFrame
{
    public abstract class BattleTargetFinder
    {
        public abstract List<IBattleUnit> FindTargets();
    }
}