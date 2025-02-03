using System;

namespace JFrame
{
    public abstract class BaseExecutor : BaseActionComponent, ICombatExecutor
    {
        public event Action<CombatExtraData> onHittingTarget;
        protected void NotifyHitting(CombatExtraData extraData) => onHittingTarget?.Invoke(extraData);

        public event Action<CombatExtraData> onHittedComplete;
        protected void NotifyHitted(CombatExtraData extraData) => onHittedComplete?.Invoke(extraData);

        public abstract void Execute(CombatExtraData extraData);
    }
}