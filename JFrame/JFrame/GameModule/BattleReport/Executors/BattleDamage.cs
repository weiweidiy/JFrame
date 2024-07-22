
namespace JFrame
{
    /// <summary>
    /// 伤害效果
    /// </summary>
    public class BattleDamage : BaseBattleExecutor
    { 
        /// <summary>
        /// 伤害倍率
        /// </summary>
        float dmgRate = 1f;


        /// <summary>
        /// 伤害效果，1：执行段数，2：延迟执行 3: 段数间隔 4：伤害倍率
        /// </summary>
        /// <param name="args"></param>
        public BattleDamage(float[] args):base(args)
        {
            if (args != null && args.Length >= 4)
            {
                dmgRate = args[3];
            }
            else
            {
                dmgRate= 1f;
            }
        }

        /// <summary>
        /// 执行命中
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="target"></param>
        /// <param name="reporter"></param>
        public override void Hit(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            var dmg = GetValue(caster,action,target);
            //to do: unit.getbuffvalue(bufftype, dmg) 返回最终受伤值
            target.OnDamage(caster, action, (int)dmg);
        }

        /// <summary>
        /// 获取执行效果的值
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            return caster.Atk * dmgRate;
        }
    }
}