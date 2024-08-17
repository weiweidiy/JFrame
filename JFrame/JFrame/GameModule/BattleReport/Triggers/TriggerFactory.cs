using System;

namespace JFrame
{
    ///// <summary>
    ///// 触发器工厂
    ///// </summary>
    //public class TriggerFactory
    //{
    //    public IBattleTrigger Create(PVPBattleManager pvpBattleManager, int triggerType, float arg, float delay = 0)
    //    {
    //        switch (triggerType)
    //        {
    //            case 1: //无
    //                return new NoneTrigger(pvpBattleManager, new float[1] { arg }, delay);
    //            case 2: //自身死亡触发
    //                return new DeathTrigger(pvpBattleManager, new float[1] { arg }, delay);
    //            case 3: //战斗开始触发
    //                return new BattleStartTrigger(pvpBattleManager, new float[1] { arg }, delay);
    //            case 4: //己方有非满血成员
    //                return new FriendsHurtTrigger(pvpBattleManager, new float[1] { arg }, delay);
    //            case 5: //指定action释放
    //                return new ActionCastTrigger(pvpBattleManager, new float[1] { arg }, delay);
    //            default:
    //                throw new Exception(triggerType + " 技能未实现的 ConditionTrigger type " + triggerType);
    //        }
    //    }
    //}

    public class TriggerFactory
    {
        public IBattleTrigger Create(PVPBattleManager pvpBattleManager, int triggerType, float[] args, float delay = 0)
        {
            switch (triggerType)
            {
                case 1: //无
                    return new NoneTrigger(pvpBattleManager, args, delay);
                case 2: //自身死亡触发
                    return new DeathTrigger(pvpBattleManager, args, delay);
                case 3: //战斗开始触发
                    return new BattleStartTrigger(pvpBattleManager, args, delay);
                case 4: //己方有非满血成员
                    return new FriendsHurtTrigger(pvpBattleManager, args, delay);
                case 5:
                    return new AttachTrigger(pvpBattleManager, args, delay);
                case 6:
                    return new HurtTrigger(pvpBattleManager, args, delay);
                case 8:
                    return new CDTimeTrigger(pvpBattleManager, args, delay);
                case 9:
                    return new AmountTrigger(pvpBattleManager, args, delay);
                case 100: //指定action释放
                    return new ActionCastTrigger(pvpBattleManager, args, delay);
                default:
                    throw new Exception(triggerType + " 技能未实现的 ConditionTrigger type " + triggerType);
            }
        }
    }
}