using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 顺序寻找目标（可复数）
    /// </summary>
    public class OrderTargetFinder : BaseTargetFinder
    {

        public OrderTargetFinder(BattlePoint selfPoint, PVPBattleManager manger, float arg) : base(selfPoint, manger, arg) { }

        /// <summary>
        /// 获取攻击目标
        /// </summary>
        /// <returns></returns>
        public override List<IBattleUnit> FindTargets()
        {
            var result = new List<IBattleUnit>();

            var units = manger.GetUnits(manger.GetOppoTeam(selfPoint.Team));

            //debug
            foreach (var unit in units)
            {
                if (unit.IsAlive())
                {
                    result.Add(unit);
                    if (result.Count >= this.arg)
                        return result;
                }
            }

            return result;
        }
    }


}