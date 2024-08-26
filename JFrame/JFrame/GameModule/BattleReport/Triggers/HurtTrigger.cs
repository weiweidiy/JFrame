using System;

namespace JFrame
{
    /// <summary>
    /// type 6  立即触发型，
    /// </summary>
    public class HurtTrigger : BaseBattleTrigger
    {
        float hitRate;
        public HurtTrigger(IPVPBattleManager battleManager, float[] args, float delay = 0) : base(battleManager, args, delay)
        {
            if(args.Length < 1)
            {
                throw new System.Exception("HurtTrigger 需要1个参数");
            }

            hitRate = args[0];
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

        private void Owner_onDamaging(IBattleUnit caster, IBattleAction action, IBattleUnit target, ExecuteInfo Info)
        {
            var r = new Random().NextDouble();
            if (r < hitRate)
            {
                NotifyTriggerOn(this, new object[] { action, target, Info });
            }
            
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