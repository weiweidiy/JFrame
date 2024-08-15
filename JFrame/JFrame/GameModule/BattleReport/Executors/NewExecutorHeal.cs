
using System;
using System.Collections.Generic;

namespace JFrame
{
    public class NewExecutorHeal : NewExecutorDamage
    {
        public NewExecutorHeal(FormulaManager formulaManager, float[] args) : base(formulaManager, args) { }

        public override float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            return caster.MaxHP * arg;
        }

        public override void Hit(IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets)
        {
            foreach (var target in targets)
            {
                var heal = GetValue(caster, action, target);
                //to do: unit.getbuffvalue(bufftype, dmg) 返回最终受伤值
                var needHp = target.MaxHP - target.HP;

                heal = Math.Min(heal, needHp);

                target.OnHeal(caster, action, new ExecuteInfo() { Value = (int)heal });
            }

        }
    }
}