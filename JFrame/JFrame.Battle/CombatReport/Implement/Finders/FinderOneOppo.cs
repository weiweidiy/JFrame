using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 找对手1个单位 type = 1
    /// </summary>
    public class FinderOneOppo : CombatBaseFinder
    {
        public override List<CombatUnit> FindTargets(CombatExtraData extraData)
        {
            var lst = GetNearestOppoUnitInRange(extraData, GetFindAmount());
            return lst;
        }       
    }
}