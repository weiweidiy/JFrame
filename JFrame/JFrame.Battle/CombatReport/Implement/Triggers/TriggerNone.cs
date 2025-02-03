using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 空触发器 type = 1
    /// </summary>
    public class TriggerNone : BaseTrigger
    {
        public TriggerNone(List<ICombatFinder> finders) : base(finders)
        {
        }

        public override void Update(BattleFrame frame)
        {
            if (finders == null)
                return;

            foreach (ICombatFinder finder in finders)
            {
                var targets = finder.FindTargets(extraData);
                if (targets != null && targets.Count > 0)
                {
                    extraData.Targets = targets;
                    NotifyTriggerOn(extraData);
                }
            }
        }
    }
}