using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 普通执行器 只能固定打1次 参数：0 执行周期
    /// </summary>
    public abstract class ExecutorCombatNormal : CombatBaseExecutor
    {
        protected int count = 0;


        public ExecutorCombatNormal(ICombatFinder combinFinder, ICombatFormula formula) : base(combinFinder, formula)
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

        protected override void OnUpdate(CombatFrame frame)
        {
            base.OnUpdate(frame);

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

            var ExecutorExtraData = extraData.Clone() as CombatExtraData;

            //double baseValue = 1;
            //获取数值
            ExecutorExtraData.Value = /*baseValue **/ GetExecutorValue()  *  extraData.FoldCount;

            //即将命中
            NotifyHittingTargets(ExecutorExtraData);

            foreach (var target in targets)
            {
                var data = ExecutorExtraData.Clone() as CombatExtraData;
                data.Target = target;
                if (formula != null)
                {
                    var miss = !formula.IsHit(data);
                    if (miss)
                        data.Value = 0;
                    else
                        data.Value = formula.GetHitValue(data);
                }
   
                SetValueType(data);
                NotifyHittingTarget(data); //即将命中单个单位
                DoHit(target, data);
                NotifyTargetHittedComplete(data);
            }
            //命中完成了
            NotifyTargetsHittedComplete(ExecutorExtraData);
            count++;
        }

        /// <summary>
        /// 设置本次执行的类型：伤害，治疗，反击等
        /// </summary>
        /// <param name="data"></param>
        protected abstract void SetValueType(CombatExtraData data);

        /// <summary>
        /// 执行参数倍率
        /// </summary>
        /// <returns></returns>
        protected abstract double GetExecutorValue();

        

        protected abstract void DoHit(CombatUnit target, CombatExtraData data);
    }
}