namespace JFrame
{
    /// <summary>
    /// 等待触发状态
    /// </summary>
    public class ActionStandbyState : BaseActionState
    {
        public override void Update(BattleFrame frame)
        {
            base.Update(frame);
        }

        protected override void OnEnter()
        {
            base.OnEnter();

            //context.reset
        }
    }
}
