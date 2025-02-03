using System;

namespace JFrame
{
    /// <summary>
    /// 傷害執行器 type = 1
    /// </summary>
    public class ExecutorCombatDamage : BaseExecutor
    {
        public override void Execute(CombatExtraData extraData)
        {
            if (extraData.Targets == null || extraData.Targets.Count == 0)
                return;

            extraData.Value = (long)(extraData.Value * GetAtkRate());
            var targets = extraData.Targets;
            foreach (var target in targets)
            {
                target.OnDamage(extraData);
            }
        }

        protected float GetAtkRate()
        {
            return GetCurArg(0);
        }
    }
}