namespace JFrame
{
    /// <summary>
    /// 加血执行器type = 3：参数：0 执行周期 , 1 : 加成
    /// </summary>
    public class ExecutorCombatHeal : ExecutorCombatNormal
    {
        public ExecutorCombatHeal(ICombatFinder combinFinder, ICombatFormula formula) : base(combinFinder, formula)
        {
        }

        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            target.OnHeal(data);
        }

        protected override double GetExecutorValueRate()
        {
            return  GetRateArg();
        }

        /// <summary>
        /// 获取加成参数
        /// </summary>
        /// <returns></returns>
        protected float GetRateArg()
        {
            return GetCurArg(1);
        }

        public override int GetValidArgsCount()
        {
            return 2;
        }
    }
}