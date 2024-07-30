using System.Collections.Generic;
using System;

namespace JFrame
{
    public interface IActionManager
    {
        event Action<IBattleAction, List<IBattleUnit>, float> onStartCast;

        bool IsBusy { get; }

        void Update(BattleFrame frame);

        void Initialize(IBattleUnit owner);

        void OnDead();
    }
}