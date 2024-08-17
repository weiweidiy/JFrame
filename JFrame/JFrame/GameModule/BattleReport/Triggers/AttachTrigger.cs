namespace JFrame
{

    /// <summary>
    /// type 5
    /// </summary>
    public class AttachTrigger : BaseBattleTrigger
    {
        public AttachTrigger(IPVPBattleManager battleManager, float[] args, float delay = 0) : base(battleManager, args, delay)
        {
        }

        public override void OnAttach(IAttachOwner target)
        {
            base.OnAttach(target);

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