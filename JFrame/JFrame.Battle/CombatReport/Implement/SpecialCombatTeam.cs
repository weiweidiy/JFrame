using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 獲取所有單位時，只會返回特殊的單位
    /// </summary>
    public class SpecialCombatTeam : CommonCombatTeam
    {
        public override List<CombatUnit> GetUnits()
        {
            return base.GetUnits();
        }
    }
}