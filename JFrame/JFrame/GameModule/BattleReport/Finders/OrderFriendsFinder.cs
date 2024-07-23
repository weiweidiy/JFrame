using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 队友顺序寻找存活目标（可复数） type = 7
    /// </summary>
    public class OrderFriendsFinder : BaseTargetFinder
    {
        public OrderFriendsFinder(BattlePoint selfPoint, PVPBattleManager manger, float arg) : base(selfPoint, manger, arg)
        {
        }

        public override List<IBattleUnit> FindTargets()
        {
            var result = new List<IBattleUnit>();

            var units = manger.GetUnits(selfPoint.Team);

            //debug
            foreach (var unit in units)
            {
                if (unit.IsAlive())
                {
                    result.Add(unit);
                    if (result.Count >= arg)
                        return result;
                }
            }

            return result;
        }
    }


}