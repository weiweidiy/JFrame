
using System;

namespace JFrame
{
    /// <summary>
    /// 治疗效果 参数  1：执行段数，2：延迟执行 3: 段数间隔  4 ：加血量（加值）  type = 3
    /// </summary>
    public class ExecutorHeal : ExecutorDamage
    {
        public ExecutorHeal(float[] args) : base(args) { }

        public override float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            return arg; //是个加值
        }

        public override void Hit(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            var heal = GetValue(caster, action, target);
            //to do: unit.getbuffvalue(bufftype, dmg) 返回最终受伤值
            var needHp = target.MaxHP - target.HP;

            heal = Math.Min(heal, needHp);

            target.OnHeal(caster, action, new IntValue() { Value = (int)heal });
        }
    }
}