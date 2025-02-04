namespace JFrame
{
    /// <summary>
    /// 持續傷害 type = 2 參數0：持續時間 1：傷害加成  2：間隔  3：次數
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

            if (count >= GetCount())
            {
                isExecuting = false;
                return;
            }
                
            delta += frame.DeltaTime;
            if (delta >= GetInterval())
            {
                delta = 0f;
                DoDamge();
            }
        }

        protected float GetInterval()
        {
            return GetCurArg(2);
        }

        protected override float GetCount()
        {
            return GetCurArg(3);
        }
    }
}