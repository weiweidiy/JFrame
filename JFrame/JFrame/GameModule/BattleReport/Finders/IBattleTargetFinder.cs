using System.Collections.Generic;

namespace JFrame
{
    public interface IBattleTargetFinder
    {
        IBattleAction Owner { get; }

        List<IBattleUnit> FindTargets();

        void OnAttach(IBattleAction action);
    }
}