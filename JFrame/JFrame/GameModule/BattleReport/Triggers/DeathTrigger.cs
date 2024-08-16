using System;

namespace JFrame
{


    public class DeathTrigger : BaseBattleTrigger
    {
        bool hited;

        public DeathTrigger(IPVPBattleManager pvpBattleManager, float[] arg, float delay = 0) : base(pvpBattleManager, arg, delay)
        { }

        public override BattleTriggerType TriggerType => BattleTriggerType.AfterDead;




        protected override void OnDelayCompleteEveryFrame()
        {
            base.OnDelayCompleteEveryFrame();

            var owner = Owner as IBattleAction;
            if (owner == null)
                throw new Exception("attach owner 转换失败 ");

            if (!owner.Owner.IsAlive() && !hited)
            {
                SetOn(true);
                hited = true;
            }

        }

    }
}

///// <summary>
///// 本体死亡触发，只能1次  arg : 没用  type = 2
///// </summary>
//public class DeathTrigger : BaseBattleTrigger
//{
//    bool hited;

//    public DeathTrigger( IPVPBattleManager pvpBattleManager, float[] arg, float delay = 0):base(pvpBattleManager, arg, delay)
//    { }

//    public override BattleTriggerType TriggerType => BattleTriggerType.AfterDead;




//    protected override void OnDelayCompleteEveryFrame()
//    {
//        base.OnDelayCompleteEveryFrame();

//        if (!Owner.Owner.IsAlive() && !hited)
//        {
//            SetOn(true);
//            hited = true;
//        }

//    }

//}