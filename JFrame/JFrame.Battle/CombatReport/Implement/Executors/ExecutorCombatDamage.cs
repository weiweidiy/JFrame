using System;

namespace JFrame
{

    /// <summary>
    /// 普通傷害執行器 只能打1次 type = 1 參數：0：执行時間 1：傷害加成
    /// </summary>
    public class ExecutorCombatDamage : ExecutorCombatNormal
    {
        public ExecutorCombatDamage(ICombatFinder combinFinder) : base(combinFinder)
        {
        }

        protected override long GetValue()
        {
            return extraData.Value = (long)(extraData.Value * GetAtkRateArg());
        }

        protected float GetAtkRateArg()
        {
            return GetCurArg(1);
        }

        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            target.OnDamage(data);
        }
    }
}