using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 找对手1个单位 type = 1
    /// </summary>
    public class FinderOneOppo : BaseFinder
    {
        public override List<ICombatUnit> FindTargets(CombatExtraData extraData)
        {
            var lst = GetNearestOppoUnitInRange(extraData, GetFindAmount());
            return lst;
        }       
    }
}