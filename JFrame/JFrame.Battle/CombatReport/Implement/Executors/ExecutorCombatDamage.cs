using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 傷害執行器 type = 1 參數：0：持續時間 1：傷害加成
    /// </summary>
    public class ExecutorCombatDamage : BaseExecutor
    {
        protected int count = 0;
        protected bool isExecuting;
        protected CombatExtraData extraData;

        public ExecutorCombatDamage(ICombatFinder combinFinder) : base(combinFinder)
        {
        }

        public override void Execute(CombatExtraData extraData)
        {
            this.extraData = extraData;
            isExecuting = true;
        }

        public override void Update(BattleFrame frame)
        {
            base.Update(frame);

            if (!isExecuting)
                return;

            if (count >= GetCount())
            {
                isExecuting = false;
                return;
            }

            DoDamge();
        }

        protected void DoDamge()
        {
            List<ICombatUnit> targets = GetTargets(extraData);
            extraData.Value = (long)(extraData.Value * GetAtkRate());
            foreach (var target in targets)
            {
                target.OnDamage(extraData);
            }
            count++;
        }

        protected float GetAtkRate()
        {
            return GetCurArg(1);
        }

        protected virtual float GetCount()
        {
            return 1;
        }
    }
}