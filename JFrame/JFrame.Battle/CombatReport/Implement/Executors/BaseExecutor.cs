using System;
using System.Collections.Generic;

namespace JFrame
{
    public abstract class BaseExecutor : BaseActionComponent, ICombatExecutor
    {
        public event Action<CombatExtraData> onHittingTarget;
        protected void NotifyHitting(CombatExtraData extraData) => onHittingTarget?.Invoke(extraData);

        public event Action<CombatExtraData> onHittedComplete;
        protected void NotifyHitted(CombatExtraData extraData) => onHittedComplete?.Invoke(extraData);

        protected ICombatFinder finder;

        public BaseExecutor(ICombatFinder combinFinder)
        {
            this.finder = combinFinder;
        }

        public abstract void Execute(CombatExtraData extraData);

        /// <summary>
        /// 獲取執行周期
        /// </summary>
        /// <returns></returns>
        protected float GetDuration()
        {
            return GetCurArg(0);
        }

        /// <summary>
        /// 獲取目標
        /// </summary>
        /// <param name="extraData"></param>
        /// <returns></returns>
        protected List<ICombatUnit> GetTargets(CombatExtraData extraData)
        {
            List<ICombatUnit> targets = new List<ICombatUnit>();
            if (finder != null)
            {
                targets = finder.FindTargets(extraData);
            }
            else
            {
                if (extraData.Targets == null || extraData.Targets.Count == 0)
                    return targets;

                targets = extraData.Targets;
            }

            return targets;
        }
    }
}