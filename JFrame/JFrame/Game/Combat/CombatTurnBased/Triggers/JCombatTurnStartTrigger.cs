using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnStartTrigger : JCombatTriggerBase<int>
    {
        public override void OnTurnStart(int frame)
        {
            base.OnTurnStart(frame);

            TriggerOn(frame);
        }
    }
}
