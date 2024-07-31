
namespace JFrame
{

    /// <summary>
    /// 伤害效果 参数  1：执行段数，2：延迟执行 3: 段数间隔  4 ：伤害倍率  type = 4
    /// </summary>
    public class ExecutorHpDamage : ExecutorDamage
    {
        public ExecutorHpDamage(FormulaManager formulaManager, float[] args) : base(formulaManager, args) { }

        public override float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            return target.MaxHP * arg;
        }
    }
}