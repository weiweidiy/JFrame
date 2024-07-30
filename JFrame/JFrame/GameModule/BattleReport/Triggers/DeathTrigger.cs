using System;

namespace JFrame
{
    /// <summary>
    /// 本体死亡触发，只能1次  arg : 没用  type = 2
    /// </summary>
    public class DeathTrigger : BaseBattleTrigger
    {
        public DeathTrigger( IPVPBattleManager pvpBattleManager, float[] arg, float delay = 0):base(pvpBattleManager, arg, delay)
        { }

        protected override void OnDelayCompleteEveryFrame()
        {
            base.OnDelayCompleteEveryFrame();

            if (!Owner.Owner.IsAlive())
            {
                isOn = true;
            }
            else
                isOn = false;
        }

    }
}