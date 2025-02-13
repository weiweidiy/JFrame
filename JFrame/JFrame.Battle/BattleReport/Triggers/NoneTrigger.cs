namespace JFrame
{

    /// <summary>
    /// type = 1
    /// </summary>
    public class NoneTrigger : BaseBattleTrigger
    {
        public NoneTrigger(IPVPBattleManager pVPBattleManager, float[] duration, float delay = 0f) : base(pVPBattleManager, duration, delay) { }



        /// <summary>
        /// 延迟完成
        /// </summary>
        protected override void OnDelayCompleteEveryFrame(ComabtFrame frame)
        {
            base.OnDelayCompleteEveryFrame(frame);

            SetOn(true);
        }
    }
}

//public class NoneTrigger : BaseBattleTrigger
//{
//    public NoneTrigger(IPVPBattleManager pVPBattleManager, float[] duration, float delay = 0f) : base(pVPBattleManager, duration, delay) { }



//    /// <summary>
//    /// 延迟完成
//    /// </summary>
//    protected override void OnDelayCompleteEveryFrame()
//    {
//        base.OnDelayCompleteEveryFrame();

//        SetOn(true);
//    }
//}