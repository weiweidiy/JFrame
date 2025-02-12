namespace JFrame
{
    /// <summary>
    /// 参数：0 执行周期 1=attrId, 2=倍率
    /// </summary>
    public class ExecutorAttribute : ExecutorCombatNormal
    {
        public override int GetValidArgsCount()
        {
            return 2;
        }

        protected float GetAttrIdArg()
        {
            return (int)GetCurArg(1);
        }

        protected float GetRateArg()
        {
            return GetCurArg(2);
        }

        public ExecutorAttribute(ICombatFinder combinFinder, ICombatFormula formula) : base(combinFinder, formula)
        {
        }

        protected override double GetExecutorValueRate()
        {
            return GetRateArg();
        }

        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            var item = target.GetAttributeCurValue((PVPAttribute)GetAttrIdArg());
            var attr = item as CombatAttributeDouble;
            var finalValue = data.Value * attr.CurValue;

            target.AddExtraValue((PVPAttribute)GetAttrIdArg(), data.Action.Uid, finalValue);
        }
    }
}