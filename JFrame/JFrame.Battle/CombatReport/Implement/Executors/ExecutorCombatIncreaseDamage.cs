namespace JFrame
{
    /// <summary>
    /// type 8 : 普通傷害執行器 只能打1次 type = 1 參數：0：执行時間 1：傷害加成  2: 递增倍率 3 递增最大次数
    /// </summary>
    public class ExecutorCombatIncreaseDamage : ExecutorCombatDamage
    {
        int increaseCount = 0;
        public ExecutorCombatIncreaseDamage(ICombatFinder combinFinder, ICombatFormula formulua) : base(combinFinder, formulua)
        {
        }


        public override int GetValidArgsCount()
        {
            return 4;
        }

        public float GetIncreaseRate()
        {
            return GetCurArg(2);
        }

        public float GetIncreaseMaxCount()
        {
            return GetCurArg(3);
        }

        protected override double GetExecutorValue()
        {
            var result = base.GetExecutorValue() * (1 + GetIncreaseRate() * increaseCount);
            if (increaseCount <= GetIncreaseMaxCount())
                increaseCount++;
            return result;
        }

        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            base.DoHit(target, data);
        }



    }
}