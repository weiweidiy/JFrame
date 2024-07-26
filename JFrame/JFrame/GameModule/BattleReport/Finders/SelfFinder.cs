using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 本体（不管死活） type = 6
    /// </summary>
    public class SelfFinder : BaseTargetFinder
    {
        public SelfFinder(BattlePoint selfPoint, IPVPBattleManager manger, float arg) : base(selfPoint, manger, arg)
        {
        }

        public override List<IBattleUnit> FindTargets()
        {
            return new List<IBattleUnit>() { Owner.Owner };
        }
    }


}