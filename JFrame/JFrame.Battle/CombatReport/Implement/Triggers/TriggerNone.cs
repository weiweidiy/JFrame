using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 空触发器
    /// </summary>
    public class TriggerNone : BaseCombatTrigger, ICombatTrigger
    {
        public TriggerNone(List<ICombatFinder> finders) : base(finders)
        {
        }
    }
}