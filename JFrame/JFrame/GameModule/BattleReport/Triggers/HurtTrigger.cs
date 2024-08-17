namespace JFrame
{
    /// <summary>
    /// type 6
    /// </summary>
    public class HurtTrigger : BaseBattleTrigger
    {
        public HurtTrigger(IPVPBattleManager battleManager, float[] args, float delay = 0) : base(battleManager, args, delay)
        {
        }



        public override void OnAttach(IAttachOwner target)
        {
            base.OnAttach(target);

            Owner.Owner.onDamaging += Owner_onDamaging; 
        }

        public override void OnDetach()
        {
            base.OnDetach();

            Owner.Owner.onDamaging -= Owner_onDamaging; 
        }

        private void Owner_onDamaging(IBattleUnit arg1, IBattleAction arg2, IBattleUnit arg3, ExecuteInfo Info)
        {
            NotifyTriggerOn(this, Info);
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