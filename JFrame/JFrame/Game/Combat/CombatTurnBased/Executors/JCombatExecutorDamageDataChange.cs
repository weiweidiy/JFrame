using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    /// <summary>
    /// 伤害提升执行器，一定是JCombatBeforeDamageTrigger触发的
    /// </summary>
    public class JCombatExecutorDamageDataChange : JCombatExecutorBase
    {
        public class ExecutorArgs : IJCombatExecutorArgs
        {
            // 可以添加一些额外的参数，如果需要的话
        }
        public JCombatExecutorDamageDataChange(IJCombatFilter filter, IJCombatTargetsFinder finder, IJCombatFormula formulua, float[] args) : base(filter,finder, formulua, args)
        {
        }

        protected override int GetValidArgsCount()
        {
            return 0; // 只需要一个参数，通常是伤害值或相关系数
        }


        protected override IJCombatExecutorArgs DoExecute(IJCombatTriggerArgs triggerArgs, IJCombatExecutorArgs executorArgs, IJCombatCasterTargetableUnit target)
        {
            var damageData = triggerArgs as DamageTriggerArgs;
            if (damageData == null)
            {
                return new ExecutorArgs();
            }

            var value = (float)damageData.DamageData.GetDamage();
            formulua.CalcHitValue(null, ref value); // 假设伤害提升20%
            damageData.DamageData.SetDamage((int)value);
            return new ExecutorArgs();
        }
    }
}
