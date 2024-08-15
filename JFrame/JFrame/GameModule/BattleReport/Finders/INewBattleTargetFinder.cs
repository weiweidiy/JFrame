using System.Collections.Generic;

namespace JFrame
{
    public interface INewBattleTargetFinder : IAttachable
    {
        List<IBattleUnit> FindTargets();
    }
}