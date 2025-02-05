namespace JFrame
{
    /// <summary>
    /// 冷却状态
    /// </summary>
    public class ActionCdingState : BaseActionState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            context.ResetCdTriggers();
            context.NotifyStartCD();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Update(BattleFrame frame)
        {
            base.Update(frame);

            context.UpdateCdTriggers(frame);
            context.UpdateExecutors(frame); //执行器会继续执行

            if (context.IsCdTriggerOn())
            {
                context.SwitchToTrigging();
            }
        }


    }
}
