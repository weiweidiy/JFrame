namespace JFrame
{
    public class NoneTrigger : BaseBattleTrigger
    {
        public NoneTrigger(IPVPBattleManager pVPBattleManager, float[] duration, float delay = 0f) : base(pVPBattleManager, duration, delay) { }



        /// <summary>
        /// 延迟完成
        /// </summary>
        protected override void OnDelayCompleteEveryFrame()
        {
            base.OnDelayCompleteEveryFrame();

            SetOn(true);
        }
    }


    public class NewNoneTrigger : NewBattleTrigger
    {
        public NewNoneTrigger(IPVPBattleManager pVPBattleManager, float[] duration, float delay = 0f) : base(pVPBattleManager, duration, delay) { }



        /// <summary>
        /// 延迟完成
        /// </summary>
        protected override void OnDelayCompleteEveryFrame()
        {
            base.OnDelayCompleteEveryFrame();

            SetOn(true);
        }
    }
}