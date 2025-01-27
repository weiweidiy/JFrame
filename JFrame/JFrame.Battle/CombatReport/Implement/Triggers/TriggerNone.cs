using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 空触发器
    /// </summary>
    public class TriggerNone : BaseActionComponent, ICombatTrigger
    {
        public event Action<CombatExtraData> onTriggerOn;

        CombatExtraData extraData;
        public TriggerNone()
        {
            extraData = new CombatExtraData();
        }

        public override void Update(BattleFrame frame)
        {
            onTriggerOn?.Invoke(new CombatExtraData());
        }
    }

    /// <summary>
    /// 找对手1个单位
    /// </summary>
    public class FinderOneOppo : BaseActionComponent, ICombatFinder
    {
        public List<ICombatUnit> FindTargets(CombatExtraData extraData)
        {
            throw new NotImplementedException();
        }

        public override void Update(BattleFrame frame)
        {
            throw new NotImplementedException();
        }
    }
}