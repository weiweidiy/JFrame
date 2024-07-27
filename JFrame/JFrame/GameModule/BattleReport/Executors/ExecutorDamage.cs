
using System.Collections.Generic;

namespace JFrame
{

    /// <summary>
    /// 伤害效果 参数  1：执行段数，2：延迟执行 3: 段数间隔  4 ：伤害倍率  type = 1
    /// </summary>
    public class ExecutorDamage : BaseExecutor
    { 
        /// <summary>
        /// 伤害倍率
        /// </summary>
        protected float arg = 1f;


        /// <summary>
        /// 伤害效果，1：执行段数，2：延迟执行 3: 段数间隔 4：伤害倍率
        /// </summary>
        /// <param name="args"></param>
        public ExecutorDamage(float[] args):base(args)
        {
            if (args != null && args.Length >= 4)
            {
                arg = args[3];
            }
            else
            {
                throw new System.Exception( this.GetType().ToString() + " 参数数量不对 缺少伤害倍率参数" );
            }
        }

        /// <summary>
        /// 执行命中
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="targets"></param>
        /// <param name="reporter"></param>
        public override void Hit(IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets)
        {
            foreach(var target in targets) {

                var dmg = GetValue(caster, action, target);
                //to do: unit.getbuffvalue(bufftype, dmg) 返回最终受伤值
                target.OnDamage(caster, action, new IntValue() { Value = (int)dmg });
            }

        }

        /// <summary>
        /// 获取执行效果的值
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            return caster.Atk * arg;
        }
    }
}