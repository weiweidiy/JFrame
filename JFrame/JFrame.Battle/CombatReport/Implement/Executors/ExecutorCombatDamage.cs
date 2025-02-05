using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 傷害執行器 type = 1 參數：0：持續時間 1：傷害加成
    /// </summary>
    public class ExecutorCombatDamage : CombatBaseExecutor
    {
        protected int count = 0;


        public ExecutorCombatDamage(ICombatFinder combinFinder) : base(combinFinder)
        {
        }

        public override void Reset()
        {
            base.Reset();
            count = 0;
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
            List<CombatUnit> targets = GetTargets(extraData);
            extraData.Value = (long)(extraData.Value * GetAtkRate());

            var d = extraData.Clone() as CombatExtraData;
            //即将命中
            NotifyHittingTargets(d);

            foreach (var target in targets)
            {
                var data = extraData.Clone() as CombatExtraData;
                data.Target = target;
                NotifyHittingTarget(data); //即将命中单个单位
                target.OnDamage(data);
                NotifyTargetHittedComplete(d);
            }
            //命中完成了
            NotifyTargetsHittedComplete(extraData);
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