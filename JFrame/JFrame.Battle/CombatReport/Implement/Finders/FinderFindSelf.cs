using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 找自己 type 1  
    /// </summary>
    public class FinderFindSelf : CombatBaseFinder
    {
        public override List<CombatUnit> FindTargets(CombatExtraData extraData)
        {
            var result = new List<CombatUnit>();

            result.Add(extraData.Caster);

            return result;
        }

        public override int GetValidArgsCount()
        {
            return 0;
        }
    }
}