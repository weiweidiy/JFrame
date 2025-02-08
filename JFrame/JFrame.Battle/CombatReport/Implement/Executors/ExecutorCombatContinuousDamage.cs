namespace JFrame
{
    /// <summary>
    /// 连续傷害执行器，可以打多次 type = 2 參數0：持續時間 1：傷害加成  2：間隔  3：次數
    /// </summary>
    public class ExecutorCombatContinuousDamage : ExecutorCombatDamage
    {

        float delta = 0f;



        public ExecutorCombatContinuousDamage(ICombatFinder combinFinder) : base(combinFinder)
        {
        }

        public override void Update(BattleFrame frame)
        {
           // base.Update(frame);

            if (!isExecuting)
                return;

            if (count >= GetCountArg())
            {
                isExecuting = false;
                return;
            }
                
            delta += frame.DeltaTime;
            if (delta >= GetIntervalArg())
            {
                delta = 0f;
                Hit();
            }
        }

        protected float GetIntervalArg()
        {
            return GetCurArg(2);
        }

        protected override float GetCountArg()
        {
            return GetCurArg(3);
        }

        public override void Reset()
        {
            base.Reset();
            delta = 0f;
        }
    }
}