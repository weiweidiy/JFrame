using System.Collections.Generic;

namespace JFrame
{
    public interface IBattleTargetFinder
    {
        List<IBattleUnit> FindTargets();
    }
}