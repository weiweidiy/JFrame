namespace JFrame
{

    /// <summary>
    /// 普通傷害執行器 只能打1次 type = 1 參數：0：执行時間 1：傷害加成
    /// </summary>
    public class ExecutorCombatDamage : ExecutorCombatNormal
    {
        public override int GetValidArgsCount()
        {
            return 2;
        }

        protected float GetRateArg()
        {
            return GetCurArg(1);
        }

        public ExecutorCombatDamage(CombatBaseFinder combinFinder , CombatBaseFormula formulua) : base(combinFinder, formulua)
        {
        }

        protected override double GetExecutorValue()
        {
            return  GetRateArg();
        }

        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            target.OnDamage(data);
        }

        protected override void SetValueType(CombatExtraData data)
        {
            data.ValueType = CombatValueType.Damage;
        }
    }
}