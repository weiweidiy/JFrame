namespace JFrame
{
    /// <summary>
    /// type 10: 反射伤害：參數：0：执行時間 1：傷害加成 
    /// </summary>
    public class ExecutorCombatTurnBackDamage : ExecutorCombatNormal
    {
        public override int GetValidArgsCount()
        {
            return 2;
        }

        protected float GetRateArg()
        {
            return GetCurArg(1);
        }

        public ExecutorCombatTurnBackDamage(CombatBaseFinder combinFinder, CombatBaseFormula formulua) : base(combinFinder, formulua)
        {
        }

        protected override void SetValueType(CombatExtraData data)
        {
            data.ValueType = CombatValueType.TurnBackDamage;
        }

        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            var attr = (double)data.Caster.GetAttributeCurValue(CombatAttribute.FightBackCoef);
            data.Value = data.ExtraArg * (GetRateArg() + attr);
            target.OnDamage(data);
        }

        protected override double GetExecutorValue()
        {
            return GetRateArg();
        }
    }
}