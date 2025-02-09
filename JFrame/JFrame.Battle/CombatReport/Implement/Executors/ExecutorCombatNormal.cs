using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 普通执行器 只能固定打1次 参数：0 执行周期
    /// </summary>
    public abstract class ExecutorCombatNormal : CombatBaseExecutor
    {
        protected int count = 0;


        public ExecutorCombatNormal(ICombatFinder combinFinder) : base(combinFinder)
        {
        }


        protected virtual float GetCountArg()
        {
            return 1;
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

            if (count >= GetCountArg())
            {
                isExecuting = false;
                return;
            }
            Hit();
        }

        /// <summary>
        /// 命中
        /// </summary>
        protected void Hit()
        {
            List<CombatUnit> targets = GetTargets(extraData);

            var d = extraData.Clone() as CombatExtraData;
            //获取数值
            d.Value = GetValue();    
            //即将命中
            NotifyHittingTargets(d);

            foreach (var target in targets)
            {
                var data = d.Clone() as CombatExtraData;
                data.Target = target;
                NotifyHittingTarget(data); //即将命中单个单位
                DoHit(target, data);
                NotifyTargetHittedComplete(d);
            }
            //命中完成了
            NotifyTargetsHittedComplete(extraData);
            count++;
        }

        protected abstract long GetValue();

        protected abstract void DoHit(CombatUnit target, CombatExtraData data);
    }
}