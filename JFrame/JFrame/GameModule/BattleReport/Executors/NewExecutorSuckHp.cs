
namespace JFrame
{
    /// <summary>
    /// 吸血 参数  1：执行段数，2：延迟执行 3: 段数间隔  4 ：伤害倍率  5 : 回血倍率 type = 9
    /// </summary>
    public class NewExecutorSuckHp : NewExecutorDamage
    {
        float suckRate;
        public NewExecutorSuckHp(FormulaManager formulaManager, float[] args) : base(formulaManager, args)
        {
            if (args.Length >= 5)
            {
                suckRate = args[4];
            }
            else
                throw new System.Exception("SuckHp 执行器参数数量不对");
        }

        protected override void OnTargetHit(IBattleUnit caster, IBattleAction action, IBattleUnit target, ExecuteInfo info)
        {
            base.OnTargetHit(caster, action, target, info);

            //吸血
            var damage = info.Value;
            var healValue = damage * suckRate;

            caster.OnHeal(caster, action, new ExecuteInfo() { Value = (int)healValue });
        }
    }
}