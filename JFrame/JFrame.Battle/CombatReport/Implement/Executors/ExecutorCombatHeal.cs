namespace JFrame
{
    /// <summary>
    /// 加血执行器type = 3：参数：0 执行周期 , 1 : 加成
    /// </summary>
    public class ExecutorCombatHeal : ExecutorCombatNormal
    {
        public ExecutorCombatHeal(ICombatFinder combinFinder) : base(combinFinder)
        {
        }

        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            target.OnHeal(data);
        }

        protected override long GetValue()
        {
            return extraData.Value = (long)(extraData.Value * GetRateArg());
        }

        /// <summary>
        /// 获取加成参数
        /// </summary>
        /// <returns></returns>
        protected float GetRateArg()
        {
            return GetCurArg(1);
        }
    }
}