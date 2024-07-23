using System;

namespace JFrame
{
    public interface IBattleTrigger
    {
        event Action onTrigger;

        IBattleAction Owner { get; }

        void SetEnable(bool isOn);

        void Update(BattleFrame frame);

        void OnAttach(IBattleAction action);
    }
}